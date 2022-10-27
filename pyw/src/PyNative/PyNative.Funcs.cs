namespace CeetemSoft.Pyw;

using System.Runtime.InteropServices;

unsafe public static partial class PyNative
{
    private struct Funcs
    {
        // ----------------------------------------------------------------------------
        // High level functions
        // ----------------------------------------------------------------------------
        public delegate* unmanaged<void> Py_Initialize;
        public delegate* unmanaged<void> Py_Finalize;

        // ----------------------------------------------------------------------------
        // String functions
        // ----------------------------------------------------------------------------
        public delegate* unmanaged<byte*, PyStr*> PyUnicode_FromString;
        public delegate* unmanaged<PyStr*, byte*> PyUnicode_AsUTF8;

        // ----------------------------------------------------------------------------
        // System object functions
        // ----------------------------------------------------------------------------
        public delegate* unmanaged<PyObj*, byte*>      PySys_GetObject;
        public delegate* unmanaged<byte*, PyObj*, int> PySys_SetObject;

        // ----------------------------------------------------------------------------
        // Object functions
        // ----------------------------------------------------------------------------
        public delegate* unmanaged<PyObj*, PyStr*>              PyObject_Str;
        public delegate* unmanaged<PyObj*, PyObj*, PyObj*>      PyObject_GetAttr;
        public delegate* unmanaged<PyObj*, byte*, PyObj*>       PyObject_GetAttrString;
        public delegate* unmanaged<PyObj*, PyObj*, PyObj*, int> PyObject_SetAttr;
        public delegate* unmanaged<PyObj*, byte*, PyObj*, int>  PyObject_SetAttrString;

        // ----------------------------------------------------------------------------
        // List functions
        // ----------------------------------------------------------------------------
        public delegate* unmanaged<PyList*, int>              PyList_Size;
        public delegate* unmanaged<PyList*, int, PyObj*>      PyList_GetItem;
        public delegate* unmanaged<PyList*, int, PyObj*, int> PyList_SetItem;

        // ----------------------------------------------------------------------------
        // Dictionary functions
        // ----------------------------------------------------------------------------
        public delegate* unmanaged<PyDict*, int>                 PyDict_Size;
        public delegate* unmanaged<PyDict*, PyDict*, PyObj*>     PyDict_GetItem;
        public delegate* unmanaged<PyDict*, PyObj*, PyObj*, int> PyDict_SetItem;
        public delegate* unmanaged<PyDict*, PyList*>             PyDict_Keys;
        public delegate* unmanaged<PyDict*, PyList*>             PyDict_Values;

        public static void Load(IntPtr hDll, Funcs* pFuncs)
        {
            // High level functions
            LoadSymbol(hDll, nameof(Py_Initialize), &pFuncs->Py_Initialize);
            LoadSymbol(hDll, nameof(Py_Finalize), &pFuncs->Py_Finalize);

            // String functions
            LoadSymbol(hDll, nameof(PyUnicode_FromString), &pFuncs->PyUnicode_FromString);
            LoadSymbol(hDll, nameof(PyUnicode_AsUTF8), &pFuncs->PyUnicode_AsUTF8);

            // System object functions
            LoadSymbol(hDll, nameof(PySys_GetObject), &pFuncs->PySys_GetObject);
            LoadSymbol(hDll, nameof(PySys_SetObject), &pFuncs->PySys_SetObject);

            // Object functions
            LoadSymbol(hDll, nameof(PyObject_Str), &pFuncs->PyObject_Str);
            LoadSymbol(hDll, nameof(PyObject_GetAttr), &pFuncs->PyObject_GetAttr);
            LoadSymbol(hDll, nameof(PyObject_GetAttrString), &pFuncs->PyObject_GetAttrString);
            LoadSymbol(hDll, nameof(PyObject_SetAttr), &pFuncs->PyObject_SetAttr);
            LoadSymbol(hDll, nameof(PyObject_SetAttrString), &pFuncs->PyObject_SetAttrString);

            // List functions
            LoadSymbol(hDll, nameof(PyList_Size), &pFuncs->PyList_Size);
            LoadSymbol(hDll, nameof(PyList_GetItem), &pFuncs->PyList_GetItem);
            LoadSymbol(hDll, nameof(PyList_SetItem), &pFuncs->PyList_SetItem);

            // Dictionary functions
            LoadSymbol(hDll, nameof(PyDict_Size), &pFuncs->PyDict_Size);
            LoadSymbol(hDll, nameof(PyDict_GetItem), &pFuncs->PyDict_GetItem);
            LoadSymbol(hDll, nameof(PyDict_SetItem), &pFuncs->PyDict_SetItem);
            LoadSymbol(hDll, nameof(PyDict_Keys), &pFuncs->PyDict_Keys);
            LoadSymbol(hDll, nameof(PyDict_Values), &pFuncs->PyDict_Values);
        }

        private static void LoadSymbol(IntPtr hDll, string symbol, void* pFunc)
        {
            // Load the exported symbol
            *((nint*)pFunc) = NativeLibrary.GetExport(hDll, symbol);
        }
    }
}