---
title: "Mapping Deprecated Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "mapping deprecated functions [ODBC], about mapping deprecated functions"
  - "backward compatibility [ODBC], mapping deprecated functions"
  - "deprecated functions [ODBC]"
  - "compatibility [ODBC], mapping deprecated functions"
  - "functions [ODBC], mapping deprecated functions"
  - "mapping deprecated functions [ODBC]"
ms.assetid: ee462617-1d79-4c88-afeb-b129cff34cc6
author: MightyPen
ms.author: genemi
manager: craigg
---
# Mapping Deprecated Functions
This section describes how deprecated functions are mapped by the ODBC 3*.x* Driver Manager to guarantee backward compatibility of ODBC 3*.x* drivers that are used with ODBC 2.*x* applications. The Driver Manager performs this mapping regardless of the version of the application. Because each of the ODBC 2.*x* functions in the following list is mapped to the corresponding ODBC 3*.x* function when called in an ODBC 3*.x* driver, the ODBC 3*.x* driver does not have to implement the ODBC 2.*x* functions.  
  
 The mapping in the list is triggered when the driver is an ODBC 3*.x* driver and the driver does not support the function that is being mapped.  
  
 The following table lists all duplicated functionality that was introduced in ODBC 3*.x*.  
  
|ODBC 2.*x* function|ODBC 3*.x* function|  
|-------------------------|-------------------------|  
|**SQLAllocConnect**|**SQLAllocHandle**|  
|**SQLAllocEnv**|**SQLAllocHandle**|  
|**SQLAllocStmt**|**SQLAllocHandle**|  
|**SQLBindParam**[1]|**SQLBindParameter**|  
|**SQLColAttributes**|**SQLColAttribute**|  
|**SQLError**|**SQLGetDiagRec**|  
|**SQLFreeConnect**|**SQLFreeHandle**|  
|**SQLFreeEnv**|**SQLFreeHandle**|  
|**SQLFreeStmt** with an *Option* of SQL_DROP|**SQLFreeHandle**|  
|**SQLGetConnectOption**|**SQLGetConnectAttr**|  
|**SQLGetStmtOption**|**SQLGetStmtAttr**|  
|**SQLParamOptions**|**SQLSetStmtAttr**|  
|**SQLSetConnectOption**|**SQLSetConnectAttr**|  
|**SQLSetParam**[2]|**SQLBindParameter**|  
|**SQLSetScrollOption**|**SQLSetStmtAttr**|  
|**SQLSetStmtOption**|**SQLSetStmtAttr**|  
|**SQLTransact**|**SQLEndTran**|  
  
 [1]   Even though this function did not exist in ODBC 2*.x*, it is in the Open Group and ISO standards.  
  
 [2]   This is an ODBC 1.0 function.  
  
 This section contains the following topics.  
  
-   [SQLAllocConnect Mapping](../../../odbc/reference/appendixes/sqlallocconnect-mapping.md)  
  
-   [SQLAllocEnv Mapping](../../../odbc/reference/appendixes/sqlallocenv-mapping.md)  
  
-   [SQLAllocStmt Mapping](../../../odbc/reference/appendixes/sqlallocstmt-mapping.md)  
  
-   [SQLBindParam Mapping](../../../odbc/reference/appendixes/sqlbindparam-mapping.md)  
  
-   [SQLColAttributes Mapping](../../../odbc/reference/appendixes/sqlcolattributes-mapping.md)  
  
-   [SQLError Mapping](../../../odbc/reference/appendixes/sqlerror-mapping.md)  
  
-   [SQLFreeConnect Mapping](../../../odbc/reference/appendixes/sqlfreeconnect-mapping.md)  
  
-   [SQLFreeEnv Mapping](../../../odbc/reference/appendixes/sqlfreeenv-mapping.md)  
  
-   [SQLFreeStmt Mapping](../../../odbc/reference/appendixes/sqlfreestmt-mapping.md)  
  
-   [SQLGetConnectOption Mapping](../../../odbc/reference/appendixes/sqlgetconnectoption-mapping.md)  
  
-   [SQLGetStmtOption Mapping](../../../odbc/reference/appendixes/sqlgetstmtoption-mapping.md)  
  
-   [SQLInstallTranslator Mapping](../../../odbc/reference/appendixes/sqlinstalltranslator-mapping.md)  
  
-   [SQLParamOptions Mapping](../../../odbc/reference/appendixes/sqlparamoptions-mapping.md)  
  
-   [SQLSetConnectOption Mapping](../../../odbc/reference/appendixes/sqlsetconnectoption-mapping.md)  
  
-   [SQLSetParam Mapping](../../../odbc/reference/appendixes/sqlsetparam-mapping.md)  
  
-   [SQLSetScrollOptions Mapping](../../../odbc/reference/appendixes/sqlsetscrolloptions-mapping.md)  
  
-   [SQLSetStmtOption Mapping](../../../odbc/reference/appendixes/sqlsetstmtoption-mapping.md)  
  
-   [SQLTransact Mapping](../../../odbc/reference/appendixes/sqltransact-mapping.md)
