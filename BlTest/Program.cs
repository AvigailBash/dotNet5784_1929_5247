// See https://aka.ms/new-console-template for more information
using DalApi;
using System.Reflection.Emit;


namespace BlTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public static void inputForTask(int num, int id = 0)
        {
            try
            {
                BO.Task? temp = s_bl.Task.Read(id);
                List<BO.TaskInList>? taskList = new List<BO.TaskInList>();
                BO.EngineerInTask engineer;
                Console.WriteLine("Enter the details:");
                Console.WriteLine("Press an alias");
                string? alias = Console.ReadLine();
                Console.WriteLine("Press a description");
                string? description = Console.ReadLine();
                Console.WriteLine("Press a deliverable");
                string? deliverables = Console.ReadLine();
                Console.WriteLine("Press a remarks");
                string? remarks = Console.ReadLine();
                Console.WriteLine("Press a schedualed Date");
                DateTime? schedualedDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press a deadline Date");
                DateTime?  deadlineDate = DateTime.Parse(Console.ReadLine());
              
                Console.WriteLine("Press a start Date");
                DateTime?  startDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press a complete Date");
                DateTime?  completeDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press a required Effort Time");
                TimeSpan? requiredEffortTime = TimeSpan.Parse(Console.ReadLine());
                Console.WriteLine("press the forecast Date");
                DateTime?  forecastDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press if it active true or false");
                bool isActive = bool.Parse(Console.ReadLine());
                Console.WriteLine("Press if there is a milestone true or false");
                bool isMileStone = bool.Parse(Console.ReadLine());
               
                Console.WriteLine("press for level:  \n 0 for Beginner \n 1 for AdvancedBeginner \n 2 for Intermediate \n 3 for Advanced \n 4 for Expert");
                BO.Engineerlevel? level = (BO.Engineerlevel)int.Parse(Console.ReadLine());
                Console.WriteLine("press details of engineer");
                Console.WriteLine("press the id");
                int engineerId = int.Parse(Console.ReadLine());
                Console.WriteLine("press the name");
                string name = Console.ReadLine();
                BO.EngineerInTask? engineerInTask = new BO.EngineerInTask(engineerId, name);
                Console.WriteLine("press the number of dependencies");
                int amountForDependencies = int.Parse(Console.ReadLine());
                for (int i = 0; i < amountForDependencies; i++)
                {
                    Console.WriteLine("press the details:");
                    Console.WriteLine("press the id");
                    int dId = int.Parse(Console.ReadLine());
                    Console.WriteLine("press the description");
                    string dDescription = Console.ReadLine();
                    Console.WriteLine("press the alias");
                    string dAlias = Console.ReadLine();
                    Console.WriteLine("press for stutus: \n0 for Unscheduled \n 1 for Scheduled \n 2 for OnTrack \n 3 for Done");
                    BO.Status dStatus = (BO.Status)int.Parse(Console.ReadLine());
                    BO.TaskInList taskInList = new BO.TaskInList(dId, dAlias, dDescription, dStatus);
                    taskList.Add(taskInList);
                }
                BO.Task t;
                if (num == 2)
                {
                    DateTime? createdAtDate2;
                    BO.Status? status2;
                    Console.WriteLine("Press a created At Date");
                    createdAtDate2 = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("press for stutus: \n0 for Unscheduled \n 1 for Scheduled \n 2 for OnTrack \n 3 for Done");
                    status2 = (BO.Status)int.Parse(Console.ReadLine());
                    t = new BO.Task(id, alias ?? temp.alias, isMileStone, description ?? temp.description, status2 ?? temp.status, taskList ?? temp.dependencies, createdAtDate2 ?? temp.createdAtDate, schedualedDate ?? temp.schedualedDate, startDate ?? temp.startDate, forecastDate ?? temp.forecastDate, deadlineDate ?? temp.deadlineDate, completeDate ?? temp.completeDate, requiredEffortTime ?? temp.requiredEffortTime, deliverables ?? temp.deliverables, remarks ?? temp.remarks, engineerInTask ?? temp.engineer, level ?? temp.coplexity, isActive);
                    //t.id
                    t.alias = alias ?? temp.alias;
                    t.isMilestone = isMileStone;
                    t.description = description ?? temp.description;
                    t.status = status2 ?? temp.status;
                    //t.dependencies = taskList ?? temp.dependencies;
                    //t.createdAtDate = createdAtDate ?? temp.createdAtDate;
                }
                else
                {
                    DateTime createdAtDate1;
                    BO.Status status1;
                    Console.WriteLine("Press a created At Date");
                    createdAtDate1 = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("press for stutus: \n0 for Unscheduled \n 1 for Scheduled \n 2 for OnTrack \n 3 for Done");
                    status1 = (BO.Status)int.Parse(Console.ReadLine());
                    t = new BO.Task(id, alias, isMileStone, description, status1, taskList, createdAtDate1, schedualedDate, startDate, forecastDate, deadlineDate, completeDate, requiredEffortTime, deliverables, remarks, engineerInTask, level, isActive);
                }
                if (num == 1)
                    s_bl.Task!.Create(t);
                else
                    s_bl.Task!.Update(t);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void printTask(BO.Task task)
        {
            Console.WriteLine($"ID: {task.id}");
            Console.WriteLine($"Alias: {task.alias}");
            Console.WriteLine($"Description: {task.description}");
            Console.WriteLine($"Is Milestone: {task.isMilestone}");
            Console.WriteLine($"Status: {task.status}");
            Console.WriteLine($"Dependencies: {task.dependencies}");
            Console.WriteLine($"Scheduled Date:  {task.schedualedDate}");
            Console.WriteLine($"Required Effort Time: {task.requiredEffortTime}");
            Console.WriteLine($"Deadline Date: {task.deadlineDate}");
            Console.WriteLine($"Created At Date: {task.createdAtDate}");
            Console.WriteLine($"Start Date: {task.startDate}");
            Console.WriteLine($"Complete Date: {task.completeDate}");
            Console.WriteLine($"Forecast Date: {task.forecastDate}");
            Console.WriteLine($"Deliverables: {task.deliverables}");
            Console.WriteLine($"Remarks: {task.remarks}");
            task.engineer?.ToString();
            Console.WriteLine($"Coplexity: {task.coplexity}");
            Console.WriteLine($"Is Active: {task.isActive}");
        }
        public static void PrintTaskInList(BO.TaskInList taskInList)
        {
            Console.WriteLine($"ID: {taskInList.id}");
            Console.WriteLine($"Description: {taskInList.description}");
            Console.WriteLine($"Alias: {taskInList.alias}");
            Console.WriteLine($"Status: {taskInList.status}");
        }

        public static void inputForEngineer(int num, int id = 0)
        {
            BO.Engineer engineer;
            try
            {
                Console.WriteLine("Enter the details:");
                BO.Engineer? temp = s_bl.Engineer.Read(id);
                Console.WriteLine("Press a name");
                string? name = Console.ReadLine();
                Console.WriteLine("Press an email");
                string? email = Console.ReadLine();
                Console.WriteLine("press for level:  \n 0 for Beginner \n 1 for AdvancedBeginner \n 2 for Intermediate \n 3 for Advanced \n 4 for Expert");
                BO.Engineerlevel? level = (BO.Engineerlevel)int.Parse(Console.ReadLine());
                Console.WriteLine("Press how much he paid");
                double? cost = double.Parse(Console.ReadLine());
                Console.WriteLine("Press if it active true or false");
                bool isActive = bool.Parse(Console.ReadLine());
                Console.WriteLine("press the details of the task");
                Console.WriteLine("press the id");
                int idTask = int.Parse(Console.ReadLine());
                Console.WriteLine("press thr alias");
                string alias = Console.ReadLine();
                BO.TaskInEngineer? taskInEngineer = new BO.TaskInEngineer(idTask, alias);
                if(num == 2)
                {
                    engineer = new BO.Engineer(id, name ?? temp.name, email ?? temp.email, level ?? temp.level, cost ?? temp.cost, isActive, taskInEngineer?? temp.task);
                }
                else
                {
                    engineer = new BO.Engineer(id, name, email, level, cost, isActive, taskInEngineer); ;
                }
                if (num == 1)
                    s_bl.Engineer!.Create(engineer);
                else
                    s_bl.Engineer!.Update(engineer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void printEngineer(BO.Engineer engineer)
        {
            Console.WriteLine($"ID: {engineer.id}");
            Console.WriteLine($"Name: {engineer.name}");
            Console.WriteLine($"Email: {engineer.email}");
            Console.WriteLine($"Level: {engineer.level}");
            Console.WriteLine($"Cost: {engineer.cost}");
            Console.WriteLine($"Active: {engineer.isActive}");
            Console.WriteLine();
        }
        enum options { EXIT, CREATE, READ, READALL, DELETE, UPDATE};

        public static void task()
        {
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
            options choice = (options)Enum.Parse(typeof(options), Console.ReadLine());
            while(choice != 0)
            {
                int id;
                switch(choice)
                {
                    case options.CREATE:
                        inputForTask(1);
                        break;
                    case options.READ:
                        Console.WriteLine("Press an id");
                        id = int.Parse(Console.ReadLine());
                        BO.Task? task = s_bl.Task!.Read(id);
                        if (task != null)
                        {
                            printTask(task);
                        }
                        break;
                    case options.READALL:
                        IEnumerable<BO.TaskInList>? tasks = s_bl.Task!.ReadAll();
                        foreach(BO.TaskInList ta in tasks)
                        {
                            PrintTaskInList(ta);
                        }
                        break;
                    case options.DELETE:
                        Console.WriteLine("Press an id");
                        id = int.Parse(Console.ReadLine());
                        s_bl.Task.Delete(id);
                        break;
                    case options.UPDATE:
                        Console.WriteLine("Press an id");
                        id = int.Parse(Console.ReadLine());
                        inputForTask(2, id);
                        break;
                    default:
                        Console.WriteLine("Wrong answer");
                        break;
                }
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
                choice = (options)Enum.Parse(typeof(options), Console.ReadLine());
            }
        }

        public static void engineer()
        {
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
            options choice = (options)Enum.Parse(typeof(options), Console.ReadLine());
            while (choice != 0)
            {
                int id;
                switch (choice)
                {
                    case options.CREATE:
                        Console.WriteLine("Press an id");
                        id = int.Parse(Console.ReadLine());
                        inputForEngineer(1, id);
                        break;
                    case options.READ:
                        Console.WriteLine("Press an id");
                        id = int.Parse(Console.ReadLine());
                        BO.Engineer? engineer = s_bl.Engineer!.Read(id);
                        if (task != null)
                        {
                            printEngineer(engineer);
                        }
                        break;
                    case options.READALL:
                        IEnumerable<BO.Engineer>? engineers = s_bl.Engineer!.ReadAll();
                        foreach (BO.Engineer en in engineers)
                        {
                            printEngineer(en);
                        }
                        break;
                    case options.DELETE:
                        Console.WriteLine("Press an id");
                        id = int.Parse(Console.ReadLine());
                        s_bl.Engineer.Delete(id);
                        break;
                    case options.UPDATE:
                        Console.WriteLine("Press an id");
                        id = int.Parse(Console.ReadLine());
                        inputForEngineer(2, id);
                        break;
                    default:
                        Console.WriteLine("Wrong answer");
                        break;
                }
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
                choice = (options)Enum.Parse(typeof(options), Console.ReadLine());
            }

        }
        enum menu { EXIT, TASK, ENGINEER};
        static void Main(string[] args)
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer");
            menu choice = (menu)Enum.Parse(typeof(menu), Console.ReadLine());
            while (choice != 0) 
            {
                switch (choice)
                {
                    case menu.TASK:
                        task();
                        break;
                    case menu.ENGINEER:
                        engineer();
                        break;
                    default:
                        Console.WriteLine("Wrong answer");
                        break;

                }
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer");
                choice = (menu)Enum.Parse(typeof(menu), Console.ReadLine());
            }
            
        }
    }
}