## Description
Supports Logical and Arithmetic Expression

## Usage

#### 1. Instantiate Tree
```
LogicalExpressionTree tree = new LogicalExpressionTree(name: "test", postfix: tokens, dataList: dataList);
ArithmeticExpressionTree tree = new ArithmeticExpressionTree(name: "test", postfix: tokens, dataList: dataList);
```
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

#### 2. ReBuild Expression Tree
Function:
```
tree.BuildExpressionTree(List<string> postfix, List<NodeData> dataList)
```

Sample Usage:
```
string postfix = ArithmeticExpressionConverter.ConvertInfixToPostfix(infix);
List<string> tokens = postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
List<NodeData> dataList = new List<NodeData>();
dataList.Add(new TestData(id: "A", des: "AAA", amount: (float) 1.0));
dataList.Add(new TestData(id: "B", des: "BBB", amount: (float) 2.0));
dataList.Add(new TestData(id: "C", des: "CCC", amount: (float) 3.0));
ArithmeticExpressionTree tree = new ArithmeticExpressionTree("test", postfix: tokens, dataList: dataList);
tree.BuildExpressionTree(tokens, dataList);
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
tree.Evaluate()
```

Sample Usage:
```
string postfix = ArithmeticExpressionConverter.ConvertInfixToPostfix(infix);
List<string> tokens = postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
List<NodeData> dataList = new List<NodeData>();
dataList.Add(new TestData(id: "A", des: "AAA", amount: (float) 1.0));
dataList.Add(new TestData(id: "B", des: "BBB", amount: (float) 2.0));
dataList.Add(new TestData(id: "C", des: "CCC", amount: (float) 3.0));
ArithmeticExpressionTree tree = new ArithmeticExpressionTree("test", postfix: tokens, dataList: dataList);

float result = tree.Evaluate();
```
