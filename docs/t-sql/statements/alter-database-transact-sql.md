---
title: "ALTER DATABASE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/20/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_DATABASE_TSQL"
  - "ALTER DATABASE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "databases [SQL Server], modifying"
  - "ALTER DATABASE statement"
  - "databases [SQL Server], renaming"
  - "renaming databases"
  - "database modifications [SQL Server]"
  - "ALTER DATABASE statement, syntax described"
  - "database names [SQL Server], ALTER DATABASE"
  - "modifying databases"
  - "collations [SQL Server], modifying"
  - "database mirroring [SQL Server], Transact-SQL"
ms.assetid: 15f8affd-8f39-4021-b092-0379fc6983da
caps.latest.revision: 282
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ALTER DATABASE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Modifies a database, or the files and filegroups associated with the database. Adds or removes files and filegroups from a database, changes the attributes of a database or its files and filegroups, changes the database collation, and sets database options. Database snapshots cannot be modified. To modify database options associated with replication, use [sp_replicationdboption](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md).  
   
 Because of its length, the ALTER DATABASE syntax is separated into the following topics:  
  
 ALTER DATABASE  
 The current topic provides the syntax for changing the name and the collation of a database.  
  
 [ALTER DATABASE File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)  
 Provides the syntax for adding and removing files and filegroups from a database, and for changing the attributes of the files and filegroups.  
  
 [ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md)  
 Provides the syntax for changing the attributes of a database by using the SET options of ALTER DATABASE.  
  
 [ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)  
 Provides the syntax for the SET options of ALTER DATABASE that are related to database mirroring.  
  
 [ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md)  
 Provides the syntax for the [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] options of ALTER DATABASE for configuring a secondary database on a secondary replica of an Always On availability group.  
  
 [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)  
 Provides the syntax for the SET options of ALTER DATABASE that are related to database compatibility levels.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
 
For Azure SQL Database, see [ALTER DATABASE &#40;Azure SQL Database&#41;](../../t-sql/statements/alter-database-azure-sql-database.md)  
For Azure SQL Data Warehouse, see [ALTER DATABASE &#40;Azure SQL Data Warehouse&#41;](../../t-sql/statements/alter-database-azure-sql-data-warehouse.md).  
For Parallel Data Warehouse, see [ALTER DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/alter-database-azure-sql-data-warehouse.md).
  
## Syntax  
  
```  
-- SQL Server Syntax  
ALTER DATABASE { database_name  | CURRENT }  
{  
    MODIFY NAME = new_database_name   
  | COLLATE collation_name  
  | <file_and_filegroup_options>  
  | <set_database_options>  
}  
[;]  
  
<file_and_filegroup_options >::=  
  <add_or_modify_files>::=  
  <filespec>::=   
  <add_or_modify_filegroups>::=  
  <filegroup_updatability_option>::=  
  
<set_database_options>::=  
  <optionspec>::=   
  <auto_option> ::=   
  <change_tracking_option> ::=  
  <cursor_option> ::=   
  <database_mirroring_option> ::=   
  <date_correlation_optimization_option> ::=  
  <db_encryption_option> ::=  
  <db_state_option> ::=  
  <db_update_option> ::=  
  <db_user_access_option> ::=  <delayed_durability_option> ::=  <external_access_option> ::=  
  <FILESTREAM_options> ::=  
  <HADR_options> ::=    
  <parameterization_option> ::=  
  <query_store_options> ::=  
  <recovery_option> ::=   
  <service_broker_option> ::=  
  <snapshot_option> ::=  
  <sql_option> ::=   
  <termination> ::=  
  
```  
  
## Arguments  
 *database_name*  
 Is the name of the database to be modified.  
  
> [!NOTE]  
>  This option is not available in a Contained Database.  
  
 CURRENT  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Designates that the current database in use should be altered.  
  
 MODIFY NAME **=***new_database_name*  
 Renames the database with the name specified as *new_database_name*.  
  
 COLLATE *collation_name*  
 Specifies the collation for the database. *collation_name* can be either a Windows collation name or a SQL collation name. If not specified, the database is assigned the collation of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 When creating databases with other than the default collation, the data in the database always respects the specified collation. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], when creating a contained database, the internal catalog information is maintained using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default collation, **Latin1_General_100_CI_AS_WS_KS_SC**.  
  
 For more information about the Windows and SQL collation names, see [COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md).  
  
 **\<delayed_durability_option> ::=**  
 **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 For more information see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md) and [Control Transaction Durability](../../relational-databases/logs/control-transaction-durability.md).  
  
 **\<file_and_filegroup_options>::=**  
 For more information, see [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).  
  
