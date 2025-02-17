#nullable enable

using System;
using System.Linq;
using StateSmith.Common;
using StateSmith.SmGraph;
using StateSmith.Input.Antlr4;
using StateSmith.Input.Expansions;
using StateSmith.Output.Gil;
using System.Collections.Generic;

namespace StateSmith.Output.Algos.Balanced1;

public class EventHandlerBuilder
{
    public string smAccess = "this";

    public const string consumeEventVarName = "consume_event";
    private readonly NameMangler mangler;
    private readonly PseudoStateHandlerBuilder pseudoStateHandlerBuilder;
    private readonly WrappingExpander wrappingExpander;

    private OutputFile? _file;

    private OutputFile File => _file.ThrowIfNull("You forgot to set file before using.");

    public EventHandlerBuilder(Expander expander, PseudoStateHandlerBuilder pseudoStateHandlerBuilder, NameMangler mangler)
    {
        this.pseudoStateHandlerBuilder = pseudoStateHandlerBuilder;
        this.mangler = mangler;
        this.wrappingExpander = new(expander);
    }

    public void SetFile(OutputFile file)
    {
        _file = file;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    /// <param name="triggerName"></param>
    public void OutputStateBehaviorsForTrigger(NamedVertex state, string triggerName)
    {
        bool noAncestorHandlesEvent = true;

        if (TriggerHelper.IsEvent(triggerName))
        {
            noAncestorHandlesEvent = OutputNextAncestorHandler(state, triggerName);
        }

        var behaviorsWithTrigger = TriggerHelper.GetBehaviorsWithTrigger(state, triggerName);
        foreach (var b in behaviorsWithTrigger)
        {
            if (b.HasTransition())
            {
                OutputTransitionCode(b, noAncestorHandlesEvent);
            }
            else
            {
                OutputNonTransitionCode(b, triggerName, noAncestorHandlesEvent);
            }

            File.RequestNewLineBeforeMoreCode();
        }
    }

    public void OutputTransitionCode(Behavior behavior, bool noAncestorHandlesEvent, bool checkForExiting = true)
    {
        OutputTransitionCodeInner(behavior, noAncestorHandlesEvent, checkForExiting);
    }

    private void OutputGuardStart(Behavior b)
    {
        if (b.HasGuardCode())
        {
            string expandedGuardCode = wrappingExpander.ExpandWrapGuardCode(b);
            File.AppendLines($"if ({expandedGuardCode})");
        }
    }

    private void DescribeBehaviorWithUmlComment(Behavior b)
    {
        string uml = b.DescribeAsUml();
        File.AppendLine($"// uml: {uml}");
    }

    private void OutputTransitionCodeInner(Behavior behavior, bool noAncestorHandlesEvent, bool checkForExiting = true)
    {
        if (behavior.TransitionTarget == null)
        {
            throw new InvalidOperationException("shouldn't happen");
        }

        Vertex source = behavior.OwningVertex;
        Vertex target = behavior.TransitionTarget;

        OutputStartOfBehaviorCode(behavior);
        File.StartCodeBlock();
        {
            var transitionPath = source.FindTransitionPathTo(target);

            File.Append($"// Step 1: Exit states until we reach `{Vertex.Describe(transitionPath.leastCommonAncestor)}` state (Least Common Ancestor for transition).");
            if (checkForExiting && IsExitingRequired(source, target, transitionPath))
            {
                File.FinishLine();
                ExitUntilLcaReached(source, transitionPath);
            }
            else
            {
                File.FinishLine(" Already at LCA, no exiting required.");
            }
            File.RequestNewLineBeforeMoreCode();

            File.AppendLine($"// Step 2: Transition action: `{behavior.GetSingleLineActionCode()}`.");
            OutputAnyActionCode(behavior, isForTransition: true);
            File.RequestNewLineBeforeMoreCode();

            File.AppendLine($"// Step 3: Enter/move towards transition target `{Vertex.Describe(target)}`.");
            EnterTowardsTarget(transitionPath);

            FinishTransitionOrContinuePseudo(behavior, target, noAncestorHandlesEvent);
        }
        OutputEndOfBehaviorCode(behavior);
    }

    private void FinishTransitionOrContinuePseudo(Behavior behavior, Vertex target, bool noAncestorHandlesEvent)
    {
        if (target is PseudoStateVertex pseudoStateVertex)
        {
            OutputTransitionsForPseudoState(behavior, pseudoStateVertex, noAncestorHandlesEvent);
        }
        else if (target is NamedVertex namedVertexTarget)
        {
            InitialState? initialState = namedVertexTarget.Children.OfType<InitialState>().FirstOrDefault();

            if (initialState != null)
            {
                OutputTransitionsForPseudoState(behavior, initialState, noAncestorHandlesEvent);
            }
            else
            {
                // no initial state, this is the final state.
                File.AppendLine("// Step 4: complete transition. Ends event dispatch. No other behaviors are checked.");
                File.AppendLine($"{smAccess}.{mangler.SmStateIdVarName} = {mangler.SmStateEnumType}.{mangler.SmStateEnumValue(namedVertexTarget)};");
                if (noAncestorHandlesEvent)
                {
                    File.AppendLine($"// No ancestor handles event. Can skip nulling `{mangler.SmAncestorEventHandlerVarName}`.");
                }
                else
                {
                    File.AppendLine($"{smAccess}.{mangler.SmAncestorEventHandlerVarName} = null;");
                }
                File.AppendLine($"return;");
            }
        }
    }

    private void EnterTowardsTarget(TransitionPath transitionPath)
    {
        if (transitionPath.toEnter.Count == 0)
        {
            File.AppendLine($"// Already in target. No entering required.");
            return;
        }

        foreach (var stateToEnter in transitionPath.toEnter)
        {
            if (stateToEnter is NamedVertex namedVertexToEnter)
            {
                var enterHandler = mangler.SmTriggerHandlerFuncName(namedVertexToEnter, TriggerHelper.TRIGGER_ENTER);
                File.AppendLine($"this.{enterHandler}();");
            }
            else if (stateToEnter is PseudoStateVertex pv)
            {
                File.AppendLine($"// {Vertex.Describe(pv)} is a pseudo state and cannot have an `enter` trigger.");
            }
            else
            {
                throw new ArgumentException("un-supported type: " + stateToEnter.GetType());
            }
        }
        File.RequestNewLineBeforeMoreCode();
    }

    private void OutputStartOfBehaviorCode(Behavior behavior)
    {
        File.AppendLine($"// {Vertex.Describe(behavior.OwningVertex)} behavior");
        DescribeBehaviorWithUmlComment(behavior);
        OutputGuardStart(behavior);
    }

    private bool RequiresConsumeEventVar(Behavior behavior)
    {
        if (behavior.HasActionCode())
        {
            var expandedAction = wrappingExpander.ExpandCode(behavior.actionCode);
            var inspector = new ActionCodeInspector();
            inspector.Parse(expandedAction);
            if (inspector.identifiersUsed.Contains(consumeEventVarName))
                return true;
        }

        return false;
    }

    private void OutputAnyActionCode(Behavior behavior, bool isForTransition)
    {
        if (behavior.HasActionCode())
        {
            if (isForTransition)
            {
                if (behavior.OwningVertex is NamedVertex)
                {
                    // file.AppendLine("// Note: no `consume_event` variable possible here because of state transition. The event must be consumed.");
                }
            }

            var expandedAction = wrappingExpander.ExpandWrapActionCode(behavior);
            File.AppendLines(expandedAction);
            File.RequestNewLineBeforeMoreCode();
        }
    }

    private void OutputTransitionsForPseudoState(Behavior b, PseudoStateVertex pseudoState, bool noAncestorHandlesEvent)
    {
        string? transitionFunction = pseudoStateHandlerBuilder.MaybeGetFunctionName(pseudoState);

        if (transitionFunction != null)
        {
            File.AppendLine($"// Finish transition by calling pseudo state transition function.");
            File.AppendLine($"this.{transitionFunction}();");
            File.AppendLine($"return; // event processing immediately stops when a transition finishes. No other behaviors for this state are checked.");
        }
        else
        {
            RenderPseudoStateTransitionsInner(pseudoState, noAncestorHandlesEvent);
        }
    }

    public void RenderPseudoStateTransitionFunctionInner(PseudoStateVertex pseudoState)
    {
        var functionName = pseudoStateHandlerBuilder.GetFunctionName(pseudoState); // just throws if not found

        const bool NoAncestorHandlesEvent = false; // assume ancestor might handle event because the pseudo state transition code can be called from multiple states.
        RenderPseudoStateTransitionsInner(pseudoState, noAncestorHandlesEvent: NoAncestorHandlesEvent);
    }

    private void RenderPseudoStateTransitionsInner(PseudoStateVertex pseudoState, bool noAncestorHandlesEvent)
    {
        foreach (Behavior pseudoStateBehavior in pseudoState.Behaviors)
        {
            if (pseudoStateBehavior.HasTransition())
            {
                OutputTransitionCodeInner(pseudoStateBehavior, noAncestorHandlesEvent);
                File.RequestNewLineBeforeMoreCode();
            }
        }
    }

    private static bool IsExitingRequired(Vertex source, Vertex target, TransitionPath transitionPath)
    {
        if (source is ExitPoint && source.Parent == target)
        {
            // self transition. exit required
            return true;
        }

        // If vertex is a pseudo state, then we know active leaf state is the containing state.
        // If it is also the LCA the the transition, we know no exiting is required at this point.
        if (source is PseudoStateVertex)
        {
            source = source.Parent!;

            if (transitionPath.leastCommonAncestor == source)
            {
                return false;
            }
        }

        return true;
    }

    private void ExitUntilLcaReached(Vertex source, TransitionPath transitionPath)
    {
        bool canUseDirectExit = CanUseSingleDirectExit(ref source, transitionPath);

        if (canUseDirectExit)
        {
            NamedVertex leafActiveState = (NamedVertex)source;
            string sourceExitHandler = mangler.SmTriggerHandlerFuncName(leafActiveState, TriggerHelper.TRIGGER_EXIT);
            File.AppendLine($"this.{sourceExitHandler}();");
        }
        else
        {
            NamedVertex leastCommonAncestor = (NamedVertex)transitionPath.leastCommonAncestor.ThrowIfNull();

            string ancestorExitHandler = mangler.SmTriggerHandlerFuncName(leastCommonAncestor, TriggerHelper.TRIGGER_EXIT);
            File.AppendLine($"this.{mangler.SmExitUpToFuncName}(this.{ancestorExitHandler});");
        }
    }

    private static bool CanUseSingleDirectExit(ref Vertex source, TransitionPath transitionPath)
    {
        bool canUseDirectExit = false;

        // We can only be exiting one state to use direct exit.
        var statesToExitCount = transitionPath.toExit.Count;

        // If source doesn't have any children, we know that it is the active leaf state and we may be able
        // to use a direct exit.
        bool sourceIsLeafState = source.Children.Any() == false;

        if (source is ExitPoint)
        {
            // This code assumes that first node to exit in transition path is source exit point.
            // That may change in the future as pseudo states cannot be exited so we add an assertion below to catch any future problems.
            if (transitionPath.toExit.First() != source)
                throw new VertexValidationException(source, "expected transition path exit list to start with source vertex");

            statesToExitCount--; // an exit point isn't a state that can be exited so decrement count.
            source = source.NonNullParent;  // an exit point should be treated as parent state.
        }

        if (source is NamedVertex && sourceIsLeafState && statesToExitCount <= 1)
        {
            canUseDirectExit = true;
        }

        return canUseDirectExit;
    }

    private bool OutputNextAncestorHandler(NamedVertex state, string triggerName)
    {
        bool noAncestorHandlesEvent;
        NamedVertex? nextHandlingState = state.FirstAncestorThatHandlesEvent(triggerName);
        noAncestorHandlesEvent = nextHandlingState == null;

        if (nextHandlingState == null)
        {
            File.AppendLine($"// No ancestor state handles `{triggerName}` event.");
        }
        else
        {
            File.AppendLine($"// Setup handler for next ancestor that listens to `{triggerName}` event.");
            File.AppendLine($"{smAccess}.{mangler.SmAncestorEventHandlerVarName} = this.{mangler.SmTriggerHandlerFuncName(nextHandlingState, triggerName)};");
        }

        File.RequestNewLineBeforeMoreCode();
        return noAncestorHandlesEvent;
    }

    private void OutputNonTransitionCode(Behavior b, in string triggerName, bool noAncestorHandlesEvent)
    {
        bool isConsumable = TriggerHelper.IsEvent(triggerName);
        bool hasConsumeEventVar = isConsumable && b.HasActionCode() && RequiresConsumeEventVar(b);

        OutputStartOfBehaviorCode(b);
        File.StartCodeBlock();
        {
            MaybeOutputConsumeEventVariable(b, triggerName, noAncestorHandlesEvent, hasConsumeEventVar);

            File.AppendLine($"// Step 1: execute action `{b.GetSingleLineActionCode()}`");
            OutputAnyActionCode(b, isForTransition: false);

            MaybeOutputConsumeEventCode(noAncestorHandlesEvent: noAncestorHandlesEvent, isConsumable: isConsumable, hasConsumeEventVar: hasConsumeEventVar, triggerName: triggerName);
        }
        OutputEndOfBehaviorCode(b);
    }

    private void OutputEndOfBehaviorCode(Behavior b)
    {
        File.FinishCodeBlock($" // end of behavior for {Vertex.Describe(b.OwningVertex)}");
    }

    private void MaybeOutputConsumeEventCode(bool noAncestorHandlesEvent, bool isConsumable, bool hasConsumeEventVar, string triggerName)
    {
        if (!isConsumable)
        {
            return;
        }

        File.AppendLine($"// Step 2: determine if ancestor gets to handle event next.");

        if (hasConsumeEventVar)
        {
            if (noAncestorHandlesEvent)
            {
                File.AppendLine("// No ancestor handles event. Ignore `consume_event` flag.");
            }
            else
            {
                File.Append("if (consume_event)");
                File.StartCodeBlock();
                {
                    File.AppendLine($"{smAccess}.{mangler.SmAncestorEventHandlerVarName} = null;  // consume event");
                }
                File.FinishCodeBlock();
            }
        }
        else
        {

            if (TriggerHelper.IsDoEvent(triggerName))
            {
                File.AppendLine("// Don't consume special `do` event.");
            }
            else
            {
                if (noAncestorHandlesEvent)
                {
                    File.AppendLine($"// No ancestor handles event. Can skip nulling `{mangler.SmAncestorEventHandlerVarName}`.");
                }
                else
                {
                    File.AppendLine($"{smAccess}.{mangler.SmAncestorEventHandlerVarName} = null;  // consume event");
                }
            }
        }
    }

    private void MaybeOutputConsumeEventVariable(Behavior behavior, string triggerName, bool noAncestorHandlesEvent, bool hasConsumeEventVar)
    {
        if (!behavior.HasActionCode())
        {
            return;
        }

        if (!hasConsumeEventVar)
        {
            return;
        }

        if (noAncestorHandlesEvent)
        {
            File.AppendLine("// note: no ancestor consumes this event, but we output `bool consume_event` anyway because a user's design might rely on it.");
        }

        File.Append("bool consume_event = ");
        if (TriggerHelper.IsDoEvent(triggerName))
        {
            File.FinishLine("false; // the `do` event is special in that it normally is not consumed.");
        }
        else
        {
            File.FinishLine("true; // events other than `do` are normally consumed by any event handler. Other event handlers in *this* state may still handle the event though.");
        }
        File.AppendLine();
    }

    public void OutputNamedStateHandlers(NamedVertex state)
    {
        File.AppendLine();
        File.AppendLine("////////////////////////////////////////////////////////////////////////////////");
        File.AppendLine($"// event handlers for state {mangler.SmStateName(state)}");
        File.AppendLine("////////////////////////////////////////////////////////////////////////////////");
        File.AppendLine();

        OutputFuncStateEnter(state);
        OutputFuncStateExit(state);

        string[] eventNames = GetEvents(state).ToArray();
        Array.Sort(eventNames);

        foreach (var eventName in eventNames)
        {
            OutputTriggerHandlerSignature(state, eventName);
            File.StartCodeBlock();
            {
                OutputStateBehaviorsForTrigger(state, eventName);
            }
            FinishAddressableFunction(forceNewLine: false);
            File.RequestNewLineBeforeMoreCode();
        }

        pseudoStateHandlerBuilder.OutputFunctionsForParent(state, RenderPseudoStateTransitionFunctionInner);
    }

    public void OutputFuncStateEnter(NamedVertex state)
    {
        //var oldSmAccess = smAccess;
        //smAccess = "this";
        OutputTriggerHandlerSignature(state, TriggerHelper.TRIGGER_ENTER);

        File.StartCodeBlock();
        {
            File.AppendLine($"// setup trigger/event handlers");
            string stateExitHandlerName = mangler.SmTriggerHandlerFuncName(state, TriggerHelper.TRIGGER_EXIT);
            File.AppendLine($"{smAccess}.{mangler.SmCurrentStateExitHandlerVarName} = this.{stateExitHandlerName};");

            string[] eventNames = GetEvents(state).ToArray();
            Array.Sort(eventNames);

            foreach (var eventName in eventNames)
            {
                string handlerName = mangler.SmTriggerHandlerFuncName(state, eventName);
                string eventEnumValueName = mangler.SmEventEnumValue(eventName);
                File.AppendLine($"{smAccess}.{mangler.SmCurrentEventHandlersVarName}[(int){mangler.SmEventEnumType}.{eventEnumValueName}] = this.{handlerName};");
            }

            File.RequestNewLineBeforeMoreCode();
            OutputStateBehaviorsForTrigger(state, TriggerHelper.TRIGGER_ENTER);
        }
        File.FinishCodeBlock(forceNewLine: true);
        File.AppendLine();

        //smAccess = oldSmAccess;
    }

    public void OutputFuncStateExit(NamedVertex state)
    {
        OutputTriggerHandlerSignature(state, TriggerHelper.TRIGGER_EXIT);

        File.StartCodeBlock();
        {
            OutputStateBehaviorsForTrigger(state, TriggerHelper.TRIGGER_EXIT);

            if (state.Parent == null)
            {
                File.AppendLine($"// State machine root is a special case. It cannot be exited. Mark as unused.");
                File.AppendLine(GilCreationHelper.MarkVarAsUnused("this"));
            }
            else
            {
                File.AppendLine($"// adjust function pointers for this state's exit");
                string parentExitHandler = mangler.SmTriggerHandlerFuncName((NamedVertex)state.Parent, TriggerHelper.TRIGGER_EXIT);
                File.AppendLine($"{smAccess}.{mangler.SmCurrentStateExitHandlerVarName} = this.{parentExitHandler};");

                string[] eventNames = GetEvents(state).ToArray();
                Array.Sort(eventNames);

                foreach (var eventName in eventNames)
                {
                    string eventEnumValueIndex = $"(int){mangler.SmEventEnumType}.{mangler.SmEventEnumValue(eventName)}";
                    var ancestor = state.FirstAncestorThatHandlesEvent(eventName);
                    if (ancestor != null)
                    {
                        string handlerName = mangler.SmTriggerHandlerFuncName(ancestor, eventName);
                        File.AppendLine($"{smAccess}.{mangler.SmCurrentEventHandlersVarName}[{eventEnumValueIndex}] = this.{handlerName};  // the next ancestor that handles this event is {mangler.SmStateName(ancestor)}");
                    }
                    else
                    {
                        File.AppendLine($"{smAccess}.{mangler.SmCurrentEventHandlersVarName}[{eventEnumValueIndex}] = null;  // no ancestor listens to this event");
                    }
                }
            }
        }

        FinishAddressableFunction(forceNewLine: true);
        File.AppendLine();
    }

    private void FinishAddressableFunction(bool forceNewLine)
    {
        var finishStr = "";

        File.FinishCodeBlock(finishStr, forceNewLine: forceNewLine);
    }

    public void OutputTriggerHandlerSignature(NamedVertex state, string eventName)
    {
        // enter functions don't need to be static delegates because we don't take their address
        string funcName = mangler.SmTriggerHandlerFuncName(state, eventName);
        File.Append($"private void {funcName}()");
    }

    /// <summary>
    /// These do NOT include entry and exit triggers
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    private static HashSet<string> GetEvents(NamedVertex state)
    {
        var triggerNames = TriggerHelper.GetSanitizedTriggersSet(state);
        triggerNames.Remove(TriggerHelper.TRIGGER_ENTER);
        triggerNames.Remove(TriggerHelper.TRIGGER_EXIT);
        return triggerNames;
    }
}
