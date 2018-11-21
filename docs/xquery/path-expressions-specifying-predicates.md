---
title: "Specifying Predicates in a Path Expression Step | Microsoft Docs"
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
  - "predicates [XQuery]"
  - "qualifiers [XQuery]"
  - "path expressions [XQuery]"
ms.assetid: 2660ceca-b8b4-4a1f-98a0-719ad5f89f81
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Path Expressions - Specifying Predicates
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  As described in the topic, [Path Expressions in XQuery](../xquery/path-expressions-xquery.md), an axis step in a path expression includes the following components:  
  
-   [An axis](../xquery/path-expressions-specifying-axis.md).  
  
-   A node test. For more information, see [Specifying Node Test in a Path Expression Step](../xquery/path-expressions-specifying-node-test.md).  
  
-   Zero or more predicates. This is optional.  
  
 The optional predicate is the third part of the axis step in a path expression.  
  
## Predicates  
 A predicate is used to filter a node sequence by applying a specified test. The predicate expression is enclosed in a square bracket and is bound to the last node in a path expression.  
  
 For example, assume that an SQL parameter value (x) of the **xml** data type is declared, as shown in the following:  
  
```  
declare @x xml  
set @x = '  
<People>  
  <Person>  
    <Name>John</Name>  
    <Age>24</Age>  
  </Person>  
  <Person>  
    <Name>Goofy</Name>  
    <Age>54</Age>  
  </Person>  
  <Person>  
    <Name>Daffy</Name>  
    <Age>30</Age>  
  </Person>  
</People>  
'  
```  
  
 In this case, the following are valid expressions that use a predicate value of [1] at each of three different node levels:  
  
```  
select @x.query('/People/Person/Name[1]')  
select @x.query('/People/Person[1]/Name')  
select @x.query('/People[1]/Person/Name')  
```  
  
 Note that in each case, the predicate binds to the node in the path expression where it is applied. For example, the first path expression selects the first <`Name`> element within each /People/Person node and, with the provided XML instance, returns the following:  
  
```  
<Name>John</Name><Name>Goofy</Name><Name>Daffy</Name>  
```  
  
 However, the second path expression selects all <`Name`> elements that are under the first /People/Person node. Therefore, it returns the following:  
  
```  
<Name>John</Name>  
```  
  
 Parentheses can also be used to change the order of evaluation of the predicate. For example, in the following expression, a set of parentheses is used to separate the path of (/People/Person/Name) from the predicate [1]:  
  
```  
select @x.query('(/People/Person/Name)[1]')  
```  
  
 In this example, the order in which the predicate is applied changes. This is because the enclosed path is first evaluated (/People/Person/Name) and then the predicate [1] operator is applied to the set that contains all nodes that matched the enclosed path. Without the parentheses, the order of operation would be different in that the [1] is applied as a `child::Name` node test, similar to the first path expression example.  
  
### Quantifiers and Predicates  
 Quantifiers can be used and added more than one time within the braces of the predicate itself. For example, using the previous example, the following is a valid use of more than one quantifier within a complex predicate subexpression.  
  
```  
select @x.query('/People/Person[contains(Name[1], "J") and xs:integer(Age[1]) < 40]/Name/text()')  
```  
  
 The result of a predicate expression is converted to a Boolean value and is referred to as the predicate truth value. Only the nodes in the sequence for which the predicate truth value is True are returned in the result. All other nodes are discarded.  
  
 For example, the following path expression includes a predicate in its second step:  
  
```  
/child::root/child::Location[attribute::LocationID=10]  
```  
  
 The condition specified by this predicate is applied to all the <`Location`> element node children. The result is that only those work center locations whose LocationID attribute value is 10 are returned.  
  
 The previous path expression is executed in the following SELECT statement:  
  
