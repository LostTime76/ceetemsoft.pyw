namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<byte*, PyObj*> _PyUnicode_FromString;

    [PySymbol]
    private static delegate* unmanaged<PyObj*, byte*> _PyUnicode_AsUTF8;

    internal static PyObj* PyUnicode_New(string str)
    {
        int   len  = GetUtf8StrLen(str);
        byte* pStr = stackalloc byte[len + 1];
        StrToUtf8Str(str, pStr, len);

        return _PyUnicode_FromString(pStr);
    }

    internal static byte* PyUnicode_AsUtf8(PyObj* pObj)
    {
        return _PyUnicode_AsUTF8(pObj);
    }
}