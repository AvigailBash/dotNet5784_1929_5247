using BlApi;
using BO;
using DO;
using System;
using System.Security.Cryptography;

namespace BlImplementation;

internal class HelpImplementation : IHelp
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public void init() => _dal.Help.init();


    public void reset() => _dal.Help.reset();

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

        //IEnumerable<BO.TaskInList> task = s_bl.Task.ReadAll();
        //if (tasks.Count() != 0)
        //{
        //    BO.Task newTask;
        //    foreach (BO.TaskInList boTask in task)
        //    {
        //        newTask = s_bl.Task.Read(boTask.id);
        //        if(newTask != null)
        //        {
        //            s_bl.Task.Update(newTask  with { schedualedDate = null, forecastDate = null });

        //        }
        //    }
        //}
    }
}
