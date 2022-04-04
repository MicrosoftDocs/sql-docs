---
description: "sys.foreign_key_columns (Transact-SQL)"
title: "sys.foreign_key_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
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
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.foreign_key_columns (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

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
  
## Examples  
 The following example shows how to retrieve all foreign keys in the database, with their related tables & columns.
  
```  
select	fk.name as ForeignKeyName
      , t_parent.name as ParentTableName
      , c_parent.name as ParentColumnName
      , t_child.name as ReferencedTableName
      , c_child.name as ReferencedColumnName
from sys.foreign_keys fk 
inner join sys.foreign_key_columns fkc on fkc.constraint_object_id = fk.object_id
inner join sys.tables t_parent on t_parent.object_id = fk.parent_object_id
inner join sys.columns c_parent on fkc.parent_column_id = c_parent.column_id  
                               and c_parent.object_id = t_parent.object_id 
inner join sys.tables t_child on t_child.object_id = fk.referenced_object_id
inner join sys.columns c_child on c_child.object_id = t_child.object_id
                              and fkc.referenced_column_id = c_child.column_id
order by t_parent.name, c_parent.name 
  
```  
  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)  
  
  
