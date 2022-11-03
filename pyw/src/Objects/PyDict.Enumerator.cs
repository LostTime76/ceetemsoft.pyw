namespace CeetemSoft.Pyw;

using System.Collections;

public partial class PyDict
{
    private class Enumerator : IEnumerator<KeyValuePair<PyObj, PyObj>>, IEnumerator<KeyValuePair<nint, nint>>
    {
        private int  _idx;
        private int  _size;
        private nint _hDict;
        private nint _hKeys;

        private (nint hKey, nint hValue) _kvp;

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
            _size = PyNative.PyList_Size(_hKeys);
        }

        public bool MoveNext()
        {
            if (_idx == _size)
            {
                _kvp = new (0, 0);
                return false;
            }

            _kvp = GetItem(_idx++);
            return true;
        }

        private nint GetKeys()
        {
            nint hKeys = PyNative.PyDict_Keys(_hDict);
            
            if (hKeys == 0)
            {
                ThrowEnumerationFailed();
            }

            return hKeys;
        }

        private (nint, nint) GetItem(int idx)
        {
            nint hKey   = PyNative.PyList_GetItem(_hKeys, idx);
            nint hValue = PyNative.PyDict_GetItem(_hDict, hKey);

            if ((hKey == 0) || (hValue == 0))
            {
                ThrowEnumerationFailed(idx);
            }

            return (hKey, hValue);
        }

        private static void ThrowEnumerationFailed()
        {
            throw new Exception("The dictionary enumeration failed.");
        }

        private static void ThrowEnumerationFailed(int idx)
        {
            throw new Exception(string.Format("The dictionary enumeration failed at key index: {0}.", idx));
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public KeyValuePair<PyObj, PyObj> Current
        {
            get { return new KeyValuePair<PyObj, PyObj>(new PyObj(_kvp.hKey), new PyObj(_kvp.hValue)); }
        }

        KeyValuePair<nint, nint> IEnumerator<KeyValuePair<nint, nint>>.Current
        {
            get { return new KeyValuePair<nint, nint>(_kvp.hKey, _kvp.hValue); }
        }
    }
}