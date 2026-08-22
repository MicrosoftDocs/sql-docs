---
title: "sys.fn_db_backup_file_snapshots (Transact-SQL)"
description: sys.fn_db_backup_file_snapshots (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/20/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
dev_langs:
  - TSQL
---
# sys.fn_db_backup_file_snapshots (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Returns Azure snapshots associated with the database files. If the specified database isn't found, or if the database files aren't stored in the Microsoft Azure Blob Storage, no rows are returned. Use this system function with the `sys.sp_delete_backup_file_snapshot` system stored procedure to identify and delete orphaned backup snapshots.

For more information, see [File-Snapshot Backups for Database Files in Azure](../backup-restore/file-snapshot-backups-for-database-files-in-azure.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.fn_db_backup_file_snapshots
   [ ( database_name ) ]
```

## Arguments

#### *database_name*

The name of the database being queried. If `NULL`, this function is executed in the current database scope.

## Table returned

| Column name | Data type | Description |
| --- | --- | --- |
| `file_id` | **int** | The File ID for the database. Not nullable. |
| `snapshot_time` | **nvarchar(260)** | The timestamp of the snapshot as it's returned by the REST API. Returns `NULL` if no snapshot exists. |
| `snapshot_url` | **nvarchar(360)** | The full URL to the file snapshot. Returns `NULL` if no snapshot exists. |

## Permissions

Requires `VIEW DATABASE STATE` permission on the database.

## Related content

- [sp_delete_backup_file_snapshot (Transact-SQL)](../system-stored-procedures/snapshot-backup-sp-delete-backup-file-snapshot.md)
- [sys.sp_delete_backup (Transact-SQL)](../system-stored-procedures/snapshot-backup-sp-delete-backup.md)
