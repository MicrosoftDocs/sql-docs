---
title: "sp_sproc_columns (Transact-SQL)"
description: Returns column information for a single stored procedure or user-defined function in the current environment.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_sproc_columns"
  - "sp_sproc_columns_TSQL"
helpviewer_keywords:
  - "sp_sproc_columns"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sp_sproc_columns (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns column information for a single stored procedure or user-defined function in the current environment.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_sproc_columns
    [ [ @procedure_name = ] N'procedure_name' ]
    [ , [ @procedure_owner = ] N'procedure_owner' ]
    [ , [ @procedure_qualifier = ] N'procedure_qualifier' ]
    [ , [ @column_name = ] N'column_name' ]
    [ , [ @ODBCVer = ] ODBCVer ]
    [ , [ @fUsePattern = ] fUsePattern ]
[ ; ]
```

## Arguments

#### [ @procedure_name = ] N'*procedure_name*'

The name of the procedure used to return catalog information. *@procedure_name* is **nvarchar(390)**, with a default of `%`, which means all tables in the current database. Wildcard pattern matching is supported.

#### [ @procedure_owner = ] N'*procedure_owner*'

The name of the owner of the procedure. *@procedure_owner* is **nvarchar(384)**, with a default of `NULL`. Wildcard pattern matching is supported. If *@procedure_owner* isn't specified, the default procedure visibility rules of the underlying database management system (DBMS) apply.

If the current user owns a procedure with the specified name, information about that procedure is returned. If *@procedure_owner* isn't specified and the current user doesn't own a procedure with the specified name, `sp_sproc_columns` looks for a procedure with the specified name that is owned by the database owner. If the procedure exists, information about its columns is returned.

#### [ @procedure_qualifier = ] N'*procedure_qualifier*'

The name of the procedure qualifier. *@procedure_qualifier* is **sysname**, with a default of `NULL`. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this parameter represents the database name. In some products, it represents the server name of the table's database environment.

#### [ @column_name = ] N'*column_name*'

A single column and is used when only one column of catalog information is desired. *@column_name* is **nvarchar(384)**, with a default of `NULL`. If *@column_name* is omitted, all columns are returned. Wildcard pattern matching is supported. For maximum interoperability, the gateway client should assume only ISO standard pattern matching (the % and _ wildcard characters).

#### [ @ODBCVer = ] *ODBCVer*

The version of ODBC being used. *@ODBCVer* is **int**, with a default of `2`, which indicates ODBC version 2.0. For more information about the differences between ODBC version 2.0 and ODBC version 3.0, see the ODBC `SQLProcedureColumns` specification for ODBC version 3.0.

#### [ @fUsePattern = ] *fUsePattern*

Determines whether the underscore (`_`), percent (`%`), and bracket (`[` and `]`) characters are interpreted as wildcard characters. *@fUsePattern* is **bit**, with a default of `1`. Valid values are `0` (pattern matching is off) and `1` (pattern matching is on).

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `PROCEDURE_QUALIFIER` | **sysname** | Procedure qualifier name. This column can be `NULL`. |
| `PROCEDURE_OWNER` | **sysname** | Procedure owner name. This column always returns a value. |
| `PROCEDURE_NAME` | **nvarchar(134)** | Procedure name. This column always returns a value. |
| `COLUMN_NAME` | **sysname** | Column name for each column of the `TABLE_NAME` returned. This column always returns a value. |
| `COLUMN_TYPE` | **smallint** | This field always returns a value:<br /><br />0 = `SQL_PARAM_TYPE_UNKNOWN`<br />1 = `SQL_PARAM_TYPE_INPUT`<br />2 = `SQL_PARAM_TYPE_OUTPUT`<br />3 = `SQL_RESULT_COL`<br />4 = `SQL_PARAM_OUTPUT`<br />5 = `SQL_RETURN_VALUE` |
| `DATA_TYPE` | **smallint** | Integer code for an ODBC data type. If this data type can't be mapped to an ISO type, the value is `NULL`. The native data type name is returned in the `TYPE_NAME` column. |
| `TYPE_NAME` | **sysname** | String representation of the data type. This value is the data type name as presented by the underlying DBMS. |
| `PRECISION` | **int** | Number of significant digits. The return value for the `PRECISION` column is in base 10. |
| `LENGTH` | **int** | Transfer size of the data. |
| `SCALE` | **smallint** | Number of digits to the right of the decimal point. |
| `RADIX` | **smallint** | The base for numeric types. |
| `NULLABLE` | **smallint** | Specifies nullability:<br /><br />`1` = Data type can be created allowing null values.<br />`0` = Null values aren't allowed. |
| `REMARKS` | **varchar(254)** | Description of the procedure column. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't return a value for this column. |
| `COLUMN_DEF` | **nvarchar(4000)** | Default value of the column. |
| `SQL_DATA_TYPE` | **smallint** | Value of the SQL data type as it appears in the `TYPE` field of the descriptor. This column is the same as the `DATA_TYPE` column, except for the **datetime** and ISO **interval** data types. This column always returns a value. |
| `SQL_DATETIME_SUB` | **smallint** | The **datetime** ISO **interval** subcode if the value of `SQL_DATA_TYPE` is `SQL_DATETIME` or `SQL_INTERVAL`. For data types other than **datetime** and ISO **interval**, this field is `NULL`. |
| `CHAR_OCTET_LENGTH` | **int** | Maximum length in bytes of a **character** or **binary** data type column. For all other data types, this column returns a `NULL`. |
| `ORDINAL_POSITION` | **int** | Ordinal position of the column in the table. The first column in the table is `1`. This column always returns a value. |
| `IS_NULLABLE` | **varchar(254)** | Nullability of the column in the table. ISO rules are followed to determine nullability. An ISO compliant DBMS can't return an empty string.<br /><br />Displays `YES` if the column can include nulls, and `NO` if the column can't include nulls.<br /><br />This column returns a zero-length string if nullability is unknown.<br /><br />The value returned for this column is different from the value returned for the `NULLABLE` column. |
| `SS_DATA_TYPE` | **tinyint** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type used by extended stored procedures. For more information, see [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md). |

## Remarks

`sp_sproc_columns` is equivalent to `SQLProcedureColumns` in ODBC. The results returned are ordered by `PROCEDURE_QUALIFIER`, `PROCEDURE_OWNER`, `PROCEDURE_NAME`, and the order that the parameters appear in the procedure definition.

## Permissions

Requires `SELECT` permission on the schema.

## Related content

- [Catalog stored procedures (Transact-SQL)](catalog-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
