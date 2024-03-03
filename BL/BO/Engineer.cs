namespace BO;

/// <summary>
/// An engineer entity that contains details about the engineer
/// </summary>
public class Engineer
{
    public int id { get;init; }
    public int password { get; set; } 
    public string? name { get; set; }
    public string? email { get; set; }
    public Engineerlevel? level { get; set; }
    public double? cost { get; set; }
    public bool isActive { get; set; }

    public BO.TaskInEngineer? task { get; set; }

    /// <summary>
    /// An empty constructor action that constructs an empty object
    /// </summary>
    public Engineer()
    {
        this.id = 0;
        this.password = 0;
        this.name = null;
        this.email = null;
        this.level = BO.Engineerlevel.Beginner;
        this.cost = 0;
        this.isActive = false;
        this.task = null;
    }
    public override string ToString() => this.ToStringProperty();

}
