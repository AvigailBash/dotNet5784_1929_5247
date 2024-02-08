namespace BO;

public class TaskInList
{
    public int id { get; init; }
    public string? description { get; set; }
    public string? alias { get; set; }
    public BO.Status status { get; set; }
    public TaskInList()
    {
        this.id = 0;
        this.description = null;
        this.alias = null;
        this.status = BO.Status.Unscheduled;
    }
    public TaskInList(int id, string? description, string? alias, Status status)
    {
        this.id = id;
        this.description = description;
        this.alias = alias;
        this.status = status;
    }
}
