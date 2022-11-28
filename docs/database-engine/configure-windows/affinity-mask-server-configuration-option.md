---
title: affinity mask Server Configuration Option
description: Learn about the affinity mask option in SQL Server. View an example that uses it to bind processors to specific threads.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: 06/11/2020
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "default affinity mask option"
  - "reloading processor cache"
  - "processor cache [SQL Server]"
  - "CPU [SQL Server], licensing"
  - "deferred process call"
  - "affinity mask option"
  - "processor affinity [SQL Server]"
  - "SMP"
  - "DPC"
---

# affinity mask Server Configuration Option

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

> [!NOTE]
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md) instead.

To carry out multitasking, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows sometimes move process threads among different processors. Although efficient from an operating system point of view, this activity can reduce SQL Server performance under heavy system loads, as each processor cache is repeatedly reloaded with data. Assigning processors to specific threads can improve performance under these conditions by eliminating processor reloads and reducing thread migration across processors, which reduces context switching. Such an association between a thread and a processor is called processor affinity.

SQL Server supports processor affinity by means of two affinity mask options: affinity mask (also known as **CPU affinity mask**) and affinity I/O mask. For more information on the affinity I/O mask option, see [affinity Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity-input-output-mask-server-configuration-option.md). CPU and I/O affinity support for servers with 33 to 64 processors requires the additional use of the [affinity64 mask Server Configuration Option](../../database-engine/configure-windows/affinity64-mask-server-configuration-option.md) and [affinity64 Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity64-input-output-mask-server-configuration-option.md), respectively.

> [!NOTE]  
> Affinity support for servers with 33 to 64 processors is only available on 64-bit operating systems.

The affinity mask option, which existed in earlier releases of SQL Server, dynamically controls CPU affinity.

In SQL Server, the affinity mask option can be configured without requiring a restart of the instance of SQL Server. When you're using sp_configure, you must run either RECONFIGURE or RECONFIGURE WITH OVERRIDE after setting a configuration option. When you're using [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], changing the affinity mask option does require a restart.

Changes to the affinity masks occur dynamically, allowing for on-demand startup and shutdown of the CPU schedulers that bind process threads within SQL Server. This can occur as conditions change on the server. For example, if a new instance of SQL Server is added to the server, it may be necessary to make adjustments to the affinity mask option to redistribute processor load.

Modifications to the affinity bitmasks require SQL Server to enable a new CPU scheduler and disable the existing CPU scheduler. New batches can then be processed on the new or remaining schedulers.

To start a new CPU scheduler, SQL Server creates a new scheduler and adds it to the list of its standard schedulers. The new scheduler is considered only for the new incoming batches. Current batches continue to run on the same scheduler. The workers migrate to the new scheduler as they free up, or as new workers are created.

Shutting down a scheduler requires all batches on the scheduler to complete their activities and exit. A scheduler that has been shut down is marked as offline so that no new batch is scheduled on it.

Whether you add or remove a new scheduler, the permanent system tasks such as lock monitor, checkpoint, system task thread (processing DTC), and signal process continue to run on the scheduler while the server is operational. These permanent system tasks do not dynamically migrate. To redistribute processor load for these system tasks across schedulers, it's necessary to restart the SQL Server instance. If SQL Server attempts to shut down a scheduler associated with a permanent system task, the task continues to run on the offline scheduler (no migration). This scheduler is bound to the processors in the modified affinity mask and doesn't put any load on the processor it was bound with before the change. Having extra offline schedulers shouldn't significantly affect the load of the system. If it does, a database server reboot is required to reconfigure these tasks on the schedulers available with the modified affinity mask.

Do not set the 'affinity mask' and 'affinity I/O mask' configuration values of SQL Server to use the same CPUs. Performance may suffer if you choose to bind a processor for both SQL Server worker thread scheduling and for I/O processing. Therefore, ensure that the configuration values aren't set for the same processor. The same recommendation applies to the 'affinity64 mask' and 'affinity64 I/O mask'.  To ensure that the affinity mask doesn't overlap with affinity IO mask, the [RECONFIGURE](../../t-sql/language-elements/reconfigure-transact-sql.md) command verifies that the normal CPU and I/O affinities are mutually exclusive. If not, an error message is reported to the client session and to the SQL Server error log, indicating that such a setting isn't recommended.

```sql
 Msg 5834, Level 16, State 1, Line 1
 The affinity mask specified conflicts with the IO affinity mask specified. Use the override option to force this configuration
```

Running RECONFIGURE WITH OVERRIDE options allows CPU and I/O affinities to overlap and to not be mutually exclusive.  

The I/O affinity tasks (such as lazy writer and log writer) are directly affected by the I/O affinity mask. If the lazy writer and log writer tasks aren't bound, they follow the same rules defined for the other permanent tasks such as lock monitor or checkpoint.
If you specify an affinity mask that attempts to map to a nonexistent CPU, the RECONFIGURE command reports an error message to both the client session and the SQL Server error log. Using the RECONFIGURE WITH OVERRIDE option has no effect in this case, and the same configuration error is reported again.

