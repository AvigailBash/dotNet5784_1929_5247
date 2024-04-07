namespace Dal;
using DalApi;

/// <summary>
/// A class that invokes each of the classes that implement the entities
/// </summary>
sealed internal class DalList : IDal
{
    private static readonly Lazy<DalList> _instance = new Lazy<DalList>(() => new DalList());

    public static IDal Instance => _instance.Value;

    private DalList() { }

    private readonly Lazy<ITask> _task = new Lazy<ITask>(() => new TaskImplementation());
    public ITask Task => _task.Value;

    private readonly Lazy<IEngineer> _engineer = new Lazy<IEngineer>(() => new EngineerImplementation());
    public IEngineer Engineer => _engineer.Value;

    private readonly Lazy<IDependency> _dependency = new Lazy<IDependency>(() => new DependencyImplementation());
    public IDependency Dependency => _dependency.Value;

    private readonly Lazy<IClock> _clock = new Lazy<IClock>(() => new ClockImplementation());
    public IClock Clock => _clock.Value;

    private readonly Lazy<IUser> _user = new Lazy<IUser>(() => new UserImplementation());
    public IUser User => _user.Value;

    private readonly Lazy<IHelp> _help = new Lazy<IHelp>(() => new HelpImplementation());
    public IHelp Help => _help.Value;
}