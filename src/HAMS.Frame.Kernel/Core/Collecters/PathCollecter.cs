using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Frame.Kernel.Core
{
    public class PathCollecter : IDictionary<Enum, BaseKind>
    {
        Dictionary<Enum, BaseKind> Paths = new Dictionary<Enum, BaseKind>();

        public BaseKind this[Enum key] { get => ((IDictionary<Enum, BaseKind>)Paths)[key]; set => ((IDictionary<Enum, BaseKind>)Paths)[key] = value; }

        public ICollection<Enum> Keys => ((IDictionary<Enum, BaseKind>)Paths).Keys;

        public ICollection<BaseKind> Values => ((IDictionary<Enum, BaseKind>)Paths).Values;

        public int Count => ((ICollection<KeyValuePair<Enum, BaseKind>>)Paths).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<Enum, BaseKind>>)Paths).IsReadOnly;

        public void Add(Enum key, BaseKind value)
        {
            ((IDictionary<Enum, BaseKind>)Paths).Add(key, value);
        }

        public void Add(KeyValuePair<Enum, BaseKind> item)
        {
            ((ICollection<KeyValuePair<Enum, BaseKind>>)Paths).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<Enum, BaseKind>>)Paths).Clear();
        }

        public bool Contains(KeyValuePair<Enum, BaseKind> item)
        {
            return ((ICollection<KeyValuePair<Enum, BaseKind>>)Paths).Contains(item);
        }

        public bool ContainsKey(Enum key)
        {
            return ((IDictionary<Enum, BaseKind>)Paths).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Enum, BaseKind>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<Enum, BaseKind>>)Paths).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Enum, BaseKind>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<Enum, BaseKind>>)Paths).GetEnumerator();
        }

        public bool Remove(Enum key)
        {
            return ((IDictionary<Enum, BaseKind>)Paths).Remove(key);
        }

        public bool Remove(KeyValuePair<Enum, BaseKind> item)
        {
            return ((ICollection<KeyValuePair<Enum, BaseKind>>)Paths).Remove(item);
        }

        public bool TryGetValue(Enum key, out BaseKind value)
        {
            return ((IDictionary<Enum, BaseKind>)Paths).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Paths).GetEnumerator();
        }
    }
}
