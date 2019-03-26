---
title: "Prepare and Execute a Statement (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "statement execution"
  - "statement preparation"
ms.assetid: 0adecc63-4da5-486c-bc48-09a004a2fae6
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Prepare and Execute a Statement (ODBC)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

    
### To prepare a statement once, and then execute it multiple times  
  
1.  Call [SQLPrepare Function](https://go.microsoft.com/fwlink/?LinkId=59360) to prepare the statement.  
  
2.  Optionally, call [SQLNumParams](https://go.microsoft.com/fwlink/?LinkId=58404) to determine the number of parameters in the prepared statement.  
  
3.  Optionally, for each parameter in the prepared statement:  
  
    -   Call [SQLDescribeParam](../../../relational-databases/native-client-odbc-api/sqldescribeparam.md) to get parameter information.  
  
    -   Bind each parameter to a program variable by using [SQLBindParameter](../../../relational-databases/native-client-odbc-api/sqlbindparameter.md). Set up any data-at-execution parameters.  
  
4.  For each execution of a prepared statement:  
  
    -   If the statement has parameter markers, put the data values into the bound parameter buffer.  
  
    -   Call [SQLExecute](https://go.microsoft.com/fwlink/?LinkId=58400) to execute the prepared statement.  
  
    -   If data-at-execution input parameters are used, [SQLExecute](https://go.microsoft.com/fwlink/?LinkId=58400) returns SQL_NEED_DATA. Send the data in chunks by using [SQLParamData](https://go.microsoft.com/fwlink/?LinkId=58405) and [SQLPutData](../../../relational-databases/native-client-odbc-api/sqlputdata.md).  
  
### To prepare a statement with column-wise parameter binding  
  
1.  Call [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) to set the following attributes:  
  
    -   Set SQL_ATTR_PARAMSET_SIZE to the number of sets (S) of parameters.  
  
    -   Set SQL_ATTR_PARAM_BIND_TYPE to SQL_PARAMETER_BIND_BY_COLUMN.  
  
    -   Set the SQL_ATTR_PARAMS_PROCESSED_PTR attribute to point to a SQLUINTEGER variable to hold the number of parameters processed.  
  
    -   Set SQL_ATTR_PARAMS_STATUS_PTR to point to an array[S] of SQLUSSMALLINT variables to hold parameter status indicators.  
  
2.  Call SQLPrepare to prepare the statement.  
  
3.  Optionally, call [SQLNumParams](https://go.microsoft.com/fwlink/?LinkId=58404) to determine the number of parameters in the prepared statement.  
  
4.  Optionally, for each parameter in the prepared statement, call SQLDescribeParam to get parameter information.  
  
5.  For each parameter marker:  
  
    -   Allocate an array of S parameter buffers to store data values.  
  
    -   Allocate an array of S parameter buffers to store data lengths.  
  
    -   Call SQLBindParameter to bind the parameter data value and data length arrays to the statement parameter.  
  
    -   If the parameter is a data-at-execution text or image parameter, set it up.  
  
    -   If any data-at-execution parameters are used, set them up.  
  
6.  For each execution of a prepared statement:  
  
    -   Put the S data values and S data lengths into the bound parameter arrays.  
  
    -   Call SQLExecute to execute the prepared statement.  
  
    -   If data-at-execution input parameters are used, SQLExecute returns SQL_NEED_DATA. Send the data in chunks by using SQLParamData and SQLPutData.  
  
### To prepare a statement with row-wise bound parameters  
  
1.  Allocate an array[S] of structures, where S is the number of sets of parameters. The structure has one element for each parameter, and each element has two parts:  
  
    -   The first part is a variable of the appropriate data type to hold the parameter data.  
  
    -   The second part is a SQLINTEGER variable to hold the status indicator.  
  
2.  Call [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) to set the following attributes:  
  
    -   Set SQL_ATTR_PARAMSET_SIZE to the number of sets (S) of parameters.  
  
    -   Set SQL_ATTR_PARAM_BIND_TYPE to the size of the structure allocated in Step 1.  
  
    -   Set the SQL_ATTR_PARAMS_PROCESSED_PTR attribute to point to a SQLUINTEGER variable to hold the number of parameters processed.  
  
    -   Set SQL_ATTR_PARAMS_STATUS_PTR to point to an array[S] of SQLUSSMALLINT variables to hold parameter status indicators.  
  
3.  Call SQLPrepare to prepare the statement.  
  
4.  For each parameter marker, call SQLBindParameter to point the parameter data value and data length pointer to their variables in the first element of the array of structures allocated in Step 1. If the parameter is a data-at-execution parameter, set it up.  
  
5.  For each execution of a prepared statement:  
  
    -   Fill the bound parameter buffer array with data values.  
  
    -   Call SQLExecute to execute the prepared statement. The driver efficiently executes the SQL statement S times, once for each set of parameters.  
  
    -   If data-at-execution input parameters are used, SQLExecute returns SQL_NEED_DATA. Send the data in chunks by using SQLParamData and SQLPutData.  
  
## See Also  
 [Executing Queries How-to Topics &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-how-to/execute-queries/executing-queries-how-to-topics-odbc.md)  
  
  
