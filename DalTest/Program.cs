
using Dal;
using DalApi;
using DO;
using System.Numerics;
using System.Transactions;

namespace DalTest
{
    internal class Program
    {

        private static ITask? s_dalTask = new TaskImplementation();
        private static IEngineer? s_dalEngineer = new EngineerImplementation();
        private static IDependency? s_dalDependency = new DependencyImplementation();
        enum mainMenu { Exit, Task, Engineer, Dependency };
        enum options { Exit, Create, Read, ReadAll, Update, Delete };
        static void Main(string[] args)
        {
            try
            {

                Initialization.Do(s_dalTask, s_dalEngineer, s_dalDependency);
                // Options to choose which entity to access
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer\n3 for Dependency");
                mainMenu choice = (mainMenu)Enum.Parse(typeof(mainMenu), Console.ReadLine());
                while (choice != mainMenu.Exit) 
                {
                    switch (choice)
                    {
                        case mainMenu.Task:
                            // Choosing which option to access each entity
                            Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                            options taskOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                            while (taskOption != options.Exit) 
                            {
                                switch (taskOption)
                                {
                                    // Receiving data and sending it to a method that creates a new task
                                    case options.Create:
                                        string?  alias, description, deliverables, remarks;
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
                                        DO.Task t = new DO.Task(0, alias, description, isMileStone, schedualedDate, requiredEffortTime, deadlineDate, createdAtDate, startDate, completeDate, deliverables, remarks, 655498745, DO.Engineerlevel.Advanced, isActive);
                                        s_dalTask!.Create(t);
                                        break;

                                    // Inserting an ID and printing the task if it exists and is active
                                    case options.Read:

                                        Console.WriteLine("Press a Id");
                                        int id = int.Parse(Console.ReadLine());
                                        DO.Task? task = s_dalTask!.Read(id);
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
                                        Console.WriteLine($"Engineer ID: {task.ingineerId}");
                                        Console.WriteLine($"Coplexity: {task.coplexity}");
                                        Console.WriteLine($"Is Active: {task.isActive}");
                                        Console.WriteLine();
                                        break;

                                    // Print all tasks
                                    case options.ReadAll:
                                        List<DO.Task>? tasks = s_dalTask!.ReadAll();
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
                                        // Handle update option
                                        Console.WriteLine("Enter the details:");
                                        Console.WriteLine("Press a id");
                                        id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter the details:");
                                        Console.WriteLine("Press an ID:");
                                        id = int.Parse(Console.ReadLine());
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
                                        isActive = bool.Parse(Console.ReadLine());
                                        Console.WriteLine("Press if there is a milestone true or false");
                                        isMileStone = bool.Parse(Console.ReadLine());
                                        DO.Task tas = new DO.Task(id, alias, description, isMileStone, schedualedDate, requiredEffortTime, deadlineDate, createdAtDate, startDate, completeDate, deliverables, remarks, 655498745, DO.Engineerlevel.Advanced ,isActive);
                                        s_dalTask!.Update(tas);
                                        break;

                                    // Getting an ID and deleting the task
                                    case options.Delete:
                                        // Handle delete option
                                        Console.WriteLine("Press a id");
                                        id = int.Parse(Console.ReadLine());
                                        s_dalTask!.Delete(id);
                                        break;

                                    default:
                                        Console.WriteLine("Wrong answer");
                                        break;
                                }
                                Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                taskOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                            }
                          
                           break;
                    
                        case mainMenu.Engineer:
                            Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                            options engineerOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                            while (engineerOption != options.Exit)
                            {
                                switch (engineerOption)
                                {
                                    // Receiving data and sending it to a method that creates a new engineer
                                    case options.Create:
                                        // Handle create option for Engineer
                                        Console.WriteLine("Enter the details:");
                                        Console.WriteLine("Press a id");
                                        int id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Press a name");
                                        string name = Console.ReadLine();
                                        Console.WriteLine("Press an email");
                                        string email = Console.ReadLine();
                                        Console.WriteLine("Press how much he paid");
                                        double cost = double.Parse(Console.ReadLine());
                                        Console.WriteLine("Press if it active true or false");
                                        bool isActive = bool.Parse(Console.ReadLine());
                                        DO.Engineer engineer = new DO.Engineer(id, name, email, DO.Engineerlevel.Expert, cost, isActive);
                                        s_dalEngineer!.Create(engineer);
                                        break;

                                    // Inserting an ID and printing the engineer if it exists and is active
                                    case options.Read:
                                        // Handle read option for Engineer
                                        Console.WriteLine("Press a id");
                                        id = int.Parse(Console.ReadLine());
                                        DO.Engineer eng = s_dalEngineer!.Read(id);
                                        Console.WriteLine($"ID: {eng.id}");
                                        Console.WriteLine($"Name: {eng.name}");
                                        Console.WriteLine($"Email: {eng.email}");
                                        Console.WriteLine($"Level: {eng.level}");
                                        Console.WriteLine($"Cost: {eng.cost}");
                                        Console.WriteLine($"Active: {eng.isActive}");
                                        Console.WriteLine();
                                        break;

                                    // Print all engineers
                                    case options.ReadAll:
                                        List<DO.Engineer>? engineers = s_dalEngineer!.ReadAll();
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
                                        // Handle update option for Engineer
                                        Console.WriteLine("Enter the details:");
                                        Console.WriteLine("Press a id");
                                        id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Press a name");
                                        name = Console.ReadLine();
                                        Console.WriteLine("Press an email");
                                        email = Console.ReadLine();
                                        Console.WriteLine("Press how much he paid");
                                        cost = double.Parse(Console.ReadLine());
                                        Console.WriteLine("Press if it active true or false");
                                        isActive = bool.Parse(Console.ReadLine());
                                        DO.Engineer e = new DO.Engineer(id, name, email, DO.Engineerlevel.Expert, cost, isActive);
                                        s_dalEngineer!.Update(e);
                                        break;

                                    // Getting an ID and deleting the engineer
                                    case options.Delete:
                                        // Handle delete option for Engineer
                                        Console.WriteLine("Press a id");
                                        id = int.Parse(Console.ReadLine());
                                        s_dalEngineer!.Delete(id);
                                        break;

                                    default: 
                                        Console.WriteLine("Wrong answer");
                                        break;
                                        
                                }
                                Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                engineerOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                            }
                            
                            break;

                        case mainMenu.Dependency:
                            Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                            options dependencyOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                            while (dependencyOption != options.Exit)
                            {
                                switch (dependencyOption)
                                {
                                    // Receiving data and sending it to a method that creates a new dependency
                                    case options.Create:
                                        // Handle create option for Dependency
                                        Console.WriteLine("Enter the details");
                                        Console.WriteLine("Press which task is dependent");
                                        int dependentTask = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Press on which task");
                                        int dependsOnTask = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Press if it active true or false");
                                        bool isActive = bool.Parse(Console.ReadLine());
                                        DO.Dependency d = new DO.Dependency(0, dependentTask, dependsOnTask, isActive);
                                        s_dalDependency!.Create(d);
                                        break;

                                    // Inserting an ID and printing the dependency if it exists and is active
                                    case options.Read:
                                        Console.WriteLine("Press a id");
                                        int id = int.Parse(Console.ReadLine());
                                        DO.Dependency dep = s_dalDependency!.Read(id);
                                        Console.WriteLine($"ID: {dep.id}");
                                        Console.WriteLine($"Depentent task: {dep.dependentTask}");
                                        Console.WriteLine($"Depends on task: {dep.dependsOnTask}");
                                        Console.WriteLine($"Active: {dep.isActive}");
                                        Console.WriteLine();
                                        break;

                                    // Print all dependencies
                                    case options.ReadAll:
                                        List<DO.Dependency>? dependencies = s_dalDependency!.ReadAll();
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
                                        // Handle update option for Dependency
                                        Console.WriteLine("Enter the details");
                                        Console.WriteLine("Press the id");
                                        id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Press which task is dependent");
                                        dependentTask = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Press on which task");
                                        dependsOnTask = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Press if it active true or false");
                                        isActive = bool.Parse(Console.ReadLine());
                                        DO.Dependency dependency = new DO.Dependency(id, dependentTask, dependsOnTask, isActive);
                                        s_dalDependency!.Update(dependency);
                                        break;

                                    // Getting an ID and deleting the dependency
                                    case options.Delete:
                                        // Handle delete option for Dependency
                                        Console.WriteLine("Press a id");
                                        id = int.Parse(Console.ReadLine());
                                        s_dalDependency!.Delete(id);
                                        break;

                                    default:
                                        Console.WriteLine("Wrong answer");
                                        break;
                                }
                                Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                                dependencyOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                            }
                           
                            break;



                        default:
                            Console.WriteLine("Wrong answer");
                            break;
                    }
                    Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer\n3 for Dependency");
                    choice = (mainMenu)Enum.Parse(typeof(mainMenu), Console.ReadLine());
                }
                


            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }
}



