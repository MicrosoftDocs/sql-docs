---
title: "sys.dm_os_enumerate_fixed_drives (Transact-SQL)"
description: "sys.dm_os_enumerate_fixed_drives enumerates volumes mounted to drive letters."
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/11/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_enumerate_fixed_drives"
  - "sys.dm_os_enumerate_fixed_drives_TSQL"
helpviewer_keywords:
  - "sys.dm_os_enumerate_fixed_drives dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_os_enumerate_fixed_drives (Transact-SQL)

Starting with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 1, `sys.dm_os_enumerate_fixed_drives` enumerates volumes mounted to drive letters like `C:\`.

| Column name | Data type | Description |
| --- | --- | --- |
| `fixed_drive_path` | **nvarchar(512)** | Path to the volume, like `C:\`. |
| `drive_type` <sup>1</sup> | **int** | Code for drive type. |
| `drive_type_desc` <sup>1</sup> | **nvarchar(512)** | Description of drive type. |
| `free_space_in_bytes` | **bigint** | Disk free space in bytes. |

<sup>1</sup> For more information, see the [GetDriveTypeW function](/windows/win32/api/fileapi/nf-fileapi-getdrivetypew).

## Permissions

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, requires VIEW SERVER STATE permission on the server.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires VIEW SERVER PERFORMANCE STATE permission on the server.

## See also

- [Dynamic Management Views and Functions (Transact-SQL)](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [I/O Related Dynamic Management Views and Functions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/i-o-related-dynamic-management-views-and-functions-transact-sql.md)
