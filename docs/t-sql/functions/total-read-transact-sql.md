---
title: "@@TOTAL_READ (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/17/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@TOTAL_READ_TSQL"
  - "@@TOTAL_READ"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "number of disk reads"
  - "disks [SQL Server], number of disk reads"
  - "@@TOTAL_READ function"
  - "total read [SQL Server]"
  - "read activity since last started"
ms.assetid: b505fbe9-9569-4f3d-80b9-b64b5109ac98
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;TOTAL_READ (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the number of disk reads, not cache reads, by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
@@TOTAL_READ  
```  
  
## Return Types  
 **integer**  
  
## Remarks  
 To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including read and write activity, run **sp_monitor**.  
  
## Examples  
 The following example shows returning the total number of disk read and writes as of the current date and time.  
  
```  
SELECT @@TOTAL_READ AS 'Reads', @@TOTAL_WRITE AS 'Writes', GETDATE() AS 'As of';  
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
 [@@TOTAL_WRITE &#40;Transact-SQL&#41;](../../t-sql/functions/total-write-transact-sql.md)  
  
  
