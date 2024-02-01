namespace BO;

public class TaskInEngineer
{
    public int id { get; init; }
    public string? alias { get; set; }

    public TaskInEngineer(int id, string? alias)
    {
        this.id = id;
        this.alias = alias;
    }
    public TaskInEngineer()
    {
        this.id = 0;
        this.alias = null;
    }
}
