using System;
using System.Text.RegularExpressions;
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
                if (root.HasParentheses)
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
                if (root.HasParentheses)
                    resultListInOrder.Add(")");
            }


            return resultListInOrder;
        }
        public static string NormalizeExpression(string infixExpression)
        {
            // Add spaces around parentheses
            return Regex.Replace(infixExpression, @"([()])", " $1 ");
        }
        public static List<string> Tokenize(string infixExpression)
        {
            // Add spaces around parentheses to help with tokenization
            infixExpression = Regex.Replace(infixExpression, @"([()])", " $1 ");
            return infixExpression.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private static string LeftMostNode(Node<T> currentNode)
        {
            if (isLogicalOperator(currentNode.getValue()) || isArithmeticOperator(currentNode.getValue())
                && currentNode.Left != null)
            {
                return LeftMostNode(currentNode.Left);
            }
            return currentNode.getValue();
        }
        
        private static string RightMostNode(Node<T> currentNode)
        {
            if (isLogicalOperator(currentNode.getValue()) || isArithmeticOperator(currentNode.getValue())
                && currentNode.Right != null)
            {
                return LeftMostNode(currentNode.Right);
            }
            return currentNode.getValue();
        }
        
        public static bool CheckParentheses(Node<T> currentNode, int parenthesesCountLeft, 
            int parenthesesCountRight, List<string> infixTokens)
        {
            string leftMostValue = LeftMostNode(currentNode);
            string rightMostValue = RightMostNode(currentNode);
            int openBracketCount = 0;
            for (int i = infixTokens.IndexOf(leftMostValue)-1; i >= 0; i--)
            {
                if (infixTokens[i] == "(") openBracketCount++;
                else break;
            }
            int closeBracketCount = 0;
            for (int i = infixTokens.IndexOf(rightMostValue)+1; i < infixTokens.Count; i--)
            {
                if (infixTokens[i] == ")") closeBracketCount++;
                else break;
            }
            return (openBracketCount >= parenthesesCountLeft && closeBracketCount >= parenthesesCountRight);
        }

        public static int GetOperatorWithParenthesesCount(Node<T> node)
        {
            int count = 0;

            if (node.Left != null)
            {
                count += GetOperatorWithParenthesesCount(node.Left);
            }
            if (node.Right != null)
            {
                count += GetOperatorWithParenthesesCount(node.Right);
            }

            if ((isLogicalOperator(node.getValue()) || isArithmeticOperator(node.getValue())) 
                && node.HasParentheses) 
                count++;
            return count;
        }

        //public abstract Node<string> BuildExpressionTree<T>(List<string> postfix, List<NodeData> dataList);

        public abstract void BuildExpressionTree(string infix, List<NodeData> dataList);

        public abstract float Evaluate();
    }
}

