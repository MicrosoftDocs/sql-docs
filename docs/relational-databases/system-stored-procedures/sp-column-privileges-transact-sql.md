---
title: "sp_column_privileges (Transact-SQL)"
description: sp_column_privileges returns column privilege information for a single table in the current environment.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_column_privileges_TSQL"
  - "sp_column_privileges"
helpviewer_keywords:
  - "sp_column_privileges"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_column_privileges (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns column privilege information for a single table in the current environment.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_column_privileges
    [ @table_name = ] N'table_name'
    [ , [ @table_owner = ] N'table_owner' ]
    [ , [ @table_qualifier = ] N'table_qualifier' ]
    [ , [ @column_name = ] N'column_name' ]
[ ; ]
```

## Arguments

#### [ @table_name = ] N'*table_name*'

The table used to return catalog information. *@table_name* is **sysname**, with no default. Wildcard pattern matching isn't supported.

#### [ @table_owner = ] N'*table_owner*'

The owner of the table used to return catalog information. *@table_owner* is **sysname**, with a default of `NULL`. Wildcard pattern matching isn't supported. If *@table_owner* isn't specified, the default table visibility rules of the underlying database management system (DBMS) apply.

If the current user owns a table with the specified name, that table's columns are returned. If *@table_owner* isn't specified and the current user doesn't own a table with the specified *@table_name*, `sp_column` privileges looks for a table with the specified *@table_name* owned by the database owner. If one exists, that table's columns are returned.

#### [ @table_qualifier = ] N'*table_qualifier*'

The name of the table qualifier. *@table_qualifier* is **sysname**, with a default of `NULL`. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the table's database environment.

#### [ @column_name = ] N'*column_name*'

A single column used when only one column of catalog information is being obtained. *@column_name* is **nvarchar(384)**, with a default of `NULL`. If *@column_name* isn't specified, all columns are returned. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], *@column_name* represents the column name as listed in the `sys.columns` table. *@column_name* can include wildcard characters using wildcard matching patterns of the underlying DBMS. For maximum interoperability, the gateway client should assume only ISO standard pattern matching (the `%` and `_` wildcard characters).

## Result set

`sp_column_privileges` is equivalent to `SQLColumnPrivileges` in ODBC. The results returned are ordered by `TABLE_QUALIFIER`, `TABLE_OWNER`, `TABLE_NAME`, `COLUMN_NAME`, and `PRIVILEGE`.

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_QUALIFIER` | **sysname** | Table qualifier name. This field can be `NULL`. |
| `TABLE_OWNER` | **sysname** | Table owner name. This field always returns a value. |
| `TABLE_NAME` | **sysname** | Table name. This field always returns a value. |
| `COLUMN_NAME` | **sysname** | Column name, for each column of the `TABLE_NAME` returned. This field always returns a value. |
| `GRANTOR` | **sysname** | Database user name that was granted permissions on this `COLUMN_NAME` to the listed `GRANTEE`. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as the `TABLE_OWNER`. This field always returns a value.<br /><br />The `GRANTOR` column can be either the database owner (`TABLE_OWNER`) or a user to whom the database owner granted permissions by using the `WITH GRANT OPTION` clause in the `GRANT` statement. |
| `GRANTEE` | **sysname** | Database user name that was granted permissions on this `COLUMN_NAME` by the listed `GRANTOR`. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column always includes a database user from the `sysusers` table. This field always returns a value. |
| `PRIVILEGE` | **varchar(32)** | One of the available column permissions. Column permissions can be one of the following values (or other values supported by the data source when implementation is defined):<br /><br />`SELECT` = `GRANTEE` can retrieve data for the columns.<br />`INSERT` = `GRANTEE` can provide data for this column when new rows are inserted (by the `GRANTEE`) into the table.<br />`UPDATE` = `GRANTEE` can modify existing data in the column.<br />`REFERENCES` = `GRANTEE` can reference a column in a foreign table in a primary key/foreign key relationship. Primary key/foreign key relationships are defined by using table constraints. |
| `IS_GRANTABLE` | **varchar(3)** | Indicates whether the `GRANTEE` is permitted to grant permissions to other users (often referred to as "grant with grant" permission). Can be `YES`, `NO`, or `NULL`. An unknown, or `NULL`, value refers to a data source for which "grant with grant" isn't applicable. |

## Remarks

With [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], permissions are granted with the `GRANT` statement and taken away by the `REVOKE` statement.

## Permissions

Requires `SELECT` permission on the schema.

## Examples

The following example returns column privilege information for a specific column.

```sql
USE AdventureWorks2022;
GO

EXEC sp_column_privileges
    @table_name = 'Employee',
    @table_owner = 'HumanResources',
    @table_qualifier = 'AdventureWorks2022',
    @column_name = 'SalariedFlag';
```

## Related content

- [GRANT (Transact-SQL)](../../t-sql/statements/grant-transact-sql.md)
- [REVOKE (Transact-SQL)](../../t-sql/statements/revoke-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
