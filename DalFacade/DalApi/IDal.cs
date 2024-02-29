namespace DalApi;

/// <summary>
/// An interface that unites all interfaces within it
/// </summary>
public interface IDal
{
    /// <summary>
    /// Task property
    /// </summary>
    ITask Task { get; }

    /// <summary>
    /// Engineer property
    /// </summary>
    IEngineer Engineer { get; }

    /// <summary>
    /// Dependency property
    /// </summary>
    IDependency Dependency { get; }

    IClock Clock { get; }

    IDal help { get; }
}
