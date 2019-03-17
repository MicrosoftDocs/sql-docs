---
title: "Options (Query Execution:SQL Server:Advanced Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.QueryExecution.SqlServer.SqlExecutionAdvanced"
ms.assetid: 3ec788c7-22c3-4216-9ad0-81a168d17074
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Query Execution:SQL Server:Advanced Page)
  Several options are available using the SET command. Use this page to specify a **set** option for running [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] queries in the SQL Server Query Editor. They have no effect on other code editors. Changes to these options are only applied to new [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] queries. To change the options for the current queries, click **Query Options** on the **Query** menu or the shortcut menu of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Query window. Under **Execution**, click **Advanced**. For more information on each of these, see [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online.  
  
## Options  
 **SET NOCOUNT**  
 Does not return the count of the number of rows, as a message with the result set. This check box is cleared by default.  
  
 **SET NOEXEC**  
 Does not run the query. This check box is cleared by default.  
  
 **SET PARSEONLY**  
 Checks the syntax of each query but does not run the queries. This check box is cleared by default.  
  
 **SET CONCAT_NULL_YIELDS_NULL**  
 When this check box is selected, queries that concatenate an existing value with a NULL, always return a NULL as the result. When this check box is cleared, an existing value concatenated with a NULL, returns the existing value. This check box is selected by default.  
  
 **SET ARITHABORT**  
 When this check box is selected, when an INSERT, DELETE, or UPDATE statement encounters an arithmetic error (overflow, divide-by-zero, or a domain error) during expression evaluation the query or batch is terminated. When this check box is cleared, a NULL is provided for that value if possible, the query continues, and a message is included with the result. For more information, see [SET ARITHABORT &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-arithabort-transact-sql). This check box is selected by default.  
  
 **SET SHOWPLAN_TEXT**  
 When this check box is selected, the query plan is returned in text format with each query. This check box cleared by default.  
  
 **SET STATISTICS TIME**  
 When this check box is selected, the time statistics are returned with each query. This check box is cleared by default.  
  
 **SET STATISTICS IO**  
 When this check box is selected, statistics regarding input and output are returned with each query. This check box is cleared by default.  
  
 **SET TRANSACTION ISOLATION LEVEL**  
 The READ COMMITTED transaction isolation level is set by default. For more information, see [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-transaction-isolation-level-transact-sql). SNAPSHOT transaction isolation level is not available. To use SNAPSHOT isolation, add the following [!INCLUDE[tsql](../includes/tsql-md.md)] statement:  
  
```  
SET TRANSACTION ISOLATION LEVEL SNAPSHOT;  
GO  
```  
  
 **SET DEADLOCK PRIORITY**  
 The default value of Normal allows each query to have the same priority when a deadlock occurs. Select a priority of Low if you want this query to lose any deadlock conflict and be selected as the query to be terminated.  
  
 **SET LOCK TIMEOUT**  
 The default value of -1 indicates that locks are held until transactions are completed. A value of 0 means not to wait at all and return a message as soon as a lock is encountered. Provide a value of greater than 0 milliseconds to terminate a transaction if the locks for transaction must be held for greater than that time.  
  
 **SET QUERY_GOVERNOR_COST_LIMIT**  
 Use the **QUERY_GOVERNOR_COST_LIMIT** option to specify an upper limit for the time in which a query can run. Query cost refers to the estimated elapsed time, in seconds, required to complete a query on a specific hardware configuration. The default setting of 0 indicates no limit to the length of time a query will run.  
  
 **Suppress provider message headers**  
 When this check box is selected, status messages from the provider (such as the SQLClient provider) are not displayed. This check box is selected by default. Clear this check box to see the provider messages when troubleshooting queries that may be failing at the provider level.  
  
 **Disconnect after the query executes**  
 When this check box is selected, the connection to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is terminated after the query completes. This check box is cleared by default.  
  
 **Reset to Default**  
 Resets all values on this page to the original default values.  
  
  
