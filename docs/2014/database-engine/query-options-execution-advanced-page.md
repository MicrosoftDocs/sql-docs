---
title: "Query Options Execution (Advanced Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.query.advanced.f1"
ms.assetid: 661595ce-99b9-4316-ad80-ed04002d04d5
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Query Options Execution (Advanced Page)
  A variety of options are available using the **SET** statement. Use this page to specify a **set** option to run Microsoft SQL Server queries. For detailed information on each of these options, see SQL Server Books Online.  
  
 **SET NOCOUNT**  
 Does not return the count of the number of rows, as a message with the result set. This option is cleared by default.  
  
 **SET NOEXEC**  
 Does not run the query. This option is cleared by default.  
  
 **SET PARSEONLY**  
 Checks the syntax of each query but does not run the queries. This option is cleared by default.  
  
 **SET CONCAT_NULL_YIELDS_NULL**  
 When this check box is selected, queries that concatenate an existing value with a `NULL`, always return a `NULL` as the result. When this check box is cleared, an existing value concatenated with a `NULL`, returns the existing value. This option is selected by default.  
  
 **SET ARITHABORT**  
 When this check box is selected, when an `INSERT`, `DELETE` or `UPDATE` statement encounters an arithmetic error (overflow, divide-by-zero, or a domain error) during expression evaluation the query or batch is terminated. When this check box is cleared, a `NULL` is provided for that value if possible, the query continues, and a message is included with the result. See Books Online for a more extensive description of this behavior. This option is selected by default.  
  
 **SET SHOWPLAN_TEXT**  
 When this check box is selected, the query plan is returned in text form with each query. This option is cleared by default.  
  
 **SET STATISTICS TIME**  
 When this check box is selected, the time statistics are returned with each query. This option is cleared by default.  
  
 **SET STATISTICS IO**  
 When this check box is selected, statistics regarding input/output (I/O) are returned with each query. This option is cleared by default.  
  
 **SET TRANSACTION ISOLATION LEVEL**  
 The READ COMMITTED transaction isolation level is set by default. For more information, see [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-transaction-isolation-level-transact-sql). SNAPSHOT transaction isolation level is not available. To use SNAPSHOT isolation, add the following [!INCLUDE[tsql](../includes/tsql-md.md)] statement:  
  
```  
SET TRANSACTION ISOLATION LEVEL SNAPSHOT;  
GO  
```  
  
 **SET DEADLOCK PRIORITY**  
 The default value of **Normal** allows each query to have the same priority when a deadlock occurs. Select the priority Low from the drop-down list, if you want this query to lose any deadlock conflict and be selected as the query to be terminated.  
  
 **SET LOCK TIMEOUT**  
 The default value of -1 indicates that locks are held until transactions are completed. A value of 0 means not to wait at all and return a message as soon as a lock is encountered. Provide a value of greater than 0 milliseconds to terminate a transaction if the locks for transaction must be held for greater than that time.  
  
 **SET QUERY_GOVERNOR_COST_LIMIT**  
 Use the **query governor cost limit** option to specify an upper limit on the time period in which a query can run. Query cost refers to the estimated elapsed time, in seconds, required to complete a query on a specific hardware configuration. The default setting of 0 indicates no limit to the length of time a query will run  
  
 **Suppress provider message headers**  
 When this check box is selected, status messages from the provider (such as the OLE DB provider) are not displayed. This check box is selected by default. Clear this check box to see the provider messages when troubleshooting queries that may be failing at the provider level.  
  
 **Disconnect after the query executes**  
 When this check box is selected, the connection to SQL Server is terminated after the query completes. This option is cleared by default.  
  
 **Reset to Default**  
 Resets all values on this page to the original default values.  
  
  
