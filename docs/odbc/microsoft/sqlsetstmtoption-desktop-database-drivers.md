---
title: "SQLSetStmtOption (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetStmtOption function [ODBC], Desktop Database Drivers"
ms.assetid: 98db9631-eb9b-4962-abe4-96f495133a5d
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetStmtOption (Desktop Database Drivers)
|*fOption*|Comments|  
|---------------|--------------|  
|SQL_ASYNC_ENABLE|Asynchronous processing is not supported. The SQL_ASYNC_ENABLE fOption will return SQLSTATE S1C00 (Driver not capable).|  
|SQL_KEYSET_SIZE|The only valid keyset size is 0, because mixed and dynamic cursors are not supported. If this value is set to any other number, it will be changed to 0 and the call will return SQL_SUCCESS_WITH_INFO and SQLSTATE 01S02 (Option value changed).|  
|SQL_MAX_ROWS|The only valid rowset size is 0, because the Desktop Database Drivers do not support limiting the number of rows that are returned. If this value is set to any other number, it will be changed to 0 and the call will return SQL_SUCCESS_WITH_INFO and SQLSTATE 01S02 (Option value changed).|  
|SQL_QUERY_TIMEOUT|Not supported.|  
|SQL_ROW_NUMBER|Not supported.|  
|SQL_SIMULATE_CURSOR|Not supported.|
