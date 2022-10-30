namespace CeetemSoft.Pyw;

using System.Runtime.InteropServices;

unsafe internal readonly struct PyTypeObj
{

}

[StructLayout(LayoutKind.Sequential)]
unsafe internal readonly struct PyObjBase
{
    internal readonly long       refCnt;
    internal readonly PyTypeObj* pType;
}