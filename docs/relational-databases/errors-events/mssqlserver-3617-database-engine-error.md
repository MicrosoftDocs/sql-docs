---
description: "MSSQLSERVER_3617"
title: "MSSQLSERVER_3617 | Microsoft Docs"
ms.custom: ""
ms.date: "07/10/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3617 (Database Engine error)"
ms.assetid: 
author: PijoCoder
ms.author: mathoma
---
# MSSQLSERVER_3617
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3617|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SYS_ATTN|  
|Message Text||  
  
## Explanation

Error 3617 is raised when a query that is in the middle of execution gets cancelled by the application or by a user, or the connection is broken. This query cancellation from the application causes an Attention event to occur in the Database engine. Attention is a SQL Server event that registers the fact that the client application requested that query execution be terminated. You can trace a attention event on the SQL Server side by using SQL Trace ([Attention Event Class](../event-classes/attention-event-class.md) ) or Xevents (attention).  Attentions show up internally as error 3617.

Attention (query cancellation) is among the top most common [TDS event](/openspecs/sql_server_protocols/ms-sstds/84ed72a9-a1df-48ec-a4d2-32fae12dbdbf) handled by SQL Server.  When a query cancellation request arrives, the attention bit is set for the session/request.  As the session processes yield points, the attention is picked up and honored. For a some more information on attentions and how they interplay with other components see [Tasks, Workers, Threads, Scheduler, Sessions, Connections, Requests ; what does it all mean?](https://techcommunity.microsoft.com/t5/sql-server-support/tasks-workers-threads-scheduler-sessions-connections-requests/ba-p/333990)
  
## User Action  

- Reduce timeout values
- Find out if user cancelled the query execution manually
- Find out if the application or OS where the application runs terminated unexpectedly 

### Attention and Transactions

Commonly attentions are raised when the application reaches a query timeout and cancel the query. When an attention events occurs, SQL server doesn't automatically roll back open transactions.  It's the application's responsibility to roll back the transaction.  One way to do this is to enable [SET XACT_ABORT ON](../../t-sql/statements/set-xact-abort-transact-sql.md) when connecting to SQL Server.  If an application does not do this, an orphaned transaction results. Another way, and more common is to handle any errors in the application by using try… catch… finally (optionally). In the try block, you open the transaction and if an error occurs, in the catch or the finally block, you roll back that transaction.  Either method is acceptable - using [XACT_ABORT ON](../../t-sql/statements/set-xact-abort-transact-sql.md) or handling the exception and rolling back the transaction.

Here is an example:

```csharp

using (SqlConnection connection = new SqlConnection(sqlConnectionString))
{
    SqlTransaction transaction;
    SqlCommand command = connection.CreateCommand();

    connection.Open();
    transaction = connection.BeginTransaction("UpdateTran_Routine1");


    command.Connection = connection;
    command.Transaction = transaction;


    try
    {
        //update one of the tables 
        command.CommandText = "update dl_tab1 set col1 = 987";
        command.ExecuteNonQuery();
        transaction.Commit();
    }

    catch (SqlException ex)
    {
        // Attempt to roll back the transaction.
        try
        {
            transaction.Rollback();
        }
        catch (Exception ex2)
        {
            // This catch block will handle any errors that may have occurred
            // on the server that would cause the rollback to fail, such as
            // a closed connection.
            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
            Console.WriteLine("  Message: {0}", ex2.Message);
        }
    }
}

```
