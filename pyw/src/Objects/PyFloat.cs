namespace CeetemSoft.Pyw;

public readonly struct PyFloat
{
    public readonly nint Handle;

    public PyFloat() : this(0D) { }

    public PyFloat(double value)
    {
        if ((Handle = PyNative.PyFloat_New(value)) == 0)
        {
            ThrowCreateFailure(value);
        }
    }

    public PyFloat(nint hFloat)
    {
        if (hFloat == 0)
        {
            ThrowInvalidHandle();
        }
        else if (!PyNative.PyFloat_CheckType(hFloat))
        {
            ThrowCastFailure();
        }

        Handle = hFloat;
    }

    public override bool Equals(object obj)
    {
        return ((obj != null) ? ((PyFloat)obj == this) : false);
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

    public static bool operator ==(PyFloat a, PyFloat b)
    {
        return ((double)a == (double)b);
    }

    public static bool operator !=(PyFloat a, PyFloat b)
    {
        return ((double)a != (double)b);
    }

    public static implicit operator nint(PyFloat hFloat)
    {
        return hFloat.Handle;
    }

    public static explicit operator double(PyFloat obj)
    {
        return PyNative.PyFloat_AsDouble(obj);
    }

    public static explicit operator PyFloat(nint hFloat)
    {
        return new PyFloat(hFloat);
    }

    private static void ThrowCreateFailure(double value)
    {
        throw new Exception(string.Format("A new float object with value: {0} could not be created.", value));
    }

    private static void ThrowInvalidHandle()
    {
        throw new ArgumentNullException("The object handle cannot be 0.");
    }

    private static void ThrowCastFailure()
    {
        throw new InvalidCastException("The object behind the handle is not of float type.");
    }

    private static void ThrowToStrFailure()
    {
        throw new Exception("The string representation of the float object could not be retrieved.");
    }
}