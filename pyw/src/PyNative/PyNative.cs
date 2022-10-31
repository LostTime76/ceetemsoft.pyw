namespace CeetemSoft.Pyw;

using System.Reflection;
using System.Runtime.InteropServices;

unsafe internal static partial class PyNative
{
    private const int SymbolStart   = 1;
    private const int Utf8_CodePage = 65001;

    [DllImport("kernel32.dll")]
    private static extern int WideCharToMultiByte(
        int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string str, int strLen, byte* pDst,
        int dstLen, byte* pDefChar, out bool usedDefChar);

    [DllImport("kernel32.dll")]
    private static extern int MultiByteToWideChar(
        int codePage, int flags, byte* pSrc, int srcLen,
        [MarshalAs(UnmanagedType.LPWStr)] string str, int strLen);

    private static nint hDll;

    internal static void SetDll(string dllPath)
    {
        if (hDll != 0)
        {
            // Clean up the dll
            NativeLibrary.Free(hDll);
        }

        // Load the dll
        hDll = NativeLibrary.Load(dllPath);

        // Load the symbols
        LoadSymbols();
    }

    private static void LoadSymbols()
    {
        // Iterate through all of the fields within the type
        foreach (FieldInfo field in typeof(PyNative).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
        {
            // Get the symbol attribute associated with the field
            PySymbolAttribute attr = field.GetCustomAttribute<PySymbolAttribute>();

            if (attr != null)
            {
                // Load the symbol
                LoadSymbol(field);
            }
        }
    }

    private static void LoadSymbol(FieldInfo field)
    {
        // Get the symbol name
        string symbol = field.Name.Substring(SymbolStart);

        // Load the symbol
        field.SetValue(null, NativeLibrary.GetExport(hDll, symbol));
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