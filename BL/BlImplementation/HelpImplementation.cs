using BlApi;
using DO;

namespace BlImplementation;

internal class HelpImplementation:IHelp
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void init() => _dal.Help.init();


    public void reset() => _dal.Help.reset();
}
