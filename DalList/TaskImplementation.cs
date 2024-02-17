namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// Creates a new task and gives it an ID
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <returns></returns>
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task copyItem = item with { id = newId};
        DataSource.Tasks?.Add(copyItem);
        return newId;
    }

    /// <summary>
    /// Gets an ID and deletes the task
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        Task? temp = Read(id);
        if(temp==null)
        {
            throw new DalDoesNotExistException($"Task with ID={id} not exists");
        }
        DataSource.Tasks?.Remove(temp);
        temp = temp with { isActive = false };
        DataSource.Tasks?.Add(temp);
        
    }

    /// <summary>
    /// Gets an ID and prints the task if it exists and is active
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <returns></returns>
    public Task? Read(int id)
    {
        return (DataSource.Tasks?.FirstOrDefault(t => (t != null && t.id == id && t.isActive == true))) ?? throw new DalDoesNotExistException($"Task with ID={id} not exists");
      
    }

    /// <summary>
    /// Returns an object according to certain search conditions
    /// </summary>
    /// <param name="filter"> The search conditions on the object </param>
    /// <returns></returns>
    public Task? Read(Func<Task, bool> filter)
    {
        return (DataSource.Tasks?.Select(item => item).Where(filter).FirstOrDefault()) ?? throw new DalDoesNotExistException("Task not exists");
    }

    /// <summary>
    /// Returns a collection of objects according to a certain search condition
    /// </summary>
    /// <param name="filter"> The search conditions on the objects </param>
    /// <returns></returns>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) 
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item) && item.isActive == true
                   select item;
        }
        return from item in DataSource.Tasks
               where item.isActive == true
               select item;
    }


    //// <summary>
    /// Receives details of a task and updates it
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <exception cref="DalDoesNotExistException"> The exception being sent </exception>
    public void Update(Task item)
    {
        if (Read(item.id) is null) 
        {
            throw new DalDoesNotExistException($"Task with ID={item.id} not exists");
        }
        //Delete(item.id);
        DataSource.Tasks?.Remove(Read(item.id)!);
        DataSource.Tasks?.Add(item);
    }

    /// <summary>
    /// A method for deleting all objects from a task entity
    /// </summary>
    public void deleteAll()
    {
        DataSource.Tasks!.Clear();
    }
}
