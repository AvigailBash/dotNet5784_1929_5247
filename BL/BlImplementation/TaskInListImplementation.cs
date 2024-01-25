using BlApi;

namespace BlImplementation;

internal class TaskInListImplementation: ITaskInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
}
