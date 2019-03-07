---
title: "Resolve Out Of Memory Issues | Microsoft Docs"
ms.custom: ""
ms.date: "12/21/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: f855e931-7502-44bd-8a8b-b8543645c7f4
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# Resolve Out Of Memory issues
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  [!INCLUDE[hek_1](../../includes/hek-1-md.md)] uses more memory and in different ways than does [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It is possible that the amount of memory you installed and allocated for [!INCLUDE[hek_2](../../includes/hek-2-md.md)] becomes inadequate for your growing needs. If so, you could run out of memory. This topic covers how to recover from an OOM situation. See [Monitor and Troubleshoot Memory Usage](../../relational-databases/in-memory-oltp/monitor-and-troubleshoot-memory-usage.md) for guidance that can help you avoid many OOM situations.  
  
## Covered in this topic  
  
|Topic|Overview|  
|-----------|--------------|  
|[Resolve database restore failures due to OOM](#bkmk_resolveRecoveryFailures)|What to do if you get the error message, "Restore operation failed for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'."|  
|[Resolve impact of low memory or OOM conditions on the workload](#bkmk_recoverFromOOM)|What to do if you find low memory issues are negatively impacting performance.|  
|[Resolve page allocation failures due to insufficient memory when sufficient memory is available](#bkmk_PageAllocFailure)|What to do if you get the error message, "Disallowing page allocations for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'. ..." when available memory is sufficient for the operation.|
|[Best Practices using In-Memory OLTP in a VM environment](#bkmk_VMs)|What to keep in mind when using In-Memory OLTP in a virtualized environment.|
  
##  <a name="bkmk_resolveRecoveryFailures"></a> Resolve database restore failures due to OOM  
 When you attempt to restore a database you may get the error message: "Restore operation failed for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'." This indicates that the server does not have enough available memory for restoring the database. 
   
The server you restore a database to must have enough available memory for the memory-optimized tables in the database backup, otherwise the database will not come online, and will be marked as suspect.  
  
If the server does have enough physical memory, but you are still seeing this error, it could be that other processes are using too much memory or a configuration issue causes not enough memory to be available for restore. For this class of issues, use the following measures to make more memory available to the restore operation: 
  
-   Temporarily close running applications.   
    By closing one or more running applications or stopping services not needed at the moment, you make the memory they were using available for the restore operation. You can restart them following the successful restore.  
  
-   Increase the value of MAX_MEMORY_PERCENT.   
    If the database is [bound to a resource pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md), which is best practice, the memory available to restore is governed by MAX_MEMORY_PERCENT. If the value is too low, restore will fail. This code snippet changes MAX_MEMORY_PERCENT for the resource pool PoolHk to 70% of installed memory.  
  
    > [!IMPORTANT]  
    > If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT to the same value as MAX_MEMORY_PERCENT.   
    > See the topic [Best Practices using In-Memory OLTP in a VM environment](#bkmk_VMs) for more information.  
  
    ```sql  
    -- disable resource governor  
    ALTER RESOURCE GOVERNOR DISABLE  
  
    -- change the value of MAX_MEMORY_PERCENT  
    ALTER RESOURCE POOL PoolHk  
    WITH  
         ( MAX_MEMORY_PERCENT = 70 )  
    GO  
  
    -- reconfigure the Resource Governor  
    --    RECONFIGURE enables resource governor  
    ALTER RESOURCE GOVERNOR RECONFIGURE  
    GO  
  
    ```  
  
     For information on maximum values for MAX_MEMORY_PERCENT see the topic section [Percent of memory available for memory-optimized tables and indexes](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_PercentAvailable).  
  
-   Increase **max server memory**.  
    For information on configuring **max server memory** see the topic [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).  
  
##  <a name="bkmk_recoverFromOOM"></a> Resolve impact of low memory or OOM conditions on the workload  
 Obviously, it is best to not get into a low memory or OOM (Out of Memory) situation. Good planning and monitoring can help avoid OOM situations. Still, the best planning does not always foresee what actually happens and you might end up with low memory or OOM. There are two steps to recovering from OOM:  
  
1.  [Open a DAC (Dedicated Administrator Connection)](#bkmk_openDAC)  
  
2.  [Take corrective action](#bkmk_takeCorrectiveAction)  
  
###  <a name="bkmk_openDAC"></a> Open a DAC (Dedicated Administrator Connection)  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a dedicated administrator connection (DAC). The DAC allows an administrator to access a running instance of SQL Server Database Engine to troubleshoot problems on the server-even when the server is unresponsive to other client connections. The DAC is available through the `sqlcmd` utility and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 For guidance on using DAC through SSMS or `sqlcmd`, refer to [Diagnostic Connection for Database Administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md).  
  
###  <a name="bkmk_takeCorrectiveAction"></a> Take corrective action  
 To resolve your OOM condition you need to either free up existing memory by reducing usage, or make more memory available to your in-memory tables.  
  
#### Free up existing memory  
  
##### Delete non-essential memory optimized table rows and wait for garbage collection  
 You can remove non-essential rows from a memory optimized table. The garbage collector returns the memory used by these rows to available memory. In-memory OLTP engine collects garbage rows aggressively. However, a long running transaction can prevent garbage collection. For example, if you have a transaction that runs for 5 minutes, any row versions created due to update/delete operations while the transaction was active can't be garbage collected.  
  
##### Move one or more rows to a disk-based table  
 The following TechNet articles provide guidance on moving rows from a memory-optimized table to a disk-based table.  
  
-   [Application-Level Partitioning](../../relational-databases/in-memory-oltp/application-level-partitioning.md)  
  
-   [Application Pattern for Partitioning Memory-Optimized Tables](../../relational-databases/in-memory-oltp/application-pattern-for-partitioning-memory-optimized-tables.md)  
  
#### Increase available memory  
  
##### Increase value of MAX_MEMORY_PERCENT on the resource pool  
 If you have not created a named resource pool for your in-memory tables you should do that and bind your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] databases to it. See the topic [Bind a Database with Memory-Optimized Tables to a Resource Pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md) for guidance on creating and binding your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] databases to a resource pool.  
  
 If your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] database is bound to a resource pool you may be able to increase the percent of memory the pool can access. See the sub-topic [Change MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on an existing pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_ChangeAllocation) for guidance on changing the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT for a resource pool.  
  
 Increase the value of MAX_MEMORY_PERCENT.   
This code snippet changes MAX_MEMORY_PERCENT for the resource pool PoolHk to 70% of installed memory.  
  
> [!IMPORTANT]  
>  If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT to the same value.   
> See the topic [Best Practices using In-Memory OLTP in a VM environment](#bkmk_VMs) for more information.  
  
```sql  
-- disable resource governor  
ALTER RESOURCE GOVERNOR DISABLE  
  
-- change the value of MAX_MEMORY_PERCENT  
ALTER RESOURCE POOL PoolHk  
WITH  
     ( MAX_MEMORY_PERCENT = 70 )  
GO  
  
-- reconfigure the Resource Governor to enabled it
ALTER RESOURCE GOVERNOR RECONFIGURE  
GO  
```  
  
 For information on maximum values for MAX_MEMORY_PERCENT see the topic section [Percent of memory available for memory-optimized tables and indexes](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_PercentAvailable).  
  
##### Install additional memory  
 Ultimately the best solution, if possible, is to install additional physical memory. If you do this, remember that you will probably be able to also increase the value of MAX_MEMORY_PERCENT (see the sub-topic [Change MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on an existing pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_ChangeAllocation)) since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] won't likely need more memory, allowing you to make most if not all of the newly installed memory available to the resource pool.  
  
> [!IMPORTANT]  
>  If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT to the same value.   
> See the topic [Best Practices using In-Memory OLTP in a VM environment](#bkmk_VMs) for more information.  
  
##  <a name="bkmk_PageAllocFailure"></a> Resolve page allocation failures due to insufficient memory when sufficient memory is available  
 If you get the error message, `Disallowing page allocations for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'. See 'https://go.microsoft.com/fwlink/?LinkId=330673' for more information.` in the error log when the available physical memory is sufficient to allocate the page, it may be due to a disabled Resource Governor. When the Resource Governor is disabled MEMORYBROKER_FOR_RESERVE induces artificial memory pressure.  
  
 To resolve this you need to enable the Resource Governor.  
  
 See [Enable Resource Governor](../../relational-databases/resource-governor/enable-resource-governor.md) for information on Limits and Restrictions as well as guidance on enabling Resource Governor using Object Explorer, Resource Governor properties, or Transact-SQL.  
 
## <a name="bkmk_VMs"></a> Best Practices using In-Memory OLTP in a VM environment
Server virtualization can help you lower IT capital and operational costs and attain greater IT efficiency with improved application provisioning, maintenance, availability, and backup/recovery processes. With recent technological advances, complex database workloads can be more readily consolidated using virtualization. This topic covers best practices for using SQL Server In-Memory OLTP in a virtualized environment.

### Memory pre-allocation
For memory in a virtualized environment, better performance and enhanced support are essential considerations. You must be able to both quickly allocate memory to virtual machines depending on their requirements (peak and off-peak loads) and ensure that the memory is not wasted. The Hyper-V Dynamic Memory feature increases agility in how the memory is allocated and managed between virtual machines running on a host.

Some best practices for virtualizing and managing SQL Server need to be modified when virtualizing a database with memory-optimized tables. Without memory-optimized tables, two of the best practices are:
-  If you use min server memory, it is better to assign only the amount of memory that is required so sufficient memory remains for other processes (thereby avoiding paging).
-  Do not set the memory pre-allocation value too high. Otherwise, other processes may not get sufficient memory at the time when they require it, and this can result in memory paging.

If you follow the above practices for a database with memory-optimized tables, an attempt to restore and recover a database could result in the database being in a "Recovery Pending" state, even if you have sufficient memory to recover the database. The reason for this is that, when starting up, In-Memory OLTP brings data into memory more aggressively than dynamic memory allocation allocates memory to the database.

### Resolution
To mitigate this, pre-allocate sufficient memory to the database to recover or restart the database, not a minimum value relying on dynamic memory to provide the additional memory when needed.
  
## See Also  
 [Managing Memory for In-Memory OLTP](https://msdn.microsoft.com/library/d82f21fa-6be1-4723-a72e-f2526fafd1b6)   
 [Monitor and Troubleshoot Memory Usage](../../relational-databases/in-memory-oltp/monitor-and-troubleshoot-memory-usage.md)   
 [Bind a Database with Memory-Optimized Tables to a Resource Pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md)   
 [Memory Management Architecture guide](../../relational-databases/memory-management-architecture-guide.md)  
 [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md) 
  
