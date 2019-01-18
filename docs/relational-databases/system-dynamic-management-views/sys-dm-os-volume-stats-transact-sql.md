---
title: "sys.dm_os_volume_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/02/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_volume_stats_TSQL"
  - "dm_os_volume_stats"
  - "sys.dm_os_volume_stats"
  - "sys.dm_os_volume_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_volume_stats dynamic management function"
ms.assetid: fa1c58ad-8487-42ad-956c-983f2229025f
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_os_volume_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about the operating system volume (directory) on which the specified databases and files are stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use this dynamic management function to check the attributes of the physical disk drive or return available free space information about the directory.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sys.dm_os_volume_stats (database_id, file_id)  
```  
  
##  <a name="Arguments"></a> Arguments  
 *database_id*  
 ID of the database. *database_id* is **int**, with no default. Cannot be NULL.  
  
 *file_id*  
 ID of the file. *file_id* is **int**, with no default. Cannot be NULL.  
  
## Table Returned  
  
||||  
|-|-|-|  
|**Column**|**Data type**|**Description**|  
|**database_id**|**int**|ID of the database. Cannot be null.|  
|**file_id**|**int**|ID of the file. Cannot be null.|  
|**volume_mount_point**|**nvarchar(512)**|Mount point at which the volume is rooted. Can return an empty string.|  
|**volume_id**|**nvarchar(512)**|Operating system volume ID. Can return an empty string|  
|**logical_volume_name**|**nvarchar(512)**|Logical volume name. Can return an empty string|  
|**file_system_type**|**nvarchar(512)**|Type of file system volume (for example, NTFS, FAT, RAW). Can return an empty string|  
|**total_bytes**|**bigint**|Total size in bytes of the volume. Cannot be null.|  
|**available_bytes**|**bigint**|Available free space on the volume. Cannot be null.|  
|**supports_compression**|**bit**|Indicates if the volume supports operating system compression. Cannot be null.|  
|**supports_alternate_streams**|**bit**|Indicates if the volume supports alternate streams. Cannot be null.|  
|**supports_sparse_files**|**bit**|Indicates if the volume supports sparse files.  Cannot be null.|  
|**is_read_only**|**bit**|Indicates if the volume is currently marked as read only. Cannot be null.|  
|**is_compressed**|**bit**|Indicates if this volume is currently compressed. Cannot be null.|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission.  
  
## Examples  
  
### A. Return total space and available space for all database files  
 The following example returns the total space and available space (in bytes) for all database files in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
SELECT f.database_id, f.file_id, volume_mount_point, total_bytes, available_bytes  
FROM sys.master_files AS f  
CROSS APPLY sys.dm_os_volume_stats(f.database_id, f.file_id);  
```  
  
### B. Return total space and available space for the current database  
 The following example returns the total space and available space (in bytes) for the database files in the current database.  
  
```  
SELECT database_id, f.file_id, volume_mount_point, total_bytes, available_bytes  
FROM sys.database_files AS f  
CROSS APPLY sys.dm_os_volume_stats(DB_ID(f.name), f.file_id);  
```  
  
## See Also  
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)  
  
  
