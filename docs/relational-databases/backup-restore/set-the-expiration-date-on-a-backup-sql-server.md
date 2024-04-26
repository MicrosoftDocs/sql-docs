---
title: "Set the expiration date on a backup (SQL Server)"
description: This article shows you how to set the expiration date on a backup in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 04/25/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "backing up databases [SQL Server], expiration dates"
  - "expiration [SQL Server]"
  - "database backups [SQL Server], expiration dates"
---
# Set the expiration date on a backup (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to set the expiration date on a backup in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

If you append multiple database backups in a single file, you can use the expiration date to avoid overwriting backups before that date. For more information, see [Media set options](../../t-sql/statements/backup-transact-sql.md#media-set-options).

## Permissions

`BACKUP DATABASE` and `BACKUP LOG` permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles.

Ownership and permission problems on the backup device's physical file can interfere with a backup operation. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be able to read and write to the device; the account under which the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service runs must have write permissions. However, [sp_addumpdevice](../system-stored-procedures/sp-addumpdevice-transact-sql.md), which adds an entry for a backup device in the system tables, doesn't check file access permissions. Such problems on the backup device's physical file might not appear until the physical resource is accessed when the backup or restore is attempted.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. After connecting to the appropriate instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, select the server name to expand the server tree.

1. Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.

1. Right-click the database, point to **Tasks**, and then select **Back Up**. The **Back Up Database** dialog box appears.

1. On the **General** page, for **Backup set will expire**, specify an expiration date to indicate when the backup set can be overwritten by another backup:

   - To have the backup set expire after a specific number of days, select **After** (the default option), and enter the number of days after set creation that the set will expire. This value can be from 0 to 99,999 days; a value of `0` days means that the backup set never expires.

     The default value is set in the **Default backup media retention (in days)** option of the **Server Properties** dialog box (**Database Settings** page). To access this value, right-click the server name in Object Explorer and select **Properties**; then select the **Database Settings** page.

   - To have the backup set expire on a specific date, select **On**, and enter the date on which the set expires.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. In the [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md) statement, specify either the EXPIREDATE or RETAINDAYS option to determine when the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] can overwrite the backup. If neither option is specified, the expiration date is determined by the [media retention](../../database-engine/configure-windows/configure-the-media-retention-server-configuration-option.md) server configuration setting. This example uses the `EXPIREDATE` option to specify an expiration date of June 1, 2024 (`20240601`).

   ```sql
   USE AdventureWorks2022;
   GO

   BACKUP DATABASE AdventureWorks2022
   TO DISK = 'Z:\SQLServerBackups\AdventureWorks2022.bak'
   WITH EXPIREDATE = '20240601';
   GO
   ```

## Related content

- [Create a Full Database Backup](create-a-full-database-backup-sql-server.md)
- [Back Up Files and Filegroups](back-up-files-and-filegroups-sql-server.md)
- [Back up a transaction log](back-up-a-transaction-log-sql-server.md)
- [Create a Differential Database Backup (SQL Server)](create-a-differential-database-backup-sql-server.md)
