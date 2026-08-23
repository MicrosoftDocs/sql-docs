---
title: "sys.sp_clean_db_file_free_space (Transact-SQL)"
description: Removes residual information left on database pages in a database file, because of data modification routines in SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest, dfurman
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_clean_db_file_free_space"
  - "sp_clean_db_file_free_space_TSQL"
helpviewer_keywords:
  - "ghost records"
  - "sp_clean_db_file_free_space"
dev_langs:
  - "TSQL"
---

# sys.sp_clean_db_file_free_space (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Removes residual information on data pages. `sp_clean_db_file_free_space` cleans all pages in only one file of a database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_clean_db_file_free_space
    [ @dbname = ] N'dbname'
    , [ @fileid = ] fileid
    [ , [ @cleaning_delay = ] cleaning_delay ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database to clean. *@dbname* is **sysname**, with no default.

#### [ @fileid = ] *fileid*

The data file ID to clean. *@fileid* is **int**, with no default.

#### [ @cleaning_delay = ] *cleaning_delay*

Specifies an interval to delay before the cleanup of each page, in seconds. *@cleaning_delay* is **int**, with a default of `0`. This delay helps reduce the load on the I/O system at the expense of increasing the duration of the cleanup process.

## Return code values

`0` (success) or `1` (failure).

## Remarks

The `sp_clean_db_file_free_space` system stored procedure moves all rows on a page, including the ghosted records if any, to the beginning of the page, and then zero-initializes the remainder of the data space on the page. In environments where the physical security of the data files or the underlying storage is at risk, you can use this stored procedure to ensure that no residual deleted data remains in the data files or in storage.

The time required to run `sp_clean_db_file_free_space` depends on the size of the data file, the number of used pages in the file, and the I/O capabilities of the disk. Because running `sp_clean_db_file_free_space` can significantly increase I/O activity, we recommend that you run this procedure outside the usual operation hours.

Before you run `sp_clean_db_file_free_space`, we recommend that you create a full database backup.

To perform this operation for all data files in a database, use [sp_clean_db_free_space](sp-clean-db-free-space-transact-sql.md).

## Permissions

Requires membership in the **db_owner** database role.

## Examples

The following example cleans all residual information from the primary data file of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE master;
GO

EXECUTE sp_clean_db_file_free_space
    @dbname = N'AdventureWorks2022',
    @fileid = 1;
```

## Related content

- [sys.sp_clean_db_free_space (Transact-SQL)](sp-clean-db-free-space-transact-sql.md)
