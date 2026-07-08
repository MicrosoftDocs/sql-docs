---
title: "sys.dm_os_windows_info (Transact-SQL)"
description: The sys.dm_os_windows_info DMV returns one row that displays Windows operating system version information.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/20/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_windows_info"
  - "dm_os_windows_info_TSQL"
  - "sys.dm_os_windows_info"
  - "sys.dm_os_windows_info_TSQL"
helpviewer_keywords:
  - "sys.dm_os_windows_info dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_os_windows_info (Transact-SQL)

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

Returns one row that displays Windows operating system version information.

Only applies to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] running on Windows. To see similar information for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] running on a non-Windows host, such as Linux, use [sys.dm_os_host_info (Transact-SQL)](sys-dm-os-host-info-transact-sql.md). Behavior on a non-Windows host is undefined.

| Column name | Data type | Description |
| --- | --- | --- |
| `windows_release` | **nvarchar(256)** | For Windows, returns the release number. For a list of values and descriptions, see [Operating system version (Windows)](/windows/win32/sysinfo/operating-system-version). Can't be `NULL`. |
| `windows_service_pack_level` | **nvarchar(256)** | For Windows, returns the service pack number. Can't be `NULL`. |
| `windows_sku` | **int** | For Windows, returns the Windows Stock Keeping Unit (SKU) ID. For a list of SKU IDs and descriptions, see [GetProductInfo function](/windows/win32/api/sysinfoapi/nf-sysinfoapi-getproductinfo). Can be `NULL`. |
| `os_language_version` | **int** | For Windows, returns the Windows locale identifier (LCID) of the operating system. For a list of LCID values and descriptions, see [Locale IDs assigned by Microsoft](/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c). Can't be `NULL`. |

## Permissions

On [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, the SELECT permission on `sys.dm_os_windows_info` is granted to the public role by default. If revoked, you require VIEW SERVER STATE permission on the server.

On [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, you require VIEW SERVER PERFORMANCE STATE permission on the server.

## Limitations

To see information for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] running on a non-Windows host, such as Linux, use [sys.dm_os_host_info (Transact-SQL)](sys-dm-os-host-info-transact-sql.md). Behavior on a non-Windows host is undefined.

## Examples

The following example returns all columns from the `sys.dm_os_windows_info` view, on Windows Server 2019 Standard:

```sql
SELECT windows_release,
    windows_service_pack_level,
    windows_sku,
    os_language_version
FROM sys.dm_os_windows_info;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| windows_release | windows_service_pack_level | windows_sku | os_language_version |
| --- | --- | --- | --- |
| 10.0 | | 7 | 1033 |

## Related content

- [sys.dm_os_sys_info (Transact-SQL)](sys-dm-os-sys-info-transact-sql.md)
- [sys.dm_os_host_info](sys-dm-os-host-info-transact-sql.md)
