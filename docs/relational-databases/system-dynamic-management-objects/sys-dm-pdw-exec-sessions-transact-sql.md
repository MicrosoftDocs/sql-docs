---
title: "sys.dm_pdw_exec_sessions (Transact-SQL)"
description: The sys.dm_pdw_exec_sessions DMV returns information about all sessions currently or recently open, listing one row per session.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 03/27/2025
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest"
---
# sys.dm_pdw_exec_sessions (Transact-SQL)

[!INCLUDE [asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Holds information about all sessions currently or recently open. The `sys.dm_pdw_exec_sessions` dynamic management view (DMV) lists one row per session.

|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
| `session_id` |**nvarchar(32)**| The ID of the current query or the last query run (if the session is TERMINATED and the query was executing at time of termination). Key for this view.|Unique across all sessions in the system.|  
| `status` |**nvarchar(10)**|For current sessions, identifies whether the session is currently active or idle. For past sessions, the session status might show closed or killed (if the session was forcibly closed).|`ACTIVE`, `CLOSED`, `IDLE`, `TERMINATED`|  
| `request_id` |**nvarchar(32)**|The ID of the current query or last query run.|Unique across all requests in the system. `NULL` if none has been run.|  
| `security_id` |**varbinary(85)**|Security ID of the principal running the session.||  
| `login_name` |**nvarchar(128)**|The login name of the principal running the session.|Any string conforming to the user naming conventions.|  
| `login_time` |**datetime**|Date and time at which the user logged in and this session was created.|Valid **datetime** before current time.|  
| `query_count` |**int**|Captures the number of queries/requests this session has run since creation.|Greater than or equal to 0.|  
| `is_transactional` |**bit**|Captures whether a session is currently within a transaction or not.|`0` for auto-commit, `1` for transactional.|  
| `client_id` |**nvarchar(255)**|Captures client information for the session. IPv6 address indicates private endpoint is used.|Any valid string.|  
| `app_name` |**nvarchar(255)**|Captures application name information optionally set as part of the connection process.|Any valid string.|  
| `sql_spid` |**int**|The IDs column contains closed SPIDs.||  

 For information about the maximum rows retained by this view, see [Capacity limits Metadata](/azure/sql-data-warehouse/sql-data-warehouse-service-capacity-limits#metadata).  

## Permissions

 Requires the `VIEW SERVER STATE` permission.  

## Remarks

[!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] For serverless SQL pool use [sys.dm_exec_sessions](sys-dm-exec-sessions-transact-sql.md).

## Examples

### A. Active sessions

To find a count of active sessions:

```sql
SELECT active_count = COUNT(session_Id)
FROM sys.dm_pdw_exec_sessions
WHERE status = 'ACTIVE';
```

### B. Open sessions for more than 10 minutes

To find sessions that have been open for longer than 10 minutes:

```sql
SELECT *, session_duration_s = DATEDIFF (s, login_time, getdate() ) 
 FROM sys.dm_pdw_exec_sessions
 WHERE DATEDIFF (s, login_time, getdate() ) > 600; -- 10 minutes
```

### C. Display session applications/users currently connected

To find session information including source application, who is connected in, where the connection is coming from, and if its using Microsoft Entra ID or SQL authentication.

```sql
SELECT DISTINCT CASE 
        WHEN len(tt) = 0
            THEN app_name
        ELSE tt
        END AS application_name
    ,login_name
    ,ip_address
FROM (
    SELECT DISTINCT app_name
        ,substring(client_id, 0, CHARINDEX(':', ISNULL(client_id, '0.0.0.0:123'))) AS ip_address
        ,login_name
        ,isnull(substring(app_name, 0, CHARINDEX('-', ISNULL(app_name, '-'))), 'h') AS tt
    FROM sys.dm_pdw_exec_sessions
    ) AS a;
```    

## Related content

- [SQL and Parallel Data Warehouse Dynamic Management Views](sql-and-parallel-data-warehouse-dynamic-management-views.md)
