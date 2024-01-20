namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Creates a new engineer and gives it an ID
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <returns></returns>
    public int Create(Engineer item)
    {
        if (Read(item.id) is not null)
        {
            throw new DalAlreadyExistsException($"Student with ID={item.id} already exists");
        }           
        DataSource.Engineers?.Add(item);
        return item.id;
    }

    /// <summary>
    /// Gets an ID and deletes the engineer
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        Engineer? temp = Read(id);
        if (temp == null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} not exists");
        }
        DataSource.Engineers?.Remove(temp);
        temp = temp with { isActive = false };
        DataSource.Engineers?.Add(temp);

    }

    /// <summary>
    /// Gets an ID and prints the engineer if it exists and is active
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <returns></returns>
    public Engineer? Read(int id)
    {
        return DataSource.Engineers?.FirstOrDefault(t => (t != null && t.id == id && t.isActive == true));
    }

    /// <summary>
    /// Returns an object according to certain search conditions
    /// </summary>
    /// <param name="filter"> The search conditions on the object </param>
    /// <returns></returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers?.Select(item => item).FirstOrDefault();
    }

    /// <summary>
    /// Returns a collection of objects according to a certain search condition
    /// </summary>
    /// <param name="filter"> The search conditions on the objects </param>
    /// <returns></returns>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) 
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }


    //// <summary>
    /// Receives details of a dependency and updates it
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <exception cref="DalDoesNotExistException"> The exception being sent </exception>
    public void Update(Engineer item)
    {

        if (Read(item.id) is null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={item.id} not exists");
        }
        Delete(item.id);
        DataSource.Engineers?.Add(item);
    }
}

