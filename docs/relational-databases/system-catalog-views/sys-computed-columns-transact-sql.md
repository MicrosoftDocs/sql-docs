---
description: "sys.computed_columns (Transact-SQL)"
title: "sys.computed_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/25/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
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
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.computed_columns (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains a row for each column found in **sys.columns** that is a computed-column.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<Inherited columns>**||The **sys.computed_columns** view returns all columns in the **sys.columns** view. It also returns the additional columns described below. For a description of the columns that the **sys.computed_columns** view inherits from **sys.columns**, see [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md). The value of the **is_computed** column is always set to 1 in the **sys.computed_columns** view.|  
|**definition**|**nvarchar(max)**|SQL text that defines this computed-column.|  
|**uses_database_collation**|**bit**|1 = The column definition depends on the default collation of the database for correct evaluation; otherwise, 0. Such a dependency prevents changing the database default collation.|  
|**is_persisted**|**bit**|Computed column is persisted.|  
|**generated_always_type**|**tinyint**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]. 7, 8, 9, 10 only applies to [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Identifies when the column value is generated (will always be 0 for columns in system tables):<br /><br /> 0 = NOT_APPLICABLE<br /> 1 = AS_ROW_START<br /> 2 = AS_ROW_END<br />7 = AS_TRANSACTION_ID_START<br />8 = AS_TRANSACTION_ID_END<br />9 = AS_SEQUENCE_NUMBER_START<br />10 = AS_SEQUENCE_NUMBER_END<br /><br /> For more information, see [Temporal Tables &#40;Relational databases&#41;](../../relational-databases/tables/temporal-tables.md).|  
|**generated_always_type_desc**|**nvarchar(60)**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Textual description of `generated_always_type`'s value (always NOT_APPLICABLE for columns in system tables) <br /><br /> NOT_APPLICABLE<br /> AS_ROW_START<br /> AS_ROW_END<br /><br />**Applies to**: [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]<br /><br />AS_TRANSACTION_ID_START<br />AS_TRANSACTION_ID_END<br />AS_SEQUENCE_NUMBER_START<br />AS_SEQUENCE_NUMBER_END|  
|**ledger_view_column_type**|**tinyint**|**Applies to**: [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> If not NULL, indicates the type of a column in a ledger view:<br /><br /> 1 = TRANSACTION_ID<br /> 2 = SEQUENCE_NUMBER<br /> 3 = OPERATION_TYPE<br /> 4 = OPERATION_TYPE_DESC<br/><br/>For more information on database ledger, see [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview).|
|**ledger_view_column_type_desc**|**nvarchar(60)**|**Applies to**: [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> If not NULL, contains a textual description of the the type of a column in a ledger view:<br /><br /> TRANSACTION_ID<br /> SEQUENCE_NUMBER<br /> OPERATION_TYPE<br /> OPERATION_TYPE_DESC|
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
