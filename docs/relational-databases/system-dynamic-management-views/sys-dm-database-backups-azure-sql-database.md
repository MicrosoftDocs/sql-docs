---
title: "sys.dm_database_backups"
description: Returns information about backups of a database in an Azure SQL Database server.
author: SudhirRaparla
ms.author: nvraparl
ms.reviewer: randolphwest
ms.date: 03/23/2023
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "dm_database_backups_TSQL"
  - "dm_database_backups"
  - "sys.dm_database_backups"
  - "sys.dm_database_backups_TSQL"
helpviewer_keywords:
  - "dm_database_backups dynamic management view"
  - "sys.dm_database_backups dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.dm_database_backups

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Returns information about backups of a database in an [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] server.

> [!NOTE]  
> The `sys.dm_database_backups` DMV is currently in preview and is available for all Azure SQL Database service tiers except Hyperscale tier.

| Column Name | Data Type | Description |
| --- | --- | --- |
| backup_file_id | **uniqueidentifier** | ID of the generated backup file. Not null. |
| database_guid | **uniqueidentifier** | Logical database ID of the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] on which the operation is performed. Not null. |
| physical_database_name | **nvarchar(128)** | Name of the physical [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] on which the operation is performed. Not null. |
| server_name | **nvarchar(128)** | Name of the physical server on which the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] that is being backed up is present. Not null. |
| backup_start_date | **datetime2(7)** | Timestamp when the backup operation started. Not null. |
| backup_finish_date | **datetime2(7)** | Timestamp when the backup operation finished. Not null. |
| backup_type | **char(1)** | Type of backup. Not null.<br /><br />D = Full database backup<br />I = Incremental or differential backup<br />L = Log backup. |
| in_retention | **bit** | Backup retention status. Tells whether backup is within retention period. Null.<br /><br />1 = In retention<br />0 = Out of retention. |

## Permissions

Requires VIEW DATABASE STATE permission on the database.

## Remarks

Backups retained and shown in the backup history view depend on configured backup retention. Some backups older than the retention period (`in_retention = 0`) are also shown in the `sys.dm_database_backups` view. They're needed to do point in time restore within the configured retention.

## Example

Show list of all active backups for the current database ordered by backup finish date.

```sql
SELECT *
FROM sys.dm_database_backups
ORDER BY backup_finish_date DESC;
```

To get a user friendly list of backups for a database, please run:

```sql
SELECT backup_file_id, 
    backup_start_date,
    backup_finish_date,
    CASE backup_type
        WHEN 'D' THEN 'Full'
        WHEN 'I' THEN 'Differential'
        WHEN 'L' THEN 'Transaction log'
        END AS BackupType,
    CASE in_retention
        WHEN 1 THEN 'In retention'
        WHEN 0 THEN 'Out of retention'
        END AS IsBackupAvailable
FROM sys.dm_database_backups
ORDER BY backup_start_date DESC;
```
