---
title: "sys.dm_repl_traninfo (Transact-SQL)"
description: sys.dm_repl_traninfo (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_repl_traninfo"
  - "dm_repl_traninfo"
  - "sys.dm_repl_traninfo_TSQL"
  - "dm_repl_traninfo_TSQL"
helpviewer_keywords:
  - "sys.dm_repl_traninfo dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 5abe2605-0506-46ec-82b5-6ec08428ba13
---
# sys.dm_repl_traninfo (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information on each replicated or change data capture transaction.  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**fp2p_pub_exists**|**tinyint**|If the transaction is in a database published using peer-to-peer transactional replication. If true, the value is 1; otherwise, it is 0.|  
|**db_ver**|**int**|Database version.|  
|**comp_range_address**|**varbinary(8)**|Defines a partial rollback range that must be skipped.|  
|**textinfo_address**|**varbinary(8)**|In-memory address of the cached text information structure.|  
|**fsinfo_address**|**varbinary(8)**|In-memory address of the cached filestream information structure.|  
|**begin_lsn**|**nvarchar(64)**|Log sequence number (LSN) of the beginning log record for the transaction.|  
|**commit_lsn**|**nvarchar(64)**|LSN of commit log record for the transaction.|  
|**dbid**|**smallint**|Database ID.|  
|**rows**|**int**|ID of the replicated command within the transaction.|  
|**xdesid**|**nvarchar(64)**|Transaction ID.|  
|**artcache_table_address**|**varbinary(8)**|In-memory address of the cached article table structure last used for this transaction.|  
|**server**|**nvarchar(514)**|Server name.|  
|**server_len_in_bytes**|**smallint**|Character length, in bytes, of the server name.|  
|**database**|**nvarchar(514)**|Database name.|  
|**db_len_in_bytes**|**smallint**|Character length, in bytes, of the database name.|  
|**originator**|**nvarchar(514)**|Name of the server where the transaction originated.|  
|**originator_len_in_bytes**|**smallint**|Character length, in bytes, of the server where the transaction originated.|  
|**orig_db**|**nvarchar(514)**|Name of the database where the transaction originated.|  
|**orig_db_len_in_bytes**|**smallint**|Character length, in bytes, of the database where the transaction originated.|  
|**cmds_in_tran**|**int**|Number of replicated commands in the current transaction, which is used to determine when a logical transaction should be committed.|  
|**is_boundedupdate_singleton**|**tinyint**|Specifies whether a unique column update affects only a single row.|  
|**begin_update_lsn**|**nvarchar(64)**|LSN used in a unique column update.|  
|**delete_lsn**|**nvarchar(64)**|LSN to delete as part of an update.|  
|**last_end_lsn**|**nvarchar(64)**|Last LSN in a logical transaction.|  
|**fcomplete**|**tinyint**|Specifies whether the command is a partial update.|  
|**fcompensated**|**tinyint**|Specifies whether the transaction is involved in a partial rollback.|  
|**fprocessingtext**|**tinyint**|Specifies whether the transaction includes a binary large data type column.|  
|**max_cmds_in_tran**|**int**|Maximum number of commands in a logical transaction, as specified by the Log Reader Agent.|  
|**begin_time**|**datetime**|Time the transaction began.|  
|**commit_time**|**datetime**|Time the transaction was committed.|  
|**session_id**|**int**|ID of the change data capture log scan session. This column maps to the **session_id** column in [sys.dm_cdc_logscan_sessions](../../relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-log-scan-sessions.md).|  
|**session_phase**|**int**|Number that indicates the phase the session was in at the time the error occurred. This column maps to the **phase_number** column in [sys.dm_cdc_errors](../../relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md).|  
|**is_known_cdc_tran**|**bit**|Indicates the transaction is tracked by change data capture.<br /><br /> 0 = Transaction replication transaction.<br /><br /> 1 = Change data capture transaction.|  
|**error_count**|**int**|Number of errors encountered.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the publication database or on the database enabled for change data capture.  
  
## Remarks  
 Information is only returned for replicated database objects or tables enabled for change data capture that are currently loaded in the article cache.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Replication Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/replication-related-dynamic-management-views-transact-sql.md)   
 [Change Data Capture Related Dynamic Management Views &#40;Transact-SQL&#41;](./system-dynamic-management-views.md)  
  
