namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint, int> _PySequence_Check;

    public static bool PySeq_Check(nint hObj)
    {
        return (_PySequence_Check(hObj) != 0);
    }
}