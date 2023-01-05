---
title: "Configure the min memory per query Server Configuration Option"
description: Learn how to use the min memory per query option to specify the minimum memory grant, or the minimum number of kilobytes that SQL Server allocates for a query.
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/24/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "memory [SQL Server], queries"
  - "minimum query memory"
  - "queries [SQL Server], memory"
  - "min memory per query option"
  - "min memory grant"
---
# Configure the min memory per query Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic describes how to configure the **min memory per query** server configuration option in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **min memory per query** option specifies the minimum amount of memory (in kilobytes) that will be allocated for the execution of a query. This is also known as the minimum memory grant. For example, if **min memory per query** is set to 2,048 KB, the query is guaranteed to get at least that much total memory. The default value is 1,024 KB. The minimum value 512 KB, and the maximum is 2,147,483,647 KB (2 GB).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To configure the min memory per query option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the min memory per query option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The amount of min memory per query has precedence over the [index create memory](../../database-engine/configure-windows/configure-the-index-create-memory-server-configuration-option.md) option. If you modify both options and the index create memory is less than min memory per query, you receive a warning message, but the value is set. During query execution, you receive another similar warning.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] professional.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query processor tries to determine the optimal amount of memory to allocate to a query. The min memory per query option lets the administrator specify the minimum amount of memory any single query receives. Queries generally receive more memory than this, if they have hash and sort operations on a large volume of data. Increasing the value of min memory per query may improve performance for some small to medium-sized queries, but doing so could lead to increased competition for memory resources. The min memory per query option includes memory allocated for sort operations.  

-    Do not set the min memory per query server configuration option too high, especially on very busy systems, because the query has to wait<sup>1</sup> until it can secure the minimum memory requested, or until the value specified in the query wait server configuration option is exceeded. If more memory is available than the specified minimum value required to execute the query, the query is allowed to make use of the additional memory, provided that the memory can be used effectively by the query.     

<sup>1</sup> In this scenario, the wait type is typically RESOURCE_SEMAPHORE. For more information, see [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md).

###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the min memory per query option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Memory** node.  
  
3.  In the **Minimum memory per query** box, enter the minimum amount of memory (in kilobytes) that will be allocated for the execution of a query.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the min memory per query option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `min memory per query` option to `3500` KB.  
  
```sql  
USE AdventureWorks2012 ;  
GO  
EXEC sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE ;  
GO  
EXEC sp_configure 'min memory per query', 3500 ;  
GO  
RECONFIGURE;  
GO    
```  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the min memory per query option  
 The setting takes effect immediately without restarting the server.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [Configure the index create memory Server Configuration Option](../../database-engine/configure-windows/configure-the-index-create-memory-server-configuration-option.md)     
 [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md)     
 [sys.dm_exec_query_memory_grants &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)
  
  
