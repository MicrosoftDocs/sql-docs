---
title: "sys.fulltext_system_stopwords (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.fulltext_system_stopwords_TSQL"
  - "fulltext_system_stopwords"
  - "fulltext_system_stopwords_TSQL"
  - "sys.fulltext_system_stopwords"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "stoplists [full-text search]"
  - "sys.fulltext_system_stopwords catalog view"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
ms.assetid: 487de53f-c637-4d78-85f6-fef5e768cd0c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fulltext_system_stopwords (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Provides access to the system stoplist.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**stopword**|**nvarchar(64)**|The term that is considered for a stop-word match.|  
|**language_id**|**int**|Locale identifier (LCID) of the language. This LCID is used for word breaking.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)]  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)   
 [sys.fulltext_stopwords &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)  
  
  
