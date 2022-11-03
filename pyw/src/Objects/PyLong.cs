namespace CeetemSoft.Pyw;

public readonly struct PyLong
{
    public readonly nint Handle;

    public PyLong() : this((long)0) { }

    public PyLong(long value)
    {
        if ((Handle = PyNative.PyLong_New(value)) == 0)
        {
            ThrowCreateFailure(value);
        }
    }

    public PyLong(nint hLong)
    {
        if (hLong == 0)
        {
            ThrowInvalidHandle();
        }
        else if (!PyNative.PyLong_CheckType(hLong))
        {
            ThrowCastFailure();
        }

        Handle = hLong;
    }

    public override bool Equals(object obj)
    {
        return ((obj != null) ? ((PyLong)obj == this) : false);
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

    public static bool operator ==(PyLong a, PyLong b)
    {
        return ((long)a == (long)b);
    }

    public static bool operator !=(PyLong a, PyLong b)
    {
        return ((long)a != (long)b);
    }

    public static implicit operator nint(PyLong hLong)
    {
        return hLong.Handle;
    }

    public static explicit operator long(PyLong obj)
    {
        return PyNative.PyLong_AsLong(obj);
    }

    public static explicit operator PyLong(nint hLong)
    {
        return new PyLong(hLong);
    }

    private static void ThrowCreateFailure(long value)
    {
        throw new Exception(string.Format("A new integer object with value: {0} could not be created.", value));
    }

    private static void ThrowInvalidHandle()
    {
        throw new ArgumentNullException("The object handle cannot be 0.");
    }

    private static void ThrowCastFailure()
    {
        throw new InvalidCastException("The object behind the handle is not of integer type.");
    }

    private static void ThrowToStrFailure()
    {
        throw new Exception("The string representation of the integer object could not be retrieved.");
    }
}