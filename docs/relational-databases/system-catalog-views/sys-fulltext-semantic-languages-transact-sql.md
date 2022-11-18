---
title: "sys.fulltext_semantic_languages (Transact-SQL)"
description: sys.fulltext_semantic_languages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fulltext_semantic_languages"
  - "fulltext_semantic_languages_TSQL"
  - "sys.fulltext_semantic_languages"
  - "sys.fulltext_semantic_languages_TSQL"
helpviewer_keywords:
  - "sys.fulltext_semantic_languages catalog view"
dev_langs:
  - "TSQL"
ms.assetid: b42a85e6-1db9-4a22-8a70-014574c95198
---
# sys.fulltext_semantic_languages (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each language whose statistics model is registered with the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When a language model is registered, that language is enabled for semantic indexing.  
  
 This catalog view is similar to [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md).  
    
|Column name|Type|Description|  
|-|-|-|   
|lcid|int|Microsoft Windows locale identifier (LCID) for the language.|  
|name|sysname|Is either the value of the alias in [sys.syslanguages &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md) corresponding to the value of **lcid**, or the string representation of the numeric LCID.|  
  
## General Remarks  
 For more information, see [Install and Configure Semantic Search](../../relational-databases/search/install-and-configure-semantic-search.md).  
  
## Metadata  
 For more information about the semantic language statistics database that is installed to support semantic indexing, query the catalog view [sys.fulltext_semantic_language_statistics_database &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-semantic-language-statistics-database-transact-sql.md).  
  
## Security  
  
### Permissions  
 The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission.  
  
## Examples  
 The following example shows how to query **sys.fulltext_semantic_languages** to get information about all the language models registered for semantic indexing on the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
SELECT * FROM sys.fulltext_semantic_languages;  
GO  
```  
  
## See Also  
 [Install and Configure Semantic Search](../../relational-databases/search/install-and-configure-semantic-search.md)  
  
  
