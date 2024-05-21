---
title: "sp_helpfilegroup (Transact-SQL)"
description: sp_helpfilegroup returns the names and attributes of filegroups associated with the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpfilegroup_TSQL"
  - "sp_helpfilegroup"
helpviewer_keywords:
  - "sp_helpfilegroup"
dev_langs:
  - "TSQL"
---
# sp_helpfilegroup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the names and attributes of filegroups associated with the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpfilegroup [ [ @filegroupname = ] N'filegroupname' ]
[ ; ]
```

## Arguments

#### [ @filegroupname = ] N'*filegroupname*'

*@filegroupname* is **sysname**, with a default of `NULL`.

The logical name of any filegroup in the current database. *@filegroupname* is **sysname**, with a default of `NULL`. If *@filegroupname* isn't specified, all filegroups in the current database are listed and only the first result set shown in the Result Sets section is displayed.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `groupname` | **sysname** | Name of the filegroup. |
| `groupid` | **smallint** | Numeric filegroup identifier. |
| `filecount` | **int** | Number of files in the filegroup. |

If *@filegroupname* is specified, one row for each file in the filegroup is returned.

| Column name | Data type | Description |
| --- | --- | --- |
| `file_in_group` | **sysname** | Logical name of the file in the filegroup. |
| `fileid` | **smallint** | Numeric file identifier. |
| `filename` | **nchar(260)** | Physical name of the file including the directory path. |
| `size` | **nvarchar(15)** | File size in kilobytes. |
| `maxsize` | **nvarchar(15)** | Maximum size of the file.<br /><br />This is the maximum size to which the file can grow. A value of `UNLIMITED` in this field indicates that the file grows until the disk is full. |
| `growth` | **nvarchar(15)** | Growth increment of the file. This value indicates the amount of space added to the file every time new space is required.<br /><br />`0` = File is a fixed size and doesn't grow. |

## Permissions

Requires membership in the **public** role.

## Examples

### A. Return all filegroups in a database

The following example returns information about the filegroups in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_helpfilegroup;
GO
```

### B. Return all files in a filegroup

The following example returns information for all files in the `PRIMARY` filegroup in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_helpfilegroup 'PRIMARY';
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sp_helpfile (Transact-SQL)](sp-helpfile-transact-sql.md)
- [sys.database_files (Transact-SQL)](../system-catalog-views/sys-database-files-transact-sql.md)
- [sys.master_files (Transact-SQL)](../system-catalog-views/sys-master-files-transact-sql.md)
- [sys.filegroups (Transact-SQL)](../system-catalog-views/sys-filegroups-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Database Files and Filegroups](../databases/database-files-and-filegroups.md)
