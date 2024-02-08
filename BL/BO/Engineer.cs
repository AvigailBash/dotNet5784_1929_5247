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

    public Engineer()
    {
        this.id = 0;
        this.name = null;
        this.email = null;
        this.level = BO.Engineerlevel.Beginner;
        this.cost = 0;
        this.isActive = false;
        this.task = null;
    }
    public Engineer(int id, string? name, string? email, Engineerlevel? level, double? cost, bool isActive, TaskInEngineer? task)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.level = level;
        this.cost = cost;
        this.isActive = isActive;
        this.task = task;
    }
    public override string ToString() => this.ToStringProperty();

}
