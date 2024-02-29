using BlImplementation;
using DalApi;

namespace BlApi;

public interface IBl
{
    /// <summary>
    /// Engineer property
    /// </summary>
    public IEngineer Engineer { get; }

    /// <summary>
    /// Engineer in task property
    /// </summary>
    public IEngineerInTask EngineerInTask { get; }
    //public IMilestone Milestone { get; }

    /// <summary>
    /// Milestone in list property
    /// </summary>
    public IMilestoneInList MilestoneInList { get; }

    /// <summary>
    /// Milestone in task property
    /// </summary>
    public IMilestoneInTask MilestoneInTask { get; }

    /// <summary>
    /// Task property
    /// </summary>
    public ITask Task { get; }

    /// <summary>
    /// Task in engineer property
    /// </summary>
    public ITaskInEngineer TaskInEngineer { get; }

    /// <summary>
    /// Task in list property
    /// </summary>
    public ITaskInList TaskInList { get; }

    /// <summary>
    /// Clock property
    /// </summary>
    public IClock Clock { get; }
    
    public DateTime clock { get; }


    void addClockObserver(Action clockObserver);
    void removeClockObserver(Action clockObserver);
    void clockNextDay();
    void clockForwardYear();
    void clockForwardDay();
    void clockForwardHour();
    void clockSetDateTime(DateTime time);
    void clockInit();
    public void reset();
}
