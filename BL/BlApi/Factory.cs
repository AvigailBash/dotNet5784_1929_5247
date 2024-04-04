namespace BlApi;
/// <summary>
/// A class that knows at runtime what data structure to use and what dal and fetches the data
/// </summary>
public static class Factory
{
    public static IBl Get() => new BlImplementation.Bl();
}
