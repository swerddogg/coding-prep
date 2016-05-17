using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    class NodeWithEqualNodes
    {
        public NodeWithEqualNodes Left;
        public NodeWithEqualNodes Right;
        public NodeWithEqualNodes Center;
        public int data;

        public NodeWithEqualNodes(int data)
        {
            this.data = data;
        }

        public static NodeWithEqualNodes Insert(NodeWithEqualNodes root, int data)
        {
            if (root == null)
            {
                return new NodeWithEqualNodes(data);
            }
            else if (data < root.data)
            {
                root.Left = Insert(root.Left, data);
            }
            else if (data > root.data)
            {
                root.Right = Insert(root.Right, data);
            }
            else
            {
                root.Center = Insert(root.Center, data);
            }
            return root;
        }

        public static void Delete(ref NodeWithEqualNodes root, int data)
        {
            if (root == null)
            {
                return;
            }

            var deleteData = FindParentAndNodeOf(root, data);

            // node doesn't exist
            if (deleteData == null)
                return;

            // extracting just so it's less typing. doing a lot of comparing below
            var parent = deleteData.Parent;
            var nodeToDelete = deleteData.NodeToDelete;
            
            // found a duplicate node, just remove one link from the head
            if (nodeToDelete.Center != null)
            {
                nodeToDelete.Center = nodeToDelete.Center.Center;
                return;
            }

            // No children, just remove reference from parent
            if (nodeToDelete.Left == null &&
                nodeToDelete.Right == null)
            {
                // find out which side of the parent the node is on
                if (nodeToDelete.data < parent.data)
                {
                    parent.Left = null;
                }
                else
                {
                    parent.Right = null;
                }
            }
            // one child is null, set parent equal to node to delete's only child
            else if (nodeToDelete.Left == null ||
                nodeToDelete.Right == null)
            {
                // find out which side of the parent the node is on
                if (nodeToDelete.data < parent.data)
                {
                    // node has only one child, find the correct one to link the parent to
                    if (nodeToDelete.Right == null)
                    {
                        parent.Left = nodeToDelete.Left;
                    }
                    else
                    {
                        parent.Left = nodeToDelete.Right;
                    }
                }
                else
                {
                    // node has only one child, find the correct one to link the parent to
                    if (nodeToDelete.Right == null)
                    {
                        parent.Right = nodeToDelete.Left;
                    }
                    else
                    {
                        parent.Right = nodeToDelete.Right;
                    }
                }
                // safe to clear both since the node had only one child to begin with
                nodeToDelete.Left = null;
                nodeToDelete.Right = null;
            }
            // node has two childen so a shift is needed
            else
            {
                // find the min node in the right subtree
                var minNodeRightSubtree = nodeToDelete.Right;

                while (minNodeRightSubtree.Left != null)
                {
                    minNodeRightSubtree = minNodeRightSubtree.Left;
                }

                // copy data and Center link from min to node 
                nodeToDelete.data = minNodeRightSubtree.data;
                nodeToDelete.Center = minNodeRightSubtree.Center;
                minNodeRightSubtree.Center = null;

                if (nodeToDelete.Right.Left == null)
                {
                    nodeToDelete.Right = null;
                }
                else
                {
                    // remove the duplicate value from the node's right
                    Delete(ref nodeToDelete.Right, nodeToDelete.data);
                }
            }
        }

        public static NodeDeleteData FindParentAndNodeOf(NodeWithEqualNodes root, int data)
        {
            return FindParentAndNodeOf(root, data, null);
        }

        private static NodeDeleteData FindParentAndNodeOf(NodeWithEqualNodes root, int data, NodeWithEqualNodes parent)
        {
            if (root == null)
                return null;

            if (data == root.data)
            {
                return new NodeDeleteData()
                {
                    Parent = parent,
                    NodeToDelete = root
                };
            }
            else if (data < root.data)
            {
                parent = root;
                return FindParentAndNodeOf(root.Left, data, parent);
            }
            else
            {
                parent = root;
                return FindParentAndNodeOf(root.Right, data, parent);
            }
        }
    }

    class NodeDeleteData
    {
        public NodeWithEqualNodes Parent;
        public NodeWithEqualNodes NodeToDelete;
    }
}
