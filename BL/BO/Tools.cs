using System.Collections;
using System.Reflection;

namespace BO;

static class Tools
{
    /// <summary>
    /// A method that overrides the To string and prints in a different format
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static string ToStringProperty<T>(this T t, string suffix = "")
    {
        string str = "";
        foreach (PropertyInfo prop in t!.GetType().GetProperties())
        {
            var value = prop.GetValue(t, null);
            if (value is IEnumerable && value is not string)
            //אם קיבלנו אוסף צריך לעבור על כל עצם באוסף ולהפעיל עליו גם את הפונקציה הזאת
            {
                str += "\n" + prop.Name + ":";
                foreach (var item in (IEnumerable)value)
                    str += item.ToStringProperty("   ");
            }
            else
                str += "\n" + suffix + prop.Name + ": " + value;
        }
        str += "\n";
        return str;
    }
}
