---
title: "Bind a Database with Memory-Optimized Tables to a Resource Pool | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: f222b1d5-d2fa-4269-8294-4575a0e78636
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Bind a Database with Memory-Optimized Tables to a Resource Pool
  A resource pool represents a subset of physical resources that can be governed. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases are bound to and consume the resources of the default resource pool. To protect [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from having its resources consumed by one or more memory-optimized tables, and to prevent other memory users from consuming memory needed by memory-optimized tables, you should create a separate resource pool to manage memory consumption for the database with memory-optimized tables.  
  
 A database can be bound on only one resource pool. However, you can bind multiple databases to the same pool. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows binding a database without memory-optimized tables to a resource pool but it has no effect. You may want to bind a database to a named resource pool if, in future, you may want to create memory-optimized tables in the database.  
  
 Before you can bind a database to a resource pool both the database and the resource pool must exist. The binding takes effect the next time the database is brought online. See [Database States](../databases/database-states.md) for more information.  
  
 For information about resource pools, see [Resource Governor Resource Pool](../resource-governor/resource-governor-resource-pool.md).  
  
## Steps to bind a database to a resource pool  
  
1.  [Create the database and resource pool](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_createpool)  
  
    1.  [Create the database](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_createdatabase)  
  
    2.  [Determine the minimum value for MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_determinepercent)  
  
    3.  [Create a resource pool and configure memory](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_createresourcepool)  
  
2.  [Bind the database to the pool](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_definebinding)  
  
3.  [Confirm the binding](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_confirmbinding)  
  
4.  [Make the binding effective](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_makebindingeffective)  
  
 Other content in this topic  
  
-   [Change MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on an existing pool](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_changeallocation)  
  
-   [Percent of memory available for memory-optimized tables and indexes](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_percentavailable)  
  
##  <a name="bkmk_CreatePool"></a> Create the database and resource pool  
 You can create the database and resource pool in any order. What matters is that they both exist prior to binding the database to the resource pool.  
  
###  <a name="bkmk_CreateDatabase"></a> Create the database  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] creates a database named IMOLTP_DB which will contain one or more memory-optimized tables. The path \<driveAndPath> must exist prior to running this command.  
  
```tsql  
CREATE DATABASE IMOLTP_DB  
GO  
ALTER DATABASE IMOLTP_DB ADD FILEGROUP IMOLTP_DB_fg CONTAINS MEMORY_OPTIMIZED_DATA  
ALTER DATABASE IMOLTP_DB ADD FILE( NAME = 'IMOLTP_DB_fg' , FILENAME = 'c:\data\IMOLTP_DB_fg') TO FILEGROUP IMOLTP_DB_fg;  
GO  
```  
  
###  <a name="bkmk_DeterminePercent"></a> Determine the minimum value for MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT  
 Once you determine the memory needs for your memory-optimized tables, you need to determine what percentage of available memory you need, and set the memory percentages to that value or higher.  
  
 **Example:**   
For this example we will assume that from your calculations you determined that your memory-optimized tables and indexes need 16 GB of memory. Assume that you have 32 GB of memory committed for your use.  
  
 At first glance it could seem that you need to set MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT to 50 (16 is 50% of 32).  However, that would not give your memory-optimized tables sufficient memory. Looking at the table below ([Percent of memory available for memory-optimized tables and indexes](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_percentavailable)) we see that if there is 32 GB of committed memory, only 80% of that is available for memory-optimized tables and indexes.  Therefore, we calculate the min and max percentages based upon the available memory, not the committed memory.  
  
 `memoryNeedeed = 16`   
 `memoryCommitted = 32`   
 `availablePercent = 0.8`   
 `memoryAvailable = memoryCommitted * availablePercent`   
 `percentNeeded = memoryNeeded / memoryAvailable`  
  
 Plugging in real numbes:   
`percentNeeded = 16 / (32 * 0.8) = 16 / 25.6 = 0.625`  
  
 Thus you need at least 62.5% of the available memory to meet the 16 GB requirement of your memory-optimized tables and indexes.  Since the values for MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT must be integers, we set them to at least 63%.  
  
###  <a name="bkmk_CreateResourcePool"></a> Create a resource pool and configure memory  
 When configuring memory for memory-optimized tables, the capacity planning should be done based on MIN_MEMORY_PERCENT, not on MAX_MEMORY_PERCENT.  See [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-resource-pool-transact-sql) for information on MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT. This provides more predictable memory availability for memory-optimized tables as MIN_MEMORY_PERCENT causes memory pressure to other resource pools to make sure it is honored. To ensure that memory is available and help avoid out-of-memory conditions, the values for MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT should be the same. See [Percent of memory available for memory-optimized tables and indexes](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_percentavailable) below for the percent of memory available for memory-optimized tables based on the amount of committed memory.  
  
 See [Best Practices: Using In-Memory OLTP in a VM environment](../../database-engine/using-in-memory-oltp-in-a-vm-environment.md) for more information when working in a VM environment.  
  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] code creates a resource pool named Pool_IMOLTP with half of the memory available for its use.  After the pool is created Resource Governor is reconfigured to include Pool_IMOLTP.  
  
