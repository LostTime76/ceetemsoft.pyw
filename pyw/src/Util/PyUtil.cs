namespace CeetemSoft.Pyw;

using System.Runtime.InteropServices;

unsafe public static class PyUtil
{
    public const char   PathDelim  = ';';
    public const string ExePathFmt = "{0}.exe";
    public const string DllPathFmt = "{0}.dll";
    public const string PathEnvVar = "Path";

    private const int Utf8_CodePage = 65001;

    [DllImport("kernel32.dll")]
    private static extern int WideCharToMultiByte(
        int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string str, int strLen, byte* pDst,
        int dstLen, byte* pDefChar, out bool usedDefChar);

    [DllImport("kernel32.dll")]
    private static extern int MultiByteToWideChar(
        int codePage, int flags, byte* pSrc, int srcLen,
        [MarshalAs(UnmanagedType.LPWStr)] string str, int strLen);

    internal static void StrToUtf8Str(string str, byte* pDst, int dstLen)
    {
        WideCharToMultiByte(Utf8_CodePage, 0, str, str.Length, pDst, dstLen, null, out _);
    }

    internal static int GetUtf8StrLen(string str)
    {
        return WideCharToMultiByte(Utf8_CodePage, 0, str, str.Length, null, 0, null, out _);
    }
}