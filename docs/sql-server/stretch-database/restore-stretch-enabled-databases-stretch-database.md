---
title: Restore Stretch-enabled databases
description: Restore Stretch-enabled databases (Stretch Database)
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
---
# Restore Stretch-enabled databases (Stretch Database)

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

Restore a backed up database when necessary to recover from many types of failures, errors, and disasters.

For more info about backup, see [Backup Stretch-enabled databases](backup-stretch-enabled-databases-stretch-database.md).

Backing up is only one part of a complete high availability and business continuity solution. For more info about high availability, see [High Availability Solutions](../../database-engine/sql-server-business-continuity-dr.md).

## Restore your SQL Server data

To recover from hardware failure or corruption, restore the Stretch-enabled SQL Server database from a backup. You can continue to use the SQL Server restore methods that you currently use. For more info, see [Restore and Recovery Overview](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md).

After you restore the SQL Server database, you have to run the stored procedure `sys.sp_rda_reauthorize_db` to re-establish the connection between the Stretch-enabled SQL Server database and the remote Azure database. For more info, see [Restore the connection between the SQL Server database and the remote Azure database](#reconnect).

## Restore your remote Azure data

### Recover a live Azure database

The SQL Server Stretch Database service on Azure snapshots all live data at least every 8 hours using Azure Storage Snapshots. These snapshots are maintained for seven days. This allows you to restore the data to one of at least 21 points in time within the past seven days up to the time when the last snapshot was taken.

To restore a live Azure database to an earlier point in time by using the Azure portal, do the following things.

1. Sign in to the [Azure portal](https://portal.azure.com/).
1. On the left side of the screen select **Browse** and then select **SQL Databases**.
1. Navigate to your database and select it.
1. At the top of the database pane, select **Restore**.
1. Specify a new **Database name**, select a **Restore Point** and then select **Create**.
1. The database restore process will begin and can be monitored using **Notifications**.

### Recover a deleted Azure database

The SQL Server Stretch Database service on Azure takes a database snapshot before a database is dropped and retains it for seven days. After this occurs, it no longer retains snapshots from the live database. This lets you restore a deleted database to the point when it was deleted.

To restore a deleted Azure database to the point when it was deleted by using the Azure portal, do the following things.

1. Sign in to the [Azure portal](https://portal.azure.com/).
1. On the left side of the screen select **Browse** and then select **SQL Servers**.
1. Navigate to your server and select it.
1. Scroll down to Operations on your server's pane, and select the **Deleted Databases** tile.
1. Select the deleted database you want to restore.
1. Specify a new **Database name** and select **Create**.
1. The database restore process will begin and can be monitored using **Notifications**.

## <a id="reconnect"></a>Restore the connection between the SQL Server database and the remote Azure database

1. If you're going to connect to a restored Azure database with a different name, or in a different region, run the stored procedure [sys.sp_rda_deauthorize_db](../../relational-databases/system-stored-procedures/sys-sp-rda-deauthorize-db-transact-sql.md) to disconnect from the previous Azure database.

1. Run the stored procedure [sys.sp_rda_reauthorize_db](../../relational-databases/system-stored-procedures/sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect the local Stretch-enabled database to the Azure database.

   - Provide the existing database scoped credential as a **sysname** or a **varchar(128)** value. (Don't use **varchar(max)**.) You can look up the credential name in the view `sys.database_scoped_credentials`.

   - Specify whether to make a copy of the remote data and connect to the copy (recommended).

     ```sql
     USE <Stretch-enabled database name>;
     GO
     EXEC sp_rda_reauthorize_db
         @credential = N'<existing_database_scoped_credential_name>',
         @with_copy = 1;
     GO
     ```

## See also

- [Backup Stretch-enabled databases](backup-stretch-enabled-databases-stretch-database.md)
- [Manage and troubleshoot Stretch Database](manage-and-troubleshoot-stretch-database.md)
- [sys.sp_rda_reauthorize_db](../../relational-databases/system-stored-procedures/sys-sp-rda-reauthorize-db-transact-sql.md)
- [sys.sp_rda_deauthorize_db](../../relational-databases/system-stored-procedures/sys-sp-rda-deauthorize-db-transact-sql.md)
- [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)
