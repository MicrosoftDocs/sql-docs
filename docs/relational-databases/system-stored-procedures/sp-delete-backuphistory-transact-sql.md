---
title: "sp_delete_backuphistory (Transact-SQL)"
description: Reduces the size of the backup and restore history tables by deleting the entries for backup sets older than the specified date.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_backuphistory"
  - "sp_delete_backuphistory_TSQL"
helpviewer_keywords:
  - "sp_delete_backuphistory"
dev_langs:
  - "TSQL"
---
# sp_delete_backuphistory (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reduces the size of the backup and restore history tables by deleting the entries for backup sets older than the specified date. More rows are added to the backup and restore history tables after each backup or restore operation is performed; therefore, we recommend that you periodically execute `sp_delete_backuphistory`.

> [!NOTE]  
> The backup and restore history tables reside in the `msdb` database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_backuphistory [ @oldest_date = ] oldest_date
[ ; ]
```

## Arguments

#### [ @oldest_date = ] *oldest_date*

The oldest date retained in the backup and restore history tables. *@oldest_date* is **datetime**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_backuphistory` must be run from the `msdb` database and affects the following tables:

- [backupfile](../system-tables/backupfile-transact-sql.md)
- [backupfilegroup](../system-tables/backupfilegroup-transact-sql.md)
- [backupmediafamily](../system-tables/backupmediafamily-transact-sql.md)
- [backupmediaset](../system-tables/backupmediaset-transact-sql.md)
- [backupset](../system-tables/backupset-transact-sql.md)
- [restorefile](../system-tables/restorefile-transact-sql.md)
- [restorefilegroup](../system-tables/restorefilegroup-transact-sql.md)
- [restorehistory](../system-tables/restorehistory-transact-sql.md)

The physical backup files are preserved, even if all the history is deleted.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example deletes all entries that are older than January 14, 2023, 12:00 A.M. in the backup and restore history tables.

```sql
USE msdb;
GO
EXEC sp_delete_backuphistory @oldest_date = '2023-01-14';
GO
```

## Related content

- [sp_delete_database_backuphistory (Transact-SQL)](sp-delete-database-backuphistory-transact-sql.md)
- [Backup History and Header Information (SQL Server)](../backup-restore/backup-history-and-header-information-sql-server.md)
