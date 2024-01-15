namespace DO;

/// <summary>
/// The fire extinguisher department in all errors
/// </summary>

[Serializable]
public class DalDoesNotExistException : Exception
{
    /// <summary>
    /// A class that handles a nonexistent object type error
    /// </summary>
    /// <param name="message"> The error message sent to the user </param>
    public DalDoesNotExistException(string? message) : base(message) { }
}

[Serializable]
public class DalAlreadyExistsException : Exception
{
    /// <summary>
    /// A class that handles an error of an object type that already exists
    /// </summary>
    /// <param name="message"> The error message sent to the user </param>
    public DalAlreadyExistsException(string? message) : base(message) { }
}
[Serializable]
public class DalDeletionImpossible : Exception
{
    /// <summary>
    /// A class that handles an undeletable object type error
    /// </summary>
    /// <param name="message"> The error message sent to the user </param>
    public DalDeletionImpossible(string? message) : base(message) { }
}