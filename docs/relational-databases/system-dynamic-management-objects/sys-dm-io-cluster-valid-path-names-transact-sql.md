---
title: "sys.dm_io_cluster_valid_path_names (Transact-SQL)"
description: sys.dm_io_cluster_valid_path_names returns information on all valid shared disks, including clustered shared volumes, for a SQL Server failover cluster instance.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_io_cluster_valid_path_names"
  - "dm_io_cluster_valid_path_names_TSQL"
  - "sys.dm_io_cluster_valid_path_names_TSQL"
  - "dm_io_cluster_valid_path_names"
helpviewer_keywords:
  - "dm_io_cluster_valid_path_names"
  - "sys.dm_io_cluster_valid_path_names"
  - "cluster valid path names"
  - "csv name"
  - "cluster shared volume names"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# sys.dm_io_cluster_valid_path_names (Transact-SQL)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Returns information on all valid shared disks, including clustered shared volumes, for a SQL Server failover cluster instance. If the instance isn't clustered, an empty rowset is returned.

| Column name | Data type | Description |
| --- | --- | --- |
| `path_name` | **nvarchar(512)** | Volume mount point or drive path that can be used as a root directory for database and log files. Not nullable. |
| `cluster_owner_node` | **nvarchar(64)** | Current owner of the drive. For cluster shared volumes (CSV), the owner is the node which is hosting the MetaData Server. Not nullable. |
| `is_cluster_shared_volume` | **bit** | Returns `1` if the drive on which this path is located is a cluster shared volume; otherwise, returns `0`. |

## Remarks

A SQL Server failover cluster instance (FCI) must use shared storage between all nodes of the FCI for data and log file storage. The disks listed in this view are the disks that are in the cluster resource group associated with the instance. They're the only disks that can be used for data or log file storage.

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Examples

The following example uses `sys.dm_io_cluster_valid_path_names` to determine the shared drives on a clustered server instance:

```sql
SELECT *
FROM sys.dm_io_cluster_valid_path_names;
```

## Related content

- [sys.dm_os_cluster_nodes (Transact-SQL)](sys-dm-os-cluster-nodes-transact-sql.md)
- [sys.dm_io_cluster_shared_drives (Transact-SQL)](sys-dm-io-cluster-shared-drives-transact-sql.md)
- [System dynamic management views](system-dynamic-management-objects.md)
