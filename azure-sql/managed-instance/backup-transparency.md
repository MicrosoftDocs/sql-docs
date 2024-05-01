---
title: Backup transparency for backup history
description: Learn how to use backup transparency to view the backup history of Azure SQL Managed Instance.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: conceptual
---
# Backup transparency in Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

In this article, learn how to use backup transparency in Azure SQL Managed Instance to view the backup history of your managed instance.

## Overview

Backup transparency in SQL Managed Instance is available through the [msdb database](/sql/relational-databases/databases/msdb-database), which makes backup history tables queryable.

It's important to know about a few key differences between the backup tables in a traditional SQL Server `msdb` database and the `msdb` database in SQL Managed Instance. The main differences include the information that's visible, supported tables, and the fields you can use.

## Included information

The `msdb` database in SQL Managed Instance displays the following backup information:

- The type of automated backup taken, such as full, differential, or log.
- Metadata about native backups taken manually, although fields like file path and usernames might not be populated. Use the `is_copyonly` column to determine whether a backup was taken manually or automatically.
- Metadata about the backup, including status, size, time, and location.

The `msdb` database *does not* have the following information:

- Backups that are stored for long-term retention. Backups for long-term retention are made by copying files at the storage level. This type of backup isn't visible to the instance.

## Supported tables

The `msdb` database in SQL Managed Instance supports these backup tables:

- [Backupset](/sql/relational-databases/system-tables/backupset-transact-sql)
- [Backupmediaset](/sql/relational-databases/system-tables/backupmediaset-transact-sql)
- [Backupmediafamily](/sql/relational-databases/system-tables/backupmediafamily-transact-sql)

SQL Managed Instance doesn't use the following backup tables, and the tables aren't populated with data:

- [Backupfile](/sql/relational-databases/system-tables/backupfile-transact-sql)
- [Backupfilegroup](/sql/relational-databases/system-tables/backupfilegroup-transact-sql)

## Removed fields

Because SQL Managed Instance is a cloud service that stores data in storage, the following fields aren't populated with data:

- Fields that pertain to the user who is logged in.
- Fields that pertain to the path of the backup file.
- Backup expiration information.

## Considerations

When you review your backup history in the `msdb` database, consider the following information:

- Fields that aren't relevant to the cloud aren't populated. Examples include the machine name, the physical drive, and the physical name.
- Backup information is inserted into the `msdb` database when the backup is finished. Ongoing backups aren't supported.
- The `msdb` database maintains records of automatic backups for up to 60 days, while the history of user-initiated backups, such as copy-only, is preserved indefinitely.

## Next steps

- To learn more, review [The msdb database in SQL Server](/sql/relational-databases/databases/msdb-database). 
- To learn about backups in Azure SQL Managed Instance, review [Automated backups](automated-backups-overview.md).
- To learn about querying the `msdb` database, review [Monitor backup activity](backup-activity-monitor.md).
