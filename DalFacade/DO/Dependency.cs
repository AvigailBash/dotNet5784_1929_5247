namespace DO;

/// <summary>
/// A dependency entity that creates an automatic ID number, and details about the task's dependencies
/// </summary>
/// <param name="id"> Automatic ID number </param>
/// <param name="dependentTask"> which a chore she has an addiction to </param>
/// <param name="dependsOnTask"> which assignment does she depend on? </param>
public record Dependency
(
    int id,
    int? dependentTask = null,
    int? dependsOnTask = null,
    bool isActive = false
)
{
    /// <summary>
    /// An empty constructive action for an entity
    /// </summary>
    public Dependency() : this(0) { }
}

