---
title: "sp_tables (Transact-SQL)"
description: sp_tables returns a list of objects that can be queried in the current environment.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_tables"
  - "sp_tables_TSQL"
helpviewer_keywords:
  - "sp_tables"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sp_tables (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns a list of objects that can be queried in the current environment. This means any table or view, except synonym objects.

> [!NOTE]  
> To determine the name of the base object of a synonym, query the [sys.synonyms](../system-catalog-views/sys-synonyms-transact-sql.md) catalog view.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, Azure Synapse Analytics, Parallel Data Warehouse:

```syntaxsql
sp_tables
    [ [ @table_name = ] N'table_name' ]
    [ , [ @table_owner = ] N'table_owner' ]
    [ , [ @table_qualifier = ] N'table_qualifier' ]
    [ , [ @table_type = ] 'table_type' ]
    [ , [ @fUsePattern = ] fUsePattern ]
[ ; ]
```

## Arguments

#### [ @table_name = ] N'*table_name*'

The table used to return catalog information. *@table_name* is **nvarchar(384)**, with a default of `NULL`. Wildcard pattern matching is supported.

#### [ @table_owner = ] N'*table_owner*'

The table owner of the table used to return catalog information. *@table_owner* is **nvarchar(384)**, with a default of `NULL`. Wildcard pattern matching is supported. If the owner isn't specified, the default table visibility rules of the underlying DBMS apply.

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if the current user owns a table with the specified name, the columns of that table are returned. If the owner isn't specified and the current user doesn't own a table with the specified name, this procedure looks for a table with the specified name owned by the database owner. If one exists, the columns of that table are returned.

#### [ @table_qualifier = ] N'*table_qualifier*'

The name of the table qualifier. *@table_qualifier* is **sysname**, with a default of `NULL`. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the table's database environment.

#### [ @table_type = ] '*table_type*'

A list of values, separated by commas, that gives information about all tables of the table types that are specified. These include `TABLE`, `SYSTEMTABLE`, and `VIEW`. *@table_type* is **varchar(100)**, with a default of `NULL`.

Single quotation marks must enclose each table type, and double quotation marks must enclose the whole parameter. Table types must be uppercase. If `SET QUOTED_IDENTIFIER` is `ON`, each single quotation mark must be doubled and the whole parameter must be enclosed in single quotation marks.

#### [ @fUsePattern = ] *fUsePattern*

Determines whether the underscore (`_`), percent (`%`), and bracket (`[` or `]`) characters are interpreted as wildcard characters. Valid values are `0` (pattern matching is off) and `1` (pattern matching is on). *@fUsePattern* is **bit**, with a default of `1`.

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_QUALIFIER` | **sysname** | Table qualifier name. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. This field can be `NULL`. |
| `TABLE_OWNER` | **sysname** | Table owner name. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the name of the database user who created the table. This field always returns a value. |
| `TABLE_NAME` | **sysname** | Table name. This field always returns a value. |
| `TABLE_TYPE` | **varchar(32)** | Table, system table, or view. |
| `REMARKS` | **varchar(254)** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't return a value for this column. |

## Remarks

For maximum interoperability, the gateway client should assume only SQL-92-standard SQL pattern matching (the % and _ wildcard characters).

Privilege information about the current user's read or write access to a specific table isn't always checked. Therefore access isn't guaranteed. This result set includes not only tables and views, but also synonyms and aliases for gateways to DBMS products that support those types. If the server attribute `ACCESSIBLE_TABLES` is Y in the result set for `sp_server_info`, only tables that can be accessed by the current user are returned.

`sp_tables` is equivalent to `SQLTables` in ODBC. The results returned are ordered by `TABLE_TYPE`, `TABLE_QUALIFIER`, `TABLE_OWNER`, and `TABLE_NAME`.

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

### A. Return a list of objects that can be queried in the current environment

The following example returns a list of objects that can be queries in the current environment.

```sql
EXEC sp_tables;
```

### B. Return information about the tables in a specified schema

The following example returns information about the tables that belong to the `Person` schema in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_tables
   @table_name = '%',
   @table_owner = 'Person',
   @table_qualifier = 'AdventureWorks2022';
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### C. Return a list of objects that can be queried in the current environment

The following example returns a list of objects that can be queries in the current environment.

```sql
EXEC sp_tables;
```

### D. Return information about the tables in a specified schema

The following example returns information about the dimension tables in the `AdventureWorksPDW2012` database.

```sql
-- Uses AdventureWorks

EXEC sp_tables
   @table_name = 'Dim%',
   @table_owner = 'dbo',
   @table_qualifier = 'AdventureWorksPDW2012';
```

## Related content

- [sys.synonyms (Transact-SQL)](../system-catalog-views/sys-synonyms-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
