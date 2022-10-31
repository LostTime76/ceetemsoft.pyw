namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<byte*, PyObj*> _PySys_GetObject;

    [PySymbol]
    private static delegate* unmanaged<byte*, PyObj*, int> _PySys_SetObject;

    internal static PyObj* PySys_GetObj(string attr)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return _PySys_GetObject(pAttr);
    }

    internal static bool PySys_SetObj(string attr, PyObj* pValue)
    {
        int   len   = GetUtf8StrLen(attr);
        byte* pAttr = stackalloc byte[len + 1];
        StrToUtf8Str(attr, pAttr, len);

        return (_PySys_SetObject(pAttr, pValue) == 0);
    }
}