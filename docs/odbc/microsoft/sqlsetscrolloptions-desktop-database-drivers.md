---
description: "SQLSetScrollOptions (Desktop Database Drivers)"
title: "SQLSetScrollOptions (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetScrollOptions function [ODBC], Desktop Database Drivers"
ms.assetid: 51d643ed-015b-4639-969a-9491d9875aca
author: David-Engel
ms.author: v-davidengel
---
# SQLSetScrollOptions (Desktop Database Drivers)
Forward and static cursors are supported for SQL_CONCUR_READ_ONLY.  
  
 Only keyset-driven cursors are supported for an *fConcurrency* argument of SQL_CONCUR_LOCK.  
  
 An *fConcurrency* argument of SQL_CONCUR_ROWVER is not supported.  
  
 Dynamic cursors and mixed cursors are not supported.
