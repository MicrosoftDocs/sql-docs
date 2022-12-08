---
description: "SQLSetPos (Desktop Database Drivers)"
title: "SQLSetPos (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetPos function [ODBC], Desktop Database Drivers"
ms.assetid: 8ef027ec-8512-48fe-8fe2-2ff7cd81e331
author: David-Engel
ms.author: v-davidengel
---
# SQLSetPos (Desktop Database Drivers)
The bulk-model semantics for **SQLSetPos** calls with the *irow* argument equal to 0 are supported.  
  
 SQL_LOCK_NO_CHANGE is supported for *fLock*. SQL_LOCK_EXCLUSIVE and SQL_LOCK_UNLOCK are not supported.  
  
 **SQLSetPos** supports updatable joins. (For more information, see the *Microsoft Jet Database Engine Programmer's Guide*.)
