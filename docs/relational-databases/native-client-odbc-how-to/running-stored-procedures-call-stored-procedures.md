---
title: "Call Stored Procedures (ODBC) | Microsoft Docs"
description: Learn how to add a data source by using ODBC Administrator, programmatically, or using a file, before using ODBC applications with SQL Server 2005 or later.
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "stored procedures [ODBC], calling"
ms.assetid: 31176be8-d40e-4f93-8d44-a46e804a3e2d
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Running Stored Procedures - Call Stored Procedures
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ODBC driver supports executing stored procedures as remote stored procedures. Executing a stored procedure as a remote stored procedure allows the driver and the server to optimize the performance of executing the procedure.  
  
  When a SQL statement calls a stored procedure using the ODBC CALL escape clause, the Microsoft® SQL Server™ driver sends the procedure to SQL Server using the remote stored procedure call (RPC) mechanism. RPC requests bypass much of the statement parsing and parameter processing in SQL Server and are faster than using the Transact-SQL EXECUTE statement.  
  
 For a sample application that demonstrates this feature, see [Process Return Codes and Output Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-how-to/running-stored-procedures-process-return-codes-and-output-parameters.md).  
  
### To run a procedure as an RPC  
  
1.  Construct a SQL statement that uses the ODBC CALL escape sequence. The statement uses parameter markers for each input, input/output, and output parameter, and for the procedure return value (if any):  
  
    ```  
    {? = CALL procname (?,?)}  
    ```  
  
2.  Call [SQLBindParameter](../../relational-databases/native-client-odbc-api/sqlbindparameter.md) for each input, input/output, and output parameter, and for the procedure return value (if any).  
  
3.  Execute the statement with [SQLExecDirect](../../odbc/reference/syntax/sqlexecdirect-function.md).  
  
> [!NOTE]  
>  If an application submits a procedure using the Transact-SQL EXECUTE syntax (as opposed to the ODBC CALL escape sequence), the SQL Server ODBC driver passes the procedure call to SQL Server as a SQL statement rather than as an RPC. Also, output parameters are not returned if the Transact-SQL EXECUTE statement is used.  
  
## See Also  
  [Batching Stored Procedure Calls](../../relational-databases/native-client-odbc-stored-procedures/batching-stored-procedure-calls.md)   
 [Running Stored Procedures](../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)   
 [Calling a Stored Procedure](../../relational-databases/native-client-odbc-stored-procedures/calling-a-stored-procedure.md)   
 [Procedures](../../relational-databases/native-client-odbc-queries/executing-statements/procedures.md)  
  
