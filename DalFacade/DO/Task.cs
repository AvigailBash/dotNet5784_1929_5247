namespace DO;

public record Task
(
    int id,
    string? alias = null,
    string? description = null,
    bool isMilestone = false,
    DateTime? schedualedDate = null,
    TimeSpan? requiredEffortTime = null,
    DateTime? deadlineDate = null,
    DateTime? createdAtDate = null,
    DateTime? startDate = null,    
    DateTime? completeDate = null,
    string? deliverables = null,
    string? remarks = null,
    int? ingineerId = null,
    DO.Engineerlevel? coplexity=null,
    bool isActive=false

)
{
    public Task() : this(0) { }
    
}
