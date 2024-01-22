namespace Dal;
using DalApi;

/// <summary>
/// A class that invokes each of the classes that implement the entities
/// </summary>
sealed public class DalList : IDal
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();
}
