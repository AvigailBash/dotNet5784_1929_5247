using DalApi;

namespace Dal;

internal class HelpImplementation : IHelp
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
