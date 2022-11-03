namespace CeetemSoft.Pyw;

public readonly struct PyStr
{
    public readonly nint Handle;

    public PyStr() : this(string.Empty) { }

    public PyStr(string str)
    {
        if (str == null)
        {
            ThrowInvalidStr();
        }
        if ((Handle = PyNative.PyUnicode_New(str)) == 0)
        {
            ThrowCreateFailure(str);
        }
    }

    public PyStr(nint hStr)
    {
        if (hStr == 0)
        {
            ThrowInvalidHandle();
        }
        else if (!PyNative.PyUnicode_CheckType(hStr))
        {
            ThrowCastFailure();
        }

        Handle = hStr;
    }

    public override bool Equals(object obj)
    {
        return ((obj != null) ? ((PyStr)obj == this) : false);
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

    public static bool operator ==(PyStr a, PyStr b)
    {
        return ((string)a == (string)b);
    }

    public static bool operator !=(PyStr a, PyStr b)
    {
        return ((string)a != (string)b);
    }

    public static implicit operator nint(PyStr str)
    {
        return str.Handle;
    }

    public static implicit operator PyValueType(PyStr str)
    {
        return new PyValueType(str.Handle);
    }

    public static explicit operator string(PyStr str)
    {
        return str.ToString();
    }

    public static explicit operator PyStr(nint hStr)
    {
        return new PyStr(hStr);
    }

    public static explicit operator PyStr(PyObj obj)
    {
        return new PyStr(obj.Handle);
    }

    private static void ThrowInvalidHandle()
    {
        throw new ArgumentNullException("The object handle cannot be 0.");
    }

    private static void ThrowInvalidStr()
    {
        throw new ArgumentException("The input string cannot be null.");
    }

    private static void ThrowCreateFailure(string str)
    {
        throw new Exception(string.Format("A new string object with value: {0} could not be created.", str));
    }

    private static void ThrowCastFailure()
    {
        throw new InvalidCastException("The object behind the handle is not of string type.");
    }

    private static void ThrowToStrFailure()
    {
        throw new Exception("The string representation of the string object could not be retrieved.");
    }
}