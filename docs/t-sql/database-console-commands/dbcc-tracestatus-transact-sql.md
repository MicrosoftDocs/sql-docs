---
title: "DBCC TRACESTATUS (Transact-SQL)"
description: "DBCC TRACESTATUS (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/17/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC_TRACESTATUS_TSQL"
  - "DBCC TRACESTATUS"
  - "TRACESTATUS_TSQL"
  - "TRACESTATUS"
helpviewer_keywords:
  - "global trace flags [SQL Server]"
  - "status information [SQL Server], trace flags"
  - "trace flags [SQL Server], status information"
  - "DBCC TRACESTATUS statement"
  - "session trace flags [SQL Server]"
  - "displaying trace flag status"
dev_langs:
  - "TSQL"
---
# DBCC TRACESTATUS (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Displays the status of trace flags.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC TRACESTATUS ( [ [ trace# [ ,...n ] ] [ , ] [ -1 ] ] )   
[ WITH NO_INFOMSGS ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*trace#*  
Is the number of the trace flag for which the status is displayed. If *trace#*, and -1 are not specified, all trace flags that are enabled for the session are displayed.
  
*n*  
Is a placeholder that indicates multiple trace flags can be specified.
  
-1  
Displays the status of trace flags that are enabled globally and for the current session.  If -1 is specified without *trace#*, all trace flags including session enabled are returned.
  
WITH NO_INFOMSGS  
Suppresses all informational messages that have severity levels from 0 through 10.
  
## Result Sets  
The following table describes the information in the result set.
  
|Column name|Description|  
|---|---|
|**TraceFlag**|Name of trace flag|  
|**Status**|Indicates whether the trace flag is set ON of OFF, either globally or for the session.<br /><br /> 1 = ON<br /><br /> 0 = OFF|  
|**Global**|Indicates whether the trace flag is set globally<br /><br /> 1 = True<br /><br /> 0 = False|  
|**Session**|Indicates whether the trace flag is set for the session<br /><br /> 1 = True<br /><br /> 0 = False|  
  
DBCC TRACESTATUS returns a column for the trace flag number and a column for the status. This indicates whether the trace flag is ON (1) or OFF (0). The column heading for the trace flag number is either **Global** or **Session**, depending on whether you are checking the status for a global or a session trace flag.
  
## Remarks  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], there are two types of trace flags: session and global. Session trace flags are active for a connection and are visible only for that connection. Global trace flags are set at the server level and are visible to every connection on the server.
  
## Permissions  
Requires membership in the **public** role.
  
## Examples  
The following example displays the status of all trace flags that are currently enabled globally.
  
```sql  
DBCC TRACESTATUS(-1);  
GO  
```  
  
The following example displays the status of trace flags `2528` and `3205`.
  
```sql  
DBCC TRACESTATUS (2528, 3205);  
GO  
```  
  
The following example displays whether trace flag `3205` is enabled for the current session or globally.
  
```sql  
DBCC TRACESTATUS (3205, -1);  
GO  
```  
  
The following example lists all the trace flags that are enabled for the current session.
  
```sql  
DBCC TRACESTATUS();  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[DBCC TRACEOFF &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceoff-transact-sql.md)  
[DBCC TRACEON &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-transact-sql.md)  
[Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
  
  