```  
SELECT Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
 /child::AWMI:root/child::AWMI:Location[attribute::LocationID=10]  
')  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
### Computing Predicate Truth Values  
 The following rules are applied in order to determine the predicate truth value, according to the XQuery specifications:  
  
1.  If the value of the predicate expression is an empty sequence, the predicate truth value is False.  
  
     For example:  
  
    ```  
    SELECT Instructions.query('  
    declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
     /child::AWMI:root/child::AWMI:Location[attribute::LotSize]  
    ')  
    FROM Production.ProductModel  
    WHERE ProductModelID=7  
    ```  
  
     The path expression in this query returns only those <`Location`> element nodes that have a LotSize attribute specified. If the predicate returns an empty sequence for a specific <`Location`>, that work center location is not returned in the result.  
  
2.  Predicate values can only be xs:integer, xs:Boolean, or node\*. For node\*, the predicate evaluates to True if there are any nodes, and False for an empty sequence. Any other numeric type, such as double and float type, generates a static typing error. The predicate truth value of an expression is True if and only if the resulting integer is equal to the value of the context position. Also, only integer literal values and the **last()** function reduce the cardinality of the filtered step expression to 1.  
  
     For example, the following query retrieves the third child element node of the <`Features`> element.  
  
    ```  
    SELECT CatalogDescription.query('  
    declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
    declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
     /child::PD:ProductDescription/child::PD:Features/child::*[3]  
    ')  
    FROM Production.ProductModel  
    WHERE ProductModelID=19  
    ```  
  
     Note the following from the previous query:  
  
    -   The third step in the expression specifies a predicate expression whose value is 3. Therefore, the predicate truth value of this expression is True only for the nodes whose context position is 3.  
  
    -   The third step also specifies a wildcard character (*) that indicates all the nodes in the node test. However, the predicate filters the nodes and returns only the node in the third position.  
  
    -   The query returns the third child element node of the <`Features`> element children of the <`ProductDescription`> element children of the document root.  
  
3.  If the value of the predicate expression is one simple type value of type Boolean, the predicate truth value is equal to the value of the predicate expression.  
  
     For example, the following query is specified against an **xml**type variable that holds an XML instance, the customer survey XML instance. The query retrieves those customers who have children. In this query, that would be \<HasChildren>1\</HasChildren>.  
  
    ```  
    declare @x xml  
    set @x='  
    <Survey>  
      <Customer CustomerID="1" >  
      <Age>27</Age>  
      <Income>20000</Income>  
      <HasChildren>1</HasChildren>  
      </Customer>  
      <Customer CustomerID="2" >  
      <Age>27</Age>  
      <Income>20000</Income>  
      <HasChildren>0</HasChildren>  
      </Customer>  
    </Survey>  
    '  
    declare @y xml  
    set @y = @x.query('  
      for $c in /child::Survey/child::Customer[( child::HasChildren[1] cast as xs:boolean ? )]  
      return   
          <CustomerWithChildren>  
              { $c/attribute::CustomerID }  
          </CustomerWithChildren>  
    ')  
    select @y  
    ```  
  
     Note the following from the previous query:  
  
    -   The expression in the **for** loop has two steps, and the second step specifies a predicate. The value of this predicate is a Boolean type value. If this value is True, the truth value of the predicate is also True.  
  
    -   The query returns the <`Customer`> element children, whose predicate value is True, of the \<Survey> element children of the document root. This is the result:  
  
        ```  
        <CustomerWithChildren CustomerID="1"/>   
        ```  
  
4.  If the value of the predicate expression is a sequence that contains at least one node, the predicate truth value is True.  
  
 For example, the following query retrieves ProductModelID for product models whose XML catalog description includes at least one feature, a child element of the <`Features`> element, from the namespace associated with the **wm** prefix.  
  
```  
SELECT ProductModelID  
FROM   Production.ProductModel  
WHERE CatalogDescription.exist('  
             declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
             declare namespace wm="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";  
             /child::PD:ProductDescription/child::PD:Features[wm:*]  
             ') = 1  
```  
  
 Note the following from the previous query:  
  
-   The WHERE clause specifies the [exist() method (XML data type)](../t-sql/xml/exist-method-xml-data-type.md).  
  
-   The path expression inside the **exist()** method specifies a predicate in the second step. If the predicate expression returns a sequence of at least one feature, the truth value of this predicate expression is True. In this case, because the **exist()** method returns a True, the ProductModelID is returned.  
  
## Static Typing and Predicate Filters  
 The predicates may also affect the statically inferred type of an expression. Integer literal values and the **last()** function reduce the cardinality of the filtered step expression to at most one.  
  
  
