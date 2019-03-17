---
title: "CREATE FULLTEXT STOPLIST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STOPLIST_TSQL"
  - "FULLTEXT STOPLIST"
  - "STOPLIST"
  - "FULLTEXT_STOPLIST_TSQL"
  - "CREATE FULLTEXT STOPLIST"
  - "CREATE_FULLTEXT_STOPLIST_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "stoplists [full-text search]"
  - "CREATE FULLTEXT STOPLIST statement"
  - "full-text search [SQL Server], stoplists"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
ms.assetid: 0669b1d0-46cc-4fac-8df7-5f7fa7af5db4
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE FULLTEXT STOPLIST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates a new full-text stoplist in the current database.  
  
 Stopwords are managed in databases by using objects called *stoplists*. A stoplist is a list of stopwords that, when associated with a full-text index, is applied to full-text queries on that index. For more information, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  
  
> [!IMPORTANT]  
>  CREATE FULLTEXT STOPLIST, ALTER FULLTEXT STOPLIST, and DROP FULLTEXT STOPLIST are supported only under compatibility level 100. Under compatibility levels 80 and 90, these statements are not supported. However, under all compatibility levels the system stoplist is automatically associated with new full-text indexes.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CREATE FULLTEXT STOPLIST stoplist_name  
[ FROM { [ database_name.]source_stoplist_name } | SYSTEM STOPLIST ]  
[ AUTHORIZATION owner_name ]  
;  
```  
  
## Arguments  
 *stoplist_name*  
 Is the name of the stoplist. *stoplist_name* can be a maximum of 128 characters. *stoplist_name* must be unique among all stoplists in the current database, and conform to the rules for identifiers.  
  
 *stoplist_name* will be used when the full-text index is created.  
  
 *database_name*  
 Is the name of the database where the stoplist specified by *source_stoplist_name* is located. If not specified, *database_name* defaults to the current database.  
  
 *source_stoplist_name*  
 Specifies that the new stoplist is created by copying an existing stoplist. If *source_stoplist_name* does not exist, or the database user does not have correct permissions, CREATE FULLTEXT STOPLIST fails with an error. If any languages specified in the stop words of the source stoplist are not registered in the current database, CREATE FULLTEXT STOPLIST succeeds, but warning(s) are returned and the corresponding stop words are not added.  
  
 SYSTEM STOPLIST  
 Specifies that the new stoplist is created from the stoplist that exists by default in the [Resource database](../../relational-databases/databases/resource-database.md).  
  
 AUTHORIZATION *owner_name*  
 Specifies the name of a database principal to own of the stoplist. *owner_name* must either be the name of a principal of which the current user is a member, or the current user must have IMPERSONATE permission on *owner_name*. If not specified, ownership is given to the current user.  
  
## Remarks  
 The creator of a stoplist is its owner.  
  
## Permissions  
 To create a STOPLIST requires CREATE FULLTEXT CATALOG permissions. The stoplist owner can grant CONTROL permission explicitly on a stoplist to allow users to add and remove words and to drop the stoplist.  
  
> [!NOTE]  
>  Using a stoplist with a full-text index requires REFERENCE permission.  
  
## Examples  
  
### A. Creating a new full-text stoplist  
 The following example creates a new full-text stoplist named `myStoplist`.  
  
```  
CREATE FULLTEXT STOPLIST myStoplist;  
GO  
```  
  
### B. Copying a full-text stoplist from an existing full-text stoplist  
 The following example creates a new full-text stoplist named `myStoplist2` by copying an existing AdventureWorks stoplist named `Customers.otherStoplist`.  
  
```  
CREATE FULLTEXT STOPLIST myStoplist2 FROM AdventureWorks.otherStoplist;  
GO  
```  
  
### C. Copying a full-text stoplist from the system full-text stoplist  
 The following example creates a new full-text stoplist named `myStoplist3` by copying from the system stoplist.  
  
```  
CREATE FULLTEXT STOPLIST myStoplist3 FROM SYSTEM STOPLIST;  
GO  
```  
  
## See Also  
 [ALTER FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md)   
 [DROP FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/drop-fulltext-stoplist-transact-sql.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)   
 [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)   
 [sys.fulltext_stopwords &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)  
  
  
