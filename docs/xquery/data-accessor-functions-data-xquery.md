---
title: "data Function (XQuery) | Microsoft Docs"
description: Learn how to use the XQuery function data() to return the typed value for each item in a specified sequence of items.
ms.custom: ""
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:data function"
  - "data function [XQuery]"
ms.assetid: 511b5d7d-c679-4cb2-a3dd-170cc126f49d
author: "rothja"
ms.author: "jroth"
---
# Data Accessor Functions - data (XQuery)
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns the typed value for each item specified by *$arg*.  
  
## Syntax  
  
```  
  
fn:data ($arg as item()*) as xdt:untypedAtomic*  
```  
  
## Arguments  
 *$arg*  
 Sequence of items whose typed values will be returned.  
  
## Remarks  
 The following applies to typed values:  
  
-   The typed value of an atomic value is the atomic value.  
  
-   The typed value of a text node is the string value of the text node.  
  
-   The typed value of a comment is the string value of the comment.  
  
-   The typed value of a processing instruction is the content of the processing-instruction, without the processing instruction target name.  
  
-   The typed value of a document node is its string value.  
  
 The following applies to attribute and element nodes:  
  
-   If an attribute node is typed with an XML schema type, its typed value is the typed value, accordingly.  
  
-   If the attribute node is untyped, its typed value is equal to its string value that is returned as an instance of **xdt:untypedAtomic**.  
  
-   If the element node has not been typed, its typed value is equal to its string value that is returned as an instance of **xdt:untypedAtomic**.  
  
 The following applies to typed element nodes:  
  
-   If the element has a simple content type, **data()** returns the typed value of the element.  
  
-   If the node is of complex type, including xs:anyType, **data()** returns a static error.  
  
 Although using the **data()** function is frequently optional, as shown in the following examples, specifying the **data()** function explicitly increases query readability. For more information, see [XQuery Basics](../xquery/xquery-basics.md).  
  
 You cannot specify **data()** on constructed XML, as shown in the following:  
  
```  
declare @x xml  
set @x = ''  
select @x.query('data(<SomeNode>value</SomeNode>)')  
```  
  
## Examples  
 This topic provides XQuery examples against XML instances stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the data() XQuery function to extract typed value of a node  
 The following query illustrates how the **data()** function is used to retrieve values of an attribute, an element, and a text node:  
  
```  
WITH XMLNAMESPACES (  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS p1,  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)  
  
SELECT CatalogDescription.query(N'  
 for $pd in //p1:ProductDescription  
 return   
    <Root   
      ProductID = "{ data( ($pd//@ProductModelID)[1] ) }"   
      Feature =   "{ data( ($pd/p1:Features/wm:Warranty/wm:Description)[1] ) }" >  
    </Root>  
 ') as Result  
FROM Production.ProductModel  
WHERE ProductModelID = 19  
```  
  
 This is the result:  
  
```  
<Root ProductID="19" Feature="parts and labor"/>  
```  
  
 As mentioned, the **data()** function is optional when you are constructing attributes. If you do not specify the **data()** function, it is implicitly assumed. The following query produces the same results as the previous query:  
  
```  
WITH XMLNAMESPACES (  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS p1,  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)  
  
SELECT CatalogDescription.query('  
      for $pd in //p1:ProductDescription  
         return   
          <Root    
                ProductID = "{ ($pd/@ProductModelID)[1] }"    
                Feature =   "{ ($pd/p1:Features/wm:Warranty/wm:Description)[1] }" >  
           </Root>  
 ') as Result  
FROM Production.ProductModel  
WHERE ProductModelID = 19  
```  
  
 The following examples illustrate instances in which the **data()** function is required.  
  
 In the following query, **$pd/p1:Specifications/Material** returns the <`Material`> element. Also, **data($pd/p1:Specifications/ Material)** returns character data typed as xdt:untypedAtomic, because <`Material`> is untyped. When the input is untyped, the result of **data()** is typed as **xdt:untypedAtomic**.  
  
```  
SELECT CatalogDescription.query('  
declare namespace p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
      for $pd in //p1:ProductDescription  
         return   
          <Root>  
             { $pd/p1:Specifications/Material }  
             { data($pd/p1:Specifications/Material) }  
           </Root>  
 ') as Result  
FROM Production.ProductModel  
WHERE ProductModelID = 19  
```  
  
 This is the result:  
  
```  
<Root>  
  <Material>Almuminum Alloy</Material>Almuminum Alloy  
</Root>  
```  
  
 In the following query, **data($pd/p1:Features/wm:Warranty)** returns a static error, because <`Warranty`> is a complex type element.  
  
```  
WITH XMLNAMESPACES (  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS p1,  
'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)  
  
SELECT CatalogDescription.query('  
 <Root>  
   {     /p1:ProductDescription/p1:Features/wm:Warranty }  
   { data(/p1:ProductDescription/p1:Features/wm:Warranty) }  
 </Root>  
 ') as Result  
FROM  Production.ProductModel  
WHERE ProductModelID = 23  
```  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
