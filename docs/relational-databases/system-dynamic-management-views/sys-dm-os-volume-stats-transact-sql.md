---
title: "sys.dm_os_volume_stats (Transact-SQL)"
description: sys.dm_os_volume_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/03/2020"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_volume_stats_TSQL"
  - "dm_os_volume_stats"
  - "sys.dm_os_volume_stats"
  - "sys.dm_os_volume_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_os_volume_stats dynamic management function"
dev_langs:
  - "TSQL"
ms.assetid: fa1c58ad-8487-42ad-956c-983f2229025f
---
# sys.dm_os_volume_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-2008R2SP1-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

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
  
|**Column**|**Data type**|**Description**|  
|-|-|-|  
|**database_id**|**int**|ID of the database. Cannot be null.|  
|**file_id**|**int**|ID of the file. Cannot be null.|  
|**volume_mount_point**|**nvarchar(512)**|Mount point at which the volume is rooted. Can return an empty string. Returns null on Linux operating system.|  
|**volume_id**|**nvarchar(512)**|Operating system volume ID. Can return an empty string. Returns null on Linux operating system.|  
|**logical_volume_name**|**nvarchar(512)**|Logical volume name. Can return an empty string. Returns null on Linux operating system.|  
|**file_system_type**|**nvarchar(512)**|Type of file system volume (for example, NTFS, FAT, RAW). Can return an empty string. Returns null on Linux operating system.|  
|**total_bytes**|**bigint**|Total size in bytes of the volume. Cannot be null.|  
|**available_bytes**|**bigint**|Available free space on the volume. Cannot be null.|  
|**supports_compression**|**tinyint**|Indicates if the volume supports operating system compression. Cannot be null on Windows and returns null on Linux operating system.|  
|**supports_alternate_streams**|**tinyint**|Indicates if the volume supports alternate streams. Cannot be null on Windows and returns null on Linux operating system.|  
|**supports_sparse_files**|**tinyint**|Indicates if the volume supports sparse files.  Cannot be null on Windows and returns null on Linux operating system.|  
|**is_read_only**|**tinyint**|Indicates if the volume is currently marked as read only. Cannot be null.|  
|**is_compressed**|**tinyint**|Indicates if this volume is currently compressed. Cannot be null on Windows and returns null on Linux operating system.|  
|**incurs_seek_penalty**|**tinyint**|Indicates the type of storage supporting this volume. Possible values are:<br /><br />0: No seek penalty on this volume, typically when the storage device is PMM or SSD<br /><br />1: Seek penalty on this volume, typically when the storage device is HDD<br /><br />2: The storage type can't be determined when the volume is on a UNC path or mounted shares<br /><br />NULL: The storage type can't be determined on Linux operating system<br /><br />**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)])|  
  
## Security  
  
### Permissions  
 Requires `VIEW SERVER STATE` permission.  
  
## Examples  
  
### A. Return total space and available space for all database files  
 The following example returns the total space and available space (in bytes) for all database files in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```sql  
SELECT f.database_id, f.file_id, volume_mount_point, total_bytes, available_bytes  
FROM sys.master_files AS f  
CROSS APPLY sys.dm_os_volume_stats(f.database_id, f.file_id);  
```  
  
### B. Return total space and available space for the current database  
 The following example returns the total space and available space (in bytes) for the database files in the current database.  
  
```sql  
SELECT database_id, f.file_id, volume_mount_point, total_bytes, available_bytes  
FROM sys.database_files AS f  
CROSS APPLY sys.dm_os_volume_stats(DB_ID(f.name), f.file_id);  
```  
  
## See Also  
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)  
  
  
