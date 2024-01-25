using BlApi;
using BO;
using DO;
using System.Threading.Tasks;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;//
    public int Create(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task(boTask.id, boTask.createdAtDate, boTask.alias, boTask.description,
            boTask.isMilestone, boTask.schedualedDate,boTask.requiredEffortTime, boTask.deadlineDate, 
            boTask.startDate, boTask.completeDate, boTask.deliverables, boTask.remarks, boTask.engineer.id,
            boTask.coplexity, boTask.isActive);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.id} already exists", ex);
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
            throw new BO.BlAlreadyExistsException($"Task with ID={id} does not exists", ex);
        }
    }

    public BO.Task Read(int Id)
    {
        DO.Task? doTask = _dal.Task.Read(Id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={Id} does Not exist");
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
            engineer = _dal.Engineer.Read(doTask.engineerId),
            coplexity = doTask.coplexity,
            isActive = doTask.isActive
        };


    }

    public IEnumerable<ITask> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select new BO.TaskInList
                {
                    id = doTask.id,
                    description = doTask.description,
                    alias = doTask.alias,
                    status = null//איך אפשר להגדיר את סטטוס אם הוא לא נמצא ב DO
                }); ;

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
            throw new BO.BlAlreadyExistsException($"Task with ID={task.id} does not exists", ex);
        }


    }
}
