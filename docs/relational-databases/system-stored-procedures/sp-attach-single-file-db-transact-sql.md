---
title: "sp_attach_single_file_db (Transact-SQL)"
description: sp_attach_single_file_db attaches a database that's only one data file to the current server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_attach_single_file_db"
  - "sp_attach_single_file_db_TSQL"
helpviewer_keywords:
  - "sp_attach_single_file_db"
dev_langs:
  - "TSQL"
---
# sp_attach_single_file_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Attaches a database that's only one data file to the current server. `sp_attach_single_file_db` can't be used with multiple data files.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use `CREATE DATABASE <database_name> FOR ATTACH` instead. For more information, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md). Don't use this procedure on a replicated database.

Don't attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE [tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_attach_single_file_db
    [ @dbname = ] N'dbname'
    , [ @physname = ] N'physname'
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database to be attached to the server. *@dbname* is **sysname**, with no default.

#### [ @physname = ] N'*physname*'

The physical name, including path, of the database file. *@physname* is **nvarchar(260)**, with no default.

This argument maps to the `FILENAME` parameter of the `CREATE DATABASE` statement. For more information, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md).

> [!NOTE]  
> When you attach a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database that contains full-text catalog files onto a newer server instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the catalog files are attached from their previous location along with the other database files, the same as in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. For more information, see [Upgrade Full-Text Search](../search/upgrade-full-text-search.md).

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Use `sp_attach_single_file_db` only on databases that were previously detached from the server by using an explicit `sp_detach_db` operation or on copied databases.

`sp_attach_single_file_db` works only on databases that have a single log file. When `sp_attach_single_file_db` attaches the database to the server, it builds a new log file. If the database is read-only, the log file is built in its previous location.

> [!NOTE]  
> A database snapshot can't be detached or attached.

Don't use this procedure on a replicated database.

## Permissions

For information about how permissions are handled when a database is attached, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md).

## Examples

The following example detaches [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] and then attaches one file from [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] to the current server.

```sql
USE master;
GO
EXEC sp_detach_db @dbname = 'AdventureWorks2022';
EXEC sp_attach_single_file_db @dbname = 'AdventureWorks2022',
    @physname =
N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\AdventureWorks2022_Data.mdf';
```

## Related content

- [Database Detach and Attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md)
- [sp_detach_db (Transact-SQL)](sp-detach-db-transact-sql.md)
- [sp_helpfile (Transact-SQL)](sp-helpfile-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
