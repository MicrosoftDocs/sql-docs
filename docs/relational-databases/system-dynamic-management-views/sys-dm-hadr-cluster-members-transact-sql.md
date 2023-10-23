---
title: "sys.dm_hadr_cluster_members (Transact-SQL)"
description: Returns a row for each of the members that constitute the WSFC quorum, and the state of each of them.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/17/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_hadr_cluster_members_TSQL"
  - "sys.dm_hadr_cluster_members"
  - "dm_hadr_cluster_members_TSQL"
  - "dm_hadr_cluster_members"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.dm_hadr_cluster_members catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017"
---
# sys.dm_hadr_cluster_members (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

If the WSFC node that hosts a local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that is enabled for [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] has WSFC quorum, returns a row for each of the members that constitute the quorum and the state of each of them. This includes of all nodes in the cluster (returned with `CLUSTER_ENUM_NODE` type by the `Clusterenum` function) and the disk or file-share witness, if any. The row returned for a given member contains information about the state of that member. For example, for a five node cluster with majority node quorum in which one node is down, when `sys.dm_hadr_cluster_members` is queried from a server instance that is enabled for [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] that resides on a node with quorum, `sys.dm_hadr_cluster_members` reflects the state of the down node as `NODE_DOWN`.

If the WSFC node has no quorum, no rows are returned.

Use this dynamic management view to answer the following questions:

- What nodes are currently running on the WSFC cluster?

- How many more failures can the WSFC cluster tolerate before losing quorum in a majority-node case?

> [!TIP]  
> Beginning in [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], this dynamic management view supports Always On failover cluster instances (FCIs) in addition to availability groups (AGs).

| Column name | Data type | Description |
| --- | --- | --- |
| `member_name` | **nvarchar(128)** | Member name, which can be a computer name, a drive letter, or a file share path. |
| `member_type` | **tinyint** | The type of member, one of:<br /><br />0 = WSFC node<br /><br />1 = Disk witness<br />2 = File share witness<br />3 = Cloud Witness |
| `member_type_desc` | **nvarchar(50)** | Description of `member_type`, one of:<br /><br />CLUSTER_NODE<br />DISK_WITNESS<br />FILE_SHARE_WITNESS<br />CLOUD_WITNESS |
| `member_state` | **tinyint** | The member state, one of:<br /><br />0 = Offline<br />1 = Online |
| `member_state_desc` | **nvarchar(60)** | Description of `member_state`, one of:<br /><br />UP<br />DOWN |
| `number_of_quorum_votes` | **tinyint** | Number of quorum votes possessed by this quorum member. For No Majority: Disk Only quorums, this value defaults to 0. For other quorum types, this value defaults to 1. |

## Remarks

[!INCLUDE [dmv-cluster-column-display](../includes/dmv-cluster-column-display.md)]

## Permissions

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, requires VIEW SERVER STATE permission on the server.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Related content

- [Always On Availability Groups Dynamic Management Views - Functions](always-on-availability-groups-dynamic-management-views-functions.md)
- [Always On Availability Groups Catalog Views (Transact-SQL)](../system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)
- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
