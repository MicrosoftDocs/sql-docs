---
title: "sp_add_data_file_recover_suspect_db (Transact-SQL)"
description: "Adds a data file to a filegroup when recovery can't complete on a database due to insufficient space on the file group (error 1105)."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_data_file_recover_suspect_db"
  - "sp_add_data_file_recover_suspect_db_TSQL"
helpviewer_keywords:
  - "sp_add_data_file_recover_suspect_db"
dev_langs:
  - "TSQL"
---
# sp_add_data_file_recover_suspect_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a data file to a filegroup when recovery can't complete on a database due to insufficient space on the file group (error 1105). After the file is added, this stored procedure turns off the suspect setting and completes the recovery of the database. The parameters are the same as for `ALTER DATABASE <database_name> ADD FILE`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_data_file_recover_suspect_db [ @dbName = ] 'database'
    , [ @filegroup = ] N'filegroup_name'
    , [ @name = ] N'logical_file_name'
    , [ @filename = ] N'os_file_name'
    , [ @size = ] N'size'
    , [ @maxsize = ] N'max_size'
    , [ @filegrowth = ] N'growth_increment'
[ ; ]
```

## Arguments

#### [ @dbName = ] '*database*'

The name of the database. *@dbName* is **sysname**, with no default.

#### [ @filegroup = ] N'*filegroup_name*'

The filegroup you're adding the file to. *@filegroup* is **nvarchar(260)**, with a default of `NULL`, which indicates the primary file.

#### [ @name = ] N'*logical_file_name*'

The name used in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to reference the file. The name must be unique in the server. *@name* is **nvarchar(260)**, with no default.

#### [ @filename = ] N'*os_file_name*'

The path and file name used by the operating system for the file. The file must reside on an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. *@filename* is **nvarchar(260)**, with no default.

#### [ @size = ] N'*size*'

The initial size of the file. *@size* is **nvarchar(20)**, with a default of `NULL`. Specify a whole number; don't include a decimal. The `MB` and `KB` suffixes can be used to specify megabytes or kilobytes. The default is `MB`. The minimum value is 512 KB. If *@size* isn't specified, the default is 1 MB.

#### [ @maxsize = ] N'*max_size*'

The maximum size to which the file can grow. *@maxsize* is **nvarchar(20)**, with a default of `NULL`. Specify a whole number; don't include a decimal. The `MB` and `KB` suffixes can be used to specify megabytes or kilobytes. The default is `MB`.

If *@maxsize* isn't specified, the file grows until the disk is full. The Windows application log warns an administrator when a disk is about to become full.

#### [ @filegrowth = ] N'*growth_increment*'

The amount of space added to the file each time new space is required. *@filegrowth* is **nvarchar(20)**, with a default of `NULL`. A value of `0` indicates no growth. Specify a whole number; don't include a decimal. The value can be specified in `MB`, `KB`, or percent (`%`). When `%` is specified, the growth increment is the specified percentage of the size of the file at the time the increment occurs. If a number is specified without an `MB`, `KB`, or `%` suffix, the default is `MB`.

If *@filegrowth* is `NULL`, the default value is `10%`, and the minimum value is `64 KB`. The size specified is rounded to the nearest 64 KB.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

Execute permissions default to members of the **sysadmin** fixed server role. These permissions aren't transferable.

## Examples

In the following example, database `db1` was marked suspect during recovery due to insufficient space (error 1105) in file group `fg1`.

```sql
USE master;
GO

EXEC sp_add_data_file_recover_suspect_db db1,
    fg1,
    file2,
    'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\db1_file2.mdf',
    '1 MB';
```

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sp_add_log_file_recover_suspect_db (Transact-SQL)](sp-add-log-file-recover-suspect-db-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
