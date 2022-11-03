namespace CeetemSoft.Pyw;

public readonly struct PyValue
{
    public readonly nint Handle;

    public PyValue() : this(0L) { }

    public PyValue(bool value) : this(FromBool(value)) { }

    public PyValue(int value) : this(FromLong(value)) { }

    public PyValue(long value) : this(FromLong(value)) { }

    public PyValue(ulong value) : this(FromLong(value)) { }

    public PyValue(double value) : this(FromFloat(value)) { }

    public PyValue(string str) : this(FromStr(str)) { }

    public PyValue(nint hValue)
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

    private static PyValue FromBool(bool value)
    {
        nint hBool = PyNative.PyBool_New(value);

        if (hBool == 0)
        {
            ThrowFromBoolFailure(value);
        }

        return new PyValue(hBool);
    }

    private static PyValue FromLong(long value)
    {
        nint hLong = PyNative.PyLong_New(value);

        if (hLong == 0)
        {
            ThrowFromLongFailure(value);
        }

        return new PyValue(hLong);
    }

    private static PyValue FromLong(ulong value)
    {
        nint hLong = PyNative.PyLong_New(value);

        if (hLong == 0)
        {
            ThrowFromLongFailure(value);
        }

        return new PyValue(hLong);
    }

    private static PyValue FromFloat(double value)
    {
        nint hFloat = PyNative.PyFloat_New(value);

        if (hFloat == 0)
        {
            ThrowFromFloatFailure(value);
        }

        return new PyValue(hFloat);
    }

    private static PyValue FromStr(string str)
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

        return new PyValue(hStr);
    }

    public static implicit operator nint(PyValue obj)
    {
        return obj.Handle;
    }

    public static explicit operator PyValue(nint hObj)
    {
        return new PyValue(hObj);
    }

    public static explicit operator bool(PyValue obj)
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

    public static explicit operator long(PyValue obj)
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

    public static explicit operator float(PyValue obj)
    {
        return (float)((double)obj);
    }

    public static explicit operator double(PyValue obj)
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

    public static explicit operator string(PyValue obj)
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