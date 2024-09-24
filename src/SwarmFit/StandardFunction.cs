namespace SwarmFit;

public static class StandardFunction
{
    public static IFunction[] GetAll()
    {
        return System.Reflection.Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsClass)
            .Where(x => !x.IsAbstract)
            .Where(x => x.GetInterfaces().Contains(typeof(IFunction)))
            .Where(x => x.GetConstructors().Where(x => x.GetParameters().Length == 0).Any())
            .Select(x => (IFunction)Activator.CreateInstance(x)!)
            .ToArray();
    }
}
