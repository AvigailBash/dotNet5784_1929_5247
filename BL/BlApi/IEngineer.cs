
namespace BlApi;

public interface IEngineer
{
    /// <summary>
    /// A method that returns all engineers according to a certain number
    /// </summary>
    /// <param name="filter"> The filter by which to search </param>
    /// <returns></returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter=null);

    /// <summary>
    /// A method that returns an engineer by ID
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    /// <returns></returns>
    public BO.Engineer? Read(int id);

    /// <summary>
    /// A method that creates a new engineer
    /// </summary>
    /// <param name="engineer"> Receives a bone and creates it </param>
    /// <returns></returns>
    public int Create(BO.Engineer engineer);

    /// <summary>
    /// A method that deletes a certain engineer, by searching the Id 
    /// </summary>
    /// <param name="id"> The engineer's identity card </param>
    public void Delete(int id);

    /// <summary>
    /// A method that updates an engineer
    /// </summary>
    /// <param name="engineer"> Getting an object to update </param>
    public void Update(BO.Engineer engineer);

    public BO.TaskInEngineer? ReadForPassword(int id, int password);
    public BO.EngineerInTask? ReadInTaskInEngineerFormat(int id);
    public IEnumerable<BO.EngineerInTask> ReadInTaskInEngineerFormatWithFilter(int id);


}
