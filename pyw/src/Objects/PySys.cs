namespace CeetemSoft.Pyw;

unsafe public readonly partial struct PySys
{
    public const string Name         = "sys";
    public const string ArgvAttr     = "argv";
    public const string CacheDirAttr = "pycache_prefix";
    public const string PathAttr     = "path";
    public const string ModulesAttr  = "modules";

    internal readonly PyObj* pObj = null;

    public PySys()
    {
        ThrowHelper.CannotNew();
    }

    internal PySys(PyObj* pObj)
    {
        this.pObj = pObj;
    }

    public void AppendToPath(string dir)
    {
        PyObj obj = Path;

        if (!PyList.IsList(obj))
        {
            ThrowHelper.PathIsNotList();
        }

        PyList paths = (PyList)obj;

        // Iterate through all of the items within the path
        foreach (PyObj item in paths)
        {
            if (PyStr.IsStr(item) && ((string)((PyStr)item) == dir))
            {
                // The directory is already within the path
                return;
            }
        }

        // The directory is not within the path, so add it
        paths.Add(new PyStr(dir));
    }

    public PyObj Argv
    {
        get { return this[ArgvAttr]; }
        set { this[ArgvAttr] = value; }
    }

    public PyObj Path
    {
        get { return this[PathAttr]; }
        set { this[PathAttr] = value; }
    }

    public PyObj this[string attr]
    {
        get
        {
            int   len   = PyUtil.GetUtf8StrLen(attr);
            byte* pAttr = stackalloc byte[len + 1];
            PyUtil.StrToUtf8Str(attr, pAttr, len);

            return new PyObj(PyNative.PyObj_GetAttrStr(pObj, pAttr));
        }
        
        set
        {
            int   len   = PyUtil.GetUtf8StrLen(attr);
            byte* pAttr = stackalloc byte[len + 1];
            PyUtil.StrToUtf8Str(attr, pAttr, len);

            if (PyNative.PyObj_SetAttrStr(pObj, pAttr, value.pObj) != 0)
            {
                ThrowHelper.CannotSetAttr(attr);
            }
        }
    }
}