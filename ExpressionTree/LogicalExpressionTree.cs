using System;
using HKSH.HIS5.LIB.DS.ExpressionTree.Components;

namespace HKSH.HIS5.LIB.DS.ExpressionTree
{
	public class LogicalExpressionTree : ExpressionTree<string>
	{
        public LogicalExpressionTree(string name)
        {
            this.Name = name;
        }

        public LogicalExpressionTree(string name, string infix, List<NodeData> dataList)
        {
            this.Name = name;
            
            string postfixExpression = LogicalExpressionConverter.ConvertInfixToPostfix(NormalizeExpression(infix)); 
            List<string> postfixTokens = postfixExpression.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> infixTokens = Tokenize(infix);
            
            this.RootNode = this.BuildExpressionTree<string>(
                postfix: postfixTokens, 
                originalInfixTokens: infixTokens, 
                dataList: dataList);
        }

        private Node<string> BuildExpressionTree<T>(List<string> postfix, List<string> originalInfixTokens, List<NodeData> dataList)
        {
            Stack<Node<string>> stack = new Stack<Node<string>>();
            int operandIndex = -1;
            Stack<int> openingParenthesesStack = new Stack<int>();
            foreach (string token in postfix)
            {
                
                if (!isLogicalOperator(token))
                {
                    Node<string> temp = new Node<string>(value: token, data: dataList.Find(d => d.Id == token));
                    stack.Push(temp);
                    operandIndex = originalInfixTokens.IndexOf(token);
                }
                else
                {
                    Node<string> temp = new Node<string>(value: token, data: dataList.Find(d => d.Id == token));
                    Node<string> t1 = stack.Pop();
                    Node<string> t2 = stack.Pop();

                    temp.Left = t2;
                    temp.Right = t1;
                    
                    // Set HasParentheses based on original tokenization
                    temp.HasParentheses = CheckParentheses(operandIndex - 1, originalInfixTokens);
                    stack.Push(temp);
                    
                    // Pop the corresponding opening parenthesis index
                    if (openingParenthesesStack.Count > 0)
                    {
                        openingParenthesesStack.Pop();
                    }
                }
            }

            Node<string> root = stack.Pop();
            return root;
        }

        public override void BuildExpressionTree(string infix, List<NodeData> dataList)
        {
            string postfixExpression = LogicalExpressionConverter.ConvertInfixToPostfix(NormalizeExpression(infix)); 
            List<string> postfixTokens = postfixExpression.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> infixTokens = Tokenize(infix);
            this.RootNode = this.BuildExpressionTree<string>(
                postfix: postfixTokens, 
                originalInfixTokens: infixTokens, 
                dataList: dataList);
        }

        public override float Evaluate()
        {
            return this.EvaluateTree<string>(this.RootNode);
        }

        private float EvaluateTree<T>(Node<T>? root)
        {
            float value = 0;
            if (root == null)
                return value;

            if (root.Left == null && root.Right == null)
            {
                if (root.Data != null)
                    return root.Data.GetAmount();
                else
                    return 0;
            }

            float leftValue = EvaluateTree(root.Left);
            float rightValue = EvaluateTree(root.Right);

            if (isLogicalOperator(root.getValue()))
            {
                LogicalOperator currentOperator = Enum.Parse<LogicalOperator>(root.getValue());
                switch (currentOperator)
                {
                    case LogicalOperator.OR:
                        value = Math.Max(leftValue, rightValue);
                        break;
                    default:
                        value = leftValue + rightValue;
                        break;
                }
            }
            return value;
        }
    }
}

