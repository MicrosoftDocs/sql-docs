---
title: "model Database | Microsoft Docs"
ms.custom: ""
ms.date: "11/19/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "template databases [SQL Server]"
  - "model database [SQL Server], about model databases"
  - "model database [SQL Server]"
ms.assetid: 4e4f739b-fd27-4dce-8be6-3d808040d8d7
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# model Database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **model** database is used as the template for all databases created on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Because **tempdb** is created every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started, the **model** database must always exist on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system. The entire contents of the **model** database, including database options, are copied to the new database. Some of the settings of **model** are also used for creating a new **tempdb** during start up, so the **model** database must always exist on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system.  
  
 Newly created user databases use the same [recovery model](../../relational-databases/backup-restore/recovery-models-sql-server.md) as the model database. The default is user configurable. To learn the current recovery model of the model, see [View or Change the Recovery Model of a Database &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-or-change-the-recovery-model-of-a-database-sql-server.md).  
  
> [!IMPORTANT]  
>  If you modify the **model** database with user-specific template information, we recommend that you back up **model**. For more information, see [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).  
  
## model Usage  
 When a CREATE DATABASE statement is issued, the first part of the database is created by copying in the contents of the **model** database. The rest of the new database is then filled with empty pages.  
  
 If you modify the **model** database, all databases created afterward will inherit those changes. For example, you could set permissions or database options, or add objects such as tables, functions, or stored procedures. File properties of the **model** database are an exception, and are ignored except the initial size of the data file. The default initial size of the model database data and log file is 8 MB.  
  
## Physical Properties of model  
 The following table lists initial configuration values of the **model** data and log files.  
  
|File|Logical name|Physical name|File growth|  
|----------|------------------|-------------------|-----------------|  
|Primary data|modeldev|model.mdf|Autogrow by 64 MB until the disk is full.|  
|Log|modellog|modellog.ldf|Autogrow by 64 MB to a maximum of 2 terabytes.|  

For SQL Server 2014, see [model Database](https://docs.microsoft.com/sql/relational-databases/databases/model-database?view=sql-server-2014) for default file growth values.  

 To move the **model** database or log files, see [Move System Databases](../../relational-databases/databases/move-system-databases.md).  
  
### Database Options  
 The following table lists the default value for each database option in the **model** database and whether the option can be modified. To view the current settings for these options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
|Database option|Default value|Can be modified|  
|---------------------|-------------------|---------------------|  
|ALLOW_SNAPSHOT_ISOLATION|OFF|Yes|  
|ANSI_NULL_DEFAULT|OFF|Yes|  
|ANSI_NULLS|OFF|Yes|  
|ANSI_PADDING|OFF|Yes|  
|ANSI_WARNINGS|OFF|Yes|  
|ARITHABORT|OFF|Yes|  
|AUTO_CLOSE|OFF|Yes|  
|AUTO_CREATE_STATISTICS|ON|Yes|  
|AUTO_SHRINK|OFF|Yes|  
|AUTO_UPDATE_STATISTICS|ON|Yes|  
|AUTO_UPDATE_STATISTICS_ASYNC|OFF|Yes|  
|CHANGE_TRACKING|OFF|No|  
|CONCAT_NULL_YIELDS_NULL|OFF|Yes|  
|CURSOR_CLOSE_ON_COMMIT|OFF|Yes|  
|CURSOR_DEFAULT|GLOBAL|Yes|  
|Database Availability Options|ONLINE<br /><br /> MULTI_USER<br /><br /> READ_WRITE|No<br /><br /> Yes<br /><br /> Yes|  
|DATE_CORRELATION_OPTIMIZATION|OFF|Yes|  
|DB_CHAINING|OFF|No|  
|ENCRYPTION|OFF|No|  
|MIXED_PAGE_ALLOCATION|ON|No|  
|NUMERIC_ROUNDABORT|OFF|Yes|  
|PAGE_VERIFY|CHECKSUM|Yes|  
|PARAMETERIZATION|SIMPLE|Yes|  
|QUOTED_IDENTIFIER|OFF|Yes|  
|READ_COMMITTED_SNAPSHOT|OFF|Yes|  
|RECOVERY|Depends on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition*|Yes|  
|RECURSIVE_TRIGGERS|OFF|Yes|  
|Service Broker Options|DISABLE_BROKER|No|  
|TRUSTWORTHY|OFF|No|  
  
 *To verify the current recovery model of the database, see [View or Change the Recovery Model of a Database &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-or-change-the-recovery-model-of-a-database-sql-server.md) or [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).  
  
 For a description of these database options, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
## Restrictions  
 The following operations cannot be performed on the **model** database:  
  
-   Adding files or filegroups.  
  
-   Changing collation. The default collation is the server collation.  
  
-   Changing the database owner. **model** is owned by **sa**.  
  
-   Dropping the database.  
  
-   Dropping the **guest** user from the database.  
  
-   Enabling change data capture.  
  
-   Participating in database mirroring.  
  
-   Removing the primary filegroup, primary data file, or log file.  
  
-   Renaming the database or primary filegroup.  
  
-   Setting the database to OFFLINE.  
  
-   Setting the primary filegroup to READ_ONLY.  
  
-   Creating procedures, views, or triggers using the WITH ENCRYPTION option. The encryption key is tied to the database in which the object is created. Encrypted objects created in the **model** database can only be used in **model**.  
  
## Related Content  
 [System Databases](../../relational-databases/databases/system-databases.md)  
  
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
 [Move Database Files](../../relational-databases/databases/move-database-files.md)  
  
  
