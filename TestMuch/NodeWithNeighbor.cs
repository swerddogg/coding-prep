using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    public class NodeWithNeighbor
    {
        public string data;
        public NodeWithNeighbor left;
        public NodeWithNeighbor right;
        public NodeWithNeighbor neighbor;

        public NodeWithNeighbor(string d)
        {
            this.data = d;
        }

        public void Insert(string d)
        {
            if (d.CompareTo(this.data) < 0)
            {
                if (left == null)
                {
                    left = new NodeWithNeighbor(d);
                    return;
                }
                this.left.Insert(d);
            }
            else
            {
                if (right == null)
                {
                    right = new NodeWithNeighbor(d);
                    return;
                }
                this.right.Insert(d);
            }
        }

        public static void SetNeighbor(NodeWithNeighbor n, Stack<NodeWithNeighbor> s)
        {
            if (n.right != null)
            {
                s.Push(n.right);
                SetNeighbor(n.right, s);
            }

            if (s.Count > 0)
            {
                n.neighbor = s.Pop();
            }

            if (n.left != null)
            {
                s.Push(n.left);
                SetNeighbor(n.left, s);
            }
        }
    }
}
