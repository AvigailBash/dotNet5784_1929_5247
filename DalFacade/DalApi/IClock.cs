namespace DalApi;

public interface IClock
{
    /// <summary>
    /// Changing the start date of the project
    /// </summary>
    /// <param name="startOfProject"> The start date </param>
    /// <returns></returns>
    public DateTime? SetStartOfProject(DateTime? startOfProject);

    /// <summary>
    /// Getting a start date for the project
    /// </summary>
    /// <returns></returns>
    public DateTime? GetStartOfProject();

    /// <summary>
    /// Changing the end date of the project
    /// </summary>
    /// <param name="endOfProject"> The end date </param>
    /// <returns></returns>
    public DateTime? SetEndOfProject(DateTime? endOfProject);


    /// <summary>
    /// Getting a end date for the project
    /// </summary>
    /// <returns></returns>
    public DateTime? GetEndOfProject();
}