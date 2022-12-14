---
title: "sys.dm_fts_memory_buffers (Transact-SQL)"
description: sys.dm_fts_memory_buffers returns information about memory buffers belonging to a specific memory pool that are used as part of a full-text crawl or a full-text crawl range.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_fts_memory_buffers"
  - "dm_fts_memory_buffers_TSQL"
  - "dm_fts_memory_buffers"
  - "sys.dm_fts_memory_buffers_TSQL"
helpviewer_keywords:
  - "sys.dm_fts_memory_buffers dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_fts_memory_buffers (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns information about memory buffers belonging to a specific memory pool that are used as part of a full-text crawl or a full-text crawl range.  
  
> [!NOTE]
> The following column will be removed in a future release of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: `row_count`. Avoid using this column in new development work, and plan to modify applications that currently use it.  

  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|**pool_id**|**int**|ID of the allocated memory pool.<br /><br /> 0 = Small buffers<br /><br /> 1 = Large buffers|  
|**memory_address**|**varbinary(8)**|Address of the allocated memory buffer.|  
|**name**|**nvarchar(4000)**|Name of the shared memory buffer for which this allocation was made.|  
|**is_free**|**bit**|Current state of memory buffer.<br /><br /> 0 = Free<br /><br /> 1 = Busy|  
|**row_count**|**int**|Number of rows that this buffer is currently handling.|  
|**bytes_used**|**int**|Amount, in bytes, of memory in use in this buffer.|  
|**percent_used**|**int**|Percentage of allocated memory used.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## Physical joins 
  
:::image type="content" source="../../relational-databases/system-dynamic-management-views/media/join-dm-fts-memory-buffers-1.svg" alt-text="Diagram of physical joins for sys.dm_fts_memory_buffers.":::
  
## Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|`dm_fts_memory_buffers.pool_id`|`dm_fts_memory_pools.pool_id`|Many-to-one|  
  
## Next steps
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)  
  
