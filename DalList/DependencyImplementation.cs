namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency copyItem = item with { id = newId };
        DataSource.Dependencies?.Add(copyItem);
        return newId;
    }

    public void Delete(int id)
    {
        Dependency? temp = Read(id);
        if (temp == null)
        {
            throw new Exception($"Dependency with ID={id} not exists");
        }
        DataSource.Dependencies?.Remove(temp);
    }
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies?.Find(t => (t != null && t.id == id));
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {

        if (Read(item.id) is null)
        {
            throw new Exception($"Dependency with ID={item.id} not exists");
        }
        Delete(item.id);
        DataSource.Dependencies?.Add(item);
    }
}

