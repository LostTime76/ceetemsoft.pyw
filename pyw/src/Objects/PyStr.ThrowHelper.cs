namespace CeetemSoft.Pyw;

public partial class PyStr
{
    private static class ThrowHelper
    {
        public static void CreateFail(string str)
        {
            throw new Exception(string.Format("Could not create a new python string with the value: {0}.", str));
        }
    }
}