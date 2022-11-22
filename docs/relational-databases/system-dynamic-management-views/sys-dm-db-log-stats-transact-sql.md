---
title: "sys.dm_db_log_stats (Transact-SQL)"
description: sys.dm_db_log_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/17/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_db_log_stats_TSQL"
  - "sys.dm_db_log_stats"
  - "sys.dm_db_log_stats_TSQL"
  - "dm_db_log_stats"
helpviewer_keywords:
  - "sys.dm_db_log_stats dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_log_stats (Transact-SQL)   
[!INCLUDE[tsql-appliesto-2016sp2-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md)]

Returns summary level attributes and information on transaction log files of databases. Use this information for monitoring and diagnostics of transaction log health.   
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
 sys.dm_db_log_stats ( database_id )
```  
  
## Arguments  

*database_id* | NULL | **DEFAULT**

Is the ID of the database. `database_id` is `int`. Valid inputs are the ID number of a database, `NULL`, or `DEFAULT`. The default is `NULL`. `NULL` and `DEFAULT` are equivalent values in the context of current database.  
The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified. When using `DB_ID` without specifying a database name, the compatibility level of the current database must be 90 or greater.

  
## Tables Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id	|**int**	|Database ID |  
|recovery_model	|**nvarchar(60)**	|	Recovery model of the database. Possible values include: <br /> SIMPLE<br /> BULK_LOGGED <br /> FULL |  
|log_min_lsn	|**nvarchar(24)**	|	Current start [log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch) in the transaction log.|  
|log_end_lsn	|**nvarchar(24)**	|	[log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch) of the last log record in the transaction log.|  
|current_vlf_sequence_number	|**bigint**	|	Current [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) sequence number at the time of execution.|  
|current_vlf_size_mb	|**float**	|	Current [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) size in MB.|   
|total_vlf_count	|**bigint**	|	Total number of [virtual log files (VLFs)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) in the transaction log. |  
|total_log_size_mb	|**float**	|	Total transaction log size in MB. |  
|active_vlf_count	|**bigint**	|	Total number of active [virtual log files (VLFs)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) in the transaction log.|  
|active_log_size_mb	|**float**	|	Total active transaction log size in MB.|  
|log_truncation_holdup_reason	|**nvarchar(60)**	|	Log truncation holdup reason. The value is same as  `log_reuse_wait_desc` column of `sys.databases`.  (For more detailed explanations of these values, see [The Transaction Log](../../relational-databases/logs/the-transaction-log-sql-server.md)). <br />Possible values include: <br />NOTHING<br />CHECKPOINT<br />LOG_BACKUP<br />ACTIVE_BACKUP_OR_RESTORE<br />ACTIVE_TRANSACTION<br />DATABASE_MIRRORING<br />REPLICATION<br />DATABASE_SNAPSHOT_CREATION<br />LOG_SCAN<br />AVAILABILITY_REPLICA<br />OLDEST_PAGE<br />XTP_CHECKPOINT<br />OTHER TRANSIENT |  
|log_backup_time	|**datetime**	|	Last transaction log backup time.|   
|log_backup_lsn	|**nvarchar(24)**	|	Last transaction log backup [log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch).|   
|log_since_last_log_backup_mb	|**float**	|	Log size in MB since last transaction log backup [log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch).|  
|log_checkpoint_lsn	|**nvarchar(24)**	|	Last checkpoint [log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch).|  
|log_since_last_checkpoint_mb	|**float**	|	Log size in MB since last checkpoint [log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch).|  
|log_recovery_lsn	|**nvarchar(24)**	|	Recovery [log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch) of the database. If `log_recovery_lsn` occurs before the checkpoint LSN, `log_recovery_lsn` is the oldest active transaction LSN, otherwise `log_recovery_lsn` is the checkpoint LSN.|  
|log_recovery_size_mb	|**float**	|	Log size in MB since log recovery [log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch).|  
|recovery_vlf_count	|**bigint**	|	Total number of [virtual log files (VLFs)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) to be recovered, if there was failover or server restart. |  


## Remarks
When running `sys.dm_db_log_stats` against a database that is participating in an Availability Group as a secondary replica, only a subset of the fields described above will be returned.  Currently, only `database_id`, `recovery_model`, and `log_backup_time` will be returned when run against a secondary database.   

## Permissions  
Requires the `VIEW DATABASE STATE` permission in the database.   
  
## Examples  

### A. Determining databases in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance with high number of VLFs   
The following query returns the databases with more than 100 VLFs in the log files. Large numbers of VLFs can affect the database startup, restore, and recovery time.

```sql  
SELECT name AS 'Database Name', total_vlf_count AS 'VLF count' 
FROM sys.databases AS s
CROSS APPLY sys.dm_db_log_stats(s.database_id) 
WHERE total_vlf_count  > 100;
```   

### B. Determining databases in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance with transaction log backups older than 4 hours   
The following query determines the last log backup times for the databases in the instance.

```sql  
SELECT name AS 'Database Name', log_backup_time AS 'last log backup time' 
FROM sys.databases AS s
CROSS APPLY sys.dm_db_log_stats(s.database_id); 
```

## See Also  
[Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
[Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)   
[sys.dm_db_log_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md)   
[sys.dm_db_log_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md)    
  
