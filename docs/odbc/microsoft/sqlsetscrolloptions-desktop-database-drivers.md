---
title: "SQLSetScrollOptions (Desktop Database Drivers)"
description: "SQLSetScrollOptions (Desktop Database Drivers)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLSetScrollOptions function [ODBC], Desktop Database Drivers"
---
# SQLSetScrollOptions (Desktop Database Drivers)
Forward and static cursors are supported for SQL_CONCUR_READ_ONLY.  
  
 Only keyset-driven cursors are supported for an *fConcurrency* argument of SQL_CONCUR_LOCK.  
  
 An *fConcurrency* argument of SQL_CONCUR_ROWVER is not supported.  
  
 Dynamic cursors and mixed cursors are not supported.