```tsql  
-- set MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT to the same value  
CREATE RESOURCE POOL Pool_IMOLTP   
  WITH   
    ( MIN_MEMORY_PERCENT = 63,   
    MAX_MEMORY_PERCENT = 63 );  
GO  
  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
##  <a name="bkmk_DefineBinding"></a> Bind the database to the pool  
 Use the system function `sp_xtp_bind_db_resource_pool` to bind the database to the resource pool. The function takes two parameters: the database name and the resource pool name.  
  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] defines a binding of the database IMOLTP_DB to the resource pool Pool_IMOLTP. The binding does not become effective until you bring the database online.  
  
```tsql  
EXEC sp_xtp_bind_db_resource_pool 'IMOLTP_DB', 'Pool_IMOLTP'  
GO  
```  
  
 The system function sp_xtp_bind_db_resourece_pool takes two string parameters: database_name and pool_name.  
  
##  <a name="bkmk_ConfirmBinding"></a> Confirm the binding  
 Confirm the binding, noting the resource pool id for IMOLTP_DB. It should not be NULL.  
  
```tsql  
SELECT d.database_id, d.name, d.resource_pool_id  
FROM sys.databases d  
GO  
```  
  
##  <a name="bkmk_MakeBindingEffective"></a> Make the binding effective  
 You must take the database offline and back online after binding it to the resource pool for binding to take effect. If your database was bound to an a different pool earlier, this removes the allocated memory from the previous resource pool and memory allocations for your memory-optimized table and indexes will now come from the resource pool newly bound with the database.  
  
```tsql  
USE master  
GO  
  
ALTER DATABASE IMOLTP_DB SET OFFLINE  
GO  
ALTER DATABASE IMOLTP_DB SET ONLINE  
GO  
  
USE IMOLTP_DB  
GO  
```  
  
 And now, the database is bound to the resource pool.  
  
##  <a name="bkmk_ChangeAllocation"></a> Change MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on an existing pool  
 If you add additional memory to the server or the amount of memory needed for your memory-optimized tables changes, you may need to alter the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT. The following steps show you how to alter the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on a resource pool. See the section below, for guidance on what values to use for MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT.  See the topic [Best Practices: Using In-Memory OLTP in a VM environment](../../database-engine/using-in-memory-oltp-in-a-vm-environment.md) for more information.  
  
1.  Use `ALTER RESOURCE POOL` to change the value of both MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT.  
  
2.  Use `ALTER RESURCE GOVERNOR` to reconfigure the Resource Governor with the new values.  
  
 **Sample Code**  
  
```tsql  
ALTER RESOURCE POOL Pool_IMOLTP  
WITH  
     ( MIN_MEMORY_PERCENT = 70,  
       MAX_MEMORY_PERCENT = 70 )   
GO  
  
