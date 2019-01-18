---
title: "msdb Database | Microsoft Docs"
ms.custom: ""
ms.date: "11/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, msdb database"
  - "alerts [SQL Server], msdb database"
  - "jobs [SQL Server], msdb database"
  - "msdb database [SQL Server]"
ms.assetid: 5032cb2d-65a0-40dd-b569-4dcecdd58ceb
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# msdb Database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  The **msdb** database is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent for scheduling alerts and jobs and by other features such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[ssSB](../../includes/sssb-md.md)] and Database Mail.  
  
 For example, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically maintains a complete online backup-and-restore history within tables in **msdb**. This information includes the name of the party that performed the backup, the time of the backup, and the devices or files where the backup is stored. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses this information to propose a plan for restoring a database and applying any transaction log backups. Backup events for all databases are recorded even if they were created with custom applications or third-party tools. For example, if you use a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] application that calls SQL Server Management Objects (SMO) objects to perform backup operations, the event is logged in the **msdb** system tables, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log, and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. To help your protect the information that is stored in **msdb**, we recommend that you consider placing the **msdb** transaction log on fault tolerant storage.  
  
 By default, **msdb** uses the simple recovery model. If you use the [backup and restore history](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md) tables, we recommend that you use the full recovery model for **msdb**. For more information, see [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md). Notice that when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed or upgraded and whenever Setup.exe is used to rebuild the system databases, the recovery model of **msdb** is automatically set to simple.  
  
> [!IMPORTANT]  
>  After any operation that updates **msdb**, such as backing up or restoring any database, we recommend that you back up **msdb**. For more information, see [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).  
  
## Physical Properties of msdb  
 The following table lists the initial configuration values of the **msdb** data and log files. The sizes of these files may vary slightly for different editions of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
|File|Logical name|Physical name|File growth|  
|----------|------------------|-------------------|-----------------|  
|Primary data|MSDBData|MSDBData.mdf|Autogrow by 10 percent until the disk is full.|  
|Log|MSDBLog|MSDBLog.ldf|Autogrow by 10 percent to a maximum of 2 terabytes.|  
  
 To move the **msdb** database or log files, see [Move System Databases](../../relational-databases/databases/move-system-databases.md).  
  
### Database Options  
 The following table lists the default value for each database option in the **msdb** database and whether the option can be modified. To view the current settings for these options, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
|Database option|Default value|Can be modified|  
|---------------------|-------------------|---------------------|  
|ALLOW_SNAPSHOT_ISOLATION|ON|No|  
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
|DB_CHAINING|ON|Yes|  
|ENCRYPTION|OFF|No|  
|MIXED_PAGE_ALLOCATION|ON|No|  
|NUMERIC_ROUNDABORT|OFF|Yes|  
|PAGE_VERIFY|CHECKSUM|Yes|  
|PARAMETERIZATION|SIMPLE|Yes|  
|QUOTED_IDENTIFIER|OFF|Yes|  
|READ_COMMITTED_SNAPSHOT|OFF|No|  
|RECOVERY|SIMPLE|Yes|  
|RECURSIVE_TRIGGERS|OFF|Yes|  
|Service Broker Options|ENABLE_BROKER|Yes|  
|TRUSTWORTHY|ON|Yes|  
  
 For a description of these database options, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
## Restrictions  
 The following operations cannot be performed on the **msdb** database:  
  
-   Changing collation. The default collation is the server collation.  
  
-   Dropping the database.  
  
-   Dropping the **guest** user from the database.  
  
-   Enabling change data capture.  
  
-   Participating in database mirroring.  
  
-   Removing the primary filegroup, primary data file, or log file.  
  
-   Renaming the database or primary filegroup.  
  
-   Setting the database to OFFLINE.  
  
-   Setting the primary filegroup to READ_ONLY.  
  
## Related Content  
 [System Databases](../../relational-databases/databases/system-databases.md)  
  
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
 [Move Database Files](../../relational-databases/databases/move-database-files.md)  
  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)  
  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  
  
  
