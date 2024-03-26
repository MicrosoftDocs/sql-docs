---
title: "sys.dm_change_feed_log_scan_sessions (Transact-SQL)"
description: sys.dm_change_feed_log_scan_sessions (Transact-SQL) shows activity for the Azure Synapse Link or Fabric Mirrored Database feature.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/18/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_change_feed_log_scan_sessions_TSQL"
  - "sys.dm_change_feed_log_scan_sessions"
  - "dm_change_feed_log_scan_sessions_TSQL"
  - "dm_change_feed_log_scan_sessions"
helpviewer_keywords:
  - "sys.dm_change_feed_log_scan_sessions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||=azuresqldb-current||=fabric||=azure-sqldw-latest"
---
# sys.dm_change_feed_log_scan_sessions (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asa-fabric](../../includes/applies-to-version/sqlserver2022-asdb-asa-fabric.md)]

Returns activity from the SQL change feed.

This dynamic management view is used for:

- The Azure Synapse Link feature for SQL Server instances and Azure SQL Database. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).
- The Fabric Mirrored Database feature for Azure SQL Database. For more information, see [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
| `session_id` |**int**| ID of the session.<br /><br /> 0 = the data returned in this row is an aggregate of all sessions since the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] was last started. |
| `start_time` |**datetime**| Time the session began.<br /><br /> When `session_id` = 0, the time aggregated data collection began. |
| `end_time` |**datetime**|Time the session ended.<br /><br /> `NULL` = session is active.<br /> When `session_id` = 0, the time the last session ended.|
| `duration` |**int**|The duration (in seconds) of the session.<br /><br /> 0 = the session does not contain change data capture transactions.<br /><br /> When `session_id` = 0, the sum of the duration (in seconds) of all sessions with change feed transactions.|
| `batch_processing_phase` |**nvarchar(200)**| The stage of scan reached in a particular log scan session. The following are the currently implemented phases:<br />1: Reading configuration<br />2: First scan, building hash table<br />3: Second scan<br />4: Second scan<br />5: Second scan<br />6: Schema versioning<br />7: Last scan, publish and commit.<br />8: Done|
| `error_count` |**int**|Number of errors encountered.<br /><br /> When `session_id` = 0, the total number of errors in all sessions. |
| `batch_start_lsn` |**nvarchar(23)**|Starting LSN for the session.<br /><br /> When `session_id` = 0, the starting LSN for the last session. |
| `currently_processed_lsn` |**nvarchar(23)**|Current LSN being scanned.<br /><br /> When `session_id` = 0, the current LSN is 0.|
| `batch_end_lsn` |**nvarchar(23)**|Ending LSN for the session.<br /><br /> `NULL` = session is active.<br /><br />When `session_id` = 0, the ending LSN for the last session.|
| `tran_count` |**bigint**|Number of change data capture transactions processed. This counter is populated in `batch_processing_phase` 2.<br /><br /> When `session_id` = 0, the number of processed transactions in all sessions. |
| `currently_processed_commit_lsn` |**nvarchar(23)**|LSN of the last commit log record processed.<br /><br /> When `session_id` = 0, the last commit log record LSN for any session.|
| `currently_processed_commit_time` |**datetime**|Time the last commit log record was processed.<br /><br /> When `session_id` = 0, the time the last commit log record for any session.|
| `log_record_count` | **bigint**|Number of log records scanned.<br /><br /> When `session_id` = 0, number of records scanned for all sessions.|
| `schema_change_count` |**int**|Number of data definition language (DDL) operations detected. This counter is populated in `batch_processing_phase` 6.<br /><br /> When `session_id` = 0, the number of DDL operations processed in all sessions.|
| `command_count` |**bigint**|Number of commands processed.<br /><br /> When `session_id` = 0, the number of commands processed in all sessions.|
| `latency` | **int**|The difference, in seconds, between `end_time` and `currently_processed_commit_time`, in the session. This counter is populated at the end of `batch_processing_phase` 7.<br /><br /> When `session_id` = 0, the last nonzero latency value recorded by a session.|
| `empty_scan_count` |**int**|Number of consecutive sessions that contained no captured transactions.|
| `failed_sessions_count` |**int**|Number of sessions that failed.|

## Permissions

Requires VIEW DATABASE STATE or VIEW DATABASE PERFORMANCE STATE permission to query the `sys.dm_change_feed_log_scan_sessions` dynamic management view. For more information about permissions on dynamic management views, see [Dynamic Management Views and Functions](system-dynamic-management-views.md).

## Related content

- [sys.dm_change_feed_errors (Transact-SQL)](sys-dm-change-feed-errors.md)
- [sys.sp_help_change_feed (Transact-SQL)](../system-stored-procedures/sp-help-change-feed.md)

**For Microsoft Fabric mirrored databases**:

- [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview)
- [Microsoft Fabric mirrored databases monitoring](/fabric/database/mirrored-database/monitor)
- [Explore data in your Mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)

**For Azure Synapse Link**:

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Troubleshoot: Azure Synapse Link for SQL initial snapshot issues](/azure/synapse-analytics/synapse-link/troubleshoot/troubleshoot-sql-snapshot-issues)
