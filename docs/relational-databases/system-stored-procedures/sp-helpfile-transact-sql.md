---
title: "sp_helpfile (Transact-SQL)"
description: sp_helpfile returns the physical names and attributes of files associated with the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpfile"
  - "sp_helpfile_TSQL"
helpviewer_keywords:
  - "sp_helpfile"
dev_langs:
  - "TSQL"
---
# sp_helpfile (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the physical names and attributes of files associated with the current database. Use this stored procedure to determine the names of files to attach to or detach from the server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpfile [ [ @filename = ] N'filename' ]
[ ; ]
```

## Arguments

#### [ @filename = ] N'*filename*'

The logical name of any file in the current database. *@filename* is **sysname**, with a default of `NULL`. If *@filename* isn't specified, the attributes of all files in the current database are returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Logical file name. |
| `fileid` | **smallint** | Numeric identifier of the file. A value isn't returned if *@filename* is specified. |
| `filename` | **nchar(260)** | Physical file name. |
| `filegroup` | **sysname** | Filegroup in which the file belongs.<br /><br />`NULL` = File is a log file. Log files are never a part of a filegroup. |
| `size` | **nvarchar(15)** | File size in kilobytes. |
| `maxsize` | **nvarchar(15)** | Maximum size to which the file can grow. A value of `UNLIMITED` in this field indicates that the file grows until the disk is full. |
| `growth` | **nvarchar(15)** | Growth increment of the file. This value indicates the amount of space added to the file every time that new space is required.<br /><br />`0` = File is a fixed size and doesn't grow. |
| `usage` | **varchar(9)** | For data file, the value is `data only`, and for the log file the value is `log only`. |

## Permissions

Requires membership in the **public** role.

## Examples

The following example returns information about the files in [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)].

```sql
USE AdventureWorks2022;
GO
EXEC sp_helpfile;
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sp_helpfilegroup (Transact-SQL)](sp-helpfilegroup-transact-sql.md)
- [sys.database_files (Transact-SQL)](../system-catalog-views/sys-database-files-transact-sql.md)
- [sys.master_files (Transact-SQL)](../system-catalog-views/sys-master-files-transact-sql.md)
- [sys.filegroups (Transact-SQL)](../system-catalog-views/sys-filegroups-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Database Files and Filegroups](../databases/database-files-and-filegroups.md)
