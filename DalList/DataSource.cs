using DO;

namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

    }
    internal static List<Task>? Tasks { get; } = new();
    internal static List<Engineer>? Engineers { get; } = new();
    internal static List<Dependency>? Dependencies { get; } = new();
}

