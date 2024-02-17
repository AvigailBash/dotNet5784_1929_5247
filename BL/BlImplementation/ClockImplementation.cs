using BlApi;

namespace BlImplementation;

internal class ClockImplementation : IClock
{
    /// <summary>
    /// A call to the method that fetches the data
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Getting a end date for the project
    /// </summary>
    /// <returns></returns>
    public DateTime? GetEndOfProject() => _dal.Clock.GetEndOfProject();

    /// <summary>
    /// Getting a start date for the project
    /// </summary>
    /// <returns></returns>
    public DateTime? GetStartOfProject()=> _dal.Clock.GetStartOfProject();

    /// <summary>
    /// Changing the end date of the project
    /// </summary>
    /// <param name="endOfProject"> The end date </param>
    /// <returns></returns>
    public DateTime? SetEndOfProject(DateTime endOfProject) => _dal.Clock.SetEndOfProject(endOfProject);

    /// <summary>
    /// Changing the start date of the project
    /// </summary>
    /// <param name="startOfProject"> The start date </param>
    /// <returns></returns>
    public DateTime? SetStartOfProject(DateTime startOfProject) => _dal.Clock.SetStartOfProject(startOfProject);

    /// <summary>
    /// A method that calculates the status of the project - at which stage we are
    /// </summary>
    /// <returns></returns>
    public BO.StatusOfProject statusForProject()
    {
        bool flag = true;
        if (GetStartOfProject == null) 
        {
            return BO.StatusOfProject.Start;
        }
        else
        {
            foreach(DO.Task ta in _dal.Task.ReadAll())
            {
                if (ta.schedualedDate == null) 
                {
                    flag = false; 
                }
            }
            if(!flag)
            {
                return BO.StatusOfProject.Middle;
            }
        }
        return BO.StatusOfProject.End;
    }

    /// <summary>
    /// A method that returns the minimum date for planning a task
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public DateTime? minimumDateForSceduale(BO.Task t)
    {
        DateTime? minDate = null;
        DateTime tempDate = DateTime.MinValue;
        if (t.dependencies == null)
        {
            minDate = _dal.Clock.GetStartOfProject();
        }
        else
        {
            foreach (BO.TaskInList task in t.dependencies)
            {
                DO.Task ta = _dal.Task.Read(task.id)!;
                if (ta.startDate == null)
                {
                    throw new BO.Exceptions.BlCannotChangeInThisStatusException("One of the dependency doesn't have start date");
                }
                if (ta.completeDate != null && ta.completeDate > tempDate)
                {
                    minDate = ta.completeDate;
                }
            }
        }
        return minDate;
    }
}
