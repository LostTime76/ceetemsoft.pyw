namespace CeetemSoft.Pyw;

#pragma warning disable 649
unsafe internal readonly struct PyObjBase
{
    internal readonly nint refCnt;
    internal readonly nint pType;
}