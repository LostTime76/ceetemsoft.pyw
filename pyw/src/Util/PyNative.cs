namespace CeetemSoft.Pyw;

using System.Runtime.InteropServices;

unsafe public static partial class PyNative
{
    // High level functions
    private static delegate* <void> Py_Initialize;
    private static delegate* <void> Py_Finalize;

    // String functions
    private static delegate* <nint, nint> PyUnicode_FromString;
    private static delegate* <nint, nint> PyUnicode_AsUTF8;

    // System object functions
    private static delegate* <nint, nint>      PySys_GetObject;
    private static delegate* <nint, nint, int> PySys_SetObject;

    // Object functions
    private static delegate* <nint, nint>            PyObject_Str;
    private static delegate* <nint, nint, nint>      PyObject_GetAttr;
    private static delegate* <nint, nint, nint>      PyObject_GetAttrString;
    private static delegate* <nint, nint, nint, int> PyObject_SetAttr;
    private static delegate* <nint, nint, nint, int> PyObject_SetAttrString;

    // List functions
    private static delegate* <nint, int>            PyList_Size;
    private static delegate* <nint, int, nint>      PyList_GetItem;
    private static delegate* <nint, int, nint, int> PyList_SetItem;

    // Dictionary functions
    private static delegate* <nint, int>             PyDict_Size;
    private static delegate* <nint, nint, nint>      PyDict_GetItem;
    private static delegate* <nint, nint, nint, int> PyDict_SetItem;
    private static delegate* <nint, nint>            PyDict_Keys;
    private static delegate* <nint, nint>            PyDict_Values;

    private static nint _hDll;

    internal static void Py_Init()
    {
        Py_Initialize();
    }

    internal static void Py_DeInit()
    {
        Py_Finalize();
    }

    public static nint PyUnicode_FromStr(string str)
    {
        nint pStr = Marshal.StringToHGlobalAnsi(str);
        nint pObj = PyUnicode_FromString(pStr);
        Marshal.FreeHGlobal(pStr);

        if (pObj == 0)
        {
            ThrowHelper.PyUnicode_FromStr_Fail(str);
        }

        return pObj;
    }

    public static string PyUnicode_AsUtf8(nint pObj)
    {
        nint pStr = PyUnicode_AsUTF8(pObj);

        if (pStr == 0)
        {
            ThrowHelper.PyUnicode_AsUtf8_Fail();
        }

        return Marshal.PtrToStringUTF8(pStr);
    }

    public static nint PySys_GetAttr(string key)
    {
        nint pKeyStr = Marshal.StringToHGlobalAnsi(key);
        nint pObj    = PySys_GetObject(pKeyStr);
        Marshal.FreeHGlobal(pKeyStr);

        // Allowed to return NULL if attribute does not exist
        return pObj;
    }

    public static void PySys_SetAttr(string key, nint pObj)
    {
        nint pKeystr = Marshal.StringToHGlobalAnsi(key);
        int  result  = PySys_SetObject(pKeystr, pObj);
        Marshal.FreeHGlobal(pKeystr);

        if (result != 0)
        {
            ThrowHelper.PySys_SetAttr_Fail(key);
        }
    }

    public static nint PyObj_Str(nint pObj)
    {
        nint pStrObj = PyObject_Str(pObj);

        if (pStrObj == 0)
        {
            ThrowHelper.PyObj_Str_Fail();
        }

        return pStrObj;
    }

    public static nint PyObj_GetAttr(nint pObj, nint pKeyObj)
    {
        // Alloed to return NULL if the attribute does not exist
        return PyObject_GetAttr(pObj, pKeyObj);
    }

    public static nint PyObj_GetAttr(nint pObj, string key)
    {
        nint pKeyStr = Marshal.StringToHGlobalAnsi(key);
        nint pKeyObj = PyObject_GetAttr(pObj, pKeyStr);
        Marshal.FreeHGlobal(pKeyStr);

        // Allowed to return NULL if the attribute does not exist
        return pKeyObj;
    }

    public static void PyObj_SetAttr(nint pObj, nint pKeyObj, nint pValObj)
    {
        if (PyObject_SetAttr(pObj, pKeyObj, pValObj) != 0)
        {
            ThrowHelper.PyObj_SetAttr_Fail();
        }
    }

