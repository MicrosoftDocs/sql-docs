---
description: "SQLGetFunctions (Cursor Library)"
title: "SQLGetFunctions (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLGetFunctions function [ODBC], Cursor Library"
ms.assetid: 931acd12-4eb6-4a78-9a77-157a18a9a2d0
author: David-Engel
ms.author: v-davidengel
---
# SQLGetFunctions (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLGetFunctions** function in the cursor library. For general information about **SQLGetFunctions**, see [SQLGetFunctions Function](../../../odbc/reference/syntax/sqlgetfunctions-function.md).  
  
 When you call **SQLGetFunctions**, the cursor library returns that it supports **SQLExtendedFetch**, **SQLFetchScroll**, **SQLSetPos**, and **SQLSetScrollOptions**, in addition to the functions supported by the driver.
