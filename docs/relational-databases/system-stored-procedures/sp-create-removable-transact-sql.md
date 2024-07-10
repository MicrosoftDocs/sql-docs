---
title: "sp_create_removable (Transact-SQL)"
description: sp_create_removable creates a removable media database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_create_removable"
  - "sp_create_removable_TSQL"
helpviewer_keywords:
  - "sp_create_removable"
dev_langs:
  - "TSQL"
---
# sp_create_removable (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a removable media database. Creates three or more files (one for the system catalog tables, one for the transaction log, and one or more for the data tables) and places the database on those files.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_create_removable
    [ [ @dbname = ] N'dbname' ]
    [ , [ @syslogical = ] N'syslogical' ]
    [ , [ @sysphysical = ] N'sysphysical' ]
    [ , [ @syssize = ] syssize ]
    [ , [ @loglogical = ] N'loglogical' ]
    [ , [ @logphysical = ] N'logphysical' ]
    [ , [ @logsize = ] logsize ]
    [ , [ @datalogical1 = ] N'datalogical1' ]
    [ , [ @dataphysical1 = ] N'dataphysical1' ]
    [ , [ @datasize1 = ] datasize1 ]
    [ , ... ]
    [ , [ @datalogical16 = ] N'datalogical16' ]
    [ , [ @dataphysical16 = ] N'dataphysical16' ]
    [ , [ @datasize16 = ] datasize16 ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database to create for use on removable media. *@dbname* is **sysname**, with a default of `NULL`.

#### [ @syslogical = ] N'*syslogical*'

The logical name of the file that contains the system catalog tables. *@syslogical* is **sysname**, with a default of `NULL`.

#### [ @sysphysical = ] N'*sysphysical*'

The physical name. *@sysphysical* is **nvarchar(260)**, with a default of `NULL`. This value includes a fully qualified path, of the file that holds the system catalog tables.

#### [ @syssize = ] *syssize*

The size, in megabytes, of the file that holds the system catalog tables. *@syssize* is **int**, with a default of `NULL`.

#### [ @loglogical = ] N'*loglogical*'

The logical name of the file that contains the transaction log. *@loglogical* is **sysname**, with a default of `NULL`.

#### [ @logphysical = ] N'*logphysical*'

The physical name. *@logphysical* is **nvarchar(260)**, with a default of `NULL`. This value includes a fully qualified path, of the file that contains the transaction log.

#### [ @logsize = ] *logsize*

The size, in megabytes, of the file that contains the transaction log. *@logsize* is **int**, with a minimum value of `1`.

#### [ @datalogical1 = ] N'*datalogical1*'

The logical name of a file that contains the data tables. *@datalogical1* is **sysname**, with a default of `NULL`.

There must be between `1` and `16` data files. Typically, more than one data file is created when the database is expected to be large, and must be distributed on multiple disks.

#### [ @dataphysical1 = ] N'*dataphysical1*'

The physical name. *@dataphysical1* is **nvarchar(260)**, with a default of `NULL`. This value includes a fully qualified path, of a file that contains data tables.

#### [ @datasize1 = ] *datasize1*

The size, in megabytes, of a file that contains data tables. *@datasize1* is **int**, with a minimum value of `1`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

If you want to make a copy of your database on removable media, such as a compact disc, and distribute the database to other users, use this stored procedure.

## Permissions

Requires `CREATE DATABASE`, `CREATE ANY DATABASE`, or `ALTER ANY DATABASE` permission.

To maintain control over disk use on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], permission to create databases is typically limited to a few login accounts.

### Permissions on data and log files

Whenever certain operations are performed on a database, corresponding permissions are set on its data and log files. The permissions prevent the files from being accidentally tampered with if they reside in a directory that's open permissions.

| Operation on database | Permissions set on files |
| --- | --- |
| **Modified to add a new file** | Created |
| **Backed up** | Attached |
| **Restored** | Detached |

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't set data and log file permissions.

## Examples

The following example creates the database `inventory` as a removable database.

```sql
EXEC sp_create_removable 'inventory',
    'invsys',
    'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\invsys.mdf',
    2,
    'invlog',
    'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\invlog.ldf',
    4,
    'invdata',
    'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\invdata.ndf',
    10;
```

## Related content

- [Database Detach and Attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md)
- [sp_certify_removable (Transact-SQL)](sp-certify-removable-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sp_dbremove (Transact-SQL)](sp-dbremove-transact-sql.md)
- [sp_detach_db (Transact-SQL)](sp-detach-db-transact-sql.md)
- [sp_helpfile (Transact-SQL)](sp-helpfile-transact-sql.md)
- [sp_helpfilegroup (Transact-SQL)](sp-helpfilegroup-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
