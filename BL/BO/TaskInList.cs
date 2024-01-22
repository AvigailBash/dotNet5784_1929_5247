namespace BO;

public class TaskInList
{
    public int id { get; init; }
    public string? description { get; set; }
    public string? alias { get; set; }
    public BO.Status status { get; set; }
}
