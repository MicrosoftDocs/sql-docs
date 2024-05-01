---
title: "sp_delete_database_backuphistory (Transact-SQL)"
description: Deletes information about the specified database from the backup and restore history tables.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_database_backuphistory"
  - "sp_delete_database_backuphistory_TSQL"
helpviewer_keywords:
  - "sp_delete_database_backuphistory"
dev_langs:
  - "TSQL"
---
# sp_delete_database_backuphistory (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes information about the specified database from the backup and restore history tables.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_database_backuphistory [ @database_name = ] N'database_name'
[ ; ]
```

## Arguments

#### [ @database_name = ] N'*database_name*'

Specifies the name of the database involved in backup and restore operations. *@database_name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_database_backuphistory` must be run from the `msdb` database.

This stored procedure affects the following tables:

- [backupfile](../system-tables/backupfile-transact-sql.md)
- [backupfilegroup](../system-tables/backupfilegroup-transact-sql.md)
- [backupmediafamily](../system-tables/backupmediafamily-transact-sql.md)
- [backupmediaset](../system-tables/backupmediaset-transact-sql.md)
- [backupset](../system-tables/backupset-transact-sql.md)
- [restorefile](../system-tables/restorefile-transact-sql.md)
- [restorefilegroup](../system-tables/restorefilegroup-transact-sql.md)
- [restorehistory](../system-tables/restorehistory-transact-sql.md)

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example deletes all entries for the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database in the backup-and-restore history tables.

```sql
USE msdb;
GO
EXEC sp_delete_database_backuphistory
    @database_name = 'AdventureWorks2022';
```

## Related content

- [sp_delete_backuphistory (Transact-SQL)](sp-delete-backuphistory-transact-sql.md)
- [Backup History and Header Information (SQL Server)](../backup-restore/backup-history-and-header-information-sql-server.md)
