---
title: "Call Stored Procedures (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "stored procedures [ODBC], calling"
ms.assetid: 31176be8-d40e-4f93-8d44-a46e804a3e2d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Call Stored Procedures (ODBC)
  When a SQL statement calls a stored procedure using the ODBC CALL escape clause, the Microsoft?? SQL Server??? driver sends the procedure to SQL Server using the remote stored procedure call (RPC) mechanism. RPC requests bypass much of the statement parsing and parameter processing in SQL Server and are faster than using the Transact-SQL EXECUTE statement.  
  
 For a sample application that demonstrates this feature, see [Process Return Codes and Output Parameters &#40;ODBC&#41;](running-stored-procedures-process-return-codes-and-output-parameters.md).  
  
### To run a procedure as an RPC  
  
1.  Construct a SQL statement that uses the ODBC CALL escape sequence. The statement uses parameter markers for each input, input/output, and output parameter, and for the procedure return value (if any):  
  
    ```  
    {? = CALL procname (?,?)}  
    ```  
  
2.  Call [SQLBindParameter](../native-client-odbc-api/sqlbindparameter.md) for each input, input/output, and output parameter, and for the procedure return value (if any).  
  
3.  Execute the statement with [SQLExecDirect](https://go.microsoft.com/fwlink/?LinkId=58399).  
  
> [!NOTE]  
>  If an application submits a procedure using the Transact-SQL EXECUTE syntax (as opposed to the ODBC CALL escape sequence), the SQL Server ODBC driver passes the procedure call to SQL Server as a SQL statement rather than as an RPC. Also, output parameters are not returned if the Transact-SQL EXECUTE statement is used.  
  
## See Also  
 [Running Stored Procedures How-to Topics &#40;ODBC&#41;](../../database-engine/dev-guide/running-stored-procedures-how-to-topics-odbc.md)   
 [Batching Stored Procedure Calls](../native-client-odbc-stored-procedures/batching-stored-procedure-calls.md)   
 [Running Stored Procedures](../native-client-odbc-stored-procedures/running-stored-procedures.md)   
 [Calling a Stored Procedure](../native-client-odbc-stored-procedures/calling-a-stored-procedure.md)   
 [Procedures](../native-client-odbc-queries/executing-statements/procedures.md)  
  
  
