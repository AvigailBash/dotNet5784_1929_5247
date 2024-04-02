using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    //internal class EngineerConverter
    //{
    //}
    public class EngineerConverter : IMultiValueConverter
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                return null;
            }

            string name = (string)values[0];
            string id = (string)values[1];

            return string.Format("{0} ({1})", name, id);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is string formattedString)
            {
                string[] parts = formattedString.Split('(', ')');
                if (parts.Length == 2)
                {
                    return new object[] { parts[0], parts[1] };
                }
            }

            return null;
        }
    }

}
