namespace CeetemSoft.Pyw;

public class PyObj
{
    public readonly nint Handle;

    public PyObj(nint hObj)
    {
        if (hObj == 0)
        {
            ThrowInvalidHandle();
        }

        Handle = hObj;
    }

    public override string ToString()
    {
        string str = PyNative.PyObj_NetStr(Handle);

        if (str == null)
        {
            ThrowToStrFailure();
        }

        return str;
    }

    public static implicit operator nint(PyObj obj)
    {
        return obj.Handle;
    }

    public static implicit operator PyObj(PyValObj obj)
    {
        return new PyObj(obj.Handle);
    }

    public static explicit operator PyObj(nint hObj)
    {
        return new PyObj(hObj);
    }

    private static void ThrowInvalidHandle()
    {
        throw new ArgumentNullException("The handle value is not valid.");
    }

    private static void ThrowToStrFailure()
    {
        throw new Exception("The string representation of the value could not be obtained.");
    }
}