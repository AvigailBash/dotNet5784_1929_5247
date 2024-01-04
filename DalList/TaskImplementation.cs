namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task copyItem = item with { id = newId};
        DataSource.Tasks?.Add(copyItem);
        return newId;
    }

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

    public Task? Read(int id)
    {
        return DataSource.Tasks?.Find(t => (t != null && t.id == id && t.isActive == true));
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        if(Read(item.id)is null)
        {
            throw new Exception($"Task with ID={item.id} not exists");
        }
        Delete(item.id);
        DataSource.Tasks?.Add(item);
    }
}
