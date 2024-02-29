namespace DO;

/// <summary>
/// An engineer entity that includes his personal details
/// </summary>
/// <param name="id"> Engineer's ID </param>
/// <param name="name"> The name of the engineer </param>
/// <param name="email"> The engineer's email </param>
/// <param name="level"> Engineer level </param>
/// <param name="cost"> The salary of the engineer </param>
/// <param name="isActive"> Is the engineer active? </param>
public record Engineer
(
    int id,
    int password,
    string? name = null,
    string? email = null,
    DO.Engineerlevel? level = null,
    double? cost = null,
    bool isActive = false
 )
{
    /// <summary>
    /// Empty constructive action for an entity
    /// </summary>
    public Engineer() : this(0, 0) { }

}

