using System;
using System.Xml.Linq;
using HKSH.HIS5.LIB.DS.ExpressionTree.Components;

namespace HKSH.HIS5.LIB.DS.ExpressionTree
{
    public abstract class ExpressionTree<T>
    {
        public string? Name { get; set; }
        public Node<T>? RootNode { get; set; }


        public static bool isArithmeticOperator(string token)
        {
            if (token.Length == 1)
            {
                char symbol = token[0];
                return Enum.IsDefined(typeof(ArithmeticOperator), (int)symbol);
            }
            return false;
        }

        public static bool isLogicalOperator(string ele)
        {
            if (Enum.IsDefined(typeof(LogicalOperator), ele))
            {
                return true;
            }
            return false;
        }

        public static List<string> printInorder<T>(Node<T> root)
        {
            List<string> resultListInOrder = new List<string>();
            if (root == null) return resultListInOrder;

            if (isLogicalOperator(root.getValue()) || isArithmeticOperator(root.getValue()))
            {
                resultListInOrder.Add("(");
                resultListInOrder.AddRange(printInorder(root.Left));
            }

            if (!string.IsNullOrEmpty(root.getValue()))
            {
                resultListInOrder.Add(root.getValue());
            }


            if (isLogicalOperator(root.getValue()) || isArithmeticOperator(root.getValue()))
            {
                resultListInOrder.AddRange(printInorder(root.Right));
                resultListInOrder.Add(")");
            }


            return resultListInOrder;
        }

        //public abstract Node<string> BuildExpressionTree<T>(List<string> postfix, List<NodeData> dataList);

        public abstract void BuildExpressionTree(List<string> postfix, List<NodeData> dataList);

        public abstract float Evaluate();
    }
}

