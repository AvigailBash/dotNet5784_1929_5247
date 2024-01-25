using BlApi;

namespace BlImplementation;

internal class TaskInEngineerImplementation: ITaskInEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
}
