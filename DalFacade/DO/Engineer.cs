namespace DO;

public record Engineer
(
    int id,
    string? name = null,
    string? email = null,
    DO.Engineerlevel? level=null,
    double? cost = null,
    bool isActive=false
 )
{
    public Engineer() : this(0) { }

}

