using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;

namespace PL;

class DataGridRowBackGrounfConverter : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DataGridRow dataGridRow = value as DataGridRow;
        var data = dataGridRow.DataContext as DataRowView;
        var taskId = int.Parse(data.Row.ItemArray[0]!.ToString()!);
        var task = s_bl.Task.Read(taskId);
        //if (dataGridRow.)
        //{

        //}
        return task.completeDate is null ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Blue);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertIdToIsEnabled : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? true: false ;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
class ConvertTextToUserId : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return int.TryParse(value.ToString(), out int result);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}


class ConvertDaTteTimeToIsEnabled : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null ? false : true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}

class ConvertStatusToIsColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (((BO.Status)value) == BO.Status.Scheduled) { return Colors.BlueViolet; }
        if (((BO.Status)value) == BO.Status.Done) { return Colors.Red; }
        if (((BO.Status)value) == BO.Status.None) { return Colors.Green; }
        return Colors.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}





//public class CellColorConverter : IMultiValueConverter
//{
//    Color[] arrColors = { Colors.White, Colors.Red, Colors.Green, Colors.RoyalBlue, Colors.Orange, Colors.LightYellow };
//    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
//    public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//    {
//        if (values[0] is DataGridCell cell && values[1] is DataRowView rowView)
//        {
//            try
//            {
//                DataRow row = rowView.Row;
//                string columnName = (string)cell.Column.Header;
//                int columnIndex = cell.Column.DisplayIndex;

//                if (columnIndex < 2)
//                {
//                    return new SolidColorBrush(Colors.LightGoldenrodYellow);
//                }

//                DateTime currentDate = s_bl.clock;
//                DateTime day;
//                if (DateTime.TryParse(columnName, out day))
//                {
//                    int? taskId = row.Field<int>("Task Id");
//                    if (taskId.HasValue)
//                    {
//                        var task = s_bl.Task.Read(taskId.Value);
//                        if (day < task.schedualedDate || day > task.forecastDate)
//                        {
//                            cell.Foreground = new SolidColorBrush(arrColors[0]);
//                            return new SolidColorBrush(arrColors[0]);
//                        }
//                        else
//                        {
//                            //המשימה הסתיימה
//                            if (task.completeDate != null)
//                            {
//                                cell.Foreground = new SolidColorBrush(arrColors[2]);
//                                return new SolidColorBrush(arrColors[2]);
//                            }
//                            // המסימה לא הושלמה והיא באיחור
//                            if (currentDate > task.forecastDate)
//                            {
//                                cell.Foreground = new SolidColorBrush(arrColors[1]);
//                                return new SolidColorBrush(arrColors[1]);
//                            }
//                            //עוד לא התחילו את המשימה
//                            if (task.startDate == null)
//                            {
//                                //לא התחילו כי לא הגיע זמן ההתחלה 
//                                if (task.schedualedDate > currentDate)
//                                {
//                                    cell.Foreground = new SolidColorBrush(arrColors[3]);
//                                    return new SolidColorBrush(arrColors[3]);
//                                }
//                                //לא התחילו את המשימה ועבר המועד המתוכנן להתחלה 
//                                if (currentDate > task.schedualedDate)
//                                {
//                                    cell.Foreground = new SolidColorBrush(arrColors[4]);
//                                    return new SolidColorBrush(arrColors[4]);
//                                }
//                            }
//                        }

//                    }
//                }
//            }
//            catch (Exception)
//            {
//                return new SolidColorBrush(Colors.Black);
//            }

//        }
//        return new SolidColorBrush(Colors.Silver);
//    }

//    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}
public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status status = (BO.Status)value;
        switch (status)
        {
            case BO.Status.None:
                return Brushes.White;
            case BO.Status.Scheduled:
                return Brushes.LightBlue;
            case BO.Status.Unscheduled:
                return Brushes.LightGreen;
            case BO.Status.OnTrack:
                return Brushes.LightYellow;
            case BO.Status.Done:
                return Brushes.LightGray;
            default:
                return Brushes.White;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}