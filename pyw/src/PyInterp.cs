namespace CeetemSoft.Pyw;

public partial class PyInterp
{
    private const string ScriptsDir  = "scripts";
    private const string DebugModule = "dbg";

    public int    DebugPort { get; set; }
    public string DebugHost { get; set; }

    public readonly string      Name;
    public readonly string      Dir;
    public readonly string      DllPath;
    public readonly string      ExePath;
    public readonly string      Version;
    public readonly PySysObject Sys;

    public PyInterp(string version = null)
    {
        Dir     = GetPythonDir(version);
        Name    = Path.GetFileName(Dir);
        Version = Name.Substring(PyConst.Python.Length);
        DllPath = Path.Combine(Dir, string.Format(PyUtil.DllPathFmt, Name));
        ExePath = Path.Combine(Dir, string.Format(PyUtil.ExePathFmt, PyConst.Python));
        Sys     = new PySysObject();
        PyNative.Init(DllPath);
    }

    public void Stop()
    {
        // Clean up the interpreter
        PyNative.Py_Finalize();
    }

    public PyModule Start(string module, params string[] args)
    {
        return Start(module, args.AsSpan());
    }

    public PyModule Start(string module, ReadOnlySpan<string> args)
    {
        // Initialize the interpreter
        PyNative.Py_Initialize();

        // Initialize the system paths so the modules can be found
        InitSysPaths(module);

        // Attach the debugger
        AttachDebugger();

        // Start the main module
        return StartMainModule(module, args);
    }

    private void AttachDebugger()
    {
        if (DebugHost == null)
        {
            // There is no request to attach a debugger
            return;
        }

        // Set the arguments for the debugger
        Sys.Args = new PyList(new PyObject[]
        {
            new PyObject(ExePath),
            new PyObject(DebugHost),
            new PyObject((long)DebugPort)
        });

        // Start the debug module
        PyModule.Import(DebugModule);
    }

    private void InitSysPaths(string module)
    {
        // Get the system paths list
        PyList paths = Sys.Path.AsList();

        // Add the starting module and internal scripts path
        paths.Append(Path.GetDirectoryName(Path.GetFullPath(module)));
        paths.Append(Path.Combine(PyUtil.AssemblyDir, ScriptsDir));
    }

    private PyModule StartMainModule(string module, ReadOnlySpan<string> args)
    {
        // Set the module arguments
        Sys.Args = new PyList(args);

        // Start the module
        return PyModule.Import(Path.GetFileName(module));
    }

    private static string GetPythonDir(string version)
    {
        // Find any python directory within the path environment variable
        string dir = FindPythonDirFromPath();

        // If the user is requesting a specific python version, we have to try and find that version. Otherwise use the directory
        // that was found in the path. There should only be one python path set in the variable, which is the latest version to be used.
        return ((version != null) ? FindPythonVersion(dir, version) : dir);
    }

    private static string FindPythonVersion(string dir, string version)
    {
        // Get the root directory that contains all the different version installs
        string root = Path.GetDirectoryName(dir);

        // Iterate through all of the child installs
        foreach (string path in Directory.GetDirectories(root))
        {
            // Match the version
            if (Path.GetFileName(path).EndsWith(version, true, null))
            {
                return path;
            }
        }

        return ThrowHelper.PythonVerNotFound(root, version);
    }

    private static string FindPythonDirFromPath()
    {
        // Get the path environment variable
        string   var   = Environment.GetEnvironmentVariable(PyUtil.PathEnvVar);
        string[] paths = var.Split(PyUtil.PathDelim, StringSplitOptions.RemoveEmptyEntries);

        // Iterate through the paths
        foreach (string path in paths)
        {
            // Split the path into segments
            string[] dirs = path.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);

            // If the last segment contains python, we have found a python directory
            if (dirs[dirs.Length - 1].StartsWith(PyConst.Python, true, null))
            {
                // Return the directory
                return (path.Trim(Path.DirectorySeparatorChar));
            }
        }

        return ThrowHelper.PythonDirNotFound();
    }
}