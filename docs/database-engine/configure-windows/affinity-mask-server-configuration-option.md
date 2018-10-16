---
title: "affinity mask Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
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
ms.assetid: 5823ba29-a75d-4b3e-ba7b-421c07ab3ac1
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# affinity mask Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md) instead.  
  
 To carry out multitasking, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows sometimes move process threads among different processors. Although efficient from an operating system point of view, this activity can reduce [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance under heavy system loads, as each processor cache is repeatedly reloaded with data. Assigning processors to specific threads can improve performance under these conditions by eliminating processor reloads and reducing thread migration across processors (thereby reducing context switching); such an association between a thread and a processor is called processor affinity.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports processor affinity by means of two affinity mask options: affinity mask (also known as **CPU affinity mask**) and affinity I/O mask. For more information on the affinity I/O maskoption, see [affinity Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity-input-output-mask-server-configuration-option.md). CPU and I/O affinity support for servers with 33 to 64 processors requires the additional use of the [affinity64 mask Server Configuration Option](../../database-engine/configure-windows/affinity64-mask-server-configuration-option.md) and [affinity64 Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity64-input-output-mask-server-configuration-option.md), respectively.  
  
> [!NOTE]  
>  Affinity support for servers with 33 to 64 processors is only available on 64-bit operating systems.  
  
 The affinity mask option, which existed in earlier releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], dynamically controls CPU affinity.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the affinity mask option can be configured without requiring a restart of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When you are using sp_configure, you must run either RECONFIGURE or RECONFIGURE WITH OVERRIDE after setting a configuration option. When you are using [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], changing the affinity mask option does require a restart.  
  
 Changes to the affinity masks occur dynamically, allowing for on-demand startup and shutdown of the CPU schedulers that bind process threads within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This can occur as conditions change on the server. For example, if a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is added to the server, it may be necessary to make adjustments to the affinity mask option to redistribute processor load.  
  
 Modifications to the affinity bitmasks require [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to enable a new CPU scheduler and disable the existing CPU scheduler. New batches can then be processed on the new or remaining schedulers.  
  
 To start a new CPU scheduler, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates a new scheduler and adds it to the list of its standard schedulers. The new scheduler is considered only for the new incoming batches. Current batches continue to run on the same scheduler. The workers migrate to the new scheduler as they free up, or as new workers are created.  
  
 Shutting down a scheduler requires all batches on the scheduler to complete their activities and exit. A scheduler that has been shut down is marked as offline so that no new batch is scheduled on it.  
  
 Whether a new scheduler is added or removed, the permanent system tasks such as lockmonitor, checkpoint, system task thread (processing DTC), and signal process continue to run on the scheduler while the server is operational. These permanent system tasks do not dynamically migrate. To redistribute processor load for these system tasks across schedulers, it is necessary to restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempts to shut down a scheduler associated with a permanent system task, the task continues to run on the offline scheduler (no migration). This scheduler is bound to the processors in the modified affinity mask and should not put any load on the processor it was affinitized with before the change. Having extra offline schedulers, should not significantly affect the load of the system. If this is not the case, a database server reboot is required to reconfigure these tasks.  
  
 The I/O affinity tasks (such as lazywriter and logwriter) are directly affected by the I/O affinity mask. If the lazywriter and logwriter tasks are not affinitized, they follow the same rules defined for the other permanent tasks such as lockmonitor or checkpoint.  
  
 To ensure that the new affinity mask is valid, the RECONFIGURE command verifies that the normal CPU and I/O affinities are mutually exclusive. If this is not the case, an error message is reported to the client session and to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, indicating that such a setting is not recommended. Running RECONFIGURE WITH OVERRIDE options allows CPU and I/O affinities that are not mutually exclusive.  
  
 If you specify an affinity mask that attempts to map to a nonexistent CPU, the RECONFIGURE command reports an error message to both the client session and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. Using the RECONFIGURE WITH OVERRIDE option has no effect in this case, and the same configuration error is reported again.  
  
 You can also exclude [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] activity from processors assigned specific workload assignments by the Windows 2000 or Windows Server 2003 operating system. If you set a bit representing a processor to 1, that processor is selected by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine for thread assignment. When you set **affinity mask** to 0 (the default), the Microsoft Windows 2000 or Windows Server 2003 scheduling algorithms set the thread's affinity. When you set **affinity mask** to any nonzero value, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] affinity interprets the value as a bitmask that specifies those processors eligible for selection.  
  
 By segregating [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] threads from running on particular processors, Microsoft Windows 2000 or Windows Server 2003 can better evaluate the system's handling of processes specific to Windows. For example, on an 8-CPU server running two instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (instance A and B), the system administrator could use the affinity mask option to assign the first set of 4 CPUs to instance A and the second set of 4 to instance B. To configure more than 32 processors, set both the affinity mask and the affinity64 mask. The values for **affinity mask** are as follows:  
  
-   A one-byte **affinity mask** covers up to 8 CPUs in a multiprocessor computer.  
  
-   A two-byte **affinity mask** covers up to 16 CPUs in a multiprocessor computer.  
  
-   A three-byte **affinity mask** covers up to 24 CPUs in a multiprocessor computer.  
  
-   A four-byte **affinity mask** covers up to 32 CPUs in a multiprocessor computer.  
  
-   To cover more than 32 CPUs, configure a four-byte affinity mask for the first 32 CPUs and up to a four-byte affinity64 mask for the remaining CPUs.  
  
 Because setting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processor affinity is a specialized operation, it is recommended that it be used only when necessary. In most cases, the Microsoft Windows 2000 or Windows Server 2003 default affinity provides the best performance. You should also consider the CPU requirements for other applications when setting the affinity masks. For more information, see your Windows operating system documentation.  
  
> [!NOTE]  
>  You can use the Windows System Monitor to view and analyze individual processor usage.  
  
 When specifying the affinity I/O mask option, you must use it in connection with the affinity mask configuration option. Do not enable the same CPU in both the **affinity mask** switch and the affinity I/O mask option. The bits corresponding to each CPU should be in one of these three states:  
  
-   0 in both the affinity mask option and the affinity I/O mask option.  
  
-   1 in the affinity mask option and 0 in the affinity I/O mask option.  
  
-   0 in the affinity mask option and 1 in the affinity I/O mask option.  
  
> [!CAUTION]  
>  Do not configure CPU affinity in the Windows operating system and also configure the affinity mask in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These settings are attempting to achieve the same result, and if the configurations are inconsistent, you may have unpredictable results. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CPU affinity is best configured using the sp_configure option in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Example  
 As an example of setting the affinity mask option, if processors 1, 2, and 5 are selected as available with bits 1, 2, and 5 set to 1 and bits 0, 3, 4, 6, and 7 set to 0, a hexadecimal value of 0x26 or the decimal equivalent of `38` is specified. Number the bits from right to left. The affinity mask option starts counting processors from 0 to 31, so that in the following example the counter `1` represents the second processor on the server.  
  
```  
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
  
 The affinity mask option is an advanced option. If you are using the sp_configure system stored procedure to change the setting, you can change **affinity mask** only when **show advanced options** is set to 1. After executing the [!INCLUDE[tsql](../../includes/tsql-md.md)] RECONFIGURE command, the new setting takes effect immediately without requiring a restart of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
## Non-uniform Memory Access (NUMA)  
 When using hardware based non-uniform memory access (NUMA) and the affinity mask is set, every scheduler in a node will be affinitized to its own CPU. When the affinity mask is not set, each scheduler is affinitized to the group of CPUs within the NUMA node and a scheduler mapped to NUMA node N1 can schedule work on any CPU in the node, but not on CPUs associated with another node.  
  
 Any operation running on a single NUMA node can only use buffer pages from that node. When an operation is run in parallel on CPUs from multiple nodes, memory can be used from any node involved.  
  
## Licensing Issues  
 Dynamic affinity is tightly constrained by CPU licensing. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not allow any configuration of affinity mask options that violates the licensing policy.  
  
### Startup  
 If a specified affinity mask violates the licensing policy during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup or during database attach, the engine layer will complete the startup process or database attach/restore operation, and then it will reset the sp_configure run value for the affinity mask to zero, issuing an error message to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  
  
### Reconfigure  
 If a specified affinity mask violates the licensing policy when running [!INCLUDE[tsql](../../includes/tsql-md.md)] RECONFIGURE command, an error message is reported to the client session and to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, requiring the database administrator to reconfigure the affinity mask. No RECONFIGURE WITH OVERRIDE command is accepted in this case.  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md)  
  
  
