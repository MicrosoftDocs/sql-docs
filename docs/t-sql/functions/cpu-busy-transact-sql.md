---
title: CPU_BUSY (Transact-SQL)
description: "&#x40;&#x40;CPU_BUSY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@CPU_BUSY_TSQL"
  - "@@CPU_BUSY"
helpviewer_keywords:
  - "CPU [SQL Server]"
  - "status information [SQL Server], CPU"
  - "ticks [SQL Server]"
  - "time [SQL Server], CPU activity"
  - "@@CPU_BUSY function"
  - "statistical information [SQL Server], CPU"
  - "CPU [SQL Server], activity"
dev_langs:
  - "TSQL"
---

# &#x40;&#x40;CPU_BUSY (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function returns the amount of time that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has spent in active operation since its latest start. `@@CPU_BUSY` returns a result measured in CPU time increments, or "ticks." This value is cumulative for all CPUs, so it may exceed the actual elapsed time. To convert to microseconds, multiply by [@@TIMETICKS](./timeticks-transact-sql.md).
  
> [!NOTE]  
>  If the time returned in @@CPU_BUSY or @@IO_BUSY exceeds 49 days (approximately) of cumulative CPU time, you may receive an arithmetic overflow warning. In that case, the value of the `@@CPU_BUSY`, `@@IO_BUSY` and `@@IDLE` variables are not accurate.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
@@CPU_BUSY  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]


## Return types
**integer**
  
## Remarks  
To see a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including CPU activity, run [sp_monitor](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md).
  
## Examples  
This example returns [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CPU activity, as of the current date and time. The example converts one of the values to the `float` data type. This avoids arithmetic overflow issues when calculating a value in microseconds.
  
```sql
SELECT @@CPU_BUSY * CAST(@@TIMETICKS AS FLOAT) AS 'CPU microseconds',   
   GETDATE() AS 'As of' ;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
CPU microseconds As of
---------------- -----------------------
18406250         2006-12-05 17:00:50.600
```
  
## See also
[sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md)  
[@@IDLE &#40;Transact-SQL&#41;](../../t-sql/functions/idle-transact-sql.md)  
[@@IO_BUSY &#40;Transact-SQL&#41;](../../t-sql/functions/io-busy-transact-sql.md)  
[sp_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)  
[System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)
  
  
