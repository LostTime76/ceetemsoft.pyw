namespace CeetemSoft.Pyw;

using System.Collections;

public partial class PyDict
{
    private class Enumerator : IEnumerator<KeyValuePair<nint, nint>>,
        IEnumerator<KeyValuePair<PyValObj, PyValObj>>
    {
        private int _idx;
        private int _size;

        private (nint hKey, nint hValue) _kvp;

        private readonly nint _hDict;
        private readonly nint _hKeys;

        public Enumerator(nint hDict)
        {
            _hDict = hDict;
            _hKeys = GetKeys();
            Reset();
        }

        public void Dispose() { }

        public void Reset()
        {
            _idx  = 0;
            _kvp  = (0, 0);
            _size = PyNative.PyList_Size(_hKeys);
        }

        public bool MoveNext()
        {
            if (_idx == _size)
            {
                return false;
            }

            _kvp = GetValue(_idx++);
            return true;
        }

        private nint GetKeys()
        {
            nint hKeys = PyNative.PyDict_Keys(_hDict);

            if (hKeys == 0)
            {
                ThrowEnumerationFailure();
            }

            return hKeys;
        }

        private (nint, nint) GetValue(int idx)
        {
            nint hKey   = PyNative.PyList_GetItem(_hKeys, idx);
            nint hValue = PyNative.PyDict_GetItem(_hDict, hKey);

            if ((hKey == 0) || (hValue == 0))
            {
                ThrowEnumerationFailure(idx);
            }

            return (hKey, hValue);
        }

        private static void ThrowEnumerationFailure()
        {
            throw new Exception("The dictionary enumeration failed.");
        }

        private static void ThrowEnumerationFailure(int idx)
        {
            throw new Exception(string.Format("The dictionary enumeration failed at key index: {0}", idx));
        }

        public KeyValuePair<nint, nint> Current
        {
            get { return new KeyValuePair<nint, nint>(_kvp.hKey, _kvp.hValue); }
        }

        KeyValuePair<PyValObj, PyValObj> IEnumerator<KeyValuePair<PyValObj, PyValObj>>.Current
        {
            get { return new KeyValuePair<PyValObj, PyValObj>(new PyValObj(_kvp.hKey), new PyValObj(_kvp.hValue)); }
        }
        
        object IEnumerator.Current
        {
            get { return ((IEnumerator<KeyValuePair<PyValObj, PyValObj>>)this).Current; }
        }
    }
}