using DalApi;
using DO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation: IEngineer
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer item)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        //Engineer en = engineers.Find(item);
        bool engineerExists = engineers.Any(e => e.id == item.id);
        if(engineerExists)
        {
            throw new DalAlreadyExistsException($"Student with ID={item.id} already exists");
        }
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, s_engineers_xml);
        return item.id;
    }

    public void Delete(int id)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineer? en = Read(id);
        if (en == null) 
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} not exists");
        }
        engineers.Remove(en);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return engineers.FirstOrDefault(filter);
    }

    public Engineer? Read(int id)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return engineers.FirstOrDefault(it => it.id == id && it.isActive == true);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        if (filter != null)
        {
            return from item in engineers where filter(item) select item;
        }
        return engineers.ToList();
    }

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
