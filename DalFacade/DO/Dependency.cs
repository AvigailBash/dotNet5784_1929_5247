namespace DO;

public record Dependency
(
    int id,
    int? dependentTask = null,
    int? dependsOnTask = null
)
{
    public Dependency() : this(0) { }
}

