namespace CeetemSoft.Pyw;

using System.Runtime.InteropServices;

unsafe public sealed partial class PyInterp
{
    public bool   Started { get; private set; }
    public string Name    { get; private set; }
    public string Dir     { get; private set; }
    public string DllPath { get; private set; }
    public string ExePath { get; private set; }

    public static readonly PyInterp Instance = new PyInterp();

    public static PySys Sys;

    private string _version;

    private PyInterp() { }

    public void Start()
    {
        if (Started)
        {
            ThrowHelper.Started();
        }
        else if (_version == null)
        {
            Init();
        }

        // Initialize and start the interpreter
        PyNative.Py_Initialize();

        // Get a reference to the system object
        Sys     = GetSysObj();
        Started = true;
    }

    public void Stop()
    {
        if (!Started)
        {
            ThrowHelper.Stopped();
        }

        // Stop and clean up the interpreter
        PyNative.Py_Finalize();

        // Clear the started flag
        Started = false;
    }

    public PyObj Eval(string expr)
    {
        int   len   = PyUtil.GetUtf8StrLen(expr);
        byte* pExpr = stackalloc byte[len + 1];
        PyUtil.StrToUtf8Str(expr, pExpr, len);

        // Get the system module dictionary
        PyObj* pGlobs = PyNative.PyModule_GetDict(Sys.pObj);

        // Evaluate the expression and return the result
        return new PyObj(PyNative.PyRun_Str(pExpr, PyConst.Py_Eval_Input, pGlobs, null));
    }

    public PyModule Import(string filepath)
    {
        return Import(filepath, ReadOnlySpan<string>.Empty);
    }

    public PyModule Import(string filepath, params string[] args)
    {
        return Import(filepath, args.AsSpan());
    }

    public PyModule Import(string filepath, params PyObj[] args)
    {
        return Import(filepath, args.AsSpan());
    }

    public PyModule Import(string filepath, ReadOnlySpan<string> args)
    {
        Span<PyObj> objArgs = stackalloc PyObj[args.Length];

        for (int idx = 0; idx < args.Length; idx++)
        {
            objArgs[idx] = Eval(args[idx]);
        }

        return Import(filepath, objArgs);
    }

    public PyModule Import(string filepath, ReadOnlySpan<PyObj> args)
    {
        // Get the full path to the script
        filepath = Path.GetFullPath(filepath);

        // Create a new argument list
        PyStr  modName = new PyStr(Path.GetFileNameWithoutExtension(filepath));
        PyList argv    = new PyList(args.Length + 1);

        // Set the first argument to the script filepath
        argv[0] = new PyStr(filepath);

        // Add the other arguments to the list
        for (int idx = 0; idx < args.Length; idx++)
        {
            argv[idx + 1] = args[idx];
        }

        // Set the arguments for the script
        Sys.Argv = argv;

        // Add the script directory to the system path if it is not already
        Sys.AppendToPath(Path.GetDirectoryName(filepath));

        // Import the module
        return new PyModule(PyNative.PyImport_Import(modName.pObj));
    }

    private void Init(string version = null)
    {
        if (Started)
        {
            ThrowHelper.VersionSetWhenStarted();
        }
        else if ((_version != null) && (version == _version))
        {
            return;
        }

        Dir      = GetPythonDir(version);
        Name     = Path.GetFileName(Dir);
        DllPath  = Path.Combine(Dir, string.Format(PyUtil.DllPathFmt, Name));
        ExePath  = Path.Combine(Dir, string.Format(PyUtil.ExePathFmt, PyConst.Python));
        _version = Name.Substring(PyConst.Python.Length);
        PyNative.SetDll(DllPath);
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

        return ThrowHelper.VerNotFound(root, version);
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

        return ThrowHelper.DirNotFound();
    }

    private static PySys GetSysObj()
    {
        int   len   = PyUtil.GetUtf8StrLen(PySys.ModulesAttr);
        byte* pAttr = stackalloc byte[len + 1];
        PyUtil.StrToUtf8Str(PySys.ModulesAttr, pAttr, len);

        // Get the modules dictionary
        PyDict modules = new PyDict(PyNative.PySys_GetObj(pAttr));

        // Return the system module
        return new PySys(modules[PySys.Name].pObj);
    }

    public string Version
    {
        get { return _version; }
        set { Init(value); }
    }
}