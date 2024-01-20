namespace Dal;
using DalApi;
using DO;
using System.Linq.Expressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class DependencyImplementation: IDependency
{
    readonly string s_dependencies_xml = "dependencies";

    public int Create(Dependency item)
    {
        XElement dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        int idAuto = Config.NextDependencyId;
        //XElement id = new XElement("id", idAuto);
        //XElement dependentTask = new XElement("dependentTask", item.dependentTask);
        //XElement dependsOnTask = new XElement("dependsOnTask", item.dependsOnTask);
        //XElement isActive = new XElement("isActive", item.isActive);
        XElement dependencyElement = new XElement("dependency", new XElement("id", idAuto), new XElement("dependentTask", item.dependentTask), new XElement("dependsOnTask", item.dependsOnTask), new XElement("isActive", item.isActive));

        dependencyRoot.Add(dependencyElement);
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencies_xml);
        return idAuto;
    }

    public void Delete(int id)
    {
        var dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        XElement dependencyElement;
        try
        {
            dependencyElement = (from p in dependencyRoot.Elements()where Convert.ToInt32(p.Element("id").Value) == id select p).FirstOrDefault();
            dependencyElement?.Remove();
            XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencies_xml);
        }
        catch
        {
            throw new DalXMLFileLoadCreateException($"Dependency with ID={id} not exists");
        }

    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements().Select(d=> getDependency(d)).FirstOrDefault(filter);
    }

    public Dependency? Read(int id)
    {
        //return XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements().Select(d=> getDependency(d)).FirstOrDefault();
        XElement? dependencyElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements().FirstOrDefault(d => (int?)d.Element("id") == id);
        return dependencyElement is null ? null : getDependency(dependencyElement);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter == null) 
        {
            return XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements().Select(d => getDependency(d));
        }
        else
        {
            return XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements().Select(d => getDependency(d)).Where(filter);
        }
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }

    static Dependency getDependency(XElement x)
    {
        return new Dependency()
        {
            id = x.ToIntNullable("id") ?? throw new FormatException("can't convert id"),
            dependentTask = x.ToIntNullable("dependentTask") ?? null,
            dependsOnTask = x.ToIntNullable("dependsOnTask") ?? null,
            isActive = (bool?)x.Element("isActive") ?? false

        };
    }
}
