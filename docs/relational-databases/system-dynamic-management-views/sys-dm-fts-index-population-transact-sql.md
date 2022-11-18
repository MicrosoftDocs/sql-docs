---
title: "sys.dm_fts_index_population (Transact-SQL)"
description: sys.dm_fts_index_population returns information about the full-text index and semantic key phrase populations currently in progress in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_fts_index_population"
  - "dm_fts_index_population"
  - "sys.dm_fts_index_population_TSQL"
  - "dm_fts_index_population_TSQL"
helpviewer_keywords:
  - "sys.dm_fts_index_population dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_fts_index_population (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns information about the full-text index and semantic key phrase populations currently in progress in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|ID of the database that contains the full-text index being populated.|  
|**catalog_id**|**int**|ID of the full-text catalog that contains this full-text index.|  
|**table_id**|**int**|ID of the table for which the full-text index is being populated.|  
|**memory_address**|**varbinary(8)**|Memory address of the internal data structure that is used to represent an active population.|  
|**population_type**|**int**|Type of population. One of the following:<br /><br /> 1 = Full population<br /><br /> 2 = Incremental timestamp-based population<br /><br /> 3 = Manual update of tracked changes<br /><br /> 4 = Background update of tracked changes.|  
|**population_type_description**|**nvarchar(120)**|Description for type of population.|  
|**is_clustered_index_scan**|**bit**|Indicates whether the population involves a scan on the clustered index.|  
|**range_count**|**int**|Number of sub-ranges into which this population has been parallelized.|  
|**completed_range_count**|**int**|Number of ranges for which processing is complete.|  
|**outstanding_batch_count**|**int**|Current number of outstanding batches for this population. For more information, see [sys.dm_fts_outstanding_batches &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-outstanding-batches-transact-sql.md).|  
|**status**|**int**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Status of this Population. Note: some of the states are transient. One of the following:<br /><br /> 3 = Starting<br /><br /> 5 = Processing normally<br /><br /> 7 = Has stopped processing<br /><br /> For example, this status occurs when an auto merge is in progress.<br /><br /> 11 = Population aborted<br /><br /> 12 = Processing a semantic similarity extraction|  
|**status_description**|**nvarchar(120)**|Description of status of the population.|  
|**completion_type**|**int**|Status of how this population completed.|  
|**completion_type_description**|**nvarchar(120)**|Description of the completion type.|  
|**worker_count**|**int**|This value is always 0.|  
|**queued_population_type**|**int**|Type of the population, based on tracked changes, which will follow the current population, if any.|  
|**queued_population_type_description**|**nvarchar(120)**|Description of the population to follow, if any. For example, when CHANGE TRACKING = AUTO and the initial full population is in progress, this column would show "Auto population."|  
|**start_time**|**datetime**|Time that the population started.|  
|**incremental_timestamp**|**timestamp**|Represents the starting timestamp for a full population. For all other population types this value is the last committed checkpoint representing the progress of the populations.|  
  
## Remarks  
 When statistical semantic indexing is enabled in addition to full-text indexing, the semantic extraction and population of key phrases, and the extraction of document similarity data, occur simultaneously with full-text indexing. The population of the document similarity index occurs later in a second phase. For more information, see [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md).  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## Physical joins  

:::image type="content" source="../../relational-databases/system-dynamic-management-views/media/join-dm-fts-index-population-1.svg" alt-text="Diagram of physical joins for sys.dm_fts_index_population.":::
  
## Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|`dm_fts_active_catalogs.database_id`|`dm_fts_index_population.database_id`|One-to-one|  
|`dm_fts_active_catalogs.catalog_id`|`dm_fts_index_population.catalog_id`|One-to-one|  
|`dm_fts_population_ranges.parent_memory_address`|`dm_fts_index_population.memory_address`|Many-to-one|  
  
## Next steps
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)
