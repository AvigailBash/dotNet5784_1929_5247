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


public class StatusToColorConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status status = (BO.Status)value;

        switch (status)
        {
            case BO.Status.None:
                return Brushes.Pink;
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
public class TaskDelayedToRowBackgroundConverter : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DataGridRow dataGridRow = value as DataGridRow;
        var data = dataGridRow.DataContext as DataRowView;

        if (data != null)
        {
            var taskId = int.Parse(data.Row.ItemArray[0]!.ToString()!);
            var task = s_bl.Task.Read(taskId);

            return task.forecastDate < s_bl.clock && task.completeDate == null ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Transparent);
        }

        return new SolidColorBrush(Colors.Transparent);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class EngineerNameConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2)
            return "";

        // values[0] יהיה ה-ID של המהנדס
        // values[1] יהיה השם של המהנדס
        string engineerId = values[0]?.ToString();
        string engineerName = values[1]?.ToString();

        // יישום הלוגיקה שלך להחזרת הערך הנכון
        // לדוגמה, יכול להחזיר שם מהנדס עם זיהוי כמו כזה: "123 - John Doe"
        return $"{engineerId} - {engineerName}";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