    public static void PyObj_SetAttr(nint pObj, string key, nint pValObj)
    {
        nint pKeyStr = Marshal.StringToHGlobalAnsi(key);
        int  result  = PyObject_SetAttrString(pObj, pKeyStr, pValObj);
        Marshal.FreeHGlobal(pKeyStr);

        if (result != 0)
        {
            ThrowHelper.PyObj_SetAttr_Fail(key);
        }
    }

    public static int PyList_Count(nint pObj)
    {
        return PyList_Size(pObj);
    }

    public static nint PyList_GetElt(nint pObj, int idx)
    {
        nint pVal = PyList_GetItem(pObj, idx);

        if (pVal == 0)
        {
            ThrowHelper.PyList_GetElt_Fail(idx);
        }

        return pVal;
    }

    public static void PyList_SetElt(nint pObj, int idx, nint pVal)
    {
        if (PyList_SetItem(pObj, idx, pVal) != 0)
        {
            ThrowHelper.PyList_SetElt_Fail(idx);
        }
    }

    public static int PyDict_Count(nint pObj)
    {
        return PyDict_Size(pObj);
    }

    public static nint PyDict_GetElt(nint pObj, nint pKeyObj)
    {
        nint pVal = PyDict_GetItem(pObj, pKeyObj);

        if (pVal == 0)
        {
            ThrowHelper.PyDict_GetElt_Fail();
        }

        return pVal;
    }

    public static void PyDict_SetElt(nint pObj, nint pKeyObj, nint pVal)
    {
        if (PyDict_SetItem(pObj, pKeyObj, pVal) != 0)
        {
            ThrowHelper.PyDict_SetElt_Fail();
        }
    }

    public static nint PyDict_GetKeys(nint pObj)
    {
        // Allowed to return NULL
        return PyDict_Keys(pObj);
    }

    public static nint PyDict_GetValues(nint pObj)
    {
        // Allowed to return NULL
        return PyDict_Values(pObj);
    }

    internal static void SetDll(string dllPath)
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
        PyUnicode_FromString = (delegate* <nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PyUnicode_FromString));
        PyUnicode_AsUTF8     = (delegate* <nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PyUnicode_AsUTF8));

        // Load system object functions
        PySys_GetObject = (delegate* <nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PySys_GetObject));
        PySys_SetObject = (delegate* <nint, nint, int>)NativeLibrary.GetExport(_hDll, nameof(PySys_SetObject));

        // Load object functions
        PyObject_Str           = (delegate* <nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PyObject_Str));
        PyObject_GetAttr       = (delegate* <nint, nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PyObject_GetAttr));
        PyObject_GetAttrString = (delegate* <nint, nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PyObject_GetAttrString));
        PyObject_SetAttr       = (delegate* <nint, nint, nint, int>)NativeLibrary.GetExport(_hDll, nameof(PyObject_SetAttr));
        PyObject_SetAttrString = (delegate* <nint, nint, nint, int>)NativeLibrary.GetExport(_hDll, nameof(PyObject_SetAttrString));

        // Load list functions
        PyList_Size    = (delegate* <nint, int>)NativeLibrary.GetExport(_hDll, nameof(PyList_Size));
        PyList_GetItem = (delegate* <nint, int, nint>)NativeLibrary.GetExport(_hDll, nameof(PyList_GetItem));
        PyList_SetItem = (delegate* <nint, int, nint, int>)NativeLibrary.GetExport(_hDll, nameof(PyList_SetItem));

        // Load dictionary functions
        PyDict_Size    = (delegate* <nint, int>)NativeLibrary.GetExport(_hDll, nameof(PyDict_Size));
        PyDict_GetItem = (delegate* <nint, nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PyDict_GetItem));
        PyDict_SetItem = (delegate* <nint, nint, nint, int>)NativeLibrary.GetExport(_hDll, nameof(PyDict_SetItem));
        PyDict_Keys    = (delegate* <nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PyDict_Keys));
        PyDict_Values  = (delegate* <nint, nint>)NativeLibrary.GetExport(_hDll, nameof(PyDict_Values));
    }
}