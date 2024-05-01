---
title: "sys.dm_os_host_info (Transact-SQL)"
description: The sys.dm_os_host_info DMV returns one row that displays operating system version information.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/20/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords:
  - "sys.dm_os_host_info"
  - "sys.dm_os_host_info_TSQL"
  - "dm_os_host_info"
  - "dm_os_host_info_TSQL"
helpviewer_keywords:
  - "sys.dm_os_host_info dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.dm_os_host_info (Transact-SQL)

[!INCLUDE [SQL Server 2017](../../includes/applies-to-version/sqlserver2017.md)]

Returns one row that displays operating system version information.

| Column name | Data type | Description |
| --- | --- | --- |
| `host_platform` | **nvarchar(256)** | The type of operating system. Can be `Windows` or `Linux`. |
| `host_distribution` | **nvarchar(256)** | Description of the operating system. |
| `host_release` | **nvarchar(256)** | [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows operating system release (version number). For a list of values and descriptions, see [Operating system version (Windows)](/windows/win32/sysinfo/operating-system-version).<br /><br />On Linux, this column returns an empty string. |
| `host_service_pack_level` | **nvarchar(256)** | Service pack level of the Windows operating system.<br /><br />On Linux, this column returns an empty string. |
| `host_sku` | **int** | Windows Stock Keeping Unit (SKU) ID. For a list of SKU IDs and descriptions, see [GetProductInfo function](/windows/win32/api/sysinfoapi/nf-sysinfoapi-getproductinfo). Is nullable.<br /><br />On Linux, this column returns `NULL`. |
| `os_language_version` | **int** | Windows locale identifier (LCID) of the operating system. For a list of LCID values and descriptions, see [Locale IDs Assigned by Microsoft](/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c). Can't be `NULL`. |

## Remarks

This view is similar to [sys.dm_os_windows_info](sys-dm-os-windows-info-transact-sql.md), adding columns to differentiate Windows and Linux.

## Security

### Permissions

On [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, the `SELECT` permission on `sys.dm_os_host_info` is granted to the public role by default. If revoked, you require `VIEW SERVER STATE` permission on the server.

On [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, you require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Examples

The following example returns all columns from the `sys.dm_os_host_info` view.

```sql
SELECT host_platform,
    host_distribution,
    host_release,
    host_service_pack_level,
    host_sku,
    os_language_version
FROM sys.dm_os_host_info;
```

Here is a sample result set on Windows Server 2019 Standard:

| host_platform | host_distribution | host_release | host_service_pack_level | host_sku | os_language_version |
| --- | --- | --- | --- | --- | --- |
| Windows | Windows Server 2019 Standard | 10.0 | | 7 | 1033 |

Here is a sample result set on Ubuntu Linux 22.04:

| host_platform | host_distribution | host_release | host_service_pack_level | host_sku | os_language_version |
| --- | --- | --- | --- | --- | --- |
| Linux | Ubuntu | 22.04 | | `NULL` | 1033 |

## Related content

- [sys.dm_os_sys_info (Transact-SQL)](sys-dm-os-sys-info-transact-sql.md)
- [sys.dm_os_windows_info (Transact-SQL)](sys-dm-os-windows-info-transact-sql.md)
