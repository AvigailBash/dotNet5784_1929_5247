namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    // Creates a new dependency and gives it an ID
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency copyItem = item with { id = newId };
        DataSource.Dependencies?.Add(copyItem);
        return newId;
    }

    // Gets an ID and deletes the dependency
    public void Delete(int id)
    {
        Dependency? temp = Read(id);
        if (temp == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} not exists");
        }
        DataSource.Dependencies?.Remove(temp);
    }

    // Gets an ID and prints the dependency if it exists and is active
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies?.Find(t => (t != null && t.id == id && t.isActive == true));
    }

    // Prints the active dependencies
    //public List<Dependency> ReadAll()
    //{
    //    return DataSource.Dependencies!.FindAll(t => t.isActive == true);
    //}
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
    }


    // Receives details of a dependency and updates it
    public void Update(Dependency item)
    {

        if (Read(item.id) is null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={item.id} not exists");
        }
        Delete(item.id);
        DataSource.Dependencies?.Add(item);
    }
}

