---
title: "sys.dm_pdw_exec_sessions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 31c262b3-7e4d-44c4-af71-aaef0fd1a980
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.dm_pdw_exec_sessions (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Holds information about all sessions currently or recently open on the appliance. It lists one row per session.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|session_id|**nvarchar(32)**|The id of the current query or the last query run (if the session is TERMINATED and the query was executing at time of termination). Key for this view.|Unique across all sessions in the system.|  
|status|**nvarchar(10)**|For current sessions, identifies whether the session is currently active or idle. For past sessions, the session status may show closed or killed (if the session was forcibly closed).|'ACTIVE', 'CLOSED', 'IDLE', 'TERMINATED'|  
|request_id|**nvarchar(32)**|The id of the current query or last query run.|Unique across all requests in the system. Null if none has been run.|  
|security_id|**varbinary(85)**|Security ID of the principal running the session.||  
|login_name|**nvarchar(128)**|The login name of the principal running the session.|Any string conforming to the user naming conventions.|  
|login_time|**datetime**|Date and time at which the user logged in and this session was created.|Valid **datetime** before current time.|  
|query_count|**int**|Captures the number of queries/requeststhis session has run since creation.|Greater than or equal to 0.|  
|is_transactional|**bit**|Captures whether a session is currently within a transaction or not.|0 for auto-commit, 1 for transactional.|  
|client_id|**nvarchar(255)**|Captures client information for the session.|Any valid string.|  
|app_name|**nvarchar(255)**|Captures application name information optionally set as part of the connection process.|Any valid string.|  
|sql_spid|**int**|The id number of the SPID. Use the `session_id` this session. Use the `sql_spid` column to join to **sys.dm_pdw_nodes_exec_sessions**.<br /><br /> **\*\* Warning \*\*** This column contains closed SPIDs.||  
  
 For information about the maximum rows retained by this view, see the Maximum System View Values section in the [Minimum and Maximum Values (SQL Server PDW)](https://msdn.microsoft.com/5243f018-2713-45e3-9b61-39b2a57401b9) topic.  
  
## Permissions  
 Requires the `VIEW SERVER STATE` permission.  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