You can also exclude SQL Server activity from specific workload assignments by the Windows operating system. If you set a bit representing a processor to 1, that processor is selected by the SQL Server Database Engine for thread assignment. When you set **affinity mask** to 0 (the default), the Microsoft Windows scheduling algorithms set the thread's affinity. When you set **affinity mask** to any nonzero value, SQL Server affinity interprets the value as a bitmask that specifies those processors eligible for selection.  

By segregating SQL Server threads from running on particular processors, Microsoft Windows can better evaluate the system's handling of processes specific to Windows. For example, on an 8-CPU server running two instances of SQL Server (instance A and B), the system administrator could use the affinity mask option to assign the first set of 4 CPUs to instance A and the second set of 4 to instance B. To configure more than 32 processors, set both the affinity mask and the affinity64 mask. The values for **affinity mask** are as follows:

- A one-byte **affinity mask** covers up to 8 CPUs in a multiprocessor computer.

- A two-byte **affinity mask** covers up to 16 CPUs in a multiprocessor computer.

- A three-byte **affinity mask** covers up to 24 CPUs in a multiprocessor computer.

- A four-byte **affinity mask** covers up to 32 CPUs in a multiprocessor computer.

- To cover more than 32 CPUs, configure a four-byte affinity mask for the first 32 CPUs and up to a four-byte affinity64 mask for the remaining CPUs.

Because setting SQL Server processor affinity is a specialized operation, it's recommended that it's used only when necessary. In most cases, the Microsoft Windows default affinity provides the best performance. Consider the CPU requirements for other applications when setting the affinity masks. For more information, see your Windows operating system documentation.

> [!NOTE]
> You can use the Windows System Monitor to view and analyze individual processor usage.

When specifying the affinity I/O mask option, you must use it with the affinity mask configuration option. However, as mentioned earlier, do not enable the same CPU in both the **affinity mask** switch and the affinity I/O mask option. The bits corresponding to each CPU should be in one of these three states:

- 0 in both the affinity mask option and the affinity I/O mask option.

- 1 in the affinity mask option and 0 in the affinity I/O mask option.

- 0 in the affinity mask option and 1 in the affinity I/O mask option.

> [!CAUTION]
> Do not configure CPU affinity in the Windows operating system and also configure the affinity mask in SQL Server. These settings are attempting to achieve the same result, and if the configurations are inconsistent, you may have unpredictable results. SQL Server CPU affinity is best configured using the sp_configure option in SQL Server.

## Example

As an example of setting the affinity mask option, if processors 1, 2, and 5 are selected as available with bits in positions 1, 2, and 5 set to 1 and bits 0, 3, 4, 6, and 7 set to 0, a hexadecimal value of 0x26 or the decimal equivalent of 38 must be used. Number the bit positions from right to left..

```sql
sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
sp_configure 'affinity mask', 38;
RECONFIGURE;
GO
```

These are **affinity mask** values for an 8-CPU system.  

|Decimal value|Binary bit mask|Allow SQL Server threads on processors|  
|-------------------|---------------------|--------------------------------------------|  
|1|00000001|0|  
|3|00000011|0 and 1|  
|7|00000111|0, 1, and 2|  
|15|00001111|0, 1, 2, and 3|  
|31|00011111|0, 1, 2, 3, and 4|  
|63|00111111|0, 1, 2, 3, 4, and 5|  
|127|01111111|0, 1, 2, 3, 4, 5, and 6|  
|255|11111111|0, 1, 2, 3, 4, 5, 6, and 7|  

The affinity mask option is an advanced option. If you're using the sp_configure system stored procedure to change the setting, you can change **affinity mask** only when **show advanced options** is set to 1. After executing the [!INCLUDE[tsql](../../includes/tsql-md.md)] RECONFIGURE command, the new setting takes effect immediately without requiring a restart of the SQL Server instance.  

## Non-uniform memory access (NUMA)

When using hardware-based non-uniform memory access (NUMA) and the affinity mask is set, every scheduler in a node binds to its own CPU. When the affinity mask isn't set, each scheduler is bound to the group of CPUs within the NUMA node and a scheduler mapped to NUMA node N1 can schedule work on any CPU in the node, but not on CPUs associated with another node.  

Any operation running on a single NUMA node can only use buffer pages from that node. When an operation is run in parallel on CPUs from multiple nodes, memory can be used from any node involved.  
  
## Licensing issues

Dynamic affinity is tightly constrained by CPU licensing. SQL Server doesn't allow any configuration of 
affinity mask options that violates the licensing policy.  

### Startup

If a specified affinity mask violates the licensing policy during SQL Server startup or during database attach, the engine layer completes the startup process or database attach/restore operation, and then it resets the sp_configure run value for the affinity mask to zero, issuing an error message to the SQL Server error log.  
  
### Reconfigure

If a specified affinity mask violates the licensing policy when running [!INCLUDE[tsql](../../includes/tsql-md.md)] RECONFIGURE command, an error message is reported to the client session and to the SQL Server error log, requiring the database administrator to reconfigure the affinity mask. No RECONFIGURE WITH OVERRIDE command is accepted in this case.  

## Next steps

- [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)
- [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md)
