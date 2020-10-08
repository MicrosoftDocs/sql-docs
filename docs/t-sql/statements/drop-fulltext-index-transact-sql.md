---
description: "DROP FULLTEXT INDEX (Transact-SQL)"
title: "DROP FULLTEXT INDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_FULLTEXT_INDEX_TSQL"
  - "DROP FULLTEXT INDEX"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "deleting full-text indexes"
  - "removing full-text indexes"
  - "full-text indexes [SQL Server], removing"
  - "DROP FULLTEXT INDEX statement"
  - "dropping full-text indexes"
ms.assetid: 7443a4ab-1d43-4a22-bbba-a07f620892cb
author: markingmyname
ms.author: maghan
---
# DROP FULLTEXT INDEX (Transact-SQL)
[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

  Removes a full-text index from a specified table or indexed view.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP FULLTEXT INDEX ON table_name  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *table_name*  
 Is the name of the table or indexed view containing the full-text index to be removed.  
  
## Remarks  
 You do not need to drop all columns from the full-text index before using the DROP FULLTEXT INDEX command.  
  
## Permissions  
 The user must have ALTER permission on the table or indexed view, or be a member of the **sysadmin** fixed server role, or **db_owner** or **db_ddladmin** fixed database roles.  
  
## Examples  
 The following example drops the full-text index that exists on the `JobCandidate` table.  
  
```sql  
USE AdventureWorks2012;  
GO  
DROP FULLTEXT INDEX ON HumanResources.JobCandidate;  
GO  
```  
  
## See Also  
 [sys.fulltext_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md)   
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
  
