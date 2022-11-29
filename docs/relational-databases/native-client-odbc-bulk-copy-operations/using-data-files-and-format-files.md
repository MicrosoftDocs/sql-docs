---
description: "Using Data Files and Format Files"
title: "Using Data Files and Format Files | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "bulk copy [ODBC], file formats"
  - "ODBC, functions"
  - "SQL Server Native Client ODBC driver, bulk copy"
  - "bulk copy [SQL Server Native Client]"
  - "ODBC, bulk copy operations"
  - "bulk copy [ODBC], data files"
ms.assetid: c01b7155-3f0a-473d-90b7-87a97bc56ca5
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Data Files and Format Files
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The simplest bulk copy program does the following:  
  
1.  Calls [bcp_init](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-init.md) to specify bulk copying out (set BCP_OUT) from a table or view to a data file.  
  
2.  Calls [bcp_exec](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-exec.md) to execute the bulk copy operation.  
  
 The data file is created in native mode; therefore, data from all columns in the table or view are stored in the data file in the same format as in the database. The file can then be bulk copied into a server by using these same steps and setting DB_IN instead of DB_OUT. This works only if both the source and target tables have exactly the same structure. The resulting data file can also be input to the **bcp** utility by using the **/n** (native mode) switch.  
  
 To bulk copy out the result set of a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement instead of directly from a table or view:  
  
1.  Call **bcp_init** to specify bulk copying out, but specify NULL for the table name.  
  
2.  Call [bcp_control](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-control.md) with *eOption* set to BCPHINTS and *iValue* set to a pointer to a SQLTCHAR string containing the Transact-SQL statement.  
  
3.  Call **bcp_exec** to execute the bulk copy operation.  

 The [!INCLUDE[tsql](../../includes/tsql-md.md)] statement can be any statement that generates a result set. The data file is created containing the first result set of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. Bulk copy ignores any result set after the first if the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement generates multiple result sets.  
  
 To create a data file in which column data is stored in a different format than in the table, call [bcp_columns](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-columns.md) to specify how many columns will be changed, then call [bcp_colfmt](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-colfmt.md) for each column whose format you want to change. This is done after calling **bcp_init** but before calling **bcp_exec**. **bcp_colfmt** specifies the format in which the column's data is stored in the data file. It can be used when bulk copying in or out. You can also use **bcp_colfmt** to set the row and column terminators. For example, if your data contains no tab characters, you can create a tab-delimited file by using **bcp_colfmt** to set the tab character as the terminator for each column.  
  
 When bulk copying out and using **bcp_colfmt**, you can easily create a format file describing the data file you have created by calling [bcp_writefmt](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-writefmt.md) after the last call to **bcp_colfmt**.  
  
 When bulk copying in from a data file described by a format file, read the format file by calling [bcp_readfmt](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-readfmt.md) after **bcp_init** but before **bcp_exec**.  
  
 The **bcp_control** function controls several options when bulk copying into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a data file. **bcp_control** sets options, such as the maximum number of errors before termination, the row in the file on which to start the bulk copy, the row to stop on, and the batch size.  
  
## See Also  
 [Performing Bulk Copy Operations &#40;ODBC&#41;](../../relational-databases/native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md)  
  
  
