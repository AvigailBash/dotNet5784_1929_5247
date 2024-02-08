namespace BO;

internal class Exceptions
{
    [Serializable]
    public class BlDoesNotExistException : Exception
    {
        public BlDoesNotExistException(string? message) : base(message) { }
        public BlDoesNotExistException(string message, Exception innerException)
                    : base(message, innerException) { }
    }
    [Serializable]
    public class BlAlreadyExistsException : Exception
    {
        public BlAlreadyExistsException(string? message) : base(message) { }
        public BlAlreadyExistsException(string message, Exception innerException)
                    : base(message, innerException) { }
    }
    public class BlDeletionImpossible : Exception
    {
        public BlDeletionImpossible(string? message) : base(message) { }
        public BlDeletionImpossible(string message, Exception innerException)
                    : base(message, innerException) { }
    }

    [Serializable]
    public class BlNullPropertyException : Exception
    {
        public BlNullPropertyException(string? message) : base(message) { }
    }


    [Serializable]
    public class BlIncorrectInput : Exception
    {
        public BlIncorrectInput(string? message) : base(message) { }
    }

    [Serializable]
    public class BlCannotDeleteThisEngineer : Exception
    {
        public BlCannotDeleteThisEngineer(string? message) : base(message) { }
    }
    [Serializable]
    public class BlCannotChangeInThisStatus : Exception
    {
        public BlCannotChangeInThisStatus(string? message) : base(message) { }
    }

}
