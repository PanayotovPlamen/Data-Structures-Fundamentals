namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            this.Root = new HtmlElement(
                ElementType.Document, 
                new HtmlElement(
                    ElementType.Html,
                    new HtmlElement(ElementType.Head),
                    new HtmlElement(ElementType.Body)
                    )
                );
        }

        public IHtmlElement Root { get; private set; }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();

                if (current.Type.Equals(type))
                {
                    return current;
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type)
        {
            var list = new List<IHtmlElement>();

            this.GetByTypeDfs(this.Root, type, list);

            return list;
        }

        private void GetByTypeDfs(IHtmlElement node, ElementType type, List<IHtmlElement> list)
        {
            foreach (var child in node.Children)
            {
                this.GetByTypeDfs(child, type, list);
            }

            if (node.Type == type)
            {
                list.Add(node);
            }
        }

        public bool Contains(IHtmlElement htmlElement)
        {
            return this.FindElement(htmlElement) == null ? false : true;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            this.ValidateElementExists(parent);

            parent.Children.Insert(0, child);

            child.Parent = parent;

        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            this.ValidateElementExists(parent);

            parent.Children.Add(child);

            child.Parent = parent;
        }

        public void Remove(IHtmlElement htmlElement)
        {
            ValidateElementExists(htmlElement);

            htmlElement.Parent.Children.Remove(htmlElement);

            htmlElement.Parent = null;

            htmlElement.Children.Clear();
        }

        public void RemoveAll(ElementType elementType)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {

                var current = queue.Dequeue();

                if (current.Type == elementType)
                {
                    var parent = current.Parent;

                    parent.Children.Remove(current);

                    current.Parent = null;

                    current.Children.Clear();
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            ValidateElementExists(htmlElement);

            return htmlElement.AddAtribute(attrKey, attrValue);
        }

        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {
            ValidateElementExists(htmlElement);

            return htmlElement.RemoveAttribute(attrKey);
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();

                if (current.HasId(idValue))
                {
                    return current;
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }
       

        private void ValidateElementExists(IHtmlElement parent)
        {
            var element = this.FindElement(parent);

            if (element == null)
            {
                throw new InvalidOperationException();
            }
        }

        private IHtmlElement FindElement(object element)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();

                if (current == element)
                {
                    return current;
                }

                foreach (var item in current.Children)
                {
                    queue.Enqueue(item);
                }
            }

            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            this.DfsToString(this.Root, 0, sb);

            return sb.ToString();
        }

        private void DfsToString(IHtmlElement node, int indent, StringBuilder sb)
        {
            sb.Append(' ', indent).AppendLine(node.Type.ToString());

            foreach (var child in node.Children)
            {
                this.DfsToString(child, indent + 2, sb);
            }
        }
    }
}
