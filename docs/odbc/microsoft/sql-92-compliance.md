---
description: "SQL-92 Compliance"
title: "SQL-92 Compliance | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Jet-based ODBC drivers [ODBC], SQL-92 compliance"
  - "desktop database drivers [ODBC], SQL-92 compliance"
  - "SQL-92 compliance [ODBC]"
  - "ODBC desktop database drivers [ODBC], SQL-92 compliance"
ms.assetid: 50c8c7df-df01-4f4d-ad62-d059cf29d73a
author: David-Engel
ms.author: v-davidengel
---
# SQL-92 Compliance
The ODBC Desktop Database Drivers and the underlying Microsoft Jet engine are not SQL-92 compliant. They support many features that have been defined in SQL-92. Some features supported in the driver are not supported in SQL-92. For more information, see the *Microsoft Jet Database Engine Programmer's Guide*. The following are the major differences between the two:  
  
-   The SQL used by the Desktop Database Drivers supports more powerful expressions than those specified by SQL-92.  
  
-   Different rules apply to the BETWEEN predicate.  
  
-   The SQL used by the Desktop Database Drivers and ANSI SQL supports different keywords.  
  
 The following SQL-92 features are not supported by Microsoft Jet SQL:  
  
-   Security statements, such as GRANT and LOCK.  
  
-   DISTINCT with aggregate function references.  
  
 The following features are enhancements in the SQL used by the Desktop Database Drivers that are not specified by SQL-92:  
  
-   The TRANSFORM statement providing support for crosstab queries.  
  
-   Additional aggregate functions (**StDev** and **VarP**).  
  
> [!NOTE]  
>  The Desktop Database Drivers support the standard ANSI syntax for % (percent) and _ (underscore), not * (asterisk) and ? (question mark).
