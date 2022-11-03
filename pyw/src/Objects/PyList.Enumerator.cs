namespace CeetemSoft.Pyw;

using System.Collections;

public partial class PyList
{
    private class Enumerator : IEnumerator<nint>, IEnumerator<PyValObj>
    {
        private int  _idx;
        private int  _size;
        private nint _value;

        private readonly nint _hList;

        public Enumerator(nint hList)
        {
            _hList = hList;
            Reset();
        }

        public void Dispose() { }

        public void Reset()
        {
            _idx   = 0;
            _value = 0;
            _size  = PyNative.PyList_Size(_hList);
        }

        public bool MoveNext()
        {
            if (_idx == _size)
            {
                return false;
            }

            _value = GetItem(_idx++);
            return true;
        }

        private nint GetItem(int idx)
        {
            nint hItem = PyNative.PyList_GetItem(_hList, idx);

            if (hItem == 0)
            {
                ThrowEnumerationFailure(idx);
            }

            return hItem;
        }

        private static void ThrowEnumerationFailure(int idx)
        {
            throw new Exception(string.Format("The list enumeration failed at index: {0}", idx));
        }

        public nint Current
        {
            get { return _value; }
        }

        PyValObj IEnumerator<PyValObj>.Current
        {
            get { return new PyValObj(_value); }
        }

        object IEnumerator.Current
        {
            get { return ((IEnumerator<PyValObj>)this).Current; }
        }
    }
}