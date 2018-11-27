---
title: "sys.columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/21/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.columns_TSQL"
  - "sys.columns"
  - "columns_TSQL"
  - "columns"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.columns catalog view"
ms.assetid: 323ac9ea-fc52-4b8c-8a7e-e0e44f8ed86c
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a row for each column of an object that has columns, such as views or tables. The following is a list of object types that have columns:  
  
-   Table-valued assembly functions (FT)  
  
-   Inline table-valued SQL functions (IF)  
  
-   Internal tables (IT)  
  
-   System tables (S)  
  
-   Table-valued SQL functions (TF)  
  
-   User tables (U)  
  
-   Views (V)  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object to which this column belongs.|  
|name|**sysname**|Name of the column. Is unique within the object.|  
|column_id|**int**|ID of the column. Is unique within the object.<br /><br /> Column IDs might not be sequential.|  
|system_type_id|**tinyint**|ID of the system type of the column.|  
|user_type_id|**int**|ID of the type of the column as defined by the user.<br /><br /> To return the name of the type, join to the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view on this column.|  
|max_length|**smallint**|Maximum length (in bytes) of the column.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br /> For **text** columns, the max_length value will be 16 or the value set by sp_tableoption 'text in row'.|  
|precision|**tinyint**|Precision of the column if numeric-based; otherwise, 0.|  
|scale|**tinyint**|Scale of column if numeric-based; otherwise, 0.|  
|collation_name|**sysname**|Name of the collation of the column if character-based; otherwise, NULL.|  
|is_nullable|**bit**|1 = Column is nullable.|  
|is_ansi_padded|**bit**|1 = Column uses ANSI_PADDING ON behavior if character, binary, or variant.<br /><br /> 0 = Column is not character, binary, or variant.|  
|is_rowguidcol|**bit**|1 = Column is a declared ROWGUIDCOL.|  
|is_identity|**bit**|1 = Column has identity values|  
|is_computed|**bit**|1 = Column is a computed column.|  
|is_filestream|**bit**|1 = Column is a FILESTREAM column.|  
|is_replicated|**bit**|1 = Column is replicated.|  
|is_non_sql_subscribed|**bit**|1 = Column has a non-SQL Server subscriber.|  
|is_merge_published|**bit**|1 = Column is merge-published.|  
|is_dts_replicated|**bit**|1 = Column is replicated by using [!INCLUDE[ssIS](../../includes/ssis-md.md)].|  
|is_xml_document|**bit**|1 = Content is a complete XML document.<br /><br /> 0 = Content is a document fragment or the column data type is not **xml**.|  
|xml_collection_id|**int**|Nonzero if the data type of the column is **xml** and the XML is typed. The value will be the ID of the collection containing the validating XML schema namespace of the column.<br /><br /> 0 = No XML schema collection.|  
|default_object_id|**int**|ID of the default object, regardless of whether it is a stand-alone object [sys.sp_bindefault](../../relational-databases/system-stored-procedures/sp-bindefault-transact-sql.md), or an inline, column-level DEFAULT constraint. The parent_object_id column of an inline column-level default object is a reference back to the table itself.<br /><br /> 0 = No default.|  
|rule_object_id|**int**|ID of the stand-alone rule bound to the column by using sys.sp_bindrule.<br /><br /> 0 = No stand-alone rule. For column-level CHECK constraints, see [sys.check_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-check-constraints-transact-sql.md).|  
|is_sparse|**bit**|1 = Column is a sparse column. For more information, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).|  
|is_column_set|**bit**|1 = Column is a column set. For more information, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).|  
|generated_always_type|**tinyint**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Identifies when the column value is generated (will always be 0 for columns in system tables):<br /><br /> 0 = NOT_APPLICABLE<br /><br /> 1 = AS_ROW_START<br /><br /> 2 = AS_ROW_END<br /><br /> For more information, see [Temporal Tables &#40;Relational databases&#41;](../../relational-databases/tables/temporal-tables.md).|  
|generated_always_type_desc|**nvarchar(60)**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Textual description of `generated_always_type`'s value (always NOT_APPLICABLE for columns in system tables) <br /><br /> NOT_APPLICABLE<br /><br /> AS_ROW_START<br /><br /> AS_ROW_END|  
|encryption_type|**int**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Encryption type:<br /><br /> 1 = Deterministic encryption<br /><br /> 2 = Randomized encryption|  
|encryption_type_desc|**nvarchar(64)**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Encryption type description:<br /><br /> RANDOMIZED<br /><br /> DETERMINISTIC|  
|encryption_algorithm_name|**sysname**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Name of encryption algorithm.<br /><br /> Only AEAD_AES_256_CBC_HMAC_SHA_512 is supported.|  
|column_encryption_key_id|**int**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> ID of the CEK.|  
|column_encryption_key_database_name|**sysname**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDW_md](../../includes/sssds-md.md)].<br /><br /> The name of the database where the column encryption key exists if different than the database of the column. NULL if the key exists in the same database as the column.|  
|is_hidden|**bit**|**Applies to**: [!INCLUDE[ssCurrentLong](../../includes/sscurrent-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Indicates if the column is hidden:<br /><br /> 0 = regular, not-hidden, visible column<br /><br /> 1 = hidden column|  
|is_masked|**bit**|**Applies to**: [!INCLUDE[ssCurrentLong](../../includes/sscurrent-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Indicates if the column is masked by a dynamic data masking:<br /><br /> 0 = regular, not-masked column<br /><br /> 1 = column is masked|  


 
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)   
 [sys.all_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-columns-transact-sql.md)   
 [sys.system_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-columns-transact-sql.md)  
  
  
