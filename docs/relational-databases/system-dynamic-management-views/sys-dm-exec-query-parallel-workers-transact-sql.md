---
title: "sys.dm_exec_query_parallel_workers (Transact-SQL)"
description: sys.dm_exec_query_parallel_workers (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/24/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_exec_query_parallel_workers_TSQL"
  - "dm_exec_query_parallel_workers"
  - "sys.dm_exec_query_parallel_workers_TSQL"
  - "sys.dm_exec_query_parallel_workers"
helpviewer_keywords:
  - "sys.dm_exec_query_parallel_workers dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 1d72cef1-22d8-4ae0-91db-6694fe918c9f
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_query_parallel_workers (Transact-SQL)
[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Returns worker availability information per node.  
  
|Name|Data type|Description|  
|----------|---------------|-----------------|  
|**node_id**|**int**|NUMA node ID.|  
|**scheduler_count**|**int**|Number of schedulers on this node.|  
|**max_worker_count**|**int**|Maximum number of workers for parallel queries.|  
|**reserved_worker_count**|**int**|Number of workers reserved by parallel queries, plus number of main workers used by all requests.| 
|**free_worker_count**|**int**|Number of workers available for tasks.<br /><br />**Note:** every incoming request consumes at least 1 worker, which is subtracted from the free worker count.  It is possible that the free worker count can be a negative number on a heavily loaded server.| 
|**used_worker_count**|**int**|Number of workers used by parallel queries.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
 
## Examples  
  
### A. Viewing current parallel worker availability  

```sql 
SELECT * FROM sys.dm_exec_query_parallel_workers;  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_os_workers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-workers-transact-sql.md)
