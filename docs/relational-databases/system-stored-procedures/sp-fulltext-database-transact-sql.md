---
title: "sp_fulltext_database (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_fulltext_database_TSQL"
  - "sp_fulltext_database"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_fulltext_database"
ms.assetid: eeb1e151-eb00-484c-8fd1-5641e621ffc6
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_fulltext_database (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Has no effect on full-text catalogs in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions and is supported for backward compatibility only. **sp_fulltext_database** does not disable the Full-Text Engine for a given database. All user-created databases in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] are always enabled for full-text indexing.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_fulltext_database [@action=] 'action'  
```  
  
## Arguments  
`[ @action = ] 'action'`
 Is the action to be performed. **action** is **varchar(20)**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**enable**|Supported for backward compatibility only. Has no effect on full-text catalogs in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions.|  
|**disable**|Supported for backward compatibility only. Has no effect on full-text catalogs in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions.|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, full-text indexing cannot be turned off. Disabling full-text indexing does not remove rows from **sysfulltextcatalogs** and does not indicate that full-text enabled tables are no longer marked for full-text indexing. All the full-text metadata definitions are still in the system tables.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role and **db_owner** fixed database role can execute **sp_fulltext_database**.  
  
## See Also  
 [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md)   
 [FULLTEXTSERVICEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/fulltextserviceproperty-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
