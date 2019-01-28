---
title: "master Database | Microsoft Docs"
ms.custom: ""
ms.date: "01/28/2019"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "master database [SQL Server], about"
  - "master database [SQL Server]"
ms.assetid: 660e909f-61eb-406b-bbce-8864dd629ba0
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# master Database

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The **master** database records all the system-level information for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system. This includes instance-wide metadata such as logon accounts, endpoints, linked servers, and system configuration settings. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], system objects are no longer stored in the **master** database; instead, they are stored in the [Resource database](../../relational-databases/databases/resource-database.md). Also, **master** is the database that records the existence of all other databases and the location of those database files and records the initialization information for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot start if the **master** database is unavailable.  

> [!IMPORTANT]
> For Azure SQL Database standalone databases and elastic pools, only master database and tempdb database apply. For more information, see [What is an Azure SQL Database server](https://docs.microsoft.com/azure/sql-database/sql-database-servers#what-is-an-azure-sql-database-server). For a discussion of tempdb in the context of Azure SQL Database, see [tempdb database in Azure SQL Database](tempdb-database.md#tempdb-database-in-sql-database). For Azure SQL Database Managed Instance, all system databases apply. For more information on Managed Instances in Azure SQL Database, see [What is a Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance)
  
## Physical Properties of master

The following table lists the initial configuration values of the **master** data and log files for SQL Server and Azure SQL Database Managed Instance. The sizes of these files may vary slightly for different editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|File|Logical name|Physical name|File growth|  
|----------|------------------|-------------------|-----------------|  
|Primary data|master|master.mdf|Autogrow by 10 percent until the disk is full.|  
|Log|mastlog|mastlog.ldf|Autogrow by 10 percent to a maximum of 2 terabytes.|  
  
For information about how to move the **master** data and log files, see [Move System Databases](../../relational-databases/databases/move-system-databases.md).  

> [!IMPORTANT]
> For Azure SQL Database server, the user has no control over the size of the **master** database.
  
### Database Options

The following table lists the default value for each database option in the **master** database for SQL Server and Azure SQL Database Managed Instance and whether the option can be modified. To view the current settings for these options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
> [!IMPORTANT]
> For Azure SQL Database standalone databases and elastic pools, the user has no control over these database options.

|Database option|Default value|Can be modified|  
|---------------------|-------------------|---------------------|  
|ALLOW_SNAPSHOT_ISOLATION|ON|No|  
|ANSI_NULL_DEFAULT|OFF|Yes|  
|ANSI_NULLS|OFF|Yes|  
|ANSI_PADDING|OFF|Yes|  
|ANSI_WARNINGS|OFF|Yes|  
|ARITHABORT|OFF|Yes|  
|AUTO_CLOSE|OFF|No|  
|AUTO_CREATE_STATISTICS|ON|Yes|  
|AUTO_SHRINK|OFF|No|  
|AUTO_UPDATE_STATISTICS|ON|Yes|  
|AUTO_UPDATE_STATISTICS_ASYNC|OFF|Yes|  
|CHANGE_TRACKING|OFF|No|  
|CONCAT_NULL_YIELDS_NULL|OFF|Yes|  
|CURSOR_CLOSE_ON_COMMIT|OFF|Yes|  
|CURSOR_DEFAULT|GLOBAL|Yes|  
|Database Availability Options|ONLINE<br /><br /> MULTI_USER<br /><br /> READ_WRITE|No<br /><br /> No<br /><br /> No|  
|DATE_CORRELATION_OPTIMIZATION|OFF|Yes|  
|DB_CHAINING|ON|No|  
|ENCRYPTION|OFF|No|  
|MIXED_PAGE_ALLOCATION|ON|No|  
|NUMERIC_ROUNDABORT|OFF|Yes|  
|PAGE_VERIFY|CHECKSUM|Yes|  
|PARAMETERIZATION|SIMPLE|Yes|  
|QUOTED_IDENTIFIER|OFF|Yes|  
|READ_COMMITTED_SNAPSHOT|OFF|No|  
|RECOVERY|SIMPLE|Yes|  
|RECURSIVE_TRIGGERS|OFF|Yes|  
|Service Broker Options|DISABLE_BROKER|No|  
|TRUSTWORTHY|OFF|Yes|  
  
For a description of these database options, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
## Restrictions  
The following operations cannot be performed on the **master** database:  
  
- Adding files or filegroups.  
- Changing collation. The default collation is the server collation.  
- Changing the database owner. **master** is owned by **sa**.  
- Creating a full-text catalog or full-text index.  
- Creating triggers on system tables in the database.  
- Dropping the database.  
- Dropping the **guest** user from the database.  
- Enabling change data capture.  
- Participating in database mirroring.  
- Removing the primary filegroup, primary data file, or log file.  
- Renaming the database or primary filegroup.  
- Setting the database to OFFLINE.  
- Setting the database or primary filegroup to READ_ONLY.  
  
## Recommendations  
When you work with the **master** database, consider the following recommendations:  
  
- Always have a current backup of the **master** database available.  
- Back up the **master** database as soon as possible after the following operations:  
  
  - Creating, modifying, or dropping any database  
  - Changing server or database configuration values  
  - Modifying or adding logon accounts  
  
- Do not create user objects in **master**. If you do, **master** must be backed up more frequently.  
- Do not set the TRUSTWORTHY option to ON for the **master** database.  
  
## What to Do If master Becomes Unusable  
 If **master** becomes unusable, you can return the database to a usable state in either of the following ways:  
  
- Restore **master** from a current database backup.  
  
  If you can start the server instance, you should be able to restore **master** from a full database backup. For more information, see [Restore the master Database &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/restore-the-master-database-transact-sql.md).  
  
- Rebuild **master** completely.  
  
  If severe damage to **master** prevents you from starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must rebuild **master**. For more information, see [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md).  
  
  > [!IMPORTANT]  
  >  Rebuilding **master** rebuilds all of the system databases.  
  
## Related Content  
- [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md)  
- [System Databases](../../relational-databases/databases/system-databases.md)  
- [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
- [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
- [Move Database Files](../../relational-databases/databases/move-database-files.md)  
