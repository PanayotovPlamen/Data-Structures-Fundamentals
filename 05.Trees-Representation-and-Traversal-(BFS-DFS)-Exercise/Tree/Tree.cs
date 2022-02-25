namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
            child.Parent = this;
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
            parent.children.Add(this);
        }

        public string AsString()
        {
            var sb = new StringBuilder();

            this.DfsAsString(sb, this, 0);

            return sb.ToString().Trim();
        }

        public IEnumerable<T> GetInternalKeys()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetLeafKeys()
        {
            throw new NotImplementedException();
        }

        public T GetDeepestKey()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetLongestPath()
        {
            throw new NotImplementedException();
        }

        private void DfsAsString(StringBuilder sb, Tree<T> tree, int indent)
        {
            sb.Append(' ', indent)
              .Append(tree.Key.ToString());
            //.AppendLine();

            foreach (var child in tree.children)
            {
                this.DfsAsString(sb, child, indent + 2);
            }

        }
    }
}
