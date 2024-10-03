---
title: "sp_pkeys (Transact-SQL)"
description: sp_pkeys returns primary key information for a single table in the current environment.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_pkeys"
  - "sp_pkeys_TSQL"
helpviewer_keywords:
  - "sp_pkeys"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sp_pkeys (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw.md)]

Returns primary key information for a single table in the current environment.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, Azure Synapse Analytics, Analytics Platform System (PDW).

```syntaxsql
sp_pkeys
    [ @table_name = ] N'table_name'
    [ , [ @table_owner = ] N'table_owner' ]
    [ , [ @table_qualifier = ] N'table_qualifier' ]
[ ; ]
```

## Arguments

#### [ @table_name = ] N'*table_name*'

Specifies the table for which to return information. *@table_name* is **sysname**, with no default. Wildcard pattern matching isn't supported.

#### [ @table_owner = ] N'*table_owner*'

Specifies the table owner of the specified table. *@table_owner* is **sysname**, with a default of `NULL`. Wildcard pattern matching isn't supported. If *@table_owner* isn't specified, the default table visibility rules of the underlying database management system (DBMS) apply.

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if the current user owns a table with the specified name, the columns of that table are returned. If the *@table_owner* isn't specified, and the current user doesn't own a table with the specified *@table_name*, this procedure looks for a table with the specified *@table_name* owned by the database owner. If one exists, the columns of that table are returned.

#### [ @table_qualifier = ] N'*table_qualifier*'

The table qualifier. *@table_qualifier* is **sysname**, with a default of `NULL`. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the database environment of the table.

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_QUALIFIER` | **sysname** | Name of the table qualifier. This field can be `NULL`. |
| `TABLE_OWNER` | **sysname** | Name of the table owner. This field always returns a value. |
| `TABLE_NAME` | **sysname** | Name of the table. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the table name as listed in the sysobjects table. This field always returns a value. |
| `COLUMN_NAME` | **sysname** | Name of the column, for each column of the `TABLE_NAME` returned. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the column name as listed in the `sys.columns` table. This field always returns a value. |
| `KEY_SEQ` | **smallint** | Sequence number of the column in a multicolumn primary key. |
| `PK_NAME` | **sysname** | Primary key identifier. Returns `NULL` if not applicable to the data source. |

## Remarks

`sp_pkeys` returns information about columns explicitly defined with a `PRIMARY KEY` constraint. Because not all systems support explicitly named primary keys, the gateway implementer determines what constitutes a primary key. The term primary key refers to a logical primary key for a table. Every key listed as being a logical primary key is expected to have a unique index defined on it. This unique index is also returned in `sp_statistics`.

The `sp_pkeys` stored procedure is equivalent to `SQLPrimaryKeys` in ODBC. Results are ordered by `TABLE_QUALIFIER`, `TABLE_OWNER`, `TABLE_NAME`, and `KEY_SEQ`.

## Permissions

Requires `SELECT` permission on the schema.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

The following example retrieves the primary key for the `HumanResources.Department` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO

EXEC sp_pkeys @table_name = N'Department',
    @table_owner = N'HumanResources';
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following example retrieves the primary key for the `DimAccount` table in the `AdventureWorksPDW2012` database. It returns zero rows indicating that the table doesn't have a primary key.

```sql
-- Uses AdventureWorksPDW

EXEC sp_pkeys @table_name = N'DimAccount';
```

## Related content

- [Catalog stored procedures (Transact-SQL)](catalog-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
