using DalApi;
using DO;
using System.Linq;
using System.Security.Cryptography;

namespace Dal;

internal class UserImplementation : IUser
{
    public int Create(User item)
    {
        if (Read(item.id) is not null)
        {
            throw new DalAlreadyExistsException($"User with ID={item.id} already exists");
        }
        DataSource.Users?.Add(item);
        return item.id;
    }

    public void Delete(int id)
    {
        User? temp = Read(id);
        if (temp == null)
        {
            throw new DalDoesNotExistException($"User with ID={id} not exists");
        }
        DataSource.Users?.Remove(temp);
        temp = temp with { isActive = false };
        DataSource.Users?.Add(temp);

    }

    public void deleteAll()
    {
        IEnumerable<User> users = ReadAll();
        var t = from user in users where user.id != 1234 select user with { isActive = false };
    }

    public User? Read(Func<User, bool> filter)
    {
        return DataSource.Users?.Select(item => item).Where(filter).FirstOrDefault() ?? throw new DalDoesNotExistException($"Engineer not exists");
    }

    public User? Read(int id)
    {
        return (DataSource.Users?.FirstOrDefault(t => (t != null && t.id == id) && t.isActive == true));
    }

    public IEnumerable<User> ReadAll(Func<User, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Users
                   where filter(item) && item.isActive == true
                   select item;
        }
        return from item in DataSource.Users
               where item.isActive == true
               select item;
    }

    public void Update(User item)
    {
        if (Read(item.id) is null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={item.id} not exists");
        }
        //Delete(item.id);
        DataSource.Users?.Remove(Read(item.id)!);
        DataSource.Users?.Add(item);
    }
}
