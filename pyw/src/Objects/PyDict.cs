namespace CeetemSoft.Pyw;

using System.Collections;

public partial class PyDict : PyObj,
    IEnumerable<KeyValuePair<nint, nint>>, IEnumerable<KeyValuePair<PyValObj, PyValObj>>
{
    public PyDict() : base(CreateDict()) { }

    public PyDict(nint hDict) : base(hDict)
    {
        if (!PyNative.PyDict_CheckType(hDict))
        {
            ThrowCastFailure();
        }
    }

    public IEnumerator<KeyValuePair<nint, nint>> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator<KeyValuePair<PyValObj, PyValObj>> IEnumerable<KeyValuePair<PyValObj, PyValObj>>.GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static nint CreateDict()
    {
        nint hDict = PyNative.PyDict_New();

        if (hDict == 0)
        {
            ThrowCreateFailure();
        }

        return hDict;
    }

    private static void ThrowCreateFailure()
    {
        throw new Exception("A new dictionary object could not be created.");
    }

    private static void ThrowCastFailure()
    {
        throw new InvalidCastException("The object is not a dictionary.");
    }
}