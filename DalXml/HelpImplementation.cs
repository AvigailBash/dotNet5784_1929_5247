using DalApi;

namespace Dal;

internal class HelpImplementation : Ihelp
{
    public void init()
    {
        Intilization.Do();
    }

    public void reset()
    {
        Intilization.reset();
    }
}
