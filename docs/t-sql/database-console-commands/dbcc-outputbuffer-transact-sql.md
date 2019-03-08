---
title: "DBCC OUTPUTBUFFER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/16/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DBCC OUTPUTBUFFER"
  - "OUTPUTBUFFER_TSQL"
  - "OUTPUTBUFFER"
  - "DBCC_OUTPUTBUFFER_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DBCC OUTPUTBUFFER statement"
  - "output buffers"
  - "current output buffer"
ms.assetid: e912a06d-9fde-4e26-b057-801255d79504
author: pmasl
ms.author: umajay
manager: craigg
---
# DBCC OUTPUTBUFFER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

Returns the current output buffer in hexadecimal and ASCII format for the specified *session_id*.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
```sql
DBCC OUTPUTBUFFER ( session_id [ , request_id ])  
[ WITH NO_INFOMSGS ]  
```  
  
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
 Allows for options to be specified.  
  
 NO_INFOMSGS  
 Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Remarks  
DBCC OUTPUTBUFFER displays the results sent to the specified client (*session_id*). For processes that do not contain output streams, an error message is returned.
  
To show the statement executed that returned the results displayed by DBCC OUTPUTBUFFER, execute DBCC INPUTBUFFER.
  
## Result Sets  
DBCC OUTPUTBUFFER returns the following (values may vary):
  
```sql
Output Buffer                                                              
------------------------------------------------------------------------   
01fb8028:  04 00 01 5f 00 00 00 00 e3 1b 00 01 06 6d 00 61  ..._.........m.a  
01fb8038:  00 73 00 74 00 65 00 72 00 06 6d 00 61 00 73 00  .s.t.e.r..m.a.s.  
'...'  
01fb8218:  04 17 00 00 00 00 00 d1 04 18 00 00 00 00 00 d1  ................  
01fb8228:   .  
  
(33 row(s) affected)  
  
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  
Requires membership in the **sysadmin** fixed server role.
  
## Examples  
The following example returns current output buffer information for an assumed session ID of `52`.
  
```sql
DBCC OUTPUTBUFFER (52);  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)  
[Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
  
  
