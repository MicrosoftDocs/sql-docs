---
title: "sp_special_columns_100 (Azure Synapse Analytics)"
description: sp_special_columns_100 returns the optimal set of columns that uniquely identify a row in the table.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest"
---
# sp_special_columns_100 (Azure Synapse Analytics)

[!INCLUDE [applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Returns the optimal set of columns that uniquely identify a row in the table. Also returns columns automatically updated when any value in the row is updated by a transaction.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for Azure Synapse Analytics and Analytics Platform System (PDW).

```syntaxsql
sp_special_columns_100
    [ @table_name = ] N'table_name'
    [ , [ @table_owner = ] N'table_owner' ]
    [ , [ @table_qualifier = ] N'table_qualifier' ]
    [ , [ @col_type = ] 'col_type' ]
    [ , [ @scope = ] 'scope' ]
    [ , [ @nullable = ] 'nullable' ]
    [ , [ @ODBCVer = ] ODBCVer ]
[ ; ]
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### [ @table_name = ] N'*table_name*'

The name of the table used to return catalog information. *@table_name* is **sysname**, with no default. Wildcard pattern matching isn't supported.

#### [ @table_owner = ] N'*table_owner*'

The table owner of the table used to return catalog information. *@table_owner* is **sysname**, with a default of `NULL`. Wildcard pattern matching isn't supported. If *@table_owner* isn't specified, the default table visibility rules of the underlying database management system (DBMS) apply.

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if the current user owns a table with the specified name, the columns of that table are returned. If *@table_owner* isn't specified, and the current user doesn't own a table of the specified *@table_name*, this procedure looks for a table of the specified *@table_name* owned by the database owner. If the table exists, its columns are returned.

#### [ @table_qualifier = ] N'*table_qualifier*'

The name of the table qualifier. *@table_qualifier* is **sysname**, with a default of `NULL`. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the database environment of the table.

#### [ @col_type = ] '*col_type*'

The column type. *@col_type* is **char(1)**, with a default of `R`.

- Type `R` returns the optimal column or set of columns that, by retrieving values from the column or columns, allows for any row in the specified table to be uniquely identified. A column can be either a pseudocolumn designed for this purpose, or the column or columns of any unique index for the table.

- Type `V` returns the column or columns in the specified table, if any, that are automatically updated by the data source when any value in the row is updated by any transaction.

#### [ @scope = ] '*scope*'

The minimum required scope of the ROWID. *@scope* is **char(1)**, with a default of `T`.

- Scope `C` specifies that the ROWID is valid only when positioned on that row.
- Scope `T` specifies that the ROWID is valid for the transaction.

#### [ @nullable = ] '*nullable*'

Specifies whether the special columns can accept a `NULL` value. *@nullable* is **char(1)**, with a default of `U`.

- `O` specifies special columns that don't allow null values.
- `U` specifies columns that are partially nullable.

#### [ @ODBCVer = ] *ODBCVer*

The ODBC version being used. *@ODBCVer* is **int**, with a default of `2`. This value indicates ODBC version 2.0. For more information about the difference between ODBC version 2.0 and ODBC version 3.0, see the ODBC `SQLSpecialColumns` specification for ODBC version 3.0.

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `SCOPE` | **smallint** | Actual scope of the row ID. Can be `0`, `1`, or `2`. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] always returns `0`. This field always returns a value.<br /><br />0 = `SQL_SCOPE_CURROW`. The row ID is guaranteed to be valid only while positioned on that row. A later reselect using the row ID might not return a row if the row was updated or deleted by another transaction.<br />1 = `SQL_SCOPE_TRANSACTION`. The row ID is guaranteed to be valid for the duration of the current transaction.<br />2 = `SQL_SCOPE_SESSION`. The row ID is guaranteed to be valid for the duration of the session (across transaction boundaries). |
| `COLUMN_NAME` | **sysname** | Column name for each column of the *@table_name* returned. This field always returns a value. |
| `DATA_TYPE` | **smallint** | ODBC SQL data type. |
| `TYPE_NAME` | **sysname** | Data source-dependent data type name; for example, **char**, **varchar**, **money**, or **text**. |
| `PRECISION` | **int** | Precision of the column on the data source. This field always returns a value. |
| `LENGTH` | **int** | Length, in bytes, required for the data type in its binary form in the data source, for example, `10` for **char(10)**, `4` for **integer**, and `2` for **smallint**. |
| `SCALE` | **smallint** | Scale of the column on the data source. `NULL` is returned for data types for which scale isn't applicable. |
| `PSEUDO_COLUMN` | **smallint** | Indicates whether the column is a pseudocolumn. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] always returns `1`:<br /><br />0 = `SQL_PC_UNKNOWN`<br />1 = `SQL_PC_NOT_PSEUDO`<br />2 = `SQL_PC_PSEUDO` |

## Remarks

`sp_special_columns` is equivalent to `SQLSpecialColumns` in ODBC. The results returned are ordered by `SCOPE`.

## Permissions

Requires `SELECT` permission on the schema.

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following example returns information about the column that uniquely identifies rows in the `FactFinance` table in the `AdventureWorks` database.

```sql
EXEC sp_special_columns_100 @table_name = 'FactFinance';
```

## Related content

- [Azure Synapse Analytics Stored Procedures](sql-data-warehouse-stored-procedures.md)
