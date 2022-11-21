---
description: "Level 1 API Functions (ODBC Driver for Oracle)"
title: "Level 1 API Functions (ODBC Driver for Oracle) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "functions [ODBC], ODBC driver for Oracle"
  - "ODBC level 1 API functions [ODBC]"
  - "ODBC driver for Oracle [ODBC], functions"
  - "level 1 API functions [ODBC]"
  - "API functions [ODBC]"
ms.assetid: 98cced6f-41b8-43c1-a3cd-f4ea1615c0af
author: David-Engel
ms.author: v-davidengel
---
# Level 1 API Functions (ODBC Driver for Oracle)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 Functions at this level provide Core interface conformance plus additional functionality such as transaction support.  
  
|API function|Notes|  
|------------------|-----------|  
|**SQLColumns**|Creates a result set for a table, which is the column list for the specified table or tables. When you request columns for a PUBLIC synonym, you must have set the SYNONYMCOLUMNS connection attribute and specified an empty string as the *szTableOwner* argument. When returning columns for PUBLIC synonyms, the driver sets the TABLE NAME column to an empty string. The result set contains an additional column, ORDINAL POSITION, at the end of each row. This value is the ordinal position of the column in the table.|  
|**SQLDriverConnect**|Connects to an existing data source. For details, see [Connection String Format and Attributes](../../odbc/microsoft/connection-string-format-and-attributes.md).|  
|**SQLGetConnectOption**|Returns the current setting of a connection option. This function is partially supported. The driver supports all values for the *fOption* argument but does not support some *vParam* values for the *fOption* argument [SQL_TXN_ISOLATION](../../odbc/microsoft/connect-options.md). For more information, see [Connect Options](../../odbc/microsoft/connect-options.md).|  
|**SQLGetData**|Retrieves the value of a single field in the current record of the given result set.|  
|**SQLGetFunctions**|Returns TRUE for all supported functions. Implemented by the Driver Manager.|  
|**SQLGetInfo**|Returns information, including SQLHDBC, SQLUSMALLINT, SQLPOINTER, SQLSMALLINT, and SQLSMALLINT \*, about the ODBC Driver for Oracle and data source associated with a connection handle, *hdbc*.|  
|**SQLGetStmtOption**|Returns the current setting of a statement option. For more information, see [Statement Options](../../odbc/microsoft/statement-options.md).|  
|**SQLGetTypeInfo**|Returns information about the data types supported by a data source. The driver returns the information in an SQL result set.|  
|**SQLParamData**|Used in conjunction with **SQLPutData** to specify parameter data at statement execution time.|  
|**SQLPutData**|Allows an application to send data for a parameter or column to the driver at statement execution time.|  
|**SQLSetConnectOption**|Provides access to options that govern aspects of the connection. This function is partially supported: The driver supports all values for the *fOption* argument but does not support some *vParam* values for the *fOption* argument [SQL_TXN_ISOLATION](../../odbc/microsoft/connect-options.md). For more information, see [Connect Options](../../odbc/microsoft/connect-options.md).|  
|**SQLSetStmtOption**|Sets options related to a statement handle, *hstmt*. For more information, see [Statement Options](../../odbc/microsoft/statement-options.md).|  
|**SQLSpecialColumns**|Retrieves the optimal set of columns that uniquely identifies a row in the table.|  
|**SQLStatistics**|Retrieves a list of statistics about a single table and the indexes, or tag names, associated with the table. The driver returns the information as a result set.|  
|**SQLTables**|Returns the list of table names specified by the parameter in the **SQLTables** statement. If no parameter is specified, returns the table names stored in the current data source. The driver returns the information as a result set.<br /><br /> Enumeration type calls will not receive a result set entry for remote views or local parameterized views. However, a call to **SQLTables** with a unique table name specifier will find a match for such a view, if present, with that name; this allows the API to check for name conflicts prior to creation of a new table.<br /><br /> PUBLIC synonyms are returned with a TABLE_OWNER value of "".<br /><br /> VIEWS owned by SYS or SYSTEM are identified as SYSTEM VIEW.|
