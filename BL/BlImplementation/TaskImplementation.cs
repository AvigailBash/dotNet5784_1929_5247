using BlApi;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;


namespace BlImplementation;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// A call to the method that fetches the data
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private IClock _clock = new ClockImplementation();
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    /// <summary>
    /// A method that creates a new task
    /// </summary>
    /// <param name="task"> Receives a bone and creates it </param>
    /// <returns></returns>
    public int Create(BO.Task boTask)
    {

        if (_clock.statusForProject() == BO.StatusOfProject.Start)
        {
            if (boTask.schedualedDate != null || boTask.engineer != null)
            {
                throw new BO.Exceptions.BlCannotCreateThisTaskException("The project is in the planning stage");
            }
        }
        if (_clock.statusForProject() == BO.StatusOfProject.End)
        {
            throw new BO.Exceptions.BlCannotCreateThisTaskException("The project is in the end stage");
        }
        DO.Task doTask = new DO.Task(boTask.id, boTask.createdAtDate, boTask.alias, boTask.description,
            boTask.isMilestone, boTask.schedualedDate, boTask.requiredEffortTime, boTask.deadlineDate,
            boTask.startDate, boTask.completeDate, boTask.deliverables, boTask.remarks, boTask.engineer?.id,
            (DO.Engineerlevel?)boTask.coplexity, boTask.isActive);
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

    /// <summary>
    /// A method that deletes a certain task, by searching the Id 
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }

        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.Exceptions.BlAlreadyExistsException($"Task with ID={id} does not exists", ex);
        }
    }

    /// <summary>
    /// A method that returns an task by ID
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    /// <returns></returns>
    public BO.Task? Read(int Id)
    {
        DO.Task? doTask = _dal.Task.Read(Id);
        if (doTask == null)
        {
            return null;
        }
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
            status = findStatus(doTask)
        };
    }

    /// <summary>
    /// A method that returns all tasks according to a certain number
    /// </summary>
    /// <param name="filter"> The filter by which to search </param>
    /// <returns></returns>
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        var task = from DO.Task doTask in _dal.Task.ReadAll()
                   select new BO.Task
                   {
                       id = doTask.id,
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
                       status = findStatus(doTask)
                   };

        if (filter != null)
        {
            task = task.Where(filter);
        }
      IEnumerable<BO.TaskInList> t=convertFromTaskToTaskInList(task);

        return t;
    }

    /// <summary>
    /// A method that updates an task
    /// </summary>
    /// <param name="task"> Getting an object to update </param>
    public void Update(BO.Task task)
    {
        try
        {
            int? id = null;
            DO.Task? doTask = _dal.Task.Read(task.id);
            if (_clock.statusForProject() == BO.StatusOfProject.End)
            {
                if (task.requiredEffortTime != doTask.requiredEffortTime || task.startDate != doTask.startDate)
                {
                    throw new BO.Exceptions.BlCannotUpdateThisTaskException("The project is in the end stage");
                }
            }
            foreach(BO.TaskInList taskInList in task.dependencies!)
            {
                DO.Dependency d=new DO.Dependency(0,task.id, taskInList.id,true);
                DO.Dependency d2 = _dal.Dependency.ReadForUpdate(d)!;
                if(d2 == null) { _dal.Dependency.Create(d); }
            }
            if(task.engineer != null)
            {
                id = task.engineer.id;
            }
           doTask = new DO.Task() { id=task.id, createdAtDate=task.createdAtDate, alias=task.alias, description=task.description, isMilestone=task.isMilestone, schedualedDate=task.schedualedDate, requiredEffortTime=task.requiredEffortTime, deadlineDate=task.deadlineDate, startDate=task.startDate, completeDate=task.completeDate, deliverables=task.deliverables, remarks=task.remarks, engineerId=id, coplexity=(DO.Engineerlevel)task.coplexity!, isActive=task.isActive};
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.Exceptions.BlAlreadyExistsException($"Task with ID={task.id} does not exists", ex);
        }

    }

    /// <summary>
    /// A method that calculates the time that should be taken for a certain task according to the dates and the time needed
    /// </summary>
    /// <param name="scheduale"> Estimated date for the start of the assignment </param>
    /// <param name="start"> Actual project start date </param>
    /// <param name="require"> The time needed to complete the task </param>
    /// <returns></returns>
    public DateTime findForecastDate(DateTime? scheduale, DateTime? start, TimeSpan? require)
    {
        DateTime maxDate;
        int max = DateTime.Compare(scheduale ?? DateTime.MinValue, start ?? DateTime.MinValue);
        if (max <= 0) { maxDate = start ?? DateTime.MinValue; }
        else { maxDate = scheduale ?? DateTime.MinValue; }
        maxDate = maxDate.Add(require ?? TimeSpan.Zero);
        return maxDate;
    }

    /// <summary>
    /// A method that finds which tasks the current task depends on
    /// </summary>
    /// <param name="task"> The task received </param>
    /// <returns></returns>
    public List<BO.TaskInList> findDependencies(DO.Task task)
    {
        IEnumerable<DO.Task?> newList = from DO.Dependency doDependency in _dal.Dependency.ReadAll() where doDependency.dependentTask == task.id select _dal.Task.Read(doDependency.dependsOnTask ?? 0);
        List<BO.TaskInList> taskInLists = (from DO.Task t in newList select new BO.TaskInList { id = t.id, description = t.description, alias = t.alias, status = findStatus(t) }).ToList();
        return taskInLists;
    }

    /// <summary>
    /// A method that finds the status of the project
    /// </summary>
    /// <param name="task"> The task received </param>
    /// <returns></returns>
    public BO.Status findStatus(DO.Task task)
    {
        if (task.schedualedDate == null)
            return BO.Status.Unscheduled;
        if (task.startDate == null)
            return BO.Status.Scheduled;
        if (task.completeDate == null)
            return BO.Status.OnTrack;
        return BO.Status.Done;
    }

    /// <summary>
    /// A method that converts from an engineer entity to an engineer entity in a task that contains more details about the engineer
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    /// <returns></returns>
    public BO.EngineerInTask? convertFromEngineerToEngineerInTask(int? id)
    {
        if (id == null)
        {
            return null;
        }
        DO.Engineer? engineer = _dal.Engineer.Read(id ?? 0);
        BO.EngineerInTask engineerInTask = new BO.EngineerInTask() { id = engineer!.id, name = engineer.name };
        return engineerInTask;
    }



    public List<BO.TaskInList> findDependencies(BO.Task task)
    {
        IEnumerable<DO.Task?> newList = from DO.Dependency doDependency in _dal.Dependency.ReadAll() where doDependency.dependentTask == task.id select _dal.Task.Read(doDependency.dependsOnTask ?? 0);
        List<BO.TaskInList> taskInLists = (from DO.Task t in newList select new BO.TaskInList { id = t.id, description = t.description, alias = t.alias, status = findStatus(t) }).ToList();
        return taskInLists;
    }

    public IEnumerable<BO.TaskInList> convertFromTaskToTaskInList(IEnumerable<BO.Task> task)
    {
        IEnumerable<BO.TaskInList> t = from BO.Task temp in task
                                       select new BO.TaskInList
                                       {
                                           id = temp.id,
                                           description = temp.description,
                                           alias = temp.alias,
                                           status = temp.status
                                       };
        return t;
    }


    public void AddDependencies(int id,BO.TaskInList taskInList)
    {

        BO.Task temp = Read(id)!;
        temp.dependencies!.Add(taskInList);
        Update(temp);
    }

    public void RemoveDependencies(BO.Task boTask, BO.TaskInList taskInList)
    {
        DO.Dependency temp = new DO.Dependency(0,boTask.id, taskInList.id,true);
        DO.Dependency temp2 = _dal.Dependency.Read(t => t.dependsOnTask == taskInList.id && t.dependentTask == boTask.id&&t.isActive==true);
        _dal.Dependency.Delete(temp2.id);
    }

    public void FindTheMinimumDate(BO.Task boTask)
    {
        DateTime? minimum = s_bl.clock;
        foreach(BO.TaskInList boTaskInList in boTask.dependencies!) { BO.Task task = s_bl.Task.Read(boTaskInList.id)!; if (minimum < task.forecastDate) minimum = task.forecastDate; }
        boTask.schedualedDate = minimum;
        s_bl.Task.Update(boTask);
    }

    public IEnumerable<BO.TaskInList> GetTasksGroupedByTaskIdSafe()
    {
        IEnumerable<DO.Task> tasks = _dal.Task.ReadAll();
        // ודא שהקלט תקין
        if (tasks == null)
        {
            throw new ArgumentNullException(nameof(tasks));
        }

        // קבלת משימות עם id חוקי
        var filteredTasks = tasks.Where(task => task?.id != null);

        // קיבוץ המשימות לפי id
        IEnumerable<BO.TaskInList> groupedTasks = filteredTasks.GroupBy(task => task.id).Select(group => new BO.TaskInList
        {
            id = group.Key
        });
            

        // החזרת אוסף הקבוצות
        return groupedTasks;
    }


    public IEnumerable<BO.Task> ReadFullTask(Func<BO.Task, bool>? filter = null)
    {
        var task = from DO.Task doTask in _dal.Task.ReadAll()
                   select new BO.Task
                   {
                       id = doTask.id,
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
                       status = findStatus(doTask)
                   };

        if (filter != null)
        {
            task = task.Where(filter);
        }

        return task;
    }

}


