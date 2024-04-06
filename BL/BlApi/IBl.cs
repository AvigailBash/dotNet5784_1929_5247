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
    /// Task property
    /// </summary>
    public ITask Task { get; }

    /// <summary>
    /// Clock  property
    /// </summary>
    public IClock Clock { get; }
    
    public DateTime clock { get; }
    public IHelp Help { get; }


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
