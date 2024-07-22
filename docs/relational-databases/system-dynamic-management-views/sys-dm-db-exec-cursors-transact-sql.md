---
title: "sys.dm_db_exec_cursors"
titleSuffix: Azure SQL Database
description: Use the dynamic management view sys.dm_db_exec_cursors to return information about open or declared cursors in Azure SQL Database.
author: RPLogan
ms.author: rilogan
ms.date: "07/12/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_exec_cursors_TSQL"
  - "dm_db_exec_cursors"
  - "dm_db_exec_cursors_TSQL"
  - "sys.dm_db_exec_cursors"
helpviewer_keywords:
  - "sys.dm_db_exec_cursors dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.dm_db_exec_cursors (Azure SQL Database)
[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

 Returns information about open or declared cursors in Azure SQL Database. 
  
## Syntax  
  
```syntaxsql
  
dm_db_exec_cursors (session_id | 0 )  
```  
  
## Arguments  

#### *session_id* | 0  
 ID of the session. If `session_id` is specified, this function returns information about cursors in the specified session. The current user must have VIEW DATABASE STATE permission to view cursor information from other sessions.
  
 If `0` is specified in a single database or elastic pool, and the current user has VIEW DATABASE STATE permission, the function returns information about all cursors for all sessions in the _current_ database. Otherwise, without the VIEW DATABASE STATE permission, the function returns information about cursors for only the current session.
  
## Table returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|ID of the session that holds this cursor.|  
|**cursor_id**|**int**|ID of the cursor object.|  
|**name**|**nvarchar(256)**|Name of the cursor as defined by the user.|  
|**properties**|**nvarchar(256)**|Specifies the properties of the cursor. The values of the following properties are concatenated to form the value of this column:<br />`Declaration Interface | Cursor Type | Cursor Concurrency | Cursor scope | (Cursor nesting level)`<br /><br /> For example, the value returned in this column might be `TSQL &#124; Dynamic &#124; Optimistic &#124; Global (0)`.|  
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

Requires VIEW DATABASE STATE permission in the current database to view all declared or open cursors for all sessions in the database.  For the server admin login (set when creating a [logical server in Azure](/azure/azure-sql/database/logical-servers)), the results are still scoped to cursors declared or open in the current database.

## Remarks  

The following table provides information about the cursor declaration interface and includes the possible values for the `properties` column, which contains a pipe-delimited string of information about the cursor object: 
  
|Property|Description|  
|--------------|-----------------|  
|API|Cursor was declared by using one of the data access APIs (ODBC, OLEDB).|  
|TSQL|Cursor was declared by using the Transact-SQL DECLARE CURSOR syntax.|  
  
The following table provides information about the cursor type and includes the possible values for the `properties` column:
  
|Type|Description|  
|----------|-----------------|  
|Keyset|Cursor was declared as Keyset.|  
|Dynamic|Cursor was declared as Dynamic.|  
|Snapshot|Cursor was declared as Snapshot or Static.|  
|Fast_Forward|Cursor was declared as Fast Forward.|  
  
The following table provides information about cursor concurrency and includes the possible values for the `properties` column.
  
|Concurrency|Description|  
|-----------------|-----------------|  
|Read Only|Cursor was declared as read-only.|  
|Scroll Locks|Cursor uses scroll locks.|  
|Optimistic|Cursor uses optimistic concurrency control.|  
  
The following table provides information about cursor concurrency and includes the possible values for the `properties` column:
  
|Scope|Description|  
|-----------|-----------------|  
|Local|Specifies that the scope of the cursor is local to the batch, stored procedure, or trigger in which the cursor was created.|  
|Global|Specifies that the scope of the cursor is global to the connection.|  
  
## Examples  
  
### A. Detect old cursors  

The following example returns information about cursors that have been declared or open in the current database longer than the specified time of 36 hours: 
  
```sql
SELECT creation_time, cursor_id, name, c.session_id, login_name   
FROM sys.dm_db_exec_cursors(0) AS c   
JOIN sys.dm_exec_sessions AS s ON c.session_id = s.session_id   
WHERE DATEDIFF(hh, c.creation_time, GETDATE()) > 36;  
GO  
```

### B. Detect all open cursors  

The following example returns information about cursors that are currently open in the database:
  
```sql
SELECT session_id, creation_time, cursor_id, name
FROM sys.dm_db_exec_cursors(0)
WHERE is_open = 1;
GO 
```   
  
## See also  


 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)  
 [sys.dm_exec_cursors &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cursors-transact-sql.md)
  
  

