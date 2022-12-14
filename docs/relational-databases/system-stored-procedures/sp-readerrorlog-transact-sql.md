---
title: sp_readerrorlog (Transact-SQL)
description: sp_readerrorlog (Transact-SQL)
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_readerrorlog_TSQL"
  - "sp_readerrorlog"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_readerrorlog"
author: pijocoder
ms.author: jopilov
ms.reviewer: ""
ms.custom: ""
ms.date: "02/08/2022"
---
# sp_readerrorlog (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Allows you to read the contents of the SQL Server or SQL Server Agent error log file and filter on keywords.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```tsql
sp_readerrorlog  
	@p1		int = 0,
	@p2		int = NULL,
	@p3		nvarchar(4000) = NULL,
	@p4		nvarchar(4000) = NULL
```  

## Arguments

#### [@p1 = ] 'log_number'

Is the integer (int) value of the log you want to view. The current error log has a value of 0, the previous is 1 (Errorlog.1), the one before previous is 2 (Errorlog.2), and so on.

#### [@p2 = ] 'product ID'

Is the integer (int) value for the product whose log you want to view. Use 1 for SQL Server or 2 SQL Server Agent. If a value isn't specified, the SQL Server product is used

#### [@p3 = ] 'string_to_search'

Is the string value for a string you want to filter on when viewing the error log. This value is **nvarchar(4000)** and has a default of NULL.

#### [@p4 = ] 'string_to_search'

Is the string value for an additional string you want to filter on to further refine the search when viewing the error log. This value is **nvarchar(4000)** and has a default of NULL. This provides an additional filter to the first string search @p3.

## Return Code Values

No return code
  
## Result Sets

Displays the content of the requested error log. If filter strings are used only the lines that match those strings are displayed. 
  
## Remarks

Every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started, the current error log is renamed to **errorlog.1**; **errorlog.1** becomes **errorlog.2**, **errorlog.2** becomes **errorlog.3**, and so on. **sp_readerrorlog** enables you to read any of these error log files as long as the files exist.  
  
## Permissions

Execute permissions for **sp_readerrorlog** are restricted to members of the **sysadmin** fixed server role.  
  
## Examples

The following example cycles the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  

### A. Read the current SQL Server error log

```tsql  
EXEC sp_readerrorlog;  
```  
  
### B. Show the previous SQL Agent error log

```tsql
exec sp_readerrorlog 1, 2;
```

### C. Find log messages that indicate a database is starting up

```tsql
exec sp_readerrorlog 0, 1, 'database', 'start'
```

## See Also

- [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
- [sp_cycle_errorlog &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cycle-errorlog-transact-sql.md)
- [sp_cycle_agent_errorlog &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cycle-agent-errorlog-transact-sql.md)
