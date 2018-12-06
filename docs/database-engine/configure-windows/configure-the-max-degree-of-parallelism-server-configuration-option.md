---
title: "Configure the max degree of parallelism Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "parallel queries [SQL Server]"
  - "processors [SQL Server], parallel queries"
  - "number of processors for parallel queries"
  - "max degree of parallelism option"
  - "MaxDop"
ms.assetid: 86b65bf1-a6a1-4670-afc0-cdfad1558032
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure the max degree of parallelism Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic describes how to configure the **max degree of parallelism (MAXDOP)** server configuration option in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. When an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs on a computer that has more than one microprocessor or CPU, it detects the best degree of parallelism, that is, the number of processors employed to run a single statement, for each parallel plan execution. You can use the **max degree of parallelism** option to limit the number of processors to use in parallel plan execution. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] considers parallel execution plans for queries, index data definition language (DDL) operations, parallel insert, online alter column, parallel stats collectiuon, and static and keyset-driven cursor population.

##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If the affinity mask option is not set to the default, it may restrict the number of processors available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on symmetric multiprocessing (SMP) systems.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] professional.  
  
-   To enable the server to determine the maximum degree of parallelism, set this option to 0, the default value. Setting maximum degree of parallelism to 0 allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use all the available processors up to 64 processors. To suppress parallel plan generation, set **max degree of parallelism** to 1. Set the value to a number from 1 to 32,767 to specify the maximum number of processor cores that can be used by a single query execution. If a value greater than the number of available processors is specified, the actual number of available processors is used. If the computer has only one processor, the **max degree of parallelism** value is ignored.  
  
-   You can override the max degree of parallelism value in queries by specifying the MAXDOP query hint in the query statement. For more information, see [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md).  
  
-   Index operations that create or rebuild an index, or that drop a clustered index, can be resource intensive. You can override the max degree of parallelism value for index operations by specifying the MAXDOP index option in the index statement. The MAXDOP value is applied to the statement at execution time and is not stored in the index metadata. For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
-   In addition to queries and index operations, this option also controls the parallelism of DBCC CHECKTABLE, DBCC CHECKDB, and DBCC CHECKFILEGROUP. You can disable parallel execution plans for these statements by using trace flag 2528. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

###  <a name="Guidelines"></a> Guidelines  
Use the following guidelines when you configure the **max degree of parallelism** server configuration value:

||||
|----------------|-----------------|-----------------|
|Server with single NUMA node|Less than 8 logical processors|Keep MAXDOP at or below # of logical processors|
|Server with single NUMA node|Greater than 8 logical processors|Keep MAXDOP at 8|
|Server with multiple NUMA nodes|Less than 8 logical processors per NUMA node|Keep MAXDOP at or below # of logical processors per NUMA node|
|Server with multiple NUMA nodes|Greater than 8 logical processors per NUMA node|Keep MAXDOP at 8|
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the max degree of parallelism option  
  
1.  In **Object Explorer**, right-click a server and select **Properties**.  
  
2.  Click the **Advanced** node.  
  
3.  In the **Max Degree of Parallelism** box, select the maximum number of processors to use in parallel plan execution.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the max degree of parallelism option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the `max degree of parallelism` option to `8`.  
  
```sql  
USE AdventureWorks2012 ;  
GO   
EXEC sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE WITH OVERRIDE;  
GO  
EXEC sp_configure 'max degree of parallelism', 8;  
GO  
RECONFIGURE WITH OVERRIDE;  
GO  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the max degree of parallelism option  
 The setting takes effect immediately without restarting the server.  
  
## See Also  
 [affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md) 
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)   
 [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [DBCC CHECKTABLE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md)   
 [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)   
 [DBCC CHECKFILEGROUP &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkfilegroup-transact-sql.md)   
 [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md)   
 [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md)
 [Set Index Options](../../relational-databases/indexes/set-index-options.md)  
 [Recommendations and guidelines for the "max degree of parallelism" configuration option in SQL Server](https://support.microsoft.com/help/2806535)
  
  
