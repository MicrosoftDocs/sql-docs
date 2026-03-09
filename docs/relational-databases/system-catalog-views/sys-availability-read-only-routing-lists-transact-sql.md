---
title: "sys.availability_read_only_routing_lists (Transact-SQL)"
description: sys.availability_read_only_routing_lists returns a row for the read-only routing list of each availability replica.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/05/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "availability_read_only_routing_lists_TSQL"
  - "availability_read_only_routing_lists"
  - "sys.availability_read_only_routing_lists"
  - "sys.availability_read_only_routing_lists_TSQL"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "read-only routing"
  - "Availability Groups [SQL Server], readable secondary replicas"
  - "Availability Groups [SQL Server], read-only routing"
  - "readable secondary replicas"
  - "sys.availability_read_only_routing_lists dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.availability_read_only_routing_lists (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for the read-only routing list of each availability replica in an Always On availability group in the WSFC failover cluster.

| Column name | Data type | Description |
| --- | --- | --- |
| `replica_id` | **uniqueidentifier** | Unique ID of the availability replica that owns the routing list. |
| `routing_priority` | **int** | Priority order for routing (1 is first, 2 is second, and so forth). |
| `read_only_replica_id` | **uniqueidentifier** | Unique ID of the availability replica to which a read-only workload is routed. |  
  
## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Related content

- [Always On Availability Groups Dynamic Management Views and Functions (Transact-SQL)](../system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)
- [Always On Availability Groups Catalog Views (Transact-SQL)](always-on-availability-groups-catalog-views-transact-sql.md)
- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
