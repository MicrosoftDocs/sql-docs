---
description: "Level 2 API Functions (ODBC Driver for Oracle)"
title: "Level 2 API Functions (ODBC Driver for Oracle) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC level 2 API functions [ODBC]"
  - "functions [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], functions"
  - "ODBC API functions [ODBC]"
  - "API functions [ODBC]"
  - "level 2 API functions [ODBC]"
ms.assetid: d9f49520-72d7-4234-8635-260d0ce4199c
author: David-Engel
ms.author: v-davidengel
---
# Level 2 API Functions (ODBC Driver for Oracle)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 Functions at this level provide Level 1 interface conformance plus additional functionality such as support for bookmarks, dynamic parameters, and asynchronous execution of ODBC functions.  
  
|API function|Notes|  
|------------------|-----------|  
|**SQLBindParameter**|Associates a buffer with a parameter marker in an SQL statement.|  
|**SQLBrowseConnect**|Returns successive levels of attributes and attribute values.|  
|**SQLDataSources**|Lists data source names. Implemented by the Driver Manager.|  
|**SQLDescribeParam**|Returns the description of a parameter marker associated with a prepared SQL statement.<br /><br /> Returns a best guess of what the parameter is, based on parsing the statement. If the parameter type cannot be determined, SQL_VARCHAR returns with length 2000.|  
|**SQLDrivers**|Implemented by the Driver Manager.|  
|**SQLExtendedFetch**|Similar to **SQLFetch** but returns multiple rows using an array for each column. The result set is forward-scrollable and can be made backward-scrollable if the cursor is defined to be static, not forward-only. For forward-only cursors with default column binding, column data from data sets larger than the BUFFERSIZE connection attribute is fetched directly into data buffers. Does not support variable-length bookmarks and does not support fetching a rowset at an offset (other than 0) from a bookmark.|  
|**SQLForeignKeys**|Returns a list of foreign keys in a single table, or a list of foreign keys in other tables that refer to a single table.|  
|**SQLMoreResults**|Determines whether more results are pending on a statement handle, hstmt, containing SELECT, UPDATE, INSERT, or DELETE statements and if so, initializes processing for those results.<br /><br /> Oracle supports multiple result sets only from stored procedures, when using {resultset... } escape sequences.|  
|**SQLNativeSql**|For information about usage, see [Returning Array Parameters from Stored Procedures](../../odbc/microsoft/returning-array-parameters-from-stored-procedures.md).|  
|**SQLNumParams**|Returns the number of parameters in an SQL statement. The number of parameters should equal the number of question marks in the SQL statement passed to **SQLPrepare**.|  
|**SQLPrimaryKeys**|Returns the column names that comprise the primary key for a table.|  
|**SQLProcedureColumns**|Returns a list of input and output parameters, the return value, the columns in the result set of a single procedure, and two additional columns, OVERLOAD and ORDINAL_POSITION. OVERLOAD is the OVERLOAD column from the ALL_ARGUMENTS table of the Oracle Data Dictionary View. ORDINAL_POSITION is the SEQUENCE column from the ALL_ARGUMENTS table of the Oracle Data Dictionary View. For packaged procedures, the PROCEDURE NAME column is in *packagename.procedurename* format. Does not return the procedure columns of a created synonym that refers to a procedure or function.|  
|**SQLProcedures**|Returns a list of procedures in the data source. For packaged procedures, the PROCEDURE NAME column is in *packagename.procedurename* format.<br /><br /> Because Oracle does not provide a way to distinguish packaged procedures from packaged functions, the driver returns SQL_PT_UNKNOWN for the PROCEDURE_TYPE column.|  
|**SQLSetPos**|Sets the cursor position in a rowset. You can use **SQLSetPos** with **SQLGetData** to retrieve rows from unbound columns after positioning the cursor to a specific row in the rowset. Rows added to the result set using *fOption* SQL_ADD are added after the last row in the result set.|  
|**SQLSetScrollOptions**|Sets options that control the behavior of cursors associated with a statement handle, hstmt. For details, see [Cursor Type and Concurrency Combinations](../../odbc/microsoft/cursor-type-and-concurrency-combinations.md).|
