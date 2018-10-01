---
title: "DBCC TRACEOFF (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "TRACEOFF_TSQL"
  - "TRACEOFF"
  - "DBCC TRACEOFF"
  - "DBCC_TRACEOFF_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "trace flags [SQL Server], disabling"
  - "DBCC TRACEOFF statement"
  - "disabling trace flags"
ms.assetid: 1379afba-6480-454b-9c65-5e64cb4f3415
author: uc-msft
ms.author: umajay
manager: craigg
---
# DBCC TRACEOFF (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

Disables the specified trace flags.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql
DBCC TRACEOFF ( trace# [ ,...n ] [ , -1 ] ) [ WITH NO_INFOMSGS ]  
```  
  
## Arguments  
*trace#*  
Is the number of the trace flag to disable.  
  
**-1**  
Disables the specified trace flags globally.  
  
WITH NO_INFOMSGS  
Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Remarks  
Trace flags are used to customize certain characteristics controlling how the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] operates.
  
## Result Sets  
DBCC TRACEOFF returns:
  
```sql
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  
Requires membership in the **sysadmin** fixed server role.
  
## Examples  
The following example disables trace flag `3205`.
  
```sql
DBCC TRACEOFF (3205);   
GO  
```  
  
The following example first disables trace flag `3205` globally
  
```sql
DBCC TRACEOFF (3205, -1);   
GO  
```  
  
The following example disables trace flags `3205` and `260` globally.
  
```sql
DBCC TRACEOFF (3205, 260, -1);  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[DBCC TRACEON &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-transact-sql.md)  
[DBCC TRACESTATUS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-tracestatus-transact-sql.md)  
[Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
  
  
