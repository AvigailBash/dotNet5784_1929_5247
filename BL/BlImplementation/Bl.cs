using BlApi;


namespace BlImplementation;

internal class Bl:IBl
{
    public IEngineer Engineer => new EngineerImplementation();

   public IEngineerInTask EngineerInTask=> new EngineerInTaskImplementation();

    //public IMilestone Milestone =>  new MilestoneImplementation();

    public IMilestoneInList MilestoneInList => new MilestoneInListImplementation();

    public IMilestoneInTask MilestoneInTask =>  new MilestoneInTaskImplementation();

    public ITask Task =>  new TaskImplementation();

    public ITaskInEngineer TaskInEngineer =>  new TaskInEngineerImplementation();

    public ITaskInList TaskInList =>  new TaskInListImplementation();
}
