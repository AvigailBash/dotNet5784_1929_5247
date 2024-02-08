using BlApi;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        //if (statusOfProject == BO.StatusOfProject.End)
        //{

        //}

        DO.Task doTask = new DO.Task(boTask.id, boTask.createdAtDate, boTask.alias, boTask.description,
            boTask.isMilestone, boTask.schedualedDate,boTask.requiredEffortTime, boTask.deadlineDate, 
            boTask.startDate, boTask.completeDate, boTask.deliverables, boTask.remarks, boTask.engineer?.id,
            (DO.Engineerlevel?)boTask.coplexity, boTask.isActive);
         //var x=boTask.dependencies.Select(t=>new DO.Dependency(boTask.id,t.id,t.))
        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.Exceptions.BlAlreadyExistsException($"Task with ID={boTask.id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }

        catch(DO.DalAlreadyExistsException ex)
        {
            throw new BO.Exceptions.BlAlreadyExistsException($"Task with ID={id} does not exists", ex);
        }
    }

    public BO.Task Read(int Id)
    {
        DO.Task? doTask = _dal.Task.Read(Id);
        if (doTask == null)
            throw new BO.Exceptions.BlDoesNotExistException($"Task with ID={Id} does Not exist");
        return new BO.Task()
        {
            id = Id,
            alias = doTask.alias,
            description = doTask.description,
            isMilestone = doTask.isMilestone,
            schedualedDate = doTask.schedualedDate,
            requiredEffortTime = doTask.requiredEffortTime,
            deadlineDate = doTask.deadlineDate,
            createdAtDate = doTask.createdAtDate,
            startDate = doTask.startDate,
            completeDate = doTask.completeDate,
            deliverables = doTask.deliverables,
            remarks = doTask.remarks,
            forecastDate = findForecastDate(doTask.schedualedDate, doTask.startDate, doTask.requiredEffortTime),
            engineer = convertFromEngineerToEngineerInTask(doTask.engineerId),
            coplexity = (BO.Engineerlevel?)doTask.coplexity,
            dependencies = findDependencies(doTask),
            isActive = doTask.isActive,
            status=findStatus(doTask)

        };
    }

    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.Task, bool>? filter = null)//filterrrrrr
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select new BO.TaskInList 
                {
                    id = doTask.id,
                    description = doTask.description,
                    alias = doTask.alias,
                    status =findStatus(doTask)
                }); 
    }

    public void Update(BO.Task task)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(task.id);
            if (doTask != null) { _dal.Task.Update(doTask); }
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.Exceptions.BlAlreadyExistsException($"Task with ID={task.id} does not exists", ex);
        }

    }


    public DateTime findForecastDate(DateTime? scheduale ,DateTime? start,TimeSpan? require)
    {
        DateTime maxDate;
        int max= DateTime.Compare(scheduale?? DateTime.MinValue, start?? DateTime.MinValue);
        if (max <=0) { maxDate = start ?? DateTime.MinValue; }
        else{  maxDate = scheduale ?? DateTime.MinValue; }
        maxDate.Add(require?? TimeSpan.Zero);
        return  maxDate;
    }

    public List<BO.TaskInList> findDependencies(DO.Task task)
    {
        IEnumerable<DO.Task?> newList= from DO.Dependency doDependency in _dal.Dependency.ReadAll() where doDependency.dependentTask==task.id select _dal.Task.Read(doDependency.dependsOnTask?? 0);
       List<BO.TaskInList> taskInLists = (from DO.Task t in newList select new BO.TaskInList { id = t.id, description = t.description, alias = t.alias, status = findStatus(t) }).ToList();
        return taskInLists;
    }

    public BO.Status findStatus(DO.Task task) 
    {
        if (task.schedualedDate == null)
            return BO.Status.Unscheduled;
        if(task.startDate== null)
            return BO.Status.Scheduled;
        if(task.completeDate== null)
            return BO.Status.OnTrack;
        return BO.Status.Done;
    }

    public BO.EngineerInTask convertFromEngineerToEngineerInTask(int? id)
    {
        DO.Engineer? engineer = _dal.Engineer.Read(id?? 0);
        BO.EngineerInTask engineerInTask=new BO.EngineerInTask(engineer.id,engineer.name);
        return engineerInTask;
    }
    
    
}


