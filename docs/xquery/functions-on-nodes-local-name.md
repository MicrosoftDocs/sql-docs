---
title: "local-name Function (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:local-name function"
  - "local-name function"
ms.assetid: c901ef5d-89c5-482a-bf64-3eefbcf3098d
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Functions on Nodes - local-name
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns the local part of the name of *$arg* as an xs:string that will either be the zero-length string or will have the lexical form of an xs:NCName. If the argument is not provided, the default is the context node.  
  
## Syntax  
  
```  
fn:local-name() as xs:string  
fn:local-name($arg as node()?) as xs:string  
```  
  
## Arguments  
 *$arg*  
 Node name whose local-name part will be retrieved.  
  
## Remarks  
  
-   In SQL Server, **fn:local-name()** without an argument can only be used in the context of a context-dependent predicate. Specifically, it can only be used inside brackets (`[ ]`).  
  
-   If the argument is supplied and is the empty sequence, the function returns the zero-length string.  
  
-   If the target node has no name, because it is a document node, a comment, or a text node, the function returns the zero-length string.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Retrieve local name of a specific node  
 The following query is specified against an untyped XML instance. The query expression, `local-name(/ROOT[1])`, retrieves the local name part of the specified node.  
  
```  
declare @x xml  
set @x='<ROOT><a>111</a></ROOT>'  
SELECT @x.query('local-name(/ROOT[1])')  
-- result = ROOT  
```  
  
 The following query is specified against the Instructions column, a typed xml column, of the ProductModel table. The expression, `local-name(/AWMI:root[1]/AWMI:Location[1])`, returns the local name, `Location`, of the specified node.  
  
```  
SELECT Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" ;  
     local-name(/AWMI:root[1]/AWMI:Location[1])') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=7  
-- result = Location  
```  
  
### B. Using local-name without argument in a predicate  
 The following query is specified against the Instructions column, typed **xml** column, of the ProductModel table. The expression returns all the element children of the <`root`> element whose local name part of the QName is "Location". The **local-name()** function is specifed in the predicate and it has no arguments The context node is used by the function.  
  
```  
SELECT Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" ;  
  /AWMI:root//*[local-name() = "Location"]') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 The query returns all the <`Location`> element children of the <`root`> element.  
  
## See Also  
 [Functions on Nodes](https://msdn.microsoft.com/library/09a8affa-3341-4f50-aebc-fdf529e00c08)   
 [namespace-uri Function &#40;XQuery&#41;](../xquery/functions-on-nodes-namespace-uri.md)  
  
  
