---
title: "sys.dm_exec_query_resource_semaphores (Transact-SQL)"
description: sys.dm_exec_query_resource_semaphores (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/09/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_exec_query_resource_semaphores_TSQL"
  - "dm_exec_query_resource_semaphores_TSQL"
  - "sys.dm_exec_query_resource_semaphores"
  - "dm_exec_query_resource_semaphores"
helpviewer_keywords:
  - "sys.dm_exec_query_resource_semaphores dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: e43a2aa9-dd52-4c89-911e-1a7d05f7ffbb
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_query_resource_semaphores (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the information about the current query-resource semaphore status in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. **sys.dm_exec_query_resource_semaphores** provides general query-execution memory status and allows you to determine whether the system can access enough memory. This view complements memory information obtained from [sys.dm_os_memory_clerks](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md) to provide a complete picture of server memory status. **sys.dm_exec_query_resource_semaphores** returns one row for the regular resource semaphore and another row for the small-query resource semaphore. There are two requirements for a small-query semaphore:  
  
-   The memory grant requested should be less than 5 MB  
  
-   The query cost should be less than 3 cost units  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name `sys.dm_pdw_nodes_exec_query_resource_semaphores`. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**resource_semaphore_id**|**smallint**|Nonunique ID of the resource semaphore. 0 for the regular resource semaphore and 1 for the small-query resource semaphore.|  
|**target_memory_kb**|**bigint**|Grant usage target in kilobytes.|  
|**max_target_memory_kb**|**bigint**|Maximum potential target in kilobytes. NULL for the small-query resource semaphore.|  
|**total_memory_kb**|**bigint**|Memory held by the resource semaphore in kilobytes. If the system is under memory pressure or if forced minimum memory is granted frequently, this value can be larger than the **target_memory_kb** or **max_target_memory_kb** values. Total memory is a sum of available and granted memory.|  
|**available_memory_kb**|**bigint**|Memory available for a new grant in kilobytes.|  
|**granted_memory_kb**|**bigint**|Total granted memory in kilobytes.|  
|**used_memory_kb**|**bigint**|Physically used part of granted memory in kilobytes.|  
|**grantee_count**|**int**|Number of active queries that have their grants satisfied.|  
|**waiter_count**|**int**|Number of queries waiting for grants to be satisfied.|  
|**timeout_error_count**|**bigint**|Total number of time-out errors since server startup. NULL for the small-query resource semaphore.|  
|**forced_grant_count**|**bigint**|Total number of forced minimum-memory grants since server startup. NULL for the small-query resource semaphore.|  
|**pool_id**|**int**|ID of the resource pool to which this resource semaphore belongs.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## Remarks  
 Queries that use dynamic management views that include ORDER BY or aggregates might increase memory consumption and thus contribute to the problem they are troubleshooting.  
  
 Use **sys.dm_exec_query_resource_semaphores** for troubleshooting but do not include it in applications that will use future versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The Resource Governor feature enables a database administrator to distribute server resources among resource pools, up to a maximum of 64 pools. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and higher, each pool behaves like a small independent server instance and requires 2 semaphores.  
  
## See Also  
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_exec_query_memory_grants &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)  
  
