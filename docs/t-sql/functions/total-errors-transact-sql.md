---
title: "@@TOTAL_ERRORS (Transact-SQL)"
description: "@@TOTAL_ERRORS (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@TOTAL_ERRORS"
  - "@@TOTAL_ERRORS_TSQL"
helpviewer_keywords:
  - "@@TOTAL_ERRORS function"
  - "total errors [SQL Server]"
  - "errors [SQL Server], read/write"
  - "number of disk read/write errors"
  - "disks [SQL Server], errors"
  - "write errors [SQL Server]"
  - "read/write errors"
dev_langs:
  - "TSQL"
---
# @@TOTAL_ERRORS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the number of disk write errors encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] last started.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@TOTAL_ERRORS  
```  
  
## Return Types
 **integer**  
  
## Remarks  
 Not all write errors encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are accounted for by this function. Occasional non-fatal write errors are handled by the server itself and are not considered errors. To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including total number of errors, run **sp_monitor**.  
  
## Examples  
 This example shows the number of errors encountered by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as of the current date and time.  
  
```sql
SELECT @@TOTAL_ERRORS AS 'Errors', GETDATE() AS 'As of';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Errors      As of                   
----------- ----------------------  
0           3/28/2003 12:32:11 PM   
```  
  
## Related content

- [sp_monitor (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)
- [System Statistical Functions (Transact-SQL)](system-statistical-functions-transact-sql.md)
