---
description: "Header Files"
title: "Header Files | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "header files [ODBC]"
ms.assetid: b4a03273-5e30-4d7b-826e-02f8f28ba078
author: David-Engel
ms.author: v-davidengel
---
# Header Files
The Sql.h header file contains prototypes for the functions and features in the Core ODBC Interface conformance level. The Sqlext.h header file contains prototypes for the functions and features in the Level 1 and Level 2 API conformance levels. The Sqltypes.h header file contains type definitions and indicators for the SQL data types.  
  
 The header files all contain a **#define**, ODBCVER, that an application or driver can set to be compiled for different versions of ODBC.  
  
 To align with the ISO CLI and Open Group CLI, the header files contain aliases for the information types used in calls to **SQLGetInfo**. In the following table, the column "ODBC name" indicates the ODBC name for the information type in [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md). The column "Alias in header file" indicates the name that is used in the ISO CLI and the Open Group CLI. The actual numeric value of these manifest names is the same in both ODBC and the standard CLIs. These aliases enable a standards-compliant application or driver to compile with the ODBC *3.x* header files.  
  
 These aliases include expansions of abbreviations in the ODBC names so that the names are more understandable. "MAX" is expanded to "MAXIMUM", "LEN" to "LENGTH", "MULT" to "MULTIPLE", "OJ" to "OUTER_JOIN", and "TXN" to "TRANSACTION."  
  
|ODBC name|Alias in header file|  
|---------------|--------------------------|  
|SQL_MAX_CATALOG_NAME_LEN|SQL_MAXIMUM_CATALOG_NAME_LENGTH|  
|SQL_MAX_COLUMN_NAME_LEN|SQL_MAXIMUM_COLUMN_NAME_LENGTH|  
|SQL_MAX_COLUMNS_IN_GROUP_BY|SQL_MAXIMUM_COLUMNS_IN_GROUP_BY|  
|SQL_MAX_COLUMNS_IN_ORDER_BY|SQL_MAXIMUM_COLUMNS_IN_ORDER_BY|  
|SQL_MAX_COLUMNS_IN_SELECT|SQL_MAXIMUM_COLUMNS_IN_SELECT|  
|SQL_MAX_COLUMNS_IN_TABLE|SQL_MAXIMUM_COLUMNS_IN_TABLE|  
|SQL_MAX_CONCURRENT_ACTIVITIES|SQL_MAXIMUM_CONCURRENT_ACTIVITIES|  
|SQL_MAX_CURSOR_NAME_LEN|SQL_MAXIMUM_CURSOR_NAME_LENGTH|  
|SQL_MAX_DRIVER_CONNECTIONS|SQL_MAXIMUM_DRIVER_CONNECTIONS|  
|SQL_MAX_IDENTIFIER_LEN|SQL_MAXIMUM_IDENTIFIER_LENGTH|  
|SQL_MAX_SCHEMA_NAME_LEN|SQL_MAXIMUM_SCHEMA_NAME_LENGTH|  
|SQL_MAX_STATEMENT_LEN|SQL_MAXIMUM_STATEMENT_LENGTH|  
|SQL_MAX_TABLE_NAME_LEN|SQL_MAXIMUM_TABLE_NAME_LENGTH|  
|SQL_MAX_TABLES_IN_SELECT|SQL_MAXIMUM_TABLES_IN_SELECT|  
|SQL_MAX_USER_NAME_LEN|SQL_MAXIMUM_USER_NAME_LENGTH|  
|SQL_MULT_RESULT_SETS|SQL_MULTIPLE_RESULT_SETS|  
|SQL_OJ_CAPABILITIES|SQL_OUTER_JOIN_CAPABILITIES|  
|SQL_TXN_CAPABLE|SQL_TRANSACTION_CAPABLE|  
|SQL_TXN_ISOLATION_OPTION|SQL_TRANSACTION_ISOLATION_OPTION|
