namespace Dal;

/// <summary>
/// A class for initializing the running number in dependency and assignment task
/// </summary>
internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
    public static void resetTaskId(bool flag) => XMLTools.ResetId(s_data_config_xml, "NextTaskId",flag,"Task");
    public static void resetDependencyId(bool flag) => XMLTools.ResetId(s_data_config_xml, "NextDependencyId",flag,"Dependency");
    internal static DateTime startProject = DateTime.Now;
    internal static DateTime? endProject = null;
}
