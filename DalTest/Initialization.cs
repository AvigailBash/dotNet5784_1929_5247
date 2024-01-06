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

    //Creates new tasks
    private static void createTasks()
    {
        string[] aliasNames =
        {
            "planning","design","write","check","correction","update","upgrades","budget","Requirements Gathering","System Architecture Design","Database Design","Technology Stack Selection","Code Planning and Organization",
            "User Interface (UI) Design","Backend Developmen","Integration Testing","Security Implementation","Documentation","Version Control","User Acceptance Testing"
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
                case "budget":
                    _description = "how much money you have";
                    break;
                case "Requirements Gathering":
                    _description = "Collect and document the functional and non-functional requirements of the software";
                    break;
                case "System Architecture Design":
                    _description = "Design the overall structure and architecture of the software system";
                    break;
                case "Database Design":
                    _description = "Plan and create the database schema, considering data storage, retrieval, and relationships";
                        break;
                case "Technology Stack Selection":
                    _description = "Choose the appropriate programming languages, frameworks, libraries, and tools for the project";
                    break;
                case "Code Planning and Organization":
                    _description = "Define coding conventions, project structure, and overall organization of the source code";
                    break;
                case "User Interface (UI) Design":
                    _description = "Create wireframes or mockups to design the user interface and user experience";
                    break;
                case "Backend Developmen":
                    _description = "Implement the server-side logic, business rules, and data processing";
                    break;
                case "Integration Testing":
                    _description = "Test the interaction between different modules and components to ensure seamless integration";
                    break;
                case "Security Implementation":
                    _description = "Incorporate security measures to protect against potential vulnerabilities and attacks";
                    break;
                case "Documentation":
                    _description = "Generate comprehensive documentation, including code comments, user manuals, and technical documentation";
                    break;
                case "Version Control":
                    _description = "Set up and manage version control systems (e.g., Git) to track changes and collaborate effectively";
                    break;
                case "User Acceptance Testing":
                    _description = "User Acceptance Testing";
                    break;
            }
            DateTime _schedualedDate = DateTime.Today;
            DateTime _createdAtDate = DateTime.Today;
            DateTime _startDate = DateTime.Today;


            DateTime today = DateTime.Today;
            DateTime sixMonthsFromNow = today.AddMonths(6);
            Random random = new Random();
            int range = (sixMonthsFromNow - today).Days;
            int randomNumberOfDays = random.Next(range);
            DateTime randomDate = today.AddDays(randomNumberOfDays);

            TimeSpan _requiredEffortTime = randomDate - today;

            DateTime _completeDate, _deadlineDate;
            do
            {
                DateTime tenMonthFromNow = today.AddMonths(10);
                DateTime yearFromNow = today.AddMonths(12);
                int range2 = (yearFromNow - tenMonthFromNow).Days;
                randomNumberOfDays = random.Next(range2);
                _deadlineDate = today.AddDays(randomNumberOfDays);



                int range3 = (tenMonthFromNow - sixMonthsFromNow).Days;
                randomNumberOfDays = random.Next(range3);
                _completeDate = today.AddDays(randomNumberOfDays);
            } while (_completeDate > _deadlineDate);
           
            bool _isActive = true;
            Task newTask = new(0, _alias, _description, true, _schedualedDate, _requiredEffortTime, _deadlineDate, _createdAtDate, _startDate, _completeDate, null, null, null, null, true);
            s_dalTask!.Create(newTask);

        }

    }

    //Creates new engineers
    private static void createEngineer()
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

    //Creates new dependencies
    private static void createDependency()
    {
        for (int i = 1; i <= 40; i++)
        {
            int _dependentTask;
            int _dependsOnTask;
            Random rand = new Random();
            do
            {
                _dependentTask = new Random().Next(1000, 1020);
                _dependsOnTask = new Random().Next(1000, 1020);

            } while (_dependsOnTask == _dependentTask);
            Dependency newDependency = new Dependency(0, _dependentTask, _dependsOnTask, true);
            s_dalDependency?.Create(newDependency);
        }
    }

    public static void Do(ITask? dalTask,IEngineer? dalEngineer,IDependency? dalDependency)
    {
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createTasks();
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        createEngineer();
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        createDependency();
    }

}
