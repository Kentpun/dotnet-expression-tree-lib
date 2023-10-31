using System;
namespace HKSH.HIS5.LIB.DS.ExpressionTree.Components
{
    public class Node<T>
    {
        public T Value;
        public NodeData? Data;
        public Node<T>? Left, Right;

        public Node(T value, NodeData? data)
        {
            this.Data = data;
            this.Value = value;
            Left = Right = null;
        }

        public string? getValue()
        {
            return Value?.ToString();
        }
    }
}
