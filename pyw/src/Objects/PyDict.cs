namespace CeetemSoft.Pyw;

using System.Collections;

public partial class PyDict : PyObj, 
    IEnumerable<KeyValuePair<nint, nint>>, IEnumerable<KeyValuePair<PyObj, PyObj>>
{
    public PyDict() : base(CreateDict()) { }

    public PyDict(nint hDict) : base(hDict)
    {
        if (!PyNative.PyDict_CheckType(hDict))
        {
            ThrowCastFailure();
        }
    }

    public void Clear()
    {
        PyNative.PyDict_Clear(Handle);
    }

    public void DelItem(nint hKey)
    {
        if (hKey == 0)
        {
            ThrowInvalidKey();
        }
        else if (!PyNative.PyDict_DelItem(Handle, hKey))
        {
            ThrowDelItemFailure();
        }
    }

    public void DelItem(string key)
    {
        if (key == null)
        {
            ThrowInvalidKey();
        }
        else if (!PyNative.PyDict_DelItem(Handle, key))
        {
            ThrowDelItemFailure(key);
        }
    }

    public bool ContainsKey(nint hKey)
    {
        if (hKey == 0)
        {
            ThrowInvalidKey();
        }

        return PyNative.PyDict_Contains(Handle, hKey);
    }

    public IEnumerator<KeyValuePair<nint, nint>> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator<KeyValuePair<PyObj, PyObj>> IEnumerable<KeyValuePair<PyObj, PyObj>>.GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<PyObj, PyObj>>)this).GetEnumerator();
    }

    public static explicit operator PyDict(nint hDict)
    {
        return new PyDict(hDict);
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
        throw new Exception("A new dictionary object with could not be created.");
    }

    private static void ThrowCastFailure()
    {
        throw new InvalidCastException("The object behind the handle is not of dictionary type.");
    }

    private static void ThrowDelItemFailure()
    {
        throw new Exception("The item could not be deleted from the dictionary.");
    }

    private static void ThrowDelItemFailure(string key)
    {
        throw new Exception(string.Format("The item with key: {0} could not be deleted from the dictionary", key));
    }

    private static void ThrowGetKeysFailure()
    {
        throw new Exception("The keys could not be obtained from the dictionary.");
    }

    private static void ThrowGetValuesFailure()
    {
        throw new Exception("The values could not be obtained from the dictionary.");
    }

    private static void ThrowInvalidKey()
    {
        throw new ArgumentException("The key is not valid.");
    }

    private static void ThrowGetItemFailure()
    {
        throw new Exception("The item could not be retrieved from the dictionary.");
    }

    private static void ThrowGetItemFailure(string key)
    {
        throw new Exception(string.Format("The item with key: {0} could not be obtained from the dictionary.", key));
    }

    private static void ThrowSetItemFailure()
    {
        throw new Exception("The item could not be set within the dictionary.");
    }

    private static void ThrowSetItemFailure(string key)
    {
        throw new Exception(string.Format("The item with key: {0} could not be set within the dictionary.", key));
    }

    public int Size
    {
        get { return PyNative.PyDict_Size(Handle); }
    }

    public nint Keys
    {
        get
        {
            nint hKeys = PyNative.PyDict_Keys(Handle);

            if (hKeys == 0)
            {
                ThrowGetKeysFailure();
            }

            return hKeys;
        }
    }

    public nint Values
    {
        get
        {
            nint hValues = PyNative.PyDict_Values(Handle);

            if (hValues == 0)
            {
                ThrowGetValuesFailure();
            }

            return hValues;
        }
    }

    public nint this[string key]
    {
        get
        {
            if (key == null)
            {
                ThrowInvalidKey();
            }

            nint hValue = PyNative.PyDict_GetItem(Handle, key);

            if (hValue == 0)
            {
                ThrowGetItemFailure(key);
            }

            return hValue;
        }

        set
        {
            if (key == null)
            {
                ThrowInvalidKey();
            }
            else if (!PyNative.PyDict_SetItem(Handle, key, value))
            {
                ThrowSetItemFailure(key);
            }
        }
    }

    public nint this[nint hKey]
    {
        get
        {
            nint hValue = PyNative.PyDict_GetItem(Handle, hKey);

            if (hValue == 0)
            {
                ThrowGetItemFailure();
            }

            return hValue;
        }

        set
        {
            if (!PyNative.PyDict_SetItem(Handle, hKey, value))
            {
                ThrowSetItemFailure();
            }
        }
    }
}