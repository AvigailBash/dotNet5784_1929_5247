using System.Collections.Generic;

namespace BO;
/// <summary>
/// An task entity that contains details about the task
/// </summary>
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

    /// <summary>
    /// An empty constructor action that initializes an empty object
    /// </summary>
    public Task()
    {
        this.id = 0;
        this.alias = null;
        this.isMilestone = false;
        this.description = null;
        this.status = BO.Status.Unscheduled;
        this.dependencies = null;
        this.createdAtDate = DateTime.MinValue;
        this.schedualedDate = null;
        this.startDate = null;
        this.forecastDate = null;
        this.deadlineDate = null;
        this.completeDate = null;
        this.requiredEffortTime = null;
        this.deliverables = null;
        this.remarks = null;
        this.engineer = null;
        this.coplexity = BO.Engineerlevel.Beginner;
        this.isActive = false;
    }
}
