namespace CeetemSoft.Pyw;

using System.Runtime.InteropServices;

unsafe public static partial class PyNative
{
    private const int Utf8_CodePage = 65001;

    [DllImport("kernel32.dll")]
    private static extern int WideCharToMultiByte(
        int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string str, int strLen, byte* pDst,
        int dstLen, byte* pDefChar, out bool usedDefChar);

    [DllImport("kernel32.dll")]
    private static extern int MultiByteToWideChar(
        int codePage, int flags, byte* pSrc, int srcLen,
        [MarshalAs(UnmanagedType.LPWStr)] string str, int strLen);

    private static nint  hDll;
    private static Funcs funcs;

    // ----------------------------------------------------------------------------
    // High level functions
    // ----------------------------------------------------------------------------
    internal static void Py_Init()
    {
        funcs.Py_Initialize();
    }

    internal static void Py_DeInit()
    {
        funcs.Py_Finalize();
    }

    // ----------------------------------------------------------------------------
    // String functions
    // ----------------------------------------------------------------------------
    internal static PyStr* PyUnicode_FromStr(string str)
    {
        int   len  = GetUtf8StrLen(str);
        byte* pDst = stackalloc byte[len + 1];

        // Convert the NET string to Utf8
        StrToUtf8Str(str, pDst, len);

        // Create the python string object
        PyStr* pStr = funcs.PyUnicode_FromString(pDst);

        if (pStr == null)
        {
            ThrowHelper.PyUnicode_FromStr();
        }

        return pStr;
    }

    internal static string PyUnicode_ToStr(PyStr* pStr)
    {
        byte* pSrc = funcs.PyUnicode_AsUTF8(pStr);

        if (pSrc == null)
        {
            ThrowHelper.PyUnicode_ToStr();
        }

        return new string((sbyte*)pSrc);
    }

    internal static void SetDll(string dllPath)
    {
        if (hDll != 0)
        {
            NativeLibrary.Free(hDll);
        }

        // Load the native python library
        hDll = NativeLibrary.Load(dllPath);

        // Get a pointer to the structure containing all of the library functions
        fixed (Funcs* pFuncs = &funcs)
        {
            // Load the library functions
            Funcs.Load(hDll, pFuncs);
        }
    }

    private static void StrToUtf8Str(string str, byte* pDst, int dstLen)
    {
        WideCharToMultiByte(Utf8_CodePage, 0, str, str.Length, pDst, dstLen, null, out _);
    }

    private static int GetUtf8StrLen(string str)
    {
        return WideCharToMultiByte(Utf8_CodePage, 0, str, str.Length, null, 0, null, out _);
    }
}