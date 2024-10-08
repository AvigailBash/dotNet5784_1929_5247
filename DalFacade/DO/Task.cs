﻿//namespace DO;
namespace DO;

/// <summary>
/// A task entity that enters an automatic ID number and the details of the task
/// </summary>
/// <param name="id"> Automatic identifier of the assignment </param>
/// <param name="alias"> The nickname of the assignment </param>
/// <param name="description"> Description of the assignment </param>
/// <param name="schedualedDate"> Estimated date for the start of the assignment </param>
/// <param name="requiredEffortTime"> The time needed for the task </param>
/// <param name="createdAtDate"> The date the assignment was created </param>
/// <param name="startDate"> Date the assignment started </param>
/// <param name="completeDate"> Date the assignment was completed </param>
/// <param name="deliverables"> the product of the task </param>
/// <param name="remarks"> Notes on the assignment </param>
/// <param name="engineerId"> The identity card of the engineer of the assignment </param>
/// <param name="coplexity"> The difficulty of the assignment </param>
/// <param name="isActive"> Is the assignment active? </param>
public record Task
(
    int id,
    DateTime createdAtDate,
    string? alias = null,
    string? description = null,
    DateTime? schedualedDate = null,
    TimeSpan? requiredEffortTime = null,
    DateTime? startDate = null,
    DateTime? completeDate = null,
    string? deliverables = null,
    string? remarks = null,
    int? engineerId = null,
    DO.Engineerlevel? coplexity = null,
    bool isActive = false
 )
{
    /// <summary>
    /// Empty constructive action for an entity
    /// </summary>
    public Task() : this(0, DateTime.Now) { }

}


