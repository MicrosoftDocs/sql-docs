---
title: "sys.all_columns (Transact-SQL)"
description: sys.all_columns shows the union of all columns belonging to user-defined objects and system objects.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/28/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "all_columns_TSQL"
  - "all_columns"
  - "sys.all_columns_TSQL"
  - "sys.all_columns"
helpviewer_keywords:
  - "sys.all_columns catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.all_columns (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Shows the union of all columns belonging to user-defined objects and system objects.

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the object to which this column belongs. |
| `name` | **sysname** | Name of the column. Is unique within the object. |
| `column_id` | **int** | ID of the column. Is unique within the object.<br /><br />Column IDs might not be sequential. |
| `system_type_id` | **tinyint** | ID of the system-type of the column. |
| `user_type_id` | **int** | ID of the type of the column as defined by the user.<br /><br />To return the name of the type, join to the [sys.types](sys-types-transact-sql.md) catalog view on this column. |
| `max_length` | **smallint** | Maximum length (in bytes) of the column.<br /><br />`-1` = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br />For **text** columns, the `max_length` value is `16` or the value set by `sp_tableoption 'text in row'`. |
| `precision` | **tinyint** | Precision of the column if numeric-based; otherwise, `0`. |
| `scale` | **tinyint** | Scale of the column if numeric-based; otherwise, `0`. |
| `collation_name` | **sysname** | Name of the collation of the column if character-based; otherwise, `NULL`. |
| `is_nullable` | **bit** | `1` = Column is nullable. |
| `is_ansi_padded` | **bit** | `1` = Column uses `ANSI_PADDING ON` behavior if character, binary, or variant.<br /><br />`0` = Column isn't character, binary, or variant. |
| `is_rowguidcol` | **bit** | `1` = Column is a declared `ROWGUIDCOL`. |
| `is_identity` | **bit** | `1` = Column has identity values |
| `is_computed` | **bit** | `1` = Column is a computed column. |
| `is_filestream` | **bit** | `1` = Column is declared to use FILESTREAM storage. |
| `is_replicated` | **bit** | `1` = Column is replicated. |
| `is_non_sql_subscribed` | **bit** | `1` = Column has a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] subscriber. |
| `is_merge_published` | **bit** | `1` = Column is merge-published. |
| `is_dts_replicated` | **bit** | `1` = Column is replicated by using [!INCLUDE [ssIS](../../includes/ssis-md.md)]. |
| `is_xml_document` | **bit** | `1` = Content is a complete XML document.<br /><br />`0` = Content is a document fragment, or the column data type isn't XML. |
| `xml_collection_id` | **int** | Non-zero if the column's data type is **xml** and the XML is typed. The value is the ID of the collection containing the column's validating XML schema namespace.<br /><br />`0` = no XML schema collection. |
| `default_object_id` | **int** | ID of the default object, regardless of whether it's a stand-alone [sys.sp_bindefault](../system-stored-procedures/sp-bindefault-transact-sql.md), or an in-line, column-level `DEFAULT` constraint. The `parent_object_id` column of an inline column-level default object is a reference back to the table itself.<br /><br />`0` = No default. |
| `rule_object_id` | **int** | ID of the stand-alone rule bound to the column by using `sys.sp_bindrule`.<br /><br />`0` = No stand-alone rule.<br /><br />For column-level `CHECK` constraints, see [sys.check_constraints](sys-check-constraints-transact-sql.md). |
| `is_sparse` | **bit** | `1` = Column is a sparse column. For more information, see [Use sparse columns](../tables/use-sparse-columns.md). |
| `is_column_set` | **bit** | `1` = Column is a column set. For more information, see [Use column sets](../tables/use-column-sets.md). |
| `generated_always_type` | **tinyint** | Identifies when the column value is generated (always `0` for columns in system tables):<br /><br />`0` = `NOT_APPLICABLE`<br />`1` = `AS_ROW_START`<br />`2` = `AS_ROW_END`<br />`7` = `AS_TRANSACTION_ID_START`<br />`8` = `AS_TRANSACTION_ID_END`<br />`9` = `AS_SEQUENCE_NUMBER_START`<br />`10` = `AS_SEQUENCE_NUMBER_END`<br /><br />For more information, see [Temporal tables](../tables/temporal-tables.md).<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. `7`, `8`, `9`, `10` only applies to [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. |
| `generated_always_type_desc` | **nvarchar(60)** | **Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)].<br /><br />Textual description of the `generated_always_type` value (always `NOT_APPLICABLE` for columns in system tables)<br /><br />`NOT_APPLICABLE`<br />`AS_ROW_START`<br />`AS_ROW_END`<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]<br /><br />`AS_TRANSACTION_ID_START`<br />`AS_TRANSACTION_ID_END`<br />`AS_SEQUENCE_NUMBER_START`<br />`AS_SEQUENCE_NUMBER_END` |
| `ledger_view_column_type` | **tinyint** | If not `NULL`, indicates the type of a column in a ledger view:<br /><br />`1` = `TRANSACTION_ID`<br />`2` = `SEQUENCE_NUMBER`<br />`3` = `OPERATION_TYPE`<br />`4` = `OPERATION_TYPE_DESC`<br /><br />For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. |
| `ledger_view_column_type_desc` | **nvarchar(60)** | If not `NULL`, contains a textual description of the type of a column in a ledger view:<br /><br />`TRANSACTION_ID`<br />`SEQUENCE_NUMBER`<br />`OPERATION_TYPE`<br />`OPERATION_TYPE_DESC`<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. |
| `is_dropped_ledger_column` | **bit** | Indicates a ledger table column that was dropped.<br /><br />**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `vector_dimensions` | **int** | Indicates how many dimensions the vector has.<br /><br />**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `vector_base_type` | **tinyint** | Indicates the data type used to store vector dimensions values.<br /><br />`0` = 32-bit (single-precision) float<br />`1` = 16-bit (half-precision) float <sup>1</sup><br /><br />**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |
| `vector_base_type_desc` | **nvarchar(10)** | Contains the textual description of the data type used to store vector dimensions values.<br /><br />**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, and [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)] |

<sup>1</sup> For more information, see [Half-precision floating-point format](https://wikipedia.org/wiki/Half-precision_floating-point_format).

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
- [sys.columns (Transact-SQL)](sys-columns-transact-sql.md)
- [sys.system_columns (Transact-SQL)](sys-system-columns-transact-sql.md)
- [sys.computed_columns (Transact-SQL)](sys-computed-columns-transact-sql.md)
