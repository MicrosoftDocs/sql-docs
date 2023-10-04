---
title: "sp_help_fulltext_catalog_components (Transact-SQL)"
description: "sp_help_fulltext_catalog_components (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_fulltext_catalog_components_TSQL"
  - "sp_help_fulltext_catalog_components"
helpviewer_keywords:
  - "sp_help_fulltext_catalog_components"
dev_langs:
  - "TSQL"
---
# sp_help_fulltext_catalog_components (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a list of all components (filters, word-breakers, and protocol handlers), used for all full-text catalogs in the current database.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_fulltext_catalog_components  
```  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**full-text catalog name**|**int**|Name of the full-text catalog.|  
|**full-text catalog id**|**sysname**|ID of the full-text catalog.|  
|**componenttype**|**sysname**|Type of component. One of the following:<br /><br /> Filter<br /><br /> Protocol handler<br /><br /> Wordbreaker|  
|**componentname**|**sysname**|Name of the component.|  
|**clsid**|**uniqueidentifier**|Class identifier of the component.|  
|**fullpath**|**nvarchar(256)**|Path to the location of the component.<br /><br /> NULL = Caller not a member of **serveradmin** fixed server role.|  
|**version**|**nvarchar(30)**|Version of the component.|  
|**manufacturer**|**sysname**|Name of the manufacturer of the component.|  
  
## Permissions  
 Requires membership in the **public** role.  
  
## See Also  
 [Full-Text Search and Semantic Search Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/full-text-search-and-semantic-search-stored-procedures-transact-sql.md)   
 [sys.fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)   
 [sp_help_fulltext_system_components &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
  
