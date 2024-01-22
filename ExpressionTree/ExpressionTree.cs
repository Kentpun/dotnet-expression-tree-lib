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
        
        public static bool CheckParentheses(int index, List<string> infixTokens)
        {
            Stack<string> operatorStack = new Stack<string>();
            bool hasOpenBracket = false;
            bool hasCloseBracket = false;
            for (int i = index; i >= 0; i--)
            {
                if (infixTokens[i] == ")")
                    operatorStack.Push(")");
                if (infixTokens[i] == "(")
                {
                    if (operatorStack.Count != 0)
                        operatorStack.Pop();
                    else
                    {
                        hasOpenBracket = true;
                        break;
                    }
                }
            }

            for (int i = index; i < infixTokens.Count; i++)
            {
                if (infixTokens[i] == "(")
                    operatorStack.Push("(");
                if (infixTokens[i] == ")")
                {
                    if (operatorStack.Count != 0)
                        operatorStack.Pop();
                    else
                    {
                        hasCloseBracket = true;
                        break;
                    }
                }
            }
            return (hasCloseBracket && hasOpenBracket);
        }

        //public abstract Node<string> BuildExpressionTree<T>(List<string> postfix, List<NodeData> dataList);

        public abstract void BuildExpressionTree(string infix, List<NodeData> dataList);

        public abstract float Evaluate();
    }
}

