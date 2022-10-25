namespace CeetemSoft.Pyw;

public readonly partial struct PySys
{
    private const string CachePrefix = "pycache_prefix";

    public PyObject CacheDir
    {
        get { return PyNative.GetSysObject(CachePrefix); }
        set { PyNative.SetSysObject(CachePrefix, value); }
    }
}