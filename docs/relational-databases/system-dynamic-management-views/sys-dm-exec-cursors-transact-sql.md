---
title: "sys.dm_exec_cursors (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_exec_cursors_TSQL"
  - "dm_exec_cursors"
  - "dm_exec_cursors_TSQL"
  - "sys.dm_exec_cursors"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_cursors dynamic management function"
ms.assetid: f520b63c-36af-40f1-bf71-6901d6331d3d
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_exec_cursors (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about the cursors that are open in various databases.  
  
## Syntax  
  
```  
  
dm_exec_cursors (session_id | 0 )  
```  
  
## Arguments  
 *session_id* | 0  
 ID of the session. If *session_id* is specified, this function returns information about cursors in the specified session.  
  
 If 0 is specified, the function returns information about all cursors for all sessions.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|ID of the session that holds this cursor.|  
|**cursor_id**|**int**|ID of the cursor object.|  
|**name**|**nvarchar(256)**|Name of the cursor as defined by the user.|  
|**properties**|**nvarchar(256)**|Specifies the properties of the cursor. The values of the following properties are concatenated to form the value of this column:<br />Declaration Interface<br />Cursor Type <br />Cursor Concurrency<br />Cursor scope<br />Cursor nesting level<br /><br /> For example, the value returned in this column might be "TSQL &#124; Dynamic &#124; Optimistic &#124; Global (0)".|  
|**sql_handle**|**varbinary(64)**|Handle to the text of the batch that declared the cursor.|  
|**statement_start_offset**|**int**|Number of characters into the currently executing batch or stored procedure at which the currently executing statement starts. Can be used together with the **sql_handle**, the **statement_end_offset**, and the [sys.dm_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md) dynamic management function to retrieve the currently executing statement for the request.|  
|**statement_end_offset**|**int**|Number of characters into the currently executing batch or stored procedure at which the currently executing statement ends. Can be used together with the **sql_handle**, the **statement_start_offset**, and the **sys.dm_exec_sql_text** dynamic management function to retrieve the currently executing statement for the request.|  
|**plan_generation_num**|**bigint**|A sequence number that can be used to distinguish between instances of plans after recompilation.|  
|**creation_time**|**datetime**|Timestamp when this cursor was created.|  
|**is_open**|**bit**|Specifies whether the cursor is open.|  
|**is_async_population**|**bit**|Specifies whether the background thread is still asynchronously populating a KEYSET or STATIC cursor.|  
|**is_close_on_commit**|**bit**|Specifies whether the cursor was declared by using CURSOR_CLOSE_ON_COMMIT.<br /><br /> 1 = Cursor will be closed when the transaction ends.|  
|**fetch_status**|**int**|Returns last fetch status of the cursor. This is the last returned @@FETCH_STATUS value.|  
|**fetch_buffer_size**|**int**|Returns information about the size of the fetch buffer.<br /><br /> 1 = Transact-SQL cursors. This can be set to a higher value for API cursors.|  
|**fetch_buffer_start**|**int**|For FAST_FORWARD and DYNAMIC cursors, it returns 0 if the cursor is not open or if it is positioned before the first row. Otherwise, it returns -1.<br /><br /> For STATIC and KEYSET cursors, it returns 0 if the cursor is not open, and -1 if the cursor is positioned beyond the last row.<br /><br /> Otherwise, it returns the row number in which it is positioned.|  
|**ansi_position**|**int**|Cursor position within the fetch buffer.|  
|**worker_time**|**bigint**|Time spent, in microseconds, by the workers executing this cursor.|  
|**reads**|**bigint**|Number of reads performed by the cursor.|  
|**writes**|**bigint**|Number of writes performed by the cursor.|  
|**dormant_duration**|**bigint**|Milliseconds since the last query (open or fetch) on this cursor was started.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Remarks  
 The following table provides information about the cursor declaration interface and includes the possible values for the properties column.  
  
|Property|Description|  
|--------------|-----------------|  
|API|Cursor was declared by using one of the data access APIs (ODBC, OLEDB).|  
|TSQL|Cursor was declared by using the Transact-SQL DECLARE CURSOR syntax.|  
  
 The following table provides information about the cursor type and includes the possible values for the properties column.  
  
|Type|Description|  
|----------|-----------------|  
|Keyset|Cursor was declared as Keyset.|  
|Dynamic|Cursor was declared as Dynamic.|  
|Snapshot|Cursor was declared as Snapshot or Static.|  
|Fast_Forward|Cursor was declared as Fast Forward.|  
  
 The following table provides information about cursor concurrency and includes the possible values for the properties column.  
  
|Concurrency|Description|  
|-----------------|-----------------|  
|Read Only|Cursor was declared as read-only.|  
|Scroll Locks|Cursor uses scroll locks.|  
|Optimistic|Cursor uses optimistic concurrency control.|  
  
 The following table provides information about cursor scope and includes the possible values for the properties column.  
  
|Scope|Description|  
|-----------|-----------------|  
|Local|Specifies that the scope of the cursor is local to the batch, stored procedure, or trigger in which the cursor was created.|  
|Global|Specifies that the scope of the cursor is global to the connection.|  
  
## Examples  
  
### A. Detecting old cursors  
 This example returns information about cursors that have been open on the server longer than the specified time of 36 hours.  
  
```  
SELECT creation_time, cursor_id, name, c.session_id, login_name   
FROM sys.dm_exec_cursors(0) AS c   
JOIN sys.dm_exec_sessions AS s ON c.session_id = s.session_id   
WHERE DATEDIFF(hh, c.creation_time, GETDATE()) > 36;  
GO  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)  
  
  

