---
description: "Assigning Storage"
title: "Assigning Storage | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "column-wise binding [ODBC]"
  - "SQL Server Native Client ODBC driver, result sets"
  - "ODBC applications, result sets"
  - "SQLBindCol function"
  - "storing data [ODBC]"
  - "result sets [ODBC], storage"
  - "SQL Server Native Client ODBC driver, data storage"
  - "row-wise binding"
  - "binding result sets [SQL Server Native Client]"
  - "array binding"
ms.assetid: 11c81955-5300-495f-925f-9256f2587b58
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Assigning Storage
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  An application can assign storage for results before or after it executes a SQL statement. If an application prepares or executes the SQL statement first, it can inquire about the result set before it assigns storage for results. For example, if the result set is unknown, the application must retrieve the number of columns before it can assign storage for them.  
  
 To associate storage for a column of data, an application calls [SQLBindCol](../../relational-databases/native-client-odbc-api/sqlbindcol.md)and passes it:  
  
-   The data type to which the data is to be converted.  
  
-   The address of an output buffer for the data.  
  
     The application must allocate this buffer, and it must be large enough to hold the data in the form to which it is converted.  
  
-   The length of the output buffer.  
  
     This value is ignored if the returned data has a fixed width in C, such as an integer, real number, or date structure.  
  
-   The address of a storage buffer in which to return the number of bytes of available data.  
  
 An application can also bind result set columns to arrays of program variables to support fetching result set rows in blocks. There are two different types of array binding:  
  
-   Column-wise binding is finished when each column is bound to its own array of variables.  
  
     Column-wise binding is specified by calling [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) with *Attribute* set to SQL_ATTR_ROW_BIND_TYPE and *ValuePtr* set to SQL_BIND_BY_COLUMN. All the arrays must have the same number of elements.  
  
-   Row-wise binding is finished when all the parameters in the SQL statement are bound as a unit to an array of structures that contain the individual variables for the parameters.  
  
     Row-wise binding is specified by calling **SQLSetStmtAttr** with *Attribute* set to SQL_ATTR_ROW_BIND_TYPE and *ValuePtr* set to the size of the structure holding the variables that will receive the result set columns.  
  
 The application also sets SQL_ATTR_ROW_ARRAY_SIZE to the number of elements in the column or row arrays and sets SQL_ATTR_ROW_STATUS_PTR and SQL_ATTR_ROWS_FETCHED_PTR.  
  
## See Also  
 [Processing Results &#40;ODBC&#41;](../../relational-databases/native-client-odbc-results/processing-results-odbc.md)  
  
  
