namespace BO;

public class MilestoneInList
{
    public int id { get; init; }
    public string? description { get; set; }
    public string? alias { get; set; }
    public BO.Status status { get; set; }
    public double? completionPercentage { get; set; }
}
