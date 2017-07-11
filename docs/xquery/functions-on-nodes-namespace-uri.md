---
title: "namespace-uri Function (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "SQL Server"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:namespace-uri function"
  - "namespace-uri function"
ms.assetid: 9b48d216-26c8-431d-9ab4-20ab187917f4
caps.latest.revision: 14
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Functions on Nodes - namespace-uri
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the namespace URI of the QName specified in *$arg* as a xs:string.  
  
## Syntax  
  
```  
fn:namespace-uri() as xs:string  
fn:namespace-uri($arg as node()?) as xs:string  
```  
  
## Arguments  
 *$arg*  
 Node name whose namespace URI part will be retrieved.  
  
## Remarks  
  
-   If the argument is omitted, the default is the context node.  
  
-   In SQL Server, **fn:namespace-uri()** without an argument can only be used in the context of a context-dependent predicate. Specifically, it can only be used inside brackets ([ ]).  
  
-   If *$arg* is the empty sequence, the zero-length string is returned.  
  
-   If *$arg* is an element or attribute node whose expanded-QName is not in a namespace, the function returns the zero-length string  
  
## Examples  
 This topic provides XQuery examples against XML instances stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Retrieve namespace URI of a specific node  
 The following query is specified against an untyped XML instance. The query expression, `namespace-uri(/ROOT[1])`, retrieves the namespace URI part of the specified node.  
  
```  
set @x='<ROOT><a>111</a></ROOT>'  
SELECT @x.query('namespace-uri(/ROOT[1])')  
```  
  
 Because the specified QName does not have the namespace URI part but only the local name part, the result is a zero-length string.  
  
 The following query is specified against the Instructions typed **xml** column. The expression, `namespace-uri(/AWMI:root[1]/AWMI:Location[1])`, returns the namespace URI of the first <`Location`> element child of the <`root`> element.  
  
```  
SELECT Instructions.query('  
declare namespace AWMI="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" ;  
     namespace-uri(/AWMI:root[1]/AWMI:Location[1])') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 This is the result:  
  
```  
http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions  
```  
  
### B. Using namespace-uri() without argument in a predicate  
 The following query is specified against the CatalogDescription typed xml column. The expression returns all the element nodes whose namespace URI is `http://www.adventure-works.com/schemas/OtherFeatures`. The namespace-**uri()** function is specified without an argument and uses the context node.  
  
```  
SELECT CatalogDescription.query('  
declare namespace p1="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
   /p1:ProductDescription//*[namespace-uri() = "http://www.adventure-works.com/schemas/OtherFeatures"]  
') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 This is partial result:  
  
```  
<p1:wheel xmlns:p1="http://www.adventure-works.com/schemas/OtherFeatures">High performance wheels.</p1:wheel>  
<p2:saddle xmlns:p2="http://www.adventure-works.com/schemas/OtherFeatures">  
  <p3:i xmlns:p3="http://www.w3.org/1999/xhtml">Anatomic design</p3:i> and made from durable leather for a full-day of riding in comfort.</p2:saddle>  
…  
```  
  
 You can change the namespace URI in the previous query to `http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain`. You will then receive all the element node children of the <`ProductDescription`> element whose namespace URI part of the expanded QName is `http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain`.  
  
### Implementation Limitations  
 These are the limitations:  
  
-   The **namespace-uri()** function returns instances of type xs:string instead of xs:anyURI.  
  
## See Also  
 [Functions on Nodes](http://msdn.microsoft.com/library/09a8affa-3341-4f50-aebc-fdf529e00c08)   
 [local-name Function &#40;XQuery&#41;](../xquery/functions-on-nodes-local-name.md)  
  
  
