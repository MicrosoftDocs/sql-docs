---
title: "@@IO_BUSY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@IO_BUSY"
  - "@@IO_BUSY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ticks [SQL Server]"
  - "I/O [SQL Server], time spent performing operations"
  - "@@IO_BUSY function"
  - "output operations [SQL Server]"
  - "input operations [SQL Server]"
  - "time [SQL Server], I/O operations"
ms.assetid: 3c26770c-41ae-4e34-8c82-7bef920ffbca
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;IO_BUSY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the time that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has spent performing input and output operations since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started. The result is in CPU time increments ("ticks"), and is cumulative for all CPUs, so it may exceed the actual elapsed time. Multiply by @@TIMETICKS to convert to microseconds.  
  
> [!NOTE]  
>  If the time returned in @@CPU_BUSY, or @@IO_BUSY exceeds approximately 49 days of cumulative CPU time, you receive an arithmetic overflow warning. In that case, the value of @@CPU_BUSY, @@IO_BUSY and @@IDLE variables are not accurate.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@IO_BUSY  
```  
  
## Return Types  
 **integer**  
  
## Remarks  
 To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, run sp_monitor.  
  
## Examples  
 The following example shows returning the number of milliseconds [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has spent performing input/output operations between the start time and the current time. To avoid arithmetic overflow when converting the value to microseconds, the example converts one of the values to the **float** data type.  
  
```  
SELECT @@IO_BUSY*@@TIMETICKS AS 'IO microseconds',   
   GETDATE() AS 'as of';  
```  
  
 Here is a typical result set:  
  
```  
  
IO microseconds as of                   
--------------- ----------------------  
4552312500      12/5/2006 10:23:00 AM   
```  
  
## See Also  
 [sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md)   
 [@@CPU_BUSY &#40;Transact-SQL&#41;](../../t-sql/functions/cpu-busy-transact-sql.md)   
 [sp_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)   
 [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
  
  
