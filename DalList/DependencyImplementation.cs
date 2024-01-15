namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Creates a new dependency and gives it an ID
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <returns></returns>
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency copyItem = item with { id = newId };
        DataSource.Dependencies?.Add(copyItem);
        return newId;
    }

    /// <summary>
    /// Gets an ID and deletes the dependency
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        Dependency? temp = Read(id);
        if (temp == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} not exists");
        }
        DataSource.Dependencies?.Remove(temp);
    }

    /// <summary>
    /// Gets an ID and prints the dependency if it exists and is active
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <returns></returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies?.FirstOrDefault(t => (t != null && t.id == id && t.isActive == true));
    }

    /// <summary>
    /// Returns an object according to certain search conditions
    /// </summary>
    /// <param name="filter"> The search conditions on the object </param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies?.Select(item => item).FirstOrDefault();
    }

    /// <summary>
    /// Returns a collection of objects according to a certain search condition
    /// </summary>
    /// <param name="filter"> The search conditions on the objects </param>
    /// <returns></returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) 
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

    /// <summary>
    /// Receives details of a dependency and updates it
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <exception cref="DalDoesNotExistException"> The exception being sent </exception>
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

