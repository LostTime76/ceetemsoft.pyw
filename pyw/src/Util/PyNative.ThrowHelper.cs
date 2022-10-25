namespace CeetemSoft.Pyw;

public static partial class PyNative
{
    private static class ThrowHelper
    {
        public static void SetSysObjectFail(string attr)
        {
            throw new Exception(string.Format("Failed to set system object attr: {0}.", attr));
        }
    }
}