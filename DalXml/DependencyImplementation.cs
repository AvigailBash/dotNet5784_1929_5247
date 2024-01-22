namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

/// <summary>
/// A class for implementing crud methods when working with XML files
/// </summary>
internal class DependencyImplementation : IDependency
{
    readonly string s_dependencies_xml = "dependencies";

    /// <summary>
    /// Creates a new dependency and gives it an ID
    /// </summary>
    /// <param name="item"> The resulting object </param>
    /// <returns></returns>
    public int Create(Dependency item)
    {
        XElement dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        int idAuto = Config.NextDependencyId;
        XElement dependencyElement = new XElement("dependency", new XElement("id", idAuto), new XElement("dependentTask", item.dependentTask), new XElement("dependsOnTask", item.dependsOnTask), new XElement("isActive", item.isActive));

        dependencyRoot.Add(dependencyElement);
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencies_xml);
        return idAuto;
    }

    /// <summary>
    /// Gets an ID and deletes the dependency
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        var dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        XElement dependencyElement;
        try
        {
            dependencyElement = (from p in dependencyRoot.Elements() where Convert.ToInt32(p.Element("id").Value) == id select p).FirstOrDefault();
            XElement temp = new("dependency", new XElement("id", id),
                new XElement("dependentTask", dependencyElement.Element("dependentTask").Value,
                new XElement("dependsOnTask", dependencyElement.Element("dependsOnTask").Value,
                new XElement("isActive"), false)));
            dependencyElement.Remove();
            dependencyRoot.Add(temp);
            XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencies_xml);
        }
        catch
        {
            throw new DalXMLFileLoadCreateException($"Dependency with ID={id} not exists");
        }

    }

    ///// <summary>
    ///// Prints a single dependency according to a certain filter
    ///// </summary>
    ///// <param name="filter"> By what to search for the object </param>
    ///// <returns></returns>
    //public Dependency? Read(Func<Dependency, bool> filter)
    //{
    //    return XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements().Select(d => getDependency(d)).FirstOrDefault(filter);
    //}

    ///// <summary>
    ///// Gets an ID and prints the dependency if it exists and is active
    ///// </summary>
    ///// <param name="id"> The ID of the received object </param>
    ///// <returns></returns>
    //public Dependency? Read(int id)
    //{
    //    //return XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements().Select(d=> getDependency(d)).FirstOrDefault();
    //    XElement? dependencyElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements().FirstOrDefault(d => (int?)d.Element("id") == id);
    //    return dependencyElement is null ? null : getDependency(dependencyElement);
    //}


    /// <summary>
    /// Prints a single dependency according to a certain filter
    /// </summary>
    /// <param name="filter"> By what to search for the object </param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
   => ReadAll(filter).FirstOrDefault();

    /// <summary>
    /// Gets an ID and prints the dependency if it exists and is active
    /// </summary>
    /// <param name="id"> The ID of the received object </param>
    /// <returns></returns>
    public Dependency? Read(int id) =>
   Read(dependency => dependency.id == id);

    /// <summary>
    /// Returns a collection of objects according to a certain search condition
    /// </summary>
    /// <param name="filter"> The search conditions on the objects </param>
    /// <returns></returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
        => XMLTools.LoadListFromXMLElement(s_dependencies_xml).Elements()
        .Select(d => getDependency(d)).Where(dependency => !dependency.isActive ? false : filter is null ? true : filter!(dependency));


/// <summary>
/// Receives details of a dependency and updates it
/// </summary>
/// <param name="item"> The resulting object </param>
/// <exception cref="DalDoesNotExistException"> The exception being sent </exception>
public void Update(Dependency item)
{
    var dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
    XElement dependencyElement;
    try
        {
            dependencyElement = (from p in dependencyRoot.Elements() where Convert.ToInt32(p.Element("id").Value) == item.id select p).FirstOrDefault();
            XElement temp = dependencyToXelementConverter(item);
            dependencyElement!.Remove();
            dependencyRoot.Add(temp);
            XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencies_xml);
        }
        catch
    {
        throw new DalXMLFileLoadCreateException($"Dependency with ID={item.id} not exists");
    }
}

    private static XElement dependencyToXelementConverter(Dependency item)
    {
        return new("dependency", new XElement("id", item.id),
            new XElement("dependentTask", item.dependentTask,
            new XElement("dependsOnTask", item.dependsOnTask,
            new XElement("isActive"), item.isActive)));
    }

    static Dependency getDependency(XElement x)
=> new Dependency()
{
   id = x.ToIntNullable("id") ?? throw new FormatException("can't convert id"),
   dependentTask = x.ToIntNullable("dependentTask") ?? null,
   dependsOnTask = x.ToIntNullable("dependsOnTask") ?? null,
   isActive = (bool?)x.Element("isActive") ?? false
};

}
