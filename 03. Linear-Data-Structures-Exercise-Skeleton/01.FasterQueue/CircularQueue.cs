namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        private T[] elements;
        private int startIndex;
        private int endIndex;

        public CircularQueue(int capacity = 4)
        {
            this.elements = new T[capacity];
        }

        public int Count { get; private set; }

        public T Dequeue()
        {
            throw new NotImplementedException();
        }

        public void Enqueue(T item)
        {
            if (this.Count >= this.elements.Length)
            {
                this.Grow();
            }

            this.elements[this.endIndex] = item;

            this.endIndex = (this.endIndex + 1) % this.elements.Length;

            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int currentIndex = 0; currentIndex < Count; currentIndex++)
            {
                var index = (startIndex + currentIndex) % this.elements.Length;

                yield return this.elements[index];
            }
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Grow()
        {
            this.elements = this.CopyElements();

            this.startIndex = 0;

            this.endIndex = this.Count;
        }

        private T[] CopyElements()
        {
            var newArr = new T[this.elements.Length * 2];

            var originalStartIndex = this.startIndex;

            for (int i = 0; i < this.Count; i++)
            {
                newArr[i] = this.elements[originalStartIndex];

                originalStartIndex = (originalStartIndex + 1) % this.elements.Length;
            }
        }
    }

}
