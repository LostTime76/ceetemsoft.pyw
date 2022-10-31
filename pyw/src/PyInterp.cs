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
        DllPath  = Path.Combine(Dir, string.Format(PyConst.DllPathFmt, Name));
        ExePath  = Path.Combine(Dir, string.Format(PyConst.ExePathFmt, PyConst.Python));
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
        string   var   = Environment.GetEnvironmentVariable(PyConst.PathEnvVar);
        string[] paths = var.Split(PyConst.PathDelim, StringSplitOptions.RemoveEmptyEntries);

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

    public string Version
    {
        get { return _version; }
        set { Init(value); }
    }
}