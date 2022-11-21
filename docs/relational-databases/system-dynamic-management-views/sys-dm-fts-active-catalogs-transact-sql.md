---
title: "sys.dm_fts_active_catalogs (Transact-SQL)"
description: sys.dm_fts_active_catalogs returns information on the full-text catalogs that have some population activity in progress on the server.
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_fts_active_catalogs_TSQL"
  - "dm_fts_active_catalogs"
  - "dm_fts_active_catalogs_TSQL"
  - "sys.dm_fts_active_catalogs"
helpviewer_keywords:
  - "sys.dm_fts_active_catalogs dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_fts_active_catalogs (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns information on the full-text catalogs that have some population activity in progress on the server.  
  
> [!NOTE]
>  The following columns will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: is_paused, previous_status, previous_status_description, row_count_in_thousands, status, status_description, and worker_count. Avoid using these columns in new development work, and plan to modify applications that currently use any of them.  
  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|ID of the database that contains the active full-text catalog.|  
|**catalog_id**|**int**|ID of the active full-text catalog.|  
|**memory_address**|**varbinary(8)**|Address of memory buffers allocated for the population activity related to this full-text catalog.|  
|**name**|**nvarchar(128)**|Name of the active full-text catalog.|  
|**is_paused**|**bit**|Indicates whether the population of the active full-text catalog has been paused.|  
|**status**|**int**|Current state of the full-text catalog. One of the following:<br /><br /> 0 = Initializing<br /><br /> 1 = Ready<br /><br /> 2 = Paused<br /><br /> 3 = Temporary error<br /><br /> 4 = Remount needed<br /><br /> 5 = Shutdown<br /><br /> 6 = Quiesced for backup<br /><br /> 7 = Backup is done through catalog<br /><br /> 8 = Catalog is corrupt|  
|**status_description**|**nvarchar(120)**|Description of current state of the active full-text catalog.|  
|**previous_status**|**int**|Previous state of the full-text catalog. One of the following:<br /><br /> 0 = Initializing<br /><br /> 1 = Ready<br /><br /> 2 = Paused<br /><br /> 3 = Temporary error<br /><br /> 4 = Remount needed<br /><br /> 5 = Shutdown<br /><br /> 6 = Quiesced for backup<br /><br /> 7 = Backup is done through catalog<br /><br /> 8 = Catalog is corrupt|  
|**previous_status_description**|**nvarchar(120)**|Description of previous state of the active full-text catalog.|  
|**worker_count**|**int**|Number of threads currently working on this full-text catalog.|  
|**active_fts_index_count**|**int**|Number of full-text indexes that are being populated.|  
|**auto_population_count**|**int**|Number of tables with an auto population in progress for this full-text catalog.|  
|**manual_population_count**|**int**|Number of tables with manual population in progress for this full-text catalog.|  
|**full_incremental_population_count**|**int**|Number of tables with a full or incremental population in progress for this full-text catalog.|  
|**row_count_in_thousands**|**int**|Estimated number of rows (in thousands) in all full-text indexes in this full-text catalog.|  
|**is_importing**|**bit**|Indicates whether the full-text catalog is being imported:<br /><br /> 1 = The catalog is being imported.<br /><br /> 2 = The catalog is not being imported.|  
  
## Remarks  
 The `is_importing` column was introduced in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
   
## Physical joins  
 
:::image type="content" source="../../relational-databases/system-dynamic-management-views/media/join-dm-fts-active-catalogs-1.svg" alt-text="Diagram of physical joins for sys.dm_fts_active_catalogs.":::
  
## Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|`dm_fts_active_catalogs.database_id`|`dm_fts_index_population.database_id`|One-to-one|  
|`dm_fts_active_catalogs.catalog_id`|`dm_fts_index_population.catalog_id`|One-to-one|  
  
## Examples  
 The following example returns information about the active full-text catalogs on the current database.  
  
```sql  
SELECT catalog.name, catalog.is_importing, catalog.auto_population_count,  
  OBJECT_NAME(population.table_id) AS table_name,  
  population.population_type_description, population.is_clustered_index_scan,  
  population.status_description, population.completion_type_description,  
  population.queued_population_type_description, population.start_time,  
  population.range_count   
FROM sys.dm_fts_active_catalogs catalog   
CROSS JOIN sys.dm_fts_index_population population   
WHERE catalog.database_id = population.database_id   
AND catalog.catalog_id = population.catalog_id   
AND catalog.database_id = (SELECT dbid FROM sys.sysdatabases WHERE name = DB_NAME());  
GO  
```  
  
## Next steps
 
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)  
  
