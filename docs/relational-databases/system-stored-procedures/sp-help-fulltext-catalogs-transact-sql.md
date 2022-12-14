---
description: "sp_help_fulltext_catalogs (Transact-SQL)"
title: "sp_help_fulltext_catalogs (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_help_fulltext_catalogs_TSQL"
  - "sp_help_fulltext_catalogs"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_fulltext_catalogs"
ms.assetid: 1b94f280-e095-423f-88bc-988c9349d44c
author: markingmyname
ms.author: maghan
---
# sp_help_fulltext_catalogs (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the ID, name, root directory, status, and number of full-text indexed tables for the specified full-text catalog.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_catalogs](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md) catalog view instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_fulltext_catalogs [ @fulltext_catalog_name = ] 'fulltext_catalog_name'  
```  
  
## Arguments  
`[ @fulltext_catalog_name = ] 'fulltext_catalog_name'`
 Is the name of the full-text catalog. *fulltext_catalog_name* is **sysname**. If this parameter is omitted or has the value NULL, information about all full-text catalogs associated with the current database is returned.  
  
## Return Code Values  
 0 (success) or (1) failure  
  
## Result Sets  
 This table shows the result set, which is ordered by **ftcatid**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**fulltext_catalog_id**|**smallint**|Full-text catalog identifier.|  
|**NAME**|**sysname**|Name of the full-text catalog.|  
|**PATH**|**nvarchar(260)**|Beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], this clause has no effect.|  
|**STATUS**|**int**|Full-text index population status of the catalog:<br /><br /> 0 = Idle<br /><br /> 1 = Full population in progress<br /><br /> 2 = Paused<br /><br /> 3 = Throttled<br /><br /> 4 = Recovering<br /><br /> 5 = Shutdown<br /><br /> 6 = Incremental population in progress<br /><br /> 7 = Building index<br /><br /> 8 = Disk is full. Paused<br /><br /> 9 = Change tracking<br /><br /> NULL = User does not have VIEW permission on the full-text catalog, or database is not full-text enabled, or full-text component not installed.|  
|**NUMBER_FULLTEXT_TABLES**|**int**|Number of full-text indexed tables associated with the catalog.|  
  
## Permissions  
 Execute permissions default to members of the **public** role.  
  
## Examples  
 The following example returns information about the `Cat_Desc` full-text catalog.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_help_fulltext_catalogs 'Cat_Desc' ;  
GO  
```  
  
## See Also  
 [FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md)   
 [sp_fulltext_catalog &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-catalog-transact-sql.md)   
 [sp_help_fulltext_catalogs_cursor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-catalogs-cursor-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
