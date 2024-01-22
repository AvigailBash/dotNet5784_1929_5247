using DalApi;
using DO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation: IEngineer
{
    readonly string s_engineers_xml = "engineers";

    /// <summary>
    /// Creates a new engineer and gives it an ID
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <returns></returns>
    public int Create(Engineer item)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        bool engineerExists = engineers.Any(e => e.id == item.id);
        if(engineerExists)
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.id} already exists");
        }
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, s_engineers_xml);
        return item.id;
    }

    /// <summary>
    /// Gets an ID and deletes the engineer
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineer? en = Read(id);
        if (en == null) 
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} not exists");
        }
        Engineer temp = new Engineer(en.id, en.name, en.email, en.level, en.cost, false);
        engineers.Remove(en);
        engineers.Add(temp);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
    }

    /// <summary>
    /// Gets an ID and prints the engineer if it exists and is active
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <returns></returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return engineers.FirstOrDefault(it => !it.isActive ? false : filter is null ? true : filter!(it));
    }

    /// <summary>
    /// Returns an object according to certain search conditions
    /// </summary>
    /// <param name="filter"> The search conditions on the object </param>
    /// <returns></returns>
    public Engineer? Read(int id)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return engineers.FirstOrDefault(it => it.id == id && it.isActive == true);
    }

    /// <summary>
    /// Returns a collection of objects according to a certain search condition
    /// </summary>
    /// <param name="filter"> The search conditions on the objects </param>
    /// <returns></returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return from item in engineers where(!item.isActive ? false : filter is null ? true : filter(item) )select item;
    }

    //// <summary>
    /// Receives details of a engineer and updates it
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <exception cref="DalDoesNotExistException"> The exception being sent </exception>
    public void Update(Engineer item)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineer? en = Read(item.id);
        if (en == null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={item.id} not exists");
        }
        engineers.Remove(en);
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
    }
}
