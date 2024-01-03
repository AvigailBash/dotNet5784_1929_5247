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
    string? deliverables = null,
    string? remarks = null,
    int? ingineerId = null
//DO.e
)
{
    public DateTime createdAtDate => DateTime.Now;
    public DateTime startDate => DateTime.Now;
    public DateTime comleteDate => DateTime.Now;
    public Task() : this(0) { }
    //public Task(int i)
    //{
    //    this.id = i;
    //}

}
