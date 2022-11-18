---
title: "sys.periods (Transact-SQL)"
description: sys.periods (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 25e66ed3-2270-4c5c-9f5a-2c0f165a57ca
---
# sys.periods (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Returns a row for each table for which periods have been defined.  
  
|Column header|Data type|Description|  
|-------------------|---------------|-----------------|  
|name|**sysname**|Name of the period|  
|period_type|**tinyint**|The numeric value representing the type of period:<br /><br /> 1 = system-time period|  
|period_type_desc|**nvarchar(60)**|The text description of the type of column:<br /><br /> SYSTEM_TIME_PERIOD|  
|object_id|**int**|The id of the table containing the period_type column|  
|start_column_id|**int**|The id of the column that defines the lower period boundary|  
|end_column_id|**int**|The id of the column that defines the upper period boundary|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.all_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-columns-transact-sql.md)   
 [sys.system_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-columns-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [Temporal Tables](../../relational-databases/tables/temporal-tables.md)  
  
