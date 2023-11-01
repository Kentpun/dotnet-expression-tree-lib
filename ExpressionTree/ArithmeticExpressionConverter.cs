using System;
using System.Text;
using HKSH.HIS5.LIB.DS.ExpressionTree.Components;

namespace HKSH.HIS5.LIB.DS.ExpressionTree
{
	public static class ArithmeticExpressionConverter
	{

        internal static Dictionary<ArithmeticOperator, int> precedence = new Dictionary<ArithmeticOperator, int>()
        {
            {ArithmeticOperator.EXPONENTIATION, 2},
            {ArithmeticOperator.MODULO, 1},
            {ArithmeticOperator.DIVISION, 1},
            {ArithmeticOperator.MULTIPLICATION, 1},
            {ArithmeticOperator.SUBSTRACTION, 0},
            {ArithmeticOperator.ADDITION, 0},
            {ArithmeticOperator.OPENBRACKET, -1},
            {ArithmeticOperator.CLOSEBRACKET, -1}
        };

        private static bool IsOperand(string token)
        {
            return !IsOperator(token) && token != "(" && token != ")";
        }

        private static bool IsOperator(string token)
        {
            if (token.Length == 1)
            {
                char symbol = token[0];
                return Enum.IsDefined(typeof(ArithmeticOperator), (int)symbol);
            }
            return false;
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
            Stack<ArithmeticOperator> operatorStack = new Stack<ArithmeticOperator>();
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
                    ArithmeticOperator currentOperator = (ArithmeticOperator)token[0];

                    if (currentOperator == ArithmeticOperator.OPENBRACKET)
                    {
                        operatorStack.Push(ArithmeticOperator.OPENBRACKET);
                    }

                    else if (currentOperator == ArithmeticOperator.CLOSEBRACKET)
                    {
                        while (operatorStack.Count > 0 && operatorStack.Peek() != ArithmeticOperator.OPENBRACKET)
                        {
                            postfix.Append((char)operatorStack.Pop()).Append(" ");
                        }

                        if (operatorStack.Count > 0 && operatorStack.Peek() != ArithmeticOperator.OPENBRACKET)
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
                        while (operatorStack.Count > 0 && IsOperator(((char)operatorStack.Peek()).ToString()) &&
                               precedence[currentOperator] <= precedence[operatorStack.Peek()])
                        {
                            postfix.Append((char)operatorStack.Pop()).Append(" ");
                        }

                        operatorStack.Push(currentOperator);
                    }
                }
                
                else
                {
                    throw new ArgumentException($"Invalid token: {token}");
                }
            }

            while (operatorStack.Count > 0)
            {
                postfix.Append((char) operatorStack.Pop()).Append(" ");
            }

            return postfix.ToString().Trim();
        }
    }
}

