---
title: "sys.dm_database_backups"
description: Returns information about backups of a database in an Azure SQL Database logical server and in Fabric SQL database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, nvraparl
ms.date: 02/10/2025
ms.service: azure-sql-database
ms.topic: "reference"
ms.custom:
  - ignite-2025
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
monikerRange: "=azuresqldb-current || =fabric-sqldb"
---
# sys.dm_database_backups

[!INCLUDE [Azure SQL Database FabricSQLDB](../../includes/applies-to-version/asdb-fabricsqldb.md)]

Returns information about backups of a database in an [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] logical server and in [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

| Column name | Data type | Description |
| --- | --- | --- |
| `backup_file_id` | **uniqueidentifier** | ID of the generated backup file. Not null. |
| `logical_database_id` | **uniqueidentifier** | Logical database ID on which the operation is performed. Not null. |
| `physical_database_name` | **nvarchar(128)** | Name of the physical database on which the operation is performed. Not null. |
| `logical_server_name` | **nvarchar(128)** | Name of the logical server on which the database that is being backed up is present. In SQL database in Fabric, this is `NULL`.|
| `logical_database_name` | **nvarchar(128)** | User-created name of the database on which the operation is performed. Not null. |
| `backup_start_date` | **datetime2(7)** | Timestamp when the backup operation started. Not null. |
| `backup_finish_date` | **datetime2(7)** | Timestamp when the backup operation finished. Not null. |
| `backup_type` | **char(1)** | Type of backup. Not null.<br /><br />`D` = Full database backup<br />`I` = Incremental or differential backup<br />`L` = Log backup. |
| `in_retention` | **bit** | Backup retention status. Tells whether backup is within retention period. <br /><br />`1` = In retention<br />`0` = Out of retention. |

## Permissions

In Azure SQL Database, in the Basic, S0, and S1 service objectives, and for databases in elastic pools, the server admin account, the Microsoft Entra ID admin account, or membership in the ##MS_ServerStateReader## server role is required. On all other SQL Database service objectives, either the VIEW DATABASE STATE permission on the database, or membership in the ##MS_ServerStateReader## server role, is required.

In Fabric SQL database, a user must be granted VIEW DATABASE STATE in the database to query this DMV. Or, a member of any [role the Fabric workspace](/fabric/get-started/roles-workspaces) can query this DMV.

## Remarks

Backups retained and shown in the backup history view depend on configured backup retention. Some backups older than the retention period (`in_retention = 0`) are also shown in the `sys.dm_database_backups` view. They're needed to do point in time restore within the configured retention.

Since the Hyperscale service tier relies on snapshots for backups, running this DMV in the Hyperscale service tier returns no results.

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

## Related content

- [Automated backups in Azure SQL Database](/azure/azure-sql/database/automated-backups-overview)
- [Automated backups in SQL database in Microsoft Fabric](/fabric/database/sql/backup)
