---
title: "sys.columns (Transact-SQL)"
description: sys.columns (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/20/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.columns_TSQL"
  - "sys.columns"
helpviewer_keywords:
  - "sys.columns catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# sys.columns (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns a row for each column of an object that has columns, such as views or tables. The following list contains the object types that have columns:

- Table-valued assembly functions (FT)
- Inline table-valued SQL functions (IF)
- Internal tables (IT)
- System tables (S)
- Table-valued SQL functions (TF)
- User tables (U)
- Views (V)

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the object to which this column belongs. |
| `name` | **sysname** | Name of the column. Is unique within the object. |
| `column_id` | **int** | ID of the column. Is unique within the object. Column IDs might not be sequential. |
| `system_type_id` | **tinyint** | ID of the system type of the column. |
| `user_type_id` | **int** | ID of the type of the column as defined by the user. To return the name of the type, join to the [sys.types](sys-types-transact-sql.md) catalog view on this column. |
| `max_length` | **smallint** | Maximum length (in bytes) of the column.<br /><br />`-1` = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br />For **text**, **ntext**, and **image** columns, the max_length value is `16` (representing the 16-byte pointer only) or the value set by `sp_tableoption 'text in row'`. |
| `precision` | **tinyint** | Precision of the column if numeric-based; otherwise, `0`. |
| `scale` | **tinyint** | Scale of column if numeric-based; otherwise, `0`. |
| `collation_name` | **sysname** | Name of the collation of the column if character-based; otherwise `NULL`. |
| `is_nullable` | **bit** | `1` = Column is nullable<br />`0` = Column isn't nullable |
| `is_ansi_padded` | **bit** | `1` = Column uses `ANSI_PADDING ON` behavior if character, binary, or variant<br /><br />`0` = Column isn't character, binary, or variant |
| `is_rowguidcol` | **bit** | `1` = Column is a declared `ROWGUIDCOL` |
| `is_identity` | **bit** | `1` = Column has identity values |
| `is_computed` | **bit** | `1` = Column is a computed column |
| `is_filestream` | **bit** | `1` = Column is a FILESTREAM column |
| `is_replicated` | **bit** | `1` = Column is replicated |
| `is_non_sql_subscribed` | **bit** | `1` = Column has a non-SQL Server subscriber |
| `is_merge_published` | **bit** | `1` = Column is merge-published |
| `is_dts_replicated` | **bit** | `1` = Column is replicated by using [!INCLUDE [ssIS](../../includes/ssis-md.md)] |
| `is_xml_document` | **bit** | `1` = Content is a complete XML document<br /><br />`0` = Content is a document fragment, or the column data type isn't **xml** |
| `xml_collection_id` | **int** | Nonzero if the data type of the column is **xml** and the XML is typed. The value is the ID of the collection containing the validating XML schema namespace of the column<br /><br />`0` = No XML schema collection |
| `default_object_id` | **int** | ID of the default object, regardless of whether it's a stand-alone object [sp_bindefault](../system-stored-procedures/sp-bindefault-transact-sql.md), or an inline, column-level `DEFAULT` constraint. The parent_object_id column of an inline column-level default object is a reference back to the table itself.<br /><br />`0` = No default |
| `rule_object_id` | **int** | ID of the stand-alone rule bound to the column by using `sys.sp_bindrule.`<br /><br />`0` = No stand-alone rule. For column-level `CHECK` constraints, see [sys.check_constraints](sys-check-constraints-transact-sql.md). |
| `is_sparse` | **bit** | `1` = Column is a sparse column. For more information, see [Use sparse columns](../tables/use-sparse-columns.md). |
| `is_column_set` | **bit** | `1` = Column is a column set. For more information, see [Use sparse columns](../tables/use-sparse-columns.md). |
| `generated_always_type` | **tinyint** | Identifies when the column value is generated (is always `0` for columns in system tables).<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)].<br /><br />`0` = `NOT_APPLICABLE`<br />`1` = `AS_ROW_START`<br />`2` = `AS_ROW_END`<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)].<br /><br />`5` = `AS_TRANSACTION_ID_START`<br />`6` = `AS_TRANSACTION_ID_END`<br />`7` = `AS_SEQUENCE_NUMBER_START`<br />`8` = `AS_SEQUENCE_NUMBER_END`<br /><br />For more information, see [Temporal Tables (Relational databases)](../tables/temporal-tables.md). |
| `generated_always_type_desc` | **nvarchar(60)** | Textual description of the `generated_always_type` value (always `NOT_APPLICABLE` for columns in system tables)<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)].<br /><br />`NOT_APPLICABLE`<br />`AS_ROW_START`<br />`AS_ROW_END`<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)].<br /><br />`AS_TRANSACTION_ID_START`<br />`AS_TRANSACTION_ID_END`<br />`AS_SEQUENCE_NUMBER_START`<br />`AS_SEQUENCE_NUMBER_END` |
| `encryption_type` | **int** | Encryption type:<br /><br />`1` = Deterministic encryption<br />`2` = Randomized encryption<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `encryption_type_desc` | **nvarchar(64)** | Encryption type description:<br /><br />`RANDOMIZED`<br />`DETERMINISTIC`<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `encryption_algorithm_name` | **sysname** | Name of encryption algorithm. Only `AEAD_AES_256_CBC_HMAC_SHA_512` is supported.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `column_encryption_key_id` | **int** | ID of the column encryption key (CEK).<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `column_encryption_key_database_name` | **sysname** | The name of the database where the column encryption key exists if different than the database of the column. `NULL` if the key exists in the same database as the column.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssazuresynapse-md](../../includes/sssds-md.md)] |
| `is_hidden` | **bit** | Indicates if the column is hidden:<br /><br />`0` = regular, not-hidden, visible column<br />`1` = hidden column<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `is_masked` | **bit** | Indicates if the column is masked by dynamic data masking:<br /><br />`0` = regular, not-masked column<br />`1` = column is masked<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `graph_type` | **int** | Internal column with a set of values. The values are between `1` and `8` for graph columns, and `NULL` for others. |
| `graph_type_desc` | **nvarchar(60)** | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `is_data_deletion_filter_column` | **bit** | Indicates if the column is the data retention filter column for the table.<br /><br />**Applies to**: Azure SQL Edge |
| `ledger_view_column_type` | **int** | If not `NULL`, indicates the type of a column in a ledger view:<br /><br />`1` = `TRANSACTION_ID`<br />`2` = `SEQUENCE_NUMBER`<br />`3` = `OPERATION_TYPE`<br />`4` = `OPERATION_TYPE_DESC`<br /><br />For more information, see [Ledger overview](/azure/azure-sql/database/ledger-overview).<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `ledger_view_column_type_desc` | **nvarchar(60)** | If not `NULL`, contains a textual description of the the type of a column in a ledger view:<br /><br />`TRANSACTION_ID`<br />`SEQUENCE_NUMBER`<br />`OPERATION_TYPE`<br />`OPERATION_TYPE_DESC`<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `is_dropped_ledger_column` | **bit** | Indicates a ledger table column that was dropped.<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Related content

- [System Views (Transact-SQL)](../../t-sql/language-reference.md)
- [Object Catalog Views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [sys.all_columns (Transact-SQL)](sys-all-columns-transact-sql.md)
- [sys.system_columns (Transact-SQL)](sys-system-columns-transact-sql.md)
