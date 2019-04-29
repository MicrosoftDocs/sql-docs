---
title: "sys.dm_os_sys_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/24/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_os_sys_info_TSQL"
  - "dm_os_sys_info"
  - "dm_os_sys_info_TSQL"
  - "sys.dm_os_sys_info"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_sys_info dynamic management view"
  - "time [SQL Server], instance started"
  - "starting time"
ms.assetid: 20f6bc9c-839a-4fa4-b3f3-a6c47d1b69af
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_sys_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Returns a miscellaneous set of useful information about the computer, and about the resources available to and consumed by [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].  
  
> **NOTE:** To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_sys_info**.  
  
|Column name|Data type|Description and version-specific notes |  
|-----------------|---------------|-----------------|  
|**cpu_ticks**|**bigint**|Specifies the current CPU tick count. CPU ticks are obtained from the processor's RDTSC counter. It is a monotonically increasing number. Not nullable.|  
|**ms_ticks**|**bigint**|Specifies the number of milliseconds since the computer started. Not nullable.|  
|**cpu_count**|**int**|Specifies the number of logical CPUs on the system. Not nullable.|  
|**hyperthread_ratio**|**int**|Specifies the ratio of the number of logical or physical cores that are exposed by one physical processor package. Not nullable.|  
|**physical_memory_in_bytes**|**bigint**|**Applies to:** [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Specifies the total amount of physical memory on the machine. Not nullable.|  
|**physical_memory_kb**|**bigint**|**Applies to:** [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Specifies the total amount of physical memory on the machine. Not nullable.|  
|**virtual_memory_in_bytes**|**bigint**|**Applies to:** [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Amount of virtual memory available to the process in user mode. This can be used to determine whether SQL Server was started by using a 3-GB switch.|  
|**virtual_memory_kb**|**bigint**|**Applies to:** [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Specifies the total amount of virtual address space available to the process in user mode. Not nullable.|  
|**bpool_commited**|**int**|**Applies to:** [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Represents the committed memory in kilobytes (KB) in the memory manager. Does not include reserved memory in the memory manager. Not nullable.|  
|**committed_kb**|**int**|**Applies to:** [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Represents the committed memory in kilobytes (KB) in the memory manager. Does not include reserved memory in the memory manager. Not nullable.|  
|**bpool_commit_target**|**int**|**Applies to:** [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Represents the amount of memory, in kilobytes (KB), that can be consumed by SQL Server memory manager.|  
|**committed_target_kb**|**int**|**Applies to:** [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Represents the amount of memory, in kilobytes (KB), that can be consumed by SQL Server memory manager. The target amount is calculated using a variety of inputs like:<br /><br /> - the current state of the system including its load<br /><br /> - the memory requested by current processes<br /><br /> - the amount of memory installed on the computer<br /><br /> - configuration parameters<br /><br /> If **committed_target_kb** is larger than **committed_kb**, the memory manager will try to obtain additional memory. If **committed_target_kb** is smaller than **committed_kb**, the memory manager will try to shrink the amount of memory committed. The **committed_target_kb** always includes stolen and reserved memory. Not nullable.|  
|**bpool_visible**|**int**|**Applies to:** [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Number of 8-KB buffers in the buffer pool that are directly accessible in the process virtual address space. When not using the Address Windowing Extensions (AWE), when the buffer pool has obtained its memory target (bpool_committed = bpool_commit_target), the value of bpool_visible equals the value of bpool_committed.When using AWE on a 32-bit version of SQL Server, bpool_visible represents the size of the AWE mapping window used to access physical memory allocated by the buffer pool. The size of this mapping window is bound by the process address space and, therefore, the visible amount will be smaller than the committed amount, and can be further reduced by internal components consuming memory for purposes other than database pages. If the value of bpool_visible is too low, you might receive out of memory errors.|  
|**visible_target_kb**|**int**|**Applies to:** [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Is the same as **committed_target_kb**. Not nullable.|  
|**stack_size_in_bytes**|**int**|Specifies the size of the call stack for each thread created by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Not nullable.|  
|**os_quantum**|**bigint**|Represents the Quantum for a non-preemptive task, measured in milliseconds. Quantum (in seconds) = **os_quantum** / CPU clock speed. Not nullable.|  
|**os_error_mode**|**int**|Specifies the error mode for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. Not nullable.|  
|**os_priority_class**|**int**|Specifies the priority class for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. Nullable.<br /><br /> 32 = Normal (Error log will say [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is starting at normal priority base (=7).)<br /><br /> 128 = High (Error log will say [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running at high priority base.  (=13).)<br /><br /> For more information, see [Configure the priority boost Server Configuration Option](../../database-engine/configure-windows/configure-the-priority-boost-server-configuration-option.md).|  
|**max_workers_count**|**int**|Represents the maximum number of workers that can be created. Not nullable.|  
|**scheduler_count**|**int**|Represents the number of user schedulers configured in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. Not nullable.|  
|**scheduler_total_count**|**int**|Represents the total number of schedulers in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Not nullable.|  
|**deadlock_monitor_serial_number**|**int**|Specifies the ID of the current deadlock monitor sequence. Not nullable.|  
|**sqlserver_start_time_ms_ticks**|**bigint**|Represents the **ms_tick** number when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] last started. Compare to the current ms_ticks column. Not nullable.|  
|**sqlserver_start_time**|**datetime**|Specifies the date and time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] last started. Not nullable.|  
|**affinity_type**|**int**|**Applies to:** [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Specifies the type of server CPU process affinity currently in use. Not nullable. For more information, see [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md).<br /><br /> 1 = MANUAL<br /><br /> 2 = AUTO|  
|**affinity_type_desc**|**varchar(60)**|**Applies to:** [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Describes the **affinity_type** column. Not nullable.<br /><br /> MANUAL = affinity has been set for at least one CPU.<br /><br /> AUTO = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can freely move threads between CPUs.|  
|**process_kernel_time_ms**|**bigint**|**Applies to:** [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Total time in milliseconds spent by all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] threads in kernel mode. This value can be larger than a single processor clock because it includes the time for all processors on the server. Not nullable.|  
|**process_user_time_ms**|**bigint**|**Applies to:** [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Total time in milliseconds spent by all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] threads in user mode. This value can be larger than a single processor clock because it includes the time for all processors on the server. Not nullable.|  
|**time_source**|**int**|**Applies to:** [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Indicates the API that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is using to retrieve wall clock time. Not nullable.<br /><br /> 0 = QUERY_PERFORMANCE_COUNTER<br /><br /> 1 = MULTIMEDIA_TIMER|  
|**time_source_desc**|**nvarchar(60)**|**Applies to:** [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Describes the **time_source** column. Not nullable.<br /><br /> QUERY_PERFORMANCE_COUNTER = the [QueryPerformanceCounter](https://go.microsoft.com/fwlink/?LinkId=163095) API retrieves wall clock time.<br /><br /> MULTIMEDIA_TIMER = The [multimedia timer](https://go.microsoft.com/fwlink/?LinkId=163094) API that retrieves wall clock time.|  
|**virtual_machine_type**|**int**|**Applies to:** [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Indicates whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running in a virtualized environment.  Not nullable.<br /><br /> 0 = NONE<br /><br /> 1 = HYPERVISOR<br /><br /> 2 = OTHER|  
|**virtual_machine_type_desc**|**nvarchar(60)**|**Applies to:** [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Describes the **virtual_machine_type** column. Not nullable.<br /><br /> NONE = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not running inside a virtual machine.<br /><br /> HYPERVISOR = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running inside a hypervisor, which implies a hardware-assisted virtualization. When the Hyper_V role is installed, the hypervisor hosts the OS, so an instance running on the host OS is running in the hypervisor.<br /><br /> OTHER = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running inside a virtual machine that does not employ hardware assistant such as Microsoft Virtual PC.|  
|**softnuma_configuration**|**int**|**Applies to:** [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Specifies the way NUMA nodes are configured. Not nullable.<br /><br /> 0 = OFF indicates hardware default<br /><br /> 1 = Automated soft-NUMA<br /><br /> 2 = Manual soft-NUMA via registry|  
|**softnuma_configuration_desc**|**nvarchar(60)**|**Applies to:** [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> OFF = Soft-NUMA feature is OFF<br /><br /> ON = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically determines the NUMA node sizes for Soft-NUMA<br /><br /> MANUAL = Manually configured soft-NUMA|
|**process_physical_affinity**|**nvarchar(3072)** |**Applies to:** Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].<br /><br />Information yet to come. |
|**sql_memory_model**|**int**|**Applies to:** [!INCLUDE[sssql11](../../includes/sssql11-md.md)] SP4, [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br />Specifies the memory model used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to allocate memory. Not nullable.<br /><br />1 = Conventional Memory Model<br />2 = Lock Pages in Memory<br /> 3 = Large Pages in Memory|
|**sql_memory_model_desc**|**nvarchar(120)**|**Applies to:** [!INCLUDE[sssql11](../../includes/sssql11-md.md)] SP4, [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br />Specifies the memory model used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to allocate memory. Not nullable.<br /><br />**CONVENTIONAL** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is using Conventional Memory model to allocate memory. This is default sql memory model when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account does not have Lock Pages in Memory privileges during startup.<br />**LOCK_PAGES** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is using Lock Pages in Memory to allocate memory. This is the default sql memory manager when SQL Server service account possess Lock Pages in Memory privilege during SQL Server startup.<br /> **LARGE_PAGES** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is using Large Pages in Memory to allocate memory. SQL Server uses Large Pages allocator to allocate memory only with Enterprise edition when SQL Server service account possess Lock Pages in Memory privilege during server startup and when Trace Flag 834 is turned ON.|
|**pdw_node_id**|**int**|**Applies to:** [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
|**socket_count** |**int** | **Applies to:** [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br />Specifies the number of processor sockets available on the system. |  
|**cores_per_socket** |**int** | **Applies to:** [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br />Specifies the number of processors per socket available on the system. |  
|**numa_node_count** |**int** | **Applies to:** [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br />Specifies the number of numa nodes available on the system. This column includes physical numa nodes as well as soft numa nodes. |  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
  



