---
title: "Specifying Node Test in a Path Expression Step | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "axis step [XQuery]"
  - "node test [XQuery]"
ms.assetid: ffe27a4c-fdf3-4c66-94f1-7e955a36cadd
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Path Expressions - Specifying Node Test
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  An axis step in a path expression includes the following components:  
  
-   [An axis](../xquery/path-expressions-specifying-axis.md)  
  
-   A node test  
  
-   [Zero or more step qualifiers (optional)](../xquery/path-expressions-specifying-predicates.md)  
  
 For more information, see [Path Expressions &#40;XQuery&#41;](../xquery/path-expressions-xquery.md).  
  
 A node test is a condition and is the second component of the axis step in a path expression. All the nodes selected by a step must satisfy this condition. For the path expression, `/child::ProductDescription`, the node test is `ProductDescription`. This step retrieves only those element node children whose name is ProductDescription.  
  
 A node test condition can include the following:  
  
-   A node name. Only nodes of the principal node kind with the specified name are returned.  
  
-   A node type. Only nodes of the specified type are returned.  
  
> [!NOTE]  
>  Node names that are specified in XQuery path expressions are not subject to the same collation-sensitive rules as Transact-SQL queries are and are always case-sensitive.  
  
## Node Name as Node Test  
 When specifying a node name as a node test in a path expression step, you must understand the concept of principal node kind. Every axis, child, parent, or attribute, has a principal node kind. For example:  
  
-   An attribute axis can contain only attributes. Therefore, the attribute node is the principal node kind of the attribute axis.  
  
-   For other axes, if the nodes selected by the axis can contain element nodes, element is the principal node kind for that axis.  
  
 When you specify a node name as a node test, the step returns the following types of nodes:  
  
-   Nodes that areof the principal node kind of the axis.  
  
-   Nodes that have the same name as specified in the node test.  
  
 For example, consider the following path expression:  
  
```  
child::ProductDescription   
```  
  
 This one-step expression specifies a `child` axis and the node name `ProductDescription` as the node test. The expression returns only those nodes that are of the principal node kind of the child axis, element nodes, and which have ProductDescription as their name.  
  
 The path expression, `/child::PD:ProductDescription/child::PD:Features/descendant::*,` has three steps. These steps specify child and descendant axes. In each step, the node name is specified as the node test. The wildcard character (`*`) in the third step indicates all nodes of the principle node kind for the descendant axis. The principal node kind of the axis determines the type of nodes selected, and the node name filters that the nodes selected.  
  
 As a result, when this expression is executed against product catalog XML documents in the **ProductModel** table, it retrieves all the element node children of the \<Features> element node child of the \<ProductDescription> element.  
  
 The path expression, `/child::PD:ProductDescription/attribute::ProductModelID`, is made up of two steps. Both of these steps specify a node name as the node test. Also, the second step uses the attribute axis. Therefore, each step selects nodes of the principal node kind of its axis that has the name specified as the node test. Thus, the expression returns **ProductModelID** attribute node of the \<ProductDescription> element node.  
  
 When specifying the names of nodes for node tests, you can also use the wildcard character (*) to specify the local name of a node or for its namespace prefix, as shown in the following example:  
  
```  
declare @x xml  
set @x = '  
<greeting xmlns="ns1">  
   <salutation>hello</salutation>  
</greeting>  
<greeting xmlns="ns2">  
   <salutation>welcome</salutation>  
</greeting>  
<farewell xmlns="ns1" />'  
select @x.query('//*:greeting')  
select @x.query('declare namespace ns="ns1"; /ns:*')  
```  
  
## Node Type as Node Test  
 To query for node types other than element nodes, use a node type test. As shown in the following table, there are four node type tests available.  
  
|Node type|Returns|Example|  
|---------------|-------------|-------------|  
|`comment()`|True for a comment node.|`following::comment()` selects all the comment nodes that appear after the context node.|  
|`node()`|True for a node of any kind.|`preceding::node()` selects all the nodes that appear before the context node.|  
|`processing-instruction()`|True for a processing instruction node.|`self::processing instruction()` selects all the processing instruction nodes within the context node.|  
|`text()`|True for a text node.|`child::text()` selects the text nodes that are children of the context node.|  
  
 If node type, such as text() or comment() ..., is specified as the node test, the step just returns nodes of the specified kind, regardless of the principal node kind of the axis. For example, the following path expression returns only the comment node children of the context node:  
  
```  
child::comment()  
```  
  
 Similarly, `/child::ProductDescription/child::Features/child::comment()` retrieves comment node children of the \<Features> element node child of the \<ProductDescription> element node.  
  
## Examples  
 The following examples compare node name and node kind.  
  
### A. Results of specifying the node name and the node type as node tests in a path expression  
 In the following example, a simple XML document is assigned to an **xml** type variable. The document is queried by using different path expressions. The results are then compared.  
  
```  
declare @x xml  
set @x='  
<a>  
 <b>text1  
   <c>text2  
     <d>text3</d>  
   </c>  
 </b>  
</a>'  
select @x.query('  
/child::a/child::b/descendant::*  
')  
```  
  
 This expression asks for the descendant element nodes of the `<b>` element node.  
  
 The asterisk (`*`) in the node test indicates a wildcard character for node name. The descendant axis has the element node as its primary node kind. Therefore, the expression returns all the descendant element nodes of element node `<b>`. That is, element nodes `<c>` and `<d>` are returned, as shown in the following result:  
  
```  
<c>text2  
     <d>text3</d>  
</c>  
<d>text3</d>  
```  
  
 If you specify a descendent-or-self axis instead of specifying a descendant axis, , the context node is returned and also its descendants:  
  
```  
/child::a/child::b/descendant-or-self::*  
```  
  
 This expression returns the element node `<b>` and its descendant element nodes. In returning the descendant nodes, the primary node kind of the descendant-or-self axis, element node type, determines what kind of nodes are returned.  
  
 This is the result:  
  
```  
<b>text1  
   <c>text2  
     <d>text3</d>  
   </c>  
</b>  
  
<c>text2  
     <d>text3</d>  
</c>  
  
<d>text3</d>   
```  
  
 The previous expression used a wildcard character as a node name. Instead, you can use the `node()` function, as show in this expression:  
  
```  
/child::a/child::b/descendant::node()  
```  
  
 Because `node()` is a node type, you will receive all the nodes of the descendant axis. This is the result:  
  
```  
text1  
<c>text2  
     <d>text3</d>  
</c>  
text2  
<d>text3</d>  
text3  
```  
  
 Again, if you specify descendant-or-self axis and `node()` as the node test, you will receive all the descendants, elements, and text nodes, and also the context node, the `<b>` element.  
  
```  
<b>text1  
   <c>text2  
     <d>text3</d>  
   </c>  
</b>  
text1  
<c>text2  
     <d>text3</d>  
</c>  
text2  
<d>text3</d>  
text3  
```  
  
### B. Specifying a node name in the node test  
 The following example specifies a node name as the node test in all the path expressions. As a result, all the expressions return nodes of the principal node kind of the axis that have the node name specified in the node test.  
  
 The following query expression returns the <`Warranty`> element from the product catalog XML document stored in the `Production.ProductModel` table:  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
 /child::PD:ProductDescription/child::PD:Features/child::wm:Warranty  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 Note the following from the previous query:  
  
-   The `namespace` keyword in the XQuery prolog defines a prefix that is used in the query body. For more information, about the XQuery prolog, see [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md) .  
  
-   All three steps in the path expression specify the child axis and a node name as the node test.  
  
-   The optional step-qualifier part of the axis step is not specified in any of the steps in the expression.  
  
 The query returns the <`Warranty`> element children of the <`Features`> element child of the <`ProductDescription`> element.  
  
 This is the result:  
  
```  
<wm:Warranty xmlns:wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
  <wm:WarrantyPeriod>3 years</wm:WarrantyPeriod>  
  <wm:Description>parts and labor</wm:Description>  
</wm:Warranty>     
```  
  
 In the following query, the path expression specifies a wildcard character (`*`) in a node test.  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
 /child::PD:ProductDescription/child::PD:Features/child::*  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 The wildcard character is specified for the node name. Thus, the query returns all the element node children of the <`Features`> element node child of the <`ProductDescription`> element node.  
  
 The following query is similar to the previous query except that together with the wildcard character, a namespace is specified. As a result, all the element node children in that namespace are returned. Note that the <`Features`> element can contain elements from different namespaces.  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
 /child::PD:ProductDescription/child::PD:Features/child::wm:*  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 You can use the wildcard character as a namespace prefix, as shown in this query:  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
 /child::PD:ProductDescription/child::PD:Features/child::*:Maintenance  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 This query returns the <`Maintenance`> element node children in all the namespaces from the product catalog XML document.  
  
### C. Specifying node kind in the node test  
 The following example specifies the node kind as the node test in all the path expressions. As a result, all the expressions return nodes of the kind specified in the node test.  
  
 In the following query, the path expression specifies a node kind in its third step:  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
 /child::PD:ProductDescription/child::PD:Features/child::text()  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 In the next query, the following is specified:  
  
-   The path expression has three steps separated by a slash mark (`/`).  
  
-   Each of these steps specifies a child axis.  
  
-   The first two steps specify a node name as the node test, and the third step specifies a node kind as the node test.  
  
-   The expression returns text node children of the <`Features`> element child of the <`ProductDescription`> element node.  
  
 Only one text node is returned. This is the result:  
  
```  
These are the product highlights.   
```  
  
 The following query returns the comment node children of the <`ProductDescription`> element:  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
 /child::PD:ProductDescription/child::comment()  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 Note the following from the previous query:  
  
-   The second step specifies a node kind as the node test.  
  
-   As a result, the expression returns the comment node children of the <`ProductDescription`> element nodes.  
  
 This is the result:  
  
```  
<!-- add one or more of these elements... one for each specific product in this product model -->  
<!-- add any tags in <specifications> -->      
```  
  
 The following query retrieves the top-level, processing-instruction nodes:  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
 /child::processing-instruction()  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 This is the result:  
  
```  
<?xml-stylesheet href="ProductDescription.xsl" type="text/xsl"?>   
```  
  
 You can pass a string literal parameter to the `processing-instruction()` node test. In this case, the query returns the processing instructions whose name attribute value is the string literal specified in the argument.  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
 /child::processing-instruction("xml-stylesheet")  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
## Implementation Limitations  
 Following are the specific limitations  
  
-   The extended SequenceType node tests are not supported.  
  
-   processing-instruction(name) is not supported. Instead, put the name in quotation marks.  
  
  
