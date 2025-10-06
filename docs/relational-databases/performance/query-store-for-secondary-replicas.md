---
title: Query Store for Secondary Replicas
description: Query Store can be configured to monitor and tuning workloads on secondary read-only replicas.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
ms.custom:
  - build-2025
helpviewer_keywords:
  - "Query Store secondary replicas"
monikerRange: ">=sql-server-ver16"
---
# Query Store for readable secondaries

[!INCLUDE [sqlserver2025-asdb](../../includes/applies-to-version/sqlserver2025-asdb.md)]

Query Store for readable secondaries enables Query Store insights for workloads that run on secondary replicas. When enabled, secondary replicas stream query execution information (such as runtime and wait statistics) to the primary replica, where the data is persisted in Query Store and made visible across all replicas.

The feature was originally introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], however it was off by default and required a trace flag to enable. This was due in part because the feature was and remains to be in a preview state for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

Beginning with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], Query Store for readable secondaries is enabled by default.

> [!IMPORTANT]  
> In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], Query Store for readable secondaries is a preview feature and requires Trace Flag 12606 to be applied to the primary and all readable secondary replicas. It **isn't** intended for production deployments that are based on [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. For more information, see [SQL Server 2022 release notes](../../sql-server/sql-server-2022-release-notes.md).
>
> For [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], the feature is **on** by default and Trace Flag 12606 **isn't** required. Enabling this trace flag has the effect of disabling the feature.

## Enable Query Store for readable secondaries

