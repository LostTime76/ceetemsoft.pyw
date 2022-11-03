namespace CeetemSoft.Pyw;

using System.Collections;
using System.Collections.Generic;

public partial class PyList
{
    private class Enumerator : IEnumerator<PyValueType>, IEnumerator<nint>
    {
        private int  _idx;
        private int  _size;
        private nint _hList;
        private nint _hValue;

        public Enumerator(nint hList)
        {
            _hList = hList;
            Reset();
        }

        public void Dispose() { }

        public void Reset()
        {
            _idx  = 0;
            _size = PyNative.PyList_Size(_hList);
        }

        public bool MoveNext()
        {
            if (_idx == _size)
            {
                _hValue = 0;
                return false;
            }

            _hValue = GetItem(_idx++);
            return true;
        }

        private PyObj GetItem(int idx)
        {
            nint hObj = PyNative.PyList_GetItem(_hList, idx);

            if (hObj == 0)
            {
                ThrowEnumerationFailed(idx);
            }

            return new PyObj(hObj);
        }

        private static void ThrowEnumerationFailed(int idx)
        {
            throw new Exception(string.Format("List enumeration failed on index: {0}.", idx));
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public PyValueType Current
        {
            get { return ((_hValue != 0) ? new PyValueType(_hValue) : null); }
        }

        nint IEnumerator<nint>.Current
        {
            get { return _hValue; }
        }
    }
}