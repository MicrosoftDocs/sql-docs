---
title: "sys.dm_os_hosts (Transact-SQL)"
description: sys.dm_os_hosts (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/18/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_hosts_TSQL"
  - "dm_os_hosts"
  - "dm_os_hosts_TSQL"
  - "sys.dm_os_hosts"
helpviewer_keywords:
  - "sys.dm_os_hosts dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: a313ff3b-1fe9-421e-b94b-cea19c43b0e5
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||>=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_os_hosts (Transact-SQL)
[!INCLUDE [sql-asa-pdw](../../includes/applies-to-version/sql-asa-pdw.md)]

  Returns all the hosts currently registered in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This view also returns the resources that are used by these hosts.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_hosts**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**host_address**|**varbinary(8)**|Internal memory address of the host object.|  
|**type**|**nvarchar(60)**|Type of hosted component. For example,<br /><br /> SOSHOST_CLIENTID_SERVERSNI= SQL Server Native Interface<br /><br /> SOSHOST_CLIENTID_SQLOLEDB = SQL Server Native Client OLE DB Provider<br /><br /> SOSHOST_CLIENTID_MSDART = Microsoft Data Access Run Time|  
|**name**|**nvarchar(32)**|Name of the host.|  
|**enqueued_tasks_count**|**int**|Total number of tasks that this host has placed onto queues in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**active_tasks_count**|**int**|Number of currently running tasks that this host has placed onto queues.|  
|**completed_ios_count**|**int**|Total number of I/Os issued and completed through this host.|  
|**completed_ios_in_bytes**|**bigint**|Total byte count of the I/Os completed through this host.|  
|**active_ios_count**|**int**|Total number of I/O requests related to this host that are currently waiting to complete.|  
|**default_memory_clerk_address**|**varbinary(8)**|Memory address of the memory clerk object associated with this host. For more information, see [sys.dm_os_memory_clerks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md).|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows components, such as an OLE DB provider, that are not part of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executable to allocate memory and participate in non-preemptive scheduling. These components are hosted by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and all resources allocated by these components are tracked. Hosting allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to better account for resources used by components external to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executable.  
  
## Relationship Cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_os_hosts. default_memory_clerk_address|sys.dm_os_memory_clerks. memory_clerk_address|one to one|  
|sys.dm_os_hosts. host_address|sys.dm_os_memory_clerks. host_address|one to one|  
  
## Examples  
 The following example determines the total amount of memory committed by a hosted component.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.|  
  
```  
SELECT h.type, SUM(mc.pages_kb) AS commited_memory  
FROM sys.dm_os_memory_clerks AS mc   
INNER JOIN sys.dm_os_hosts AS h   
    ON mc.memory_clerk_address = h.default_memory_clerk_address  
GROUP BY h.type;  
```  
  
## See Also  

 [sys.dm_os_memory_clerks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md)   
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
