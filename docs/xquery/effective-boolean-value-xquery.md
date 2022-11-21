---
title: "Effective Boolean Value (XQuery) | Microsoft Docs"
description: Learn about effective Boolean values in XQuery.
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "effective Boolean value [XQuery]"
  - "Boolean values"
  - "XQuery, effective Boolean values"
  - "EBV"
ms.assetid: 506682b1-b6c9-45e2-aa54-7abd5844c3f1
author: "rothja"
ms.author: "jroth"
---
# Effective Boolean Value (XQuery)
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  These are the effective Boolean values:  
  
-   False if the operand is an empty sequence or a Boolean false.  
  
-   Otherwise, the value is true.  
  
 The effective Boolean value can be computed for expressions that return a single Boolean value, a node sequence, or an empty sequence. Note that the Boolean value is computed implicitly when the following types of expressions are processed:  
  
-   Logical expressions  
  
-   The [not function](../xquery/functions-on-boolean-values-not-function.md)  
  
-   The WHERE clause of a FLWOR expression  
  
-   [Conditional expressions](../xquery/conditional-expressions-xquery.md)  
  
-   [QuantifiedeExpressions](../xquery/quantified-expressions-xquery.md)  
  
 Following is an example of an effective Boolean value. When the **if** expression is processed, the effective Boolean value of the condition is determined. Because `/a[1]` returns an empty sequence, the effective Boolean value is false. The result is returned as XML with one text node (false).  
  
```  
value is false  
DECLARE @x XML  
SET @x = '<b/>'  
SELECT @x.query('if (/a[1]) then "true" else "false"')  
go  
```  
  
 In the following example, the effective Boolean value is true, because the expression returns a nonempty sequence.  
  
```  
DECLARE @x XML  
SET @x = '<a/>'  
SELECT @x.query('if (/a[1]) then "true" else "false"')  
go  
```  
  
 When querying typed **xml** columns or variables, you can have nodes of Boolean type. The **data()** in this case returns a Boolean value. If the query expression returns a Boolean true value, the effective Boolean value is true, as shown in the next example. The following is also illustrated in the example:  
  
-   An XML schema collection is created. The element \<b> in the collection is of Boolean type.  
  
-   A typed **xml** variable is created and queried.  
  
-   The expression `data(/b[1])` returns a Boolean true value. Therefore, the effective Boolean value in this case is true.  
  
-   The expression `data(/b[2])` returns a Boolean false value. Therefore, the effective Boolean value in this case is false.  
  
```  
CREATE XML SCHEMA COLLECTION SC AS '  
<schema xmlns="http://www.w3.org/2001/XMLSchema">  
      <element name="s" type="string"/>  
      <element name="b" type="boolean"/>  
</schema>'  
go  
DECLARE @x XML(SC)  
SET @x = '<b>true</b><b>false</b>'  
SELECT @x.query('if (data(/b[1])) then "true" else "false"')  
SELECT @x.query('if (data(/b[2])) then "true" else "false"')  
go  
```  
  
## See Also  
 [XQuery Basics](../xquery/xquery-basics.md)   
 [FLWOR Statement and Iteration &#40;XQuery&#41;](../xquery/flwor-statement-and-iteration-xquery.md)  
  
  
