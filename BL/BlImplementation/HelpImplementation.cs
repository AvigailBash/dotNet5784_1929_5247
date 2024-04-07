using BlApi;
using System;
using System.Security.Cryptography;

namespace BlImplementation;

internal class HelpImplementation : IHelp
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    /// method to initialize the data
    /// </summary>
    public void init() => _dal.Help.init();

    /// <summary>
    /// A method to reset the data
    /// </summary>
    public void reset() => _dal.Help.reset();

    /// <summary>
    /// A method that creates an estimated start date for all tasks
    /// </summary>
    /// <exception cref="BO.Exceptions.BlCannotCreateTheScheduleException"></exception>
    public void AutomaticScheduale()
    {
        Random random = new Random(); // Initialize a random number generator
        DateTime? maxDate = DateTime.MinValue;
        IEnumerable<BO.TaskInList> tasks = s_bl.Task.ReadAll(item => item.dependencies.Count == 0 && item.schedualedDate == null);
            if (tasks.Count() != 0)
            {
                DateTime minimumDate = s_bl.Clock.GetStartOfProject() ?? DateTime.Now;
                //DateTime maximumDate = s_bl.clock.AddDays(3);
               
            TimeSpan require= TimeSpan.FromDays(5);
            //IEnumerable<DO.Task> task = _dal.Task.ReadAll();
            int daysToAdd;
            foreach (BO.TaskInList boTaskInList in tasks)
            {

                BO.Task boTask = s_bl.Task.Read(boTaskInList.id)!;
                if (boTask.requiredEffortTime == null)
                {
                    daysToAdd = random.Next(3, 7);
                    boTask.requiredEffortTime = TimeSpan.FromDays(Math.Max(daysToAdd, 0));
                }
                s_bl.Task.Update(boTask);
                daysToAdd = random.Next(0, 4);
                boTask!.schedualedDate = minimumDate.AddDays(daysToAdd);
                s_bl.Task.Update(boTask);
                boTask.forecastDate = s_bl.Task.findForecastDate(boTask.schedualedDate, boTask.schedualedDate, boTask.requiredEffortTime);
                if (boTask.forecastDate > maxDate)
                {
                    maxDate = boTask.forecastDate;
                }


            }
        }
        if (maxDate > s_bl.Clock.GetEndOfProject())
        {
            throw new BO.Exceptions.BlCannotCreateTheScheduleException("The date of the end date of the project is not enough, you must choose further");
        }


        tasks = s_bl.Task.ReadAll(item => item.schedualedDate == null && item.dependencies.Count != 0);
        foreach (BO.TaskInList boTaskInList in tasks)
        {
            BO.Task boTask = s_bl.Task.Read(boTaskInList.id)!;
            s_bl.Task.FindTheMinimumDate(boTask);

            if (boTask.requiredEffortTime == null)
            {
                int daysToAdd = random.Next(3, 7);
                boTask.requiredEffortTime = TimeSpan.FromDays(Math.Max(daysToAdd, 0));
            }

            boTask.forecastDate = s_bl.Task.findForecastDate(boTask.schedualedDate, boTask.schedualedDate, boTask.requiredEffortTime);
            if (boTask.forecastDate > maxDate)
            {
                maxDate = boTask.forecastDate;
            }
            s_bl.Task.Update(boTask);
        }
        if (maxDate > s_bl.Clock.GetEndOfProject())
        {
            throw new BO.Exceptions.BlCannotCreateTheScheduleException("The date of the end date of the project is not enough, you must choose further");
        }
    }

    /// <summary>
    /// A method that creates a start date for all tasks
    /// </summary>
    public void SetStartDates()
    {
        IEnumerable<BO.TaskInList> tasksList = s_bl.Task.ReadAll(e => e.engineer == null);
        foreach(BO.TaskInList ta in tasksList)
        {
            BO.Task t = s_bl.Task.Read(ta.id);
            BO.Engineer en = s_bl.Engineer.ReadAll(e => e.task == null && e.level >= t.coplexity).FirstOrDefault();
            BO.EngineerInTask engineerInTask = new BO.EngineerInTask() { id = en.id, name = en.name };
            t.engineer = engineerInTask;
            s_bl.Task.Update(t);
        }
        IEnumerable<BO.TaskInList> tasks = s_bl.Task.ReadAll(t => t.startDate == null);
        foreach (BO.TaskInList task in tasks)
        {
            BO.Task newTask = s_bl.Task.Read(task.id)!;
            BO.Task taskForUpdate = new BO.Task() { id=newTask.id, alias= newTask.alias, description= newTask.description, createdAtDate=newTask.createdAtDate, forecastDate=newTask.forecastDate, deliverables=newTask.deliverables, requiredEffortTime=newTask.requiredEffortTime, schedualedDate=newTask.schedualedDate, startDate=newTask.schedualedDate, completeDate=newTask.completeDate, coplexity=newTask.coplexity, dependencies=newTask.dependencies, engineer=newTask.engineer, isActive=newTask.isActive, remarks=newTask.remarks, status=newTask.status };
            s_bl.Task.Update(taskForUpdate);
        }
    }

    /// <summary>
    /// A method that returns all estimated start dates to be null
    /// </summary>
    public void SetNullInScheduale()
    {
        IEnumerable<DO.Task> tasks = _dal.Task.ReadAll();
        if (tasks.Count() != 0)
        {
            foreach (DO.Task doTask in tasks)
            {

                _dal.Task.Update(doTask with { schedualedDate = null });
            }
        }
    }
}
