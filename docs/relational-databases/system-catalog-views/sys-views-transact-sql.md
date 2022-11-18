---
title: sys.views (Transact-SQL)
description: sys.views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "sys.views_TSQL"
  - "sys.views"
helpviewer_keywords:
  - "sys.views catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# sys.views (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains a row for each view object, with **sys.objects.type** = V.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**||For a list of columns that this view inherits, see [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)|  
|**is_replicated**|**bit**|1 = View is replicated.|  
|**has_replication_filter**|**bit**|1 = View has a replication filter.|  
|**has_opaque_metadata**|**bit**|1 = VIEW_METADATA option specified for view. For more information, see [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md).|  
|**has_unchecked_assembly_data**|**bit**|1 = View contains persisted data that depends on an assembly whose definition changed during the last ALTER ASSEMBLY. Resets to 0 after the next successful DBCC CHECKDB or DBCC CHECKTABLE.|  
|**with_check_option**|**bit**|1 = WITH CHECK OPTION was specified in the view definition.|  
|**is_date_correlation_view**|**bit**|1 = View was created automatically by the system to store correlation information between datetime columns. Creation of this view was enabled by setting DATE_CORRELATION_OPTIMIZATION to ON.|
|**ledger_view_type**|**tinyint**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>The numeric value indicating if a view is a ledger view for an updatable ledger table.<br/><br/>0 = NON_LEDGER_VIEW<br/>1 = LEDGER_VIEW<br /><br />For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).|
|**ledger_view_type_desc**|**nvarchar(60)**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>The text description of a value in the ledger_view_type column:<br/><br/>NON_LEDGER_VIEW<br/>LEDGER_VIEW|
|**is_dropped_ledger_view**|**bit**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>Indicates a ledger view that has been dropped.|
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [ALTER ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-assembly-transact-sql.md)   
 [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)   
 [DBCC CHECKTABLE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)  
  
  
