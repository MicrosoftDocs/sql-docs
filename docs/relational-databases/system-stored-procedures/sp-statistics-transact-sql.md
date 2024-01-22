---
title: "sp_statistics (Transact-SQL)"
description: "The sp_statistics system stored procedure returns a list of all indexes and statistics on a specified table or indexed view."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_statistics_TSQL"
  - "sp_statistics"
helpviewer_keywords:
  - "sp_statistics"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_statistics (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns a list of all indexes and statistics on a specified table or indexed view.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_statistics
    [ @table_name = ] N'table_name'
    [ , [ @table_owner = ] N'table_owner' ]
    [ , [ @table_qualifier = ] N'table_qualifier' ]
    [ , [ @index_name = ] N'index_name' ]
    [ , [ @is_unique = ] 'is_unique' ]
    [ , [ @accuracy = ] 'accuracy' ]
[ ; ]
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### [ @table_name = ] N'*table_name*'

Specifies the table used to return catalog information. *@table_name* is **sysname**, with no default. Wildcard pattern matching isn't supported.

#### [ @table_owner = ] N'*table_owner*'

The name of the table owner of the table used to return catalog information. *@table_owner* is **sysname**, with a default of `NULL`. Wildcard pattern matching isn't supported. If `owner` isn't specified, the default table visibility rules of the underlying database management system (DBMS) apply.

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if the current user owns a table with the specified name, the indexes of that table are returned. If `owner` isn't specified and the current user doesn't own a table with the specified `name`, this procedure looks for a table with the specified `name` owned by the database owner. If one exists, the indexes of that table are returned.

#### [ @table_qualifier = ] N'*table_qualifier*'

The name of the table qualifier. *@table_qualifier* is **sysname**, with a default of `NULL`. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this parameter represents the database name. In some products, it represents the server name of the table's database environment.

#### [ @index_name = ] N'*index_name*'

The index name. *@index_name* is **sysname**, with a default of `%`. Wildcard pattern matching is supported.

#### [ @is_unique = ] '*is_unique*'

Whether only unique indexes (if `Y`) are to be returned. *@is_unique* is **char(1)**, with a default of empty string.

#### [ @accuracy = ] '*accuracy*'

The level of cardinality and page accuracy for statistics. *@accuracy* is **char(1)**, with a default of `Q`. Specify `E` to make sure that statistics are updated so that cardinality and pages are accurate.

- `E` (`SQL_ENSURE`) asks the driver to unconditionally retrieve the statistics.

- `Q` (`SQL_QUICK`) asks the driver to retrieve the cardinality and pages, only if they're readily available from the server. In this case, the driver doesn't ensure that the values are current. Applications that are written to the Open Group standard always get `SQL_QUICK` behavior from ODBC 3.x-compliant drivers.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_QUALIFIER` | **sysname** | Table qualifier name. This column can be `NULL`. |
| `TABLE_OWNER` | **sysname** | Table owner name. This column always returns a value. |
| `TABLE_NAME` | **sysname** | Table name. This column always returns a value. |
| `NON_UNIQUE` | **smallint** | Not nullable.<br /><br />`0` = Unique<br />`1` = Not unique |
| `INDEX_QUALIFIER` | **sysname** | Index owner name. Some DBMS products allow for users other than the table owner to create indexes. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as `TABLE_NAME`. |
| `INDEX_NAME` | **sysname** | The name of the index. This column always returns a value. |
| `TYPE` | **smallint** | This column always returns a value:<br /><br />`0` = Statistics for a table<br />`1` = Clustered<br />`2` = Hashed<br />`3` = Nonclustered |
| `SEQ_IN_INDEX` | **smallint** | Position of the column within the index. |
| `COLUMN_NAME` | **sysname** | Column name for each column of the `TABLE_NAME` returned. This column always returns a value. |
| `COLLATION` | **char(1)** | Order used in collation. Can be:<br /><br />`A` = Ascending<br />`D` = Descending<br />`NULL` = Not applicable |
| `CARDINALITY` | **int** | Number of rows in the table or unique values in the index. |
| `PAGES` | **int** | Number of pages to store the index or table. |
| `FILTER_CONDITION` | **varchar(128)** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't return a value. |

## Return code values

None.

## Remarks

The indexes in the result set appear in ascending order by the columns `NON_UNIQUE`, `TYPE`, `INDEX_NAME`, and `SEQ_IN_INDEX`.

The index type clustered refers to an index in which table data is stored in the order of the index. This value corresponds to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] clustered indexes.

The index type Hashed accepts exact match or range searches, but pattern matching searches don't use the index.

The `sp_statistics` system stored procedure is equivalent to `SQLStatistics` in ODBC. The results returned are ordered by `NON_UNIQUE`, `TYPE`, `INDEX_QUALIFIER`, `INDEX_NAME`, and `SEQ_IN_INDEX`. For more information, see the [ODBC Reference](../../odbc/reference/syntax/odbc-reference.md).

## Permissions

Requires `SELECT` permission on the schema.

## Example: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following example returns information about the `DimEmployee` table from the `AdventureWorks` sample database.

```sql
EXEC sp_statistics DimEmployee;
```

## Related content

- [Catalog stored procedures (Transact-SQL)](catalog-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Statistics for Memory-Optimized Tables](../in-memory-oltp/statistics-for-memory-optimized-tables.md)
- [Statistics](../statistics/statistics.md)
- [Update statistics](../statistics/update-statistics.md)
