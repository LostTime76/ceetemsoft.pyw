namespace CeetemSoft.Pyw;

public readonly struct PyBool
{
    public readonly nint Handle;

    public PyBool() : this(false) { }

    public PyBool(bool value)
    {
        if ((Handle = PyNative.PyBool_New(value)) == 0)
        {
            ThrowCreateFailure(value);
        }
    }

    public PyBool(nint hBool)
    {
        if (hBool == 0)
        {
            ThrowInvalidHandle();
        }
        else if (!PyNative.PyBool_CheckType(hBool))
        {
            ThrowCastFailure();
        }

        Handle = hBool;
    }

    public override bool Equals(object obj)
    {
        return ((obj != null) ? ((PyBool)obj == this) : false);
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

    public static bool operator ==(PyBool a, PyBool b)
    {
        return ((bool)a == (bool)b);
    }

    public static bool operator !=(PyBool a, PyBool b)
    {
        return ((bool)a != (bool)b);
    }

    public static implicit operator nint(PyBool hBool)
    {
        return hBool.Handle;
    }

    public static implicit operator PyValueType(PyBool obj)
    {
        return new PyValueType(obj.Handle);
    }

    public static explicit operator bool(PyBool obj)
    {
        return PyNative.PyBool_AsBool(obj);
    }

    public static explicit operator PyBool(nint hBool)
    {
        return new PyBool(hBool);
    }

    public static explicit operator PyBool(PyObj obj)
    {
        return new PyBool(obj.Handle);
    }

    private static void ThrowCreateFailure(bool value)
    {
        throw new Exception(string.Format("A new boolean object with value: {0} could not be created.", value));
    }

    private static void ThrowInvalidHandle()
    {
        throw new ArgumentNullException("The object handle cannot be 0.");
    }

    private static void ThrowCastFailure()
    {
        throw new InvalidCastException("The object behind the handle is not of boolean type.");
    }

    private static void ThrowToStrFailure()
    {
        throw new Exception("The string representation of the boolean object could not be retrieved.");
    }
}