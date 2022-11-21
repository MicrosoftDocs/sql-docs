---
description: "Execute a Statement Directly (ODBC)"
title: "Execute a Statement Directly (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "statement execution"
ms.assetid: b690f9de-66e1-4ee5-ab6a-121346fb5f85
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Execute a Statement Directly (ODBC)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

    
### To execute a statement directly and one time only  
  
1.  If the statement has parameter markers, use [SQLBindParameter](../../../relational-databases/native-client-odbc-api/sqlbindparameter.md) to bind each parameter to a program variable. Fill the program variables with data values, and then set up any data-at-execution parameters.  
  
2.  Call [SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md) to execute the statement.  
  
3.  If data-at-execution input parameters are used, [SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md) returns SQL_NEED_DATA. Send the data in chunks by using [SQLParamData](../../../odbc/reference/syntax/sqlparamdata-function.md) and [SQLPutData](../../../relational-databases/native-client-odbc-api/sqlputdata.md).  

### To execute a statement multiple times by using column-wise parameter binding  
  
1.  Call [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) to set the following attributes:  
  
     Set SQL_ATTR_PARAMSET_SIZE to the number of sets (S) of parameters.  
  
     Set SQL_ATTR_PARAM_BIND_TYPE to SQL_PARAMETER_BIND_BY_COLUMN.  
  
     Set the SQL_ATTR_PARAMS_PROCESSED_PTR attribute to point to a SQLUINTEGER variable to hold the number of parameters processed.  
  
     Set SQL_ATTR_PARAMS_STATUS_PTR to point to an array[S] of SQLUSSMALLINT variables to hold the parameter status indicators.  
  
2.  For each parameter marker:  
  
     Allocate an array of S parameter buffers to store data values.  
  
     Allocate an array of S parameter buffers to store data lengths.  
  
     Call [SQLBindParameter](../../../relational-databases/native-client-odbc-api/sqlbindparameter.md) to bind the parameter data value and data length arrays to the statement parameter.  
  
     Set up any data-at-execution text or image parameters.  
  
     Put S data values and S data lengths into the bound parameter arrays.  
  
3.  Call [SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md) to execute the statement. The driver efficiently executes the statement S times, once for each set of parameters.  
  
4.  If data-at-execution input parameters are used, [SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md) returns SQL_NEED_DATA. Send the data in chunks by using [SQLParamData](../../../odbc/reference/syntax/sqlparamdata-function.md) and [SQLPutData](../../../relational-databases/native-client-odbc-api/sqlputdata.md).  
  
### To execute a statement multiple times by using row-wise parameter binding  
  
1.  Allocate an array[S] of structures, where S is the number of sets of parameters. The structure has one element for each parameter, and each element has two parts:  
  
     The first part is a variable of the appropriate data type to hold the parameter data.  
  
     The second part is a SQLINTEGER variable to hold the status indicator.  
  
2.  Call [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) to set the following attributes:  
  
     Set SQL_ATTR_PARAMSET_SIZE to the number of sets (S) of parameters.  
  
     Set SQL_ATTR_PARAM_BIND_TYPE to the size of the structure allocated in Step 1.  
  
     Set the SQL_ATTR_PARAMS_PROCESSED_PTR attribute to point to a SQLUINTEGER variable to hold the number of parameters processed.  
  
     Set SQL_ATTR_PARAMS_STATUS_PTR to point to an array[S] of SQLUSSMALLINT variables to hold the parameter status indicators.  
  
3.  For each parameter marker, call [SQLBindParameter](../../../relational-databases/native-client-odbc-api/sqlbindparameter.md) to point the parameter's data value and data length pointer to their variables in the first element of the array of structures allocated in Step 1. If the parameter is a data-at-execution parameter, set it up.  
  
4.  Fill the bound parameter buffer array with data values.  
  
5.  Call [SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md) to execute the statement. The driver efficiently executes the statement S times, once for each set of parameters.  
  
6.  If data-at-execution input parameters are used, [SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md) returns SQL_NEED_DATA. Send the data in chunks by using [SQLParamData](../../../odbc/reference/syntax/sqlparamdata-function.md) and [SQLPutData](../../../relational-databases/native-client-odbc-api/sqlputdata.md).  
  
 **Note** Column-wise and row-wise binding are more typically used in conjunction with [SQLPrepare Function](../../../odbc/reference/syntax/sqlprepare-function.md) and [SQLExecute](../../../odbc/reference/syntax/sqlexecute-function.md) than with [SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md).  
  
## See Also  
 [Executing Queries How-to Topics &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-how-to/execute-queries/executing-queries-how-to-topics-odbc.md)  
  
