using System;
using System.Collections.Generic;

namespace _03.MinHeap
{
    public class PriorityQueue<T> : MinHeap<T> where T : IComparable<T>
    {

        private Dictionary<T, int> indexes;

        public PriorityQueue()
        {
            this.indexes = new Dictionary<T, int>();
            this.elements = new List<T>();
        }

        public void Enqueue(T element)
        {
            this.elements.Add(element);

            this.indexes.Add(element, this.Count - 1);

            this.HeapifyUp(this.Count - 1);
        }

        public T Dequeue()
        {
            //this.ValidateIfEmpty();

            T result = this.elements[0];

            this.Swap(0, this.Count - 1);

            this.elements.RemoveAt(this.Count - 1);

            this.indexes.Remove(result);

            this.HeapifyDown(0);

            return result;
        }

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[index];

            this.elements[index] = this.elements[parentIndex];

            this.elements[parentIndex] = temp;

            this.indexes[this.elements[index]] = index;
            this.indexes[this.elements[parentIndex]] = parentIndex;
        }

        public void DecreaseKey(T key)
        {
            HeapifyUp(this.indexes[key]);
        }

        public void DecreaseKey(T key, T newKey)
        {
            var oldIndex = this.indexes[key];
            this.elements[oldIndex] = newKey;
            this.indexes.Remove(key);
            this.indexes.Add(newKey, oldIndex);
            this.HeapifyUp(oldIndex);
        }

        private bool IsGreater(int index, int parentIndex)
        {
            return this.elements[index].CompareTo(this.elements[parentIndex]) > 0;
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = GetParentIndex(index);

            while (IsIndexValid(index) && IsGreater(index, parentIndex))
            {
                this.Swap(index, parentIndex);

                index = parentIndex;

                parentIndex = this.GetParentIndex(index);

            }
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private void HeapifyDown(int index)
        {
            var biggerChildIndex = this.GetBiggerChildIndex(index);

            while (IsIndexValid(index) && this.IsGreater(biggerChildIndex, index))
            {
                this.Swap(biggerChildIndex, index);

                index = biggerChildIndex;

                biggerChildIndex = this.GetBiggerChildIndex(index);
            }
        }

        private bool IsIndexValid(int index)
        {
            return index >= 0 && index < this.elements.Count;
        }

        private int GetBiggerChildIndex(int index)
        {
            var firstChildIndex = index * 2 + 1;

            var secondChildIndex = index * 2 + 1;

            if (secondChildIndex < this.elements.Count)
            {
                if (this.IsGreater(firstChildIndex, secondChildIndex))
                {
                    return firstChildIndex;
                }

                return secondChildIndex;
            }
            else if (firstChildIndex < this.elements.Count)
            {
                return firstChildIndex;
            }
            else
            {
                return -1;
            }

        }
    }
}
