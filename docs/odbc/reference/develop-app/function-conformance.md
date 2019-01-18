---
title: "Function Conformance | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "conformance levels [ODBC], function"
  - "function conformance levels [ODBC]"
  - "data sources [ODBC], conformance levels"
  - "ODBC drivers [ODBC], conformance levels"
ms.assetid: bb5d68cf-d238-481e-babc-2e9401b4700e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Function Conformance
The following table indicates the conformance level of each ODBC function, where this is well defined.  
  
|Function|Conformance level|  
|--------------|-----------------------|  
|**SQLAllocHandle**|Core|  
|**SQLBindCol**|Core|  
|**SQLBindParameter**|Core[1]|  
|**SQLBrowseConnect**|Level 1|  
|**SQLBulkOperations**|Level 1|  
|**SQLCancel**|Core[1]|  
|**SQLCloseCursor**|Core|  
|**SQLColAttribute**|Core[1]|  
|**SQLColumnPrivileges**|Level 2|  
|**SQLColumns**|Core|  
|**SQLConnect**|Core|  
|**SQLCopyDesc**|Core|  
|**SQLDataSources**|Core|  
|**SQLDescribeCol**|Core[1]|  
|**SQLDescribeParam**|Level 2|  
|**SQLDisconnect**|Core|  
|**SQLDriverConnect**|Core|  
|**SQLDrivers**|Core|  
|**SQLEndTran**|Core[1]|  
|**SQLExecDirect**|Core|  
|**SQLExecute**|Core|  
|**SQLFetch**|Core|  
|**SQLFetchScroll**|Core[1]|  
|**SQLForeignKeys**|Level 2|  
|**SQLFreeHandle**|Core|  
|**SQLFreeStmt**|Core|  
|**SQLGetConnectAttr**|Core|  
|**SQLGetCursorName**|Core|  
|**SQLGetData**|Core|  
|**SQLGetDescField**|Core|  
|**SQLGetDescRec**|Core|  
|**SQLGetDiagField**|Core|  
|**SQLGetDiagRec**|Core|  
|**SQLGetEnvAttr**|Core|  
|**SQLGetFunctions**|Core|  
|**SQLGetInfo**|Core|  
|**SQLGetStmtAttr**|Core|  
|**SQLGetTypeInfo**|Core|  
|**SQLMoreResults**|Level 1|  
|**SQLNativeSql**|Core|  
|**SQLNumParams**|Core|  
|**SQLNumResultCols**|Core|  
|**SQLParamData**|Core|  
|**SQLPrepare**|Core|  
|**SQLPrimaryKeys**|Level 1|  
|**SQLProcedureColumns**|Level 1|  
|**SQLProcedures**|Level 1|  
|**SQLPutData**|Core|  
|**SQLRowCount**|Core|  
|**SQLSetConnectAttr**|Core[2]|  
|**SQLSetCursorName**|Core|  
|**SQLSetDescField**|Core[1]|  
|**SQLSetDescRec**|Core|  
|**SQLSetEnvAttr**|Core[2]|  
|**SQLSetPos**|Level 1[1]|  
|**SQLSetStmtAttr**|Core[2]|  
|**SQLSpecialColumns**|Core[1]|  
|**SQLStatistics**|Core|  
|**SQLTablePrivileges**|Level 2|  
|**SQLTables**|Core|  
  
 [1]   Significant features of this function are available only at higher conformance levels.  
  
 [2]   Setting certain attributes to nondefault values depends on the conformance level. For more information, see the next section, [Attribute Conformance](../../../odbc/reference/develop-app/attribute-conformance.md).
