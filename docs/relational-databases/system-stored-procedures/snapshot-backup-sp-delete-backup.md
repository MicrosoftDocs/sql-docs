---
title: "sp_delete_backup (Transact-SQL)"
description: "Deletes all snapshots and the backup file that comprise a snapshot backup set from the specified database."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
---
# sp_delete_backup (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Deletes all snapshots and the backup file that comprise a snapshot backup set from the specified database. This system stored procedure is the only recommended method for managing snapshot backup sets. For more information, see [File-Snapshot Backups for Database Files in Azure](../backup-restore/file-snapshot-backups-for-database-files-in-azure.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_delete_backup
    [ @backup_url = ] N'backup_metadata_file_url'
    , [ [ @db_name = ] N'database_name' | NULL ]
```

## Arguments

#### [ @backup_url = ] N'*backup_meta_file_url*'

The URL of the backup to be deleted, which deletes all snapshots comprising the specified backup set including the backup file itself.

#### [ @db_name = ] N'*database_name*'

The name of the database containing the snapshot to be deleted. When a database name is provided, the system verifies that the backup URL provided is a backup URL for the specified database and uses [sp_delete_backup_file_snapshot (Transact-SQL)](snapshot-backup-sp-delete-backup-file-snapshot.md) to delete each snapshot. If no database name is provided, this database check is not performed.

## Permissions

Requires ALTER ANY DATABASE permission or ALTER permission on the specified database.

## Related content

- [sys.fn_db_backup_file_snapshots (Transact-SQL)](../system-functions/sys-fn-db-backup-file-snapshots-transact-sql.md)
- [sp_delete_backup_file_snapshot (Transact-SQL)](snapshot-backup-sp-delete-backup-file-snapshot.md)
