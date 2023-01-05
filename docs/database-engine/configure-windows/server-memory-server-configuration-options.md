---
title: "Server memory configuration options"
description: Learn how to configure the amount of memory the SQL Server Memory Manager allocates to SQL Server processes. View memory management approaches and examples.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf
ms.date: 08/01/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
ms.custom: contperf-fy20q4
helpviewer_keywords:
  - "Virtual Memory Manager"
  - "max server memory option"
  - "virtual memory [SQL Server]"
  - "VMM"
  - "server memory options [SQL Server]"
  - "servers [SQL Server], memory"
  - "buffer pools [SQL Server]"
  - "min server memory option"
  - "manual memory options [SQL Server]"
  - "memory [SQL Server], servers"
---
# Server memory configuration options

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Memory utilization for the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] is bounded by a pair of configuration settings, **min server memory (MB)** and **max server memory (MB)**. Over time and under normal circumstances, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will attempt claim memory up to the limit set by **max server memory (MB)**.

> [!NOTE]  
> [Columnstore indexes](../../relational-databases/indexes/columnstore-indexes-overview.md) and [In-Memory OLTP](../../relational-databases/in-memory-oltp/overview-and-usage-scenarios.md) objects have their own memory clerks, which makes it easier to monitor their buffer pool usage. For more information, see [sys.dm_os_memory_clerks](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md#types).

In older versions of SQL Server, memory utilization was virtually uncapped, indicating to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that all system memory was available for use. It is recommended in all versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to configure an upper limit for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory utilization by configuring the **max server memory (MB)**.

- Since [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], SQL Setup in Windows servers provides a recommendation for the **max server memory (MB)** for a standalone SQL Server instance based on a percentage of available system memory at the time of installation.
- At any time you can reconfigure the bounds of memory (in megabytes) for a SQL Server process used by an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] via the **min server memory (MB)** and **max server memory (MB)** configuration options.

> [!NOTE]  
> This guide refers to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on Windows. For information on memory configuration in Linux, see [Performance best practices and configuration guidelines for SQL Server on Linux](../../linux/sql-server-linux-performance-best-practices.md#virtual-machines-and-dynamic-memory) and the [memory.memorylimitmb setting](../../linux/sql-server-linux-configure-mssql-conf.md#memorylimit).

## Recommendations

The default settings and minimum allowable values for these options are:

|Option  |  Default | Minimum allowable  | Recommended |
|---------|---------|---------|---------|
|**min server memory (MB)**     |    0     |    0     | 0 |
|**max server memory (MB)**     |     2,147,483,647 megabytes (MB)     |  128 MB       | 75% of available system memory not consumed by other processes, including [other instances](#multiple-instances-of-). For more detailed recommendations, see [max server memory](#max_server_memory). |

Within these bounds, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can change its memory requirements dynamically based on available system resources. For more information, see [dynamic memory management](../../relational-databases/memory-management-architecture-guide.md#dynamic-memory-management).

- Setting **max server memory (MB)** value too high can cause a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to compete for memory with other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances hosted on the same host.
- However, setting **max server memory (MB)** too low is a lost performance opportunity, and could cause memory pressure and performance problems in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
- Setting **max server memory (MB)** to the minimum value can even prevent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from starting. If you can't start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] after changing this option, start it using the `-f` startup option and reset **max server memory (MB)** to its previous value. For more information, see [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md).
- It isn't recommended to set **max server memory (MB)** and **min server memory (MB)** to be the same value, or near the same values.

> [!NOTE]  
> The max server memory option only limits the size of the SQL Server buffer pool. The max server memory option does not limit a remaining unreserved memory area that SQL Server leaves for allocations of other components such as extended stored procedures, COM objects, non-shared DLLs and EXEs. 

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use memory dynamically. However, you can set the memory options manually and restrict the amount of memory that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can access. Before you set the amount of memory for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], determine the appropriate memory setting by subtracting, from the total physical memory, the memory required for the operating system (OS), memory allocations not controlled by the **max server memory (MB)** setting, and any other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (and other system uses, if the server is home to other applications that consume memory, including other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]). This difference is the maximum amount of memory you can assign to the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

Memory can be configured up to the process virtual address space limit in all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] editions. For more information, see [Memory Limits for Windows and Windows Server Releases](/windows/desktop/Memory/memory-limits-for-windows-releases#memory-and-address-space-limits).

## <a name="min_server_memory"></a> Min server memory

Use **min server memory (MB)** to guarantee a minimum amount of memory available to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Memory Manager.

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] won't immediately allocate the amount of memory specified in **min server memory (MB)** on startup. However, after memory usage has reached this value due to client load, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can't free memory unless the value of **min server memory (MB)** is reduced. For example, when several instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are installed concurrently in the same server, consider setting the **min server memory (MB)** parameter to reserve memory for an instance.

