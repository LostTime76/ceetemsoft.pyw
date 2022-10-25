using System.Runtime.InteropServices;

namespace CeetemSoft.Pyw;

public static partial class PyNative
{
    // High level functions
    unsafe private static delegate* <void> Py_Initialize;
    unsafe private static delegate* <void> Py_Finalize;

    // String functions
    unsafe private static delegate* <string, IntPtr> PyUnicode_FromString;
    unsafe private static delegate* <IntPtr, IntPtr> PyUnicode_AsUTF8;

    // System object functions
    unsafe private static delegate* <string, IntPtr> PySys_GetObject;
    unsafe private static delegate* <string, IntPtr, int> PySys_SetObject;

    private static IntPtr _hDll;

    unsafe public static void SetSysObject([MarshalAs(UnmanagedType.LPStr)] string attr, PyObject obj)
    {
        if (PySys_SetObject(attr, obj._pObj) != 0)
        {
            
        }

        return (PySys_SetObject(attr, obj._pObj) == 0);
    }

    unsafe public static string ObjectToString(IntPtr pObj)
    {
        return Marshal.PtrToStringUTF8((PyUnicode_AsUTF8(pObj)));
    }

    unsafe public static IntPtr StringToObject([MarshalAs(UnmanagedType.LPStr)] string input)
    {
        return PyUnicode_FromString(input);
    }

    unsafe public static PyObject GetSysObject([MarshalAs(UnmanagedType.LPStr)] string attr)
    {
        return new PyObject(PySys_GetObject(attr));
    }

    unsafe internal static void PyStart()
    {
        Py_Initialize();
    }

    unsafe internal static void PyStop()
    {
        Py_Finalize();
    }

    unsafe internal static void Init(string dllPath)
    {
        if (_hDll != IntPtr.Zero)
        {
            NativeLibrary.Free(_hDll);
        }

        _hDll = NativeLibrary.Load(dllPath);

        // Load high level functions
        Py_Initialize = (delegate* <void>)NativeLibrary.GetExport(_hDll, nameof(Py_Initialize));
        Py_Finalize   = (delegate* <void>)NativeLibrary.GetExport(_hDll, nameof(Py_Finalize));

        // Load string functions
        PyUnicode_FromString = (delegate* <string, IntPtr>)NativeLibrary.GetExport(_hDll, nameof(PyUnicode_FromString));
        PyUnicode_AsUTF8     = (delegate* <IntPtr, IntPtr>)NativeLibrary.GetExport(_hDll, nameof(PyUnicode_AsUTF8));

        // Load system object functions
        PySys_GetObject = (delegate* <string, IntPtr>)NativeLibrary.GetExport(_hDll, nameof(PySys_GetObject));
        PySys_SetObject = (delegate* <string, IntPtr, int>)NativeLibrary.GetExport(_hDll, nameof(PySys_SetObject));
    }
}