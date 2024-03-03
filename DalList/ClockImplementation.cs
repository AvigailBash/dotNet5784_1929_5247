using DalApi;

namespace Dal;

internal class ClockImplementation : IClock
{
    /// <summary>
    /// Getting a end date for the project
    /// </summary>
    /// <returns></returns>
    public DateTime? GetEndOfProject()
    {
        DateTime? endProject = DataSource.Config.endProject;
        return endProject;
    }

    /// <summary>
    /// Getting a start date for the project
    /// </summary>
    /// <returns></returns>
    public DateTime GetStartOfProject()
    {
        DateTime startProject = DataSource.Config.startProject;
        return startProject;
    }

    /// <summary>
    /// Changing the end date of the project
    /// </summary>
    /// <param name="endOfProject"> The end date </param>
    /// <returns></returns>
    public DateTime? SetEndOfProject(DateTime endOfProject)
    {
        DataSource.Config.endProject = endOfProject;
        return endOfProject;
    }

    /// <summary>
    /// Changing the start date of the project
    /// </summary>
    /// <param name="startOfProject"> The start date </param>
    /// <returns></returns>
    public DateTime? SetStartOfProject(DateTime startOfProject)
    {
        DataSource.Config.startProject = startOfProject;
        return startOfProject;
    }
}
