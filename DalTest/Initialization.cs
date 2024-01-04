namespace DalTest;
using DalApi;
using DO;
using Dal;
using System.Data.Common;


public static class Initialization
{
    public static ITask? s_dalTask;
    public static IEngineer? s_dalEngineer;
    public static IDependency? s_dalDependency;
    private static readonly Random s_rand = new();

    private static void createTasks()
    {
        string[] aliasNames =
        {
            "planning","design","write","check","correction","update","upgrades"
        };
        string? _description = null;
        foreach (var _alias in aliasNames)
        {
            switch (_alias)
            {
                case "planning":
                    _description = "planning the event";
                    break;
                case "design":
                    _description = "design the project";
                    break;
                case "write":
                    _description = "write the project code";
                    break;
                case "check":
                    _description = "search for errors in the project";
                    break;
                case "correction":
                    _description = "correction errors in the project";
                    break;
                case "update":
                    _description = "update the project";
                    break;
                case "upgrades":
                    _description = "upgrades the code";
                    break;
            }
            DateTime _schedualedDate = DateTime.Today;

            //DateTime today = DateTime.Today;
            //DateTime sixMonthsFromNow = today.AddMonths(6);
            //Random random = new Random();
            //int range = (sixMonthsFromNow - today).Days;
            //int randomNumberOfDays = random.Next(range);
            //// Add the random number of days to today's date
            //TimeSpan _requiredEffortTime = today.AddDays(randomNumberOfDays);
            DateTime today = DateTime.Today;
            DateTime sixMonthsFromNow = today.AddMonths(6);
            Random random = new Random();
            int range = (sixMonthsFromNow - today).Days;
            int randomNumberOfDays = random.Next(range);
            // Add the random number of days to today's date
            DateTime randomDate = today.AddDays(randomNumberOfDays);

            // Calculate the TimeSpan between today and the random date
            TimeSpan _requiredEffortTime = randomDate - today;

            DateTime tenMonthFromNow = today.AddMonths(10);
            DateTime yearFromNow = today.AddMonths(12);
            int range2 = (yearFromNow - tenMonthFromNow).Days;
            randomNumberOfDays = random.Next(range2);
            DateTime _deadlineDate = today.AddDays(randomNumberOfDays);


            DateTime _createdAtDate = DateTime.Today;
            DateTime _startDate = DateTime.Today;

            int range3 = (tenMonthFromNow - sixMonthsFromNow).Days;
            randomNumberOfDays = random.Next(range3);
            DateTime _completeDate = today.AddDays(randomNumberOfDays);
            bool _isActive = true;
            Task newTask = new(0, _alias, _description, true, _schedualedDate, _requiredEffortTime, _deadlineDate, _createdAtDate, _startDate, _completeDate, null, null, null, null, true);
            s_dalTask!.Create(newTask);

        }

    }

    private static void createTask()
    {

        string[] names =
        {
            "Noa Levi","Moshe Choen","Yael Levi","Maor Tal","Dan Bash"
        };
        foreach (string name in names)
        {
            int _id;
            int minValue = 200000000;
            int maxValue = 400000000;
            do
                _id = new Random().Next(minValue, maxValue);
            while (s_dalEngineer!.Read(_id) != null);

            //level
            double _cost = new Random().Next(10000, 20000);
            bool _isActive = true;//לעשות מיל לפי שם
            Engineer newEngineer = new Engineer(_id, name, "engineer12@gmail.com", DO.Engineerlevel.Intermediate, _cost, _isActive);
            s_dalEngineer.Create(newEngineer);
        }
    }



    private static void createDependency()
    {
        int _dependentTask;
        int _dependsOnTask;
        Random rand = new Random();
        do
        {
            _dependentTask = new Random().Next(1000, 1020);
            _dependsOnTask = new Random().Next(1000, 1020);

        } while (_dependsOnTask == _dependentTask);
        Dependency newDependency = new Dependency(0, _dependentTask, _dependsOnTask);
        s_dalDependency?.Create(newDependency);
    }

    public static void Do(ITask? dalTask,IEngineer? dalEngineer,IDependency? dalDependency)
    {
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");

    }

}
