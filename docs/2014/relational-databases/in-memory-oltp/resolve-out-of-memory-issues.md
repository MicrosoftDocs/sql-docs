---
title: "Resolve Out Of Memory Issues | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: f855e931-7502-44bd-8a8b-b8543645c7f4
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Resolve Out Of Memory Issues
  [!INCLUDE[hek_1](../../includes/hek-1-md.md)] uses more memory and in different ways than does [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It is possible that the amount of memory you installed and allocated for [!INCLUDE[hek_2](../../includes/hek-2-md.md)] becomes inadequate for your growing needs. If so, you could run out of memory. This topic covers how to recover from an OOM situation. See [Monitor and Troubleshoot Memory Usage](monitor-and-troubleshoot-memory-usage.md) for guidance that can help you avoid many OOM situations.  
  
## Covered in this topic  
  
|Topic|Overview|  
|-----------|--------------|  
| [Resolve database restore failures due to OOM](#resolve-database-restore-failures-due-to-oom) |What to do if you get the error message, "Restore operation failed for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'."|  
| [Resolve impact of low memory or OOM conditions on the workload](#resolve-impact-of-low-memory-or-oom-conditions-on-the-workload)|What to do if you find low memory issues are negatively impacting performance.|  
| [Resolve page allocation failures due to insufficient memory when sufficient memory is available](#resolve-page-allocation-failures-due-to-insufficient-memory-when-sufficient-memory-is-available) |What to do if you get the error message, "Disallowing page allocations for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'. ..." when available memory is sufficient for the operation.|  
  
## Resolve database restore failures due to OOM  
 When you attempt to restore a database you may get the error message: "Restore operation failed for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'." Before you can successfully restore the database you must resolve the insufficient memory issue by making more memory available.  
  
 To resolve recovery failure due to OOM increase available memory using any or al of these means to temporarily increase memory available for the recovery operation.  
  
-   Temporarily close running applications.   
    By closing one or more running applications, such as Visual Studio, Internet Explorer, OneNote, and others, you make the memory they were using available for the restore operation. You can restart them following the successful restore.  
  
-   Increase the value of MAX_MEMORY_PERCENT.   
    This code snippet changes MAX_MEMORY_PERCENT for the resource pool PoolHk to 70% of installed memory.  
  
    > [!IMPORTANT]  
    >  If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT to the same value as MAX_MEMORY_PERCENT.   
    > See the topic [Best Practices: Using In-Memory OLTP in a VM environment](../../database-engine/using-in-memory-oltp-in-a-vm-environment.md) for more information.  
  
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
  
     For information on maximum values for MAX_MEMORY_PERCENT see the topic section [Percent of memory available for memory-optimized tables and indexes](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#-percent-of-memory-available-for-memory-optimized-tables-and-indexes)
  
-   Reconfigure **max server memory**.  
    For information on configuring **max server memory** see the topic [Optimizing Server Performance Using Memory Configuration Options](https://technet.microsoft.com/library/ms177455\(v=SQL.105\).aspx).  
  
## Resolve impact of low memory or OOM conditions on the workload  
 Obviously, it is best to not get into a low memory or OOM (Out of Memory) situation. Good planning and monitoring can help avoid OOM situations. Still, the best planning does not always foresee what actually happens and you might end up with low memory or OOM. There are two steps to recovering from OOM:  
  
1.  [Open a DAC (Dedicated Administrator Connection)](resolve-out-of-memory-issues.md#bkmk_opendac)  
  
2.  [Take corrective action](resolve-out-of-memory-issues.md#bkmk_takecorrectiveaction)  
  
###  Open a DAC (Dedicated Administrator Connection)  
 Microsoft SQL Server provides a dedicated administrator connection (DAC). The DAC allows an administrator to access a running instance of SQL Server Database Engine to troubleshoot problems on the server-even when the server is unresponsive to other client connections. The DAC is available through the `sqlcmd` utility and SQL Server Management Studio (SSMS).  
  
 For guidance on using `sqlcmd` and DAC see [Using a Dedicated Administrator Connection](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md). For guidance on using DAC through SSMS see [How to: Use the Dedicated Administrator Connection with SQL Server Management Studio](https://msdn.microsoft.com/library/ms178068.aspx).  
  
###  Take corrective action  
 To resolve your OOM condition you need to either free up existing memory by reducing usage, or make more memory available to your in-memory tables.  
  
#### Free up existing memory  
  
##### Delete non-essential memory optimized table rows and wait for garbage collection  
 You can remove non-essential rows from a memory optimized table. The garbage collector returns the memory used by these rows to available memory. . In-memory OLTP engine collects garbage rows aggressively. However, a long running transaction can prevent garbage collection. For example, if you have a transaction that runs for 5 minutes, any row versions created due to update/delete operations while the transaction was active can't be garbage collected.  
  
##### Move one or more rows to a disk-based table  
 The following TechNet articles provide guidance on moving rows from a memory-optimized table to a disk-based table.  
  
-   [Application-Level Partitioning](https://technet.microsoft.com/library/dn296452\(v=sql.120\).aspx)  
  
-   [Application Pattern for Partitioning Memory-Optimized Tables](https://technet.microsoft.com/library/dn133171\(v=sql.120\).aspx)  
  
#### Increase available memory  
  
##### Increase value of MAX_MEMORY_PERCENT on the resource pool  
 If you have not created a named resource pool for your in-memory tables you should do that and bind your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] databases to it. See the topic [Bind a Database with Memory-Optimized Tables to a Resource Pool](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md) for guidance on creating and binding your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] databases to a resource pool.  
  
 If your [!INCLUDE[hek_2](../../includes/hek-2-md.md)] database is bound to a resource pool you may be able to increase the percent of memory the pool can access. See the sub-topic [Change MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on an existing pool](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#-change-minmemorypercent-and-maxmemorypercent-on-an-existing-pool) for guidance on changing the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT for a resource pool.  
  
 Increase the value of MAX_MEMORY_PERCENT.   
This code snippet changes MAX_MEMORY_PERCENT for the resource pool PoolHk to 70% of installed memory.  
  
> [!IMPORTANT]  
>  If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT to the same value.   
> See the topic [Best Practices: Using In-Memory OLTP in a VM environment](../../database-engine/using-in-memory-oltp-in-a-vm-environment.md) for more information.  
  
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
  
 For information on maximum values for MAX_MEMORY_PERCENT see the topic section [Percent of memory available for memory-optimized tables and indexes](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#-percent-of-memory-available-for-memory-optimized-tables-and-indexes).  
  
##### Install additional memory  
 Ultimately the best solution, if possible, is to install additional physical memory. If you do this, remember that you will probably be able to also increase the value of MAX_MEMORY_PERCENT (see the sub-topic [Change MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT on an existing pool](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md#-change-minmemorypercent-and-maxmemorypercent-on-an-existing-pool)) since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] won't likely need more memory, allowing you to make most if not all of the newly installed memory available to the resource pool.  
  
> [!IMPORTANT]  
>  If the server is running on a VM and is not dedicated, set the value of MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT to the same value.   
> See the topic [Best Practices: Using In-Memory OLTP in a VM environment](../../database-engine/using-in-memory-oltp-in-a-vm-environment.md) for more information.  
  
## Resolve page allocation failures due to insufficient memory when sufficient memory is available  
 If you get the error message, "Disallowing page allocations for database '*\<databaseName>*' due to insufficient memory in the resource pool '*\<resourcePoolName>*'. See '<https://go.microsoft.com/fwlink/?LinkId=330673>' for more information." in the error log when the available physical memory is sufficient to allocate the page, it may be due to a disabled Resource Governor. When the Resource Governor is disabled MEMORYBROKER_FOR_RESERVE induces artificial memory pressure.  
  
 To resolve this you need to enable the Resource Governor.  
  
 See [Enable Resource Governor](https://technet.microsoft.com/library/bb895149.aspx) for information on Limits and Restrictions as well as guidance on enabling Resource Governor using Object Explorer, Resource Governor properties, or Transact-SQL.  
  
## See Also  
 [Managing Memory for In-Memory OLTP](../../database-engine/managing-memory-for-in-memory-oltp.md)   
 [Monitor and Troubleshoot Memory Usage](monitor-and-troubleshoot-memory-usage.md)   
 [Bind a Database with Memory-Optimized Tables to a Resource Pool](bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md)   
 [Best Practices: Using In-Memory OLTP in a VM environment](../../database-engine/using-in-memory-oltp-in-a-vm-environment.md)  
  
  
