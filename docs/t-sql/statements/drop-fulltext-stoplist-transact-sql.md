---
title: "DROP FULLTEXT STOPLIST (Transact-SQL)"
description: DROP FULLTEXT STOPLIST (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_FULLTEXT_STOPLIST_TSQL"
  - "DROP FULLTEXT STOPLIST"
helpviewer_keywords:
  - "DROP FULLTEXT STOPLIST statement"
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stoplists"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
dev_langs:
  - "TSQL"
---
# DROP FULLTEXT STOPLIST (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Drops a full-text stoplist from the database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
> [!IMPORTANT]  
>  CREATE FULLTEXT STOPLIST is supported only for compatibility level 100 and higher. For compatibility levels 80 and 90, the system stoplist is always assigned to the database.  
  
## Syntax  
  
```syntaxsql
DROP FULLTEXT STOPLIST stoplist_name  
;  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *stoplist_name*  
 Is the name of the full-text stoplist to drop from the database.  
  
## Remarks  
 DROP FULLTEXT STOPLIST fails if any full-text indexes refer to the full-text stoplist being dropped.  
  
## Permissions  
 To drop a stoplist requires having DROP permission on the stoplist or membership in the **db_owner** or **db_ddladmin** fixed database roles.  
  
## Examples  
 The following example drops a full-text stoplist named `myStoplist`.  
  
```sql 
DROP FULLTEXT STOPLIST myStoplist;  
```  
  
## See Also  
 [ALTER FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md)   
 [CREATE FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md)   
 [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)   
 [sys.fulltext_stopwords &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql.md)  
  
  
