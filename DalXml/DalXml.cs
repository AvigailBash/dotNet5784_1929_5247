using DalApi;
using System.Diagnostics;

namespace Dal;
/// <summary>
/// A class for initializing entities when working with XML files
/// </summary>
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();
    public IClock Clock => new ClockImplementation();
    public IUser User => new UserImplementation();
}
