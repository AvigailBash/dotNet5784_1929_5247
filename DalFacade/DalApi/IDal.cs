namespace DalApi;

public interface IDal
{
    ITask Task { get; }
    IEngineer Engineer { get; }
    IDependency dependency { get; }
}
