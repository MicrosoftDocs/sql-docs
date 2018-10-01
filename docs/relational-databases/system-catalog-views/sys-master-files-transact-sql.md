---
title: "sys.master_files (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.master_files"
  - "master_files_TSQL"
  - "sys.master_files_TSQL"
  - "master_files"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.master_files catalog view"
ms.assetid: 803b22f2-0016-436b-a561-ce6f023d6b6a
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.master_files (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]

  Contains a row per file of a database as stored in the master database. This is a single, system-wide view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**int**|ID of the database to which this file applies. The masterdatabase_id is always 1.|  
|file_id|**int**|ID of the file within database. The primary file_id is always 1.|  
|file_guid|**uniqueidentifier**|Unique identifier of the file.<br /><br /> NULL = Database was upgraded from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|type|**tinyint**|File type:<br /><br /> 0 = Rows.<br /><br /> 1 = Log<br /><br /> 2 = FILESTREAM<br /><br /> 3 = [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br /><br /> 4 = Full-text (Full-text catalogs earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]; full-text catalogs that are upgraded to or created in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or higher will report a file type 0.)|  
|type_desc|**nvarchar(60)**|Description of the file type:<br /><br /> ROWS<br /><br /> LOG<br /><br /> FILESTREAM<br /><br /> FULLTEXT (Full-text catalogs earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].)|  
|data_space_id|**int**|ID of the data space to which this file belongs. Data space is a filegroup.<br /><br /> 0 = Log files|  
|name|**sysname**|Logical name of the file in the database.|  
|physical_name|**nvarchar(260)**|Operating-system file name.|  
|state|**tinyint**|File state:<br /><br /> 0 = ONLINE<br /><br /> 1 = RESTORING<br /><br /> 2 = RECOVERING<br /><br /> 3 = RECOVERY_PENDING<br /><br /> 4 = SUSPECT<br /><br /> 5 = [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br /><br /> 6 = OFFLINE<br /><br /> 7 = DEFUNCT|  
|state_desc|**nvarchar(60)**|Description of the file state:<br /><br /> ONLINE<br /><br /> RESTORING<br /><br /> RECOVERING<br /><br /> RECOVERY_PENDING<br /><br /> SUSPECT<br /><br /> OFFLINE<br /><br /> DEFUNCT<br /><br /> For more information, see [File States](../../relational-databases/databases/file-states.md).|  
|size|**int**|Current file size, in 8-KB pages. For a database snapshot, size reflects the maximum space that the snapshot can ever use for the file.<br /><br /> Note: This field is populated as zero for FILESTREAM containers. Query the *sys.database_files* catalog view for the actual size of FILESTREAM containers.|  
|max_size|**int**|Maximum file size, in 8-KB pages:<br /><br /> 0 = No growth is allowed.<br /><br /> -1 = File will grow until the disk is full.<br /><br /> 268435456 = Log file will grow to a maximum size of 2 TB.<br /><br /> Note: Databases that are upgraded with an unlimited log file size will report -1 for the maximum size of the log file.|  
|growth|**int**|0 = File is fixed size and will not grow.<br /><br /> >0 = File will grow automatically.<br /><br /> If is_percent_growth = 0, growth increment is in units of 8-KB pages, rounded to the nearest 64 KB<br /><br /> If is_percent_growth = 1, growth increment is expressed as a whole number percentage.|  
|is_media_read_onlyF|**bit**|1 = File is on read-only media.<br /><br /> 0 = File is on read/write media.|  
|is_read_only|**bit**|1 = File is marked read-only.<br /><br /> 0 = file is marked read/write.|  
|is_sparse|**bit**|1 = File is a sparse file.<br /><br /> 0 = File is not a sparse file.<br /><br /> For more information, see [View the Size of the Sparse File of a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md).|  
|is_percent_growth|**bit**|1 = Growth of the file is a percentage.<br /><br /> 0 = Absolute growth size in pages.|  
|is_name_reserved|**bit**|1 = Dropped file name is reusable. A log backup must be taken before the name (name or physical_name) can be reused for a new file name.<br /><br /> 0 = File name is unavailable for reuse.|  
|create_lsn|**numeric(25,0)**|Log sequence number (LSN) at which the file was created.|  
|drop_lsn|**numeric(25,0)**|LSN at which the file was dropped.|  
|read_only_lsn|**numeric(25,0)**|LSN at which the filegroup that contains the file changed from read/write to read-only (most recent change).|  
|read_write_lsn|**numeric(25,0)**|LSN at which the filegroup that contains the file changed from read-only to read/write (most recent change).|  
|differential_base_lsn|**numeric(25,0)**|Base for differential backups. Data extents changed after this LSN will be included in a differential backup.|  
|differential_base_guid|**uniqueidentifier**|Unique identifier of the base backup on which a differential backup will be based.|  
|differential_base_time|**datetime**|Time corresponding to differential_base_lsn.|  
|redo_start_lsn|**numeric(25,0)**|LSN at which the next roll forward must start.<br /><br /> Is NULL unless state = RESTORING or state = RECOVERY_PENDING.|  
|redo_start_fork_guid|**uniqueidentifier**|Unique identifier of the recovery fork. The first_fork_guid of the next log backup restored must match this value. This represents the current state of the container.|  
|redo_target_lsn|**numeric(25,0)**|LSN at which the online roll forward on this file can stop.<br /><br /> Is NULL unless state = RESTORING or state = RECOVERY_PENDING.|  
|redo_target_fork_guid|**uniqueidentifier**|The recovery fork on which the container can be recovered. Paired with redo_target_lsn.|  
|backup_lsn|**numeric(25,0)**|The LSN of the most recent data or differential backup of the file.|  
|credential_id|**int**|The `credential_id` from `sys.credentials` used for storing the file. For example, when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running on an Azure Virtual Machine and the database files are stored in Azure blob storage, a credential is configured with the access credentials to the storage location.|  
  
> [!NOTE]  
>  When you drop or rebuild large indexes, or drop or truncate large tables, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. Deferred drop operations do not release allocated space immediately. Therefore, the values returned by sys.master_files immediately after dropping or truncating a large object may not reflect the actual disk space available.  
  
## Permissions  
 The minimum permissions that are required to see the corresponding row are CREATE DATABASE, ALTER ANY DATABASE, or VIEW ANY DEFINITION.  
  
## See Also  
 [Databases and Files Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)   
 [File States](../../relational-databases/databases/file-states.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)  
  
  
