using BlApi;

namespace BlImplementation;

internal class MilestoneInListImplementation:IMilestoneInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
}
