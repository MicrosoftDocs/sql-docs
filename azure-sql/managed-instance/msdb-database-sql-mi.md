---
title: The msdb database
description: "Learn about the msdb database when working with Azure SQL Managed Instance"
author: MilanMSFT
ms.author: mlazic
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom:
---
# The msdb database in Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [SQL Server](/sql/relational-database/databases/msdb-database)
> * [Azure SQL Managed Instance](msdb-database-sql-mi.md)

This article describes the msdb database in Azure SQL Managed Instance and how it differs from the traditional msdb database in SQL Server. 

## Overview 

In SQL Server, the [msdb database](/sql/relational-databases/databases/msdb-database) is a system database used for a number of system objects and features, such as the SQL Server Agent, Service Broker and Database mail. In Azure SQL Managed Instance, the msdb database is used solely for backup history and transparency. There are a number of differences between the msdb database in Azure SQL Managed Instance and the msdb database in SQL Server, such as what information is visible, what tables are supported, and what fields are available. 

## Visible information 

The msdb database in SQL Managed Instance displays the following backup information: 

- The type of automated backup taken, such as full, differential, or log.
- The replica where the backup was taken, such as the primary, or secondary. The ability to take backups from the secondary replica is currently in private preview. 
- Metadata information about the backup such as the status, size, time, and location 


The msdb database **will not** have the following: 

- Information about backups stored for long-term retention, as this is done by copying out files at the storage level, and is not visible to the managed instance. 
- Information about native backups taken manually. 


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

