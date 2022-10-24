using System.Reflection;
using System.Runtime.InteropServices;

namespace CeetemSoft.Pyw;

internal static class PyNative
{
    // --------------------------------------------------------------------------------
    // High level functions
    // --------------------------------------------------------------------------------
    [DllImport(DllName)]
    internal static extern void Py_Initialize();

    [DllImport(DllName)]
    internal static extern void Py_Finalize();

    [DllImport(DllName)]
    internal static extern IntPtr PyImport_Import(IntPtr pPath);

    // --------------------------------------------------------------------------------
    // Number functions 
    // --------------------------------------------------------------------------------
    [DllImport(DllName)]
    internal static extern IntPtr PyLong_FromLong(long value);

    // --------------------------------------------------------------------------------
    // String functions 
    // --------------------------------------------------------------------------------
    [DllImport(DllName)]
    internal static extern IntPtr PyUnicode_AsUTF8(IntPtr pObj);

    [DllImport(DllName, CharSet = CharSet.Ansi)]
    internal static extern IntPtr PyUnicode_FromString(string input);

    // --------------------------------------------------------------------------------
    // Object functions
    // --------------------------------------------------------------------------------
    [DllImport(DllName)]
    internal static extern int PyObject_TypeCheck(IntPtr pObj, IntPtr pType);

    [DllImport(DllName)]
    internal static extern int PyObject_IsInstance(IntPtr pObj, IntPtr pRefObj);

    [DllImport(DllName)]
    internal static extern IntPtr PyObject_Str(IntPtr pObj);

    // --------------------------------------------------------------------------------
    // System object functions
    // --------------------------------------------------------------------------------
    [DllImport(DllName, CharSet = CharSet.Ansi)]
    internal static extern int PySys_SetObject(string attr, IntPtr pObj);

    [DllImport(DllName, CharSet = CharSet.Ansi)]
    internal static extern IntPtr PySys_GetObject(string path);

    // --------------------------------------------------------------------------------
    // List functions
    // --------------------------------------------------------------------------------
    [DllImport(DllName)]
    internal static extern int PyList_Size(IntPtr pList);

    [DllImport(DllName)]
    internal static extern int PyList_Append(IntPtr pList, IntPtr pItem);

    [DllImport(DllName)]
    internal static extern int PyList_SetSlice(IntPtr pList, int low, int high, IntPtr pItems);

    [DllImport(DllName)]
    internal static extern int PyList_SetItem(IntPtr pList, int idx, IntPtr pItem);

    [DllImport(DllName)]
    internal static extern IntPtr PyList_GetItem(IntPtr pList, int idx);

    [DllImport(DllName)]
    internal static extern IntPtr PyList_GetSlice(IntPtr pList, int low, int high);

    [DllImport(DllName)]
    internal static extern IntPtr PyList_New(int len);

    // --------------------------------------------------------------------------------
    // --------------------------------------------------------------------------------
    // --------------------------------------------------------------------------------
    private const string DllName = "pythonxxx.dll";

    private static string _dllPath;
    private static IntPtr _hDll;

    internal static void Init(string dllPath)
    {
        // Was there a previously loaded dll?
        if (_hDll != IntPtr.Zero)
        {
            // Clean up the loaded library
            NativeLibrary.Free(_hDll);
        }

        _dllPath = dllPath;
        _hDll    = IntPtr.Zero;

        // Set the resolver
        NativeLibrary.SetDllImportResolver(PyUtil.Assembly, DllResolve);
    }

    private static IntPtr DllResolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        // Load the library
        return _hDll = NativeLibrary.Load(_dllPath);
    }
}