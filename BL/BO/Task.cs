using System.Collections.Generic;

namespace BO;

public class Task
{
    public int id { get; init; }
    public string? alias { get; set; }
    public bool isMilestone { get; set; }
    public string? description { get; set; }
    public BO.Status status { get; set; }
    public List<BO.TaskInList>? dependencies { get; init; }
    //public BO.MilestoneInTask milestone { get; set; }
    public DateTime createdAtDate { get; init; }
    public DateTime? schedualedDate { get; set; }
    public DateTime? startDate { get; init; }
    public DateTime? forecastDate { get; set; }
    public DateTime? deadlineDate { get; set; }
    public DateTime? completeDate { get; init; }
    public TimeSpan? requiredEffortTime { get; set; }
    public string? deliverables { get; set; }
    public string? remarks { get; set; }
    public BO.EngineerInTask? engineer { get; set; }
    public BO.Engineerlevel? coplexity { get; set; }
    public bool isActive { get; set; }

}
