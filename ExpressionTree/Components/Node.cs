using System;
namespace HKSH.HIS5.LIB.DS.ExpressionTree.Components
{
    public class Node<T>
    {
        public T Value { get; set; }
        public NodeData? Data { get; set; }
        public Node<T>? Left { get; set;  }
        public Node<T>? Right { get; set; }
        public bool HasParentheses { get; set; } = false;

        public Node(T value, NodeData? data)
        {
            this.Data = data;
            this.Value = value;
            Left = Right = null;
        }

        public string getValue()
        {
            return Value.ToString();
        }
    }
}
