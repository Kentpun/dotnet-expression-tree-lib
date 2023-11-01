using System;
using System.Collections.Generic;
using System.Text;
using HKSH.HIS5.LIB.DS.ExpressionTree.Components;

namespace HKSH.HIS5.LIB.DS.ExpressionTree
{
	public static class LogicalExpressionConverter
	{
        private static Dictionary<LogicalOperator, int> precedence = new Dictionary<LogicalOperator, int>()
        {
            {LogicalOperator.AND, 2},
            {LogicalOperator.OR, 1},
            {LogicalOperator.THEN, 0},
            {LogicalOperator.OPENBRACKET, -1},
            {LogicalOperator.CLOSEBRACKET, -1}
        };

        public static string ConvertInfixToPostfix(string infixExpression)
        {
            Stack<LogicalOperator> operatorStack = new Stack<LogicalOperator>();
            StringBuilder postfix = new StringBuilder();

            string[] tokens = infixExpression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                if (IsOperand(token))
                {
                    postfix.Append(token).Append(" ");
                }
                else if (IsOperator(token))
                {
                    LogicalOperator currentOperator = Enum.Parse<LogicalOperator>(token);
                    while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek().ToString()) &&
                           precedence[currentOperator] <= precedence[operatorStack.Peek()])
                    {
                        postfix.Append(operatorStack.Pop()).Append(" ");
                    }

                    operatorStack.Push(currentOperator);
                }
                else if (token == "(")
                {
                    operatorStack.Push(LogicalOperator.OPENBRACKET);
                }
                else if (token == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != LogicalOperator.OPENBRACKET)
                    {
                        postfix.Append(operatorStack.Pop()).Append(" ");
                    }

                    if (operatorStack.Count > 0 && operatorStack.Peek() != LogicalOperator.OPENBRACKET)
                    {
                        throw new ArgumentException("Invalid expression: Unmatched parentheses");
                        
                    }
                    else
                    {
                        operatorStack.Pop(); 
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid token: {token}");
                }
            }

            while (operatorStack.Count > 0)
            {
                postfix.Append(operatorStack.Pop()).Append(" ");
            }

            return postfix.ToString().Trim();
        }

        private static bool IsOperand(string token)
        {
            return !IsOperator(token) && token != "(" && token != ")";
        }

        private static bool IsOperator(string token)
        {
            return Enum.TryParse<LogicalOperator>(token, out _);
        }
            
    }

}

