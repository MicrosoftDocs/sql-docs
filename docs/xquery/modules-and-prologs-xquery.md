---
title: "Modules and Prologs (XQuery) | Microsoft Docs"
description: Learn which specifications are not supported when declaring a namespace in an XQuery prolog.
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "XQuery, prolog"
  - "prolog"
ms.assetid: 0f17b4a4-6234-41d4-a996-6db4e27bff7e
author: "rothja"
ms.author: "jroth"
---
# Modules and Prologs (XQuery)
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md) is a series of namespace declarations. In using the declare namespace in prolog, you can specify prefix to namespace binding and use the prefix in the query body.  
  
## Implementation Limitations  
 The following XQuery specifications are not supported in this implementation:  
  
-   Module declaration (`version`)  
  
-   Module declaration (`module namespace`)  
  
-   Xmpspacedeclaration (`xmlspace`)  
  
-   Default collation declaration (`declare default collation`)  
  
-   Base URI declaration (`declare base-uri`)  
  
-   Construction declaration (`declare construction`)  
  
-   Default ordering declaration (`declare ordering`)  
  
-   Schema import (`import schema namespace`)  
  
-   Module import (`import module`)  
  
-   Variable declaration in the prolog (`declare variable`)  
  
-   Function declaration (`declare function`)  
  
## In This Section  
 [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md)  
 Describes the XQuery prolog.  
  
## See Also  
 [XQuery Language Reference &#40;SQL Server&#41;](../xquery/xquery-language-reference-sql-server.md)  
  
  
