## Description
Supports Logical and Arithmetic Expression

## Usage

#### 1. Convert Infix Expression to Postfix
Function:
```
ArithmeticExpressionConverter.ConvertInfixToPostfix(infix);
```
```
LogicalExpressionConverter.ConvertInfixToPostfix(infix);
```

Sample Input:
```
( A + B + C )
```

Sample Output:
```
A B + C +
```

#### 2. Build Expression Tree
Function:
```
ArithmeticExpressionTree.BuildExpressionTree<T>(List<string> postfix, List<NodeData> dataList)
```
```
LogicalExpressionTree.BuildExpressionTree<T>(List<string> postfix, List<NodeData> dataList)
```

Sample Usage:
```
string postfix = ArithmeticExpressionConverter.ConvertInfixToPostfix(infix);
List<string> tokens = postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
List<NodeData> dataList = new List<NodeData>();
dataList.Add(new TestData(id: "A", des: "AAA", amount: 1));
dataList.Add(new TestData(id: "B", des: "BBB", amount: 2));
dataList.Add(new TestData(id: "C", des: "CCC", amount: 3));
Node<string> node = ArithmeticExpressionTree.BuildExpressionTree<string>(postfix: tokens, dataList: dataList);
```

#### 3. Convert Postfix Expression to Infix
Function:
```
ExpressionTree.printInorder<T>(Node<T> root)
```
Sample Input:
```
( A AND B ) AND C
```

Sample Output:
```
[
  "(","(","A","AND","B",")","AND","C",")"
]
```

#### 4. Evaluate Expression Tree
Function: 
```
ArithmeticExpressionTree.EvaluateTree<T>(Node<T>? root)
```
```
LogicalExpressionTree.EvaluateTree<T>(Node<T>? root)
```

Sample Usage:
```
string postfix = ArithmeticExpressionConverter.ConvertInfixToPostfix(infix);
List<string> tokens = postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
List<NodeData> dataList = new List<NodeData>();
dataList.Add(new TestData(id: "A", des: "AAA", amount: (float) 1.0));
dataList.Add(new TestData(id: "B", des: "BBB", amount: (float) 2.0));
dataList.Add(new TestData(id: "C", des: "CCC", amount: (float) 3.0));
Node<string> node = ArithmeticExpressionTree.BuildExpressionTree<string>(postfix: tokens, dataList: dataList);
float result = ArithmeticExpressionTree.EvaluateTree(node);
```
