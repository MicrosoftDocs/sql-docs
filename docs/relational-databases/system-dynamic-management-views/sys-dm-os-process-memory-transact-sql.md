---
title: "sys.dm_os_process_memory (Transact-SQL)"
description: sys.dm_os_process_memory (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_process_memory_TSQL"
  - "dm_os_process_memory_TSQL"
  - "dm_os_process_memory"
  - "sys.dm_os_process_memory"
helpviewer_keywords:
  - "sys.dm_os_process_memory dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: e838130c-95d4-4605-9e3b-eb0ab71cd250
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_process_memory (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

  Most memory allocations that are attributed to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process space are controlled through interfaces that allow for tracking and accounting of those allocations. However, memory allocations might be performed in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] address space that bypasses internal memory management routines. Values are obtained through calls to the base operating system. They are not manipulated by methods internal to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], except when it adjusts for locked or large page allocations.  
  
 All returned values that indicate memory sizes are shown in kilobytes (KB). The column **total_virtual_address_space_reserved_kb** is a duplicate of **virtual_memory_in_bytes** from **sys.dm_os_sys_info**.  
  
 The following table provides a complete picture of the process address space.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_process_memory**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**physical_memory_in_use_kb**|**bigint**|Indicates the process working set in KB, as reported by operating system, as well as tracked allocations by using large page APIs. Not nullable.|  
|**large_page_allocations_kb**|**bigint**|Specifies physical memory allocated by using large page APIs. Not nullable.|  
|**locked_page_allocations_kb**|**bigint**|Specifies memory pages locked in memory. Not nullable.|  
|**total_virtual_address_space_kb**|**bigint**|Indicates the total size of the user mode part of the virtual address space. Not nullable.|  
|**virtual_address_space_reserved_kb**|**bigint**|Indicates the total amount of virtual address space reserved by the process. Not nullable.|  
|**virtual_address_space_committed_kb**|**bigint**|Indicates the amount of reserved virtual address space that has been committed or mapped to physical pages. Not nullable.|  
|**virtual_address_space_available_kb**|**bigint**|Indicates the amount of virtual address space that is currently free. Not nullable.<br /><br /> **Note:** Free regions that are smaller than the allocation granularity can exist. These regions are unavailable for allocations.|  
|**page_fault_count**|**bigint**|Indicates the number of page faults that are incurred by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. Not nullable.|  
|**memory_utilization_percentage**|**int**|Specifies the percentage of committed memory that is in the working set. Not nullable.|  
|**available_commit_limit_kb**|**bigint**|Indicates the amount of memory that is available to be committed by the process. Not nullable.|  
|**process_physical_memory_low**|**bit**|Indicates that the process is responding to low physical memory notification. Not nullable.|  
|**process_virtual_memory_low**|**bit**|Indicates that low virtual memory condition has been detected. Not nullable.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  
 On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires VIEW SERVER STATE permission on the server.  
  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
