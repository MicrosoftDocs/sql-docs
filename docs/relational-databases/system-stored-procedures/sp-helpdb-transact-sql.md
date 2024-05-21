---
title: "sp_helpdb (Transact-SQL)"
description: sp_helpdb reports information about a specified database or all databases.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpdb"
  - "sp_helpdb_TSQL"
helpviewer_keywords:
  - "sp_helpdb"
dev_langs:
  - "TSQL"
---
# sp_helpdb (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about a specified database or all databases.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpdb [ [ @dbname = ] N'dbname' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database for which information is reported. *@dbname* is **sysname**, with a default of `NULL`. If *@dbname* isn't specified, `sp_helpdb` reports on all databases in the `sys.databases` catalog view.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Database name. |
| `db_size` | **nvarchar(13)** | Total size of the database. |
| `owner` | **sysname** | Database owner, such as `sa`. |
| `dbid` | **smallint** | Database ID. |
| `created` | **nvarchar(11)** | Date the database was created. |
| `status` | **nvarchar(600)** | Comma-separated list of values of database options that are currently set on the database.<br /><br />Boolean-valued options are listed only if they're enabled. Non-Boolean options are listed with their corresponding values in the form of `<option_name>=<value>`.<br /><br />For more information, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md). |
| `compatibility_level` | **tinyint** | Database [compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md): 90, 100, 110, 120, 130, 140, 150, or 160. |

If *@dbname* is specified, an extra result set shows the file allocation for the specified database.

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **nchar(128)** | Logical file name. |
| `fileid` | **smallint** | File ID. |
| `filename` | **nchar(260)** | Operating-system file name (physical file name). |
| `filegroup` | **nvarchar(128)** | Filegroup in which the file belongs.<br /><br />`NULL` = file is a log file. Log files are never a part of a filegroup. |
| `size` | **nvarchar(18)** | File size in megabytes. |
| `maxsize` | **nvarchar(18)** | Maximum size to which the file can grow. A value of `UNLIMITED` in this field indicates that the file grows until the disk is full. |
| `growth` | **nvarchar(18)** | Growth increment of the file. This value indicates the amount of space added to the file each time new space is needed. |
| `usage` | **varchar(9)** | Usage of the file. For a data file, the value is `data only` and for the log file the value is `log only`. |

## Remarks

The `status` column in the result set reports which options are set to `ON` in the database. Not all database options are reported by the `status` column. To see a complete list of the current database option settings, use the `sys.databases` catalog view.

## Permissions

When a single database is specified, membership in the **public** role in the database is required. When no database is specified, membership in the **public** role in the `master` database is required.

If a database can't be accessed, `sp_helpdb` displays error message 15622 and as much information about the database as it can.

## Examples

### A. Return information about a single database

The following example displays information about the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_helpdb N'AdventureWorks2022';
```

### B. Return information about all databases

This following example displays information about all databases on the server running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
EXEC sp_helpdb;
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files (Transact-SQL)](../system-catalog-views/sys-database-files-transact-sql.md)
- [sys.filegroups (Transact-SQL)](../system-catalog-views/sys-filegroups-transact-sql.md)
- [sys.master_files (Transact-SQL)](../system-catalog-views/sys-master-files-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
