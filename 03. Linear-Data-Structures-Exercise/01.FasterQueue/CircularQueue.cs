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
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var element = this.elements[startIndex];
            
            startIndex = (this.startIndex + 1) % this.elements.Length;

            elements = CopyElements(new T[this.Count]);

            Count--;

            return element;
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
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.elements[startIndex];
        }

        public T[] ToArray()
        {
            return this.CopyElements(new T[this.Count]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Grow()
        {
            this.elements = this.CopyElements(new T[this.elements.Length * 2]);

            this.startIndex = 0;

            this.endIndex = this.Count;
        }

        private T[] CopyElements(T[] resultArr)
        {             
           
            for (int currentIndex = 0; currentIndex < this.Count; currentIndex++)
            {
                resultArr[currentIndex] = this.elements[(this.startIndex + currentIndex) % this.elements.Length];
            }

            return resultArr;
        }
    }

}
