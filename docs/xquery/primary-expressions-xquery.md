---
title: "Primary Expressions (XQuery) | Microsoft Docs"
description: Learn about XQuery primary expressions that include literals, variable references, context item expressions, constructors, and function calls.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "variable references [XQuery]"
  - "primary expressions [XQuery]"
  - "function calls [XQuery]"
  - "expressions [XQuery], primary"
  - "literals [XQuery]"
  - "context item expressions [XQuery]"
ms.assetid: d4183c3e-12b5-4ca0-8413-edb0230cb159
author: "rothja"
ms.author: "jroth"
---
# Primary Expressions (XQuery)
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  The XQuery primary expressions include literals, variable references, context item expressions, constructors, and function calls.  
  
## Literals  
 XQuery literals can be numeric or string literals. A string literal can include predefined entity references, and an entity reference is a sequence of characters. The sequence starts with an ampersand that represents a single character that otherwise might have syntactic significance. Following are the predefined entity references for XQuery.  
  
|Entity reference|Represents|  
|----------------------|----------------|  
|`&lt;`|\<|  
|`&gt;`|>|  
|`&amp;`|&|  
|`&quot;`|"|  
|`&apos;`|'|  
  
 A string literal can also contain a character reference, an XML-style reference to a Unicode character, that is identified by its decimal or hexadecimal code point. For example, the Euro symbol can be represented by the character reference, "&\#8364;".  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses XML version 1.0 as the basis for parsing.  
  
### Examples  
 The following examples illustrate the use of literals and also entity and character references.  
  
 This code returns an error, because the `<'` and `'>` characters have special meaning.  
  
```  
DECLARE @var XML  
SET @var = ''  
SELECT @var.query(' <SalaryRange>Salary > 50000 and < 100000</SalaryRange>')  
GO  
```  
  
 If you use an entity reference instead, the query works.  
  
```  
DECLARE @var XML  
SET @var = ''  
SELECT @var.query(' <SalaryRange>Salary &gt; 50000 and &lt; 100000</SalaryRange>')  
GO  
```  
  
 The following example illustrates the use of a character reference to represent the Euro symbol.  
  
```  
DECLARE @var XML  
SET @var = ''  
SELECT @var.query(' <a>€12.50</a>')  
```  
  
 This is the result.  
  
 `<a>€12.50</a>`  
  
 In the following example, the query is delimited by apostrophes. Therefore, the apostrophe in the string value is represented by two adjacent apostrophes.  
  
```  
DECLARE @var XML  
SET @var = ''  
SELECT @var.query('<a>I don''t know</a>')  
Go  
```  
  
 This is the result.  
  
 `<a>I don't know</a>`  
  
 The built-in Boolean functions, **true()** and **false()**, can be used to represent Boolean values, as shown in the following example.  
  
```  
DECLARE @var XML  
SET @var = ''  
SELECT @var.query('<a>{true()}</a>')  
GO  
```  
  
 The direct element constructor specifies an expression in curly braces. This is replaced by its value in the resulting XML.  
  
 This is the result.  
  
 `<a>true</a>`  
  
## Variable References  
 A variable reference in XQuery is a QName preceded by a $ sign. This implementation supports only unprefixed variable references. For example, the following query defines the variable `$i` in the FLWOR expression.  
  
```  
DECLARE @var XML  
SET @var = '<root>1</root>'  
SELECT @var.query('  
 for $i in /root return data($i)')  
GO  
```  
  
 The following query will not work because a namespace prefix is added to the variable name.  
  
```  
DECLARE @var XML  
SET @var = '<root>1</root>'  
SELECT @var.query('  
DECLARE namespace x="https://X";  
for $x:i in /root return data($x:i)')  
GO  
```  
  
 You can use the sql:variable() extension function to refer to SQL variables, as shown in the following query.  
  
```  
DECLARE @price money  
SET @price=2500  
DECLARE @x xml  
SET @x = ''  
SELECT @x.query('<value>{sql:variable("@price") }</value>')  
```  
  
 This is the result.  
  
 `<value>2500</value>`  
  
#### Implementation Limitations  
 These are the implementation limitations:  
  
-   Variables with namespace prefixes are not supported.  
  
-   Module import is not supported.  
  
-   External variable declarations are not supported. A solution to this is to use the [sql:variable() function](../xquery/xquery-extension-functions-sql-variable.md).  
  
## Context Item Expressions  
 The context item is the item currently being processed in the context of a path expression. It is initialized in a not-NULL XML data type instance with the document node. It can also be changed by the nodes() method, in the context of XPath expressions or the [] predicates.  
  
 The context item is returned by an expression that contains a dot (.). For example, the following query evaluates each element <`a`> for the presence of attribute `attr`. If the attribute is present, the element is returned. Note that the condition in the predicate specifies that the context node is specified by single period.  
  
```  
DECLARE @var XML  
SET @var = '<ROOT>  
<a>1</a>  
<a attr="1">2</a>  
</ROOT>'  
SELECT @var.query('/ROOT[1]/a[./@attr]')  
```  
  
 This is the result.  
  
 `<a attr="1">2</a>`  
  
## Function Calls  
 You can call built-in XQuery functions and the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] sql:variable() and sql:column() functions. For a list of implemented functions, see [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md).  
  
#### Implementation Limitations  
 These are the implementation limitations:  
  
-   Function declaration in the XQuery prolog is not supported.  
  
-   Function import is not supported.  
  
## See Also  
 [XML Construction &#40;XQuery&#41;](../xquery/xml-construction-xquery.md)
 
