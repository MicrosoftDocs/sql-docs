---
title: "Modules and Prologs (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
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
  - "XQuery, prolog"
  - "prolog"
ms.assetid: 0f17b4a4-6234-41d4-a996-6db4e27bff7e
caps.latest.revision: 13
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Modules and Prologs (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

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
  
  