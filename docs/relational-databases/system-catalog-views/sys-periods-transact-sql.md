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
  
## Related content

- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.all_columns (Transact-SQL)](sys-all-columns-transact-sql.md)
- [sys.system_columns (Transact-SQL)](sys-system-columns-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [Temporal tables](../tables/temporal/overview.md)
