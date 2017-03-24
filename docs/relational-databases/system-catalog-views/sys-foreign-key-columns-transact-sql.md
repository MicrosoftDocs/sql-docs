---
title: "sys.foreign_key_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.foreign_key_columns"
  - "foreign_key_columns"
  - "sys.foreign_key_columns_TSQL"
  - "foreign_key_columns_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.foreign_key_columns catalog view"
ms.assetid: 7247f065-5441-4bcf-9f25-c84a03290dc6
caps.latest.revision: 22
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.foreign_key_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each column, or set of columns, that comprise a foreign key.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**constraint_object_id**|**int**|ID of the FOREIGN KEY constraint.|  
|**constraint_column_id**|**int**|ID of the column, or set of columns, that comprise the FOREIGN KEY (*1..n* where n=number of columns).|  
|**parent_object_id**|**int**|ID of the parent of the constraint, which is the referencing object.|  
|**parent_column_id**|**int**|ID of the parent column, which is the referencing column.|  
|**referenced_object_id**|**int**|ID of the referenced object, which has the candidate key.|  
|**referenced_column_id**|**int**|ID of the referenced column (candidate key column).|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)  
  
  