namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PyModule
{
    internal readonly PyObj* pObj;

    internal PyModule(PyObj* pObj)
    {
        this.pObj = pObj;
    }

    public static implicit operator PyObj(PyModule module)
    {
        return new PyObj(module.pObj);
    }
}