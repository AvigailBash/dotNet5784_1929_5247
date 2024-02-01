namespace BO;

public class EngineerInTask
{
    public int id { get; init; }
    public string? name { get; set; }

    public EngineerInTask()
    {
        this.id = 0;
        this.name = null;
    }
    public EngineerInTask (int id, string? name)
    {
        this.id = id;
        this.name = name;
    }
}

