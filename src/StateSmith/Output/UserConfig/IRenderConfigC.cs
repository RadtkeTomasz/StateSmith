namespace StateSmith.Output.UserConfig;

public interface IRenderConfigC : IRenderConfig
{
    /// <summary>
    /// Whatever this property returns will be placed at the top of the rendered .h file.
    /// </summary>
    string HFileTop => "";

    string HFileIncludes => "";
    string CFileIncludes => "";

    /// <summary>
    /// Whatever this property returns will be placed at the top of the rendered .c file.
    /// </summary>
    string CFileTop => "";
}
