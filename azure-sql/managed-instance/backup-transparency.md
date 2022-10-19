---
title: Backup transparency
description: "Learn about the msdb database when working with Azure SQL Managed Instance"
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

The [msdb database](/sql/relational-databases/databases/msdb-database) enables backup transparency in Azure SQL Managed Instance by making backup history tables queryable. However, there are a few key differences between the backup tables in a traditional SQL Server msdb database, and the msdb database in Azure SQL Managed Instance, such as what information is visible, what tables are supported, and what fields are available. 

## Visible information 

The msdb database in SQL Managed Instance displays the following backup information: 

- The type of automated backup taken, such as full, differential, or log.
- The replica where the backup was taken, such as the primary, or secondary. The ability to take backups from the secondary replica is currently in private preview. 
- Metadata information about the backup such as the status, size, time, and location 

The msdb database **does not** have information about the following: 

- Backups stored for long-term retention, as this is done by copying out files at the storage level, and is not visible to the managed instance. 
- Native backups taken manually. 

## Supported tables

The msdb database in SQL Managed Instance supports the following backup tables: 

- [Backupset](/sql/relational-databases/system-tables/backupset-transact-sql)
- [Backupmediaset](/sql/relational-databases/system-tables/backupmediaset-transact-sql)
- [Backupmediafamily](/sql/relational-databases/system-tables/backupmediafamily-transact-sql)

The following backup tables have been removed: 

- [Backupfile](/sql/relational-databases/system-tables/backupfile-transact-sql)
- [Backupfilegroup](/sql/relational-databases/system-tables/backupfilegroup-transact-sql)

## Removed fields 

Since SQL Managed Instance is a cloud service with data stored in storage, the following fields have been removed from existing msdb tabes: 

- Fields that pertain to the user who is logged in. 
- Fields that pertain to the path of the backup file. 


## Next steps

- To learn more, review [The msdb database in SQL Server](/sql/relational-database/databases/msdb-database). 
- To learn about about backups in Azure SQL Managed Instance, review [Automated backups](automated-backups-overview.md)
- To learn about querying the msdb database, review [monitor backup activity](backup-activity-monitor.md)

