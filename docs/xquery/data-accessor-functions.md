---
title: "Data Accessor Functions | Microsoft Docs"
description: Learn how to use the XQuery data-accessor functions fn:data(), fn:string(), and text().
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "data-accessor functions [XQuery]"
ms.assetid: 31bad04f-7c74-4773-9f83-612704fdd21c
author: rothja
ms.author: jroth
---
# Data Accessor Functions
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  The topics in this section discuss and provide sample code for the data-accessor functions.  
  
## Understanding fn:data(), fn:string(), and text()  
 XQuery has a function **fn:data()** to extract scalar, typed values from nodes, a node test **text()** to return text nodes, and the function **fn:string()** that returns the string value of a node. Their use can be confusing. The following are guidelines for using them correctly in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The XML instance \<age>12\</age> is used for the purpose of illustration.  
  
-   Untyped XML: The path expression /age/text() returns the text node "12". The function fn:data(/age) returns the string value "12" and so does fn:string(/age).  
  
-   Typed XML: The expression /age/text() returns a static error for any simple typed \<age> element. On the other hand, fn:data(/age) returns integer 12. The fn:string(/age) yields the string "12".  
  
## In This Section  
  
-   [string Function &#40;XQuery&#41;](../xquery/data-accessor-functions-string-xquery.md)  
  
-   [data Function &#40;XQuery&#41;](../xquery/data-accessor-functions-data-xquery.md)  
  
## See Also  
 [Path Expressions &#40;XQuery&#41;](../xquery/path-expressions-xquery.md)  
  
  
