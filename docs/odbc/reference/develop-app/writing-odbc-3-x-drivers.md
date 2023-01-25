---
description: "Writing ODBC 3.x Drivers"
title: "Writing ODBC 3.x Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading drivers [ODBC]"
  - "ODBC drivers [ODBC], upgrading"
  - "backward compatibility [ODBC], drivers"
  - "compatibility [ODBC], drivers"
ms.assetid: 9b75f59b-623f-4711-9ca2-e751b3622e00
author: David-Engel
ms.author: v-davidengel
---
# Writing ODBC 3.x Drivers
The following table shows function support in an ODBC 3.*x* driver and an ODBC application, and the mapping performed by the Driver Manager when the functions are called against an ODBC 3.*x* driver.  
  
|Function|Supported<br /><br /> by an<br /><br /> ODBC 3.*x*<br /><br /> driver?|Supported<br /><br /> by an<br /><br /> ODBC 3.*x*<br /><br /> application?|Mapped/supported<br /><br /> by the ODBC 3.*x*<br /><br /> Driver Manager to<br /><br /> an ODBC 3.*x* driver?|  
|--------------|----------------------------------------------------|---------------------------------------------------------|---------------------------------------------------------------------------------------------|  
|**SQLAllocConnect**|No|No[1]|Yes|  
|**SQLAllocEnv**|No|No[1]|Yes|  
|**SQLAllocHandle**|Yes|Yes|No|  
|**SQLAllocStmt**|No|No[1]|Yes|  
|**SQLBindCol**|Yes|Yes|No|  
|**SQLBindParam**|No|Yes[2]|Yes|  
|**SQLBindParameter**|Yes|Yes|No|  
|**SQLBrowseConnect**|Yes|Yes|No|  
|**SQLBulkOperations**|Yes|Yes|No|  
|**SQLCancel**|Yes|Yes|No|  
|**SQLCloseCursor**|Yes|Yes|No|  
|**SQLColAttribute**|Yes|Yes|No|  
|**SQLColAttributes**|No[3]|No|Yes|  
|**SQLColumnPrivileges**|Yes|Yes|No|  
|**SQLColumns**|Yes|Yes|No|  
|**SQLConnect**|Yes|Yes|No|  
|**SQLCopyDesc**|Yes|Yes|Yes[4]|  
|**SQLDataSources**|No|Yes|Yes|  
|**SQLDescribeCol**|Yes|Yes|No|  
|**SQLDescribeParam**|Yes|Yes|No|  
|**SQLDisconnect**|Yes|Yes|No|  
|**SQLDriverConnect**|Yes|Yes|No|  
|**SQLDrivers**|No|Yes|Yes|  
|**SQLEndTran**|Yes|Yes|No|  
|**SQLError**|No|No[1]|Yes|  
|**SQLExecDirect**|Yes|Yes|No|  
|**SQLExecute**|Yes|Yes|No|  
|**SQLExtendedFetch**|Yes|No|No|  
|**SQLFetch**|Yes|Yes|No|  
|**SQLFetchScroll**|Yes|Yes|No|  
|**SQLForeignKeys**|Yes|Yes|No|  
|**SQLFreeConnect**|No|Yes[1]|Yes|  
|**SQLFreeEnv**|No|Yes[1]|Yes|  
|**SQLFreeHandle**|Yes|Yes|No|  
|**SQLFreeStmt**|Yes|Yes|No|  
|**SQLGetConnectAttr**|Yes|Yes|No|  
|**SQLGetConnectOption**|No[5]|No[1]|Yes|  
|**SQLGetCursorName**|Yes|Yes|No|  
|**SQLGetData**|Yes|Yes|No|  
|**SQLGetDescField**|Yes|Yes|No|  
|**SQLGetDescRec**|Yes|Yes|No|  
|**SQLGetDiagField**|Yes|Yes|No|  
|**SQLGetDiagRec**|Yes|Yes|No|  
|**SQLGetEnvAttr**|Yes|Yes|No|  
|**SQLGetFunctions**|No[6]|Yes|Yes|  
|**SQLGetInfo**|Yes|Yes|No|  
|**SQLGetStmtAttr**|Yes|Yes|No|  
|**SQLGetStmtOption**|No[5]|No[1]|Yes|  
|**SQLGetTypeInfo**|Yes|Yes|No|  
|**SQLMoreResults**|Yes|Yes|No|  
|**SQLNativeSql**|Yes|Yes|No|  
|**SQLNumParams**|Yes|Yes|No|  
|**SQLNumResultCols**|Yes|Yes|No|  
|**SQLParamData**|Yes|Yes|No|  
|**SQLParamOptions**|No|No|Yes|  
|**SQLPrepare**|Yes|Yes|No|  
|**SQLPrimaryKeys**|Yes|Yes|No|  
|**SQLProcedureColumns**|Yes|Yes|No|  
|**SQLProcedures**|Yes|Yes|No|  
|**SQLPutData**|Yes|Yes|No|  
|**SQLRowCount**|Yes|Yes|No|  
|**SQLSetConnectAttr**|Yes|Yes|No|  
|**SQLSetConnectOption**|No[5]|No[1]|Yes|  
|**SQLSetCursorName**|Yes|Yes|No|  
|**SQLSetDescField**|Yes|Yes|No|  
|**SQLSetDescRec**|Yes|Yes|No|  
|**SQLSetEnvAttr**|Yes|Yes|No|  
|**SQLSetPos**|Yes|Yes|No|  
|**SQLSetParam**|No|No|Yes|  
|**SQLSetScrollOption**|Yes|Yes|No|  
|**SQLSetStmtAttr**|Yes|Yes|No|  
|**SQLSetStmtOption**|No[5]|No[1]|Yes|  
|**SQLSpecialColumns**|Yes|Yes|No|  
|**SQLStatistics**|Yes|Yes|No|  
|**SQLTablePrivileges**|Yes|Yes|No|  
|**SQLTables**|Yes|Yes|No|  
|**SQLTransact**|No|No[1]|Yes|  
  
 [1]   This function is deprecated in ODBC 3.*x*. ODBC 3.*x* applications should not use this function. However, an Open Group or ISO CLI-compliant application can call this function.  
  
 [2]   ODBC 3.*x* applications should use **SQLBindParameter** instead of **SQLBindParam**. However, an Open Group or ISO CLI-compliant application can call this function.  
  
 [3]   Driver writers should note that the ODBC 2.*x* column attributes SQL_COLUMN_PRECISION, SQL_COLUMN_SCALE, and SQL_COLUMN_LENGTH must be supported with **SQLColAttribute**.  
  
 [4]   **SQLCopyDesc** is partially implemented by the Driver Manager when a descriptor is being copied across connections that belong to different drivers. Drivers are required to support **SQLCopyDesc** across two of their own connections. Functions such as **SQLDrivers**, which are implemented solely by the Driver Manager, do not show up on this list.  
  
 [5]   Under certain circumstances, drivers may need to support this function. For more information, see this function's reference page.  
  
 [6]   The driver can choose to support **SQLGetFunctions** if the set of functions that the driver supports varies from connection to connection.
