---
title: "managed_backup.sp_backup_on_demand (Transact-SQL)"
description: "Requests SQL Server Managed Backup to Microsoft Azure to perform a backup of the specified database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "smart_admin.sp_backup_on_demand"
  - "smart_admin.sp_backup_on_demand_TSQL"
  - "sp_backup_on_demand_TSQL"
  - "sp_backup_on_demand"
helpviewer_keywords:
  - "smart_admin.sp_backup_on_demand"
  - "sp_backup_on_demand"
dev_langs:
  - "TSQL"
---
# managed_backup.sp_backup_on_demand (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Requests [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] to perform a backup of the specified database.

Use this stored procedure to perform ad hoc backups for a database configured with [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. This prevents any break in the backup chain and [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] processes are aware and the backup is stored in the same Azure Blob storage container.

Upon successful completion of the backup, the full backup file path is returned. This includes the name and location of the new backup file resulting from the backup operation.

An error is returned if [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] is in the process of executing a backup of given type for the specified database. In this case, the error message returned includes the full backup file path where the current backup is being uploaded to.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
EXEC managed_backup.sp_backup_on_demand
    [ @database_name = ] 'database name'
    , [ @type = ] { 'Database' | 'Log' }
[ ; ]
```

## Arguments

#### [ @database_name = ] '*database name*'

The name of the database on which the backup is to be performed. The @database_name is **sysname**.

#### [ @type = ] { 'Database' | 'Log' }

The type of backup to be performed: Database or Log. The @type parameter is **nvarchar(32)**.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires membership in **db_backupoperator** database role, with ALTER ANY CREDENTIAL permissions, and EXECUTE permissions on `sp_delete_backuphistory` stored procedure.

## Examples

The following example makes a database backup request for the database `TestDB`. This database has [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] enabled.

```sql
USE msdb;
GO

EXEC managed_backup.sp_backup_on_demand
    @database_name = 'TestDB',
    @type = 'Database';
GO
```
