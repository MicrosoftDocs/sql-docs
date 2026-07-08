---
title: "sys.dm_os_ring_buffers (Transact-SQL)"
description: sys.dm_os_ring_buffers (Transact-SQL)
author: dimitri-furman
ms.author: dfurman
ms.reviewer: randolphwest
ms.date: 05/09/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_ring_buffers_TSQL"
  - "dm_os_ring_buffers_TSQL"
  - "sys.dm_os_ring_buffers"
  - "dm_os_ring_buffers"
helpviewer_keywords:
  - "sys.dm_os_ring_buffers dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
ms.custom:
  - build-2025
---

# sys.dm_os_ring_buffers (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Each row represents a record in a ring buffer of a specific type.

| Column name | Data type | Description |
| --- | --- | --- |
| `ring_buffer_address` | **varbinary(8)** | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] Not nullable. |
| `ring_buffer_type` | **nvarchar(60)** | The type of the ring buffer record. Not nullable. |
| `timestamp` | **bigint** | The time when a ring buffer record was added, in milliseconds since the computer started. Not nullable. |
| `record` | **nvarchar(max)** | Identified for informational purposes only. Not supported unless described in the official Microsoft product documentation, or used as directed by Microsoft for diagnostic and troubleshooting purposes. Future compatibility is not guaranteed. Nullable. |
| `ring_buffer_group` | **nvarchar(60)** | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] Not nullable.<br /><br />**Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] |
| `create_time` | **datetime2** | The time when a ring buffer record was added, in the local time of the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] instance. Not nullable.<br /><br />**Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] |

## Remarks

A ring buffer is a memory structure within the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] that is limited to a fixed number of records. As new records arrive, older records are removed.

Records in ring buffers contain diagnostic data for the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)]. Most ring buffer types are used for internal purposes and aren't supported, unless described in the official Microsoft product documentation. For example, you can [use ring buffers to obtain health information about Always On availability groups](../../database-engine/availability-groups/windows/always-on-ring-buffers.md).

The `sys.dm_os_ring_buffers` DMV can also be used as directed by Microsoft for diagnostic and troubleshooting purposes.

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions require the `VIEW SERVER STATE` permission.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], require the `VIEW SERVER PERFORMANCE STATE` permission on the server.

On [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerPerformanceStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE PERFORMANCE STATE` permission on the database, or membership in the `##MS_ServerPerformanceStateReader##` server role is required.

## Related content

- [SQL Server Operating System related dynamic management views (Transact-SQL)](sql-server-operating-system-related-dynamic-management-views-transact-sql.md)
