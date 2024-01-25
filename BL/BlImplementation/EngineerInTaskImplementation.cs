using BlApi;

namespace BlImplementation;

internal class EngineerInTaskImplementation:IEngineerInTask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
}
