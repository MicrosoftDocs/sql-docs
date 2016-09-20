---
title: "sys.database_files (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 60b3d20c-bd96-4c67-8f9d-05314518f809
caps.latest.revision: 14
author: BarbKess
---
# sys.database_files (SQL Server PDW)
Returns one row per file of the current SQL Server PDW database as stored in the database itself. This is a per-database view.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|file_id|**int**|ID of the file within database.||  
|file_guid|**uniqueidentifier**|GUID for the file.||  
|type|**tinyint**|File type.|0 = Rows (distributed or replicated)<br /><br />1 = Log|  
|type_desc|**nvarchar(60)**|Description of the file type.|'ROWS','LOG'|  
|data_space_id|**int**|Value can be 0 or greater than 0. A value of 0 represents the database log file, and a value greater than 0 represents the ID of the filegroup where this data file is stored.||  
|name|**sysname**|Logical name of the file in the database.||  
|physical_name|**nvarchar(260)**|Operating system file name.||  
|state|**tinyint**|File state:<br /><br />0 = ONLINE<br /><br />1 = RESTORING<br /><br />2 = RECOVERING<br /><br />3 = RECOVERY_PENDING<br /><br />4 = SUSPECT<br /><br />5 = Not supported.<br /><br />6 = OFFLINE<br /><br />7 = DEFUNCT||  
|state_desc|**nvarchar(60)**|Description of the file state:<br /><br />ONLINE<br /><br />RESTORING<br /><br />RECOVERING<br /><br />RECOVERY_PENDING<br /><br />SUSPECT<br /><br />OFFLINE<br /><br />DEFUNCT||  
|size|**int**|Always 0. The size column is not supported in this release.||  
|max_size|**int**|Maximum file size, in 8 KB pages.<br /><br />0 = No growth is allowed.<br /><br />-1 = File will grow until the disk is full.<br /><br />268435456 = Log file will grow to a maximum size of 2 TB.<br /><br />**Note** Databases that are upgraded with an unlimited log file size will report -1 for the maximum size of the log file.||  
|growth|**int**|0 = File is fixed size and will not grow.<br /><br />>0 = File will grow automatically.<br /><br />If is_percent_growth = 0, growth increment is in units of 8 KB pages, rounded to the nearest 64 KB.<br /><br />If is_percent_growth = 1, growth increment is expressed as a whole number percentage.||  
|is_media_read_only|**bit**|1 = File is on read-only media.<br /><br />0 = File is on read-write media.|Always 0.|  
|is_read_only|**bit**|1 = File is marked read-only.<br /><br />0 = File is marked read/write.|Always 0.|  
|is_sparse|**bit**|1 = File is a sparse file.<br /><br />0 = File is not a sparse file.|Always 0.|  
|is_percent_growth|**bit**|1 = Growth of the file is a percentage.<br /><br />0 = Absolute growth size in pages.||  
|is_name_reserved|**bit**|1 = Dropped file name (name or physical_name) is reusable only after the next log backup. When files are dropped from a database, the logical names stay in a reserved state until the next log backup. This column is relevant only under the full recovery model and the bulk-logged recovery model.||  
|create_lsn|**numeric(25,0)**|Log sequence number (LSN) at which the file was created.||  
|drop_lsn|**numeric (25,0)**|LSN at which the file was dropped.<br /><br />0 = File name is unavailable for reuse.||  
|read_only_lsn|**numeric (25,0)**|LSN at which the filegroup that contains the file changed from read/write to read-only (most recent change).||  
|read_write_lsn|**numeric (25,0)**|LSN at which the filegroup that contains the file changed from read-only to read/write (most recent change).||  
|differential_base_lsn|**numeric (25,0)**|Base for differential backups. Data extents changed after this LSN will be included in a differential backup.||  
|differential_base_guid|**uniqueidentifier**|Unique identifier of the base backup on which a differential backup will be based.||  
|differential_base_time|**datetime**|Time corresponding to differential_base_lsn.||  
|redo_start_lsn|**numeric(25,0)**|LSN at which the next roll forward must start.<br /><br />Is NULL unless state = RESTORING or state = RECOVERY_PENDING.||  
|redo_start_fork_guid|**uniqueidentifier**|Unique identifier of the recovery fork. The first_fork_guid of the next log backup restored must match this value. This represents the current state of the file.||  
|redo_target_lsn|**numeric(25,0)**|LSN at which the online roll forward on this file can stop.<br /><br />Is NULL unless state = RESTORING or state = RECOVERY_PENDING.||  
|redo_target_fork_guid|**uniqueidentifier**|The recovery fork on which the file can be recovered. Paired with redo_target_lsn.||  
|backup_lsn|**numeric(25,0)**|The LSN of the most recent data or differential backup of the file.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQL Server Catalog Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
  
