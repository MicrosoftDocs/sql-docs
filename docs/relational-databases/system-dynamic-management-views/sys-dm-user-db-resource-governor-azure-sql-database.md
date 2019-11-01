---
title: "sys.dm_user_db_resource_governance (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2019"
ms.prod: sql
ms.technology: system-objects
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
monikerRange: "=azuresqldb-current||=sqlallproducts-allversions"
---
# sys.dm_user_db_resource_governance (Transact-SQL)

[!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md](../../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]

The `sys.dm_user_db_resource_governance` view returns configuration and capacity settings used by resource governance mechanisms in the current database or elastic pool.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|int|ID of the database, unique within an Azure SQL Database server.|
|**logical_database_guid**|uniqueidentifier|Logical GUID for user database that stays through the life of a user database.  Renaming the database or changing its service level objective will not change this value.|
|**physical_database_guid**|uniqueidentifier|Physical GUID for a user database which stays through the life of the physical instance of the user database. Changing the database service level objective will cause this value to change.|
|**server_name**|nvarchar|Logical server name.|
|**database_name**|nvarchar|Logical database name.|
|**slo_name**|nvarchar|Service level objective, including hardware generation.|
|**dtu_limit**|int|DTU limit of database (NULL for vCore).|
|**cpu_limit**|int|vCore limit of database (NULL for DTU databases).|
|**min_cpu**|tinyint|The MIN_CPU_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor-resource-pool?#resource-pool-concepts).|
|**max_cpu**|tinyint|The MAX_CPU_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor-resource-pool?#resource-pool-concepts).|
|**cap_cpu**|tinyint|The CAP_CPU_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor-resource-pool?#resource-pool-concepts).|
|**min_cores**|smallint|Internal use only.|
|**max_dop**|smallint|The MAX_DOP value for the user workload group. See [CREATE WORKLOAD GROUP](https://docs.microsoft.com/sql/t-sql/statements/create-workload-group-transact-sql).|
|**min_memory**|int|The MIN_MEMORY_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor-resource-pool?#resource-pool-concepts).|
|**max_memory**|int|The MAX_MEMORY_PERCENT value of the user workload resource pool. See [Resource Pool Concepts](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor-resource-pool?#resource-pool-concepts).|
|**max_sessions**|int|The maximum number of sessions that can be classified into the user workload group.|
|**max_memory_grant**|int|The REQUEST_MAX_MEMORY_GRANT_PERCENT value for the user workload group. See [CREATE WORKLOAD GROUP](https://docs.microsoft.com/sql/t-sql/statements/create-workload-group-transact-sql).|
|**max_db_memory**|int|Internal use only.|
|**govern_background_io**|bit|Internal use only.|
|**min_db_max_size_in_mb**|bigint|The minimum max_size value for a data file, in MB. See [sys.database_files](https://docs.microsoft.com/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql).|
|**max_db_max_size_in_mb**|bigint|The maximum max_size value for a data file, in MB. See [sys.database_files](https://docs.microsoft.com/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql).|
|**default_db_max_size_in_mb**|bigint|The default max_size value for a data file, in MB. See [sys.database_files](https://docs.microsoft.com/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql).|
|**db_file_growth_in_mb**|bigint|Default growth increment for a data file, in MB. See [sys.database_files](https://docs.microsoft.com/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql).|
|**initial_db_file_size_in_mb**|bigint|Default size for new data file, in MB. See [sys.database_files](https://docs.microsoft.com/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql).|
|**log_size_in_mb**|bigint|Default size for new log file, in MB. See [sys.database_files](https://docs.microsoft.com/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql).|
|**instance_cap_cpu**|int|Internal use only.|
|**instance_max_log_rate**|bigint|Log generation rate limit for the SQL Server instance, in bytes per second. Applies to all log generated by the instance, including `tempdb` and other system databases.|
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
|||

## Permissions

This view requires VIEW DATABASE STATE permission.

## Remarks

Azure SQL Database uses a resource governance implementation that is based on SQL Server [Resource Governor](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor), modified and extended to run a SQL Server database service in Azure. On each SQL Server instance in the service, there are multiple [resource pools](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor-resource-pool) and [workload groups](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor-workload-group), with resource limits set at both pool and group levels to provide a [balanced Database-as-a-Service](https://azure.microsoft.com/blog/resource-governance-in-azure-sql-database/). User workload and internal workloads are classified into separate resource pools and workload groups. User workload on the primary and readable secondary replicas, including geo-replicas, is classified into the `SloSharedPool1` resource pool and `UserPrimaryGroup.DBId[N]` workload group, where `N` stands for the database ID value. In addition, there are multiple resource pools and workload groups for various internal workloads.

In addition to using Resource Governor to govern resources within the SQL Server process, Azure SQL Database also uses Windows [Job Objects](https://docs.microsoft.com/windows/win32/procthread/job-objects) and Windows [File Server Resource Manager (FSRM)](https://docs.microsoft.com/windows-server/storage/fsrm/fsrm-overview) to implement resource governance at the operating system level.

Azure SQL Database resource governance is hierarchical in nature. From top to bottom, limits are enforced at the SQL Server instance level using the operating system resource governance mechanisms and Resource Governor, at the resource pool level using Resource Governor, and at the workload group level using Resource Governor.

For example, if a query could generate 1000 IOPS without any IO resource governance, but the workload group maximum IOPS limit is set to 900 IOPS, the query will not be able to generate more than 900 IOPS. However, if the resource pool maximum IOPS limit is set to 1500 IOPS, and the total IO from all workload groups associated with the resource pool exceeds 1500 IOPS, then the IO of the same query may be reduced below the workgroup limit of 900 IOPS. Similar considerations apply to other resource types such as CPU, workers, and log rate.

For disk IO governance, workload group and resource pool limits apply to IO against user databases. The total IO generated by the SQL Server instance, including IO against `tempdb`, other system databases, etc. can exceed workload group and resource pool limits, but will not exceed instance limits.

> [!IMPORTANT]
> Most of the data returned by this DMV is intended for internal consumption and is subject to change at any time.

## Examples

The following example returns maximum log rate for the primary user workload group for a single database, or for all databases in an elastic pool.

```
SELECT database_name,
       primary_group_id,
       primary_max_log_rate
FROM sys.dm_user_db_resource_governance
ORDER BY database_name;  
```

## See Also

- [Resource Governor](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor)
- [sys.dm_resource_governor_resource_pools (Transact-SQL)](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql)
- [sys.dm_resource_governor_workload_groups (Transact-SQL)](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql)
- [sys.dm_resource_governor_resource_pools_history_ex (Transact-SQL)](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-history-ex-azure-sql-database)
- [sys.dm_resource_governor_workload_groups_history_ex (Azure SQL Database)](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-history-ex-azure-sql-database)
- [Transaction log rate governance](https://docs.microsoft.com/azure/sql-database/sql-database-resource-limits-database-server#transaction-log-rate-governance)
- [Single database DTU resource limits](https://docs.microsoft.com/azure/sql-database/sql-database-dtu-resource-limits-single-databases)
- [Single database vCore resource limits](https://docs.microsoft.com/azure/sql-database/sql-database-vcore-resource-limits-single-databases)
