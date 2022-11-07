namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint, int> _PyMapping_Check;
    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyMapping_Items;

    public static bool PyMap_Check(nint hObj)
    {
        return (_PyMapping_Check(hObj) != 0);
    }

    public static nint PyMap_Items(nint hObj)
    {
        return _PyMapping_Items(hObj);
    }
}