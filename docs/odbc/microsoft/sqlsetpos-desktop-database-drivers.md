---
title: "SQLSetPos (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQLSetPos function [ODBC], Desktop Database Drivers"
ms.assetid: 8ef027ec-8512-48fe-8fe2-2ff7cd81e331
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLSetPos (Desktop Database Drivers)
The bulk-model semantics for **SQLSetPos** calls with the *irow* argument equal to 0 are supported.  
  
 SQL_LOCK_NO_CHANGE is supported for *fLock*. SQL_LOCK_EXCLUSIVE and SQL_LOCK_UNLOCK are not supported.  
  
 **SQLSetPos** supports updatable joins. (For more information, see the *Microsoft Jet Database Engine Programmer's Guide*.)