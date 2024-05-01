---
title: "sp_autostats (Transact-SQL)"
description: Displays or changes AUTO_UPDATE_STATISTICS for an index, statistics object, a table, or an indexed view.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_autostats_TSQL"
  - "sp_autostats"
helpviewer_keywords:
  - "sp_autostats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_autostats (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Displays or changes the automatic statistics update option, `AUTO_UPDATE_STATISTICS`, for an index, a statistics object, a table, or an indexed view.

For more information about the `AUTO_UPDATE_STATISTICS` option, see [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md) and [Statistics](../statistics/statistics.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_autostats
    [ @tblname = ] N'tblname'
    [ , [ @flagc = ] 'flagc' ]
    [ , [ @indname = ] N'indname' ]
[ ; ]
```

## Arguments

#### [ @tblname = ] N'*tblname*'

The name of the table or indexed view for which to display the `AUTO_UPDATE_STATISTICS` option. *@tblname* is **nvarchar(776)**, with no default.

#### [ @flagc = ] '*flagc*'

Updates or displays the `AUTO_UPDATE_STATISTICS` option. *@flagc* is **varchar(10)**, and can be one of these values:

| Value | Description |
| --- | --- |
| `ON` | On |
| `OFF` | Off |
| Not specified | Displays the current `AUTO_UPDATE_STATISTICS` setting |

#### [ @indname = ] N'*indname*'

The name of the statistics for which to display or update the `AUTO_UPDATE_STATISTICS` option. *@indname* is **sysname**, with a default of `NULL`. To display the statistics for an index, you can use the name of the index; an index and its corresponding statistics object have the same name.

## Return code values

`0` (success) or `1` (failure).

## Result set

If *@flagc* is specified, `sp_autostats` reports the action that was taken but returns no result set.

If *@flagc* isn't specified, `sp_autostats` returns the following result set.

| Column name | Data type | Description |
| --- | --- | --- |
| `Index Name` | **sysname** | Name of the index or statistics. |
| `AUTOSTATS` | **varchar(3)** | Current value for the `AUTO_UPDATE_STATISTICS` option. |
| `Last Updated` | **datetime** | Date of the most recent statistics update. |

The result set for a table or indexed view includes statistics created for indexes, single-column statistics generated with the `AUTO_CREATE_STATISTICS` option and statistics created with the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement.

## Remarks

If the specified index is disabled, or the specified table has a disabled clustered index, an error message is displayed.

`AUTO_UPDATE_STATISTICS` is always OFF for memory-optimized tables.

## Permissions

To change the `AUTO_UPDATE_STATISTICS` option, you need membership in the **db_owner** fixed database role, or `ALTER` permission on *@tblname*.

To display the `AUTO_UPDATE_STATISTICS` option, you need membership in the **public** role.

## Examples

### A. Display the status of all statistics on a table

The following displays the status of all statistics on the `Production.Product` table.

```sql
USE AdventureWorks2022;
GO
EXEC sp_autostats 'Production.Product';
GO
```

### B. Enable AUTO_UPDATE_STATISTICS for all statistics on a table

The following example enables the `AUTO_UPDATE_STATISTICS` option for all statistics on the `Production.Product` table.

```sql
USE AdventureWorks2022;
GO
EXEC sp_autostats 'Production.Product', 'ON';
GO
```

### C. Disable AUTO_UPDATE_STATISTICS for a specific index

The following example disables the `AUTO_UPDATE_STATISTICS` option for the `AK_Product_Name` index on the `Production.Product` table.

```sql
USE AdventureWorks2022;
GO
EXEC sp_autostats 'Production.Product', 'OFF', AK_Product_Name;
GO
```

## Related content

- [Statistics](../statistics/statistics.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
- [DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [DROP STATISTICS (Transact-SQL)](../../t-sql/statements/drop-statistics-transact-sql.md)
- [sp_createstats (Transact-SQL)](sp-createstats-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
