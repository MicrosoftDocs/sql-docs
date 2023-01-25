---
title: "Configure the index create memory Server Configuration Option"
description: See how to use the index create memory option to set the maximum amount of memory that SQL Server initially allocates for sort operations when creating indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/24/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "index create memory option"
---
# Configure the index create memory Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic describes how to configure the **index create memory** server configuration option in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **index create memory** option controls the maximum amount of memory initially allocated for sort operations when creating indexes. The default value for this option is 0 (self-configuring). If more memory is later needed for index creation and the memory is available, the server will use it; thereby, exceeding the setting of this option. If additional memory is not available, the index creation will continue using the memory already allocated.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To configure the index create memory option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the index create memory option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The setting of the **[min memory per query](../../database-engine/configure-windows/configure-the-min-memory-per-query-server-configuration-option.md)** option has precedence over the **index create memory** option. If you change both options and the **index create memory** is less than **min memory per query**, you receive a warning message, but the value is set. During query execution, you receive a similar warning.  
  
-   When using partitioned tables and indexes, the minimum memory requirements for index creation may increase significantly if there are non-aligned partitioned indexes and a high degree of parallelism. This option controls the total initial amount of memory allocated for all index partitions in a single index creation operation. The query will terminate with an error message if the amount set by this option is less than the minimum required to run the query.  
  
-   The run value for this option will not exceed the actual amount of memory that can be used for the operating system and hardware platform on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] professional.  
  
-   The **index create memory** option is self-configuring and usually works without requiring adjustment. However, if you experience difficulties creating indexes, consider increasing the value of this option from its run value.  

-   Creating an index on a production system is usually an infrequently performed task, often scheduled as a job to execute during off-peak time. Therefore, when creating indexes infrequently and during off-peak time, increasing the **index create memory** can improve the performance of index creation. Keep the **[min memory per query](../../database-engine/configure-windows/configure-the-min-memory-per-query-server-configuration-option.md)** configuration option at a lower number, however, so the index creation job still starts even if all the requested memory is not available.
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the index create memory option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Memory** node.  
  
3.  Under **Index creation memory**, type or select the desired value for the index create memory option.  
  
     Use the **index create memory** option to control the amount of memory used by index creation sorts. The **index create memory** option is self-configuring and should work in most cases without requiring adjustment. However, if you experience difficulties creating indexes, consider increasing the value of this option from its run value. Query sorts are controlled through the **min memory per query** option.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the index create memory option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `index create memory` option to `4096`.  
  
```sql  
USE AdventureWorks2012 ;  
EXEC sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
EXEC sp_configure 'index create memory', 4096  
GO  
RECONFIGURE;  
GO  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the index create memory option  
 The setting takes effect immediately without restarting the server.  
  
## See Also  
 [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
