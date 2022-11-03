namespace CeetemSoft.Pyw;

public readonly struct PyValObj
{
    public readonly nint Handle;

    public PyValObj() : this(0L) { }

    public PyValObj(bool value) : this(FromBool(value)) { }

    public PyValObj(int value) : this(FromLong(value)) { }

    public PyValObj(long value) : this(FromLong(value)) { }

    public PyValObj(ulong value) : this(FromLong(value)) { }

    public PyValObj(double value) : this(FromFloat(value)) { }

    public PyValObj(string str) : this(FromStr(str)) { }

    public PyValObj(nint hValue)
    {
        if (hValue == 0)
        {
            ThrowInvalidHandle();
        }

        Handle = hValue;
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

    private static PyValObj FromBool(bool value)
    {
        nint hBool = PyNative.PyBool_New(value);

        if (hBool == 0)
        {
            ThrowFromBoolFailure(value);
        }

        return new PyValObj(hBool);
    }

    private static PyValObj FromLong(long value)
    {
        nint hLong = PyNative.PyLong_New(value);

        if (hLong == 0)
        {
            ThrowFromLongFailure(value);
        }

        return new PyValObj(hLong);
    }

    private static PyValObj FromLong(ulong value)
    {
        nint hLong = PyNative.PyLong_New(value);

        if (hLong == 0)
        {
            ThrowFromLongFailure(value);
        }

        return new PyValObj(hLong);
    }

    private static PyValObj FromFloat(double value)
    {
        nint hFloat = PyNative.PyFloat_New(value);

        if (hFloat == 0)
        {
            ThrowFromFloatFailure(value);
        }

        return new PyValObj(hFloat);
    }

    private static PyValObj FromStr(string str)
    {
        if (str == null)
        {
            ThrowInvalidStr();
        }

        nint hStr = PyNative.PyUnicode_New(str);

        if (hStr == 0)
        {
            ThrowFromStrFailure(str);
        }

        return new PyValObj(hStr);
    }

    public static implicit operator nint(PyValObj obj)
    {
        return obj.Handle;
    }

    public static implicit operator PyValObj(PyObj obj)
    {
        return new PyValObj(obj.Handle);
    }

    public static explicit operator PyValObj(nint hObj)
    {
        return new PyValObj(hObj);
    }

    public static explicit operator bool(PyValObj obj)
    {
        nint hObj = obj.Handle;

        if (PyNative.PyLong_CheckType(hObj))
        {
            return PyNative.PyBool_AsBool(hObj);
        }

        nint hConv = PyNative.PyLong_Conv(hObj);
        bool value = PyNative.PyBool_AsBool(hConv);
        PyNative.PyObj_DecRef(hConv);

        if (hConv == 0)
        {
            ThrowToBoolFailure();
        }

        return value;
    }

    public static explicit operator long(PyValObj obj)
    {
        nint hObj = obj.Handle;

        if (PyNative.PyLong_CheckType(hObj))
        {
            return PyNative.PyLong_AsLong(hObj);
        }

        nint hConv = PyNative.PyLong_Conv(hObj);
        long value = PyNative.PyLong_AsLong(hConv);
        PyNative.PyObj_DecRef(hConv);

        if (hConv == 0)
        {
            ThrowToLongFailure();
        }

        return value;
    }

    public static explicit operator float(PyValObj obj)
    {
        return (float)((double)obj);
    }

    public static explicit operator double(PyValObj obj)
    {
        nint hObj = obj.Handle;

        if (PyNative.PyFloat_CheckType(hObj))
        {
            return PyNative.PyFloat_AsDouble(hObj);
        }

        nint   hConv = PyNative.PyFloat_Conv(hObj);
        double value = PyNative.PyFloat_AsDouble(hConv);
        PyNative.PyObj_DecRef(hConv);

        if (hConv == 0)
        {
            ThrowToDoubleFailure();
        }

        return value;
    }

    public static explicit operator string(PyValObj obj)
    {
        return obj.ToString();
    }

    private static void ThrowInvalidHandle()
    {
        throw new ArgumentNullException("The handle value is not valid.");
    }

    private static void ThrowInvalidStr()
    {
        throw new ArgumentNullException("The string value cannot be null.");
    }

    private static void ThrowToStrFailure()
    {
        throw new Exception("The string representation of the value could not be obtained.");
    }

    private static void ThrowFromBoolFailure(bool value)
    {
        throw new Exception(string.Format("A bool object with value: {0} could not be created.", value));
    }

    private static void ThrowFromLongFailure(long value)
    {
        throw new Exception(string.Format("A long object with value: {0} could not be created.", value));
    }

    private static void ThrowFromLongFailure(ulong value)
    {
        throw new Exception(string.Format("A long object with value: {0} could not be created.", value));
    }

    private static void ThrowFromFloatFailure(double value)
    {
        throw new Exception(string.Format("A float object with value: {0} could not be created.", value));
    }

    private static void ThrowFromStrFailure(string str)
    {
        throw new Exception(string.Format("A string object with value: {0} could not be created.", str));
    }

    private static void ThrowToBoolFailure()
    {
        throw new Exception("The object could not be converted into a boolean value.");
    }

    private static void ThrowToLongFailure()
    {
        throw new Exception("The object could not be converted into a long value.");
    }

    private static void ThrowToFloatFailure()
    {
        throw new Exception("The object could not be converted into a float value.");
    }

    private static void ThrowToDoubleFailure()
    {
        throw new Exception("The object could not be converted into a double value.");
    }
}