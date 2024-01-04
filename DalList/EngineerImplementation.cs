namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.id) is not null)
            throw new Exception($"Engineer with ID={item.id} already exists");
        DataSource.Engineers?.Add(item);
        return item.id;
    }

    public void Delete(int id)
    {
        Engineer? temp = Read(id);
        if (temp == null)
        {
            throw new Exception($"Engineer with ID={id} not exists");
        }
        DataSource.Engineers?.Remove(temp);
        temp = temp with { isActive = false };
        DataSource.Engineers?.Add(temp);

    }


    public Engineer? Read(int id)
    {
        return DataSource.Engineers?.Find(t=>(t!=null&&t.id == id&&t.isActive==true));
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {

        if (Read(item.id) is null)
        {
            throw new Exception($"Engineer with ID={item.id} not exists");
        }
        Delete(item.id);
        DataSource.Engineers?.Add(item);
    }
}

