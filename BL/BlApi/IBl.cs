using BlImplementation;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineer{ get; }
    public IEngineerInTask EngineerInTask { get; }
    //public IMilestone Milestone { get; }
    public IMilestoneInList MilestoneInList { get; }
    public IMilestoneInTask MilestoneInTask { get; }
    public ITask Task { get; }
    public ITaskInEngineer TaskInEngineer { get; }
    public ITaskInList TaskInList { get; }

}
