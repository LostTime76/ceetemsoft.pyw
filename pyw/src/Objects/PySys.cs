namespace CeetemSoft.Pyw;

public sealed partial class PySys
{
    public const string CacheDirAttrKey = "pycache_prefix";
    public const string ModulesAttrKey  = "modules";

    internal PySys() { }

    public PyStr CacheDir
    {
        get { return new PyStr(PyNative.PySys_GetAttr(CacheDirAttrKey)); }
        set { PyNative.PySys_SetAttr(CacheDirAttrKey, value); }
    }

    public PyDict Modules
    {
        get { return new PyDict(PyNative.PySys_GetAttr(ModulesAttrKey)); }
        set { PyNative.PySys_SetAttr(ModulesAttrKey, value); }
    }

    public PyObj this[string key]
    {
        get { return PyNative.PySys_GetAttr(key); }
        set { PyNative.PySys_SetAttr(key, value); }
    }
}