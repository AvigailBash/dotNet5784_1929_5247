using DO;

namespace Dal;

/// <summary>
/// Building a list for each entity
/// </summary>
internal static class DataSource
{
    /// <summary>
    /// Creates an automatic running number for the identifier of dependency and assignment entities
    /// </summary>
    internal static class Config
    {
        internal const int startTaskId = 1000;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }


        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
    }
    internal static List<DO.Task>? Tasks { get; } = new();
    internal static List<DO.Engineer>? Engineers { get; } = new();
    internal static List<DO.Dependency>? Dependencies { get; } = new();
}

