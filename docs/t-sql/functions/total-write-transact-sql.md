---
title: "@@TOTAL_WRITE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@TOTAL_WRITE"
  - "@@TOTAL_WRITE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "write activity since last started [SQL Server]"
  - "number of disk writes"
  - "@@TOTAL_WRITE function"
  - "disks [SQL Server], number of disk writes"
  - "total write [SQL Server]"
ms.assetid: cd528126-51ee-4aa4-a21f-f32ce5c80fac
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;TOTAL_WRITE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the number of disk writes by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
@@TOTAL_WRITE  
```  
  
## Return Types  
 **integer**  
  
## Remarks  
 To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including read and write activity, run **sp_monitor**.  
  
## Examples  
 The following example shows returning the total number of disk reads and writes as of the current date and time.  
  
```  
SELECT @@TOTAL_READ AS 'Reads', @@TOTAL_WRITE AS 'Writes', GETDATE() AS 'As of'  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Reads       Writes      As of                   
----------- ----------- ----------------------  
7760        97263       12/5/2006 10:23:00 PM   
```  
  
## See Also  
 [sp_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)   
 [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)   
 [@@TOTAL_READ &#40;Transact-SQL&#41;](../../t-sql/functions/total-read-transact-sql.md)  
  
  