- Setting a **min server memory (MB)** value is essential in a virtualized environment to ensure memory pressure from the underlying host doesn't attempt to deallocate memory from the buffer pool on a guest virtual machine (VM) beyond what is needed for acceptable performance. Ideally, instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a virtual machine don't have to compete with the virtual host proactive memory deallocation processes.

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] isn't guaranteed to allocate the amount of memory specified in **min server memory (MB)**. If the load on the server never requires allocating the amount of memory specified in **min server memory (MB)**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will use less memory.

## <a name="max_server_memory"></a> Max server memory

Use **max server memory (MB)** to guarantee the OS and other applications don't experience detrimental memory pressure coming from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- Before you set the **max server memory (MB)** configuration, monitor overall memory consumption of the server hosting the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, during normal operation, to determine memory availability and requirements. For an initial configuration or when there was no opportunity to collect [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process memory usage over time, use the following generalized best practice approach to configure **max server memory (MB)** for a single instance:
  - From the total OS memory, subtract the equivalent of potential [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] thread memory allocations outside **max server memory (MB)** control, which is the **stack size**<sup>1</sup> multiplied by **calculated max worker threads**<sup>2</sup>.
  - Then subtract 25% for other memory allocations outside **max server memory (MB)** control, such as backup buffers, extended stored procedure DLLs, objects that are created by using Automation procedures (sp_OA calls), and allocations from linked server providers. This is a generic approximation, and your mileage may vary.
  - What remains should be the **max server memory (MB)** setting for a single instance setup.

