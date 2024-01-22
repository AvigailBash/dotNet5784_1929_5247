using DalApi;

namespace Dal;
/// <summary>
/// A class for initializing entities when working with XML files
/// </summary>
public class DalXml : IDal
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();
}
