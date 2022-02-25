namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Value { get; set; }

            public Node Next { get; set; }
            public Node Previous { get; set; }
           
            public Node(T value)
            {
                this.Value = value;
            }
        }

        private Node tail;
        private Node head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node(item);

            if (this.head == null)
            {
                this.head = this.tail = newNode;                
            }
            else
            {
                this.head.Previous = newNode;

                newNode.Next = this.head;

                this.head = newNode;
            }

            Count++;

        }

        public void AddLast(T item)
        {
            var newNode = new Node(item);

            if (this.head == null)
            {
                this.head = this.tail = newNode;
            }
            else
            {                

                this.tail.Next = newNode;

                newNode.Previous = this.tail;

                this.tail = newNode;               
            }

            Count++;
        }

        public T GetFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.head.Value;
        }

        public T GetLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.tail.Value;
        }

        public T RemoveFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var currentHead = this.head;

            if (this.head.Next == null)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.head = this.head.Next;

                this.head.Previous = null;                 
            }

            Count--;

            return currentHead.Value;
        }

        public T RemoveLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }           

            var current = this.tail;

            if (this.head.Next == null)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.tail = this.tail.Previous;

                this.tail.Next = null;
            }
                        
            Count--;

            return current.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;

            while (current.Next != null)
            {
                yield return current.Value;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();        
    }
}