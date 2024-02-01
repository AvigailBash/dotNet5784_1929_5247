namespace BO;

public class Engineer
{
    public int id { get;init; }
    public string? name { get; set; }
    public string? email { get; set; }
    public Engineerlevel? level { get; set; }
    public double? cost { get; set; }
    public bool isActive { get; set; }

    public BO.TaskInEngineer? task { get; set; }
 
}
