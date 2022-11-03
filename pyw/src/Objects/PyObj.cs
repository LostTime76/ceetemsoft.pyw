namespace CeetemSoft.Pyw;

public abstract class PyObj
{
    public readonly nint Handle;

    protected PyObj(nint hObj)
    {
        if (hObj == 0)
        {
            ThrowInvalidHandle();
        }

        Handle = hObj;
    }

    public override bool Equals(object obj)
    {
        return ((obj != null) ? ((PyObj)obj == this) : false);
    }

    public override int GetHashCode()
    {
        return Handle.GetHashCode();
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

    public static bool operator ==(PyObj a, PyObj b)
    {
        return (a.Handle == b.Handle);
    }

    public static bool operator !=(PyObj a, PyObj b)
    {
        return (a.Handle != b.Handle);
    }

    public static implicit operator nint(PyObj obj)
    {
        return obj.Handle;
    }

    private static void ThrowInvalidHandle()
    {
        throw new ArgumentNullException("The object handle cannot be 0.");
    }

    private static void ThrowToStrFailure()
    {
        throw new Exception("The string representation of the object could not be obtained.");
    }
}