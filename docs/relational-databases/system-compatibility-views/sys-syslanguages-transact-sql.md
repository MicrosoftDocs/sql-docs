---
title: "sys.syslanguages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.syslanguages"
  - "sys.syslanguages_TSQL"
  - "syslanguages"
  - "syslanguages_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syslanguages system table"
  - "sys.syslanguages compatibility view"
ms.assetid: f216d1cd-997c-42f0-a737-abbdfcd88383
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.syslanguages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains one row for each language present in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|langid|**smallint**|Unique language ID.|  
|dateformat|**nchar(3)**|Date order, for example, DMY.|  
|datefirst|**tinyint**|First day of the week: 1 for Monday, 2 for Tuesday, and so on through 7 for Sunday.|  
|upgrade|**int**|Reserved for system use.|  
|name|**sysname**|Official language name, for example, Français.|  
|alias|**sysname**|Alternative language name, for example, French.|  
|months|**nvarchar(372)**|Comma-separated list of full-length month names in order from January through December, with each name having up to 20 characters.|  
|shortmonths|**nvarchar(132)**|Comma-separated list of short-month names in order from January through December, with each name having up to 9 characters.|  
|days|**nvarchar(217)**|Comma-separated list of day names in order from Monday through Sunday, with each name having up to 30 characters.|  
|lcid|**int**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows locale ID for the language.|  
|msglangid|**smallint**|[!INCLUDE[ssDE](../../includes/ssde-md.md)] message group ID.|  
  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] contains the following installed languages.  
  
|Name in English|Windows LCID|[!INCLUDE[ssDE](../../includes/ssde-md.md)] message group ID|  
|---------------------|------------------|-----------------------------------------|  
|English|1033|1033|  
|German|1031|1031|  
|French|1036|1036|  
|Japanese|1041|1041|  
|Danish|1030|1030|  
|Spanish|3082|3082|  
|Italian|1040|1040|  
|Dutch|1043|1043|  
|Norwegian|2068|2068|  
|Portuguese|2070|2070|  
|Finnish|1035|1035|  
|Swedish|1053|1053|  
|Czech|1029|1029|  
|Hungarian|1038|1038|  
|Polish|1045|1045|  
|Romanian|1048|1048|  
|Croatian|1050|1050|  
|Slovak|1051|1051|  
|Slovene|1060|1060|  
|Greek|1032|1032|  
|Bulgarian|1026|1026|  
|Russian|1049|1049|  
|Turkish|1055|1055|  
|British English|2057|1033|  
|Estonian|1061|1061|  
|Latvian|1062|1062|  
|Lithuanian|1063|1063|  
|Brazilian|1046|1046|  
|Traditional Chinese|1028|1028|  
|Korean|1042|1042|  
|Simplified Chinese|2052|2052|  
|Arabic|1025|1025|  
|Thai|1054|1054|  
  
## See Also  
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)   
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)  
  
  
