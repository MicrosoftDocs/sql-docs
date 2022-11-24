---
title: "sys.dm_os_nodes (Transact-SQL)"
description: sys.dm_os_nodes (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/13/2018"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_nodes"
  - "dm_os_nodes_TSQL"
  - "dm_os_nodes"
  - "sys.dm_os_nodes_TSQL"
helpviewer_keywords:
  - "sys.dm_os_nodes dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: c768b67c-82a4-47f5-850b-0ea282358d50
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_nodes (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

An internal component named the SQLOS creates node structures that mimic hardware processor locality. These structures can be changed by using [soft-NUMA](../../database-engine/configure-windows/soft-numa-sql-server.md) to create custom node layouts.  

> [!NOTE]
> Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will automatically use soft-NUMA for certain hardware configurations. For more information, see [Automatic Soft-NUMA](../../database-engine/configure-windows/soft-numa-sql-server.md#automatic-soft-numa).
  
The following table provides information about these nodes.  
  
> [!NOTE]
> To call this DMV from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_nodes**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|node_id|**smallint**|ID of the node.|  
|node_state_desc|**nvarchar(256)**|Description of the node state. Values are displayed with the mutually exclusive values first, followed by the combinable values. For example:<br /> Online, Thread Resources Low, Lazy Preemptive<br /><br />There are four mutually exclusive node_state_desc values. They are listed below with their descriptions.<br /><ul><li>ONLINE: Node is online<li>OFFLINE: Node is offline<li>IDLE: Node has no pending work requests, and has entered an idle state.<li>IDLE_READY: Node has no pending work requests, and is ready to enter an idle state.</li></ul><br />There are three combinable node_state_desc values, listed below with their descriptions.<br /><ul><li>DAC: This node is reserved for the [Dedicated Administrative Connection](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md).<li>THREAD_RESOURCES_LOW: No new threads can be created on this node because of a low-memory condition.<li>HOT ADDED: Indicates the nodes were added in response to a hot add CPU event.</li></ul>|  
|memory_object_address|**varbinary(8)**|Address of memory object associated with this node. One-to-one relation to [sys.dm_os_memory_objects](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md).memory_object_address.|  
|memory_clerk_address|**varbinary(8)**|Address of memory clerk associated with this node. One-to-one relation to [sys.dm_os_memory_clerks](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md).memory_clerk_address.|  
|io_completion_worker_address|**varbinary(8)**|Address of worker assigned to IO completion for this node. One-to-one relation to [sys.dm_os_workers](../../relational-databases/system-dynamic-management-views/sys-dm-os-workers-transact-sql.md).worker_address.|  
|memory_node_id|**smallint**|ID of the memory node this node belongs to. Many-to-one relation to [sys.dm_os_memory_nodes](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-nodes-transact-sql.md).memory_node_id.|  
|cpu_affinity_mask|**bigint**|Bitmap identifying the CPUs this node is associated with.|  
|online_scheduler_count|**smallint**|Number of online schedulers that are managed by this node.|  
|idle_scheduler_count|**smallint**|Number of online schedulers that have no active workers.|  
|active_worker_count|**int**|Number of workers that are active on all schedulers managed by this node.|  
|avg_load_balance|**int**|Average number of tasks per scheduler on this node.|  
|timer_task_affinity_mask|**bigint**|Bitmap identifying the schedulers that can have timer tasks assigned to them.|  
|permanent_task_affinity_mask|**bigint**|Bitmap identifying the schedulers that can have permanent tasks assigned to them.|  
|resource_monitor_state|**bit**|Each node has one resource monitor assigned to it. The resource monitor can be running or idle. A value of 1 indicates running, a value of 0 indicates idle.|  
|online_scheduler_mask|**bigint**|Identifies the process affinity mask for this node.|  
|processor_group|**smallint**|Identifies the group of processors for this node.|  
|cpu_count |**int** |Number of CPUs available for this node. |
|pdw_node_id|**int**|The identifier for the node that this distribution is on.<br /><br /> **Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## See Also    
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)   
 [Soft-NUMA &#40;SQL Server&#41;](../../database-engine/configure-windows/soft-numa-sql-server.md)  
