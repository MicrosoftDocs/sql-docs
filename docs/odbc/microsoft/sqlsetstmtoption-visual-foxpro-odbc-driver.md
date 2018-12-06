---
title: "SQLSetStmtOption (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetStmtOption function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 76b813e3-c7dc-4bb2-a710-d2aa9dcfdc36
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetStmtOption (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Level 1  
  
 Sets options related to a statement handle, *hstmt*.  
  
|*fOption*|Allowed values|Comments|  
|---------------|--------------------|--------------|  
|SQL_ASYNC_ENABLE|SQL_ASYNC_ENABLE_OFF|If you attempt to set this *fOption*, the driver returns the error: "Driver not capable". Visual FoxPro does not support asynchronous execution.|  
|SQL_BIND_TYPE|SQL_BIND_BY_COLUMN or a 32-bit value denoting the length of the structure or an instance of a buffer into which result columns will be bound.||  
|SQL_CONCURRENCY|SQL_CONCUR_READ_ONLY<br /><br /> SQL_CONCUR_LOCK<br /><br /> SQL_CONCUR_VALUES|The driver doesn't allow SQL_CONCUR_ROWVER, because Visual FoxPro does not have row versioning based on timestamps.|  
|SQL_CURSOR_TYPE|SQL_CURSOR_FORWARD_ONLY<br /><br /> SQL_CURSOR_STATIC|The driver does not allow SQL_CURSOR_KEYSET_DRIVEN or SQL_CURSOR_DYNAMIC; see [SQLSetScrollOptions](../../odbc/microsoft/sqlsetscrolloptions-visual-foxpro-odbc-driver.md) for more information.|  
|SQL_KEYSET_SIZE|Error: "Driver not capable"|Visual FoxPro does not support the keyset cursor model.|  
|SQL_MAX_LENGTH|0|If you attempt to set this *fOption* value, the driver returns the error "Driver not capable".|  
|SQL_MAX_ROWS|0|If you attempt to set this *fOption* value, the driver returns the error "Driver not capable".|  
|SQL_NOSCAN|SQL_NOSCAN_OFF||  
|SQL_QUERY_TIMEOUT|0|If you attempt to set this *fOption* value, the driver returns the error "Driver not capable".|  
|SQL_RETRIEVE_DATA|SQL_RD_ON, SQL_RD_OFF||  
|SQL_ROWSET_SIZE|1 to 4,294,967,296||  
|SQL_SIMULATE_CURSOR|Error: "Driver not capable"||  
|SQL_USE_BOOKMARKS|SQL_UB_OFF<br /><br /> SQL_UB_ON||  
  
 For more information, see [SQLSetStmtOption](../../odbc/reference/syntax/sqlsetstmtoption-function.md) in the *ODBC Programmer's Reference*.
