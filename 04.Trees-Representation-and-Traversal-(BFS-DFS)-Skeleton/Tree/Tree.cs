namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private T Value;
        private List<Tree<T>> children;        
        private Tree<T> parent;

        public Tree(T value)
        {
            this.Value = value;

            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.parent = this;
                this.children.Add(child);
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var parentNode = this.FindNodeWithBfs(parentKey);

            if (parentNode != null)
            {
                parentNode.children.Add(child);

                child.parent = parentNode;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public IEnumerable<T> OrderBfs()
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();

                result.Add(subtree.Value);

                foreach (var child in subtree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            var list = new List<T>();

            this.Dfs(this, list);

            return list;
        }

        public void RemoveNode(T nodeKey)
        {
            var nodeToBeDeleted = this.FindNodeWithBfs(nodeKey);

            if (nodeToBeDeleted is null)
            {
                throw new ArgumentNullException();
            }
            var parentNode = nodeToBeDeleted.parent;

            if (parentNode is null)
            {
                throw new ArgumentException();
            }

            parentNode.children = parentNode.children.Where(x => !x.Value.Equals(nodeKey)).ToList();

            nodeToBeDeleted.parent = null;

        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = FindNodeWithBfs(firstKey);

            var secondNode = FindNodeWithBfs(secondKey);

            if (firstNode is null || secondNode is null)
            {
                throw new ArgumentNullException();
            }

            var firstParent = firstNode.parent;
            var secondParent = secondNode.parent;

            if (firstParent is null || secondParent is null)
            {
                throw new ArgumentException();
            }

            var indexOfFirstChild = firstNode.children.IndexOf(firstNode);
            var indexOfSecondChild = secondNode.children.IndexOf(secondNode);

            firstParent.children[indexOfFirstChild] = secondNode;
            secondNode.parent = firstParent;

            secondParent.children[indexOfSecondChild] = firstNode;
            firstNode.parent = secondParent;
            
        }

        private void Dfs(Tree<T> node, ICollection<T> result)
        {
            foreach (var child in node.children)
            {
                this.Dfs(child, result);
            }

            result.Add(node.Value);
        }

        private Tree<T> FindNodeWithBfs(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();
                
                if (subtree.Value.Equals(parentKey))
                {
                    return subtree;
                }

                foreach (var child in subtree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }
    }
}
