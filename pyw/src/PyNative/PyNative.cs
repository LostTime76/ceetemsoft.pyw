namespace CeetemSoft.Pyw;

using System.Reflection;
using System.Runtime.InteropServices;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    internal static delegate* unmanaged<void> Py_Initialize;

    [PySymbol]
    internal static delegate* unmanaged<void> Py_Finalize;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, PyObj*> PyImport_Import;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, PyObj*> PyModule_GetDict;

    [PySymbol("PySys_GetObject")]
    internal static delegate* unmanaged<byte*, PyObj*> PySys_GetObj;

    [PySymbol("PySys_SetObject")]
    internal static delegate* unmanaged<byte*, PyObj*, int> PySys_SetObj;

    [PySymbol("PyUnicode_FromString")]
    internal static delegate* unmanaged<byte*, PyObj*> PyUnicode_FromStr;

    [PySymbol("PyUnicode_AsUTF8")]
    internal static delegate* unmanaged<PyObj*, byte*> PyUnicode_AsUtf8;

    [PySymbol("PyRun_String")]
    internal static delegate* unmanaged<byte*, int, PyObj*, PyObj*, PyObj*> PyRun_Str;

    [PySymbol]
    internal static delegate* unmanaged<PyTypeObj*, int> PyType_GetFlags;

    [PySymbol("PyObject_GetAttrString")]
    internal static delegate* unmanaged<PyObj*, byte*, PyObj*> PyObj_GetAttrStr;

    [PySymbol("PyObject_SetAttrString")]
    internal static delegate* unmanaged<PyObj*, byte*, PyObj*, int> PyObj_SetAttrStr;

    [PySymbol("PyObject_Str")]
    internal static delegate* unmanaged<PyObj*, PyObj*> PyObj_Str;

    [PySymbol]
    internal static delegate* unmanaged<int, PyObj*> PyList_New;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, int> PyList_Size;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, int, PyObj*> PyList_GetItem;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, int, int, PyObj*> PyList_GetSlice;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, PyObj*, int> PyList_Append;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, int, PyObj*, int> PyList_SetItem;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, int, int, PyObj*, int> PyList_SetSlice;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, int, PyObj*, int> PyList_Insert;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*> PyDict_New;

    [PySymbol("PyDict_GetItemString")]
    internal static delegate* unmanaged<PyObj*, byte*, PyObj*> PyDict_GetItemStr;

    [PySymbol]
    internal static delegate* unmanaged<PyObj*, PyObj*, PyObj*, int> PyDict_SetItem;

    [PySymbol("PyDict_SetItemString")]
    internal static delegate* unmanaged<PyObj*, byte*, PyObj*, int> PyDict_SetItemStr;

    private static nint hDll;

    internal static void SetDll(string dllPath)
    {
        if (hDll != 0)
        {
            // Clean up the dll
            NativeLibrary.Free(hDll);
        }

        // Load the dll
        hDll = NativeLibrary.Load(dllPath);

        // Load the symbols
        LoadSymbols();
    }

    private static void LoadSymbols()
    {
        // Iterate through all of the fields within the type
        foreach (FieldInfo field in typeof(PyNative).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
        {
            // Get the symbol attribute associated with the field
            PySymbolAttribute attr = field.GetCustomAttribute<PySymbolAttribute>();

            if (attr != null)
            {
                // Load the symbol
                LoadSymbol(field, attr);
            }
        }
    }

    private static void LoadSymbol(FieldInfo field, PySymbolAttribute attr)
    {
        // Get the symbol name
        string symbol = ((attr.Symbol != null) ? attr.Symbol : field.Name);

        // Load the symbol from the dll
        field.SetValue(null, NativeLibrary.GetExport(hDll, symbol));
    }
}