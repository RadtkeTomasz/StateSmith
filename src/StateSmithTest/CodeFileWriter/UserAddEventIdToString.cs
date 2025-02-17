using FluentAssertions;
using StateSmith.Output;
using StateSmith.Output.Algos.Balanced1;
using StateSmith.Runner;
using System.IO;
using System.Text;
using Xunit;

namespace StateSmithTest.CodeFileWriterTests;

// See https://github.com/StateSmith/StateSmith/issues/109
public class UserAddEventIdToString
{
    public class MyCodeFileWriter : ICodeFileWriter
    {
        StateMachineProvider smProvider;
        NameMangler mangler;
        CodeStyleSettings codeStyle;

        public MyCodeFileWriter(StateMachineProvider smProvider, NameMangler mangler, CodeStyleSettings codeStyle)
        {
            this.smProvider = smProvider;
            this.mangler = mangler;
            this.codeStyle = codeStyle;
        }

        string CreateFuncSignature()
        {
            var output = "// Converts an event id to a string. Thread safe.\n";
            output += $"const char* {mangler.Sm.Name}_event_id_to_string(const enum {mangler.SmEventEnumType} id)";
            return output;
        }

        string CreateFunctionPrototype()
        {
            return CreateFuncSignature() + ";\n";
        }

        string CreateFunction()
        {
            var sm = smProvider.GetStateMachine();
            var sb = new StringBuilder();
            var output = new OutputFile(codeStyle, sb);

            output.Append(CreateFuncSignature());
            output.StartCodeBlock();
            {
                output.Append("switch (id)");
                output.StartCodeBlock();
                {
                    foreach (var eventName in sm.GetEventListCopy())
                    {
                        output.AppendLine($"case {mangler.SmEventEnumValue(eventName)}: return \"{eventName}\";");
                    }
                }
                output.FinishCodeBlock();
                output.AppendLine("return \"?\";");
            }
            output.FinishCodeBlock();
            return output.ToString();
        }

        void ICodeFileWriter.WriteFile(string filePath, string code)
        {
            if (filePath.EndsWith(".h"))
            {
                code += CreateFunctionPrototype();
            }
            else
            {
                code += CreateFunction();
            }

            File.WriteAllText(path: filePath, code);
        }
    }

    // 
    [Fact]
    public void ExampleCustomCodeGen()
    {
        SmRunner runner = new(diagramPath: "Ex2.drawio.svg");

        // register our custom code file writer
        runner.GetExperimentalAccess().DiServiceProvider.AddSingletonT<ICodeFileWriter, MyCodeFileWriter>();

        // adjust settings because we are unit testing. Normally wouldn't do below.
        runner.Settings.propagateExceptions = true;

        // run StateSmith with our custom code file writer
        runner.Run();

        // Test that we saw the expected output from your custom code generator.
        var cCode = File.ReadAllText(runner.Settings.outputDirectory + "Ex2.c");
        var hCode = File.ReadAllText(runner.Settings.outputDirectory + "Ex2.h");

        cCode.Should().Contain("_event_id_to_string");
        hCode.Should().Contain("_event_id_to_string");
    }
}
