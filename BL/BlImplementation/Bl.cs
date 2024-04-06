using BlApi;


namespace BlImplementation;

internal class Bl:IBl
{
    /// <summary>
    /// Creating the engineer property
    /// </summary>
    public IEngineer Engineer => new EngineerImplementation();

    /// <summary>
    /// Creating the task property
    /// </summary>
    public ITask Task =>  new TaskImplementation();

    /// <summary>
    /// Creating the clock property
    /// </summary>
    public IClock Clock => new ClockImplementation();
    public IBl bl => this;

    //public void reset() => DalTest.Initialization.reset();


    private static readonly TimeSpan s_oneHour = new(1, 0, 0);
    private static readonly TimeSpan s_oneDay = new (1, 0, 0, 0);
    private static readonly TimeSpan s_oneYear = new(365, 0, 0, 0);
    private static event Action? s_clockObserver;
    private static DateTime s_clock = DateTime.Now.Date;

    public DateTime clock
    { 
        get => (DateTime)s_clock; 
        private set 
        {
            if(s_clock!= value)
            {
                s_clock = value;
                s_clockObserver?.Invoke();
            }
          
        } 
    }

    public IHelp Help => new HelpImplementation();

    public void addClockObserver(Action clockObserver) => s_clockObserver += clockObserver;
    public void removeClockObserver(Action clockObserver) => s_clockObserver -= clockObserver;
    public void clockNextDay() => clock = (clock + s_oneDay).Date;
    public void clockForwardDay() => clock += s_oneDay;
    public void clockForwardHour() => clock += s_oneHour;
    public void clockSetDateTime(DateTime time)=> clock = time;
    public void clockForwardYear() => clock += s_oneYear;

    public void clockInit() => clock  =DateTime.Now;

    public void reset()
    {
        throw new NotImplementedException();
    }
}
