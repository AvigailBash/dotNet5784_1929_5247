using BlApi;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer(boEngineer.id, boEngineer.name, boEngineer.email, boEngineer.level, boEngineer.cost, boEngineer.isActive);
        try
        {
            int idEn = _dal.Engineer.Create(doEngineer);
            return idEn;
        }
        catch(DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boEngineer.id} already exists", ex);
        }
    }
    
    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        try
        {
                _dal.Engineer.Delete(id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={doEngineer.id} does Not exist");
        }
    }
   
    public BO.Engineer Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if(doEngineer == null)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        }
        return new BO.Engineer()
        {
            id = doEngineer.id,
            name = doEngineer.name,
            email = doEngineer.email,
            level = doEngineer.level,
            cost = doEngineer.cost,
            isActive = doEngineer.isActive,
        };
    }

    public IEnumerable<IEngineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll() select new BO.Engineer()
        {
            id = doEngineer.id,
            name = doEngineer.name,
            email = doEngineer.email,
            level = doEngineer.level,
            cost = doEngineer.cost,
            isActive = doEngineer.isActive
        });
    }

    public void Update(BO.Engineer engineer)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(engineer.id);
        try
        {
            if (doEngineer != null) 
            {
                _dal.Engineer.Update(doEngineer);
            }
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={doEngineer.id} does Not exist");
        }
    }
}
