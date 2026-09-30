---
title: Dynamic Management Objects
titleSuffix: Azure Synapse Analytics
description: Azure Synapse Analytics dynamic management objects.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/14/2026
ms.service: sql
ms.subservice: data-warehouse
ms.topic: reference
dev_langs:
  - TSQL
monikerRange: "=azure-sqldw-latest"
---
# Azure Synapse Analytics dynamic management objects

[!INCLUDE [asa-md](../../includes/applies-to-version/asa.md)]

This article lists the [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] dynamic management objects.

All [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] dynamic management objects begin with `sys.dm_pdw`.

> [!NOTE]  
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

| Dynamic management object | Description |
| --- | --- |
| [sys.dm_pdw_dms_cores](sys-dm-pdw-dms-cores-transact-sql.md) | Returns all DMS services running on the Compute nodes of the appliance, one row per service instance. |
| [sys.dm_pdw_dms_external_work](sys-dm-pdw-dms-external-work-transact-sql.md) | Returns all Data Movement Service (DMS) steps for external operations. |
| [sys.dm_pdw_dms_workers](sys-dm-pdw-dms-workers-transact-sql.md) | Returns all workers completing DMS steps. |
| [sys.dm_pdw_errors](sys-dm-pdw-errors-transact-sql.md) | Returns all errors encountered during execution of a request or query. |
| [sys.dm_pdw_exec_connections](sys-dm-pdw-exec-connections-transact-sql.md) | Returns the connections established to this Azure Synapse Analytics instance, and the details of each connection. |
| [sys.dm_pdw_exec_requests](sys-dm-pdw-exec-requests-transact-sql.md) | Returns all requests currently or recently active, one row per request or query. |
| [sys.dm_pdw_exec_sessions](sys-dm-pdw-exec-sessions-transact-sql.md) | Returns all sessions currently or recently open, one row per session. |
| [sys.dm_pdw_hadoop_operations](sys-dm-pdw-hadoop-operations-transact-sql.md) | Returns one row for each map-reduce job pushed down to Hadoop when running a query on an external Hadoop table. |
| [sys.dm_pdw_lock_waits](sys-dm-pdw-lock-waits-transact-sql.md) | Returns requests that are waiting for locks. |
| [sys.dm_pdw_nodes_database_encryption_keys](sys-dm-pdw-nodes-database-encryption-keys-transact-sql.md) | Returns the encryption state of a database and its database encryption keys, for each node. |
| [sys.dm_pdw_nodes_exec_query_plan](sys-dm-pdw-nodes-exec-query-plan-transact-sql.md) | Returns the Showplan in XML format for the batch specified by the plan handle. |
| [sys.dm_pdw_nodes_exec_query_profiles](sys-dm-pdw-nodes-exec-query-profiles-transact-sql.md) | Monitors real time data warehouse query progress while the query executes. |
| [sys.dm_pdw_nodes_exec_query_statistics_xml](sys-dm-pdw-nodes-exec-query-statistics-xml-transact-sql.md) | Returns the query execution plan for in-flight requests, with showplan XML and transient statistics. |
| [sys.dm_pdw_nodes_exec_sql_text](sys-dm-pdw-nodes-exec-sql-text-transact-sql.md) | Returns the text of the SQL batch identified by the specified `sql_handle`. |
| [sys.dm_pdw_nodes_exec_text_query_plan](sys-dm-pdw-nodes-exec-text-query-plan-transact-sql.md) | Returns the Showplan in text format for a Transact-SQL batch or for a specific statement within the batch. |
| [sys.dm_pdw_nodes](sys-dm-pdw-nodes-transact-sql.md) | Returns all nodes in the appliance, one row per node. |
| [sys.dm_pdw_os_threads](sys-dm-pdw-os-threads-transact-sql.md) | Returns the operating system threads running on the nodes. |
| [sys.dm_pdw_request_steps](sys-dm-pdw-request-steps-transact-sql.md) | Returns all steps that compose a given request or query, one row per query step. |
| [sys.dm_pdw_resource_waits](sys-dm-pdw-resource-waits-transact-sql.md) | Returns wait information for all resource types. |
| [sys.dm_pdw_sql_requests](sys-dm-pdw-sql-requests-transact-sql.md) | Returns all SQL Server query distributions that are part of a SQL step in the query. |
| [sys.dm_pdw_sys_info](sys-dm-pdw-sys-info-transact-sql.md) | Returns appliance-level counters that reflect overall activity on the appliance. |
| [sys.dm_pdw_wait_stats](sys-dm-pdw-wait-stats-transact-sql.md) | Returns the SQL Server OS state for instances running on the different nodes. |
| [sys.dm_pdw_waits](sys-dm-pdw-waits-transact-sql.md) | Returns all wait states encountered during execution of a request or query, including locks and waits on transmission queues. |
| [sys.dm_workload_management_workload_groups_stats](sys-dm-workload-management-workload-group-stats-transact-sql.md) (Preview) | Returns workload group statistics and the effective values of the workload group. |

## Related content

- [System dynamic management views and functions](system-dynamic-management-objects.md)
