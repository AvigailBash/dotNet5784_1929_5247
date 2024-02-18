using BlApi;
using BO;
using DO;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// A call to the method that fetches the data
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private IClock _clock = new ClockImplementation();
    // BO.StatusOfProject status;

    /// <summary>
    /// A method that creates a new engineer
    /// </summary>
    /// <param name="engineer"> Receives a bone and creates it </param>
    /// <returns></returns>
    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.id <= 0 || boEngineer.name == null || boEngineer.cost <= 0 || CheckEmail(boEngineer.email) == false)
            throw new BO.Exceptions.BlIncorrectInputException($"One of the detail not correct");

        DO.Engineer doEngineer = new DO.Engineer(boEngineer.id, boEngineer.name, boEngineer.email,(DO.Engineerlevel?)boEngineer.level, boEngineer.cost, boEngineer.isActive);
        try
        {
            int idEn = _dal.Engineer.Create(doEngineer);
            return idEn;
        }
        catch(DO.DalAlreadyExistsException ex)
        {
            throw new BO.Exceptions.BlAlreadyExistsException($"Engineer with ID={boEngineer.id} already exists", ex);
        }
    }

    /// <summary>
    /// A method that deletes a certain engineer, by searching the Id 
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    public void Delete(int id)
    {
        BO.Engineer? boEngineer=Read(id);
        if (_clock.statusForProject() == BO.StatusOfProject.End)
        {
            if (boEngineer.task != null)
            {
                throw new BO.Exceptions.BlCannotDeleteThisEngineerException("The project is in the end stage");
            }
        }
        if (boEngineer != null && boEngineer.task != null) 
        { 
            throw new BO.Exceptions.BlCannotDeleteThisEngineerException("Cannot Delete This Engineer");
        }
        try
        {     
              _dal.Engineer.Delete(id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.Exceptions.BlDoesNotExistException($"Engineer with ID={id} does Not exist", ex);
        }
    }

    /// <summary>
    /// A method that returns an engineer by ID
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    /// <returns></returns>
    public BO.Engineer? Read(int id)
    {
            DO.Engineer? doEngineer = _dal.Engineer.Read(id);
            if (doEngineer == null)
            {
                return null;
            }
            return new BO.Engineer()
            {
                id = doEngineer.id,
                name = doEngineer.name,
                email = doEngineer.email,
                level = (BO.Engineerlevel?)doEngineer.level,
                cost = doEngineer.cost,
                isActive = doEngineer.isActive,
                task = _dal.Task.ReadAll().Where(t => t.id == doEngineer.id).Select(t => new BO.TaskInEngineer(t.id, t.alias)).FirstOrDefault()!
            };
    }

    /// <summary>
    /// A method that returns all engineers according to a certain number
    /// </summary>
    /// <param name="filter"> The filter by which to search </param>
    /// <returns></returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        var engineers = from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
        select new BO.Engineer
        {
          id = doEngineer.id,
          name = doEngineer.name,
          email = doEngineer.email,
          level = (BO.Engineerlevel?)doEngineer.level,
          cost = doEngineer.cost,
          isActive = doEngineer.isActive,
          task = _dal.Task.ReadAll().Where(t => t.id == doEngineer.id).Select(t => new BO.TaskInEngineer() { id = t.id, alias = t.alias }).FirstOrDefault()
        };

        if (filter != null)
        {
            engineers = engineers.Where(filter);
        }
        return engineers;
    }

    /// <summary>
    /// A method that updates an engineer
    /// </summary>
    /// <param name="engineer"> Getting an object to update </param>
    public void Update(BO.Engineer engineer)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(engineer.id);
        try
        { 
            if (doEngineer != null)
            {
                if (engineer.name == null || engineer.cost <= 0 || CheckEmail(engineer.email) == false ||engineer.level< (BO.Engineerlevel)doEngineer.level!)
                    throw new BO.Exceptions.BlIncorrectInputException($"One of the detail not correct");
                doEngineer = doEngineer with { name = engineer.name, email = engineer.email, cost = engineer.cost, isActive = engineer.isActive, level = (DO.Engineerlevel)engineer.level! };
                _dal.Engineer.Update(doEngineer);
            }
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.Exceptions.BlDoesNotExistException($"Engineer with ID={doEngineer!.id} does Not exist", ex);
        }
    }

    //public IEnumerable<BO.Engineer> groupByLevel()
    //{
    //    IEnumerable<BO.Engineer> en = ReadAll();
    //    var group = from e in en where e.level == BO.Engineerlevel.Beginner orderby e.id group e.id by e.name![0] into newGroup select newGroup;
    //    return group;
    //}

    /// <summary>
    /// A method that checks the integrity of the email
    /// </summary>
    /// <param name="email"> The received email </param>
    /// <returns></returns>
    public bool CheckEmail(string email)//בדיקת תקינות מייל
    {

        // Regular expression pattern for validating email addresses
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        // Check if the email matches the pattern
        return Regex.IsMatch(email, pattern);
    }
}
