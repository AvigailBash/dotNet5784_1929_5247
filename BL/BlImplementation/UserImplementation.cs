using BlApi;
using DalApi;
using System.Linq;

namespace BlImplementation;

internal class UserImplementation : BlApi.IUser
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.User boUser)
    {
        if (boUser.Id <= 0 || boUser.Password <= 0)
            throw new BO.Exceptions.BlIncorrectInputException($"One of the detail not correct");

        DO.User doUser = new DO.User(boUser.Id, boUser.Password, boUser.IsActive);
        try
        {
            int idUs = _dal.User.Create(doUser);
            return idUs;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.Exceptions.BlAlreadyExistsException($"User with ID={boUser.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        BO.User? boEngineer = Read(id);
        DO.Engineer doEngineer = _dal.Engineer.Read(id)!;
        if (doEngineer.isActive)
        {
            throw new BO.Exceptions.BlCannotDeleteThisEngineerException("Cannot Delete This User");
        }
        try
        {
            _dal.User.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.Exceptions.BlDoesNotExistException($"User with ID={id} does Not exist", ex);
        }
    }

    public BO.User? Read(int id)
    {
        DO.User? doUser = _dal.User.Read(id);
        if (doUser == null)
        {
            return null;
        }
        return new BO.User()
        {
            Id = doUser.id,
            Password = doUser.password,
            IsActive = doUser.isActive,
        };
    }

    public IEnumerable<BO.User> ReadAll(Func<BO.User, bool>? filter = null)
    {
        var users = from DO.User doUser in _dal.User.ReadAll()
                        select new BO.User
                        {
                            Id = doUser.id,
                            Password = doUser.password,
                            IsActive = doUser.isActive,
                        };

        if (filter != null)
        {
            users = users.Where(filter);
        }
        return users;
    }

    public void Update(BO.User user)
    {
        DO.User? doUser = _dal.User.Read(user.Id);
        try
        {
            if (doUser != null)
            {
                if (user.Id <= 0 || user.Password <= 0)
                    throw new BO.Exceptions.BlIncorrectInputException($"One of the detail not correct");
                doUser = doUser with { id = user.Id, password = user.Password, isActive = user.IsActive};
                _dal.User.Update(doUser);
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.Exceptions.BlDoesNotExistException($"User with ID={doUser!.id} does Not exist", ex);
        }
    }
}
