using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace CeetemSoft.Pyw;

public readonly struct PyDict : IDictionary<PyObj, PyObj>
{
    public readonly nint pObj;

    public PyDict(nint pObj)
    {
        this.pObj = pObj;
    }

    public void Add(PyObj key, PyObj value)
    {
        throw new NotImplementedException();
    }

    public void Add(KeyValuePair<PyObj, PyObj> item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<PyObj, PyObj> item)
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(PyObj key)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<PyObj, PyObj>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<PyObj, PyObj>> GetEnumerator()
    {
        foreach (PyObj key in Keys)
        {
            yield return new KeyValuePair<PyObj, PyObj>(key, this[key]);
        }
    }

    public bool Remove(PyObj key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<PyObj, PyObj> item)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(PyObj key, [MaybeNullWhen(false)] out PyObj value)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return PyNative.PyUnicode_AsUtf8(PyNative.PyObj_Str(this));
    }

    public static implicit operator nint(PyDict obj)
    {
        return obj.pObj;
    }

    public static implicit operator PyObj(PyDict obj)
    {
        return new PyObj(obj);
    }

    public static explicit operator PyDict(PyObj obj)
    {
        return new PyDict(obj);
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public int Count
    {
        get { return PyNative.PyDict_Count(this); }
    }

    public PyObj this[string key]
    {
        get { return PyNative.PyObj_GetAttr(this, key); }
        set { PyNative.PyObj_SetAttr(this, key, value); }
    }

    public PyObj this[PyObj keyObj]
    {
        get { return PyNative.PyDict_GetElt(this, keyObj); }
        set { PyNative.PyDict_SetElt(this, keyObj, value); }
    }

    public ICollection<PyObj> Keys
    {
        get { return new PyList(PyNative.PyDict_GetKeys(this)); }
    }

    public ICollection<PyObj> Values
    {
        get { return new PyList(PyNative.PyDict_GetValues(this)); }
    }
}