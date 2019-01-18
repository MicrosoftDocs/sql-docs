---
title: "sys.function_order_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "function_order_columns"
  - "sys.function_order_columns_TSQL"
  - "function_order_columns_TSQL"
  - "sys.function_order_columns"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.function_order_columns catalog view"
ms.assetid: 29287973-3125-4d35-8ca9-92cb45828854
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.function_order_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row per column that is a part of an **ORDER** expression of a commmon language runtime (CLR) table-valued function.  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object (CLR table-valued function) the order is defined on.|  
|**order_column_id**|**int**|ID of the order column. **order_column_id** is unique only within **object_id**.<br /><br /> **order_column_id** represents the position of this column in the ordering.|  
|**column_id**|**int**|ID of the column in **object_id**.<br /><br /> **column_id** is unique only within **object_id**.|  
|**is_descending**|**bit**|1 = order column has a descending sort direction.<br /><br /> 0 = order column has an ascending sort direction.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
