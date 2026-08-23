---
title: "sys.sp_add_log_file_recover_suspect_db (Transact-SQL)"
description: "Adds a log file to a database when recovery can't complete on a database due to insufficient log space (error 9002)."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_log_file_recover_suspect_db_TSQL"
  - "sp_add_log_file_recover_suspect_db"
helpviewer_keywords:
  - "sp_add_log_file_recover_suspect_db"
dev_langs:
  - "TSQL"
---
# sys.sp_add_log_file_recover_suspect_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a log file to a database when recovery can't complete on a database due to insufficient log space (error 9002). After the file is added, `sp_add_log_file_recover_suspect_db` turns off the suspect setting and completes the recovery of the database. The parameters are the same as for ALTER DATABASE *database_name* ADD LOG FILE.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_add_log_file_recover_suspect_db
    [ @dbName = ] N'dbName'
    , [ @name = ] N'name'
    , [ @filename = ] N'filename'
    [ , [ @size = ] N'size' ]
    [ , [ @maxsize = ] N'maxsize' ]
    [ , [ @filegrowth = ] N'filegrowth' ]
[ ; ]
```

## Arguments

#### [ @dbName = ] N'*dbName*'

The name of the database. *@dbName* is **sysname**, with no default.

#### [ @name = ] N'*name*'

The name used in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to reference the file. The name must be unique in the server. *@name* is **nvarchar(260)**, with no default.

#### [ @filename = ] N'*filename*'

The path and file name used by the operating system for the file. The file must reside on an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. *@filename* is **nvarchar(260)**, with no default.

#### [ @size = ] N'*size*'

The initial size of the file. *@size* is **nvarchar(20)**, with a default of `NULL`. Specify a whole number; don't include a decimal. The `MB` and `KB` suffixes can be used to specify megabytes or kilobytes. The default is `MB`. The minimum value is 512 KB. If *@size* isn't specified, the default is 1 MB.

#### [ @maxsize = ] N'*maxsize*'

The maximum size to which the file can grow. *@maxsize* is **nvarchar(20)**, with a default of `NULL`. Specify a whole number; don't include a decimal. The `MB` and `KB` suffixes can be used to specify megabytes or kilobytes. The default is `MB`.

If *@maxsize* isn't specified, the file grows until the disk is full. The Windows application log warns an administrator when a disk is about to become full.

#### [ @filegrowth = ] N'*filegrowth*'

The amount of space added to the file each time new space is required. *@filegrowth* is **nvarchar(20)**, with a default of `NULL`. A value of `0` indicates no growth. Specify a whole number; don't include a decimal. The value can be specified in `MB`, `KB`, or percent (`%`). When `%` is specified, the growth increment is the specified percentage of the size of the file at the time the increment occurs. If a number is specified without an `MB`, `KB`, or `%` suffix, the default is `MB`.

If *@filegrowth* is `NULL`, the default value is `10%`, and the minimum value is `64 KB`. The size specified is rounded to the nearest 64 KB.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

Execute permissions default to members of the **sysadmin** fixed server role. These permissions aren't transferable.

## Examples

In the following example, the database `db1` was marked suspect during recovery due to insufficient log space (error 9002).

```sql
USE master;
GO

EXECUTE sp_add_log_file_recover_suspect_db
    db1,
    logfile2,
    'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Data\db1_logfile2.ldf',
    '1 MB';
```

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sys.sp_add_data_file_recover_suspect_db (Transact-SQL)](sp-add-data-file-recover-suspect-db-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
