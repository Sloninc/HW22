using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW22
{
    public class Node<TNode>: IComparable<TNode> where TNode : IComparable<TNode>
    {
        public Node(TNode value)
        {
            Value = value;
        }
        public TNode Value { get; private set; }    
        public Node<TNode>? Left { get; set; }
        public Node<TNode>? Right { get; set; }
        public int CompareTo(TNode? other)
        {
            return Value.CompareTo(other);
        }
        public int CompareNode(Node<TNode> other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
