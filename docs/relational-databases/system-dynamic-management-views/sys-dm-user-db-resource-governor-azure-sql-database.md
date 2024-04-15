---
title: "sys.dm_user_db_resource_governance (Transact-SQL)"
description: sys.dm_user_db_resource_governance (Transact-SQL)
author: MikeRayMSFT
ms.author: mikeray
ms.date: 02/26/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_user_db_resource_governance"
  - "sys.dm_user_db_resource_governance_TSQL"
  - "dm_user_db_resource_governance"
  - "dm_user_db_resource_governance_TSQL"
helpviewer_keywords:
  - "sys.dm_user_db_resource_governance catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.dm_user_db_resource_governance (Transact-SQL)

[!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md](../../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]

Returns the actual configuration and capacity settings used by resource governance mechanisms in the current database or elastic pool.

For single databases, returns a single row for the current database. For elastic pools, returns a row for each database where the caller holds the `VIEW DATABASE STATE` or `VIEW DATABASE PERFORMANCE STATE` permission, or rows for all databases in the elastic pool if the caller holds the `VIEW SERVER STATE` or `VIEW SERVER PERFORMANCE STATE` permission.

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|int|ID of the database, unique within a database or within an elastic pool, but not within a logical server. For details, see [DB_ID](../../t-sql/functions/db-id-transact-sql.md#remarks).|
|**logical_database_guid**|uniqueidentifier|A unique identifier for a user database that remains unchanged through the life of a user database. Renaming the database or changing its service level objective will not change this value.|
|**physical_database_guid**|uniqueidentifier|A unique identifier for the current physical database corresponding to the user database. Changing the database service level objective will cause this value to change.|
|**server_name**|nvarchar|Logical server name.|
|**database_name**|nvarchar|User database name.|
|**slo_name**|nvarchar|Service level objective, including hardware generation.|
|**dtu_limit**|int|DTU limit of database (NULL for vCore).|
|**cpu_limit**|int|vCore limit of database (NULL for DTU databases).|
|**min_cpu**|tinyint|The MIN_CPU_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](../resource-governor/resource-governor-resource-pool.md#resource-pool-concepts).|
|**max_cpu**|tinyint|The MAX_CPU_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](../resource-governor/resource-governor-resource-pool.md#resource-pool-concepts).|
|**cap_cpu**|tinyint|The CAP_CPU_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](../resource-governor/resource-governor-resource-pool.md#resource-pool-concepts).|
|**min_cores**|smallint|Internal use only.|
|**max_dop**|smallint|The MAX_DOP value for the user workload group. See [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md).|
|**min_memory**|int|The MIN_MEMORY_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](../resource-governor/resource-governor-resource-pool.md#resource-pool-concepts).|
|**max_memory**|int|The MAX_MEMORY_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](../resource-governor/resource-governor-resource-pool.md#resource-pool-concepts).|
|**max_sessions**|int|The maximum number of sessions allowed in the user workload group.|
|**max_memory_grant**|int|The REQUEST_MAX_MEMORY_GRANT_PERCENT value for the user workload group. See [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md).|
|**max_db_memory**|int|Internal use only.|
|**govern_background_io**|bit|Internal use only.|
|**min_db_max_size_in_mb**|bigint|The minimum max_size value for a data file, in MB. See [sys.database_files](../system-catalog-views/sys-database-files-transact-sql.md).|
|**max_db_max_size_in_mb**|bigint|The maximum max_size value for a data file, in MB. See [sys.database_files](../system-catalog-views/sys-database-files-transact-sql.md).|
|**default_db_max_size_in_mb**|bigint|The default max_size value for a data file, in MB. See [sys.database_files](../system-catalog-views/sys-database-files-transact-sql.md).|
|**db_file_growth_in_mb**|bigint|Default growth increment for a data file, in MB. See [sys.database_files](../system-catalog-views/sys-database-files-transact-sql.md).|
|**initial_db_file_size_in_mb**|bigint|Default size for new data file, in MB. See [sys.database_files](../system-catalog-views/sys-database-files-transact-sql.md).|
|**log_size_in_mb**|bigint|Default size for new log file, in MB. See [sys.database_files](../system-catalog-views/sys-database-files-transact-sql.md).|
|**instance_cap_cpu**|int|Internal use only.|
|**instance_max_log_rate**|bigint|Log generation rate limit for the SQL Server instance, in bytes per second. Applies to all log generated by the instance, including `tempdb` and other system databases. In an elastic pool, applies to log generated by all databases in the pool.|
|**instance_max_worker_threads**|int|Worker thread limit for the SQL Server instance.|
|**replica_type**|int|Replica type, where 0 is Primary, and 1 is Secondary.|
|**max_transaction_size**|bigint|Max log space used by any transaction, in KB.|
|**checkpoint_rate_mbps**|int|Internal use only.|
|**checkpoint_rate_io**|int|Internal use only.|
|**last_updated_date_utc**|datetime|Date and time of the last setting change or reconfiguration, in UTC.|
|**primary_group_id**|int|Workload group ID for the user workload on primary replica and on secondary replicas.|
|**primary_group_max_workers**|int|Worker thread limit for the user workload group.|
|**primary_min_log_rate**|bigint|Minimum log rate in bytes per second at user workload group level. Resource governance will not attempt to reduce log rate below this value.|
|**primary_max_log_rate**|bigint|Maximum log rate in bytes per second at user workload group level. Resource governance will not allow log rate above this value.|
|**primary_group_min_io**|int|Minimum IOPS for the user workload group. Resource governance will not attempt to reduce IOPS below this value.|
|**primary_group_max_io**|int|Maximum IOPS for the user workload group. Resource governance will not allow IOPS above this value.|
|**primary_group_min_cpu**|float|Minimum CPU percent for the user workload group level. Resource governance will not attempt to reduce CPU utilization below this value.|
|**primary_group_max_cpu**|float|Maximum CPU percent for the user workload group level. Resource governance will not allow CPU utilization above this value.|
|**primary_log_commit_fee**|int|Log rate governance commit fee for the user workload group, in bytes. A commit fee increases the size of each log IO by a fixed value for the purposes of log rate accounting only. Actual log IO to storage is not increased.|
|**primary_pool_max_workers**|int|Worker thread limit for the user workload resource pool.|
|**pool_max_io**|int|Maximum IOPS limit for the user workload resource pool.|
|**govern_db_memory_in_resource_pool**|bit|Internal use only.|
|**volume_local_iops**|int|Internal use only.|
|**volume_managed_xstore_iops**|int|Internal use only.|
|**volume_external_xstore_iops**|int|Internal use only.|
|**volume_type_local_iops**|int|Internal use only.|
|**volume_type_managed_xstore_iops**|int|Internal use only.|
|**volume_type_external_xstore_iops**|int|Internal use only.|
|**volume_pfs_iops**|int|Internal use only.|
|**volume_type_pfs_iops**|int|Internal use only.|
|**user_data_directory_space_quota_mb**|int|Maximum local storage for the database engine instance. See [Storage space governance](/azure/azure-sql/database/resource-limits-logical-server#storage-space-governance)|
|**user_data_directory_space_usage_mb**|int|Current local storage consumption by data files, transaction log files, and `tempdb`` files. Updated every five minutes. |
|**bufferpool_extension_size_gb**|int| Internal use only.|
|**pool_max_log_rate**|bigint|Maximum log rate in bytes per second at the user resource pool level. Resource governance will not allow the total log rate across all workload groups in the resource pool to be above this value.|
|**primary_group_max_outbound_connection_workers**|int|Outbound connection worker thread limit for the primary user workload group.|
|**primary_pool_max_outbound_connection_workers**|int|Outbound connection worker thread limit for the user workload resource pool.|
|**replica_role**|tinyint|Represents the current replica role. </br></br> 0 - Primary</br>1 - High availability (HA) secondary</br>2 - Geo-replication forwarder</br>3 - Named replica</br></br>Reports 1 when connected with ReadOnly intent to any [readable secondary](/azure/azure-sql/database/read-scale-out). If connecting to a geo-secondary without specifying ReadOnly intent, reports 2 to reflect a connection to a geo-replication forwarder. If connecting to a named replica without specifying ReadOnly intent, reports 3.|

## Permissions

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.

## Remarks

For description of resource governance in Azure SQL Database, see [SQL Database resource limits](/azure/sql-database/sql-database-resource-limits-database-server).

> [!IMPORTANT]
> Most of the data returned by this DMV is intended for internal consumption and is subject to change at any time.

## Examples

The following query, executed in the context of a user database, returns maximum log rate and maximum IOPS at the user workload group and resource pool level. For a single database, one row is returned. For a database in an elastic pool, a row is returned for each database in the pool.

```sql
SELECT database_name,
       primary_group_id,
       primary_max_log_rate,
       primary_group_max_io,
       pool_max_io
FROM sys.dm_user_db_resource_governance
ORDER BY database_name;  
```

## Next steps

- [Resource Governor](../resource-governor/resource-governor.md)
- [sys.dm_resource_governor_resource_pools (Transact-SQL)](./sys-dm-resource-governor-resource-pools-transact-sql.md)
- [sys.dm_resource_governor_workload_groups (Transact-SQL)](./sys-dm-resource-governor-workload-groups-transact-sql.md)
- [sys.dm_resource_governor_resource_pools_history_ex (Transact-SQL)](./sys-dm-resource-governor-resource-pools-history-ex-azure-sql-database.md)
- [sys.dm_resource_governor_workload_groups_history_ex (Azure SQL Database)](./sys-dm-resource-governor-workload-groups-history-ex-azure-sql-database.md)
- [Transaction log rate governance](/azure/sql-database/sql-database-resource-limits-database-server#transaction-log-rate-governance)
- [Single database DTU resource limits](/azure/sql-database/sql-database-dtu-resource-limits-single-databases)
- [Single database vCore resource limits](/azure/sql-database/sql-database-vcore-resource-limits-single-databases)
- [Elastic pool vCore resource limits](/azure/azure-sql/database/resource-limits-vcore-elastic-pools)
