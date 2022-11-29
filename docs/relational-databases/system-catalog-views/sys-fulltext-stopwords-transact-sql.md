---
title: "sys.fulltext_stopwords (Transact-SQL)"
description: sys.fulltext_stopwords (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fulltext_stopwords_TSQL"
  - "fulltext_stopwords"
  - "sys.fulltext_stopwords"
  - "sys.fulltext_stopwords_TSQL"
helpviewer_keywords:
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stopwords"
  - "sys.fulltext_stopwords catalog view"
  - "stopwords [full-text search]"
dev_langs:
  - "TSQL"
ms.assetid: 79787bb7-d729-448e-b56a-0a467bbb304f
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fulltext_stopwords (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Contains a row per stopword for all stoplists in the database.  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**stoplist_id**|**int**|ID of the stoplist to which **stopword** belongs. This ID is  unique within the database.|  
|**stopword**|**nvarchar(64)**|The term to be considered for a stop-word match.|  
|**language**|**sysname**|Is either the value of the alias in [sys.fulltext_languages](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md)corresponding to the value of the locale identifier (**LCID**), or is the string representation of the numeric LCID.|  
|**language_id**|**int**|LCID used for word breaking.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)]  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)   
 [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)   
 [sys.fulltext_system_stopwords &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-system-stopwords-transact-sql.md)  
  
  
