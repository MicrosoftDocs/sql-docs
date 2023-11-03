---
title: "sys.dm_hadr_availability_replica_cluster_nodes (Transact-SQL)"
description: Returns a row for every availability replica of the availability groups in the WSFC cluster.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/17/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_hadr_availability_replica_cluster_nodes"
  - "sys.dm_hadr_availability_replica_cluster_nodes_TSQL"
  - "dm_hadr_availability_replica_cluster_nodes_TSQL"
  - "sys.dm_hadr_availability_replica_cluster_nodes"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.dm_hadr_availability_replica_cluster_nodes dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_hadr_availability_replica_cluster_nodes (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for every availability replica (regardless of join state) of the Always On availability groups in the Windows Server Failover Clustering (WSFC) cluster.

## <a id="connected_state"></a>

| Column name | Data type | Description |
| --- | --- | --- |
| `group_name` | **nvarchar(256)** | Name of the availability group. |
| `replica_server_name` | **nvarchar(256)** | Name of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] hosting the replica. |
| `node_name` | **nvarchar(256)** | Name of the cluster node. |

## Remarks

[!INCLUDE [dmv-cluster-column-display](../includes/dmv-cluster-column-display.md)]

## Permissions

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, requires VIEW SERVER STATE permission on the server.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Related content

- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
