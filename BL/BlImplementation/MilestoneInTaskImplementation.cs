using BlApi;

namespace BlImplementation;

internal class MilestoneInTaskImplementation: IMilestoneInTask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
}
