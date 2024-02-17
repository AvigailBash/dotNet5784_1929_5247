namespace BO;

internal class Exceptions
{
    /// <summary>
    /// Throw exception when the bone does not exist
    /// </summary>
    [Serializable]
    public class BlDoesNotExistException : Exception
    {
        public BlDoesNotExistException(string? message) : base(message) { }
        public BlDoesNotExistException(string message, Exception innerException)
                    : base(message, innerException) { }
    }

    /// <summary>
    /// Throw exception when the bone already exists
    /// </summary>
    [Serializable]
    public class BlAlreadyExistsException : Exception
    {
        public BlAlreadyExistsException(string? message) : base(message) { }
        public BlAlreadyExistsException(string message, Exception innerException)
                    : base(message, innerException) { }
    }

    /// <summary>
    /// An exception throw when the object cannot be deleted
    /// </summary>
    public class BlDeletionImpossibleException : Exception
    {
        public BlDeletionImpossibleException(string? message) : base(message) { }
        public BlDeletionImpossibleException(string message, Exception innerException)
                    : base(message, innerException) { }
    }

    /// <summary>
    /// Throwing an exception to null
    /// </summary>
    [Serializable]
    public class BlNullPropertyException : Exception
    {
        public BlNullPropertyException(string? message) : base(message) { }
    }

    /// <summary>
    /// Throwing an exception when the input is incorrect
    /// </summary>
    [Serializable]
    public class BlIncorrectInputException : Exception
    {
        public BlIncorrectInputException(string? message) : base(message) { }
    }

    /// <summary>
    /// Throwing an exception when the engineer cannot be deleted
    /// </summary>
    [Serializable]
    public class BlCannotDeleteThisEngineerException : Exception
    {
        public BlCannotDeleteThisEngineerException(string? message) : base(message) { }
    }

    /// <summary>
    /// Throw an exception when the status cannot be changed
    /// </summary>
    [Serializable]
    public class BlCannotChangeInThisStatusException : Exception
    {
        public BlCannotChangeInThisStatusException(string? message) : base(message) { }
    }

    /// <summary>
    /// Throw an exception when the schedule cannot be created
    /// </summary>
    [Serializable]
    public class BlCannotCreateTheScheduleException : Exception
    {
        public BlCannotCreateTheScheduleException(string? message) : base(message) { }
    }

    [Serializable]
    public class BlCannotCreateThisTaskException : Exception
    {
        public BlCannotCreateThisTaskException(string? message) : base(message) { }
    }


    [Serializable]
    public class BlCannotUpdateThisTaskException : Exception
    {
        public BlCannotUpdateThisTaskException(string? message) : base(message) { }
    }
}
