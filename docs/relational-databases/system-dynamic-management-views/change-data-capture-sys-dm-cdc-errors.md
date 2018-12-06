---
title: "sys.dm_cdc_errors (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_cdc_errors_TSQL"
  - "dm_cdc_errors"
  - "sys.dm_cdc_errors"
  - "sys.dm_cdc_errors_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_cdc_errors dynamic management view"
  - "change data capture [SQL Server], error reporting"
ms.assetid: 898f2d76-9e63-45ef-94da-8034e86004ab
author: stevestein
ms.author: sstein
manager: craigg
---
# Change Data Capture - sys.dm_cdc_errors
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row for each error encountered during the change data capture log scan session.  
 
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|ID of the session.<br /><br /> 0 = the error did not occur within a log scan session.|  
|**phase_number**|**int**|Number indicating the phase the session was in at the time the error occurred. For a description of each phase, see [sys.dm_cdc_log_scan_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-log-scan-sessions.md).|  
|**entry_time**|**datetime**|The date and time the error was logged. This value corresponds to the timestamp in the SQL error log.|  
|**error_number**|**int**|ID of the error message.|  
|**error_severity**|**int**|Severity level of the message, between 1 and 25.|  
|**error_state**|**int**|State number of the error.|  
|**error_message**|**nvarchar(1024)**|Message text of the error.|  
|**start_lsn**|**nvarchar(23)**|Starting LSN value of the rows being processed when the error occurred.<br /><br /> 0 = the error did not occur within a log scan session.|  
|**begin_lsn**|**nvarchar(23)**|Beginning LSN value of the transaction being processed when the error occurred.<br /><br /> 0 = the error did not occur within a log scan session.|  
|**sequence_value**|**nvarchar(23)**|LSN value of the rows being processed when the error occurred.<br /><br /> 0 = the error did not occur within a log scan session.|  
  
## Remarks  
 **sys.dm_cdc_errors** contains error information for the previous 32 sessions.  
  
## Permissions  
 Requires VIEW DATABASE STATE permission to query the **sys.dm_cdc_errors** dynamic management view. For more information about permissions on dynamic management views, see [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md).  
  
## See Also  
 [sys.dm_cdc_log_scan_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-log-scan-sessions.md)   
 [sys.dm_repl_traninfo &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-repl-traninfo-transact-sql.md)  
  
  

