using System.Reflection;

namespace CeetemSoft.Pyw;

public static partial class PyDbg
{
    private static readonly Dictionary<nint, IPyDbgViewProvider> _providers = InitProviders();

    public static string GetValue(PyObj obj)
    {
        // Get the type associated with the object
        nint hType = PyNative.PyObj_Type(obj.Handle);

        // Fall back to the ToString method if the type is not in the dictionary
        return (_providers.ContainsKey(hType) ? _providers[hType].GetValue(obj) : PyNative.PyObj_NetStr(obj.Handle));
    }

    public static object CreateView(PyObj obj, string name)
    {
        // Get the type associated with the object
        nint hType = PyNative.PyObj_Type(obj.Handle);

        // Fall back to the object view if the type is not in the dictionary
        return (_providers.ContainsKey(hType) ? _providers[hType].CreateView(obj, name) : new PyObjDbgView(obj, name));
    }

    private static Dictionary<nint, IPyDbgViewProvider> InitProviders()
    {
        Dictionary<nint, IPyDbgViewProvider> providers = new Dictionary<nint, IPyDbgViewProvider>();

        // Iterate through all the assemblies in the current app domain
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // Add providers from assembly
            AddCreatorsFromAssembly(assembly, providers);
        }

        // Register for new assembly loads
        AppDomain.CurrentDomain.AssemblyLoad += AssemblyLoaded;

        return providers;
    }

    private static void AddCreatorsFromAssembly(Assembly assembly, Dictionary<nint, IPyDbgViewProvider> providers)
    {
        // Iterate through all the types in the assembly
        foreach (Type type in assembly.GetTypes())
        {
            // Determine if the type is a debug view provider
            if (!typeof(IPyDbgViewProvider).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract)
            {
                continue;
            }

            // Instantiate a new provider for the type
            IPyDbgViewProvider provider = (IPyDbgViewProvider)Activator.CreateInstance(type);

            // Add the provider to the dictionary
            providers.Add(provider.GetPyType(), provider);
        }
    }

    private static void AssemblyLoaded(object sender, AssemblyLoadEventArgs args)
    {
        // Add providers to the dictionary
        AddCreatorsFromAssembly(args.LoadedAssembly, _providers);
    }
}