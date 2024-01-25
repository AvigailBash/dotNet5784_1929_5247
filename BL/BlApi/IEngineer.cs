
namespace BlApi;

public interface IEngineer
{
    public IEnumerable<IEngineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    public BO.Engineer Read(int id);
    public int Create(BO.Engineer engineer);
    public void Delete(int id);
    public void Update(BO.Engineer engineer);
}
