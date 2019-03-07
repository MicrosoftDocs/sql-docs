---
title: "DROP FULLTEXT STOPLIST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_FULLTEXT_STOPLIST_TSQL"
  - "DROP FULLTEXT STOPLIST"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DROP FULLTEXT STOPLIST statement"
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stoplists"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
ms.assetid: 3ee2a2bb-1dfb-4e7c-90e9-9d917cd84a15
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP FULLTEXT STOPLIST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Drops a full-text stoplist from the database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
> [!IMPORTANT]  
>  CREATE FULLTEXT STOPLIST is supported only for compatibility level 100 and higher. For compatibility levels 80 and 90, the system stoplist is always assigned to the database.  
  
## Syntax  
  
```  
  
DROP FULLTEXT STOPLIST stoplist_name  
;  
```  
  
## Arguments  
 *stoplist_name*  
 Is the name of the full-text stoplist to drop from the database.  
  
## Remarks  
 DROP FULLTEXT STOPLIST fails if any full-text indexes refer to the full-text stoplist being dropped.  
  
## Permissions  
 To drop a stoplist requires having DROP permission on the stoplist or membership in the **db_owner** or **db_ddladmin** fixed database roles.  
  
## Examples  
 The following example drops a full-text stoplist named `myStoplist`.  
  
```  
DROP FULLTEXT STOPLIST myStoplist;  
```  
  
## See Also  
 [ALTER FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md)   
 [CREATE FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md)   
 [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)   
 [sys.fulltext_stopwords &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql.md)  
  
  
