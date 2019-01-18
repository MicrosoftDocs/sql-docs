---
title: "sys.dm_cdc_log_scan_sessions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_cdc_log_scan_sessions"
  - "dm_cdc_log_scan_sessions_TSQL"
  - "sys.dm_cdc_log_scan_sessions_TSQL"
  - "sys.dm_cdc_log_scan_sessions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "change data capture [SQL Server], log scan reporting"
  - "sys.dm_cdc_log_scan_sessions dynamic management view"
ms.assetid: d337e9d0-78b1-4a07-8820-2027d0b9f87c
author: stevestein
ms.author: sstein
manager: craigg
---
# Change Data Capture - sys.dm_cdc_log_scan_sessions
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row for each log scan session in the current database. The last row returned represents the current session. You can use this view to return status information about the current log scan session, or aggregated information about all sessions since the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|ID of the session.<br /><br /> 0 = the data returned in this row is an aggregate of all sessions since the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started.|  
|**start_time**|**datetime**|Time the session began.<br /><br /> When **session_id** = 0, the time aggregated data collection began.|  
|**end_time**|**datetime**|Time the session ended.<br /><br /> NULL = session is active.<br /><br /> When **session_id** = 0, the time the last session ended.|  
|**duration**|**bigint**|The duration (in seconds) of the session.<br /><br /> 0 = the session does not contain change data capture transactions.<br /><br /> When **session_id** = 0, the sum of the duration (in seconds) of all sessions with change data capture transactions.|  
|**scan_phase**|**nvarchar(200)**|The current phase of the session. The following are the possible values and their descriptions:<br /><br /> 1: Reading configuration<br />2: First scan, building hash table<br />3: Second scan<br />4: Second scan<br />5: Second scan<br />6: Schema versioning<br />7: Last scan<br />8: Done<br /><br /> When **session_id** = 0, this value is always "Aggregate".|  
|**error_count**|**int**|Number of errors encountered.<br /><br /> When **session_id** = 0, the total number of errors in all sessions.|  
|**start_lsn**|**nvarchar(23)**|Starting LSN for the session.<br /><br /> When **session_id** = 0, the starting LSN for the last session.|  
|**current_lsn**|**nvarchar(23)**|Current LSN being scanned.<br /><br /> When **session_id** = 0, the current LSN is 0.|  
|**end_lsn**|**nvarchar(23)**|Ending LSN for the session.<br /><br /> NULL = session is active.<br /><br /> When **session_id** = 0, the ending LSN for the last session.|  
|**tran_count**|**bigint**|Number of change data capture transactions processed. This counter is populated in phase 2.<br /><br /> When **session_id** = 0, the number of processed transactions in all sessions.|  
|**last_commit_lsn**|**nvarchar(23)**|LSN of the last commit log record processed.<br /><br /> When **session_id** = 0, the last commit log record LSN for any session.|  
|**last_commit_time**|**datetime**|Time the last commit log record was processed.<br /><br /> When **session_id** = 0, the time the last commit log record for any session.|  
|**log_record_count**|**bigint**|Number of log records scanned.<br /><br /> When **session_id** = 0, number of records scanned for all sessions.|  
|**schema_change_count**|**int**|Number of data definition language (DDL) operations detected. This counter is populated in phase 6.<br /><br /> When **session_id** = 0, the number of DDL operations processed in all sessions.|  
|**command_count**|**bigint**|Number of commands processed.<br /><br /> When **session_id** = 0, the number of commands processed in all sessions.|  
|**first_begin_cdc_lsn**|**nvarchar(23)**|First LSN that contained change data capture transactions.<br /><br /> When **session_id** = 0, the first LSN that contained change data capture transactions.|  
|**last_commit_cdc_lsn**|**nvarchar(23)**|LSN of the last commit log record that contained change data capture transactions.<br /><br /> When **session_id** = 0, the last commit log record LSN for any session that contained change data capture transactions|  
|**last_commit_cdc_time**|**datetime**|Time the last commit log record was processed that contained change data capture transactions.<br /><br /> When **session_id** = 0, the time the last commit log record for any session that contained change data capture transactions.|  
|**latency**|**int**|The difference, in seconds, between **end_time** and **last_commit_cdc_time** in the session. This counter is populated at the end of phase 7.<br /><br /> When **session_id** = 0, the last nonzero latency value recorded by a session.|  
|**empty_scan_count**|**int**|Number of consecutive sessions that contained no change data capture transactions.|  
|**failed_sessions_count**|**int**|Number of sessions that failed.|  
  
## Remarks  
 The values in this dynamic management view are reset whenever the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started.  
  
## Permissions  
 Requires VIEW DATABASE STATE permission to query the **sys.dm_cdc_log_scan_sessions** dynamic management view. For more information about permissions on dynamic management views, see [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md).  
  
## Examples  
 The following example returns information for the most current session.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT session_id, start_time, end_time, duration, scan_phase  
    error_count, start_lsn, current_lsn, end_lsn, tran_count  
    last_commit_lsn, last_commit_time, log_record_count, schema_change_count  
    command_count, first_begin_cdc_lsn, last_commit_cdc_lsn,   
    last_commit_cdc_time, latency, empty_scan_count, failed_sessions_count  
FROM sys.dm_cdc_log_scan_sessions  
WHERE session_id = (SELECT MAX(b.session_id) FROM sys.dm_cdc_log_scan_sessions AS b);  
GO  
```  
  
## See Also  
 [sys.dm_cdc_errors &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md)  
  
  

