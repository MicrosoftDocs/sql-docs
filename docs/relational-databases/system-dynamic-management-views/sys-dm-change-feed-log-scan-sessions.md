---
title: "sys.dm_change_feed_log_scan_sessions (Transact-SQL)"
description: sys.dm_change_feed_log_scan_sessions (Transact-SQL) shows activity for the Azure Synapse Link feature.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "sys.dm_change_feed_log_scan_sessions_TSQL"
  - "sys.dm_change_feed_log_scan_sessions"
  - "dm_change_feed_log_scan_sessions_TSQL"
  - "dm_change_feed_log_scan_sessions"
helpviewer_keywords:
  - "sys.dm_change_feed_log_scan_sessions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||=azuresqldb-current"
---
# sys.dm_change_feed_log_scan_sessions (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

  Returns activity for the Azure Synapse Link fo SQL change feed. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id** |**int**| ID of the session.<br /><br /> 0 = the data returned in this row is an aggregate of all sessions since the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started. |
|**start_time** |**datetime**| Time the session began.<br /><br /> When `session_id` = 0, the time aggregated data collection began. |
|**end_time** |**datetime**|Time the session ended.<br /><br /> `NULL` = session is active.<br /><br /> When `session_id` = 0, the time the last session ended.|
|**duration** |**int**|The duration (in seconds) of the session.<br /><br /> 0 = the session does not contain change data capture transactions.<br /><br /> When `session_id` = 0, the sum of the duration (in seconds) of all sessions with change feed transactions.|
|**batch_processing_phase** |**nvarchar(200)**| The stage of scan reached in a particular log scan session. The following are the currently implemented phases:<BR>1: Reading configuration<BR>2: First scan, building hash table<BR>3: Second scan<BR>4: Second scan<BR>5: Second scan<BR>6: Schema versioning<BR>7: Last scan, publish and commit.<BR>8: Done|
|**error_count** |**int**|Number of errors encountered.<br /><br /> When `session_id` = 0, the total number of errors in all sessions. |
|**batch_start_lsn** |**nvarchar(23)**|Starting LSN for the session.<br /><br /> When `session_id` = 0, the starting LSN for the last session. |
|**currently_processed_lsn** |**nvarchar(23)**|Current LSN being scanned.<br /><br /> When `session_id` = 0, the current LSN is 0.|
|**batch_end_lsn** |**nvarchar(23)**|Ending LSN for the session.<br /><br /> `NULL` = session is active.<br /><br /> When `session_id` = 0, the ending LSN for the last session.|
|**tran_count** |**bigint**|Number of change data capture transactions processed. This counter is populated in `batch_processing_phase` 2.<br /><br /> When `session_id` = 0, the number of processed transactions in all sessions. |
|**currently_processed_commit_lsn** |**nvarchar(23)**|LSN of the last commit log record processed.<br /><br /> When `session_id` = 0, the last commit log record LSN for any session.|
|**currently_processed_commit_time** |**datetime**|Time the last commit log record was processed.<br /><br /> When `session_id` = 0, the time the last commit log record for any session.|
|**log_record_count** | **bigint**|Number of log records scanned.<br /><br /> When `session_id` = 0, number of records scanned for all sessions.|
|**schema_change_count** |**int**|Number of data definition language (DDL) operations detected. This counter is populated in `batch_processing_phase` 6.<br /><br /> When `session_id` = 0, the number of DDL operations processed in all sessions.|
|**command_count** |**bigint**|Number of commands processed.<br /><br /> When `session_id` = 0, the number of commands processed in all sessions.|
|**latency** | **int**|The difference, in seconds, between `end_time` and `currently_processed_commit_time`, in the session. This counter is populated at the end of `batch_processing_phase` 7.<br /><br /> When `session_id` = 0, the last nonzero latency value recorded by a session.|
|**empty_scan_count** |**int**|Number of consecutive sessions that contained no captured transactions.|
|**failed_sessions_count** |**int**|Number of sessions that failed.|

## Permissions  

Requires VIEW DATABASE STATE permission to query the `sys.dm_change_feed_log_scan_sessions` dynamic management view. For more information about permissions on dynamic management views, see [Dynamic Management Views and Functions](system-dynamic-management-views.md).

## See also  

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [sys.dm_change_feed_errors (Transact-SQL)](sys-dm-change-feed-errors.md)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
