﻿using System;
using HKSH.HIS5.LIB.DS.ExpressionTree.Components;

namespace HKSH.HIS5.LIB.DS.ExpressionTree
{
	public class ArithmeticExpressionTree : ExpressionTree<string>
	{
        public ArithmeticExpressionTree(string name)
        {
            this.Name = name;
        }

        public ArithmeticExpressionTree(string name, List<string> postfix, List<NodeData> dataList)
        {
            this.Name = name;
            this.RootNode = this.BuildExpressionTree<string>(postfix: postfix, dataList: dataList);
        }

        private Node<string> BuildExpressionTree<T>(List<string> postfix, List<NodeData> dataList)
        {
            Stack<Node<string>> stack = new Stack<Node<string>>();

            foreach (string token in postfix)
            {
                if (!isArithmeticOperator(token))
                {
                    Node<string> temp = new Node<string>(value: token, data: dataList.Find(d => d.Id == token));
                    stack.Push(temp);
                }
                else
                {
                    Node<string> temp = new Node<string>(value: token, data: dataList.Find(d => d.Id == token));
                    Node<string> t1 = stack.Pop();
                    Node<string> t2 = stack.Pop();

                    temp.Left = t2;
                    temp.Right = t1;

                    stack.Push(temp);
                }
            }

            Node<string> root = stack.Pop();
            return root;
        }

        public override void BuildExpressionTree(List<string> postfix, List<NodeData> dataList)
        {
            this.RootNode = this.BuildExpressionTree<string>(postfix: postfix, dataList: dataList);
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

            if (isArithmeticOperator(root.getValue()))
            {
                ArithmeticOperator arthimeticOperator = (ArithmeticOperator)root.getValue()[0];
                switch (arthimeticOperator)
                {
                    case ArithmeticOperator.SUBSTRACTION:
                        value = leftValue - rightValue;
                        break;
                    case ArithmeticOperator.MULTIPLICATION:
                        value = leftValue * rightValue;
                        break;
                    case ArithmeticOperator.DIVISION:
                        value = leftValue / rightValue;
                        break;
                    case ArithmeticOperator.ADDITION:
                        value = leftValue + rightValue;
                        break;
                    case ArithmeticOperator.MODULO:
                        value = leftValue % rightValue;
                        break;
                    case ArithmeticOperator.EXPONENTIATION:
                        value = (float)Math.Pow(leftValue, rightValue);
                        break;

                }
            }
            return value;
        }
    }
}

