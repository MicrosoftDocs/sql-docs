---
title: "SQLSetConnectOption (Paradox Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetConnectOption function [ODBC], Paradox Driver"
  - "Paradox driver [ODBC], SQLSetConnectOption"
ms.assetid: 050ee2be-594e-4dbd-af67-8b6aae756cd1
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetConnectOption (Paradox Driver)
> [!NOTE]  
>  This topic provides Paradox Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|fOption|Comment|  
|-------------|-------------|  
|SQL_ACCESS_MODE|The SQL_ACCESS_MODE fOption can be set to either SQL_MODE_READ_ONLY or SQL_MODE_READ_WRITE. However, the driver does not prevent updates if SQL_ACCESS_MODE is set to SQL_MODE_READ_ONLY.|  
|SQL_AUTOCOMMIT|The Paradox driver only supports SQL_AUTOCOMMIT being set to ON (the default state), because they do not support transactions.|  
|SQL_CURRENT_QUALIFIER|Supported.|  
|SQL_LOGIN_TIMEOUT|Not supported.|  
|SQL_OPT_TRACE|Supported.|  
|SQL_OPT_TRACEFILE|Supported.|  
|SQL_PACKET_SIZE|Not supported.|  
|SQL_QUIET_MODE|Not supported.|  
|SQL_TRANSLATE_DLL|Not supported.|  
|SQL_TRANSLATION_OPTION|Not supported.|  
|SQL_TXN_ISOLATION|Not supported.|
