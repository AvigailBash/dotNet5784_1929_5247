namespace DO;

public record Engineer
(
    int id,
    string? name = null,
    string? email = null,
    //DO.Engineer
    double? cost = null
 )
{
    public Engineer() : this(0) { }

}

