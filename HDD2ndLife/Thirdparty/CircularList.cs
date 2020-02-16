using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace

namespace Clifton.Collections.Generic
{
    /// <summary>
    /// Taken from https://www.codeproject.com/Articles/17858/A-Circular-List
    /// This article, along with any associated source code and files, is licensed under The Code Project Open License (CPOL)
    /// https://www.codeproject.com/info/cpol10.aspx
    /// </summary>
    /// <remarks>
    /// Code has been subjected to ReSharper
    /// </remarks>
    public class CircularList<T> : IEnumerable<T>, IEnumerator<T>
    {
        private readonly T[] items;
        private int idx;
        private bool loaded;
        private int enumIdx;

        /// <summary>
        /// Constructor that initializes the list with the 
        /// required number of items.
        /// </summary>
        public CircularList(int numItems)
        {
            if (numItems <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numItems), @"numItems can't be negative or 0.");
            }

            items = new T[numItems];
            idx = 0;
            loaded = false;
            enumIdx = -1;
        }

        /// <summary>
        /// Gets/sets the item value at the current index.
        /// </summary>
        public T Value
        {
            get => items[idx];
            set => items[idx] = value;
        }

        /// <summary>
        /// Returns the count of the number of loaded items, up to
        /// and including the total number of items in the collection.
        /// </summary>
        public int Count => loaded ? items.Length : idx;

        /// <summary>
        /// Returns the length of the items array.
        /// </summary>
        public int Length => items.Length;

        /// <summary>
        /// Gets/sets the value at the specified index.
        /// </summary>
        public T this[int index]
        {
            get
            {
                RangeCheck(index);
                return items[index];
            }
            set
            {
                RangeCheck(index);
                items[index] = value;
            }
        }

        /// <summary>
        /// Advances to the next item or wraps to the first item.
        /// </summary>
        public void Next()
        {
            if (++idx == items.Length)
            {
                idx = 0;
                loaded = true;
            }
        }

        /// <summary>
        /// Clears the list, resetting the current index to the 
        /// beginning of the list and flagging the collection as unloaded.
        /// </summary>
        public void Clear()
        {
            idx = 0;
            items.Initialize();
            loaded = false;
        }

        /// <summary>
        /// Sets all items in the list to the specified value, resets
        /// the current index to the beginning of the list and flags the
        /// collection as loaded.
        /// </summary>
        public void SetAll(T val)
        {
            idx = 0;
            loaded = true;

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = val;
            }
        }

        /// <summary>
        /// Internal indexer range check helper. Throws
        /// ArgumentOutOfRange exception if the index is not valid.
        /// </summary>
        protected void RangeCheck(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), @"Indexer cannot be less than 0.");
            }

            if (index >= items.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), @"Indexer cannot be greater than or equal to the number of items in the collection.");
            }

        }

        // Interface implementations:

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public T Current => this[enumIdx];

        public void Dispose()
        {
        }

        object IEnumerator.Current => this[enumIdx];

        public bool MoveNext()
        {
            ++enumIdx;
            bool ret = enumIdx < Count;

            if (!ret)
            {
                Reset();
            }

            return ret;
        }

        public void Reset()
        {
            enumIdx = -1;
        }
    }
}