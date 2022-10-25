namespace CeetemSoft.Pyw;

public readonly struct PyObject
{
    public readonly IntPtr _pObj;

    public PyObject(IntPtr pObj)
    {
        _pObj = pObj;
    }

    public static implicit operator PyObject(PyString str)
    {
        return new PyObject(str._pObj);
    }
}