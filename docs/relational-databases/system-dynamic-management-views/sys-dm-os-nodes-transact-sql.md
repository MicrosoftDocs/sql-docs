---
title: "sys.dm_os_nodes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_os_nodes"
  - "dm_os_nodes_TSQL"
  - "dm_os_nodes"
  - "sys.dm_os_nodes_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_nodes dynamic management view"
ms.assetid: c768b67c-82a4-47f5-850b-0ea282358d50
caps.latest.revision: 33
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.dm_os_nodes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  An internal component named the SQLOS creates node structures that mimic hardware processor locality. These structures can be changed by using soft-NUMA to create custom node layouts.  
  
 The following table provides information about these nodes.  
  
> **NOTE:** To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_nodes**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|node_id|**smallint**|ID of the node.|  
|node_state_desc|**nvarchar(256)**|Description of the node state. Values are displayed with the mutually exclusive values first, followed by the combinable values. For example:<br /><br /> Online, Thread Resources Low, Lazy Preemptive<br /><br /> There are four mutually exclusive node_state_desc values. They are listed below with their descriptions.<br /><br /> ONLINE: Node is online<br /><br /> OFFLINE: Node is offline<br /><br /> IDLE: Node has no pending work requests, and has entered an idle state.<br /><br /> IDLE_READY: Node has no pending work requests, and is ready to enter an idle state.<br /><br /> There are five combinable node_state_desc values, listed below with their descriptions.<br /><br /> DAC: This node is reserved for the Dedicated Administrative Connection.<br /><br /> THREAD_RESOURCES_LOW: No new threads can be created on this node because of a low-memory condition.<br /><br /> HOT ADDED: Indicates the nodes were added in response to a hot add CPU event.|  
|memory_object_address|**varbinary(8)**|Address of memory object associated with this node. One-to-one relation to sys.dm_os_memory_objects.memory_object_address.|  
|memory_clerk_address|**varbinary(8)**|Address of memory clerk associated with this node. One-to-one relation to sys.dm_os_memory_clerks.memory_clerk_address.|  
|io_completion_worker_address|**varbinary(8)**|Address of worker assigned to IO completion for this node. One-to-one relation to sys.dm_os_workers.worker_address.|  
|memory_node_id|**smallint**|ID of the memory node this node belongs to. Many-to-one relation to sys.dm_os_memory_nodes.memory_node_id.|  
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
|cpu_count |**int** |Number of CPU's available for this node. |
|pdw_node_id|**int**|The identifier for the node that this distribution is on.<br /><br /> **Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]|  
  
## Permissions  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Premium Tiers, requires the `VIEW DATABASE STATE` permission in the database. On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Standard and Basic Tiers, requires the  **Server admin** or an **Azure Active Directory admin** account.  
  
## See Also  
  
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)   
 [Soft-NUMA &#40;SQL Server&#41;](../../database-engine/configure-windows/soft-numa-sql-server.md)  
  
  


