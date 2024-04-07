using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;
using System.Windows.Media.TextFormatting;
using System.Windows;

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
        return (int)value == 0 ? true : false;
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
                return Brushes.LightGray;
            case BO.Status.Scheduled:
                return "#468982";
            case BO.Status.Unscheduled:
                return Brushes.LightGreen;
            case BO.Status.OnTrack:
                return Brushes.LightYellow;
            case BO.Status.Done:
                return "#EFB71A";
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

public class DateTimeToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // בדיקה האם הערך המתקבל הוא תאריך ריק
        if (value == null || (DateTime)value == DateTime.MinValue)
        {
            return Visibility.Collapsed;
        }
        else
        {
            return Visibility.Visible;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // המרת חזרה אינה נדרשת, נחזיר את הערך המקורי
        return value;
    }
}



public class ProjectStatusToButtonIsEnabledConverter : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (s_bl.Clock.statusForProject()==BO.StatusOfProject.Start)
        {
            return true;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}