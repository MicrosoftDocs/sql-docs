---
title: "@@IDLE (Transact-SQL)"
description: "&#x40;&#x40;IDLE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@IDLE_TSQL"
  - "@@IDLE"
helpviewer_keywords:
  - "time [SQL Server], idle"
  - "ticks [SQL Server]"
  - "@@IDLE function"
  - "status information [SQL Server], idle time"
  - "idle time [SQL Server]"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;IDLE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the time that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has been idle since it was last started. The result is in CPU time increments, or "ticks," and is cumulative for all CPUs, so it may exceed the actual elapsed time. Multiply by @@TIMETICKS to convert to microseconds.  
  
> [!NOTE]  
>  If the time returned in @@CPU_BUSY, or @@IO_BUSY exceeds approximately 49 days of cumulative CPU time, you receive an arithmetic overflow warning. In that case, the value of @@CPU_BUSY, @@IO_BUSY and @@IDLE variables are not accurate.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
@@IDLE  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **integer**  
  
## Remarks  
 To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, run **sp_monitor**.  
  
## Examples  
 The following example shows returning the number of milliseconds [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was idle between the start time and the current time. To avoid arithmetic overflow when converting the value to microseconds, the example converts one of the values to the `float` data type.  
  
```sql  
SELECT @@IDLE * CAST(@@TIMETICKS AS float) AS 'Idle microseconds',  
   GETDATE() AS 'as of';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
I  
Idle microseconds  as of                   
----------------- ----------------------  
8199934           12/5/2006 10:23:00 AM   
```  
  
## See Also  
 [@@CPU_BUSY &#40;Transact-SQL&#41;](../../t-sql/functions/cpu-busy-transact-sql.md)   
 [sp_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)   
 [@@IO_BUSY &#40;Transact-SQL&#41;](../../t-sql/functions/io-busy-transact-sql.md)   
 [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
  
  