-- reconfigure the Resource Governor  
ALTER RESOURCE GOVERNOR RECONFIGURE  
GO  
```  
  
##  <a name="bkmk_PercentAvailable"></a> Percent of memory available for memory-optimized tables and indexes  
 If you map a database with memory-optimized tables and a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] workload to the same resource pool, the Resource Governor sets an internal threshold for [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] use so that the users of the pool do not have conflicts over pool usage. Generally speaking, the threshold for [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] use is about 80% of the pool. The following table shows actual thresholds for various memory sizes.  
  
 When you create a dedicated resource pool for the [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] database, you need to estimate how much physical memory you need for the in-memory tables after accounting for row versions and data growth. Once estimate the memory needed, you create a resource pool with a percent of the commit target memory for SQL Instance as reflected by column 'committed_target_kb' in the DMV `sys.dm_os_sys_info` (see [sys.dm_os_sys_info](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql)). For example, you can create a resource pool P1 with 40% of the total memory available to the instance. Out of this 40%, the [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] engine gets a smaller percent to store [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] data.  This is done to make sure [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] does not consume all the memory from this pool.  This value of the smaller percent depends upon the Target committed Memory. The following table describes memory available to [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] database in a resource pool (named or default) before an OOM error is raised.  
  
|Target Committed Memory|Percent available for in-memory tables|  
|-----------------------------|---------------------------------------------|  
|<= 8 GB|70%|  
|<= 16 GB|75%|  
|<= 32 GB|80%|  
|\<= 96 GB|85%|  
|>96 GB|90%|  
  
 For example, if your 'target committed memory' is 100 GB, and you estimate your memory-optimized tables and indexes need 60GBof memory, then you can create a resource pool with MAX_MEMORY_PERCENT = 67 (60GB needed / 0.90 = 66.667GB - round up to 67GB; 67GB / 100GB installed = 67%) to ensure that your [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] objects have the 60GB they need.  
  
 Once a database has been bound to a named resource pool, use the following query to see memory allocations across different resource pools.  
  
```tsql  
SELECT pool_id  
     , Name  
     , min_memory_percent  
     , max_memory_percent  
     , max_memory_kb/1024 AS max_memory_mb  
     , used_memory_kb/1024 AS used_memory_mb   
     , target_memory_kb/1024 AS target_memory_mb  
   FROM sys.dm_resource_governor_resource_pools  
```  
  
 This sample output shows that the memory taken by memory-optimized objects is 1356 MB in resource pool, PoolIMOLTP, with an upper bound of 2307 MB. This upper bound controls the total memory that can be taken by user and system memory-optimized objects mapped to this pool.  
  
 **Sample Output**   
This output is from the database and tables we created above.  
  
```  
pool_id     Name        min_memory_percent max_memory_percent max_memory_mb used_memory_mb target_memory_mb  
----------- ----------- ------------------ ------------------ ------------- -------------- ----------------   
1           internal    0                  100                3845          125            3845  
2           default     0                  100                3845          32             3845  
259         PoolIMOLTP 0                  100                3845          1356           2307  
```  
  
 For more information see [sys.dm_resource_governor_resource_pools (Transact-SQL)](/sql/relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql).  
  
 If you do not bind your database to a named resource pool, it is bound to the 'default' pool. Since default resource pool is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for most other allocations, you will not be able to monitor memory consumed by memory-optimized tables using the DMV sys.dm_resource_governor_resource_pools accurately for the database of interest.  
  
## See Also  
 [sys.sp_xtp_bind_db_resource_pool &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-bind-db-resource-pool-transact-sql)   
 [sys.sp_xtp_unbind_db_resource_pool &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-unbind-db-resource-pool-transact-sql)   
 [Resource Governor](../resource-governor/resource-governor.md)   
 [Resource Governor Resource Pool](../resource-governor/resource-governor-resource-pool.md)   
 [Create a Resource Pool](../resource-governor/create-a-resource-pool.md)   
 [Change Resource Pool Settings](../resource-governor/change-resource-pool-settings.md)   
 [Delete a Resource Pool](../resource-governor/delete-a-resource-pool.md)  
  
  
