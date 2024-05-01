---
title: "sp_attach_db (Transact-SQL)"
description: sp_attach_db attaches a database to a server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_attach_db_TSQL"
  - "sp_attach_db"
helpviewer_keywords:
  - "sp_attach_db"
dev_langs:
  - "TSQL"
---
# sp_attach_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Attaches a database to a server.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use `CREATE DATABASE <database_name> FOR ATTACH` instead. For more information, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md). To rebuild multiple log files when one or more have a new location, use `CREATE DATABASE <database_name> FOR ATTACH_REBUILD_LOG`.

Don't attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE [tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database.

## Syntax

```syntaxsql
sp_attach_db
    [ @dbname = ] N'dbname'
    , [ { @filename1 ... @filename16 } = ] { N'*filename1*' ... N'*filename16*' }
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database to be attached to the server. *@dbname* is **sysname**, with no default.

#### [ { @filename1 ... @filename16 } = ] { N'*filename1*' ... N'*filename16*' }

The physical name, including path, of a database file. This parameter is **nvarchar(260)**, with a default of `NULL`. You can specify up to 16 file names. The parameter names start at *@filename1* and increment to *@filename16*. The file name list must include at least the primary file (`.mdf`). The primary file contains the system tables that point to other files in the database. The list must also include any files that were moved after the database was detached.

This parameter maps to the `FILENAME` parameter of the `CREATE DATABASE` statement. For more information, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md).  

> [!NOTE]  
> When you attach a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database that contains full-text catalog files onto a newer version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the catalog files are attached from their previous location along with the other database files, the same as in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. For more information, see [Upgrade Full-Text Search](../search/upgrade-full-text-search.md).

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The `sp_attach_db` stored procedure should only be executed on databases that were previously detached from the database server by using an explicit `sp_detach_db` operation or on copied databases. If you have to specify more than 16 files, use `CREATE DATABASE <database_name> FOR ATTACH` or `CREATE DATABASE <database_name> FOR_ATTACH_REBUILD_LOG`. For more information, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md).

Any unspecified file is assumed to be in its last known location. To use a file in a different location, you must specify the new location.

A database created by a more recent version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't be attached in earlier versions.

> [!NOTE]  
> A database snapshot can't be detached or attached.

When you attach a replicated database that was copied instead of being detached, consider the following conditions:

- If you attach the database to the same server instance and version as the original database, no further steps are required.

- If you attach the database to the same server instance but with an upgraded version, you must execute [sp_vupgrade_replication](sp-vupgrade-replication-transact-sql.md) to upgrade replication after the attach operation is complete.

- If you attach the database to a different server instance, regardless of version, you must execute [sp_removedbreplication](sp-removedbreplication-transact-sql.md) to remove replication after the attach operation is complete.

When a database is first attached or restored to a new instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], a copy of the database master key (DMK) - encrypted by the service master key (SMK) - isn't yet stored in the server. You must use the `OPEN MASTER KEY` statement to decrypt the DMK. Once the DMK has been decrypted, you have the option of enabling automatic decryption in the future by using the `ALTER MASTER KEY REGENERATE` statement to provide the server with a copy of the DMK, encrypted with the SMK. When a database is upgraded from an earlier version, the DMK should be regenerated to use the newer AES algorithm. For more information about regenerating the DMK, see [ALTER MASTER KEY (Transact-SQL)](../../t-sql/statements/alter-master-key-transact-sql.md). The time required to regenerate the DMK key to upgrade to AES depends upon the number of objects protected by the DMK. Regenerating the DMK key to upgrade to AES is only necessary once, and has no effect on future regenerations as part of a key rotation strategy.

## Permissions

For information about how permissions are handled when a database is attached, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md).

## Examples

The following example attaches files from [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] to the current server.

```sql
EXEC sp_attach_db @dbname = N'AdventureWorks2022',
    @filename1 =
N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\AdventureWorks2022_Data.mdf',
    @filename2 =
N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\AdventureWorks2022_log.ldf';
```

## Related content

- [Database Detach and Attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md)
- [sp_detach_db (Transact-SQL)](sp-detach-db-transact-sql.md)
- [sp_helpfile (Transact-SQL)](sp-helpfile-transact-sql.md)
- [sp_removedbreplication (Transact-SQL)](sp-removedbreplication-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
