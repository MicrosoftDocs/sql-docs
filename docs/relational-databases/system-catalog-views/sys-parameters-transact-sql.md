---
title: "sys.parameters (Transact-SQL)"
description: sys.parameters (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/28/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.parameters_TSQL"
  - "sys.parameters"
  - "parameters"
  - "parameters_TSQL"
helpviewer_keywords:
  - "sys.parameters catalog view"
  - "table-valued parameters,sys.parameters"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.parameters (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Contains a row for each parameter of an object that accepts parameters. If the object is a scalar function, there's also a single row describing the return value. That row has a `parameter_id` value of `0`.

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the object to which this parameter belongs. |
| `name` | **sysname** | Name of the parameter. Is unique within the object.<br /><br />If the object is a scalar function, the parameter name is an empty string in the row representing the return value. |
| `parameter_id` | **int** | ID of the parameter. Is unique within the object.<br /><br />If the object is a scalar function, `parameter_id = 0` represents the return value. |
| `system_type_id` | **tinyint** | ID of the system type of the parameter. |
| `user_type_id` | **int** | ID of the type of the parameter as defined by the user.<br /><br />To return the name of the type, join to the [sys.types](sys-types-transact-sql.md) catalog view on this column. |
| `max_length` | **smallint** | Maximum length of the parameter, in bytes.<br /><br />Value = `-1` when the column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**. |
| `precision` | **tinyint** | Precision of the parameter if numeric-based; otherwise, `0`. |
| `scale` | **tinyint** | Scale of the parameter if numeric-based; otherwise, `0`. |
| `is_output` | **bit** | `1` = Parameter is `OUTPUT` or `RETURN`; otherwise, `0`. |
| `is_cursor_ref` | **bit** | `1` = Parameter is a cursor-reference parameter. |
| `has_default_value` | **bit** | `1` = Parameter has default value.<br /><br />[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] only maintains default values for CLR objects in this catalog view; therefore, this column has a value of `0` for [!INCLUDE [tsql](../../includes/tsql-md.md)] objects. To view the default value of a parameter in a [!INCLUDE [tsql](../../includes/tsql-md.md)] object, query the `definition` column of the [sys.sql_modules](sys-sql-modules-transact-sql.md) catalog view, or use the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) system function. |
| `is_xml_document` | **bit** | `1` = Content is a complete XML document.<br /><br />`0` = Content is a document fragment, or the data type of the column isn't **xml**. |
| `default_value` | **sql_variant** | If `has_default_value` is `1`, the value of this column is the value of the default for the parameter; otherwise `NULL`. |
| `xml_collection_id` | **int** | Non-zero if the data type of the parameter is **xml** and the XML is typed. The value is the ID of the collection containing the validating XML schema namespace of the parameter.<br /><br />`0` = No XML schema collection. |
| `is_readonly` | **bit** | `1` = Parameter is `READONLY`; otherwise, `0`. |
| `is_nullable` | **bit** | `1` = Parameter is nullable. (the default).<br /><br />`0` = Parameter isn't nullable, for more efficient execution of natively compiled stored procedures. |
| `encryption_type` | **int** | Encryption type:<br /><br />`1` = Deterministic encryption<br />`2` = Randomized encryption<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. |
| `encryption_type_desc` | **nvarchar(64)** | Encryption type description:<br /><br />`RANDOMIZED`<br />`DETERMINISTIC`<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. |
| `encryption_algorithm_name` | **sysname** | Name of encryption algorithm.<br /><br />Only `AEAD_AES_256_CBC_HMAC_SHA_512` is supported.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. |
| `column_encryption_key_id` | **int** | ID of the CEK.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. |
| `column_encryption_key_database_name` | **sysname** | The name of the database where the column encryption key exists if different than the database of the column. `NULL` if the key exists in the same database as the column.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE [ssazuresynapse-md](../../includes/sssds-md.md)]. |
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
- [sys.all_parameters (Transact-SQL)](sys-all-parameters-transact-sql.md)
- [sys.system_parameters (Transact-SQL)](sys-system-parameters-transact-sql.md)
