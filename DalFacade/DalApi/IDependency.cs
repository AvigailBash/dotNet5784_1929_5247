namespace DalApi;
using DO;

public interface IDependency : ICrud<Dependency>
{
    public Dependency? ReadForUpdate(Dependency dependency);
}
