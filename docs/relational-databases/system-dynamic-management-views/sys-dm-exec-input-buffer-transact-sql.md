---
title: "sys.dm_exec_input_buffer (Transact-SQL)"
description: sys.dm_exec_input_buffer (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/14/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_exec_input_buffer"
  - "sys.dm_exec_input_buffer _tsql"
  - "dm_exec_input_buffer"
  - "dm_exec_input_buffer_tsql"
helpviewer_keywords:
  - "sys.dm_exec_input_buffer dynamic management function"
dev_langs:
  - "TSQL"
ms.assetid: fb34a560-bde9-4ad9-aa96-0d4baa4fc104
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_input_buffer (Transact-SQL)

[!INCLUDE[tsql-appliesto-2014sp2-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md)]

Returns information about statements submitted to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Syntax

```
sys.dm_exec_input_buffer ( session_id , request_id )
```

## Arguments

*session_id*
Is the session ID executing the batch to be looked up. *session_id* is **smallint**. *session_id* can be obtained from the following dynamic management objects:

- [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)
- [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)
- [sys.dm_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql.md)

*request_id*
The request_id from [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md). *request_id* is **int**.

## Table Returned

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**event_type**|**nvarchar(256)**|The type of event in the input buffer for the given spid.|
|**parameters**|**smallint**|Any parameters provided for the statement.|
|**event_info**|**nvarchar(max)**|The text of the statement in the input buffer for the given spid.|

## Permissions

On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the user has VIEW SERVER STATE permission, the user will see all executing sessions on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; otherwise, the user will see only the current session.

> [!IMPORTANT]
> Running this DMV outside of SQL Server Management Studio against SQL Server without VIEW SERVER STATE permissions (such as in a trigger, stored procedure, or function) throws a permission error on the master database.

On [!INCLUDE[ssSDS](../../includes/sssds-md.md)], if the user is the database owner, the user will see all executing sessions on the [!INCLUDE[ssSDS](../../includes/sssds-md.md)]; otherwise, the user will see only the current session.

> [!IMPORTANT]
> Running this DMV outside of SQL Server Management Studio against Azure SQL Database without owner permissions (such as in a trigger, stored procedure, or function) throws a permission error on the master database.

## Remarks

This dynamic management function can be used in conjunction with sys.dm_exec_sessions or sys.dm_exec_requests by doing **CROSS APPLY**.

## Examples

### A. Simple example

The following example demonstrates passing a session ID (SPID) and a request ID to the function.

```sql
SELECT * FROM sys.dm_exec_input_buffer (52, 0);
GO
```

### B. Using cross apply to additional information

The following example lists the input buffer for user sessions.

```sql
SELECT es.session_id, ib.event_info
FROM sys.dm_exec_sessions AS es
CROSS APPLY sys.dm_exec_input_buffer(es.session_id, NULL) AS ib
WHERE es.is_user_process = 1;
GO
```

## See Also

- [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)
- [sys.dm_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)
- [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)
- [DBCC INPUTBUFFER &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-inputbuffer-transact-sql.md)
