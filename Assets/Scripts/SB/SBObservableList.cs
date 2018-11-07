using System.Collections;
using System.Collections.Generic;
using System;

namespace SB.Observables
{
    public class SBObservable<T>
    {
        public event Action<T> dataUpdated;

        T m_data = default(T);

        public T Data
        {
            get
            {
                return m_data;
            }
            set
            {
                var previousValue = m_data;

                //trap null values
                if (value == null)
                {
                    return;
                }

                m_data = value;


                if (m_data != null)
                {
                    dataUpdated(m_data);//you could also make a custom handler, and pass the new value for convenience
                }
            }
        }
    }

    public class ObservableList<T> : IList<T>
    {
        public delegate void ListUpdateHandler(object sender, object updatedValue);

        public Action<T> ItemAdded;
        public Action<T> ItemRemoved;
        public Action ListCleared;

        List<T> m_list = new List<T>();

        #region IList[T] implementation
        public int IndexOf(T value)
        {
            return m_list.IndexOf(value);
        }

        public void Insert(int index, T value)
        {
            m_list.Insert(index, value);
        }

        public void RemoveAt(int index)
        {
            m_list.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return m_list[index];
            }
            set
            {
                m_list[index] = value;
            }
        }
        #endregion

        #region IEnumerable implementation
        public IEnumerator<T> GetEnumerator()
        {
            return m_list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region ICollection[T] implementation
        public void Add(T item)
        {
            if (ItemAdded != null)
            {
                ItemAdded(item);
            }

            m_list.Add(item);
        }

        public void Clear()
        {
            m_list.Clear();

            if (ListCleared != null)
            {
                ListCleared();
            }
        }

        public bool Contains(T item)
        {
            return m_list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (ItemRemoved != null)
            {
                ItemRemoved(item);
            }

            return m_list.Remove(item);
        }

        public int Count
        {
            get
            {
                return m_list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}