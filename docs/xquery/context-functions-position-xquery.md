---
title: "position Function (XQuery) | Microsoft Docs"
description: Learn about the XQuery function position() that returns an integer value indicating the position of a context item within a sequence of items.  
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "position function"
  - "fn:position function"
ms.assetid: f1bab9e4-1715-4c06-9cb0-06c7e0c9c97f
author: "rothja"
ms.author: "jroth"
---
# Context Functions - position (XQuery)
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

  Returns an integer value that indicates the position of the context item within the sequence of items currently being processed.  
  
## Syntax  
  
```  
  
fn:position() as xs:integer  
```  
  
## Remarks  
 In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], **fn:position()** can only be used in the context of a context-dependent predicate. Specifically, it can only be used inside brackets ([ ]).Comparing against this function does not reduce the cardinality during static type inference.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
### A. Using the position() XQuery function to retrieve the first two product features  
 The following query retrieves the first two features, the first two child elements of the <`Features`> element, from the product model catalog description. If there are more features, it adds a <`there-is-more/`> element to the result.  
  
```  
SELECT CatalogDescription.query('  
     declare namespace pd="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
     <Product>   
          { /pd:ProductDescription/@ProductModelID }  
          { /pd:ProductDescription/@ProductModelName }   
          {  
            for $f in /pd:ProductDescription/pd:Features/*[position()<=2]  
            return  
            $f   
          }  
          {  
            if (count(/pd:ProductDescription/pd:Features/*) > 2)  
            then <there-is-more/>  
            else ()  
          }   
     </Product>          
') as x  
FROM Production.ProductModel  
WHERE CatalogDescription is not null  
```  
  
 Note the following from the previous query:  
  
-   The **namespace** keyword in the [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md) defines a namespace prefix that is used in the query body.  
  
-   The query body constructs XML that has a \<Product> element with **ProductModelID** and **ProductModelName** attributes, and has product features returned as child elements.  
  
-   The **position()** function is used in the predicate to determine the position of the \<Features> child element in context. If it is the first or second feature, it is returned.  
  
-   The IF statement adds a \<there-is-more/> element to the result if there are more than two features in the product catalog.  
  
-   Because not all product models have their catalog descriptions stored in the table, the WHERE clause is used to discard rows where CatalogDescriptions is NULL.  
  
 This is a partial result:  
  
```  
<Product ProductModelID="19" ProductModelName="Mountain 100">  
  <p1:Warranty xmlns:p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
    <p1:WarrantyPeriod>3 year</p1:WarrantyPeriod>  
    <p1:Description>parts and labor</p1:Description>  
  </p1:Warranty>  
  <p2:Maintenance xmlns:p2="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
    <p2:NoOfYears>10</p2:NoOfYears>  
    <p2:Description>maintenance contact available through your dealer or  
                    any AdventureWorks retail store.</p2:Description>  
  </p2:Maintenance>  
  <there-is-more/>  
</Product>   
...  
```  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
