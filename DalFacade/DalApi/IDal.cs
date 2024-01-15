namespace DalApi;

public interface IDal
{
    ITask Task { get; } // properties
    IEngineer Engineer { get; } // properties
    IDependency dependency { get; } // properties
}
