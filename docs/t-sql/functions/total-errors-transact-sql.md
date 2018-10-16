---
title: "@@TOTAL_ERRORS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@TOTAL_ERRORS"
  - "@@TOTAL_ERRORS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@TOTAL_ERRORS function"
  - "total errors [SQL Server]"
  - "errors [SQL Server], read/write"
  - "number of disk read/write errors"
  - "disks [SQL Server], errors"
  - "write errors [SQL Server]"
  - "read/write errors"
ms.assetid: 09e62428-ee0e-4ef5-b969-da9d255f1199
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;TOTAL_ERRORS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the number of disk write errors encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] last started.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@TOTAL_ERRORS  
```  
  
## Return Types  
 **integer**  
  
## Remarks  
 Not all write errors encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are accounted for by this function. Occasional non-fatal write errors are handled by the server itself and are not considered errors. To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including total number of errors, run **sp_monitor**.  
  
## Examples  
 This example shows the number of errors encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as of the current date and time.  
  
```  
SELECT @@TOTAL_ERRORS AS 'Errors', GETDATE() AS 'As of';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Errors      As of                   
----------- ----------------------  
0           3/28/2003 12:32:11 PM   
```  
  
## See Also  
 [sp_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)   
 [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
  
  
