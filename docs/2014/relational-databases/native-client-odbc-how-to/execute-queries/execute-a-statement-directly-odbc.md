---
title: "Execute a Statement Directly (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "statement execution"
ms.assetid: b690f9de-66e1-4ee5-ab6a-121346fb5f85
author: MightyPen
ms.author: genemi
manager: craigg
---
# Execute a Statement Directly (ODBC)
    
### To execute a statement directly and one time only  
  
1.  If the statement has parameter markers, use [SQLBindParameter](../../native-client-odbc-api/sqlbindparameter.md) to bind each parameter to a program variable. Fill the program variables with data values, and then set up any data-at-execution parameters.  
  
2.  Call [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399) to execute the statement.  
  
3.  If data-at-execution input parameters are used, [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399) returns SQL_NEED_DATA. Send the data in chunks by using [SQLParamData](https://go.microsoft.com/fwlink/?LinkId=58405) and [SQLPutData](../../native-client-odbc-api/sqlputdata.md).  
  
### To execute a statement multiple times by using column-wise parameter binding  
  
1.  Call [SQLSetStmtAttr](../../native-client-odbc-api/sqlsetstmtattr.md) to set the following attributes:  
  
     Set SQL_ATTR_PARAMSET_SIZE to the number of sets (S) of parameters.  
  
     Set SQL_ATTR_PARAM_BIND_TYPE to SQL_PARAMETER_BIND_BY_COLUMN.  
  
     Set the SQL_ATTR_PARAMS_PROCESSED_PTR attribute to point to a SQLUINTEGER variable to hold the number of parameters processed.  
  
     Set SQL_ATTR_PARAMS_STATUS_PTR to point to an array[S] of SQLUSSMALLINT variables to hold the parameter status indicators.  
  
2.  For each parameter marker:  
  
     Allocate an array of S parameter buffers to store data values.  
  
     Allocate an array of S parameter buffers to store data lengths.  
  
     Call [SQLBindParameter](../../native-client-odbc-api/sqlbindparameter.md) to bind the parameter data value and data length arrays to the statement parameter.  
  
     Set up any data-at-execution text or image parameters.  
  
     Put S data values and S data lengths into the bound parameter arrays.  
  
3.  Call [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399) to execute the statement. The driver efficiently executes the statement S times, once for each set of parameters.  
  
4.  If data-at-execution input parameters are used, [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399) returns SQL_NEED_DATA. Send the data in chunks by using [SQLParamData](https://go.microsoft.com/fwlink/?LinkId=58405) and [SQLPutData](../../native-client-odbc-api/sqlputdata.md).  
  
### To execute a statement multiple times by using row-wise parameter binding  
  
1.  Allocate an array[S] of structures, where S is the number of sets of parameters. The structure has one element for each parameter, and each element has two parts:  
  
     The first part is a variable of the appropriate data type to hold the parameter data.  
  
     The second part is a SQLINTEGER variable to hold the status indicator.  
  
2.  Call [SQLSetStmtAttr](../../native-client-odbc-api/sqlsetstmtattr.md) to set the following attributes:  
  
     Set SQL_ATTR_PARAMSET_SIZE to the number of sets (S) of parameters.  
  
     Set SQL_ATTR_PARAM_BIND_TYPE to the size of the structure allocated in Step 1.  
  
     Set the SQL_ATTR_PARAMS_PROCESSED_PTR attribute to point to a SQLUINTEGER variable to hold the number of parameters processed.  
  
     Set SQL_ATTR_PARAMS_STATUS_PTR to point to an array[S] of SQLUSSMALLINT variables to hold the parameter status indicators.  
  
3.  For each parameter marker, call [SQLBindParameter](../../native-client-odbc-api/sqlbindparameter.md) to point the parameter's data value and data length pointer to their variables in the first element of the array of structures allocated in Step 1. If the parameter is a data-at-execution parameter, set it up.  
  
4.  Fill the bound parameter buffer array with data values.  
  
5.  Call [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399) to execute the statement. The driver efficiently executes the statement S times, once for each set of parameters.  
  
6.  If data-at-execution input parameters are used, [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399) returns SQL_NEED_DATA. Send the data in chunks by using [SQLParamData](https://go.microsoft.com/fwlink/?LinkId=58405) and [SQLPutData](../../native-client-odbc-api/sqlputdata.md).  
  
 **Note** Column-wise and row-wise binding are more typically used in conjunction with [SQLPrepare Function](https://go.microsoft.com/fwlink/?LinkId=59360) and [SQLExecute](https://go.microsoft.com/fwlink/?LinkId=58400) than with [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399).  
  
## See Also  
 [Executing Queries How-to Topics &#40;ODBC&#41;](executing-queries-how-to-topics-odbc.md)  
  
  
