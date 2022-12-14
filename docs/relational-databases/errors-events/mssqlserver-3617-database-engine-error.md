---
description: "MSSQLSERVER_3617"
title: "MSSQLSERVER_3617 | Microsoft Docs"
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3617 (Database Engine error)"
author: PijoCoder
ms.author: jopilov
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

Error 3617 is raised when a query that is in the middle of execution gets canceled by the application or by a user, or the connection is broken. This query cancellation from the application causes an Attention event to occur in the Database Engine. The Attention event is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] event that registers the client application's request to terminate query execution. You can trace an Attention event on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] side by using the Extended Events or SQL Trace [Attention Event Class](../event-classes/attention-event-class.md).  Attentions show up internally as error 3617.

Attention (query cancellation) is among the top most common [TDS event](/openspecs/sql_server_protocols/ms-sstds/84ed72a9-a1df-48ec-a4d2-32fae12dbdbf) handled by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  When a query cancellation request arrives, the attention bit is set for the session/request.  As the session processes yield points, the attention is picked up and honored. For some more information on attentions and how they interplay with other components see [Tasks, Workers, Threads, Scheduler, Sessions, Connections, Requests ; what does it all mean?](https://techcommunity.microsoft.com/t5/sql-server-support/tasks-workers-threads-scheduler-sessions-connections-requests/ba-p/333990)
  
## User Action  

Summary of Causes:

- Ensure queries complete within expected duration (less than configured query timeout value)
- Increase query or command timeout
- Find out if user(s) canceled the query execution manually
- Find out if the application or OS terminated unexpectedly

**Ensure queries complete within expected duration (less than configured query timeout value):** The most common reason for attention events is queries are terminated automatically by the application due to exceeding query timeout values. If a query/commmand timeout value is set to 30 seconds and the query does not return even a single packet of data back to the client application, the latter would cancel the query. In such cases, the best approach is to understand why the query is taking so long and take appropriate steps to reduce its duration.

**Increase query or command timeout:** If you establish that the canceled query is running within pre-established baseline duration, but a command timeout is still reached, you may consider increasing the timeout value in the database application.

**Find out if user(s) canceled the query execution manually:** In some cases, the attention event may be raised simply because the user canceled the query. In such cases, it may be prudent to establish if users' expectations exceed the actual speed of the query and address those either by tuning the query or documenting the expected baseline.

**Find out if the application or OS terminated the query or connection unexpectedly or if the application itself terminated:** Investigate the situation to understand what happen on the application end. Examining application logs or System logs may provide clues on possible root cause.

### Attention and Transactions

Commonly, Attention events are raised when the application reaches a query timeout and cancels the query. When an Attention event occurs, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't automatically roll back open transactions.  It's the application's responsibility to roll back the transaction, and there are a couple common ways to handle:

- Control transaction rollback by enabling [SET XACT_ABORT ON](../../t-sql/statements/set-xact-abort-transact-sql.md) when connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  If an application does not do this, an orphaned transaction results. 

- More commonly, applications handle any errors by using `try.. catch... finally`. In the try block, you open the transaction and if an error occurs, roll back the transaction in the catch or finally block.

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
