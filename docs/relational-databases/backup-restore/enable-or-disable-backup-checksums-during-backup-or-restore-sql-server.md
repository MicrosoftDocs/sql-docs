---
title: "Enable or disable backup checksums during backup or restore (SQL Server)"
description: This article shows you how to enable or disable backup checksums for a database in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 03/28/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "backup checksums [SQL Server]"
  - "disabling checksums"
  - "checksums [SQL Server]"
---
# Enable or disable backup checksums during backup or restore (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to enable or disable backup checksums when you're backing up or restoring a database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Permissions

#### BACKUP

`BACKUP DATABASE` and `BACKUP LOG` permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles.

Ownership and permission problems on the backup device's physical file can interfere with a backup operation. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be able to read and write to the device; the account under which the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service runs must have write permissions. However, [sp_addumpdevice](../system-stored-procedures/sp-addumpdevice-transact-sql.md), which adds an entry for a backup device in the system tables, doesn't check file access permissions. Such problems on the backup device's physical file might not appear until the physical resource is accessed when the backup or restore is attempted.

#### RESTORE

If the database being restored doesn't exist, the user must have `CREATE DATABASE` permissions to be able to execute `RESTORE`. If the database exists, `RESTORE` permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database (for the `FROM DATABASE_SNAPSHOT` option, the database always exists).

`RESTORE` permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which isn't always the case when `RESTORE` is executed, members of the **db_owner** fixed database role don't have `RESTORE` permissions.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

#### Enable or disable checksums during a backup operation

1. Follow the steps to [create a database backup](create-a-full-database-backup-sql-server.md).

1. On the **Options** page, in the **Reliability** section, select **Perform checksum before writing to media**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

#### Enable or disable backup checksum for a backup operation

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. To enable backup checksums in a [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md) statement, specify the `WITH CHECKSUM` option. To disable backup checksums, specify the `WITH NO_CHECKSUM` option. This is the default behavior, except for a compressed backup. The following example specifies that checksums be performed.

```sql
BACKUP DATABASE AdventureWorks2022
TO DISK = 'Z:\SQLServerBackups\AdvWorksData.bak'
WITH CHECKSUM;
GO
```

#### Enable or disable backup checksum for a restore operation

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. To enable backup checksums in a [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md) statement, specify the `WITH CHECKSUM` option. This is the default behavior for a compressed backup. To disable backup checksums, specify the `WITH NO_CHECKSUM` option. This is the default behavior, except for a compressed backup. The following example specifies that backup checksums be performed.

```sql
RESTORE DATABASE AdventureWorks2022
FROM DISK = 'Z:\SQLServerBackups\AdvWorksData.bak'
WITH CHECKSUM;
GO
```

> [!WARNING]  
> If you explicitly request `CHECKSUM` for a restore operation and if the backup contains backup checksums, backup checksums and page checksums are both verified, as in the default case. However, if the backup set lacks backup checksums, the restore operation fails with a message indicating that checksums aren't present.

## Related content

- [RESTORE Statements - FILELISTONLY (Transact-SQL)](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)
- [RESTORE statements - HEADERONLY (Transact-SQL)](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)
- [RESTORE Statements - LABELONLY (Transact-SQL)](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)
- [RESTORE Statements - VERIFYONLY (Transact-SQL)](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [backupset (Transact-SQL)](../system-tables/backupset-transact-sql.md)
- [RESTORE Statements - Arguments (Transact-SQL)](../../t-sql/statements/restore-statements-arguments-transact-sql.md)
- [Possible Media Errors During Backup and Restore (SQL Server)](possible-media-errors-during-backup-and-restore-sql-server.md)
- [Specify backup or restore to continue or stop after error](specify-if-backup-or-restore-continues-or-stops-after-error.md)
