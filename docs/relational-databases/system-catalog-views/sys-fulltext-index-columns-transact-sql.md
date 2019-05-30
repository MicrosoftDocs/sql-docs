---
title: "sys.fulltext_index_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.fulltext_index_columns"
  - "fulltext_index_columns"
  - "sys.fulltext_index_columns_TSQL"
  - "fulltext_index_columns_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text indexes [SQL Server], columns"
  - "sys.fulltext_index_columns catalog view"
  - "full-text indexes [SQL Server], properties"
ms.assetid: c34b8625-e53c-4281-ace6-d46230d5cb84
aauthor: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fulltext_index_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Contains a row for each column that is part of a full-text index.    
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object of which this is part.|  
|**column_id**|**int**|ID of the column that is part of the full-text index.|  
|**type_column_id**|**int**|ID of the type column that stores the user-supplied document file extension-".doc", ".xls", and so forth-of the document in a given row. The type column is specified only for columns whose data requires filtering during full-text indexing. NULL if not applicable. For more information, see [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md).|  
|**language_id**|**int**|LCID of language whose word breaker is used to index this full-text column.<br /><br /> 0 = Neutral.<br /><br /> For more information, see [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md).|  
|**statistical_semantics**|**int**|1 = This column has statistical semantics enabled in addition to full-text indexing.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)]  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
