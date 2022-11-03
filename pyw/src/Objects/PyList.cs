namespace CeetemSoft.Pyw;

using System.Collections;

public partial class PyList : PyObj, IEnumerable<nint>, IEnumerable<PyValObj>
{
    public PyList() : base(CreateList()) { }

    public PyList(nint hList) : base(hList)
    {
        if (!PyNative.PyList_CheckType(hList))
        {
            ThrowCastFailure();
        }
    }

    public void Clear()
    {
        if (!PyNative.PyList_SetSlice(Handle, 0, Size, 0))
        {
            ThrowClearFailure();
        }
    }

    public void Append(nint hObj)
    {
        if (!PyNative.PyList_Append(Handle, hObj))
        {
            ThrowAppendFailure();
        }
    }

    public void Insert(nint hObj, int idx)
    {
        if (!PyNative.PyList_Insert(Handle, idx, hObj))
        {
            ThrowInsertFailure(idx);
        }
    }

    public nint Slice(int start)
    {
        return Slice(start, (Size - start));
    }

    public nint Slice(int start, int len)
    {
        nint hSlice = PyNative.PyList_GetSlice(Handle, start, (start + len));
        
        if (hSlice == 0)
        {
            ThrowSliceFailure(start, len);
        }

        return hSlice;
    }

    public void SetSlice(int start, nint hObj)
    {
        SetSlice(start, (Size - start), hObj);
    }

    public void SetSlice(int start, int len, nint hObj)
    {
        if (hObj == 0)
        {
            ThrowInvalidHandle();
        }
        else if (!PyNative.PyList_SetSlice(Handle, start, (start + len), hObj))
        {
            ThrowSetSliceFailure(start, len);
        }
    }

    public void RemoveAt(int idx)
    {
        Remove(idx, (idx + 1));
    }

    public void Remove(int start)
    {
        Remove(start, (Size - start));
    }

    public void Remove(int start, int len)
    {
        if (!PyNative.PyList_SetSlice(Handle, start, (start + len), 0))
        {
            ThrowRemoveFailure(start, len);
        }
    }

    public IEnumerator<nint> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator<PyValObj> IEnumerable<PyValObj>.GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static nint CreateList()
    {
        nint hList = PyNative.PyList_New(0);

        if (hList == 0)
        {
            ThrowCreateFailure();
        }

        return hList;
    }

    private static void ThrowInvalidHandle()
    {
        throw new ArgumentNullException("The handle value is invalid.");
    }

    private static void ThrowCreateFailure()
    {
        throw new Exception("A new list object could not be created.");
    }

    private static void ThrowCastFailure()
    {
        throw new InvalidCastException("The object is not a list.");
    }

    private static void ThrowClearFailure()
    {
        throw new Exception("The list could not be cleared.");
    }

    private static void ThrowAppendFailure()
    {
        throw new Exception("The item could not be appended to the list.");
    }

    private static void ThrowInsertFailure(int idx)
    {
        throw new Exception(string.Format("The item could not be inserted into the list at index: {0}", idx));
    }

    private static void ThrowSliceFailure(int start, int len)
    {
        throw new Exception(string.Format("The slice: [{0}:{1}] could not be obtained from the list",
            start, (start + len)));
    }

    private static void ThrowSetSliceFailure(int start, int len)
    {
        throw new Exception(string.Format("The slice: [{0}:{1}] within the list could not be set.",
            start, (start + len)));
    }

    private static void ThrowRemoveFailure(int start, int len)
    {
        throw new Exception(string.Format("The slice: [{0}:{1}] could not be removed from the list.",
            start, (start + len)));
    }

    private static void ThrowGetItemFailure(int idx)
    {
        throw new Exception(string.Format("The item at index: {0} could not be retrieved from the list", idx));
    }

    private static void ThrowSetItemFailure(int idx)
    {
        throw new Exception(string.Format("The item at index: {0} could not be set within the list.", idx));
    }

    public int Size
    {
        get { return PyNative.PyList_Size(Handle); }
    }

    public nint this[int idx]
    {
        get
        {
            nint hObj = PyNative.PyList_GetItem(Handle, idx);

            if (hObj == 0)
            {
                ThrowGetItemFailure(idx);
            }

            return hObj;
        }

        set
        {
            if (value == 0)
            {
                ThrowInvalidHandle();
            }
            else if (!PyNative.PyList_SetItem(Handle, idx, value))
            {
                ThrowSetItemFailure(idx);
            }
        }
    }
}