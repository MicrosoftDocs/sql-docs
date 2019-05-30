---
title: "Type System (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2016"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "sequence [XQuery]"
  - "type system [XQuery]"
  - "typed values [XQuery]"
  - "XQuery, sequence"
  - "string values [XQuery]"
  - "XQuery, XPath data type namespace"
  - "xdt prefix [XML in SQL Server]"
  - "XQuery, type system"
  - "built-in XML schema types [SQL Server]"
  - "xs prefix [XML in SQL Server]"
ms.assetid: 22d6f861-d058-47ee-b550-cbe9092dcb12
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Type System (XQuery)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  XQuery is a strongly-typed language for schema types and a weakly-typed language for untyped data. The predefined types of XQuery include the following:  
  
-   Built-in types of XML schema in the **http://www.w3.org/2001/XMLSchema** namespace.  
  
-   Types defined in the **http://www.w3.org/2004/07/xpath-datatypes** namespace.  
  
 This topic also describes the following:  
  
-   The typed value versus the string value of a node.  
  
-   The [data Function &#40;XQuery&#41;](../xquery/data-accessor-functions-data-xquery.md) and the [string Function &#40;XQuery&#41;](../xquery/data-accessor-functions-string-xquery.md).  
  
-   Matching the sequence type returned by an expression.  
  
## Built-in Types of XML Schema  
 The built-in types of XML schema have a predefined namespace prefix of xs. Some of these types include **xs:integer** and **xs:string**. All these built-in types are supported. You can use these types when you create an XML schema collection.  
  
 When querying typed XML, the static and dynamic type of the nodes is determined by the XML schema collection associated with the column or variable that is being queried. For more information about static and dynamic types, see [Expression Context and Query Evaluation &#40;XQuery&#41;](../xquery/expression-context-and-query-evaluation-xquery.md). For example, the following query is specified against a typed **xml** column (`Instructions`). The expression uses `instance of` to verify that the typed value of the `LotSize` attribute returned is of `xs:decimal` type.  
  
```  
SELECT Instructions.query('  
   DECLARE namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
   data(/AWMI:root[1]/AWMI:Location[@LocationID=10][1]/@LotSize)[1] instance of xs:decimal  
') AS Result  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 This typing information is provided by the XML schema collection associated with the column.  
  
## Types Defined in XPath Data Types Namespace  
 The types defined in the **http://www.w3.org/2004/07/xpath-datatypes** namespace have a predefined prefix of **xdt**. The following applies to these types:  
  
-   You cannot use these types when you are creating an XML schema collection. These types are used in the XQuery type system and are used for [XQuery and Static Typing](../xquery/xquery-and-static-typing.md). You can cast to the atomic types, for example, **xdt:untypedAtomic**, in the **xdt** namespace.  
  
-   When querying untyped XML, the static and dynamic type of element nodes is **xdt:untyped**, and the type of attribute values is **xdt:untypedAtomic**. The result of a **query()** method generates untyped XML. This means that the XML nodes are returned as **xdt:untyped** and **xdt:untypedAtomic**, respectively.  
  
-   The **xdt:dayTimeDuration** and **xdt:yearMonthDuration** types are not supported.  
  
 In the following example, the query is specified against an untyped XML variable. The expression, `data(/a[1]`), returns a sequence of one atomic value. The `data()` function returns the typed value of the element `<a>`. Because the XML being queried is untyped, the type of the value returned is `xdt:untypedAtomic`. Therefore, `instance of` returns true.  
  
```  
DECLARE @x xml  
SET @x='<a>20</a>'  
SELECT @x.query( 'data(/a[1]) instance of xdt:untypedAtomic' )  
```  
  
 Instead of retrieving the typed value, the expression (`/a[1]`) in the following example returns a sequence of one element, element `<a>`. The `instance of` expression uses the element test to verify that the value returned by the expression is an element node of `xdt:untyped type`.  
  
```  
DECLARE @x xml  
SET @x='<a>20</a>'  
-- Is this an element node whose name is "a" and type is xdt:untyped.  
SELECT @x.query( '/a[1] instance of element(a, xdt:untyped?)')  
-- Is this an element node of type xdt:untyped.  
SELECT @x.query( '/a[1] instance of element(*, xdt:untyped?)')  
-- Is this an element node?  
SELECT @x.query( '/a[1] instance of element()')  
```  
  
> [!NOTE]  
>  When you are querying a typed XML instance and the query expression includes the parent axis, the static type information of the resulting nodes is no longer available. However, the dynamic type is still associated with the nodes.  
  
## Typed Value vs. String Value  
 Every node has a typed value and a string value. For typed XML data, the type of the typed value is provided by the XML schema collection associated with the column or variable that is being queried. For untyped XML data, the type of the typed value is **xdt:untypedAtomic**.  
  
 You can use the **data()** or **string()** function to retrieve the value of a node:  
  
-   The [data Function &#40;XQuery&#41;](../xquery/data-accessor-functions-data-xquery.md) returns the typed value of a node.  
  
-   The [string Function &#40;XQuery&#41;](../xquery/data-accessor-functions-string-xquery.md) returns the string value of the node.  
  
 In the following XML schema collection, the <`root`> element of the integer type is defined:  
  
```  
CREATE XML SCHEMA COLLECTION SC AS N'  
<schema xmlns="http://www.w3.org/2001/XMLSchema">  
      <element name="root" type="integer"/>  
</schema>'  
GO  
```  
  
 In the following example, the expression first retrieves the typed value of `/root[1]` and then adds `3` to it.  
  
```  
DECLARE @x xml(SC)  
SET @x='<root>5</root>'  
SELECT @x.query('data(/root[1]) + 3')  
```  
  
 In the next example, the expression fails, because the `string(/root[1])` in the expression returns a string type value. This value is then passed to an arithmetic operator that takes only numeric type values as its operands.  
  
```  
-- Fails because the argument is string type (must be numeric primitive type).  
DECLARE @x xml(SC)  
SET @x='<root>5</root>'  
SELECT @x.query('string(/root[1]) + 3')  
```  
  
 The following example computes the total of the `LaborHours` attributes. The `data()` function retrieves the typed values of `LaborHours` attributes from all the <`Location`> elements for a product model. According to the XML schema associated with the `Instruction` column, `LaborHours` is of **xs:decimal** type.  
  
```  
SELECT Instructions.query('   
DECLARE namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";   
             sum(data(//AWMI:Location/@LaborHours))   
') AS Result   
FROM Production.ProductModel   
WHERE ProductModelID=7  
```  
  
 This query returns 12.75 as the result.  
  
> [!NOTE]  
>  The explicit use of the **data()** function in this example is for illustration only. If it is not specified, **sum()** implicitly applies the **data()** function to extract the typed values of the nodes.  
  
## See Also  
 [SQL Server Profiler Templates and Permissions](../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [XQuery Basics](../xquery/xquery-basics.md)  
  
  
