//using DalApi;
//using System.Diagnostics;

//namespace Dal;
///// <summary>
///// A class for initializing entities when working with XML files
///// </summary>
//sealed internal class DalXml : IDal
//{
//    public static IDal Instance { get; } = new DalXml();
//    private DalXml() { }
//    public ITask Task => new TaskImplementation();

//    public IEngineer Engineer => new EngineerImplementation();

//    public IDependency Dependency => new DependencyImplementation();
//    public IClock Clock => new ClockImplementation();
//    public IUser User => new UserImplementation();

//    public IHelp Help => new HelpImplementation();
//}
using DalApi;
using System.Diagnostics;

namespace Dal;

/// <summary>
/// A class for initializing entities when working with XML files
/// </summary>
sealed internal class DalXml : IDal
{
    private static readonly Lazy<DalXml> _instance = new Lazy<DalXml>(() => new DalXml());

    public static IDal Instance => _instance.Value;

    private DalXml() { }

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