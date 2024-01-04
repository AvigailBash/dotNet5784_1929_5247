
using Dal;
using DalApi;
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
                Console.WriteLine("Press a number:\n0 for Exit\n1 for Task\n2 for Engineer\n3 for Dependency");
                mainMenu choice = (mainMenu)Enum.Parse(typeof(mainMenu), Console.ReadLine());
                switch (choice)
                {
                    case mainMenu.Task:
                        Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                        options taskOption = (options)Enum.Parse(typeof(options), Console.ReadLine());
                        switch (taskOption)
                        {
                            case options.Create:
                                int id, ingineerId;
                                string name, alias, description, deliverables, remarks;
                                DateTime schedualedDate, deadlineDate, createdAtDate, startDate, completeDate;
                                TimeSpan requiredEffortTime;
                                bool isActive = true;
                                DO.Engineerlevel level;
                                Console.WriteLine("Enter the details:");
                                Console.WriteLine("Press an ID:");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Press a name");
                                name=Console.ReadLine();
                                Console.WriteLine("Press an alias");
                                alias = Console.ReadLine();
                                Console.WriteLine("Press a description");
                                description = Console.ReadLine();
                                Console.WriteLine("Press a deliverable");
                                deliverables = Console.ReadLine();
                                Console.WriteLine("Press a remarks");
                                remarks = Console.ReadLine();
                                Console.WriteLine("Press a schedualed Date");
                                schedualedDate=DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Press a deadline Date");
                                deadlineDate = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Press a created At Date");
                                createdAtDate=DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Press a start Date");
                                startDate=DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Press a complete Date");
                                completeDate=DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Press a required Effort Time");
                                requiredEffortTime=TimeSpan.Parse(Console.ReadLine());
                                break;

                            case options.Read:
                                // Handle read option
                                Console.WriteLine("Read option selected");
                                break;

                            case options.ReadAll:
                                // Handle readAll option
                                Console.WriteLine("ReadAll option selected");
                                break;

                            case options.Update:
                                // Handle update option
                                Console.WriteLine("Update option selected");
                                break;

                            case options.Delete:
                                // Handle delete option
                                Console.WriteLine("Delete option selected");
                                break;

                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        break;

                    case mainMenu.Engineer:
                        Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                        options engineerOption = (options)Enum.Parse(typeof(options), Console.ReadLine());

                        switch (engineerOption)
                        {
                            case options.Exit:
                                return;

                            case options.Create:
                                // Handle create option for Engineer
                                Console.WriteLine("Create option for Engineer selected");
                                break;

                            case options.Read:
                                // Handle read option for Engineer
                                Console.WriteLine("Read option for Engineer selected");
                                break;

                            case options.ReadAll:
                                // Handle readAll option for Engineer
                                Console.WriteLine("ReadAll option for Engineer selected");
                                break;

                            case options.Update:
                                // Handle update option for Engineer
                                Console.WriteLine("Update option for Engineer selected");
                                break;

                            case options.Delete:
                                // Handle delete option for Engineer
                                Console.WriteLine("Delete option for Engineer selected");
                                break;

                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        break;

                    case mainMenu.Dependency:
                        Console.WriteLine("Press a number:\n0 for Exit\n1 for create\n2 for read\n3 for readAll\n4 for update\n5 for delete");
                        options dependencyOption = (options)Enum.Parse(typeof(options), Console.ReadLine());

                        switch (dependencyOption)
                        {
                            case options.Exit:
                                return;

                            case options.Create:
                                // Handle create option for Dependency
                                Console.WriteLine("Create option for Dependency selected");
                                break;

                            case options.Read:
                                // Handle read option for Dependency
                                Console.WriteLine("Read option for Dependency selected");
                                break;

                            case options.ReadAll:
                                // Handle readAll option for Dependency
                                Console.WriteLine("ReadAll option for Dependency selected");
                                break;

                            case options.Update:
                                // Handle update option for Dependency
                                Console.WriteLine("Update option for Dependency selected");
                                break;

                            case options.Delete:
                                // Handle delete option for Dependency
                                Console.WriteLine("Delete option for Dependency selected");
                                break;

                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        break;



                    default:
                        break;
                }


            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }
}



