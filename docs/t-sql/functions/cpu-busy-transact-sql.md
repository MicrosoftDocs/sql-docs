---
title: "@@CPU_BUSY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@CPU_BUSY_TSQL"
  - "@@CPU_BUSY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CPU [SQL Server]"
  - "status information [SQL Server], CPU"
  - "ticks [SQL Server]"
  - "time [SQL Server], CPU activity"
  - "@@CPU_BUSY function"
  - "statistical information [SQL Server], CPU"
  - "CPU [SQL Server], activity"
ms.assetid: 81ae0e64-79fa-4a74-9aa5-37045c4cd211
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;CPU_BUSY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

This function returns the amount of time that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has spent in active operation since its latest start. `@@CPU_BUSY` returns a result measured in CPU time increments, or "ticks." This value is cumulative for all CPUs, so it may exceed the actual elapsed time. To convert to microseconds, multiply by [@@TIMETICKS](./timeticks-transact-sql.md).
  
> [!NOTE]  
>  If the time returned in @@CPU_BUSY or @@IO_BUSY exceeds 49 days (approximately) of cumulative CPU time, you may receive an arithmetic overflow warning. In that case, the value of the `@@CPU_BUSY`, `@@IO_BUSY` and `@@IDLE` variables are not accurate.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```
@@CPU_BUSY  
```  
  
## Return types
**integer**
  
## Remarks  
To see a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including CPU activity, run [sp_monitor](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md).
  
## Examples  
This example returns [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CPU activity, as of the current date and time. The example converts one of the values to the `float` data type. This avoids arithmetic overflow issues when calculating a value in microseconds.
  
```sql
SELECT @@CPU_BUSY * CAST(@@TIMETICKS AS float) AS 'CPU microseconds',   
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
  
  
