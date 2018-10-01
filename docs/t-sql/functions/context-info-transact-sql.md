---
title: "CONTEXT_INFO  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CONTEXT_INFO_TSQL"
  - "CONTEXT_INFO"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CONTEXT_INFO function"
  - "Multiple Active Result Sets"
  - "context information [SQL Server]"
  - "MARS [SQL Server]"
  - "session context information [SQL Server]"
ms.assetid: 571320f5-7228-4b0e-9d01-ab732d2d1eab
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# CONTEXT_INFO  (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the **context_info** value either set for the current session or batch, or derived through use of the [SET CONTEXT_INFO](../../t-sql/statements/set-context-info-transact-sql.md) statement.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CONTEXT_INFO()  
```  
  
## Return value
The **context_info** value.
  
If **context_info** was not set:
-   In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns NULL.  
-   In [!INCLUDE[ssSDS](../../includes/sssds-md.md)] returns a unique session-specific GUID.  
  
## Remarks  
The Multiple Active Result Sets (MARS) feature enables applications to run multiple batches, or requests, at the same time, on the same connection. When one of the MARS connection batches runs SET CONTEXT_INFO, the `CONTEXT_INFO` function returns the new context value, when the `CONTEXT_INFO` function runs in the same batch as the SET statement. If the `CONTEXT_INFO` function runs in one or more of the other connection batches, the `CONTEXT_FUNCTION` does not return the new value unless those batches started after completion of the batch that ran the SET statement.
  
## Permissions  
Requires no special permissions. The following system views store the context information, but querying these views directly requires SELECT and VIEW SERVER STATE permissions:
- **sys.dm_exec_requests**
- **sys.dm_exec_sessions**
- **sys.sysprocesses**
  
## Examples  
This simple example sets the **context_info** value to `0x1256698456`, and then uses the `CONTEXT_INFO` function to retrieve the value.
  
```sql
SET CONTEXT_INFO 0x1256698456;  
GO  
SELECT CONTEXT_INFO();  
GO  
```  
  
## See also
[SET CONTEXT_INFO &#40;Transact-SQL&#41;](../../t-sql/statements/set-context-info-transact-sql.md)
  
  
