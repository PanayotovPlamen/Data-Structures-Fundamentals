namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private LinkedList<ILink> links;

        public BrowserHistory()
        {
            this.links = new LinkedList<ILink>();
        }

        public int Size => this.links.Count;

        public void Clear()
        {
            this.links.Clear();
        }

        public bool Contains(ILink link)
        {
            return this.links.Contains(link);
        }

        public ILink DeleteFirst()
        {
            if (this.links.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var link = this.links.Last.Value;

            this.links.RemoveLast();

            return link;

        }

        public ILink DeleteLast()
        {
            if (this.links.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var link = this.links.First.Value;

            this.links.RemoveFirst();

            return link;
        }

        public ILink GetByUrl(string url)
        {
            foreach (var item in this.links)
            {
                if (item.Url == url)
                {
                    return item;
                }
            }

            return null;
        }

        public ILink LastVisited()
        {
            if (this.links.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var list = this.links.First();

            return list;

        }

        public void Open(ILink link)
        {
            this.links.AddFirst(link);
        }

        public int RemoveLinks(string url)
        {
            int count = 0;

            url = url.ToLower();

            var node = this.links.First;

            while (node != null)
            {
                var nextNode = node.Next;

                if (node.Value.Url.Contains(url))
                {
                    count++;

                    this.links.Remove(node);
                }

                node = nextNode;
            }

            if (count == 0)
            {
                throw new InvalidOperationException();
            }

            return count;
        }

        public ILink[] ToArray()
        {
            return this.links.ToArray();
        }

        public List<ILink> ToList()
        {
            return this.links.ToList();
        }

        public string ViewHistory()
        {

            if (this.links.Count == 0)
            {
                return "Browser history is empty!";
            }

            var sb = new StringBuilder();

            foreach (var item in this.links)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
