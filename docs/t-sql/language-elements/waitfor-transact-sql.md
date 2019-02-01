---
title: "WAITFOR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "WAITFOR"
  - "WAITFOR_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "TIME option"
  - "delaying executions [SQL Server]"
  - "batches [SQL Server], execution times"
  - "stored procedures [SQL Server], executing"
  - "DELAY"
  - "time [SQL Server], execution delays"
  - "blocking executions"
  - "transactions [SQL Server], execution times"
  - "WAITFOR statement"
  - "timing executions"
ms.assetid: 8e896e73-af27-4cae-a725-7a156733f3bd
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# WAITFOR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Blocks the execution of a batch, stored procedure, or transaction until a specified time or time interval is reached, or a specified statement modifies or returns at least one row.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
WAITFOR   
{  
    DELAY 'time_to_pass'   
  | TIME 'time_to_execute'   
  | [ ( receive_statement ) | ( get_conversation_group_statement ) ]   
    [ , TIMEOUT timeout ]  
}  
```  
  
## Arguments  
 DELAY  
 Is the specified period of time that must pass, up to a maximum of 24 hours, before execution of a batch, stored procedure, or transaction proceeds.  
  
 '*time_to_pass*'  
 Is the period of time to wait. *time_to_pass* can be specified in one of the acceptable formats for **datetime** data, or it can be specified as a local variable. Dates cannot be specified; therefore, the date part of the **datetime** value is not allowed. This is formatted as hh:mm[[:ss].mss].
  
 TIME  
 Is the specified time when the batch, stored procedure, or transaction runs.  
  
 '*time_to_execute*'  
 Is the time at which the WAITFOR statement finishes. *time_to_execute* can be specified in one of the acceptable formats for **datetime** data, or it can be specified as a local variable. Dates cannot be specified; therefore, the date part of the **datetime** value is not allowed. This is formatted as hh:mm[[:ss].mss] and can optionally include the date of 1900-01-01.
  
 *receive_statement*  
 Is a valid RECEIVE statement.  
  
> [!IMPORTANT]  
>  WAITFOR with a *receive_statement* is applicable only to [!INCLUDE[ssSB](../../includes/sssb-md.md)] messages. For more information, see [RECEIVE &#40;Transact-SQL&#41;](../../t-sql/statements/receive-transact-sql.md).  
  
 *get_conversation_group_statement*  
 Is a valid GET CONVERSATION GROUP statement.  
  
> [!IMPORTANT]  
>  WAITFOR with a *get_conversation_group_statement* is applicable only to [!INCLUDE[ssSB](../../includes/sssb-md.md)] messages. For more information, see [GET CONVERSATION GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/get-conversation-group-transact-sql.md).  
  
 TIMEOUT *timeout*  
 Specifies the period of time, in milliseconds, to wait for a message to arrive on the queue.  
  
> [!IMPORTANT]  
>  Specifying WAITFOR with TIMEOUT is applicable only to [!INCLUDE[ssSB](../../includes/sssb-md.md)] messages. For more information, see [RECEIVE &#40;Transact-SQL&#41;](../../t-sql/statements/receive-transact-sql.md) and [GET CONVERSATION GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/get-conversation-group-transact-sql.md).  
  
## Remarks  
 While executing the WAITFOR statement, the transaction is running and no other requests can run under the same transaction.  
  
 The actual time delay may vary from the time specified in *time_to_pass*, *time_to_execute*, or *timeout* and depends on the activity level of the server. The time counter starts when the thread associated with the WAITFOR statement is scheduled. If the server is busy, the thread may not be immediately scheduled; therefore, the time delay may be longer than the specified time.  
  
 WAITFOR does not change the semantics of a query. If a query cannot return any rows, WAITFOR will wait forever or until TIMEOUT is reached, if specified.  
  
 Cursors cannot be opened on WAITFOR statements.  
  
 Views cannot be defined on WAITFOR statements.  
  
 When the query exceeds the query wait option, the WAITFOR statement argument can complete without running. For more information about the configuration option, see [Configure the query wait Server Configuration Option](../../database-engine/configure-windows/configure-the-query-wait-server-configuration-option.md). To see the active and waiting processes, use [sp_who](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md).  
  
 Each WAITFOR statement has a thread associated with it. If many WAITFOR statements are specified on the same server, many threads can be tied up waiting for these statements to run. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] monitors the number of threads associated with WAITFOR statements, and randomly selects some of these threads to exit if the server starts to experience thread starvation.  
  
 You can create a deadlock by running a query with WAITFOR within a transaction that also holds locks preventing changes to the rowset that the WAITFOR statement is trying to access. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifies these scenarios and returns an empty result set if the chance of such a deadlock exists.  
  
> [!CAUTION]  
>  Including WAITFOR will slow the completion of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process and can result in a timeout message in the application. If necessary, adjust the timeout setting for the connection at the application level.  
  
## Examples  
  
### A. Using WAITFOR TIME  
 The following example executes the stored procedure `sp_update_job` in the msdb database at 10:20 P.M. (`22:20`).  
  
```  
EXECUTE sp_add_job @job_name = 'TestJob';  
BEGIN  
    WAITFOR TIME '22:20';  
    EXECUTE sp_update_job @job_name = 'TestJob',  
        @new_name = 'UpdatedJob';  
END;  
GO  
```  
  
### B. Using WAITFOR DELAY  
 The following example executes the stored procedure after a two-hour delay.  
  
```  
BEGIN  
    WAITFOR DELAY '02:00';  
    EXECUTE sp_helpdb;  
END;  
GO  
```  
  
### C. Using WAITFOR DELAY with a local variable  
 The following example shows how a local variable can be used with the `WAITFOR DELAY` option. A stored procedure is created to wait for a variable period of time and then returns information to the user as to the number of hours, minutes, and seconds that have elapsed.  
  
```  
IF OBJECT_ID('dbo.TimeDelay_hh_mm_ss','P') IS NOT NULL  
    DROP PROCEDURE dbo.TimeDelay_hh_mm_ss;  
GO  
CREATE PROCEDURE dbo.TimeDelay_hh_mm_ss   
    (  
    @DelayLength char(8)= '00:00:00'  
    )  
AS  
DECLARE @ReturnInfo varchar(255)  
IF ISDATE('2000-01-01 ' + @DelayLength + '.000') = 0  
    BEGIN  
        SELECT @ReturnInfo = 'Invalid time ' + @DelayLength   
        + ',hh:mm:ss, submitted.';  
        -- This PRINT statement is for testing, not use in production.  
        PRINT @ReturnInfo   
        RETURN(1)  
    END  
BEGIN  
    WAITFOR DELAY @DelayLength  
    SELECT @ReturnInfo = 'A total time of ' + @DelayLength + ',   
        hh:mm:ss, has elapsed! Your time is up.'  
    -- This PRINT statement is for testing, not use in production.  
    PRINT @ReturnInfo;  
END;  
GO  
/* This statement executes the dbo.TimeDelay_hh_mm_ss procedure. */  
EXEC TimeDelay_hh_mm_ss '00:00:10';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `A total time of 00:00:10, in hh:mm:ss, has elapsed. Your time is up.`  
  
## See Also  
 [Control-of-Flow Language &#40;Transact-SQL&#41;](~/t-sql/language-elements/control-of-flow.md)   
 [datetime &#40;Transact-SQL&#41;](../../t-sql/data-types/datetime-transact-sql.md)   
 [sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)  
  
