---
title: "sys.dm_db_xtp_checkpoint_files (Transact-SQL)"
description: sys.dm_db_xtp_checkpoint_files displays information about In-Memory OLTP checkpoint files, including file size, physical location, and transaction ID. Learn how this view differs for versions of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2022"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: "reference"
f1_keywords:
  - "dm_db_xtp_checkpoint_files"
  - "sys.dm_db_xtp_checkpoint_files_TSQL"
  - "dm_db_xtp_checkpoint_files_TSQL"
  - "sys.dm_db_xtp_checkpoint_files"
helpviewer_keywords:
  - "sys.dm_db_xtp_checkpoint_files dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_xtp_checkpoint_files (Transact-SQL)
[!INCLUDE[sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Displays information about [!INCLUDE[inmemory](../../includes/inmemory-md.md)] checkpoint files, including file size, physical location and the transaction ID.  
  
> [!NOTE]
> For the current checkpoint that has not closed, the state column of `sys.dm_db_xtp_checkpoint_files` will be UNDER CONSTRUCTION for new files. A checkpoint closes automatically when there is sufficient transaction log growth since the last checkpoint, or if you issue the `CHECKPOINT` command. For more information, see [CHECKPOINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/checkpoint-transact-sql.md).  
  
 A memory-optimized file group internally uses append-only files to store inserted and deleted rows for in-memory tables. There are two types of files. A data file contains inserted rows while a delta file contains references to deleted rows. 
  
 [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] is substantially different from more recent versions and is discussed lower in the topic at [SQL Server 2014](#bkmk_2014).  
  
 For more information, see [Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md).  
  
##  <a name="bkmk_2016"></a> [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later  
 The following table describes the columns for `sys.dm_db_xtp_checkpoint_files`, beginning with **[!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]**.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|container_id|**int**|The ID of the container (represented as a file with type FILESTREAM in `sys.database_files`) that the data or delta file is part of. Joins with `file_id` in [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md).|  
|container_guid|**uniqueidentifier**|GUID of the Container, which the root, data or delta file is part of. Joins with `file_guid` in the `sys.database_files` table.|  
|checkpoint_file_id|**uniqueidentifier**|GUID of the checkpoint file.|  
|relative_file_path|**nvarchar(256)**|Path of the file relative to container it is mapped to.|  
|file_type|**smallint**|-1 for FREE<br /><br /> 0 for DATA file.<br /><br /> 1 for DELTA file.<br /><br /> 2 for ROOT file<br /><br /> 3 for LARGE DATA file|  
|file_type_desc|**nvarchar(60)**|FREE- All files maintained as FREE are available for allocation. Free files can vary in size depending on anticipated needs by the system. The maximum size is 1GB.<br /><br /> DATA - Data files contain rows that have been inserted into memory-optimized tables.<br /><br /> DELTA - Delta files contain references to rows in data files that have been deleted.<br /><br /> ROOT - Root files contain system metadata for memory-optimized and natively compiled objects.<br /><br /> LARGE DATA - Large data files contain values inserted in (n)varchar(max) and varbinary(max) columns, as well as the column segments that are part of columnstore indexes on memory-optimized tables.|  
|internal_storage_slot|**int**|The index of the file in the internal storage array. NULL for ROOT or for state other than 1.|  
|checkpoint_pair_file_id|**uniqueidentifier**|Corresponding DATA or DELTA file. NULL for ROOT.|  
|file_size_in_bytes|**bigint**|Size of the file on the disk.|  
|file_size_used_in_bytes|**bigint**|For checkpoint file pairs that are still being populated, this column will be updated after the next checkpoint.|  
|logical_row_count|**bigint**|For Data, number of rows inserted.<br /><br /> For Delta, number of rows deleted after accounting for drop table.<br /><br /> For Root, NULL.|  
|state|**smallint**|0 - PRECREATED<br /><br /> 1 - UNDER CONSTRUCTION<br /><br /> 2 - ACTIVE<br /><br /> 3 - MERGE TARGET<br /><br /> 8 - WAITING FOR LOG TRUNCATION|  
|state_desc|**nvarchar(60)**|PRECREATED - A number of checkpoint files are pre-allocated to minimize or eliminate any waits to allocate new files as transactions are being executed. These pre-created files can vary in size, depending on the estimated needs of the workload, but they contain no data. This is a storage overhead in databases with a MEMORY_OPTIMIZED_DATA filegroup.<br /><br /> UNDER CONSTRUCTION - These checkpoint files are under construction, meaning they are being populated based on the log records generated by the database, and are not yet part of a checkpoint.<br /><br /> ACTIVE - These contain the inserted/deleted rows from previous closed checkpoints. They contain the contents of the tables that area read into memory before applying the active part of the transaction log at the database restart. We expect that size of these checkpoint files to be approximately 2x of the in-memory size of memory-optimized tables, assuming the merge operation is keeping up with the transactional workload.<br /><br /> MERGE TARGET - The target of merge operations - these checkpoint files store the consolidated data rows from the source files that were identified by the merge policy. Once the merge is installed, the MERGE TARGET transitions into ACTIVE state.<br /><br /> WAITING FOR LOG TRUNCATION - Once the merge has been installed and the MERGE TARGET CFP is part of durable checkpoint, the merge source checkpoint files transition into this state. Files in this state are needed for operational correctness of the database with memory-optimized table.  For example, to recover from a durable checkpoint to go back in time.|  
|lower_bound_tsn|**bigint**|Lower bound of the transaction in the file; null if state not in (1, 3).|  
|upper_bound_tsn|**bigint**|Upper bound of the transaction in the file; null if state not in (1, 3).|  
|begin_checkpoint_id|**bigint**|ID of the begin checkpoint.|  
|end_checkpoint_id|**bigint**|ID of the end checkpoint.|  
|last_updated_checkpoint_id|**bigint**|ID of the last checkpoint that updated this file.|  
|encryption_status|**smallint**|0, 1, 2|  
|encryption_status_desc|**nvarchar(60)**|0 => UNENCRYPTED<br /><br /> 1 => ENCRYPTED WITH KEY 1<br /><br /> 2 => ENCRYPTED WITH KEY 2. Valid only for active files.|  
  
##  <a name="bkmk_2014"></a> [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
 The following table describes the columns for `sys.dm_db_xtp_checkpoint_files`, for **[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]**.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|container_id|**int**|The ID of the container (represented as a file with type FILESTREAM in `sys.database_files`) that the data or delta file is part of. Joins with `file_id` in [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md).|  
|container_guid|**uniqueidentifier**|The GUID of the container that the data or delta file is part of.|  
|checkpoint_file_id|**GUID**|ID of the data or delta file.|  
|relative_file_path|**nvarchar(256)**|Path to the data or delta file, relative to the location of the container.|  
|file_type|**tinyint**|0 for data file.<br /><br /> 1 for delta file.<br /><br /> NULL if the state column is set to 7.|  
|file_type_desc|**nvarchar(60)**|The type of file: DATA_FILE, DELTA_FILE, or NULL if the state column is set to 7.|  
|internal_storage_slot|**int**|The index of the file in the internal storage array. NULL if the state column is not 2 or 3.|  
|checkpoint_pair_file_id|**uniqueidentifier**|The corresponding data or delta file.|  
|file_size_in_bytes|**bigint**|Size of the file that is used. NULL if the state column is set to 5, 6, or 7.|  
|file_size_used_in_bytes|**bigint**|Used size of the file that is used. NULL if the state column is set to 5, 6, or 7.<br /><br /> For checkpoint file pairs that are still being populated, this column will be updated after the next checkpoint.|  
|inserted_row_count|**bigint**|Number of rows in the data file.|  
|deleted_row_count|**bigint**|Number of deleted rows in the delta file.|  
|drop_table_deleted_row_count|**bigint**|The number of rows in the data files affected by a drop table. Applies to data files when the state column equals 1.<br /><br /> Shows deleted row counts from dropped table(s). The drop_table_deleted_row_count statistics are compiled after the memory garbage collection of the rows from dropped table(s) is complete and a checkpoint is taken. If you restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before the drop tables statistics are reflected in this column, the statistics will be updated as part of recovery. The recovery process does not load rows from dropped tables. Statistics for dropped tables are compiled during the load phase and reported in this column when recovery completes.|  
|state|**int**|0 - PRECREATED<br /><br /> 1 - UNDER CONSTRUCTION<br /><br /> 2 - ACTIVE<br /><br /> 3 - MERGE TARGET<br /><br /> 4 - MERGED SOURCE<br /><br /> 5 - REQUIRED FOR BACKUP/HA<br /><br /> 6 - IN TRANSITION TO TOMBSTONE<br /><br /> 7 - TOMBSTONE|  
|state_desc|**nvarchar(60)**|PRECREATED - A small set of data and delta file pairs, also known as checkpoint file pairs (CFPs) are kept pre-allocated to minimize or eliminate any waits to allocate new files as transactions are being executed. These are full sized with data file size of 128MB and delta file size of 8 MB but contain no data. The number of CFPs is computed as the number of logical processors or schedulers (one per core, no maximum) with a minimum of 8. This is a fixed storage overhead in databases with memory-optimized tables.<br /><br /> UNDER CONSTRUCTION - Set of CFPs that store newly inserted and possibly deleted data rows since the last checkpoint.<br /><br /> ACTIVE - These contain the inserted and deleted rows from previous closed checkpoints. These CFPs contain all required inserted and deleted rows required before applying the active part of the transaction log at the database restart. The size of these CFPs will be approximately 2 times the in-memory size of memory-optimized tables, assuming the merge operation is current with the transactional workload.<br /><br /> MERGE TARGET - The CFP stores the consolidated data rows from the CFP(s) that were identified by the merge policy. Once the merge is installed, the MERGE TARGET transitions into ACTIVE state.<br /><br /> MERGED SOURCE - Once the merge operation is installed, the source CFPs are marked as MERGED SOURCE. Note, the merge policy evaluator may identify multiple merges but a CFP can only participate in one merge operation.<br /><br /> REQUIRED FOR BACKUP/HA - Once the merge has been installed and the MERGE TARGET CFP is part of durable checkpoint, the merge source CFPs transition into this state. CFPs in this state are needed for operational correctness of the database with memory-optimized table.  For example, to recover from a durable checkpoint to go back in time. A CFP can be marked for garbage collection once the log truncation point moves beyond its transaction range.<br /><br /> IN TRANSITION TO TOMBSTONE - These CFPs are not needed by the [!INCLUDE[inmemory](../../includes/inmemory-md.md)] engine and can they can be garbage collected. This state indicates that these CFPs are waiting for the background thread to transition them to the next state, which is TOMBSTONE.<br /><br /> TOMBSTONE - These CFPs are waiting to be garbage collected by the filestream garbage collector. ([sp_filestream_force_garbage_collection &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/filestream-and-filetable-sp-filestream-force-garbage-collection.md))|  
|lower_bound_tsn|**bigint**|The lower bound of transactions contained in the file. Null if the state column is other than 2, 3, or 4.|  
|upper_bound_tsn|**bigint**|The upper bound of transactions contained in the file. Null if the state column is other than 2, 3, or 4.|  
|last_backup_page_count|**int**|Logical page count that is determined at last backup. Applies when the state column is set to 2, 3, 4, or 5. NULL if page count not known.|  
|delta_watermark_tsn|**int**|The transaction of the last checkpoint that wrote to this delta file. This is the watermark for the delta file.|  
|last_checkpoint_recovery_lsn|**nvarchar(23)**|Recovery log sequence number of the last checkpoint that still needs the file.|  
|tombstone_operation_lsn|**nvarchar(23)**|The file will be deleted once the tombstone_operation_lsn falls behind the log truncation log sequence number.|  
|logical_deletion_log_block_id|**bigint**|Applies only to state 5.|  
  
## Permissions  
 Requires `VIEW DATABASE STATE` permission on the server.  
  
## Use Cases  
 You can estimate the total storage used by [!INCLUDE[inmemory](../../includes/inmemory-md.md)] as follows:  
  
```sql  
-- total storage used by In-Memory OLTP  
SELECT SUM (file_size_in_bytes)/(1024*1024) as file_size_in_MB  
FROM sys.dm_db_xtp_checkpoint_files;
```  
  
To see a breakdown of storage utilization by state and file type run the following query:
  
```sql  
SELECT state_desc  
 , file_type_desc  
 , COUNT(*) AS [count]  
 , SUM(file_size_in_bytes) / 1024 / 1024 AS [on-disk size MB]   
FROM sys.dm_db_xtp_checkpoint_files  
GROUP BY state, state_desc, file_type, file_type_desc  
ORDER BY state, file_type;
```  
  
## See also

- [Introduction to Memory-Optimized Tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md)
- [Memory-Optimized Table Dynamic Management Views](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)

## Next steps 

- [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] Overview and Usage Scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview)
  
