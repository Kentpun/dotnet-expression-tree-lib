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

        private static bool IsOperand(string token)
        {
            return !IsOperator(token) && token != "(" && token != ")";
        }

        private static bool IsOperator(string token)
        {
            return Enum.TryParse<LogicalOperator>(token, out _);
        }

        private static bool IsParentheses(string token)
        {
            if (token.Length == 1)
            {
                char symbol = token[0];
                return Enum.IsDefined(typeof(Parentheses), (int)symbol);
            }
            return false;
        }

        private static bool ValidateInfixExpression(string infixExpression)
        {
            string[] infixElements = infixExpression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            // Check for null or empty expression
            if (string.IsNullOrEmpty(infixExpression))
            {
                return false;
            }

            // Stack to validate parentheses
            Stack<string> parenthesesStack = new Stack<string>();
            bool isOperandExpected = true;

            foreach (string ele in infixElements)
            {
                // Check for valid characters
                if (!IsOperator(ele) && !IsOperand(ele) && !IsParentheses(ele))
                {
                    return false;
                }

                if (isOperandExpected)
                {
                    if (IsOperand(ele))
                        isOperandExpected = false;
                    else if (ele != "(")
                        return false;
                }
                else
                {
                    if (IsOperator(ele) && !IsParentheses(ele))
                        isOperandExpected = true;
                    else if (ele != ")")
                        return false;
                }

                // Check parentheses balance
                if (ele.Length == 1 && ele[0] == '(')
                {
                    parenthesesStack.Push(ele);
                }
                else if (ele.Length == 1 && ele[0] == ')')
                {
                    if (parenthesesStack.Count == 0)
                    {
                        return false; // Unbalanced parentheses
                    }
                    parenthesesStack.Pop();
                }

                // Add other validation checks based on your specific requirements
            }

            // Check for unbalanced parentheses
            if (parenthesesStack.Count != 0)
                return false;

            if (isOperandExpected)
                return false;

            return true; // Valid expression
        }


        public static string ConvertInfixToPostfix(string infixExpression)
        {
            if (!ValidateInfixExpression(infixExpression))
                throw new ArgumentException("Invalid InfixExpression");

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
            
    }

}

