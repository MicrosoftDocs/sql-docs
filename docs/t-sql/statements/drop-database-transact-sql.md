---
title: "DROP DATABASE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/15/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP DATABASE"
  - "DROP_DATABASE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "snapshots [SQL Server database snapshots], deleting"
  - "removing databases"
  - "database snapshots [SQL Server], removing"
  - "deleting databases"
  - "dropping databases"
  - "databases [SQL Server], dropping"
  - "DROP DATABASE statement"
  - "database removal [SQL Server], DROP DATABASE statement"
ms.assetid: 477396a9-92dc-43c9-9b97-42c8728ede8e
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP DATABASE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes one or more user databases or database snapshots from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- SQL Server Syntax  
DROP DATABASE [ IF EXISTS ] { database_name | database_snapshot_name } [ ,...n ] [;]  
```  
  
```  
-- Azure SQL Database, Azure SQL Data Warehouse and Parallel Data Warehouse Syntax   
DROP DATABASE database_name [;]  
```  
  
## Arguments  
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).  
  
 Conditionally drops the database only if it already exists.  
  
 *database_name*  
 Specifies the name of the database to be removed. To display a list of databases, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
 *database_snapshot_name*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the name of a database snapshot to be removed.  
  
## General Remarks  
 A database can be dropped regardless of its state: offline, read-only, suspect, and so on. To display the current state of a database, use the **sys.databases** catalog view.  
  
 A dropped database can be re-created only by restoring a backup. Database snapshots cannot be backed up and, therefore, cannot be restored.  
  
 When a database is dropped, the [master database](../../relational-databases/databases/master-database.md) should be backed up.  
  
 Dropping a database deletes the database from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and deletes the physical disk files used by the database. If the database or any one of its files is offline when it is dropped, the disk files are not deleted. These files can be deleted manually by using Windows Explorer. To remove a database from the current server without deleting the files from the file system, use [sp_detach_db](../../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md).  
  
> [!WARNING]  
>  Dropping a database that has FILE_SNAPSHOT backups associated with it will succeed, but the database files that have associated snapshots will not be deleted to avoid invalidating the backups referring to these database files. The file will be truncated, but will not be physically deleted in order to keep the FILE_SNAPSHOT backups intact. For more information, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md). **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658).  
  
### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 Dropping a database snapshot deletes the database snapshot from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and deletes the physical NTFS File System sparse files used by the snapshot. For information about using sparse files by database snapshots, see [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md). Dropping a database snapshot clears the plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: " [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations". This message is logged every five minutes as long as the cache is flushed within that time interval.  
  
## Interoperability  
  
### SQL Server  
 To drop a database published for transactional replication, or published or subscribed to merge replication, you must first remove replication from the database. If a database is damaged or replication cannot first be removed or both, in most cases you still can drop the database by using ALTER DATABASE to set the database offline and then dropping it.  
  
 If the database is involved in log shipping, remove log shipping before dropping the database. For more information, see [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md).  
  
  
## Limitations and Restrictions  
 [System databases](../../relational-databases/databases/system-databases.md) cannot be dropped.  
  
 The DROP DATABASE statement must run in autocommit mode and is not allowed in an explicit or implicit transaction. Autocommit mode is the default transaction management mode.  
  
 You cannot drop a database currently being used. This means open for reading or writing by any user. One way to remove users from the database is to use ALTER DATABASE to set the database to SINGLE_USER. 
 
 >[!Warning] 
 > This is not a fail-proof approach, since first consecutive connection made by any thread will receive the SINGLE_USER thread, causing your connection to fail. Sql server does not provide a built-in way to drop databases under load.
  
### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 Any database snapshots on a database must be dropped before the database can be dropped.  
  
 Dropping a database enable for Stretch Database does not remove the remote data. If you want to delete the remote data, you have to remove it manually.  
  
### [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]  
 You must be connected to the master database to drop a database.
  
 The DROP DATABASE statement must be the only statement in a SQL batch and you can drop only one database at a time.
  
### [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)]  
 You must be connected to the master database to drop a database.
  
 The DROP DATABASE statement must be the only statement in a SQL batch and you can drop only one database at a time.

## Permissions  
  
### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 Requires the **CONTROL** permission on the database, or **ALTER ANY DATABASE** permission, or membership in the **db_owner** fixed database role.  
  
### [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]  
 Only the server-level principal login (created by the provisioning process) or members of the **dbmanager** database role can drop a database.  
  
### [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 Requires the **CONTROL** permission on the database, or **ALTER ANY DATABASE** permission, or membership in the **db_owner** fixed database role.  
  
## Examples  
  
### A. Dropping a single database  
 The following example removes the `Sales` database.  
  
```  
DROP DATABASE Sales;  
```  
  
### B. Dropping multiple databases  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 The following example removes each of the listed databases.  
  
```  
DROP DATABASE Sales, NewSales;  
```  
  
### C. Dropping a database snapshot  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 The following example removes a database snapshot, named `sales_snapshot0600`, without affecting the source database.  
  
```  
DROP DATABASE sales_snapshot0600;  
```  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md?&tabs=sqlserver)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
  
