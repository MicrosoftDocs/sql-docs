---
title: "sys.computed_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.computed_columns_TSQL"
  - "sys.computed_columns"
  - "computed_columns_TSQL"
  - "computed_columns"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.computed_columns catalog view"
ms.assetid: c962c619-e18f-4315-9251-8d9862462299
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.computed_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each column found in **sys.columns** that is a computed-column.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<Inherited columns>**||The **sys.computed_columns** view returns all columns in the **sys.columns** view. It also returns the additional columns described below. For a description of the columns that the **sys.computed_columns** view inherits from **sys.columns**, see [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md). The value of the **is_computed** column is always set to 1 in the **sys.computed_columns** view.|  
|**definition**|**nvarchar(max)**|SQL text that defines this computed-column.|  
|**uses_database_collation**|**bit**|1 = The column definition depends on the default collation of the database for correct evaluation; otherwise, 0. Such a dependency prevents changing the database default collation.|  
|**is_persisted**|**bit**|Computed column is persisted.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
