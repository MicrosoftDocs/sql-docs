---
title: "Modules and Prologs (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "XQuery, prolog"
  - "prolog"
ms.assetid: 0f17b4a4-6234-41d4-a996-6db4e27bff7e
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Modules and Prologs (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

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
  
  
