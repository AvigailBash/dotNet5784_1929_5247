using DalApi;
using System.Xml.Linq;

namespace Dal;

internal class ClockImplementation : IClock
{
    private readonly string s_clock_xml = "data-config";

    /// <summary>
    /// Getting the project קמג date by extracting it from an XML file
    /// </summary>
    /// <returns></returns>
    public DateTime? GetEndOfProject()
    {
        XElement clockRoot = XMLTools.LoadListFromXMLElement(s_clock_xml).Element("endProject")!;
        if(clockRoot.Value == "")
        {
            return null;
        }
        return DateTime.Parse(clockRoot.Value);
    }

    /// <summary>
    /// Getting the project start date by extracting it from an XML file
    /// </summary>
    /// <returns></returns>
    public DateTime GetStartOfProject()
    {
        XElement clockRoot = XMLTools.LoadListFromXMLElement(s_clock_xml).Element("startProject")!;
        if (clockRoot.Value == "")
        {
           return DateTime.Today;
        }
        return DateTime.Parse(clockRoot.Value);
    }

    /// <summary>
    /// Change the project end date by extracting from the XML file, changing and saving back to the file
    /// </summary>
    /// <param name="endOfProject"> Change the project start date by extracting from the XML file, changing and saving back to the file </param>
    /// <returns></returns>
    public DateTime? SetEndOfProject(DateTime endOfProject)
    {
        XElement clockRoot = XMLTools.LoadListFromXMLElement(s_clock_xml);
        clockRoot.Element("endProject")!.Value = endOfProject.ToString();
        XMLTools.SaveListToXMLElement(clockRoot, s_clock_xml);
        return endOfProject;
    }

    /// <summary>
    /// Change the project start date by extracting from the XML file, changing and saving back to the file
    /// </summary>
    /// <param name="startOfProject"> Change the project start date by extracting from the XML file, changing and saving back to the file </param>
    /// <returns></returns>
    public DateTime? SetStartOfProject(DateTime startOfProject)
    {
        XElement clockRoot = XMLTools.LoadListFromXMLElement(s_clock_xml);
        clockRoot.Element("startProject")!.Value = startOfProject.ToString();
        XMLTools.SaveListToXMLElement(clockRoot, s_clock_xml);
        return startOfProject;
    }
}
