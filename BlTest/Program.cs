// See https://aka.ms/new-console-template for more information
using DalApi;
using DO;
using BO;
using System.Reflection.Emit;


namespace BlTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// A method that captures details for a task in update or create state
        /// </summary>
        /// <param name="num"> Checking whether the status is creation or update </param>
        /// <param name="Id"> The automatic number of the task, is equal to zero in the state of creation </param>
        public static void inputForTask(int num, int Id = 0)
        {
            try
            {
                if (num == 2)
                {
                    BO.Task? temp = s_bl.Task.Read(Id);
                }
                List<BO.TaskInList>? taskList = new List<BO.TaskInList>();
                BO.EngineerInTask engineer;
                Console.WriteLine("Enter the details:");
                Console.WriteLine("Press an alias");
                string? Alias = Console.ReadLine() ?? null;
                if (Alias == "") { Alias = null; }
                Console.WriteLine("Press a description");
                string? Description = Console.ReadLine();
                if (Description == "") { Description = null; }
                Console.WriteLine("Press a deliverable");
                string? Deliverables = Console.ReadLine();
                if (Deliverables == "") { Deliverables = null; }
                Console.WriteLine("Press a remarks");
                string? Remarks = Console.ReadLine();
                if (Remarks == "") { Remarks = null; }
                Console.WriteLine("Press a schedualed Date");
                DateTime? SchedualedDate = DateTime.Parse(Console.ReadLine()!);
                Console.WriteLine("Press a deadline Date");
                DateTime? DeadlineDate = DateTime.Parse(Console.ReadLine()!);

                Console.WriteLine("Press a start Date");
                DateTime? StartDate = DateTime.Parse(Console.ReadLine()!);
                Console.WriteLine("Press a complete Date");
                DateTime? CompleteDate = DateTime.Parse(Console.ReadLine()!);
                Console.WriteLine("Press a required Effort Time");
                TimeSpan? RequiredEffortTime = TimeSpan.Parse(Console.ReadLine()!);
                Console.WriteLine("press the forecast Date");
                DateTime? ForecastDate = DateTime.Parse(Console.ReadLine()!);
                Console.WriteLine("Press if it active true or false");
                bool IsActive = bool.Parse(Console.ReadLine()!);
                Console.WriteLine("Press if there is a milestone true or false");
                bool IsMileStone = bool.Parse(Console.ReadLine()!);

                Console.WriteLine("press for level:  \n 0 for Beginner \n 1 for AdvancedBeginner \n 2 for Intermediate \n 3 for Advanced \n 4 for Expert");
                BO.Engineerlevel? Level = (BO.Engineerlevel)int.Parse(Console.ReadLine());
                Console.WriteLine("press details of engineer");
                Console.WriteLine("press the id");
                int EngineerId = int.Parse(Console.ReadLine());
                Console.WriteLine("press the name");
                string Name = Console.ReadLine();
                BO.EngineerInTask? EngineerInTask = new BO.EngineerInTask() { id = EngineerId, name = Name };
                Console.WriteLine("press the number of dependencies");
                int AmountForDependencies = int.Parse(Console.ReadLine());
                for (int i = 0; i < AmountForDependencies; i++)
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
                    BO.TaskInList taskInList = new BO.TaskInList { id = dId, alias = dAlias, description = dDescription, status = dStatus };
                    taskList.Add(taskInList);
                }
                BO.Task t;
                if (num == 2)
                {
                    BO.Task? temp = s_bl.Task.Read(Id);
                    DateTime? createdAtDate2;
                    BO.Status? status2;
                    Console.WriteLine("Press a created At Date");
                    createdAtDate2 = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("press for stutus: \n0 for Unscheduled \n 1 for Scheduled \n 2 for OnTrack \n 3 for Done");
                    status2 = (BO.Status)int.Parse(Console.ReadLine());
                    t = new BO.Task { id = Id, alias = Alias ?? temp!.alias, isMilestone = IsMileStone, description = Description ?? temp!.description, status = status2 ?? temp!.status, dependencies = taskList ?? temp!.dependencies, createdAtDate = createdAtDate2 ?? temp!.createdAtDate, schedualedDate = SchedualedDate ?? temp!.schedualedDate, startDate = StartDate ?? temp!.startDate, forecastDate = ForecastDate ?? temp!.forecastDate, deadlineDate = DeadlineDate ?? temp!.deadlineDate, completeDate = CompleteDate ?? CompleteDate, requiredEffortTime = RequiredEffortTime ?? temp!.requiredEffortTime, deliverables = Deliverables ?? temp!.deliverables, remarks = Remarks ?? temp!.remarks, engineer = EngineerInTask ?? temp!.engineer, coplexity = Level ?? temp!.coplexity, isActive = IsActive };
                    //t.id
                    t.alias = Alias ?? temp.alias;
                    t.isMilestone = IsMileStone;
                    t.description = Description ?? temp.description;
                    t.status = status2 ?? temp.status;
                }
                else
                {
                    DateTime createdAtDate1;
                    BO.Status status1;
                    Console.WriteLine("Press a created At Date");
                    createdAtDate1 = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("press for stutus: \n0 for Unscheduled \n 1 for Scheduled \n 2 for OnTrack \n 3 for Done");
                    status1 = (BO.Status)int.Parse(Console.ReadLine());
                    t = new BO.Task() { id = Id, alias = Alias, isMilestone = IsMileStone, description = Description, status = status1, dependencies = taskList, createdAtDate = createdAtDate1, schedualedDate = SchedualedDate, startDate = StartDate, forecastDate = ForecastDate, deadlineDate = DeadlineDate, completeDate = CompleteDate, requiredEffortTime = RequiredEffortTime, deliverables = Deliverables, remarks = Remarks, engineer = EngineerInTask, coplexity = Level, isActive = IsActive };
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

        }//לא עשינו רק כאן TRY PARSE


        public static void inputForEngineer(int num, int Id = 0)
        {
            BO.Engineer engineer;
            try
            {
                if (num == 2)
                {
                    BO.Engineer? temp = s_bl.Engineer.Read(Id);
                }

                Console.WriteLine("Enter the details:");
                Console.WriteLine("Press a name");
                string? Name = Console.ReadLine();
                if (Name == "") { Name = null; }

                Console.WriteLine("Press an email");
                string? Email = Console.ReadLine();
                if (Email == "") { Email = null; }

                Console.WriteLine("Press for level: \n 0 for Beginner \n 1 for AdvancedBeginner \n 2 for Intermediate \n 3 for Advanced \n 4 for Expert");

                BO.Engineerlevel? Level;
                if (int.TryParse(Console.ReadLine()!, out int levelValue))
                {
                    Level = (BO.Engineerlevel)levelValue;
                }
                else
                {
                    Console.WriteLine("Invalid level selection. Please enter a number between 0 and 4.");
                    Level = null; // Set Level to null if parsing fails
                }

                Console.WriteLine("Press how much he paid");

                double? Cost;
                if (double.TryParse(Console.ReadLine()!, out double costValue))
                {
                    Cost = costValue;
                }
                else
                {
                    Console.WriteLine("Invalid cost format. Please enter a number.");
                    Cost = null; // Set Cost to null if parsing fails
                }

                Console.WriteLine("Press if it active true or false");
                bool IsActive = bool.Parse(Console.ReadLine()!);

                Console.WriteLine("Press the details of the task");
                Console.WriteLine("Press the id");

                int idTask;
                if (int.TryParse(Console.ReadLine()!, out int idTaskValue))
                {
                    idTask = idTaskValue;
                }
                else
                {
                    Console.WriteLine("Invalid task ID format. Please enter an integer.");
                    idTask = 0; // Set idTask to a default value if parsing fails
                }

                Console.WriteLine("Press the alias");
                string Alias = Console.ReadLine()!;

                BO.TaskInEngineer? taskInEngineer = new BO.TaskInEngineer() { id = idTask,alias=Alias };

                if (num == 2)
                {
                    BO.Engineer? temp = s_bl.Engineer.Read(Id);
                    engineer = new BO.Engineer()
                    {
                        id = Id,
                        name = Name ?? temp!.name,
                        email = Email ?? temp!.email,
                        level = Level ?? temp!.level,
                        cost = Cost ?? temp!.cost,
                        isActive = IsActive,
                        task = taskInEngineer ?? temp!.task
                    };
                }
                else
                {
                    engineer = new BO.Engineer()
                    {
                        id = Id,
                        name = Name,
                        email = Email,
                        level = Level,
                        cost = Cost,
                        isActive = IsActive,
                        task = taskInEngineer
                    };
                }

                if (num == 1)
                {
                    s_bl.Engineer!.Create(engineer);
                }
                else
                {
                    s_bl.Engineer!.Update(engineer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// A method that prints the details of the task
        /// </summary>
        /// <param name="engineer"> The resulting task for printing </param>
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

        /// <summary>
        /// The actions you can perform for a task
        /// </summary>
        /// <exception cref="DalDoesNotExistException"></exception>
        //public static void task()
        //{
        //    Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
        //    options choice = (options)Enum.Parse(typeof(options), Console.ReadLine()!);
        //    try
        //    {
        //        while (choice != 0)
        //        {
        //            int id;
        //            switch (choice)
        //            {
        //                case options.CREATE:
        //                    inputForTask(1);
        //                    break;
        //                case options.READ:
        //                    Console.WriteLine("Press an id");
        //                    id = int.Parse(Console.ReadLine()!);
        //                    BO.Task? task = s_bl.Task!.Read(id);
        //                    if (task != null)
        //                    {
        //                        printTask(task);
        //                    }
        //                    else
        //                    {
        //                        try
        //                        {
        //                            throw new DalDoesNotExistException($"task with ID={id} not exists");
        //                        }
        //                        catch
        //                        (Exception e)
        //                        { Console.WriteLine(e.Message); }
        //                    }
        //                    break;
        //                case options.READALL:
        //                    IEnumerable<BO.TaskInList>? tasks = s_bl.Task!.ReadAll();
        //                    foreach (BO.TaskInList ta in tasks)
        //                    {
        //                        PrintTaskInList(ta);
        //                    }
        //                    break;
        //                case options.DELETE:
        //                    Console.WriteLine("Press an id");
        //                    id = int.Parse(Console.ReadLine()!);
        //                    s_bl.Task.Delete(id);
        //                    break;
        //                case options.UPDATE:
        //                    Console.WriteLine("Press an id");
        //                    id = int.Parse(Console.ReadLine());
        //                    inputForTask(2, id);
        //                    break;
        //                default:
        //                    Console.WriteLine("Wrong answer");
        //                    break;
        //            }
        //            Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
        //            choice = (options)Enum.Parse(typeof(options), Console.ReadLine()!);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //}
        public static void task()
        {
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");

            options choice;
            string choiceStr = Console.ReadLine()!;

            // Use TryParse to handle potential invalid input for choice
            if (Enum.TryParse<options>(choiceStr, out choice))
            {
                try
                {
                    while (choice != 0)
                    {
                        int id;

                        switch (choice)
                        {
                            case options.CREATE:
                                inputForTask(1);
                                break;
                            case options.READ:
                                Console.WriteLine("Press an id");

                                // Use TryParse to handle potential invalid input for id
                                if (int.TryParse(Console.ReadLine()!, out id))
                                {
                                    BO.Task? task = s_bl.Task!.Read(id);
                                    if (task != null)
                                    {
                                        printTask(task);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            throw new DalDoesNotExistException($"Task with ID={id} not exists");
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid ID format. Please enter an integer.");
                                }
                                break;
                            case options.READALL:
                                IEnumerable<BO.TaskInList>? tasks = s_bl.Task!.ReadAll();
                                foreach (BO.TaskInList ta in tasks)
                                {
                                    PrintTaskInList(ta);
                                }
                                break;
                            case options.DELETE:
                                Console.WriteLine("Press an id");

                                // Use TryParse to handle potential invalid input for id
                                if (int.TryParse(Console.ReadLine()!, out id))
                                {
                                    s_bl.Task.Delete(id);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid ID format. Please enter an integer.");
                                }
                                break;
                            case options.UPDATE:
                                Console.WriteLine("Press an id");

                                // Use TryParse to handle potential invalid input for id
                                if (int.TryParse(Console.ReadLine()!, out id))
                                {
                                    inputForTask(2, id);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid ID format. Please enter an integer.");
                                }
                                break;
                            default:
                                Console.WriteLine("Wrong answer");
                                break;
                        }

                        Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
                        choiceStr = Console.ReadLine()!;

                        // Use TryParse again for the next menu selection
                        if (Enum.TryParse<options>(choiceStr, out choice))
                        {
                            // Continue the loop
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection. Please enter a number between 0 and 5.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid menu selection. Please enter a number between 0 and 5.");
            }
        }

        /// <summary>
        /// A method that prints the details of the task
        /// </summary>
        /// <param name="task"> The resulting task for printing </param>
        public static void printTask(BO.Task task)
        {
            Console.WriteLine($"ID: {task.id}");
            Console.WriteLine($"Alias: {task.alias}");
            Console.WriteLine($"Description: {task.description}");
            Console.WriteLine($"Is Milestone: {task.isMilestone}");
            Console.WriteLine($"Status: {task.status}");
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
            Console.WriteLine("Dependencies:");
            foreach (BO.TaskInList tl in task.dependencies!)
            {
                Console.WriteLine(tl.id);
                Console.WriteLine(tl.alias);
            }
        }

        /// <summary>
        /// Printing details of a task in the list
        /// </summary>
        /// <param name="taskInList"> The resulting object </param>
        public static void PrintTaskInList(BO.TaskInList taskInList)
        {
            Console.WriteLine($"ID: {taskInList.id}");
            Console.WriteLine($"Description: {taskInList.description}");
            Console.WriteLine($"Alias: {taskInList.alias}");
            Console.WriteLine($"Status: {taskInList.status}");
        }

        /// <summary>
        /// The actions you can perform for a task
        /// </summary>
        /// <exception cref="DalDoesNotExistException"></exception>
        public static void engineer()
        {
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
            options choice = (options)Enum.Parse(typeof(options), Console.ReadLine()!);
            try
            {
                while (choice != 0)
                {
                    int id;
                    switch (choice)
                    {
                        case options.CREATE:
                            Console.WriteLine("Press an id");
                            //id = int.Parse(Console.ReadLine()!);
                            //inputForEngineer(1, id);
                            //break;
                            if (int.TryParse(Console.ReadLine()!, out id))
                            {
                                inputForEngineer(1, id);
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID format. Please enter an integer.");
                            }
                            break;
                        case options.READ:
                        //Console.WriteLine("Press an id");
                        //id = int.Parse(Console.ReadLine()!);
                        //BO.Engineer? engineer = s_bl.Engineer!.Read(id);
                        //if (engineer != null)
                        //{
                        //    printEngineer(engineer);
                        //}
                        //else
                        //{
                        //    try
                        //    {
                        //        throw new DalDoesNotExistException($"task with ID={id} not exists");
                        //    }
                        //    catch
                        //    (Exception e)
                        //    { Console.WriteLine(e.Message); }
                        //}
                        //break;
                            Console.WriteLine("Press an id");

                            // Use TryParse to handle potential invalid input for id
                            if (int.TryParse(Console.ReadLine()!, out id))
                            {
                                BO.Engineer? engineer = s_bl.Engineer!.Read(id);
                                if (engineer != null)
                                {
                                    printEngineer(engineer);
                                }
                                else
                                {
                                    try
                                    {
                                        throw new DalDoesNotExistException($"Engineer with ID={id} not exists");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID format. Please enter an integer.");
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
                            //Console.WriteLine("Press an id");
                            //id = int.Parse(Console.ReadLine()!);
                            //s_bl.Engineer.Delete(id);
                            //break;
                            Console.WriteLine("Press an id");

                            // Use TryParse to handle potential invalid input for id
                            if (int.TryParse(Console.ReadLine()!, out id))
                            {
                                s_bl.Engineer.Delete(id);
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID format. Please enter an integer.");
                            }
                            break;
                        case options.UPDATE:
                            //Console.WriteLine("Press an id");
                            //id = int.Parse(Console.ReadLine()!);
                            //inputForEngineer(2, id);
                            //break;
                            Console.WriteLine("Press an id");

                            // Use TryParse to handle potential invalid input for id
                            if (int.TryParse(Console.ReadLine()!, out id))
                            {
                                inputForEngineer(2, id);
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID format. Please enter an integer.");
                            }
                            break;
                        default:
                            Console.WriteLine("Wrong answer");
                            break;
                    }
                    Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n5 for Update");
                    choice = (options)Enum.Parse(typeof(options), Console.ReadLine()!);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        enum menu { EXIT, TASK, ENGINEER, START};
        static void Main(string[] args)
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer\n3 for select date to start the project");
            menu choice = (menu)Enum.Parse(typeof(menu), Console.ReadLine()!);
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
                    case menu.START:
                        Console.WriteLine("Press the start date");
                        DateTime start = DateTime.Parse(Console.ReadLine()!);
                        s_bl.Clock.SetStartOfProject(start);
                        Console.WriteLine($"The project wiil start on: {s_bl.Clock.GetStartOfProject()}");
                        break;
                    default:
                        Console.WriteLine("Wrong answer");
                        break;

                }
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer");
                choice = (menu)Enum.Parse(typeof(menu), Console.ReadLine()!);
            }
            
        }//גם כאן לא עשינו TRY
    }
}