---
description: "sp_linkedservers (Transact-SQL)"
title: "sp_linkedservers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_linkedservers"
  - "sp_linkedservers_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_linkedservers"
ms.assetid: d8f82f78-8a1f-4831-bcee-7c36c6e7dfbb
author: markingmyname
ms.author: maghan
---
# sp_linkedservers (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the list of linked servers defined in the local server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_linkedservers  
```  
  
## Return Code Values  
 0 (success) or a nonzero number (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**SRV_NAME**|**sysname**|Name of the linked server.|  
|**SRV_PROVIDERNAME**|**nvarchar(**128**)**|Friendly name of the OLE DB provider managing access to the specified linked server.|  
|**SRV_PRODUCT**|**nvarchar(**128**)**|Product name of the linked server.|  
|**SRV_DATASOURCE**|**nvarchar(**4000**)**|OLE DB data source property corresponding to the specified linked server.|  
|**SRV_PROVIDERSTRING**|**nvarchar(**4000**)**|OLE DB provider string property corresponding to the linked server.|  
|**SRV_LOCATION**|**nvarchar(**4000**)**|OLE DB location property corresponding to the specified linked server.|  
|**SRV_CAT**|**sysname**|OLE DB catalog property corresponding to the specified linked server.|  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## See Also  
 [sp_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-catalogs-transact-sql.md)   
 [sp_column_privileges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-column-privileges-transact-sql.md)   
 [sp_columns_ex &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-columns-ex-transact-sql.md)   
 [sp_foreignkeys &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-foreignkeys-transact-sql.md)   
 [sp_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-indexes-transact-sql.md)   
 [sp_primarykeys &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-primarykeys-transact-sql.md)   
 [sp_table_privileges &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-table-privileges-transact-sql.md)   
 [sp_tables_ex &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tables-ex-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Distributed Queries Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/distributed-queries-stored-procedures-transact-sql.md)  
  
  
