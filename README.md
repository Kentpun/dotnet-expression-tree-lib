## Description
Supports Logical and Arithmetic Expression

Infix Expression - mathematical expression in which operators are placed between the operands.
<br>
Assume operators and operands in the infix expression are separated by a space " ".
## Usage

#### 1. Instantiate Tree
```
LogicalExpressionTree tree = new LogicalExpressionTree("Tree Name", infix: infix, dataList: dataList);
ArithmeticExpressionTree tree = new ArithmeticExpressionTree("test", infix, dataList);
```
#### 1. Convert Infix Expression to Postfix
Function:
```
ArithmeticExpressionConverter.ConvertInfixToPostfix(ArithmeticExpressionTree.NormalizeExpression(infix2));
```
```
LogicalExpressionConverter.ConvertInfixToPostfix(ExpressionTree<T>.NormalizeExpression(infix2));
```

Sample Input:
```
( A + B + C )
```

Sample Output:
```
A B + C +
```

#### 2. ReBuild Expression Tree
Function:
```
tree.BuildExpressionTree(newInfix, newDataList);
```

Sample Usage:
```
string infix = "((1 + 4) * (2 + 3))";
List<NodeData> dataList = new List<NodeData>();
dataList.Add(new TestData(id: "1", des: "AAA", amount: (float) 1.0));
dataList.Add(new TestData(id: "4", des: "AAA", amount: (float) 4.0));
dataList.Add(new TestData(id: "2", des: "BBB", amount: (float) 2.0));
dataList.Add(new TestData(id: "3", des: "CCC", amount: (float) 3.0));

ArithmeticExpressionTree tree2 = new ArithmeticExpressionTree("Arithmetic Tree", infix, dataList);
// new infix and new DataList
tree.BuildExpressionTree(newInfix, newDataList);
```

#### 3. Convert Postfix Expression to Infix
Function:
```
ExpressionTree.printInorder<T>(Node<T> rootNode)
```
Sample Input:
```
( A AND B ) AND C
```

Sample Output:
```
[
  "(","A","AND","B",")","AND","C"
]
```

#### 4. Evaluate Expression Tree
Function: 
```
tree.Evaluate()
```

Sample Usage:
```
Console.WriteLine("Arithmetic");
string infix2 = "((1 + 4) * (2 + 3))";
List<NodeData> dataList2 = new List<NodeData>();
dataList2.Add(new TestData(id: "1", des: "AAA", amount: (float) 1.0));
dataList2.Add(new TestData(id: "4", des: "AAA", amount: (float) 4.0));
dataList2.Add(new TestData(id: "2", des: "BBB", amount: (float) 2.0));
dataList2.Add(new TestData(id: "3", des: "CCC", amount: (float) 3.0));

ArithmeticExpressionTree tree2 = new ArithmeticExpressionTree("test", infix2, dataList2);

float result2 = tree2.Evaluate();
Console.WriteLine(result2);
```
