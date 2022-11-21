---
title: "SESSION_ID (Transact-SQL)"
description: "SESSION_ID (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "02/23/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest"
---
# SESSION_ID (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Returns the ID of the current [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW_md](../../includes/sspdw-md.md)] session.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
-- Azure Synapse Analytics and Parallel Data Warehouse  
SESSION_ID ( )  
```  
  
## Return Value  
 Returns an **nvarchar(32)** value.  
  
## General Remarks  
 The session ID is assigned to each user connection when the connection is made. It persists for the duration of the connection. When the connection ends, the session ID is released.  
  
 The session ID begins with the alphabetical characters 'SID'. These are case-sensitive and must be capitalized when session ID is used in [!INCLUDE[DWsql](../../includes/dwsql-md.md)] commands.  
  
 You can query the view [sys.dm_pdw_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md) to retrieve the same information as this function.  
  
## Examples  
 The following example returns the current session ID.  
  
```sql  
SELECT SESSION_ID();  
```  
  
## See Also  
 [DB_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/db-name-transact-sql.md)   
 [VERSION &#40;Azure Synapse Analytics&#41;](../../t-sql/functions/version-transact-sql-configuration-functions.md)
  
  
