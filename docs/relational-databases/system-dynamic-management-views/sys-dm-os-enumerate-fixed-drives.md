---
title: "sys.dm_os_enumerate_fixed_drives (Transact-SQL)"
description: sys.dm_os_enumerate_fixed_drives (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/27/2023"
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

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Introduced in SQL Server 2019.

Enumerates volumes mounted to drive letters like `C:\`.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|  
|`fixed_drive_path`|`nvarchar(512)`|Path to the volume, like `C:\`.|  
|`drive_type`|`int`|Code for drive type. See [`GetDriveTypeW` function](/windows/win32/api/fileapi/nf-fileapi-getdrivetypew).|
|`drive_type_desc`|`nvarchar(512)`|Description of drive type. See [`GetDriveTypeW` function](/windows/win32/api/fileapi/nf-fileapi-getdrivetypew).|
|`free_space_in_bytes`|`bigint`|Disk free space in bytes.|

## Permissions

The user must have `VIEW SERVER STATE` permission on the server.

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## See also  

 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [I/O Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/i-o-related-dynamic-management-views-and-functions-transact-sql.md)  
