using System.Reflection;
using System.Runtime.InteropServices;

namespace CeetemSoft.Pyw;

internal static class PyNative
{
    internal static string DllPath { get; private set; }

    [DllImport(Pyw.Python)]
    internal static extern void Py_Initialize();

    [DllImport(Pyw.Python)]
    internal static extern int PyList_Size(IntPtr list);

    [DllImport(Pyw.Python)]
    internal static extern int PyList_Append(IntPtr list, IntPtr item);

    [DllImport(Pyw.Python)]
    internal static extern int PyList_SetSlice(IntPtr list, int low, int high, IntPtr items);

    [DllImport(Pyw.Python)]
    internal static extern int PyList_SetItem(IntPtr list, int idx, IntPtr item);

    [DllImport(Pyw.Python, CharSet = CharSet.Ansi)]
    internal static extern int PySys_SetObject(string attr, IntPtr obj);

    [DllImport(Pyw.Python)]
    internal static extern IntPtr PyUnicode_AsUTF8(IntPtr obj);

    [DllImport(Pyw.Python)]
    internal static extern IntPtr PyList_GetItem(IntPtr list, int idx);

    [DllImport(Pyw.Python)]
    internal static extern IntPtr PyList_New(int len);

    [DllImport(Pyw.Python, CharSet = CharSet.Ansi)]
    internal static extern IntPtr PyUnicode_FromString(string input);

    [DllImport(Pyw.Python, CharSet = CharSet.Ansi)]
    internal static extern IntPtr PySys_GetObject(string path);

    [DllImport(Pyw.Python)]
    internal static extern IntPtr PyImport_Import(IntPtr path);

    internal static void Init(string dllPath)
    {
        DllPath = dllPath;

        // Set the resolver
        NativeLibrary.SetDllImportResolver(Pyw.Assembly, DllResolve);
    }

    private static IntPtr DllResolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        return NativeLibrary.Load(DllPath);
    }
}