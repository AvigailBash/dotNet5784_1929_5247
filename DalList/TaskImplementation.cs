namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    // Creates a new task and gives it an ID
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task copyItem = item with { id = newId};
        DataSource.Tasks?.Add(copyItem);
        return newId;
    }

    // Gets an ID and deletes the task
    public void Delete(int id)
    {
        Task? temp = Read(id);
        if(temp==null)
        {
            throw new Exception($"Task with ID={id} not exists");
        }
        DataSource.Tasks?.Remove(temp);
        temp = temp with { isActive = false };
        DataSource.Tasks?.Add(temp);
        
    }

    // Gets an ID and prints the task if it exists and is active
    public Task? Read(int id)
    {
        return DataSource.Tasks?.Find(t => (t != null && t.id == id && t.isActive == true));
    }

    // Prints the active tasks
    public List<Task> ReadAll()
    {
        return DataSource.Tasks!.FindAll(d => d.isActive == true);
    }

    // Receives details of an task and updates it
    public void Update(Task item)
    {
        if (Read(item.id) is null) 
        {
            throw new Exception($"Task with ID={item.id} not exists");
        }
        Delete(item.id);
        DataSource.Tasks?.Add(item);
    }
}
