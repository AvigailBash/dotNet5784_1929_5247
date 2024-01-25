// See https://aka.ms/new-console-template for more information
using DalApi;

Console.WriteLine("Hello, World!");

namespace BlApi
{
    internal class program
    {
        public static void inputForTask(int num, int id = 0)
        {
            try
            {
                BO.Task? temp = s_bl.Task.Read(id);
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
                BO.Task t = new BO.Task(id, createdAtDate, alias, description);
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
        enum taskMenu { EXIT, CREATE, READ, READALL, DELETE, UPDATE};
        public static void task()
        {
            Console.WriteLine("Press a number:\n0 for Exit\n1 for Create\n2 for Read\n3 for ReadAll\n4 for Delete\n for Update");
            taskMenu choice = (taskMenu)Enum.Parse(typeof(taskMenu), Console.ReadLine());
            while(choice != 0)
            {
                switch(choice)
                {
                    case taskMenu.CREATE:

                }
            }
        }

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
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
                        enginerr();
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