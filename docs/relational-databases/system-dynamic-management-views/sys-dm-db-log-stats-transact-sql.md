---
title: "sys.dm_db_log_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "dm_db_log_stats_TSQL"
  - "sys.dm_db_log_stats"
  - "sys.dm_db_log_stats_TSQL"
  - "dm_db_log_stats"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_log_stats dynamic management function"
ms.assetid: 
caps.latest.revision: 
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.dm_db_log_stats (Transact-SQL)   
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]  

Returns summary level attributes and information on transaction log files of databases. Use this information for monitoring and diagnostics of transaction log health.   
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
 sys.dm_db_log_stats ( database_id )
```  
  
## Arguments  

*database_id* | NULL | **DEFAULT**

Is the ID of the database. `database_id` is `int`. Valid inputs are the ID number of a database, `NULL`, or `DEFAULT`. The default is `NULL`. `NULL` and `DEFAULT` are equivalent values in the context of current database.  
The built-in function `DB_ID` can be specified. When using `DB_ID` without specifying a database name, the compatibility level of the current database must be 90 or greater.

  
## Tables Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id	|**int**	|Database ID |  
|recovery_model	|**nvarchar(60)**	|	Recovery model of the database. Possible values include: <br /> SIMPLE<br /> BULK_LOGGED <br /> FULL |  
|log_min_lsn	|**nvarchar(24)**	|	Current start LSN in the transaction log. |  
|log_end_lsn	|**nvarchar(24)**	|	LSN of the last log record in the transaction log. |  
|current_vlf_sequence_number	|**bigint**	|	Current VLF sequence number at the time of execution. |  
|current_vlf_size_mb	|**float**	|	Current VLF size in MB. |   
|total_vlf_count	|**bigint**	|	Total number of VLFs in the transaction log. |  
|total_log_size_mb	|**float**	|	Total transaction log size in MB. |  
|active_vlf_count	|**bigint**	|	Total number of active VLFs in the transaction log. |  
|active_log_size_mb	|**float**	|	Total active transaction log size in MB. |  
|log_truncation_holdup_reason	|**nvarchar(60)**	|	Log truncation holdup reason. The value is same as  `log_reuse_wait_desc` column of `sys.databases`.  (For more detailed explanations of these values, see [The Transaction Log](../../relational-databases/logs/the-transaction-log-sql-server.md)). <br />Possible values include: <br />NOTHING<br />CHECKPOINT<br />LOG_BACKUP<br />ACTIVE_BACKUP_OR_RESTORE<br />ACTIVE_TRANSACTION<br />DATABASE_MIRRORING<br />REPLICATION<br />DATABASE_SNAPSHOT_CREATION<br />LOG_SCAN<br />AVAILABILITY_REPLICA<br />OLDEST_PAGE<br />XTP_CHECKPOINT<br />OTHER TRANSIENT |  
|log_backup_time	|**datetime**	|	Last transaction log backup time. |   
|log_backup_lsn	|**nvarchar(24)**	|	Last transaction log backup LSN. |   
|log_since_last_log_backup_mb	|**float**	|	Log size in MB since last transaction log backup LSN. |  
|log_checkpoint_lsn	|**nvarchar(24)**	|	Last checkpoint LSN. |  
|log_since_last_checkpoint_mb	|**float**	|	Log size in MB since last checkpoint LSN. |  
|log_recovery_lsn	|**nvarchar(24)**	|	Recovery LSN of the database. If `log_recovery_lsn` occurs before the checkpoint LSN, `log_recovery_lsn` is the oldest active transaction LSN, otherwise `log_recovery_lsn` is the checkpoint LSN. |  
|log_recovery_size_mb	|**float**	|	Log size in MB since log recovery LSN. |  
|recovery_vlf_count	|**bigint**	|	Total number of VLFs to be recovered, if there was failover or server restart. |  


## Permissions  
Requires the `VIEW DATABASE STATE` permission in the database.   
  
## Examples  

### A. Determining databases in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance with high number of vlfs   
The following query returns the databases with more than 100 vlfs in the log files. Large numbers of vlfs can affect the database startup, restore, and recovery time.

```  
SELECT name AS 'Database Name', total_vlf_count AS 'VLF count' 
FROM sys.databases AS s
CROSS APPLY sys.dm_db_log_stats(s.database_id) 
WHERE total_vlf_count  > 100;
```   

### B. Determining databases in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance with transaction log backups older than 4 hours   
The following query determines the last log backup times for the databases in the instance.

```  
SELECT name AS 'Database Name', log_backup_time AS 'last log backup time' 
FROM sys.databases AS s
CROSS APPLY sys.dm_db_log_stats(s.database_id); 
```

## See Also  
[Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
[Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)   
[sys.dm_db_log_space_usage](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md)   
[sys.dm_db_log_info](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md)  

  
