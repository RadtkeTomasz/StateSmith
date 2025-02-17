namespace StateSmith.Output.UserConfig;

public interface IRenderConfig
{
    /// <summary>
    /// This section allows you to define custom variables for your state machine. Any code/text you put in here
    /// will be output directly inside the state machine variables object/struct.
    /// </summary>
    string VariableDeclarations => "";

    /// <summary>
    /// This section allows you to conveniently do two things at once: 1) define variables, 2) automatically create expansions for those variables.
    /// </summary>
    string AutoExpandedVars => "";

    /// <summary>
    /// Not used yet. A comma seperated list of allowed event names. TODO case sensitive?
    /// </summary>
    string EventCommaList => "";

    /// <summary>
    /// `FileTop` text will appear at the top of the file. Use for comments, copyright notices, code...
    /// </summary>
    string FileTop => "";
}

public class DummyIRenderConfig : IRenderConfig
{

}
