using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using System;

#nullable enable

/*
 * This file is intended to provide language agnostic helpers and expansions
 */

namespace Spec.Spec2;

public class Spec2GenericVarExpansions : SpecGenericVarExpansions
{
    #pragma warning disable IDE1006 // Naming Styles
    public string count => AutoVarName();
    #pragma warning restore IDE1006 // Naming Styles
}

public class Spec2Fixture : SpecFixture
{
    public static string Spec2Directory => SpecInputDirectoryPath + "2/";

    public static void CompileAndRun(IRenderConfig renderConfig, string outputDir, Action<SmRunner>? action = null)
    {
        var diagramFile = Spec2Directory + "Spec2Sm.graphml";
        CompileAndRun(renderConfig, diagramFile, outputDir, smRunnerAction: action);
    }
}

