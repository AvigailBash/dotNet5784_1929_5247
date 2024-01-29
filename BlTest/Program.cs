// See https://aka.ms/new-console-template for more information
using DalApi;

Console.WriteLine("Hello, World!");

namespace BlApi
{
    internal class program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public static void inputForTask(int num, int id = 0)
        {
            try
            {
                BO.Task? temp = s_bl.Task.Read(id);
                string? alias, description, deliverables, remarks;
                DateTime? schedualedDate, deadlineDate, createdAtDate, startDate, completeDate;
                TimeSpan? requiredEffortTime;
                BO.Engineerlevel level;
                Console.WriteLine("Enter the details:");
                Console.WriteLine("Press an alias");
                alias = Console.ReadLine();
                Console.WriteLine("Press a description");
                description = Console.ReadLine();
                Console.WriteLine("Press a deliverable");
                deliverables = Console.ReadLine();
                Console.WriteLine("Press a remarks");
                remarks = Console.ReadLine();
                Console.WriteLine("Press a schedualed Date");
                schedualedDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press a deadline Date");
                deadlineDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press a created At Date");
                createdAtDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press a start Date");
                startDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press a complete Date");
                completeDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Press a required Effort Time");
                requiredEffortTime = TimeSpan.Parse(Console.ReadLine());
                Console.WriteLine("Press if it active true or false");
                bool isActive = bool.Parse(Console.ReadLine());
                Console.WriteLine("Press if there is a milestone true or false");
                bool isMileStone = bool.Parse(Console.ReadLine());
                //level = BO.Engineerlevel;
                if (num == 2)
                {
                    if (alias == null)
                    {
                        alias = temp.alias;
                    }
                    if (description == null)
                    {
                        description = temp.description;
                    }
                    if (deliverables == null)
                    {
                        deliverables = temp.deliverables;
                    }
                    if (remarks == null)
                    {
                        remarks = temp.remarks;
                    }
                    if (schedualedDate == null)
                    {
                        schedualedDate = temp.schedualedDate;
                    }
                    if (requiredEffortTime == null)
                    {
                        requiredEffortTime = temp.requiredEffortTime;
                    }
                    if (deadlineDate == null)
                    {
                        deadlineDate = temp.deadlineDate;
                    }
                    if (createdAtDate == null)
                    {
                        createdAtDate = temp.completeDate;
                    }
                    if (startDate == null)
                    {
                        startDate = temp.startDate;
                    }
                    if (completeDate == null)
                    {
                        completeDate = temp.completeDate;
                    }
                }
                BO.Task t = new BO.Task(id, alias, description, );
                //BO.Task = new BO.Task();
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
            Console.WriteLine($"Scheduled Date:  {task.schedualedDate}");
            Console.WriteLine($"Required Effort Time: {task.requiredEffortTime}");
            Console.WriteLine($"Deadline Date: {task.deadlineDate}");
            Console.WriteLine($"Created At Date: {task.createdAtDate}");
            Console.WriteLine($"Start Date: {task.startDate}");
            Console.WriteLine($"Complete Date: {task.completeDate}");
            Console.WriteLine($"Deliverables: {task.deliverables}");
            Console.WriteLine($"Remarks: {task.remarks}");
            Console.WriteLine($"Engineer ID: {task.engineerId}");
            Console.WriteLine($"Coplexity: {task.coplexity}");
            Console.WriteLine($"Is Active: {task.isActive}");
            Console.WriteLine();
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
                Console.WriteLine("Press how much he paid");
                double? cost = double.Parse(Console.ReadLine());
                Console.WriteLine("Press if it active true or false");
                bool isActive = bool.Parse(Console.ReadLine());
                if (num == 2)
                {
                    if (name == "")
                    {
                        name = temp.name;
                    }
                    if (email == "")
                    {
                        email = temp.email;
                    }
                    if (cost == null)
                    {
                        cost = temp.cost;
                    }
                }
                engineer = new BO.Engineer(id, name, email, BO.Engineerlevel.Expert, cost, isActive);
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
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n for Update");
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
                        IEnumerable<Task>? tasks = s_bl.Task!.ReadAll();
                        foreach(BO.Task ta in tasks)
                        {
                            printTask(ta);
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
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n for Update");
                choice = (options)Enum.Parse(typeof(options), Console.ReadLine());
            }
        }

        public static void engineer()
        {
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n for Update");
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
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n for Update");
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