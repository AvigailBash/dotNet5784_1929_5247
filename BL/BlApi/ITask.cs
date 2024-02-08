namespace BlApi;

public interface ITask
{
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.Task, bool>? filter = null);
    public BO.Task Read(int id);
    public int Create(BO.Task task);
    public void Update(BO.Task task);
    public void Delete(int id);
    public DateTime findForecastDate(DateTime? scheduale, DateTime? start, TimeSpan? require);
}
