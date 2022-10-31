namespace CeetemSoft.Pyw;

public static class PyConst
{
    public const int    Error      = -1;
    public const string Python     = "python";
    public const char   PathDelim  = ';';
    public const string ExePathFmt = "{0}.exe";
    public const string DllPathFmt = "{0}.dll";
    public const string PathEnvVar = "Path";

    internal const int Py_Long_Subclass    = (1 << 24);
    internal const int Py_List_Subclass    = (1 << 25);
    internal const int Py_Tuple_Subclass   = (1 << 26);
    internal const int Py_Bytes_Subclass   = (1 << 27);
    internal const int Py_Unicode_Subclass = (1 << 28);
    internal const int Py_Dict_Subclass    = (1 << 29);
    internal const int Py_BaseExc_Subclass = (1 << 30);
    internal const int Py_Type_Subclass    = (1 << 31);
    internal const int Py_Single_Input     = 256;
    internal const int Py_File_Input       = 257;
    internal const int Py_Eval_Input       = 258;
}