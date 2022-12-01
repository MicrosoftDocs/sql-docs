---
title: "sys.all_views (Transact-SQL)"
description: sys.all_views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.all_views_TSQL"
  - "sys.all_views"
  - "all_views"
  - "all_views_TSQL"
helpviewer_keywords:
  - "sys.all_views catalog view"
dev_langs:
  - "TSQL"
ms.assetid: d8829213-fce2-41c6-9ab2-aaab5836c941
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.all_views (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Shows the UNION of all user-defined and system views.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**||For a list of columns that this view inherits, see [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).|  
|**is_replicated**|**bit**|1 = View is replicated.|  
|**has_replication_filter**|**bit**|1 = View has a replication filter.|  
|**has_opaque_metadata**|**bit**|1 = VIEW_METADATA option specified for view. For more information, see [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md).|  
|**has_unchecked_assembly_data**|**bit**|1 = Table contains persisted data that depends on an assembly whose definition changed during the last ALTER ASSEMBLY. Resets to 0 after the next successful DBCC CHECKDB or DBCC CHECKTABLE.|  
|**with_check_option**|**bit**|1 = WITH CHECK OPTION was specified in the view definition.|  
|**is_date_correlation_view**|**bit**|1 = View was created automatically by the system to store correlation information between datetime columns. Creation of this view was enabled by setting DATE_CORRELATION_OPTIMIZATION to **ON**.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)   
 [ALTER ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-assembly-transact-sql.md)   
 [DBCC CHECKTABLE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md)  
  
  
