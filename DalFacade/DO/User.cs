namespace DO;

public record User
(
    int id,
    int password,
    bool isActive
)
{
    //public User() : this(0) { }
}