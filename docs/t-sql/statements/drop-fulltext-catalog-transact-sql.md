---
title: "DROP FULLTEXT CATALOG (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_FULLTEXT_CATALOG_TSQL"
  - "DROP FULLTEXT CATALOG"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dropping full-text catalogs"
  - "removing full-text catalogs"
  - "full-text catalogs [SQL Server], removing"
  - "deleting full-text catalogs"
  - "DROP FULLTEXT CATALOG statement"
ms.assetid: b54efb0b-400b-49ce-923b-ce20a2a12255
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP FULLTEXT CATALOG (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Removes a full-text catalog from a database. You must drop all full-text indexes associated with the catalog before you drop the catalog.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP FULLTEXT CATALOG catalog_name  
```  
  
## Arguments  
 *catalog_name*  
 Is the name of the catalog to be removed. If *catalog_name* does not exist, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error and does not perform the DROP operation. The filegroup of the full-text catalog must not be marked OFFLINE or READONLY for the command to succeed.  
  
## Permissions  
 User must have DROP permission on the full-text catalog or be a member of the **db_owner**, or **db_ddladmin** fixed database roles.  
  
## See Also  
 [sys.fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)   
 [ALTER FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md)   
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-catalog-transact-sql.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
  
