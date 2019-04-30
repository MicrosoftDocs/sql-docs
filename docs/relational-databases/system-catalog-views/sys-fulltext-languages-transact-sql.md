---
title: "sys.fulltext_languages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.fulltext_languages"
  - "sys.fulltext_languages_TSQL"
  - "fulltext_languages"
  - "fulltext_languages_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "languages [full-text search]"
  - "sys.fulltext_languages catalog view"
ms.assetid: 2ed6b53d-1cf2-4763-9d58-36ea24a610ef
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fulltext_languages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  This catalog view contains one row per language whose word breakers are registered with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each row displays the LCID and name of the language. When word breakers are registered for a language, its other linguistic resources-stemmers, noise words (stopwords), and thesaurus files-become available to full-text indexing/querying operations. The value of **name** or **lcid** can be specified in the full-text queries and full-text index [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
   
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|**lcid**|**int**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows locale identifier (LCID) for the language.|  
|**name**|**sysname**|Is either the value of the alias in [sys.syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md) corresponding to the value of **lcid** or the string representation of the numeric LCID.|  
  
## Values Returned for Default Languages  
 The following table shows values for the languages whose word breakers are registered by default.  
  
|Language|LCID|  
|--------------|----------|  
|Arabic|1025|  
|Bengali (India)|1093|  
|British English|2057|  
|Bulgarian|1026|  
|Catalan|1027|  
|Chinese (Hong Kong SAR, PRC)|3076|  
|Chinese (Macao SAR)|5124|  
|Chinese (Singapore)|4100|  
|Croatian|1050|  
|Czech|1029|  
|Danish|1030|  
|Dutch|1043|  
|English|1033|  
|French|1036|  
|German|1031|  
|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Greek|1032|  
|Gujarati|1095|  
|Hebrew|1037|  
|Hindi|1081|  
|Icelandic|1039|  
|Indonesian|1057|  
|Italian|1040|  
|Japanese|1041|  
|Kannada|1099|  
|Korean|1042|  
|Latvian|1062|  
|Lithuanian|1063|  
|Malay - Malaysia|1086|  
|Malayalam|1100|  
|Marathi|1102|  
|Neutral|0|  
|Norwegian (Bokmål)|1044|  
|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Polish|1045|  
|Portuguese (Brazil)|1046|  
|Portuguese (Portugal)|2070|  
|Punjabi|1094|  
|Romanian|1048|  
|Russian|1049|  
|Serbian (Cyrillic)|3098|  
|Serbian (Latin)|2074|  
|Simplified Chinese|2052|  
|Slovak|1051|  
|Slovenian|1060|  
|Spanish|3082|  
|Swedish|1053|  
|Tamil|1097|  
|Telugu|1098|  
|Thai|1054|  
|Traditional Chinese|1028|  
|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Turkish|1055|  
|Ukrainian|1058|  
|Urdu|1056|  
|Vietnamese|1066|  
  
## Remarks  
 To update the list of languages registered with full-text search, use [sp_fulltext_service](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md)'**update_languages**'.  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)]  
  
## See Also  
 [sp_fulltext_load_thesaurus_file &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-load-thesaurus-file-transact-sql.md)   
 [sp_fulltext_service &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md)   
 [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md)   
 [Configure and Manage Thesaurus Files for Full-Text Search](../../relational-databases/search/configure-and-manage-thesaurus-files-for-full-text-search.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)   
 [Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md)  
  
  
