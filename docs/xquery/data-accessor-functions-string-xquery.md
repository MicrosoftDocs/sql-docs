---
title: "string Function (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "string function"
  - "fn:string function"
ms.assetid: 7baa2959-9340-429b-ad53-3df03d8e13fc
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Data Accessor Functions - string (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns the value of *$arg* represented as a string.  
  
## Syntax  
  
```  
  
fn:string() as xs:string  
fn:string($arg as item()?) as xs:string  
```  
  
## Arguments  
 *$arg*  
 Is a node or an atomic value.  
  
## Remarks  
  
-   If *$arg* is the empty sequence, the zero-length string is returned.  
  
-   If *$arg* is a node, the function returns the string value of the node that is obtained by using the string-value accessor. This is defined in the W3C XQuery 1.0 and XPath 2.0 Data Model specification.  
  
-   If *$arg* is an atomic value, the function returns the same string that is returned by the expression cast as **xs:string**, *$arg*, except when noted otherwise.  
  
-   If the type of *$arg* is **xs:anyURI**, the URI is converted to a string without escaping special characters.  
  
-   Inthis implementation, **fn:string()** without an argument can only be used in the context of a context-dependent predicate. Specifically, it can only be used inside brackets ([ ]).  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the string function  
 The following query retrieves the <`Features`> child element node of the <`ProductDescription`> element.  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
 /PD:ProductDescription/PD:Features  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 This is the partial result:  
  
```  
<PD:Features xmlns:PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">  
   These are the product highlights.   
   <p1:Warranty xmlns:p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
    <p1:WarrantyPeriod>3 years</p1:WarrantyPeriod>  
    <p1:Description>parts and labor</p1:Description>  
   </p1:Warranty>  
       ...  
</PD:Features>  
```  
  
 If you specify the **string()** function, you receive the string value of the specified node.  
  
```  
SELECT CatalogDescription.query('  
declare namespace PD="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
 string(/PD:ProductDescription[1]/PD:Features[1])  
')  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 This is the partial result.  
  
```  
These are the product highlights.   
3 yearsparts and labor...    
```  
  
### B. Using the string function on various nodes  
 In the following example, an XML instance is assigned to an xml type variable. Queries are specified to illustrate the result of applying **string()** to various nodes.  
  
```  
declare @x xml  
set @x = '<?xml version="1.0" encoding="UTF-8" ?>  
<!--  This is a comment -->  
<root>  
  <a>10</a>  
just text  
  <b attr="x">20</b>  
</root>  
'  
```  
  
 The following query retrieves the string value of the document node. This value is formed by concatenating the string value of all its descendent text nodes.  
  
```  
select @x.query('string(/)')  
```  
  
 This is the result:  
  
```  
This is a comment 10  
just text  
 20  
```  
  
 The following query tries to retrieve the string value of a processing instruction node. The result is an empty sequence, because it does not contain a text node.  
  
```  
select @x.query('string(/processing-instruction()[1])')  
```  
  
 The following query retrieves the string value of the comment node and returns the text node.  
  
```  
select @x.query('string(/comment()[1])')  
```  
  
 This is the result:  
  
```  
This is a comment   
```  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
