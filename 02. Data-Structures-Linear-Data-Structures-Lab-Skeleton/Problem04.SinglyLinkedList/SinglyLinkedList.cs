namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {

        private class Node
        {
            public T Element { get; set; }

            public Node Next { get; set; }

            public Node(T element, Node node)
            {
                this.Element = element;

                this.Next = node;
            }

            public Node(T element)
            {
                this.Element = element;
            }
        }

        private Node head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node(item);

            if (head == null)
            {               
                this.head = newNode;
            }
            else
            {
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
                var currentNode = newNode;

                this.head = currentNode;                               
            }
            else
            {
                var current = this.head;

                while (current.Next != null)
                {
                    current = current.Next;
                }
                
                current.Next = newNode;
            }
            
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;

            while (current != null)
            {
                yield return current.Element;

                current = current.Next;
            }
        }

        public T GetFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.head.Element;
        }

        public T GetLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var current = this.head;

            while (current.Next != null)
            {               
                current = current.Next;
            }

            return current.Element;
        }

        public T RemoveFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            var oldHead = this.head;

            this.head = oldHead.Next;

            this.Count--;

            return oldHead.Element;
        }

        public T RemoveLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            if (this.head.Next == null)
            {
                var headElement = this.head.Element;

                this.head = null;

                this.Count--;

                return headElement;
            }

            var current = this.head;

            Node newLastNode = default;

            while (current.Next != null)
            {
                newLastNode = current;

                current = current.Next;
            }

            newLastNode.Next = null;

            Count--;

            return current.Element;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}