Before you use Query Store for readable secondaries on a [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instance, an [Always On availability group](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) must be configured.

For Azure SQL Database, Query Store for readable secondaries supports the following service tiers:

  - General purpose with [active geo-replication](/azure/azure-sql/database/active-geo-replication-overview) (no built-in high availability replicas; requires geo-replication configuration for secondary support)
  - Premium (includes built-in high availability replicas; active geo-replication also supported)
  - Business critical (includes built-in high availability replicas; active geo-replication also supported)
<!--
  - Hyperscale with at least one [high Availability replica](/azure/azure-sql/database/service-tier-hyperscale-replicas#high-availability-replica) and/or [named replica](/azure/azure-sql/database/service-tier-hyperscale-replicas#named-replica) (requires explicit replica configuration; active geo-replication also supported)
  - Azure SQL Managed Instance
    - General purpose with a failover group [failover group](../../../azure-sql/managed-instance/failover-group-sql-mi.md)
    - Business critical (includes built-in high availability replicas)
-->

>[NOTE]
>[!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] existing and newly created databases are automatically enrolled and enabled to support the Query Store for readable secondaries feature on supported service tiers.

**Applies to**: [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)] and later versions.

If Query Store isn't already enabled and in `READ_WRITE` mode on the primary replica, you must enable it before proceeding. Execute the following script for each desired database on the primary replica:

```sql
ALTER DATABASE [Database_Name]
    SET QUERY_STORE = ON(OPERATION_MODE = READ_WRITE);
```

To enable the Query Store on all readable secondaries, connect to the primary replica and execute the following script for each database that is to be enlisted to use the feature.

```sql
ALTER DATABASE [Database_Name]
    FOR SECONDARY
    SET QUERY_STORE = ON
    (OPERATION_MODE = READ_WRITE);
GO
```

### Enable automatic plan correction for secondary replicas
**Applies to**: [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

After enabling Query Store for secondary replicas, you can optionally enable automatic tuning to allow the automatic plan correction feature to force plans on secondary replicas. This enables the query optimizer to automatically identify and fix query performance issues caused by execution plan regressions on secondary replicas. 

To enable automatic plan correction for secondary replicas, connect to the primary replica and execute the following script for each desired database:

```sql
ALTER DATABASE [Database_Name]
FOR SECONDARY
SET AUTOMATIC_TUNING (FORCE_LAST_GOOD_PLAN = ON);
GO
```

This setting works in conjunction with Query Store for readable secondaries and allows the system to automatically revert to the last known good execution plan when performance regressions are detected on secondary replicas.

## Disable Query Store for secondary replicas

To disable the Query Store for secondary replicas feature on all secondary replicas, connect to the `master` database on the `primary` replica and execute the following script for each desired database:

```sql
ALTER DATABASE [Database_Name]
    FOR SECONDARY
    SET QUERY_STORE = ON
    (OPERATION_MODE = READ_ONLY);
```

### Validate Query Store is enabled on secondary replicas

You can validate that Query Store is enabled on a `secondary` replica by connecting to the database on the secondary replica and execute the following t-sql statement:

```sql
SELECT desired_state_desc,
       actual_state_desc,
       readonly_reason
FROM sys.database_query_store_options;
GO
```

The results from querying the [sys.database_query_store_options](../system-catalog-views/sys-database-query-store-options-transact-sql.md) catalog view should indicate that the Query Store's actual state is `READ_CAPTURE_SECONDARY` with a `readonly_reason` of `8`.

| `desired_state_desc` | `actual_state_desc` | `readonly_reason` |
| --- | --- | --- |
| `READ_CAPTURE_SECONDARY` | `READ_CAPTURE_SECONDARY` | `8` |

<!-- Once enabled, you can use [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md) to verify the health of the Query Store on the secondary replica.
-->

## Remarks

### Terminology

A **replica set** is defined as a database's read/write replica (primary) and one or more read-only replicas (secondary) treated as a logical unit. A **role** in this context refers to the role of a specific replica. When a replica is serving in the primary role, it's the read/write replica that can perform both data modifications and read activity. When a replica is configured to only perform read-only activity, it's serving in a secondary role (secondary, geo secondary, geo ha secondary). Roles can change via a planned or unplanned failover events, when this happens, a primary can become a secondary or vice versa.

The currently supported roles are:

- Primary
- Secondary
- Geo secondary
- Geo HA secondary
- Named Replica

### How it works

The data stored about queries can be analyzed as workloads on a role basis. Query Store for readable secondaries give you the ability to monitor the performance of any unique, read-only workload that might be executing against secondary replicas. The data is aggregated at the role level. For example, a SQL Server [distributed availability groups](../../database-engine/availability-groups/windows/distributed-availability-groups.md) configuration might consist of:

- One primary replica, part of Availability Group 1 (AG1)

- Two local secondary replicas, also part AG1

- One remote primary replica in another location that is part of a separate availability group (AG2). In SQL Server terms, it would also be commonly referred to as a global forwarder, however, the Query Store for readable secondaries feature will recognize and refer to it as a `Geo secondary` replica, assuming that it's a geographically distributed secondary replica.

If AG1 and AG2 have been configured to allow read-only connections when a read-only workload executes against either of AG1's secondary replicas, the Query Store execution statistics are sent to AG1's primary replica and aggregated and persisted as data that was generated from the `secondary` role before that data is sent back to all of the secondary replicas including the global forwarder in AG2. When a separate workload is executed against AG2's primary, the global fowarder, its data is sent back to the primary replica of AG1 and persisted as data that was generated from `Geo secondary` role.

From an observability perspective, the [sys.query_store_runtime_stats](../system-catalog-views/sys-query-store-runtime-stats-transact-sql.md) system catalog view is extended to help identify the role where the execution statistics originated from. There's a relationship between this view and the [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md) system catalog view, which can provide a more friendly name of the role. In SQL Server, Azure SQL Database, and Azure SQL Managed Instance, the replica_name column is `NULL`. However, the replica_name column is populated for the Hyperscale service tier if there's a named replica present and is being used for read-only workloads.

An example of a t-sql query that could be used to provide an overall analysis of the top 50 queries over the last 8 hours, which consumed CPU resources from all replicas would be:

```sql
-- Top 50 queries by CPU across all replicas in the last 8 hours
DECLARE @hours AS INT = 8;

SELECT TOP 50 qsq.query_id,
              qsp.plan_id,
              CASE qrs.replica_group_id WHEN 1 THEN 'PRIMARY' WHEN 2 THEN 'SECONDARY' WHEN 3 THEN 'GEO SECONDARY' WHEN 4 THEN 'GEO HA SECONDARY' ELSE CONCAT('NAMED REPLICA_', qrs.replica_group_id) END AS replica_type,
              qsq.query_hash,
              qsp.query_plan_hash,
              SUM(qrs.count_executions) AS sum_executions,
              SUM(qrs.count_executions * qrs.avg_logical_io_reads) AS total_logical_reads,
              SUM(qrs.count_executions * qrs.avg_cpu_time / 1000.0) AS total_cpu_ms,
              AVG(qrs.avg_logical_io_reads) AS avg_logical_io_reads,
              AVG(qrs.avg_cpu_time / 1000.0) AS avg_cpu_ms,
              ROUND(TRY_CAST (SUM(qrs.avg_duration * qrs.count_executions) AS FLOAT) / NULLIF (SUM(qrs.count_executions), 0) * 0.001, 2) AS avg_duration_ms,
              COUNT(DISTINCT qsp.plan_id) AS number_of_distinct_plans,
              qsqt.query_sql_text
FROM sys.query_store_runtime_stats_interval AS qsrsi
     INNER JOIN sys.query_store_runtime_stats AS qrs
         ON qrs.runtime_stats_interval_id = qsrsi.runtime_stats_interval_id
     INNER JOIN sys.query_store_plan AS qsp
         ON qsp.plan_id = qrs.plan_id
     INNER JOIN sys.query_store_query AS qsq
         ON qsq.query_id = qsp.query_id
     INNER JOIN sys.query_store_query_text AS qsqt
         ON qsq.query_text_id = qsqt.query_text_id
WHERE qsrsi.start_time >= DATEADD(HOUR, -@hours, GETUTCDATE())
GROUP BY qsq.query_id, qsq.query_hash, qsp.query_plan_hash, qsp.plan_id, qrs.replica_group_id, qsqt.query_sql_text
ORDER BY SUM(qrs.count_executions * qrs.avg_cpu_time / 1000.0) DESC, AVG(qrs.avg_cpu_time / 1000.0) DESC;
```

The Query Store reports in [SQL Server Management Studio (SSMS) 21](/ssms/release-notes-21#whats-new-in-2100) and later versions provide a **Replica** dropdown list, which provides a way to view Query Store data across various replica sets/roles. Also, inside of the Object explorer view, the Query Store node reflects the current state of Query Store (that is, READ_CAPTURE) if connected to a readable secondary replica.

### Performance considerations for Query Store for readable secondaries

The channel used by secondary replicas to send query information back to the primary replica is the same channel used to keep secondary replicas up to date. What does `channel` mean here?

In an availability group (HADR) configuration, replicas synchronize with each other using a dedicated transport layer that carries log blocks, acknowledgments, and status messages between the primary and secondary replicas. This ensures data consistency and failover readiness.

When Query Store for readable secondaries is enabled, it doesn't create a separate network endpoint. Instead, it establishes a new logical communication path over the existing transport layer:

- For Azure SQL Database (non-Hyperscale), Azure SQL Managed Instance, and SQL Server, this uses the high availability and disaster recovery (HADR) Always On transport layer.

- For Azure SQL Database Hyperscale, the RbIo (Remote Blob I/O) transport layer is used, which is the communications channel between the compute nodes and the Log Service/Page Servers. RbIo provides a reliable, encrypted channel for moving log records and data pages.

This path multiplexes Query Store execution data (query text, plans, runtime/wait stats) alongside the normal log record traffic, using the same encrypted session. The feature has its own capture and receive queues, which can be viewed by querying the `sys.database_query_store_internal_state` view from any replica's perspective:

```sql
SELECT pending_message_count,
       messaging_memory_used_mb
FROM sys.database_query_store_internal_state;
```

Data from secondaries is persisted in the same Query Store tables on the primary, which can increase storage requirements. Under heavy load, you might observe latency or backpressure on the transport channel. The same ad hoc query capture limitations that apply to Query Store on the primary also apply to secondaries. For more information and guidance on managing Query Store size and capture policies, see [Keep the most relevant data in Query Store](best-practice-with-the-query-store.md#keep-the-most-relevant-data-in-query-store).

### Negative query ID/plan ID visibility

Negative IDs indicate temporary in-memory placeholders for queries/plans on secondaries before persistence to the primary.

Before Query Store data is persisted to the primary from readable secondary replicas, queries and plans might be assigned temporary identifiers within the local in-memory representation of Query Store - the [MEMORYCLERK_QUERYDISKSTORE_HASHMAP](how-query-store-collects-data.md#remarks). The query and plan IDs can appear as negative numbers and are placeholders until the primary replica assigns an authoritative identifier, which occurs after Query Store determines that a query meets the configured [capture mode requirements](manage-the-query-store.md#set-the-optimal-query-store-capture-mode). If a [custom capture policy](manage-the-query-store.md#custom-capture-policies) is in place, you can review the requirements that must be met by querying the `sys.database_query_store_options` system catalog view.

```sql
SELECT query_capture_mode_desc,
       capture_policy_execution_count,
       capture_policy_total_compile_cpu_time_ms,
       capture_policy_total_execution_cpu_time_ms
FROM sys.database_query_store_options;
```

Once a query is designated as captured, its runtime/wait statistics and plan can be persisted, and the local temporary IDs are replaced with positive IDs. This also allows you to use plan forcing or hinting capabilities.

## Related content

- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md)
- [sys.query_store_plan_forcing_locations (Transact-SQL)](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md)
- [sys.sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [Query Store hints](query-store-hints.md)
- [Query Store Usage Scenarios](query-store-usage-scenarios.md)
- [sys.database_query_store_options (Transact-SQL)](../system-catalog-views/sys-database-query-store-options-transact-sql.md)
- [Best practices for monitoring workloads with Query Store](best-practice-with-the-query-store.md)
- [Best practices for managing the Query Store](manage-the-query-store.md)
- [Tune performance with the Query Store](tune-performance-with-the-query-store.md)
