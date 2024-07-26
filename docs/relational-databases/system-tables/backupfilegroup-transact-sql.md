---
title: "backupfilegroup (Transact-SQL)"
description: backupfilegroup contains one row for each filegroup in a database at the time of backup.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "backupfilegroup_TSQL"
  - "backupfilegroup"
helpviewer_keywords:
  - "filegroups [SQL Server], backupfilegroup system table"
  - "backupfilegroup system table"
dev_langs:
  - "TSQL"
---
# backupfilegroup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Contains one row for each filegroup in a database at the time of backup. `backupfilegroup` is stored in the `msdb` database.

> [!NOTE]  
> The `backupfilegroup` table shows the filegroup configuration of the database, not of the backup set. To identify whether a file is included in the backup set, use the `is_present` column of the [backupfile](backupfile-transact-sql.md) table.

| Column name | Data type | Description |
| --- | --- | --- |
| `backup_set_id` | **int** | Backup set containing this filegroup. |
| `name` | **sysname** | Name of the filegroup. |
| `filegroup_id` | **int** | ID of the filegroup; unique within the database. Corresponds to `data_space_id` in `sys.filegroups`. |
| `filegroup_guid` | **uniqueidentifier** | Globally unique identifier for the filegroup. Can be `NULL`. |
| `type` | **char(2)** | Content type, one of:<br /><br />`FG` = Rows filegroup<br />`SL` = Log filegroup |
| `type_desc` | **nvarchar(60)** | Description of function type, one of:<br /><br />`ROWS_FILEGROUP`<br />`SQL_LOG_FILEGROUP` |
| `is_default` | **bit** | The default filegroup, used when no filegroup is specified in `CREATE TABLE` or `CREATE INDEX`. |
| `is_readonly` | **bit** | `1` = Filegroup is read-only. |
| `log_filegroup_guid` | **uniqueidentifier** | Can be `NULL`. |

## Remarks

> [!IMPORTANT]  
> The same filegroup name can appear in different databases; however, each filegroup has its own GUID. Therefore, `(backup_set_id,filegroup_guid)` is a unique key that identifies a filegroup in `backupfilegroup`.

`RESTORE VERIFYONLY FROM <backup_device> WITH LOADHISTORY` populates the columns of the `backupmediaset` table with the appropriate values from the media-set header.

To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.

## Related content

- [Backup and restore tables (Transact-SQL)](backup-and-restore-tables-transact-sql.md)
- [backupfile (Transact-SQL)](backupfile-transact-sql.md)
- [backupmediafamily (Transact-SQL)](backupmediafamily-transact-sql.md)
- [backupmediaset (Transact-SQL)](backupmediaset-transact-sql.md)
- [backupset (Transact-SQL)](backupset-transact-sql.md)
- [System tables (Transact-SQL)](system-tables-transact-sql.md)
