---
title: "Always On availability groups dynamic management views and functions"
description: Always On availability groups dynamic management views and functions for monitoring availability groups, replicas, and databases.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/24/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ai-usage: ai-assisted
helpviewer_keywords:
  - "dynamic management views [SQL Server], AlwaysOn Availability Groups"
  - "dynamic management views [SQL Server], Always On Availability Groups"
dev_langs:
  - "TSQL"
---
# Always On availability groups dynamic management views and functions

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section contains the dynamic management views (DMVs) and functions that are related to [!INCLUDE[ssHADR](../../includes/sshadr-md.md)].

## Availability group monitoring

These DMVs provide information about availability groups and their state.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_hadr_availability_group_states](sys-dm-hadr-availability-group-states-transact-sql.md) | Returns a row for each availability group that owns an availability replica on the local instance. Shows synchronization health and role state. |
| [sys.dm_hadr_ag_threads](sys-dm-hadr-ag-threads-transact-sql.md) | Returns a row for each thread associated with an availability group. Useful for diagnosing thread-level issues. |
| [sys.dm_hadr_name_id_map](sys-dm-hadr-name-id-map-transact-sql.md) | Maps availability group names to their unique identifiers (GUIDs). |

## Availability replica monitoring

These DMVs provide state and health information for availability replicas.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_hadr_availability_replica_states](sys-dm-hadr-availability-replica-states-transact-sql.md) | Returns a row for each replica showing synchronization state, role, and operational health. |
| [sys.dm_hadr_availability_replica_cluster_nodes](sys-dm-hadr-availability-replica-cluster-nodes-transact-sql.md) | Returns a row for each availability replica mapped to its cluster node name. |
| [sys.dm_hadr_availability_replica_cluster_states](sys-dm-hadr-availability-replica-cluster-states-transact-sql.md) | Returns cluster state information for each availability replica regardless of where it's hosted. |

## Database replica monitoring

These DMVs track synchronization and state for databases within availability groups.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_hadr_database_replica_states](sys-dm-hadr-database-replica-states-transact-sql.md) | Returns a row for each database participating in an availability group, showing synchronization state and log progress. |
| [sys.dm_hadr_database_replica_cluster_states](sys-dm-hadr-database-replica-cluster-states-transact-sql.md) | Returns cluster-level state information for each database in an availability group. |
| [sys.dm_hadr_db_threads](sys-dm-hadr-db-threads-transact-sql.md) | Returns thread information for databases participating in availability groups. |

## Cluster monitoring

These DMVs provide information about the Windows Server Failover Clustering (WSFC) cluster.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_hadr_cluster](sys-dm-hadr-cluster-transact-sql.md) | Returns one row with the cluster name and quorum information. |
| [sys.dm_hadr_cluster_members](sys-dm-hadr-cluster-members-transact-sql.md) | Returns a row for each cluster member and its state. |
| [sys.dm_hadr_cluster_networks](sys-dm-hadr-cluster-networks-transact-sql.md) | Returns a row for each cluster network and its subnet configuration. |
| [sys.dm_hadr_instance_node_map](sys-dm-hadr-instance-node-map-transact-sql.md) | Maps SQL Server instances to their hosting WSFC cluster nodes. |

## Automatic seeding

These DMVs track automatic seeding operations for availability group databases.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_hadr_automatic_seeding](sys-dm-hadr-automatic-seeding.md) | Returns status information for automatic seeding operations on the primary replica. |
| [sys.dm_hadr_physical_seeding_stats](sys-dm-hadr-physical-seeding-stats.md) | Returns detailed statistics about physical seeding operations including progress and throughput. |

## Automatic page repair

These DMVs track automatic page repair operations that fix corrupt pages using data from replicas.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_hadr_auto_page_repair](sys-dm-hadr-auto-page-repair-transact-sql.md) | Returns a row for each automatic page repair attempt on any availability database. |

## Listener monitoring

These DMVs provide information about availability group listeners.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_tcp_listener_states](sys-dm-tcp-listener-states-transact-sql.md) | Returns state information for each TCP listener including availability group listeners. |

## Related content

- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Always On Availability Groups Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)
- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)