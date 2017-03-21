---
title: "Resolve Out Of Memory Issues | Microsoft Docs"
ms.custom: ""
ms.date: "08/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: f855e931-7502-44bd-8a8b-b8543645c7f4
caps.latest.revision: 18
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Resolve Out Of Memory Issues
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]

  [!INCLUDE[hek_1](../../includes/hek-1-md.md)] uses more memory and in different ways than does [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It is possible that the amount of memory you installed and allocated for [!INCLUDE[hek_2](../../includes/hek-2-md.md)] becomes inadequate for your growing needs. If so, you could run out of memory. This topic covers how to recover from an OOM situation. See [Monitor and Troubleshoot Memory Usage](../../relational-databases/in-memory-oltp/monitor-and-troubleshoot-memory-usage.md) for guidance that can help you avoid many OOM situations.  
  
## Covered in this topic  
  
|Topic|Overview|  
|-----------|--------------|  
|[Resolve database restore failures due to OOM](../../relational-databases/in-memory-oltp/resolve-out-of-memory-issues.md#bkmk_resolveRecoveryFailures)|What to do if you get the error message, “Restore operation failed for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'.”|  
|[Resolve impact of low memory or OOM conditions on the workload](../../relational-databases/in-memory-oltp/resolve-out-of-memory-issues.md#bkmk_recoverFromOOM)|What to do if you find low memory issues are negatively impacting performance.|  
|[Resolve page allocation failures due to insufficient memory when sufficient memory is available](../../relational-databases/in-memory-oltp/resolve-out-of-memory-issues.md#bkmk_PageAllocFailure)|What to do if you get the error message, “Disallowing page allocations for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'. …” when available memory is sufficient for the operation.|  
  
##  <a name="bkmk_resolveRecoveryFailures"></a> Resolve database restore failures due to OOM  
 When you attempt to restore a database you may get the error message: “Restore operation failed for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'.” This indicates that the server does not have enough available memory for restoring the database.
   
The server you restore a database to must have enough available memory for the memory-optimized tables in the database backup, otherwise the database will not come online.  
  
If the server does have enough physical memory, but you are still seeing this error, it could be that other processes are using too much memory or a configuration issue causes not enough memory to be available for restore. For this class of issues, use the following measures to make more memory available to the restore operation: 
  
-   Temporarily close running applications.   
    By closing one or more running applications or stopping services not needed at the moment, you make the memory they were using available for the restore operation. You can restart them following the successful restore.  
  
-   Increase the value of MAX_MEMORY_PERCENT.   
    If the database is [bound to a resource pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md), which is best practice, the memory available to restore is governed by MAX_MEMORY_PERCENT. If the value is too low, restore will fail. This code snippet changes MAX_MEMORY_PERCENT for the resource pool PoolHk to 70% of installed memory.  
  
    > [!IMPORTANT]  
    >  If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT to the same value as MAX_MEMORY_PERCENT.   
    > See the topic [Best Practices: Using In-Memory OLTP in a VM environment](http://msdn.microsoft.com/library/27ec7eb3-3a24-41db-aa65-2f206514c6f9) for more information.  
  
    ```tsql  
  
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
    For information on configuring **max server memory** see the topic [Optimizing Server Performance Using Memory Configuration Options](http://technet.microsoft.com/library/ms177455\(v=SQL.105\).aspx).  
  
##  <a name="bkmk_recoverFromOOM"></a> Resolve impact of low memory or OOM conditions on the workload  
 Obviously, it is best to not get into a low memory or OOM (Out of Memory) situation. Good planning and monitoring can help avoid OOM situations. Still, the best planning does not always foresee what actually happens and you might end up with low memory or OOM. There are two steps to recovering from OOM:  
  
1.  [Open a DAC (Dedicated Administrator Connection)](../../relational-databases/in-memory-oltp/resolve-out-of-memory-issues.md#bkmk_openDAC)  
  
2.  [Take corrective action](../../relational-databases/in-memory-oltp/resolve-out-of-memory-issues.md#bkmk_takeCorrectiveAction)  
  
###  <a name="bkmk_openDAC"></a> Open a DAC (Dedicated Administrator Connection)  
 Microsoft SQL Server provides a dedicated administrator connection (DAC). The DAC allows an administrator to access a running instance of SQL Server Database Engine to troubleshoot problems on the server—even when the server is unresponsive to other client connections. The DAC is available through the `sqlcmd` utility and SQL Server Management Studio (SSMS).  
  
 For guidance on using `sqlcmd` and DAC see [Using a Dedicated Administrator Connection](http://msdn.microsoft.com/library/ms189595\(v=sql.100\).aspx/css). For guidance on using DAC through SSMS see [How to: Use the Dedicated Administrator Connection with SQL Server Management Studio](http://msdn.microsoft.com/library/ms178068.aspx).  
  
###  <a name="bkmk_takeCorrectiveAction"></a> Take corrective action  
 To resolve your OOM condition you need to either free up existing memory by reducing usage, or make more memory available to your in-memory tables.  
  
#### Free up existing memory  
  
##### Delete non-essential memory optimized table rows and wait for garbage collection  
 You can remove non-essential rows from a memory optimized table. The garbage collector returns the memory used by these rows to available memory. . In-memory OLTP engine collects garbage rows aggressively. However, a long running transaction can prevent garbage collection. For example, if you have a transaction that runs for 5 minutes, any row versions created due to update/delete operations while the transaction was active can’t be garbage collected.  
  
##### Move one or more rows to a disk-based table  
 The following TechNet articles provide guidance on moving rows from a memory-optimized table to a disk-based table.  
  
-   [Application-Level Partitioning](http://technet.microsoft.com/library/dn296452\(v=sql.120\).aspx)  
  
-   [Application Pattern for Partitioning Memory-Optimized Tables](http://technet.microsoft.com/library/dn133171\(v=sql.120\).aspx)  
  
#### Increase available memory  
  
##### Increase value of MAX_MEMORY_PERCENT on the resource pool  
 If you have not created a named resource pool for your in-memory tables you should do that and bind your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] databases to it. See the topic [Bind a Database with Memory-Optimized Tables to a Resource Pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md) for guidance on creating and binding your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] databases to a resource pool.  
  
 If your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] database is bound to a resource pool you may be able to increase the percent of memory the pool can access. See the sub-topic [Change MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on an existing pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_ChangeAllocation) for guidance on changing the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT for a resource pool.  
  
 Increase the value of MAX_MEMORY_PERCENT.   
This code snippet changes MAX_MEMORY_PERCENT for the resource pool PoolHk to 70% of installed memory.  
  
> [!IMPORTANT]  
>  If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT to the same value.   
> See the topic [Best Practices: Using In-Memory OLTP in a VM environment](http://msdn.microsoft.com/library/27ec7eb3-3a24-41db-aa65-2f206514c6f9) for more information.  
  
```tsql  
  
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
  
##### Install additional memory  
 Ultimately the best solution, if possible, is to install additional physical memory. If you do this, remember that you will probably be able to also increase the value of MAX_MEMORY_PERCENT (see the sub-topic [Change MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on an existing pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#bkmk_ChangeAllocation)) since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] won’t likely need more memory, allowing you to make most if not all of the newly installed memory available to the resource pool.  
  
> [!IMPORTANT]  
>  If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT to the same value.   
> See the topic [Best Practices: Using In-Memory OLTP in a VM environment](http://msdn.microsoft.com/library/27ec7eb3-3a24-41db-aa65-2f206514c6f9) for more information.  
  
##  <a name="bkmk_PageAllocFailure"></a> Resolve page allocation failures due to insufficient memory when sufficient memory is available  
 If you get the error message, “Disallowing page allocations for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'. See 'http://go.microsoft.com/fwlink/?LinkId=330673' for more information.” in the error log when the available physical memory is sufficient to allocate the page, it may be due to a disabled Resource Governor. When the Resource Governor is disabled MEMORYBROKER_FOR_RESERVE induces artificial memory pressure.  
  
 To resolve this you need to enable the Resource Governor.  
  
 See [Enable Resource Governor](http://technet.microsoft.com/library/bb895149.aspx) for information on Limits and Restrictions as well as guidance on enabling Resource Governor using Object Explorer, Resource Governor properties, or Transact-SQL.  
  
## See Also  
 [Managing Memory for In-Memory OLTP](http://msdn.microsoft.com/library/d82f21fa-6be1-4723-a72e-f2526fafd1b6)   
 [Monitor and Troubleshoot Memory Usage](../../relational-databases/in-memory-oltp/monitor-and-troubleshoot-memory-usage.md)   
 [Bind a Database with Memory-Optimized Tables to a Resource Pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md)   
 [Best Practices: Using In-Memory OLTP in a VM environment](http://msdn.microsoft.com/library/27ec7eb3-3a24-41db-aa65-2f206514c6f9)  
  
  
