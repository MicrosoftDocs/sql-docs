---
title: "sys.dm_linux_proc_sql_maps (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sys.dm_linux_proc_sql_maps"
  - "sys.dm_linux_proc_sql_maps_TSQL"
  - "dm_linux_proc_sql_maps"
  - "dm_linux_proc_sql_maps_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_linux_proc_sql_maps dynamic management view"
ms.assetid: 0d0e134a-3a7a-4c68-995b-26b47753f47b
caps.latest.revision: 6
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
ROBOTS: NOINDEX
---
# sys.dm_linux_proc_sql_maps (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssLinx-xxxx-xxxx-xxx](../../includes/tsql-appliesto-sslinx-xxxx-xxxx-xxx.md)]

This dmv is based off of the linux `/proc/self/maps` file. It provides information about the mapped memory regions of [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> Implementation of this DMV has been delayed. Expect it, or something similar in a future CTP.

|Column name |Data type |Description |  
|------------- |------------- |---------------- |  
|**address** |**nvarchar(256)** |The address space in the process that the mapping occupies. |
|**perms** |**nvarchar(256)** |Set of permissions: <br>r: read <br>w: write <br>x: execute <br>s: shared <br>p: private (copy on write) |
|**offset** |**nvarchar(256)** |The offset into the file. |
|**dev** |**nvarchar(256)** |The device (major:minor). |
|**inode** |**bigint** |The inode on that device. 0 indicates that no inode is associated with the memory region, as would be the case with BSS (uninitialized data). |
|**pathname** |**nvarchar(256)** |The file that is backing the mapping. |

## Remarks

Returns an empty row set when called on a Windows computer.

## Permissions 

Requires `VIEW SERVER STATE` permission.

## Examples  

The following queries return information about specific paths:       
```
SELECT * FROM sys.dm_linux_proc_sql_maps WHERE pathname LIKE '%stack%';
SELECT * FROM sys.dm_linux_proc_sql_maps WHERE pathname LIKE '%mssql%';
SELECT * FROM sys.dm_linux_proc_sql_maps WHERE pathname LIKE '%/usr/%';
```

## See Also  

[Linux Process Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/linux-process-dynamic-management-views-transact-sql.md)   

