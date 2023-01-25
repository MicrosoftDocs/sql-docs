---
description: "SQLSetConnectOption (Access Driver)"
title: "SQLSetConnectOption (Access Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Access driver [ODBC], SQLSetConnectOption"
  - "SQLSetConnectOption function [ODBC], Access Driver"
ms.assetid: 58399bc4-d0b1-4eaa-a474-c92b2d5855ea
author: David-Engel
ms.author: v-davidengel
---
# SQLSetConnectOption (Access Driver)
> [!NOTE]  
>  This topic provides Access Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|fOption|Comment|  
|-------------|-------------|  
|SQL_ACCESS_MODE|The SQL_ACCESS_MODE fOption can be set to either SQL_MODE_READ_ONLY or SQL_MODE_READ_WRITE. However, the driver does not prevent updates if SQL_ACCESS_MODE is set to SQL_MODE_READ_ONLY.|  
|SQL_AUTOCOMMIT|When the Microsoft Access driver is used, the SQL_AUTOCOMMIT option may be set to either SQL_AUTOCOMMIT_ON or SQL_AUTOCOMMIT_OFF, because the Microsoft Access driver supports transactions[1].|  
|SQL_CURRENT_QUALIFIER|Supported.|  
|SQL_LOGIN_TIMEOUT|Not supported.|  
|SQL_OPT_TRACE|Supported.|  
|SQL_OPT_TRACEFILE|Supported.|  
|SQL_PACKET_SIZE|Not supported.|  
|SQL_QUIET_MODE|Not supported.|  
|SQL_TRANSLATE_DLL|Not supported.|  
|SQL_TRANSLATION_OPTION|Not supported.|  
|SQL_TXN_ISOLATION|SQL_TXN_ISOLATION is always SQL_TXN_READ_COMMITTED.|  
  
 [1]   Atomic transactions are not supported by the Microsoft Access driver. When committing a transaction using the Microsoft Access driver, a finite delay exists between the time the transaction is committed and the time the values are written to disk. This delay is determined by a delay inherent in the Microsoft Jet engine. The page timeout will not be less than a minimum value, even if the PageTimeout option is set below that value. As a result, there is no guarantee that committed data is stable, since changes may be made during the delay.
