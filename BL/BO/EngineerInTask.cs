namespace BO;

/// <summary>
/// An entity showing an engineer in charge of a task with some details about the engineer
/// </summary>
public class EngineerInTask
{
    public int id { get; init; }
    public string? name { get; set; }

    /// <summary>
    /// An empty constructor action that constructs an empty object
    /// </summary>
    public EngineerInTask()
    {
        this.id = 0;
        this.name = null;
    }


    /// <summary>
    /// A method that overrides the To string and prints in a different format
    /// </summary>
    public void ToString()
    {
        Console.WriteLine($"Id: {this.id}");
        Console.WriteLine($"Name: {this.name}");
    }
}

