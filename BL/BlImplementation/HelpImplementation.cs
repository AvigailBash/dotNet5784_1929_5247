
using DalApi;
using DO;

namespace BlImplementation;

internal class HelpImplementation:Ihelp
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void init()=>_dal.


    public void reset()
    {
        throw new NotImplementedException();
    }
}
