---
title: "sys.all_columns (Transact-SQL)"
description: sys.all_columns (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "all_columns_TSQL"
  - "all_columns"
  - "sys.all_columns_TSQL"
  - "sys.all_columns"
helpviewer_keywords:
  - "sys.all_columns catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 40e04fe9-0b64-4799-84c0-57f128b2bdc2
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.all_columns (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Shows the union of all columns belonging to user-defined objects and system objects.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object to which this column belongs.|  
|name|**sysname**|Name of the column. Is unique within the object.|  
|column_id|**int**|ID of the column. Is unique within the object.<br /><br /> Column IDs might not be sequential.|  
|system_type_id|**tinyint**|ID of the system-type of the column.|  
|user_type_id|**int**|ID of the type of the column as defined by the user.<br /><br /> To return the name of the type, join to the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view on this column.|  
|max_length|**smallint**|Maximum length (in bytes) of the column.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br /> For **text** columns, the max_length value will be 16 or the value set by sp_tableoption 'text in row'.|  
|precision|**tinyint**|Precision of the column if numeric-based; otherwise, 0.|  
|scale|**tinyint**|Scale of the column if numeric-based; otherwise, 0.|  
|collation_name|**sysname**|Name of the collation of the column if character-based; otherwise, NULL.|  
|is_nullable|**bit**|1 = Column is nullable.|  
|is_ansi_padded|**bit**|1 = Column uses ANSI_PADDING ON behavior if character, binary, or variant.<br /><br /> 0 = Column is not character, binary, or variant.|  
|is_rowguidcol|**bit**|1 = Column is a declared ROWGUIDCOL.|  
|is_identity|**bit**|1 = Column has identity values|  
|is_computed|**bit**|1 = Column is a computed column.|  
|is_filestream|**bit**|1 = Column is declared to use filestream storage.|  
|is_replicated|**bit**|1 = Column is replicated.|  
|is_non_sql_subscribed|**bit**|1 = Column has a non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] subscriber.|  
|is_merge_published|**bit**|1 = Column is merge-published.|  
|is_dts_replicated|**bit**|1 = Column is replicated by using [!INCLUDE[ssIS](../../includes/ssis-md.md)].|  
|is_xml_document|**bit**|1 = Content is a complete XML document.<br /><br /> 0 = Content is a document fragment, or the column data type is not XML.|  
|xml_collection_id|**int**|Non-zero if the column's data type is **xml** and the XML is typed. The value will be the ID of the collection containing the column's validating XML schema namespace<br /><br /> 0 = no XML schema collection.|  
|default_object_id|**int**|ID of the default object, regardless of whether it is a stand-alone [sys.sp_bindefault](../../relational-databases/system-stored-procedures/sp-bindefault-transact-sql.md), or an in-line, column-level DEFAULT constraint. The parent_object_id column of an inline column-level default object is a reference back to the table itself.<br /><br /> 0 = No default.|  
|rule_object_id|**int**|ID of the stand-alone rule bound to the column by using sys.sp_bindrule.<br /><br /> 0 = No stand-alone rule.<br /><br /> For column-level CHECK constraints, see [sys.check_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-check-constraints-transact-sql.md).|  
|is_sparse|bit|1 = Column is a sparse column. For more information, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).|  
|is_column_set|bit|1 = Column is a column set. For more information, see [Use Column Sets](../../relational-databases/tables/use-column-sets.md).|  
|generated_always_type|**tinyint**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]. 7, 8, 9, 10 only applies to [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Identifies when the column value is generated (will always be 0 for columns in system tables):<br /><br /> 0 = NOT_APPLICABLE<br /> 1 = AS_ROW_START<br /> 2 = AS_ROW_END<br />7 = AS_TRANSACTION_ID_START<br />8 = AS_TRANSACTION_ID_END<br />9 = AS_SEQUENCE_NUMBER_START<br />10 = AS_SEQUENCE_NUMBER_END<br /><br /> For more information, see [Temporal Tables &#40;Relational databases&#41;](../../relational-databases/tables/temporal-tables.md).|  
|generated_always_type_desc|**nvarchar(60)**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Textual description of `generated_always_type`'s value (always NOT_APPLICABLE for columns in system tables) <br /><br /> NOT_APPLICABLE<br /> AS_ROW_START<br /> AS_ROW_END<br /><br />**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]<br /><br />AS_TRANSACTION_ID_START<br />AS_TRANSACTION_ID_END<br />AS_SEQUENCE_NUMBER_START<br />AS_SEQUENCE_NUMBER_END|   
|ledger_view_column_type|**tinyint**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> If not NULL, indicates the type of a column in a ledger view:<br /><br /> 1 = TRANSACTION_ID<br /> 2 = SEQUENCE_NUMBER<br /> 3 = OPERATION_TYPE<br /> 4 = OPERATION_TYPE_DESC<br/><br/>For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).|
|ledger_view_column_type_desc|**nvarchar(60)**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> If not NULL, contains a textual description of the the type of a column in a ledger view:<br /><br /> TRANSACTION_ID<br /> SEQUENCE_NUMBER<br /> OPERATION_TYPE<br /> OPERATION_TYPE_DESC|
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.system_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-columns-transact-sql.md)   
 [sys.computed_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-computed-columns-transact-sql.md)  
  
  
