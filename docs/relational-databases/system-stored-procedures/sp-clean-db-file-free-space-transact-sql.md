---
title: "sp_clean_db_file_free_space (Transact-SQL)"
description: Removes residual information left on database pages in a database file, because of data modification routines in SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
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
# sp_clean_db_file_free_space (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes residual information left on database pages because of data modification routines in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. `sp_clean_db_file_free_space` cleans all pages in only one file of a database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_clean_db_file_free_space
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

Specifies an interval to delay between the cleaning of pages. *@cleaning_delay* is **int**, with a default of `0`. This delay helps reduce the effect on the I/O system.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Deletes operations from a table or update operations that cause a row to move can immediately free up space on a page by removing references to the row. However, under certain circumstances, the row can physically remain on the data page as a ghost record. A background process periodically removes ghost records. This residual data isn't returned by the [!INCLUDE [ssDE](../../includes/ssde-md.md)] in response to queries. However, in environments in which the physical security of the data or backup files is at risk, you can use `sp_clean_db_file_free_space` to clean these ghost records. To perform this operation for all database files at once, use [sp_clean_db_free_space](sp-clean-db-free-space-transact-sql.md).

The length of time required to run `sp_clean_db_file_free_space` depends on the size of the file, the available free space, and the capacity of the disk. Because running `sp_clean_db_file_free_space` can significantly affect I/O activity, we recommend that you run this procedure outside usual operation hours.

Before you run `sp_clean_db_file_free_space`, we recommend that you create a full database backup.

The related [sp_clean_db_free_space](sp-clean-db-free-space-transact-sql.md) stored procedure cleans all files in the database.

## Permissions

Requires membership in the **db_owner** database role.

## Examples

The following example cleans all residual information from the primary data file of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE master;
GO

EXEC sp_clean_db_file_free_space
    @dbname = N'AdventureWorks2022',
    @fileid = 1;
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [Ghost cleanup process guide](../ghost-record-cleanup-process-guide.md)
- [sp_clean_db_free_space (Transact-SQL)](sp-clean-db-free-space-transact-sql.md)
