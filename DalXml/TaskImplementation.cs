using DalApi;
using DO;

namespace Dal;

internal class TaskImplementation: ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(DO.Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        int nextId = Config.NextTaskId;
        DO.Task copy = item with { id = nextId };
        tasks.Add(copy);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        return nextId;
    }

    public void Delete(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        DO.Task? t = Read(id);
        if (t == null) 
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} not exists");
        }
        tasks.Remove(t);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        return tasks.FirstOrDefault(filter);
    }

    public DO.Task? Read(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        return tasks.FirstOrDefault(it => it.id == id);
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        if(filter!=null)
        {
            return from item in tasks where filter(item) select item;
        }
        return tasks.ToList();
    }

    public void Update(DO.Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        DO.Task? t = Read(item.id);
        if(t==null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={item.id} not exists");
        }
        tasks.Remove(t);
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }
}
