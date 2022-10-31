namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<byte*, int, PyObj*, PyObj*, PyObj*> _PyRun_String;

    internal static PyObj* PyRun_Run(string input, int mode, PyObj* pGlobals, PyObj* pLocals)
    {
        int   len    = GetUtf8StrLen(input);
        byte* pInput = stackalloc byte[len + 1];
        StrToUtf8Str(input, pInput, len);

        return _PyRun_String(pInput, mode, pGlobals, pLocals);
    }
}