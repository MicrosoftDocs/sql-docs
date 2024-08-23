---
title: "sys.dm_database_backups"
description: Returns information about backups of a database in an Azure SQL Database server.
author: SudhirRaparla
ms.author: nvraparl
ms.reviewer: randolphwest
ms.date: 09/28/2023
ms.service: azure-sql-database
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

| Column name | Data type | Description |
| --- | --- | --- |
| `backup_file_id` | **uniqueidentifier** | ID of the generated backup file. Not null. |
| `logical_database_id` | **uniqueidentifier** | Logical database ID of the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] on which the operation is performed. Not null. |
| `physical_database_name` | **nvarchar(128)** | Name of the physical [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] on which the operation is performed. Not null. |
| `logical_server_name` | **nvarchar(128)** | Name of the logical server on which the [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] that is being backed up is present. Not null. |
| `logical_database_name` | **nvarchar(128)** | User-created name of the database on which the operation is performed. Not null. |
| `backup_start_date` | **datetime2(7)** | Timestamp when the backup operation started. Not null. |
| `backup_finish_date` | **datetime2(7)** | Timestamp when the backup operation finished. Not null. |
| `backup_type` | **char(1)** | Type of backup. Not null.<br /><br />D = Full database backup<br />I = Incremental or differential backup<br />L = Log backup. |
| `in_retention` | **bit** | Backup retention status. Tells whether backup is within retention period. Null.<br /><br />1 = In retention<br />0 = Out of retention. |

## Permissions

On SQL Database Basic, S0, and S1 service objectives, and for databases in elastic pools, the server admin account, the Microsoft Entra ID admin account, or membership in the ##MS_ServerStateReader## server role is required. On all other SQL Database service objectives, either the VIEW DATABASE STATE permission on the database, or membership in the ##MS_ServerStateReader## server role, is required.

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
