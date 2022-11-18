---
title: "cdc.captured_columns (Transact-SQL)"
description: cdc.captured_columns (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "cdc.captured_columns"
  - "cdc.captured_columns_TSQL"
helpviewer_keywords:
  - "cdc.captured_columns"
dev_langs:
  - "TSQL"
ms.assetid: 7bb4d408-d764-4ef6-802c-f271c8d39c2a
---
# cdc.captured_columns (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns one row for each column tracked in a capture instance. By default, all columns of the source table are captured. However, columns can be included or excluded when the source table is enabled for change data capture by specifying a column list. For more information, see [sys.sp_cdc_enable_table &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md).  
  
 We recommend that you **do not query the system tables directly**. Instead, execute the [sys.sp_cdc_get_source_columns](../../relational-databases/system-stored-procedures/sys-sp-cdc-get-captured-columns-transact-sql.md) stored procedure.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the change table to which the captured column belongs.|  
|**column_name**|**sysname**|Name of the captured column.|  
|**column_id**|**int**|ID of the captured column within the source table.|  
|**column_type**|**sysname**|Type of the captured column.|  
|**column_ordinal**|**int**|Column ordinal (1-based) in the change table. The metadata columns in the change table are excluded. Ordinal 1 is assigned to the first captured column.|  
|**is_computed**|**bit**|Indicates that the captured column is a computed column in the source table.|  
  
## See Also  
 [cdc.change_tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/cdc-change-tables-transact-sql.md)  
  
  
