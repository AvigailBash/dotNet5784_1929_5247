namespace BlApi;

public interface ITask
{
    /// <summary>
    /// A method that returns all tasks according to a certain number
    /// </summary>
    /// <param name="filter"> The filter by which to search </param>
    /// <returns></returns>
    public IEnumerable<BO.TaskInList> ReadAll(Func <BO.Task, bool>? filter = null);

    /// <summary>
    /// A method that returns an task by ID
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    /// <returns></returns>
    public BO.Task? Read(int id);

    /// <summary>
    /// A method that creates a new task
    /// </summary>
    /// <param name="task"> Receives a bone and creates it </param>
    /// <returns></returns>
    public int Create(BO.Task task);

    /// <summary>
    /// A method that updates an task
    /// </summary>
    /// <param name="task"> Getting an object to update </param>
    public void Update(BO.Task task);

    /// <summary>
    /// A method that deletes a certain task, by searching the Id 
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    public void Delete(int id);

    /// <summary>
    /// A method that calculates the time that should be taken for a certain task according to the dates and the time needed
    /// </summary>
    /// <param name="scheduale"> Estimated date for the start of the assignment </param>
    /// <param name="start"> Actual project start date </param>
    /// <param name="require"> The time needed to complete the task </param>
    /// <returns></returns>
    public DateTime findForecastDate(DateTime? scheduale, DateTime? start, TimeSpan? require);

    /// <summary>
    /// A method that finds which tasks the current task depends on
    /// </summary>
    /// <param name="task"> The task received </param>
    /// <returns></returns>
    public List<BO.TaskInList> findDependencies(DO.Task task);

    /// <summary>
    /// A method that finds the status of the project
    /// </summary>
    /// <param name="task"> The task received </param>
    /// <returns></returns>
    public BO.Status findStatus(DO.Task task);

    /// <summary>
    /// A method that converts from an engineer entity to an engineer entity in a task that contains more details about the engineer
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    /// <returns></returns>
    public BO.EngineerInTask convertFromEngineerToEngineerInTask(int? id);

    public List<BO.TaskInList> findDependencies(BO.Task task);

    public void AddDependencies(int id, BO.TaskInList taskInList);
    public void RemoveDependencies(BO.Task boTask, BO.TaskInList taskInList);
}