## Remarks  
 To remove a database, use [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md).  
  
 To decrease the size of a database, use [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).  
  
 The ALTER DATABASE statement must run in autocommit mode (the default transaction management mode) and is not allowed in an explicit or implicit transaction.  
  
 The state of a database file (for example, online or offline), is maintained independently from the state of the database. For more information, see [File States](../../relational-databases/databases/file-states.md). The state of the files within a filegroup determines the availability of the whole filegroup. For a filegroup to be available, all files within the filegroup must be online. If a filegroup is offline, any try to access the filegroup by an SQL statement will fail with an error. When you build query plans for SELECT statements, the query optimizer avoids nonclustered indexes and indexed views that reside in offline filegroups. This enables these statements to succeed. However, if the offline filegroup contains the heap or clustered index of the target table, the SELECT statements fail. Additionally, any INSERT, UPDATE, or DELETE statement that modifies a table with any index in an offline filegroup will fail.  
  
 When a database is in the RESTORING state, most ALTER DATABASE statements will fail. The exception is setting database mirroring options. A database may be in the RESTORING state during an active restore operation or when a restore operation of a database or log file fails because of a corrupted backup file.  
  
 The plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is cleared by setting one of the following options.  
  
|||  
|-|-|  
|OFFLINE|READ_WRITE|  
|ONLINE|MODIFY FILEGROUP DEFAULT|  
|MODIFY_NAME|MODIFY FILEGROUP READ_WRITE|  
|COLLATE|MODIFY FILEGROUP READ_ONLY|  
|READ_ONLY|PAGE_VERIFY|  
  
 Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: "[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations". This message is logged every five minutes as long as the cache is flushed within that time interval.  
  
 The procedure cache is also flushed in the following scenarios:  
  
-   A database has the AUTO_CLOSE database option set to ON. When no user connection references or uses the database, the background task tries to close and shut down the database automatically.  
  
-   You run several queries against a database that has default options. Then, the database is dropped.  
  
-   A database snapshot for a source database is dropped.  
  
-   You successfully rebuild the transaction log for a database.  
  
-   You restore a database backup.  
  
-   You detach a database.  
  
## Changing the Database Collation  
 Before you apply a different collation to a database, make sure that the following conditions are in place:  
  
-   You are the only one currently using the database.  
  
-   No schema-bound object depends on the collation of the database.  
  
     If the following objects, which depend on the database collation, exist in the database, the ALTER DATABASE*database_name*COLLATE statement will fail. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return an error message for each object blocking the ALTER action:  
  
    -   User-defined functions and views created with SCHEMABINDING.  
  
    -   Computed columns.  
  
    -   CHECK constraints.  
  
    -   Table-valued functions that return tables with character columns with collations inherited from the default database collation.  
  
     Dependency information for non-schema-bound entities is automatically updated when the database collation is changed.  
  
 Changing the database collation does not create duplicates among any system names for the database objects. If duplicate names result from the changed collation, the following namespaces may cause the failure of a database collation change:  
  
-   Object names such as a procedure, table, trigger, or view.  
  
-   Schema names.  
  
-   Principals such as a group, role, or user.  
  
-   Scalar-type names such as system and user-defined types.  
  
-   Full-text catalog names.  
  
-   Column or parameter names within an object.  
  
-   Index names within a table.  
  
Duplicate names resulting from the new collation will cause the change action to fail, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return an error message specifying the namespace where the duplicate was found.  
  
## Viewing Database Information  
 You can use catalog views, system functions, and system stored procedures to return information about databases, files, and filegroups.  
  
## Permissions  
 Requires ALTER permission on the database.  
  
## Examples  
  
### A. Changing the name of a database  
 The following example changes the name of the `AdventureWorks2012` database to `Northwind`.  
  
```  
USE master;  
GO  
ALTER DATABASE AdventureWorks2012  
Modify Name = Northwind ;  
GO  
```  
  
### B. Changing the collation of a database  
 The following example creates a database named `testdb` with the `SQL_Latin1_General_CP1_CI_A`S collation, and then changes the collation of the `testdb` database to `COLLATE French_CI_AI`.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
USE master;  
GO  
  
CREATE DATABASE testdb  
COLLATE SQL_Latin1_General_CP1_CI_AS ;  
GO  
  
ALTER DATABASE testDB  
COLLATE French_CI_AI ;  
GO  
```  
  
## See Also  
- [ALTER DATABASE &#40;Azure SQL Database&#41;](https://msdn.microsoft.com/library/mt574871.aspx)  
- [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)   
- [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md)   
- [DROP DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-transact-sql.md)   
- [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)   
- [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
- [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
- [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)   
- [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
- [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
- [sys.database_mirroring_witnesses &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)   
- [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)   
- [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)   
- [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)   
- [System Databases](../../relational-databases/databases/system-databases.md)  
  
