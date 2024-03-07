using BlApi;
using BO;
using DO;
using System.Security.Cryptography;

namespace BlImplementation;

internal class HelpImplementation:IHelp
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public void init() => _dal.Help.init();


    public void reset() => _dal.Help.reset();

    public void AutomaticScheduale()
    {
        IEnumerable<BO.TaskInList> tasks = s_bl.Task.ReadAll(item => item.dependencies.Count == 0 && item.schedualedDate == null);
        if (tasks.Count() != 0)
        {
            DateTime minimumDate = s_bl.clock;
            DateTime maximumDate = s_bl.clock.AddDays(8);
            Random random = new Random(); // Initialize a random number generator

            //IEnumerable<DO.Task> task = _dal.Task.ReadAll();

            foreach (BO.TaskInList boTaskInList in tasks)
            {
                int daysToAdd = random.Next(0, 9);
                BO.Task boTask = s_bl.Task.Read(boTaskInList.id)!;
                boTask!.schedualedDate = minimumDate.AddDays(daysToAdd);
                s_bl.Task.Update(boTask);
            }
        }
        


        tasks = s_bl.Task.ReadAll(item => item.schedualedDate == null && item.dependencies.Count != 0);
        foreach (BO.TaskInList boTaskInList in tasks)
        {
            BO.Task boTask = s_bl.Task.Read(boTaskInList.id)!;
            s_bl.Task.FindTheMinimumDate(boTask);
        }
    }

    public void SetNullInScheduale()
    {
        IEnumerable<DO.Task> tasks = _dal.Task.ReadAll();
        if(tasks.Count() != 0)
        {
            foreach (DO.Task doTask in tasks)
            {

                _dal.Task.Update(doTask with { schedualedDate = null });
            }
        }
      
    }
}
