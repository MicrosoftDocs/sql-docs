---
title: Backup transparency for backup history
description: "Use backup transparency to view your backup history for your  Azure SQL Managed Instance. "
author: MilanMSFT
ms.author: mlazic
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom:
---
# Backup transparency in Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes backup transparency in Azure SQL Managed Instance. 

## Overview 

The [msdb database](/sql/relational-databases/databases/msdb-database) enables backup transparency in Azure SQL Managed Instance by making backup history tables queryable. However, there are a few key differences between the backup tables in a traditional SQL Server `msdb` database, and the `msdb` database in Azure SQL Managed Instance, such as what information is visible, what tables are supported, and what fields are available. 

## Visible information 

The `msdb` database in SQL Managed Instance displays the following backup information: 

- The type of automated backup taken, such as full, differential, or log.
- Metadata about native backups taken manually, though some fields, such as file path and usernames, may not be populated. Use the `is_copyonly` column to determine if a backup was taken manually or automatically. 
- Metadata about the backup such as the status, size, time, and location.
- The replica where the backup was taken, such as the primary, or secondary. The ability to take backups from the secondary replica is currently in private preview, and only available on the Business Critical service tier. 


The `msdb` database **does not** have information about the following: 

- Backups stored for long-term retention, as this is done by copying out files at the storage level, and isn't visible to the instance. 


## Supported tables

The `msdb` database in SQL Managed Instance supports the following backup tables: 

- [Backupset](/sql/relational-databases/system-tables/backupset-transact-sql)
- [Backupmediaset](/sql/relational-databases/system-tables/backupmediaset-transact-sql)
- [Backupmediafamily](/sql/relational-databases/system-tables/backupmediafamily-transact-sql)

The following backup tables aren't used by SQL Managed Instance, and won't be populated with data: 

- [Backupfile](/sql/relational-databases/system-tables/backupfile-transact-sql)
- [Backupfilegroup](/sql/relational-databases/system-tables/backupfilegroup-transact-sql)

## Removed fields 

Since SQL Managed Instance is a cloud service with data stored in storage, the following fields won't be populated with data: 

- Fields that pertain to the user who is logged in. 
- Fields that pertain to the path of the backup file. 
- Backup expiration information. 

## Considerations

When reviewing your backup history in the `msdb` database, consider the following:

- Backup history doesn't contain all information about user-initiated backups, such as usernames of the user who initiated the backup. 
- Fields that aren't relevant to the cloud won't be populated, such as the machine name, physical drive, and physical name. 
- Backup information is inserted to the `msdb` database when the backup completes. Ongoing backups aren't supported. 


## Next steps

- To learn more, review [The msdb database in SQL Server](/sql/relational-database/databases/msdb-database). 
- To learn about backups in Azure SQL Managed Instance, review [Automated backups](automated-backups-overview.md)
- To learn about querying the `msdb` database, review [monitor backup activity](backup-activity-monitor.md)

