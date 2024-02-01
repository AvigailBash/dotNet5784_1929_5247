using BlApi;
using static BO.Exceptions;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.id <= 0 || boEngineer.name == null || boEngineer.cost <= 0 || CheckEmail(boEngineer.email) == false)
            throw new BO.Exceptions.BlIncorrectInput($"One of the detail not correct");

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
    
    public void Delete(int id)
    {
        BO.Engineer? boEngineer=Read(id);
        if(boEngineer != null&& boEngineer.task!=null)
        { 
            throw new BO.Exceptions.BlCannotDeleteThisEngineer("Cannot Delete This Engineer");
        }
        try
        {     
              _dal.Engineer.Delete(id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.Exceptions.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        }
    }
   
    public BO.Engineer Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if(doEngineer == null)
        {
            throw new BO.Exceptions.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
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

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter) select new BO.Engineer()
        {
            id = doEngineer.id,
            name = doEngineer.name,
            email = doEngineer.email,
            level = (BO.Engineerlevel?)doEngineer.level,
            cost = doEngineer.cost,
            isActive = doEngineer.isActive,
            task=_dal.Task.ReadAll().Where(t =>t.id==doEngineer.id).Select(t => new BO.TaskInEngineer(t.id,t.alias)).FirstOrDefault()
        });
    }

    public void Update(BO.Engineer engineer)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(engineer.id);
        try
        { 
            if (doEngineer != null)
            {
                if (engineer.name == null || engineer.cost <= 0 || CheckEmail(engineer.email) == false ||engineer.level< (BO.Engineerlevel)doEngineer.level!)
                    throw new BO.Exceptions.BlIncorrectInput($"One of the detail not correct");

                _dal.Engineer.Update(doEngineer);
            }
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.Exceptions.BlDoesNotExistException($"Engineer with ID={doEngineer.id} does Not exist");
        }
    }

    public bool CheckEmail(string email)//בדיקת תקינות מייל
    {
        return true;
    }
}
