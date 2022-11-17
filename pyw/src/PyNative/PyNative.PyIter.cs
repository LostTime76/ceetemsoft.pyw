namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal static partial class PyNative
{
    [PySymbol]
    private static delegate* unmanaged<nint, nint> _PyIter_Next;

    internal static nint PyIter_Next(nint hIter)
    {
        return _PyIter_Next(hIter);
    }
}