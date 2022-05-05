---
description: "sys.dm_change_feed_log_scan_sessions (Transact-SQL) shows activity for the Azure Synapse Link feature."
title: "sys.dm_change_feed_log_scan_sessions (Transact-SQL)"
ms.custom: ""
ms.date: "05/24/2022"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.dm_change_feed_log_scan_sessions_TSQL"
  - "sys.dm_change_feed_log_scan_sessions"
  - "dm_change_feed_log_scan_sessions_TSQL"
  - "dm_change_feed_log_scan_sessions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_change_feed_log_scan_sessions dynamic management view"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sys.dm_change_feed_log_scan_sessions (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

  Returns activity for the Azure Synapse Link feature. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id** | | This is the `session_id` maintained by history cache of the logreader. |
|**start_time** | | Time when the log scan, publish, and commit starts. |
|**end_time** | | Time when the history session ends. |
|**duration** | | The amount of time in seconds the log scan, publish, and commit remains open for. |
|**batch_processing_phase** | | The stage of scan reached in a particular log scan session. The following are the currently implemented phases:<BR>1: Reading configuration<BR>2: First scan, building hash table<BR>3: Second scan<BR>4: Second scan<BR>5: Second scan<BR>6: Schema versioning<BR>7: Last scan, publish and commit.<BR>8: Done|
|**error_count** |  | The number of errors in particular scan, publish and commit. |
|**batch_start_lsn** |  | The starting LSN for the session. |
|**currently_processed_lsn** |  | |
|**batch_end_lsn** |  | The `current_lsn` being scanned. |
|**tran_count** |  | Command ID from the change row that failed to publish. Will be `NULL` for snapshot errors. |
|**currently_processed_commit_lsn** |  | The `current_lsn` being scanned. |
|**currently_processed_commit_time** |  | The `current_lsn` being scanned commit time. |
|**Log_record_count** |  | Number of physical log records processed in the batch so far. |
|**schema_change_count** |  | Number of data definition language in the batch. |
|**command_count** |  | Number of commands processed. |
|**latency** |  | The difference, between current time and `currently_processed_commit_time`. Only updated at phase 8 after processing the entire batch.|
|**empty_scan_count** |  | |
|**is_session_failed** |  | |

## Permissions  

A user must have the VIEW PERFORMANCESTATE permission.

## See also  

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [sys.dm_change_feed_errors (Transact-SQL)](sys-dm-change-feed-errors.md)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)