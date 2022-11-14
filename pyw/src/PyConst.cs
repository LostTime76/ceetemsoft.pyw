namespace CeetemSoft.Pyw;

public static class PyConst
{
    public const int    Error         = -1;
    public const int    True          = 1;
    public const int    False         = 0;
    public const nint   InvalidHandle = 0;
    public const char   PathDelim     = ';';
    public const string Python        = "python";
    public const string ExePathFmt    = "{0}.exe"; 
    public const string DllPathFmt    = "{0}.dll";
    public const string PathEnvVar    = "Path";

    internal const int Py_Single_Input = 256;
    internal const int Py_File_Input   = 257;
    internal const int Py_Eval_Input   = 258;
}