<sup>1</sup> Refer to the [Memory Management Architecture guide](../../relational-databases/memory-management-architecture-guide.md#stacksizes) for information on thread stack sizes per architecture.

<sup>2</sup> Refer to the documentation page on how to [Configure the max worker threads Server Configuration Option](../../database-engine/configure-windows/configure-the-max-worker-threads-server-configuration-option.md), for information on the calculated default worker threads for a given number of affinitized CPUs in the current host.

## <a id="manually"></a> Set options manually

The server options **min server memory (MB)** and **max server memory (MB)** can be set to span a range of memory values. This method is useful for system or database administrators to configure an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the memory requirements of other applications, or other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that run on the same host.

### Use Transact-SQL

The **min server memory (MB)** and **max server memory (MB)** options are advanced options. When using the `sp_configure` system stored procedure to change these settings, you can change them only when **show advanced options** is set to 1. These settings take effect immediately without a server restart. For more information, see [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

The following example sets the **max server memory (MB)** option to 12,288 MB or 12 GB. Although `sp_configure` specifies the name of the option as `max server memory (MB)`, you can omit the `(MB)`.

```sql
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'max server memory', 12288;
GO
RECONFIGURE;
GO
```

The following query returns information about the currently configured values, and the value currently in use. This query will return results regardless of whether the `sp_configure` option 'show advanced options' is enabled.

```sql
SELECT [name], [value], [value_in_use]
FROM sys.configurations
WHERE [name] = 'max server memory (MB)' OR [name] = 'min server memory (MB)';
```

### Use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]

Use **min server memory (MB)** and **max server memory (MB)** to reconfigure the amount of memory (in megabytes) managed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Memory Manager for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

1.  In Object Explorer, right-click a server and select **Properties**.

2.  Select the **Memory** page of the **Server Properties** window. The current values of **Minimum server memory** and **Maximum server memory** are displayed.

3.  In **Server memory options**, enter desired numbers for **Minimum server memory** and **Maximum server memory**. For recommendations, see [min server memory (MB)](#min_server_memory) and [max server memory (MB)](#max_server_memory) in this article.

The following screenshot demonstrates all three steps:

:::image type="content" source="media/server-memory-server-configuration-options/configure-memory-in-ssms.png" alt-text="Screenshot of the memory configuration options in SSMS.":::

## Lock pages in memory (LPIM)

Windows-based applications can use Windows Address Windowing Extensions (AWE) APIs to allocate and map physical memory into the process address space. The LPIM Windows policy determines which accounts can access the API to keep data in physical memory, preventing the system from paging the data to virtual memory on disk. The memory allocated using AWE is locked until the application explicitly frees it or exits. Using the AWE APIs for memory management in 64-bit SQL Server is also frequently referred to as *locked pages*. Locking pages in memory may keep the server responsive when paging memory to disk occurs. The **Lock pages in memory** option is **enabled** in instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard edition and higher when the account with privileges to run `sqlservr.exe` has been granted the Windows *Lock pages in memory* (LPIM) user right.

To disable the **Lock pages in memory** option for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], remove the *Lock pages in memory* user right for the account with privileges to run `sqlservr.exe` (the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup account) startup account.

Using LPIM doesn't affect [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [dynamic memory management](../../relational-databases/memory-management-architecture-guide.md#dynamic-memory-management), allowing it to expand or shrink at the request of other memory clerks. When using the *Lock pages in memory* user right, it is strongly recommended to set an upper limit for **max server memory (MB)**. For more information, see [max server memory (MB)](#max_server_memory).

LPIM should be used when there are signs that the `sqlservr` process is being paged out. In this case, error 17890 will be reported in the Errorlog, resembling the below example:

```output
A significant part of sql server process memory has been paged out. This may result in a performance degradation. Duration: #### seconds. Working set (KB): ####, committed (KB): ####, memory utilization: ##%.
```

Using LPIM with an incorrectly configured **max server memory (MB)** setting that does not account for other memory consumers in the system may cause instability, depending on the amount of memory required by other processes, or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory requirements outside the scope of **max server memory (MB)**. For more information, see [max server memory](#max_server_memory). If the **Lock pages in memory** (LPIM) privilege is granted (on 32-bit or 64-bit systems), we strongly recommend that you set **max server memory (MB)** to a specific value, rather than leaving the default of 2,147,483,647 megabytes (MB).

> [!NOTE]  
> Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [trace flag 845](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) is not needed for Standard Edition to use Locked Pages.

### Enable *Lock pages in memory*

After considering the previous information, to enable the **Lock pages in memory** option by granting the privilege to the service account for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Enable the Lock pages in memory Option (Windows)](enable-the-lock-pages-in-memory-option-windows.md#enable-the-lock-pages-in-memory-option-windows).

To determine the service account for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], refer to the SQL Server Configuration Manager or query the `service_account` from `sys.dm_server_services`. For more information, see [sys.dm_server_services (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-server-services-transact-sql.md).

### View *Lock pages in memory* status

To determine whether **Lock pages in memory** privilege is granted to the service account for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use the following query. This query is supported in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1 and later.

```sql
SELECT sql_memory_model_desc FROM sys.dm_os_sys_info;
```

The following values of `sql_memory_model_desc` indicate the status of LPIM:

- `CONVENTIONAL`. Lock pages in memory privilege isn't granted.
- `LOCK_PAGES`. Lock pages in memory privilege is granted.
- `LARGE_PAGES`. Lock pages in memory privilege is granted in Enterprise mode with Trace Flag 834 enabled. This is an advanced configuration and not recommended for most environments. For more information and important caveats, see [Trace Flag 834](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf834).

Use the following methods to determine whether the SQL Server instance is using locked pages:

- The output of the following Transact-SQL query will indicate nonzero values for `locked_page_allocations_kb`:

    ```sql
    SELECT osn.node_id, osn.memory_node_id, osn.node_state_desc, omn.locked_page_allocations_kb 
    FROM sys.dm_os_memory_nodes omn 
    INNER JOIN sys.dm_os_nodes osn ON (omn.memory_node_id = osn.memory_node_id) 
    WHERE osn.node_state_desc <> 'ONLINE DAC';
    ```

- The current SQL Server error log will report the message, "Using locked pages in the memory manager" during server startup.

- The Memory Manager section of the [DBCC MEMORYSTATUS](/troubleshoot/sql/performance/dbcc-memorystatus-monitor-memory-usage#memory-manager) output will show a nonzero value for the `AWE Allocated` item.

## Multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

When you are running multiple instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], there are different approaches you can use to manage memory:

- Use **max server memory (MB)** in each instance to control memory usage, as [detailed above](#max_server_memory). Establish maximum settings for each instance, being careful that the total allowance isn't more than the total physical memory on your machine. You might want to give each instance memory proportional to its expected workload or database size. This approach has the advantage that when new processes or instances start up, free memory will be available to them immediately. The drawback is that if you aren't running all of the instances, none of the running instances will be able to utilize the remaining free memory.

- Use **min server memory (MB)** in each instance to control memory usage, as [detailed above](#min_server_memory). Establish minimum settings for each instance, so that the sum of these minimums is 1 - 2 GB less than the total physical memory on your machine. Again, you may establish these minimums proportionately to the expected load of that instance. This approach has the advantage that if not all instances are running at the same time, the ones that are running can use the remaining free memory. This approach is also useful when there is another memory-intensive process on the computer, since it would ensure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] would at least get a reasonable amount of memory. The drawback is that when a new instance (or any other process) starts, it may take some time for the running instances to release memory, especially if they must write modified pages back to their databases to do so.

- Use both **max server memory (MB)** and **min server memory (MB)** in each instance to control memory usage, observing and tuning each instance's maximum utilization and minimum memory protection within a wide range of potential memory utilization levels.

- Do nothing (not recommended). The first instances presented with a workload will tend to allocate all of memory. Idle instances, or instances started later, may end up running with only a minimal amount of memory available. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] makes no attempt to balance memory usage across instances. All instances will, however, respond to Windows Memory Notification signals to adjust the size of their memory footprint. Windows doesn't balance memory across applications with the Memory Notification API. It merely provides global feedback as to the availability of memory on the system.

You can change these settings without restarting the instances, so you can easily experiment to find the best settings for your usage pattern.

## Examples

### A. Set the max server memory option to 4 GB

The following example sets the **max server memory (MB)** option to 4096 MB or 4 GB. Although `sp_configure` specifies the name of the option as `max server memory (MB)`, you can omit the `(MB)`.

```sql
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'max server memory', 4096;
GO
RECONFIGURE;
GO
```

This will output a statement similar to `Configuration option 'max server memory (MB)' changed from 2147483647 to 4096. Run the RECONFIGURE statement to install.` The new memory limit takes effect immediately upon execution of `RECONFIGURE`. For more information, see [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

### B. Determine current memory allocation

The following query returns information about currently allocated memory.

```sql
SELECT
  physical_memory_in_use_kb/1024 AS sql_physical_memory_in_use_MB,
   large_page_allocations_kb/1024 AS sql_large_page_allocations_MB,
   locked_page_allocations_kb/1024 AS sql_locked_page_allocations_MB,
   virtual_address_space_reserved_kb/1024 AS sql_VAS_reserved_MB,
   virtual_address_space_committed_kb/1024 AS sql_VAS_committed_MB,
   virtual_address_space_available_kb/1024 AS sql_VAS_available_MB,
   page_fault_count AS sql_page_fault_count,
   memory_utilization_percentage AS sql_memory_utilization_percentage,
   process_physical_memory_low AS sql_process_physical_memory_low,
   process_virtual_memory_low AS sql_process_virtual_memory_low
FROM sys.dm_os_process_memory;
```

### C. View the value of `max server memory (MB)`

The following query returns information about the currently configured value and the value in use. This query will return results regardless of whether the `sp_configure` option 'show advanced options' is enabled.

```sql
SELECT [value], [value_in_use]
FROM sys.configurations WHERE [name] = 'max server memory (MB)';
```

## Next steps

- [Memory Management Architecture Guide](../../relational-databases/memory-management-architecture-guide.md)
- [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)
- [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md)
- [Memory Limits for Windows and Windows Server Releases](/windows/desktop/Memory/memory-limits-for-windows-releases)