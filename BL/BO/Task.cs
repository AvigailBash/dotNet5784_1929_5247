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
    public Task(int id, string? alias, bool isMilestone, string? description, Status status, List<TaskInList>? dependencies, DateTime createdAtDate, DateTime? schedualedDate, DateTime? startDate, DateTime? forecastDate, DateTime? deadlineDate, DateTime? completeDate, TimeSpan? requiredEffortTime, string? deliverables, string? remarks, EngineerInTask? engineer, Engineerlevel? coplexity, bool isActive)
    {
        this.id = id;
        this.alias = alias;
        this.isMilestone = isMilestone;
        this.description = description;
        this.status = status;
        this.dependencies = dependencies;
        this.createdAtDate = createdAtDate;
        this.schedualedDate = schedualedDate;
        this.startDate = startDate;
        this.forecastDate = forecastDate;
        this.deadlineDate = deadlineDate;
        this.completeDate = completeDate;
        this.requiredEffortTime = requiredEffortTime;
        this.deliverables = deliverables;
        this.remarks = remarks;
        this.engineer = engineer;
        this.coplexity = coplexity;
        this.isActive = isActive;
    }
}
