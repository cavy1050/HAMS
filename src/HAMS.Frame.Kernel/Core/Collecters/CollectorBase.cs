using System.Collections;
using System.Collections.Generic;

namespace HAMS.Frame.Kernel.Core
{
    internal abstract class CollectorBase<T> : ICollection<T>
    {
        T[] t = default;

        public int Count => ((ICollection<T>)t).Count;

        public bool IsReadOnly => ((ICollection<T>)t).IsReadOnly;

        public void Add(T item)
        {
            ((ICollection<T>)t).Add(item);
        }

        public void Clear()
        {
            ((ICollection<T>)t).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)t).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)t).CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)t).GetEnumerator();
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)t).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return t.GetEnumerator();
        }
    }
}
