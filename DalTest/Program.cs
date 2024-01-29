
using Dal;
using DalApi;
using DO;
using System.Data.SqlTypes;
using System.Numerics;
using System.Transactions;

namespace DalTest
{
    internal class Program
    {
        static readonly IDal s_dal = Factory.Get;
      // static readonly IDal s_dal = new DalList();
        //static readonly IDal s_dal = new DalXml();

        /// <summary>
        /// Receiving data and sending it to a method that creates a new task or update the task
        /// </summary>
        /// <param name="num"> The recipient's number to know whether to create or update </param>
        public static void creatTask(int num, int id = 0)
        {
            try
            {
                DO.Task? temp = s_dal.Task.Read(id);
                string? alias, description, deliverables, remarks;
                DateTime? schedualedDate, deadlineDate, createdAtDate, startDate, completeDate;
                TimeSpan? requiredEffortTime;
                DO.Engineerlevel level;
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
                    if(deadlineDate == null)
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
                DO.Task t = new DO.Task(id, alias, description, isMileStone, schedualedDate, requiredEffortTime, deadlineDate, createdAtDate, startDate, completeDate, deliverables, remarks, 655498745, DO.Engineerlevel.Advanced, isActive);
                if (num == 1)
                    s_dal.Task!.Create(t);
                else
                    s_dal.Task!.Update(t);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Receiving data and sending it to a method that creates a new engineerr or to update the engineer
        /// </summary>
        /// <param name="num"> The recipient's number to know whether to create or update </param>
        public static void creatEngineer(int num)
        {
            DO.Engineer engineer;
            try
            {
                Console.WriteLine("Enter the details:");
                Console.WriteLine("Press a id");
                int id = int.Parse(Console.ReadLine());
                Engineer? temp = s_dal.Engineer.Read(id);
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
                engineer = new DO.Engineer(id, name, email, DO.Engineerlevel.Expert, cost, isActive);
                if (num == 1)
                    s_dal.Engineer!.Create(engineer);
                else
                    s_dal.Engineer!.Update(engineer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Receiving data and sending it to a method that creates a new dependency or to update the dependency
        /// </summary>
        /// <param name="num"> The recipient's number to know whether to create or update </param>
        public static void creatDependency(int num, int id = 0)
        {
            try
            {
                Dependency temp = s_dal.Dependency.Read(id)!;
                Console.WriteLine("Enter the details");
                Console.WriteLine("Press which task is dependent");
                int? dependentTask = int.Parse(Console.ReadLine());
                Console.WriteLine("Press on which task");
                int? dependsOnTask = int.Parse(Console.ReadLine());
                Console.WriteLine("Press if it active true or false");
                bool isActive = bool.Parse(Console.ReadLine());
                if (num == 2) 
                {
                    if (dependentTask == null)
                    {
                        dependentTask = temp.dependentTask;
                    }
                    if (dependsOnTask == null)
                    {
                        dependsOnTask = temp.dependsOnTask;
                    }
                }
                DO.Dependency d = new DO.Dependency(id, dependentTask, dependsOnTask, isActive);
                if (num == 1)
                    s_dal.Dependency!.Create(d);
                else
                    s_dal.Dependency!.Update(d);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        }
        enum mainMenu { Exit, Task, Engineer, Dependency, Initialization };
        enum options { Exit, Create, Read, ReadAll, Update, Delete };
        static void Main(string[] args)
        {
            try
            {
                //Initialization.Do(s_dal);
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer\n3 for Dependency\n4 for initialization");
                mainMenu choice = (mainMenu)Enum.Parse(typeof(mainMenu), Console.ReadLine());
                while (choice != mainMenu.Exit)
                {

                    switch (choice)
                    {
                        case mainMenu.Task:
                            try
                            {
                                // Choosing which option to access each entity
                                Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                options taskOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                                while (taskOption != options.Exit)
                                {
                                    switch (taskOption)
                                    {

                                        // Receiving data and sending it to a method that creates a new task
                                        case options.Create:
                                            creatTask(1);
                                            break;

                                        // Inserting an ID and printing the task if it exists and is active
                                        case options.Read:

                                            Console.WriteLine("Press a Id");
                                            int id = int.Parse(Console.ReadLine());
                                            DO.Task? task = s_dal.Task!.Read(id);
                                            if (task != null)
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
                                            else
                                            {
                                                try
                                                {
                                                    throw new DalDoesNotExistException($"task with ID={id} not exists");
                                                }
                                                catch
                                                (Exception e)
                                                { Console.WriteLine(e.Message); }

                                            }

                                            break;

                                        // Print all tasks
                                        case options.ReadAll:
                                            IEnumerable<DO.Task>? tasks = s_dal.Task!.ReadAll();
                                            if (tasks != null)
                                            {
                                                foreach (DO.Task ta in tasks)
                                                {
                                                    Console.WriteLine($"ID: {ta.id}");
                                                    Console.WriteLine($"Alias: {ta.alias}");
                                                    Console.WriteLine($"Description: {ta.description}");
                                                    Console.WriteLine($"Is Milestone: {ta.isMilestone}");
                                                    Console.WriteLine($"Scheduled Date: {ta.schedualedDate}");
                                                    Console.WriteLine($"Required Effort Time: {ta.requiredEffortTime}");
                                                    Console.WriteLine($"Deadline Date: {ta.deadlineDate}");
                                                    Console.WriteLine($"Created At Date: {ta.createdAtDate}");
                                                    Console.WriteLine($"Start Date: {ta.startDate}");
                                                    Console.WriteLine($"Complete Date: {ta.completeDate}");
                                                    Console.WriteLine($"Deliverables: {ta.deliverables}");
                                                    Console.WriteLine($"Remarks: {ta.remarks}");
                                                    Console.WriteLine($"Engineer ID: {ta.ingineerId}");
                                                    Console.WriteLine($"Coplexity: {ta.coplexity}");
                                                    Console.WriteLine($"Is Active: {ta.isActive}");
                                                    Console.WriteLine();
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No tasks found.");
                                            }
                                            break;

                                        // Receiving data and updating the received task
                                        case options.Update:
                                            Console.WriteLine("press which task");
                                            int taskNum = int.Parse(Console.ReadLine());
                                            creatTask(2, taskNum);
                                            break;

                                        // Getting an ID and deleting the task
                                        case options.Delete:
                                            // Handle delete option
                                            Console.WriteLine("Press a id");
                                            id = int.Parse(Console.ReadLine());
                                            s_dal.Task!.Delete(id);
                                            break;

                                        default:
                                            Console.WriteLine("Wrong answer");
                                            break;
                                    }
                                    Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                    taskOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                                }

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;


                        case mainMenu.Engineer:
                            try
                            {
                                Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                options engineerOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                                while (engineerOption != options.Exit)
                                {
                                    switch (engineerOption)
                                    {
                                        // Receiving data and sending it to a method that creates a new engineer
                                        case options.Create:
                                            creatEngineer(1);
                                            break;

                                        // Inserting an ID and printing the engineer if it exists and is active
                                        case options.Read:
                                            // Handle read option for Engineer
                                            Console.WriteLine("Press a id");
                                            int id = int.Parse(Console.ReadLine());
                                            DO.Engineer eng = s_dal.Engineer!.Read(id);
                                            if (eng != null)
                                            {
                                                Console.WriteLine($"ID: {eng.id}");
                                                Console.WriteLine($"Name: {eng.name}");
                                                Console.WriteLine($"Email: {eng.email}");
                                                Console.WriteLine($"Level: {eng.level}");
                                                Console.WriteLine($"Cost: {eng.cost}");
                                                Console.WriteLine($"Active: {eng.isActive}");
                                                Console.WriteLine();
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    throw new DalDoesNotExistException($"engineer with ID={id} not exists");
                                                }
                                                catch
                                                (Exception e)
                                                { Console.WriteLine(e.Message); }

                                            }
                                            break;

                                        // Print all engineers
                                        case options.ReadAll:
                                            //List<DO.Engineer>? engineers = s_dal.Engineer!.ReadAll();
                                            IEnumerable<DO.Engineer>? engineers = s_dal.Engineer!.ReadAll();
                                            if (engineers != null)
                                            {
                                                foreach (DO.Engineer en in engineers)
                                                {
                                                    Console.WriteLine($"ID: {en.id}");
                                                    Console.WriteLine($"Name: {en.name}");
                                                    Console.WriteLine($"Email: {en.email}");
                                                    Console.WriteLine($"Level: {en.level}");
                                                    Console.WriteLine($"Cost: {en.cost}");
                                                    Console.WriteLine($"Active: {en.isActive}");
                                                    Console.WriteLine();
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No engineers found.");
                                            }
                                            break;

                                        // Receiving data and updating the received engineer
                                        case options.Update:
                                            creatEngineer(2);
                                            break;

                                        // Getting an ID and deleting the engineer
                                        case options.Delete:
                                            // Handle delete option for Engineer
                                            Console.WriteLine("Press a id");
                                            id = int.Parse(Console.ReadLine());
                                            s_dal.Engineer!.Delete(id);
                                            break;

                                        default:
                                            Console.WriteLine("Wrong answer");
                                            break;

                                    }
                                    Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                    engineerOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                                }


                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;


                        case mainMenu.Dependency:
                            try
                            {
                                Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                options dependencyOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                                while (dependencyOption != options.Exit)
                                {
                                    switch (dependencyOption)
                                    {
                                        // Receiving data and sending it to a method that creates a new dependency
                                        case options.Create:
                                            creatDependency(1);
                                            break;

                                        // Inserting an ID and printing the dependency if it exists and is active
                                        case options.Read:
                                            Console.WriteLine("Press a id");
                                            int id = int.Parse(Console.ReadLine());
                                            DO.Dependency dep = s_dal.Dependency!.Read(id);
                                            if (dep != null)
                                            {
                                                Console.WriteLine($"ID: {dep.id}");
                                                Console.WriteLine($"Depentent task: {dep.dependentTask}");
                                                Console.WriteLine($"Depends on task: {dep.dependsOnTask}");
                                                Console.WriteLine($"Active: {dep.isActive}");
                                                Console.WriteLine();
                                            }

                                            else
                                            {
                                                try
                                                {
                                                    throw new DalDoesNotExistException($"dependency with ID={id} not exists");
                                                }
                                                catch
                                                (Exception e)
                                                { Console.WriteLine(e.Message); }

                                            }
                                            break;
                                            break;

                                        // Print all dependencies
                                        case options.ReadAll:
                                            //List<DO.Dependency>? dependencies = s_dal.dependency!.ReadAll();
                                            IEnumerable<DO.Dependency>? dependencies = s_dal.Dependency!.ReadAll();
                                            if (dependencies != null)
                                            {
                                                foreach (DO.Dependency de in dependencies)
                                                {
                                                    Console.WriteLine($"ID: {de.id}");
                                                    Console.WriteLine($"Depentent task: {de.dependentTask}");
                                                    Console.WriteLine($"Depends on task: {de.dependsOnTask}");
                                                    Console.WriteLine($"Active: {de.isActive}");
                                                    Console.WriteLine();
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No depedency found.");
                                            }
                                            break;

                                        // Receiving data and updating the received dependency
                                        case options.Update:
                                            Console.WriteLine("press which dependency");
                                            int dependencyNum = int.Parse(Console.ReadLine());

                                            creatDependency(2, dependencyNum);
                                            break;

                                        // Getting an ID and deleting the dependency
                                        case options.Delete:
                                            // Handle delete option for Dependency
                                            Console.WriteLine("Press a id");
                                            id = int.Parse(Console.ReadLine());
                                            s_dal.Dependency!.Delete(id);
                                            break;

                                        default:
                                            Console.WriteLine("Wrong answer");
                                            break;
                                    }
                                    Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                    dependencyOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;

                        case mainMenu.Initialization:
                            // Verifies with the user that he really wants to initialize the data and initializes if he answered yes
                            Console.Write("Would you like to create Initial data? (Y/N)");
                            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
                            if (ans == "Y")
                            {

                                Initialization.Do();
                            }
                            break;


                                default:
                            Console.WriteLine("Wrong answer");
                            break;
                    }
                    Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer\n3 for Dependency\n4 for initialization");
                    choice = (mainMenu)Enum.Parse(typeof(mainMenu), Console.ReadLine());
                }



            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}



