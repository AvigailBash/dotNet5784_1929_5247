using DalApi;
using DO;
using System.Reflection.Metadata.Ecma335;

namespace Dal;

internal class TaskImplementation: ITask
{
    readonly string s_tasks_xml = "tasks";

    /// <summary>
    /// Creates a new task and gives it an ID
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <returns></returns>
    public int Create(DO.Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        int nextId = Config.NextTaskId;
        DO.Task copy = item with { id = nextId };
        tasks.Add(copy);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        return nextId;
    }

    /// <summary>
    /// Gets an ID and deletes the task
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        DO.Task? t = Read(id);
        if (t == null) 
        {
            throw new DalDoesNotExistException($"Task with ID={id} not exists");
        }
        DO.Task temp = new(t.id, t.createdAtDate, t.alias, t.description, t.schedualedDate, t.requiredEffortTime, t.startDate, t.completeDate, t.deliverables, t.remarks, t.engineerId, t.coplexity, false);
        tasks.Remove(t);
        tasks.Add(temp);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }

    /// <summary>
    /// Returns an object according to certain search conditions
    /// </summary>
    /// <param name="filter"> The search conditions on the object </param>
    /// <returns></returns>
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        return tasks.FirstOrDefault(it => !it.isActive ? false : filter is null ? true : filter(it)) ?? null; /*throw new DalDoesNotExistException("Task not exists");*/
    }

    /// <summary>
    /// Gets an ID and prints the task if it exists and is active
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <returns></returns>
    public DO.Task? Read(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        return tasks.FirstOrDefault(it => it.id == id && it.isActive == true) ?? null;//throw new DalDoesNotExistException($"Task with ID={id} not exists");
    }

    /// <summary>
    /// Returns a collection of objects according to a certain search condition
    /// </summary>
    /// <param name="filter"> The search conditions on the objects </param>
    /// <returns></returns>
    public IEnumerable<DO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        return from item in tasks where (!item.isActive ? false : filter is null ? true : filter(item)) select item;
    }

    //// <summary>
    /// Receives details of a task and updates it
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <exception cref="DalDoesNotExistException"> The exception being sent </exception>
    public void Update(DO.Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        DO.Task? t = Read(item.id);
        if(t==null)
        {
            throw new DalDoesNotExistException($"Task with ID={item.id} not exists");
        }
        tasks.Remove(t);
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }
    /// <summary>
    /// A method for deleting all objects from a dependency entity
    /// </summary>
    public void deleteAll()
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        tasks.Clear();
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }
}
