using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class Intilization
{
    private static IDal? s_dal;
    private static readonly Random s_rand = new();
    readonly string s_dependencies_xml = "dependencies";


    /// <summary>
    /// A method that deletes the objects that were added and restarts when you choose this in the program
    /// </summary>
    private static void deleteAll()
    {

        s_dal!.Engineer.deleteAll();
        s_dal!.Dependency.deleteAll();
        s_dal!.Task.deleteAll();
    }

    /// <summary>
    /// Creates new tasks
    /// </summary>
    private static void createTasks()
    {
        string[] aliasNames =
        {
            "planning","design","write","check","correction","update","upgrades","budget","Requirements Gathering","System Architecture Design","Database Design","Technology Stack Selection","Code Planning and Organization",
            "User Interface (UI) Design","Backend Developmen","Integration Testing","Security Implementation","Documentation","Version Control","User Acceptance Testing"
        };
        string? _description = null;
        IEnumerable<DO.Engineer> engineers = s_dal!.Engineer.ReadAll();
        int[] arr = new int[5];
        int i = 0;
        DO.Engineerlevel level;
        foreach (DO.Engineer engineer in engineers)
        {
            arr[i++] = engineer.id;
        }
        i = 0;
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
            DateTime? _schedualedDate = null;
            DateTime _createdAtDate = s_dal.Clock.GetStartOfProject()?? DateTime.Now;
            DateTime? _startDate = null;


            DateTime today =s_dal.Clock.GetStartOfProject()?? DateTime.Now;
            DateTime sixMonthsFromNow = today.AddMonths(6);
            Random random = new Random();
            int range = (sixMonthsFromNow - today).Days;
            int randomNumberOfDays = random.Next(range);
            DateTime randomDate = today.AddDays(randomNumberOfDays);

            TimeSpan? _requiredEffortTime = randomDate - today;

            DateTime? _completeDate = null, _deadlineDate = null;
            bool _isActive = true;
            int engineerId = arr[i];
            i = (i + 1) % 5;
            level = s_dal.Engineer.Read(engineerId)!.level!.Value;
            _deadlineDate = null;
            _requiredEffortTime = null;
            DO.Task newTask = new(0, _createdAtDate, _alias, _description, true, _schedualedDate, _requiredEffortTime, _deadlineDate, _startDate, _completeDate, null, null, /*engineerId*/ null, level, true);
            s_dal!.Task.Create(newTask);

        }

    }

    /// <summary>
    /// Creates new engineers
    /// </summary>
    private static void createEngineer()
    {

        string[] names =
        {
            "Noa Levi","Moshe Choen","Yael Levi","Maor Tal","Dan Bash"
        };
        int i = 0;
        foreach (string name in names)
        {
            int _id;
            int minValue = 200000000;
            int maxValue = 400000000;
            int password;
            int minValueForPassword = 20000;
            int maxValueForPassword = 70000;
            do
                _id = new Random().Next(minValue, maxValue);
            while (s_dal!.Engineer.Read(_id) != null);
            password = new Random().Next(minValueForPassword, maxValueForPassword);

            DO.Engineerlevel level;
            switch (i)
            {
                case 0:
                    level = Engineerlevel.Beginner;
                    break;
                case 1:
                    level = Engineerlevel.AdvancedBeginner;
                    break;
                case 2:
                    level = Engineerlevel.Intermediate;
                    break;
                case 3:
                    level = Engineerlevel.Advanced;
                    break;
                case 4:
                    level = Engineerlevel.Expert;
                    break;
                default:
                    level = Engineerlevel.Beginner;
                    break;
            }
            double _cost = new Random().Next(10000, 20000);
            bool _isActive = true;
            Engineer newEngineer = new Engineer(_id, password, name, "engineer12@gmail.com", level, _cost, _isActive);
            s_dal!.Engineer.Create(newEngineer);
            i++;
        }
    }

    /// <summary>
    /// Creates new dependencies
    /// </summary>
    private static void createDependency()
    {
        string s_dependencies_xml = "dependencies";
        XElement dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        for (int i = 1; i <= 40; i++)
        {
            int _dependentTask;
            int _dependsOnTask;
            Random rand = new Random();
            do
            {
                _dependentTask = new Random().Next(1000, 1010);
                _dependsOnTask = new Random().Next(1000, 1020);

            } while (_dependsOnTask == _dependentTask || s_dal!.Dependency.ReadAll(t => t.dependsOnTask == _dependentTask && t.dependentTask == _dependsOnTask).Count() != 0 || s_dal!.Dependency.ReadAll(t => t.dependsOnTask == _dependsOnTask && t.dependentTask == _dependentTask).Count() != 0);
            Dependency newDependency = new Dependency(0, _dependentTask, _dependsOnTask, true);
            s_dal!.Dependency.Create(newDependency);
        }
    }


    /// <summary>
    /// Checks whether the objects exist properly, if not sends an error and calls for initialization
    /// </summary>
    /// <param name="dal"></param>
    /// <exception cref="NullReferenceException"></exception>
    public static void Do()
    {
        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!");
        s_dal = DalApi.Factory.Get;
        deleteAll();
        Config.resetDependencyId(false);
        Config.resetTaskId(false);
        s_dal.Clock.SetStartOfProject(null);
        s_dal.Clock.SetEndOfProject(null);
        createEngineer();
        createDependency();
        createTasks();
    }


    public static void reset()
    {
        deleteAll();
        Config.resetDependencyId(true);
        Config.resetTaskId(true);
        s_dal.Clock.SetStartOfProject(null);
        s_dal.Clock.SetEndOfProject(null);
    }

}
