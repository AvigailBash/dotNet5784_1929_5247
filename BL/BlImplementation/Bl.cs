using BlApi;


namespace BlImplementation;

internal class Bl:IBl
{
    /// <summary>
    /// Creating the engineer property
    /// </summary>
    public IEngineer Engineer => new EngineerImplementation();

    /// <summary>
    /// Creating the engineer in task property
    /// </summary>
    public IEngineerInTask EngineerInTask=> new EngineerInTaskImplementation();

    //public IMilestone Milestone =>  new MilestoneImplementation();

    /// <summary>
    /// Creating the milestone in list property
    /// </summary>
    public IMilestoneInList MilestoneInList => new MilestoneInListImplementation();

    /// <summary>
    /// Creating the milstone in task property
    /// </summary>
    public IMilestoneInTask MilestoneInTask =>  new MilestoneInTaskImplementation();

    /// <summary>
    /// Creating the task property
    /// </summary>
    public ITask Task =>  new TaskImplementation();

    /// <summary>
    /// Creating the task in engineer property
    /// </summary>
    public ITaskInEngineer TaskInEngineer =>  new TaskInEngineerImplementation();

    /// <summary>
    /// Creating the task in list property
    /// </summary>
    public ITaskInList TaskInList =>  new TaskInListImplementation();

    /// <summary>
    /// Creating the clock property
    /// </summary>
    public IClock Clock => new ClockImplementation();
}
