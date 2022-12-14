---
title: "sys.columns (Transact-SQL)"
description: sys.columns (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "sys.columns_TSQL"
  - "sys.columns"
helpviewer_keywords:
  - "sys.columns catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# sys.columns (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns a row for each column of an object that has columns, such as views or tables. The following is a list of object types that have columns:  
  
- Table-valued assembly functions (FT)  
  
- Inline table-valued SQL functions (IF)  
  
- Internal tables (IT)  
  
- System tables (S)  
  
- Table-valued SQL functions (TF)  
  
- User tables (U)  
  
- Views (V)  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object to which this column belongs.|  
|name|**sysname**|Name of the column. Is unique within the object.|  
|column_id|**int**|ID of the column. Is unique within the object.<br /><br /> Column IDs might not be sequential.|  
|system_type_id|**tinyint**|ID of the system type of the column.|  
|user_type_id|**int**|ID of the type of the column as defined by the user.<br /><br /> To return the name of the type, join to the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view on this column.|  
|max_length|**smallint**|Maximum length (in bytes) of the column.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br /> For **text**, **ntext**, and **image** columns, the max_length value will be 16 (representing the 16-byte pointer only) or the value set by sp_tableoption 'text in row'.|  
|precision|**tinyint**|Precision of the column if numeric-based; otherwise, 0.|  
|scale|**tinyint**|Scale of column if numeric-based; otherwise, 0.|  
|collation_name|**sysname**|Name of the collation of the column if character-based; otherwise `NULL`.|  
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
|generated_always_type|**tinyint**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]. 5, 6, 7, 8 only applies to [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Identifies when the column value is generated (will always be 0 for columns in system tables):<br /><br /> 0 = NOT_APPLICABLE<br /> 1 = AS_ROW_START<br /> 2 = AS_ROW_END<br />5 = AS_TRANSACTION_ID_START<br />6 = AS_TRANSACTION_ID_END<br />7 = AS_SEQUENCE_NUMBER_START<br />8 = AS_SEQUENCE_NUMBER_END<br /><br /> For more information, see [Temporal Tables &#40;Relational databases&#41;](../../relational-databases/tables/temporal-tables.md).|  
|generated_always_type_desc|**nvarchar(60)**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Textual description of `generated_always_type`'s value (always NOT_APPLICABLE for columns in system tables) <br /><br /> NOT_APPLICABLE<br /> AS_ROW_START<br /> AS_ROW_END<br /><br />**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]<br /><br />AS_TRANSACTION_ID_START<br />AS_TRANSACTION_ID_END<br />AS_SEQUENCE_NUMBER_START<br />AS_SEQUENCE_NUMBER_END|  
|encryption_type|**int**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Encryption type:<br /><br /> 1 = Deterministic encryption<br /><br /> 2 = Randomized encryption|  
|encryption_type_desc|**nvarchar(64)**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Encryption type description:<br /><br /> RANDOMIZED<br /><br /> DETERMINISTIC|  
|encryption_algorithm_name|**sysname**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Name of encryption algorithm.<br /><br /> Only AEAD_AES_256_CBC_HMAC_SHA_512 is supported.|  
|column_encryption_key_id|**int**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> ID of the CEK.|  
|column_encryption_key_database_name|**sysname**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDW_md](../../includes/sssds-md.md)].<br /><br /> The name of the database where the column encryption key exists if different than the database of the column. `NULL` if the key exists in the same database as the column.|  
|is_hidden|**bit**|**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Indicates if the column is hidden:<br /><br /> 0 = regular, not-hidden, visible column<br /><br /> 1 = hidden column|  
|is_masked|**bit**|**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Indicates if the column is masked by a dynamic data masking:<br /><br /> 0 = regular, not-masked column<br /><br /> 1 = column is masked|  
|graph_type |**int** |Internal column with a set of values. The values are between 1-8 for graph columns and `NULL` for others.  |
|graph_type_desc |**nvarchar(60)**  |internal column with a set of values |
|is_data_deletion_filter_column|**bit**|**Applies to**: Azure SQL Edge. Indicates if the column is the data retention filter column for the table.|
|ledger_view_column_type|**tinyint**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> If not NULL, indicates the type of a column in a ledger view:<br /><br /> 1 = TRANSACTION_ID<br /> 2 = SEQUENCE_NUMBER<br /> 3 = OPERATION_TYPE<br /> 4 = OPERATION_TYPE_DESC<br/><br/>For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).|
|ledger_view_column_type_desc|**nvarchar(60)**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> If not NULL, contains a textual description of the the type of a column in a ledger view:<br /><br /> TRANSACTION_ID<br /> SEQUENCE_NUMBER<br /> OPERATION_TYPE<br /> OPERATION_TYPE_DESC|
|is_dropped_ledger_table_column|**bit**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Indicates a ledger table column that has been dropped.|

## Permissions  

 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  

 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [sys.all_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-columns-transact-sql.md)   
 [sys.system_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-columns-transact-sql.md)  
