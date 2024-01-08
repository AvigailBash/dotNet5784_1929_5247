namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    // Creates a new engineer and gives it an ID
    public int Create(Engineer item)
    {
        if (Read(item.id) is not null)
        {
            throw new DalAlreadyExistsException($"Student with ID={item.id} already exists");
        }           
        DataSource.Engineers?.Add(item);
        return item.id;
    }

    // Gets an ID and deletes the engineer
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

    // Gets an ID and prints the engineer if it exists and is active
    public Engineer? Read(int id)
    {
        return DataSource.Engineers?.Find(t=>(t!=null&&t.id == id&&t.isActive==true));
    }

    // Prints the active engineers
    public List<Engineer> ReadAll()
    {
        return DataSource.Engineers!.FindAll(t => t.isActive == true);
    }
    //public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    //{
    //    if (filter != null)
    //    {
    //        return from item in DataSource.Engineers
    //               where filter(item)
    //               select item;
    //    }
    //    return from item in DataSource.Engineers
    //           select item;
    //}


    // Receives details of a engineer and updates it
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

