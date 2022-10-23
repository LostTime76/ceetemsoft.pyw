using System.Net;

namespace CeetemSoft.Pyw;

public partial class PyInterp
{
    private const string ScriptsDir  = "scripts";
    private const string DebugModule = "dbg";

    public int    DebugPort { get; set; }
    public string DebugHost { get; set; }

    public readonly string Name;
    public readonly string Dir;
    public readonly string DllPath;
    public readonly string ExePath;
    public readonly string Version;

    public PyInterp(string version = null)
    {
        Dir     = GetPythonDir(version);
        Name    = Path.GetFileName(Dir);
        Version = Name.Substring(Pyw.Python.Length);
        DllPath = Path.Combine(Dir, string.Format(Pyw.DllPathFmt, Name));
        ExePath = Path.Combine(Dir, string.Format(Pyw.ExePathFmt, Pyw.Python));
    }

    public PyObject Start(string modulePath, params string[] args)
    {
        return Start(modulePath, args.AsSpan<string>());
    }

    public PyObject Start(string modulePath, ReadOnlySpan<string> args)
    {
        // Set up the native python library
        PyNative.Init(DllPath);

        // Initialize the interpreter
        PyNative.Py_Initialize();

        // Initialize the system paths so modules can be found
        InitSysPaths(modulePath);

        // Has debugging been requested?
        if (DebugHost != null)
        {
            AttachDebugger();
        }

        // Set the args passed to the starting module
        SetSysArgs(args);

        // Import and run the module
        return Import(Path.GetFileName(modulePath));
    }

    public PyObject Import(string modulePath)
    {
        return new PyObject(PyNative.PyImport_Import(PyNative.PyUnicode_FromString(modulePath)));
    }

    public PyObject GetSysObject(string attr)
    {
        return new PyObject(PyNative.PySys_GetObject(attr));
    }

    public PyList GetSysList(string attr)
    {
        return new PyList(PyNative.PySys_GetObject(attr));
    }

    private void InitSysPaths(string modulePath)
    {
        PyList paths = GetSysList(Pyw.SysPathAttr);

        // Add the starting module and internal scripts path
        paths.Append(Path.GetDirectoryName(Path.GetFullPath(modulePath)));
        paths.Append(Path.Combine(Pyw.AssemblyDir, ScriptsDir));
    }

    private void SetSysArgs(params string[] args)
    {
        SetSysArgs(args.AsSpan());
    }

    private void SetSysArgs(ReadOnlySpan<string> args)
    {
        // Create a new argument list
        PyList argv = new PyList(args.Length);

        for (int idx = 0; idx < args.Length; idx++)
        {
            argv.SetItem(idx, args[idx]);
        }

        // Set the system arguments
        PyNative.PySys_SetObject(Pyw.SysArgvAttr, argv.Pointer);
    }

    private void AttachDebugger()
    {
        // Set up the system arguments for the debugger
        // argv[0] = Python executable path
        // argv[1] = host
        // argv[2] = port
        SetSysArgs(ExePath, DebugHost, DebugPort.ToString());

        // Import and execute the debug module
        Import(DebugModule);
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
            if (Path.GetFileName(path).EndsWith(version))
            {
                return path;
            }
        }

        return ThrowHelper.PythonVerNotFound(root, version);
    }

    private static string FindPythonDirFromPath()
    {
        // Get the path environment variable
        string   var   = Environment.GetEnvironmentVariable(Pyw.PathEnvVar);
        string[] paths = var.Split(Pyw.PathDelim, StringSplitOptions.RemoveEmptyEntries);

        // Iterate through the paths
        foreach (string path in paths)
        {
            // Split the path into segments
            string[] dirs = path.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);

            // If the last segment contains python, we have found a python directory
            if (dirs[dirs.Length - 1].StartsWith(Pyw.Python, StringComparison.OrdinalIgnoreCase))
            {
                // Return the directory
                return (path.Trim(Path.DirectorySeparatorChar));
            }
        }

        return ThrowHelper.PythonDirNotFound();
    }
}