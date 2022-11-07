namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static nint _PyUnicode_Type;

    [PySymbol]
    private static delegate* unmanaged<byte*, nint> _PyUnicode_FromString;

    [PySymbol]
    private static delegate* unmanaged<nint, byte*> _PyUnicode_AsUTF8;

    internal static bool PyUnicode_CheckType(nint hObj)
    {
        return PyObj_IsInstance(hObj, _PyUnicode_Type);
    }

    internal static nint PyUnicode_Type()
    {
        return _PyUnicode_Type;
    }

    internal static nint PyUnicode_New(string str)
    {
        int   len  = GetUtf8StrLen(str);
        byte* pStr = stackalloc byte[len + 1];
        StrToUtf8Str(str, pStr, len);

        return _PyUnicode_FromString(pStr);
    }

    internal static byte* PyUnicode_AsUtf8(nint hStr)
    {
        return _PyUnicode_AsUTF8(hStr);
    }
}