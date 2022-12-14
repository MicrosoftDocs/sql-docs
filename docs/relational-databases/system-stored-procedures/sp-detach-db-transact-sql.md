---
description: "sp_detach_db (Transact-SQL)"
title: "sp_detach_db (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/30/2015"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_detach_db"
  - "sp_detach_db_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_detach_db"
  - "detaching databases [SQL Server]"
ms.assetid: abcb1407-ff78-4c76-b02e-509c86574462
author: markingmyname
ms.author: maghan
---
# sp_detach_db (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Detaches a database that is currently not in use from a server instance and, optionally, runs UPDATE STATISTICS on all tables before detaching.  
  
> [!IMPORTANT]  
>  For a replicated database to be detached, it must be unpublished. For more information, see the "Remarks" section later in this topic.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_detach_db [ @dbname= ] 'database_name'   
    [ , [ @skipchecks= ] 'skipchecks' ]   
    [ , [ @keepfulltextindexfile = ] 'KeepFulltextIndexFile' ]   
```  
  
## Arguments  
`[ @dbname = ] 'database_name'`
 Is the name of the database to be detached. *database_name* is a **sysname** value, with a default value of NULL.  
  
`[ @skipchecks = ] 'skipchecks'`
 Specifies whether to skip or run UPDATE STATISTIC. *skipchecks* is a **nvarchar(10)** value, with a default value of NULL. To skip UPDATE STATISTICS, specify **true**. To explicitly run UPDATE STATISTICS, specify **false**.  
  
 By default, UPDATE STATISTICS is performed to update information about the data in the tables and indexes. Performing UPDATE STATISTICS is useful for databases that are to be moved to read-only media.  
  
`[ @keepfulltextindexfile = ] 'KeepFulltextIndexFile'`
 Specifies that the full-text index file associated with the database that is being detached will not be dropped during the database detach operation. *KeepFulltextIndexFile* is a **nvarchar(10)** value with a default of **true**. If *KeepFulltextIndexFile* is **false**, all the full-text index files associated with the database and the metadata of the full-text index are dropped, unless the database is read-only. If NULL or **true**, full-text related metadata are kept.  
  
> [!IMPORTANT]
>  The **\@keepfulltextindexfile** parameter will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Do not use this parameter in new development work, and modify applications that currently use this parameter as soon as possible.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 When a database is detached, all its metadata is dropped. If the database was the default database of any login accounts, **master** becomes their default database.  
  
> [!NOTE]  
>  For information about how to view the default database of all the login accounts, see [sp_helplogins &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helplogins-transact-sql.md). If you have the required permissions, you can use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) to assign a new default database to a login.  
  
## Restrictions  
 A database cannot be detached if any of the following are true:  
  
-   The database is currently in use. For more information, see "Obtaining Exclusive Access," later in this topic.  
  
-   If replicated, the database is published.  
  
     Before you can detach the database, you must disable publishing by running [sp_replicationdboption](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md).  
  
    > [!NOTE]  
    >  If you cannot use **sp_replicationdboption**, you can remove replication by running [sp_removedbreplication](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md).  
  
-   A database snapshot exists on the database.  
  
     Before you can detach the database, you must drop all of its snapshots. For more information, see [Drop a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/drop-a-database-snapshot-transact-sql.md).  
  
    > [!NOTE]  
    >  A database snapshot cannot be detached or attached.  
  
-   The database is being mirrored.  
  
     The database cannot be detached until the database mirroring session is terminated. For more information, see [Removing Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/removing-database-mirroring-sql-server.md).  
  
-   The database is suspect.  
  
     You must put a suspect database into emergency mode before you can detach the database. For more information about how to put a database into emergency mode, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
-   The database is a system database.  
  
## Obtaining Exclusive Access  
 Detaching a database requires exclusive access to the database. If the database that you want to detach is in use, before you can detach it, set the database to SINGLE_USER mode to obtain exclusive access.

 Before you set the database to SINGLE_USER, verify that the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF. When this option is set to ON, the background thread that is used to update statistics takes a connection against the database, and you will be unable to access the database in single-user mode. For more information, see [set a database to single user mode](../databases/set-a-database-to-single-user-mode.md).

 For example, the following `ALTER DATABASE` statement obtains exclusive access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database after all current users disconnect from the database.  
  
```  
USE master;  
ALTER DATABASE AdventureWorks2012  
SET SINGLE_USER;  
GO  
```  
  
> [!NOTE]  
>  To force current users out of the database immediately or within a specified number of seconds, also use the ROLLBACK option: ALTER DATABASE *database_name* SET SINGLE_USER WITH ROLLBACK *rollback_option*. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
## Reattaching a Database  
 The detached files remain and can be reattached by using CREATE DATABASE (with the FOR ATTACH or FOR ATTACH_REBUILD_LOG option). The files can be moved to another server and attached there.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role or membership in the **db_owner** role of the database.  
  
## Examples  
 The following example detaches the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database with *skipchecks* set to true.  
  
```  
EXEC sp_detach_db 'AdventureWorks2012', 'true';  
```  
  
 The following example detaches the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database and keeps the full-text index files and the metadata of the full-text index. This command runs UPDATE STATISTICS, which is the default behavior.  
  
```  
exec sp_detach_db @dbname='AdventureWorks2012'  
    , @keepfulltextindexfile='true';  
```  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md)   
 [Detach a Database](../../relational-databases/databases/detach-a-database.md)  
  
