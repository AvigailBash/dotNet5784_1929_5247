namespace Dal;
using DalApi;

/// <summary>
/// A class that invokes each of the classes that implement the entities
/// </summary>
sealed internal class DalList : IDal
{
    //public static IDal Instance = null;
    //public static IDal GetInstance()
    //{
    //    //This is not thread-safe
    //    if (Instance == null)
    //    {
    //        Instance = new DalList();
    //    }
    //    //Return the Singleton Instance
    //    return Instance;
    //}
    //private DalList() { }
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IClock Clock => new ClockImplementation();
    public IUser User => new UserImplementation();
}
