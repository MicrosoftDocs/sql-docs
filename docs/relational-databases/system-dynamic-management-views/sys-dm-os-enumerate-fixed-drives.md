---
description: "sys.dm_os_enumerate_fixed_drives (Transact-SQL)"
title: "sys.dm_os_enumerate_fixed_drives (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_os_enumerate_fixed_drives"
  - "sys.dm_os_enumerate_fixed_drives_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_enumerate_fixed_drives dynamic management view"
ms.assetid: 2e27489e-cf69-4a89-9036-77723ac3de66
author: markingmyname
ms.author: maghan
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

## See Also  

 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [I/O Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/i-o-related-dynamic-management-views-and-functions-transact-sql.md)  
