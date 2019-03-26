---
title: "Guidelines for Retry Logic for Transactions on Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: f2a35c37-4449-49ee-8bba-928028f1de66
author: stevestein
ms.author: sstein
manager: craigg
---
# Guidelines for Retry Logic for Transactions on Memory-Optimized Tables
  There are error conditions that occur with transactions that access memory-optimized tables.  
  
-   41302. The current transaction attempted to update a record that has been updated since the transaction started.  
  
-   41305. The current transaction failed to commit due to a repeatable read validation failure.  
  
-   41325. The current transaction failed to commit due to a serializable validation failure.  
  
-   41301. A previous transaction that the current transaction took a dependency on has aborted, and the current transaction can no longer commit.  
  
 A common cause of these errors is interference between concurrently executing transaction. The common corrective action is to retry the transaction.  
  
 For more information about these error conditions, see the section on Conflict Detection, Validation, and Commit Dependency Checks in [Transactions in Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md).  
  
 Deadlocks (error code 1205) cannot occur for memory-optimized tables. Locks are not used for memory-optimized tables. However, if the application already contains retry logic for deadlocks, the existing logic could be extended to include the new error codes.  
  
## Considerations for Retrying  
 Applications will typically encounter conflicts between transactions and need to implement retry logic to resolve those conflicts. The number of conflicts encountered depends on a number of factors:  
  
-   Contention for individual rows. The potential for conflicts increases as the number of transactions attempting to update the same row increases.  
  
-   Number of rows read by REPEATABLE READ transactions. The more rows read, the greater the chance that some of these rows are updated by concurrent transactions. This causes repeatable read validation failures.  
  
-   Size of the scan ranges used by SERIALIZABLE transactions. The larger the scan ranges, the higher the chance that concurrent transactions will introduce phantom rows, causing serializable validation failures.  
  
     It is difficult for an application to avoid these conflicts, requiring retry logic.  
  
> [!IMPORTANT]  
>  Read-write transactions that access memory-optimized tables require retry logic.  
  
### Considerations for Read-Only Transactions and Natively Compiled Stored Procedures  
 Read-only transactions that span a single execution of a natively compiled stored procedure do not require validation for REPEATABLE READ and SERIALIZABLE transactions. Write conflicts cannot occur due to a transaction being read-only.  
  
 However, dependency failures can still occur. Dependency failures are rarer than errors resulting from conflicts. Therefore, in many cases, specific retry logic is not required for read-only transactions that span single executions of natively compiled stored procedures.  
  
### Considerations for Read-Only Transactions and Cross-Container Transactions  
 Read-only cross-container transactions, which are transactions that are started outside the context of a natively compiled stored procedure, do not perform validation if the memory-optimized tables are all accessed under SNAPSHOT isolation. However, when memory-optimized tables are accessed under REPEATABLE READ or SERIALIZABLE isolation, validation is performed at commit time. In this case, retry logic may be required.  
  
 For more information, see the section on Cross-Container Transactions in [Transaction Isolation Levels](../../2014/database-engine/transaction-isolation-levels.md).  
  
## Implementing Retry Logic  
 As with all transactions that access memory-optimized tables, you need to consider retry logic to handle potential failures, such as write conflicts (error code 41302) or dependency failures (error code 41301). In most applications the failure rate will be low, but it is still necessary to handle failures by retrying the transaction. Two suggested ways of implementing retry logic are:  
  
-   Client-side retries. Client-side retries is the preferred way to implement retry logic in the general case. The client application catches the error thrown by the transaction, and retries the transaction. If an existing client application has retry logic to handle deadlocks, you can extend the application to handle the new error codes.  
  
-   Using a wrapper stored procedure. The client calls an interpreted [!INCLUDE[tsql](../includes/tsql-md.md)] stored procedure that calls the natively compiled stored procedure or executes the transaction. The wrapper procedure then uses try/catch logic to catch the error and retry the procedure call if needed. It is possible that results are returned to the client before the failure, and the client would not know to discard them. Therefore, to be safe, it is best to use this method only with natively compiled stored procedures that do not return any result sets to the client.  
  
 The retry logic can be implemented either in [!INCLUDE[tsql](../includes/tsql-md.md)] or in the application code in the mid-tier.  
  
 Two possible reasons to consider the retry logic are:  
  
-   The client application has retry logic for other error codes, such as 1205, which you can extend.  
  
-   Conflicts are rare, and it is important to reduce end-to-end latency by using prepared execution. For more information about executing natively compiled stored procedures directly, see [Natively Compiled Stored Procedures](../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md).  
  
 The following sample shows retry logic in an interpreted [!INCLUDE[tsql](../includes/tsql-md.md)] stored procedure that contains a call either to a natively compiled stored procedure or to a cross-container transaction.  
  
```tsql  
CREATE PROCEDURE usp_my_procedure @param1 type1, @param2 type2, ...  
AS  
BEGIN  
  -- number of retries - tune based on the workload  
  DECLARE @retry INT = 10  
  
  WHILE (@retry > 0)  
  BEGIN  
    BEGIN TRY  
  
      -- exec usp_my_native_proc @param1, @param2, ...  
  
      --       or  
  
      -- BEGIN TRANSACTION  
      --   ...  
      -- COMMIT TRANSACTION  
  
      SET @retry = 0  
    END TRY  
    BEGIN CATCH  
      SET @retry -= 1  
  
      -- the error number for deadlocks (1205) does not need to be included for   
      -- transactions that do not access disk-based tables  
      IF (@retry > 0 AND error_number() in (41302, 41305, 41325, 41301, 1205))  
      BEGIN  
        -- these error conditions are transaction dooming - rollback the transaction  
        -- this is not needed if the transaction spans a single native proc execution  
        --   as the native proc will simply rollback when an error is thrown   
        IF XACT_STATE() = -1  
          ROLLBACK TRANSACTION  
  
        -- use a delay if there is a high rate of write conflicts (41302)  
        --   length of delay should depend on the typical duration of conflicting transactions  
        -- WAITFOR DELAY '00:00:00.001'  
      END  
      ELSE  
      BEGIN  
        -- insert custom error handling for other error conditions here  
  
        -- throw if this is not a qualifying error condition  
        ;THROW  
      END  
    END CATCH  
  END  
END  
```  
  
## See Also  
 [Understanding Transactions on Memory-Optimized Tables](../../2014/database-engine/understanding-transactions-on-memory-optimized-tables.md)   
 [Transactions in Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md)   
 [Guidelines for Transaction Isolation Levels with Memory-Optimized Tables](../../2014/database-engine/guidelines-for-transaction-isolation-levels-with-memory-optimized-tables.md)  
  
  
