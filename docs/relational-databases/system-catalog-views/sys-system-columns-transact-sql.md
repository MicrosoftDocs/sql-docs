---
title: "sys.system_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "system_columns_TSQL"
  - "system_columns"
  - "sys.system_columns"
  - "sys.system_columns_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.system_columns catalog view"
ms.assetid: 4ab1d48a-d57a-4e76-a08c-9627eeaf4588
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.system_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each column of system objects that have columns.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object to which this column belongs.|  
|**name**|**sysname**|Name of the column. Is unique within the object.|  
|**column_id**|**int**|ID of the column. Is unique within the object.<br /><br /> Column IDs might not be sequential.|  
|**system_type_id**|**tinyint**|ID of the system-type of the column|  
|**user_type_id**|**int**|ID of the type of the column as defined by the user.<br /><br /> To return the name of the type, join to the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view on this column.|  
|**max_length**|**smallint**|Maximum length (in bytes) of column.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br /> For **text** columns, the **max_length** value will be 16 or the value set by **sp_tableoption** 'text in row'.|  
|**precision**|**tinyint**|Precision of the column if numeric-based; otherwise, 0.|  
|**scale**|**tinyint**|Scale of the column if numeric-based; otherwise, 0.|  
|**collation_name**|**sysname**|Name of the collation of the column if character-based; otherwise, NULL.|  
|**is_nullable**|**bit**|1 = Column is nullable.|  
|**is_ansi_padded**|**bit**|1 = Column uses ANSI_PADDING ON behavior if character, binary, or variant.<br /><br /> 0 = Column is not character, binary, or variant.|  
|**is_rowguidcol**|**bit**|1 = Column is a declared ROWGUIDCOL.|  
|**is_identity**|**bit**|1 = Column has identity values.|  
|**is_computed**|**bit**|1 = Column is a computed column.|  
|**is_filestream**|**bit**|1 = Column is declared to use filestream storage.|  
|**is_replicated**|**bit**|1 = Column is replicated.|  
|**is_non_sql_subscribed**|**bit**|1 = Column has a non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] subscriber.|  
|**is_merge_published**|**bit**|1 = Column is merge-published.|  
|**is_dts_replicated**|**bit**|1 = Column is replicated by using [!INCLUDE[ssIS](../../includes/ssis-md.md)].|  
|**is_xml_document**|**bit**|1 = Content is a complete XML document.<br /><br /> 0 = Content is a document fragment, or the column data type is not **xml**.|  
|**xml_collection_id**|**int**|Non-zero if the column data type is **xml** and the XML is typed. The value will be the ID of the collection containing the validating XML schema namespace of the column.<br /><br /> 0 = No XML schema collection.|  
|**default_object_id**|**int**|ID of the default object, regardless of whether it is a stand-alone [sys.sp_bindefault](../../relational-databases/system-stored-procedures/sp-bindefault-transact-sql.md), or an inline, column-level DEFAULT constraint. The **parent_object_id** column of an inline column-level default object is a reference back to the table itself. Or, 0 if there is no default.|  
|**rule_object_id**|**int**|ID of the stand-alone rule bound to the column by using **sys.sp_bindrule**.<br /><br /> 0 = No stand-alone rule.<br /><br /> For column-level CHECK constraints, see [sys.check_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-check-constraints-transact-sql.md).|  
|is_sparse|**bit**|1 = Column is a sparse column. For more information, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).|  
|is_column_set|**bit**|1 = Column is a column set. For more information, see [Use Column Sets](../../relational-databases/tables/use-column-sets.md).|  
|generated_always_type|**tinyint**|The numeric value representing the type of column:<br /><br /> 0 = NOT_APPLICABLE<br /><br /> 1 = AS_ROW_START<br /><br /> 2 = AS_ROW_END|  
|generated_always_type_desc|**nvarchar(60)**|The text description of the type of column:<br /><br /> NOT_APPLICABLE<br /><br /> AS_ROW_START<br /><br /> AS_ROW_END<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.all_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-columns-transact-sql.md)   
 [sys.computed_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-computed-columns-transact-sql.md)  
  
  
