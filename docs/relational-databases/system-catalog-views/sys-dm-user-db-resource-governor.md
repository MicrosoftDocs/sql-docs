---
title: "sys.dm_user_db_resource_governance (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/27/2019"
ms.prod: 
ms.prod_service: sql-database
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.resource_governance"
  - "sys.resource_governance_TSQL"
  - "resource_governance"
  - "resource_governance_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.resource_governance catalog view"
ms.assetid: 
author: joesackmsft
ms.author: josack
manager: craigg
monikerRange: "=azuresqldb-current||=sqlallproducts-allversions"
---
# sys.dm_user_db_resource_governance (Transact-SQL)
[!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md](../../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]

Returns resource governance configuration and capacity settings for an Azure SQL Database database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|int|ID of the database, unique within a Azure SQL Database server.|
|**logical_database_guid**|uniqueidentifier|Logical guid for user database and stays through the life of a user database.  Rename or setting a database to a different SLO will not change the GUID. |
|physical_database_guid|uniqueidentifier|Physical guid for a user database which stays through the life of the physical instance of the user database. Setting to a different SLO will cause this column to change.|
|server_name|nvarchar|Logical server name.|
|database_name|nvarchar|Logical database name.|
|slo_name|nvarchar|Service level objective and hardware generation.|
|dtu_limit|int|DTU limit of database (NULL for vCore).|
|cpu_limit|int|vCore limit of database (NULL for DTU databases).|
|min_cpu|tinyint|Minimum CPU percent that can be used by user workload.|
|max_cpu|tinyint|Maximum CPU percent that can be used by user workload.|
|cap_cpu|tinyint|Cap of CPU percent for user workload groups.|
|min_cores|smallint|Number of CPUs used by SQL.|
|max_dop|smallint|Maximum degree of parallelism used by user workload.|
|min_memory|int|Minimum memory percent that can be used by user workload.|
|max_memory|int|Maximum memory percent that can be used by user workload.|
|max_sessions|int|Session limit for user group.|
|max_memory_grant|int|Maximum memory grant for each query in user workload, in percent.|
|max_db_memory|int|Max buffer pool memory cap for the user DB workload|
|govern_background_io|bit|Indicates whether background writes are charged to user group.|
|min_db_max_size_in_mb|bigint|Minimum Max database file size, in MB.|
|max_db_max_size_in_mb|bigint|Maximum Max database file, in MB.|
|default_db_max_size_in_mb|bigint|Default Max database file size, in MB.|
|db_file_growth_in_mb|bigint|Default growth of azure database file, in MB.|
|initial_db_file_size_in_mb|bigint|Default database file size, in MB.|
|log_size_in_mb|bigint|Default log file size, in MB.|
|instance_cap_cpu|int|CPU cap at instance level.|
|instance_max_log_rate|bigint|Log generation rate cap at instance level, in bytes per second.|
|instance_max_worker_threads|int|Worker thread limit at instance level.|
|replica_type|int|Replica type, where 0 is primary, and 1 is secondary.|
|max_transaction_size|bigint|Max log space used by any transaction, in KB.|
|checkpoint_rate_mbps|int|Checkpoint bandwidth, in Mbps.|
|checkpoint_rate_io|int|Checkpoint IO rate in IOs per second.|
|last_updated_date_utc|datetime|Date and time of the last setting change or reconfiguration.|
|primary_group_id|int|Primary user workload group id.|
|primary_group_max_workers|int|Worker limit at primary user workload group level.|
|primary_min_log_rate|bigint|Minimum log rate (bytes per sec) at primary user workload group level.|
|primary_max_log_rate|bigint|Maximum log rate (bytes per sec) at primary user workload group level.|
|primary_group_min_io|int|Minimum IO at primary user workload group level.|
|primary_group_max_io|int|Maximum IO at primary user workload group level.|
|primary_group_min_cpu|float|Minimum CPU percent limit at primary user workload group level.|
|primary_group_max_cpu|float|Maximum CPU percent limit at primary user workload group level.|
|primary_log_commit_fee|int|Log rate governance commit fee at primary user workload group level.|
|primary_pool_max_workers|int|Worker limit at primary user pool level.
|pool_max_io|int|Maximum IO limit at primary user pool level.|
|govern_db_memory_in_resource_pool|bit|Indicated whether max size of buffer pool is governed at the resource pool level. Usually set for databases within elastic pools.|
|volume_local_iops|int|IOs per second cap for local volume (e.g. C:, D:).|
|volume_managed_xstore_iops|int|IOs per second cap for remote storage account.|
|volume_external_xstore_iops|int|IOs per second cap for remote storage account used by Azure SQL DB backups and telemetry.|
|volume_type_local_iops|int|IOs per second cap for all local volumes.|
|volume_type_managed_xstore_iops|int|IOs per second cap for all remote storage accounts used by the instance.|
|volume_type_external_xstore_iops|int|IOs per second cap for all remote storage accounts used by Azure SQL DB backups and telemetry for the instance.|
|volume_pfs_iops|int|IOs per second cap for premium file storage.|
|volume_type_pfs_iops|int|IOs per second cap for all premium file storage used by the instance.|

## Permissions

This view requires VIEW DATABASE STATE permission.

## Remarks

Users can access this dynamic management view for resource governance configuration and capacity settings for an Azure SQL Database database. Most of the data surfaced by this DMV is intended for internal consumption and is subject to change.

## Examples

The following example returns instance maximum log rate data ordered by database name within the database server for a single or pooled database.
  
```
SELECT database_name,
       primary_max_log_rate
FROM sys.dm_user_db_resource_governance
ORDER BY database_name DESC;  
```
  
## See Also


  
  
