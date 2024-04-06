namespace BO;
/// <summary>
/// An entity that contains details about a task in a list
/// </summary>
public class TaskInList
{
    public int id { get; init; }
    public string? description { get; set; }
    public string? alias { get; set; }
    public BO.Status status { get; set; }

    /// <summary>
    /// An empty constructor action that initializes an empty object
    /// </summary>
    public TaskInList()
    {
        this.id = 0;
        this.description = null;
        this.alias = null;
        this.status = BO.Status.Unscheduled;
    }
   
    public override string ToString() => this.ToStringProperty();
}
