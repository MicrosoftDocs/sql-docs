---
title: "sys.database_files (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/19/2016"
ms.prod: ""
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.database_files"
  - "sys.database_files_TSQL"
  - "database_files"
  - "database_files_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.database_files catalog view"
ms.assetid: 0f5b0aac-c17d-4e99-b8f7-d04efc9edf44
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_files (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row per file of a database as stored in the database itself. This is a per-database view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**file_id**|**int**|ID of the file within database.|  
|**file_guid**|**uniqueidentifier**|GUID for the file.<br /><br /> NULL = Database was upgraded from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**type**|**tinyint**|File type:<br /><br /> 0 = Rows (Includes files of full-text catalogs that are upgraded to or created in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].)<br /><br /> 1 = Log<br /><br /> 2 = FILESTREAM<br /><br /> 3 = [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br /><br /> 4 = Full-text (Full-text catalogs earlier than [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]; full-text catalogs that are upgraded to or created in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] will report a file type 0.)|  
|**type_desc**|**nvarchar(60)**|Description of the file type:<br /><br /> ROWS (Includes files of full-text catalogs that are upgraded to or created in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].)<br /><br /> LOG<br /><br /> FILESTREAM<br /><br /> FULLTEXT (Full-text catalogs earlier than [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].)|  
|**data_space_id**|**int**|Value can be 0 or greater than 0. A value of 0 represents the database log file, and a value greater than 0 represents the ID of the filegroup where this data file is stored.|  
|**name**|**sysname**|Logical name of the file in the database.|  
|**physical_name**|**nvarchar(260)**|Operating-system file name. If the database is hosted by an AlwaysOn [readable secondary replica](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md), **physical_name** indicates the file location of the primary replica database. For the correct file location of a readable secondary database, query [sys.sysaltfiles](../../relational-databases/system-compatibility-views/sys-sysaltfiles-transact-sql.md).|  
|**state**|**tinyint**|File state:<br /><br /> 0 = ONLINE<br /><br /> 1 = RESTORING<br /><br /> 2 = RECOVERING<br /><br /> 3 = RECOVERY_PENDING<br /><br /> 4 = SUSPECT<br /><br /> 5 = [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br /><br /> 6 = OFFLINE<br /><br /> 7 = DEFUNCT|  
|**state_desc**|**nvarchar(60)**|Description of the file state:<br /><br /> ONLINE<br /><br /> RESTORING<br /><br /> RECOVERING<br /><br /> RECOVERY_PENDING<br /><br /> SUSPECT<br /><br /> OFFLINE<br /><br /> DEFUNCT<br /><br /> For more information, see [File States](../../relational-databases/databases/file-states.md).|  
|**size**|**int**|Current size of the file, in 8-KB pages.<br /><br /> 0 = Not applicable<br /><br /> For a database snapshot, size reflects the maximum space that the snapshot can ever use for the file.<br /><br /> For FILESTREAM filegroup containers, size reflects the current used size of the container.|  
|**max_size**|**int**|Maximum file size, in 8-KB pages:<br /><br /> 0 = No growth is allowed.<br /><br /> -1 = File will grow until the disk is full.<br /><br /> 268435456 = Log file will grow to a maximum size of 2 TB.<br /><br /> For FILESTREAM filegroup containers, max_size reflects the maximum size of the container.<br /><br /> Note that databases that are upgraded with an unlimited log file size will report -1 for the maximum size of the log file.|  
|**growth**|**int**|0 = File is fixed size and will not grow.<br /><br /> >0 = File will grow automatically.<br /><br /> If is_percent_growth = 0, growth increment is in units of 8-KB pages, rounded to the nearest 64 KB.<br /><br /> If is_percent_growth = 1, growth increment is expressed as a whole number percentage.|  
|**is_media_read_only**|**bit**|1 = File is on read-only media.<br /><br /> 0 = File is on read-write media.|  
|**is_read_only**|**bit**|1 = File is marked read-only.<br /><br /> 0 = File is marked read/write.|  
|**is_sparse**|**bit**|1 = File is a sparse file.<br /><br /> 0 = File is not a sparse file.<br /><br /> For more information, see [View the Size of the Sparse File of a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md).|  
|**is_percent_growth**|**bit**|1 = Growth of the file is a percentage.<br /><br /> 0 = Absolute growth size in pages.|  
|**is_name_reserved**|**bit**|1 = Dropped file name (name or physical_name) is reusable only after the next log backup. When files are dropped from a database, the logical names stay in a reserved state until the next log backup. This column is relevant only under the full recovery model and the bulk-logged recovery model.|  
|**create_lsn**|**numeric(25,0)**|Log sequence number (LSN) at which the file was created.|  
|**drop_lsn**|**numeric(25,0)**|LSN at which the file was dropped.<br /><br /> 0 = The file name is unavailable for reuse.|  
|**read_only_lsn**|**numeric(25,0)**|LSN at which the filegroup that contains the file changed from read/write to read-only (most recent change).|  
|**read_write_lsn**|**numeric(25,0)**|LSN at which the filegroup that contains the file changed from read-only to read/write (most recent change).|  
|**differential_base_lsn**|**numeric(25,0)**|Base for differential backups. Data extents changed after this LSN will be included in a differential backup.|  
|**differential_base_guid**|**uniqueidentifier**|Unique identifier of the base backup on which a differential backup will be based.|  
|**differential_base_time**|**datetime**|Time corresponding to differential_base_lsn.|  
|**redo_start_lsn**|**numeric(25,0)**|LSN at which the next roll forward must start.<br /><br /> Is NULL unless state = RESTORING or state = RECOVERY_PENDING.|  
|**redo_start_fork_guid**|**uniqueidentifier**|Unique identifier of the recovery fork. The first_fork_guid of the next log backup restored must match this value. This represents the current state of the file.|  
|**redo_target_lsn**|**numeric(25,0)**|LSN at which the online roll forward on this file can stop.<br /><br /> Is NULL unless state = RESTORING or state = RECOVERY_PENDING.|  
|**redo_target_fork_guid**|**uniqueidentifier**|The recovery fork on which the file can be recovered. Paired with redo_target_lsn.|  
|**backup_lsn**|**numeric(25,0)**|The LSN of the most recent data or differential backup of the file.|  
  
> [!NOTE]  
>  When you drop or rebuild large indexes, or drop or truncate large tables, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. Deferred drop operations do not release allocated space immediately. Therefore, the values returned by sys.database_files immediately after dropping or truncating a large object may not reflect the actual disk space available.  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  

## Examples  
The following statement returns the name, file size, and the amount of empty space for each database file.

```
SELECT name, size/128.0 FileSizeInMB,
size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 
   AS EmptySpaceInMB
FROM sys.database_files;
```
For more information when using [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], see [Determining Database Size in Azure SQL Database V12](https://blogs.msdn.microsoft.com/sqlcat/2016/09/21/determining-database-size-in-azure-sql-database-v12/) on the SQL Customer Advisory Team blog.
  
## See Also  
 [Databases and Files Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)   
 [File States](../../relational-databases/databases/file-states.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)   
 [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)   
 [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)  
  
  
