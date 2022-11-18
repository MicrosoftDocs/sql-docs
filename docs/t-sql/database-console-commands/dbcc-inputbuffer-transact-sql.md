---
title: "DBCC INPUTBUFFER (Transact-SQL)"
description: "DBCC INPUTBUFFER (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/04/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC INPUTBUFFER"
  - "INPUTBUFFER"
  - "DBCC_INPUTBUFFER_TSQL"
  - "INPUTBUFFER_TSQL"
helpviewer_keywords:
  - "input buffers [SQL Server]"
  - "last statement from client"
  - "displaying last statement sent"
  - "statements [SQL Server], last statement"
  - "DBCC INPUTBUFFER statement"
dev_langs:
  - "TSQL"
---
# DBCC INPUTBUFFER (Transact-SQL)
[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Displays the last statement sent from a client to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC INPUTBUFFER ( session_id [ , request_id ])  
[WITH NO_INFOMSGS ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*session_id*  
Is the session ID associated with each active primary connection.  
  
*request_id*  
Is the exact request (batch) to search for within the current session.  

The following query returns *request_id*:  
```sql
SELECT request_id   
FROM sys.dm_exec_requests   
WHERE session_id = @@spid;  
```  
WITH  
Enables options to be specified.  
  
NO_INFOMSGS  
Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Result Sets  
DBCC INPUTBUFFER returns a rowset with the following columns.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**EventType**|**nvarchar(30)**|Event type. This could be **RPC Event** or **Language Event**. The output will be **No Event** when no last event was detected.|  
|**Parameters**|**smallint**|0 = Text<br /><br /> 1- *n* = Parameters|  
|**EventInfo**|**nvarchar(4000)**|For an **EventType** of RPC, **EventInfo** contains only the procedure name. For an **EventType** of Language, only the first 4000 characters of the event are displayed.|  
  
For example, DBCC INPUTBUFFER returns the following result set when the last event in the buffer is DBCC INPUTBUFFER(11).
  
```
EventType      Parameters EventInfo               
-------------- ---------- ---------------------   
Language Event 0          DBCC INPUTBUFFER (11)  
  
(1 row(s) affected)  
  
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  

> [!NOTE]
> Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2, use [sys.dm_exec_input_buffer](../../relational-databases/system-dynamic-management-views/sys-dm-exec-input-buffer-transact-sql.md) to return information about statements submitted to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Permissions  
On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires the VIEW SERVER STATE permission or membership in the **sysadmin** fixed server role.

Without any of these, users can only view the inputbuffer of their own session. That means the *session_id* must be the same as the session ID on which the command is being run. To determine the session ID execute the following query:  
  
```sql
SELECT @@spid;  
```
  
On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Premium and Business Critical tiers requires the VIEW DATABASE STATE permission in the database. On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Standard, Basic, and General Purpose tiers requires the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] admin account.
  
## Examples  
The following example runs `DBCC INPUTBUFFER` on a second connection while a long transaction is running on a previous connection.
  
```sql
CREATE TABLE dbo.T1 (Col1 INT, Col2 CHAR(3));  
GO  
DECLARE @i INT = 0;  
BEGIN TRAN  
SET @i = 0;  
WHILE (@i < 100000)  
BEGIN  
INSERT INTO dbo.T1 VALUES (@i, CAST(@i AS CHAR(3)));  
SET @i += 1;  
END;  
COMMIT TRAN;  
--Start new connection #2.  
DBCC INPUTBUFFER (52);  
```  

## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)  
[sys.dm_exec_input_buffer &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-input-buffer-transact-sql.md)
  
  
