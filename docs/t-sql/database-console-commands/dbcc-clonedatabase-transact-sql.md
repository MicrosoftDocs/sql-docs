---
title: "DBCC CLONEDATABASE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/01/2018"
ms.prod: "sql"
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CLONEDATABASE"
  - "CLONE DATABASE"
  - "DBCC_CLONEDATABASE_TSQL"
  - "DBCC CLONEDATABASE"
  - "DBCC CLONE DATABASE"
  - "CLONEDATABASE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database cloning [SQL Server]"
  - "cloning databases"
  - "clone databases"
  - "cloning database"
  - "clone database"
  - "copying databases"
  - "copy databases"
  - "copying database"
  - "copy database"
  - "NO_STATISTICS option"
  - "NO_QUERYSTORE option"
  - "VERIFY_CLONEDB option"
  - "BACKUP_CLONEDB option"
  - "database copying [SQL Server]"
  - "database cloning [SQL Server]"
  - "DBCC CLONEDATABASE statement"
ms.assetid: 
author: "pamela" 
ms.author: "pamela"
manager: "amitban"
---
# DBCC CLONEDATABASE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

Generates a schema-only clone of a database by using DBCC CLONEDATABASE in order to investigate performance issues related to the query optimizer.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```
DBCC CLONEDATABASE   
(  
    source_database_name
    ,  target_database_name
    [ WITH { [ NO_STATISTICS ] [ , NO_QUERYSTORE ] [ , VERIFY_CLONEDB | SERVICEBROKER ] [ , BACKUP_CLONEDB ] } ]   
)  
```  
  
## Arguments  
*source_database_name*  
The name of the database to be copied. 
  
*target_database_name*  
The name of the database the source database will be copied to. This database will be created by DBCC CLONEDATABASE and shouldn't already exist. 
  
NO_STATISTICS  
Specifies if table/index statistics need to be excluded from the clone. If this option is not specified, table/index statistics are automatically included. This option is available starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 CU3 and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1.

NO_QUERYSTORE<br>
Specifies if query store data needs to be excluded from the clone. If this option is not specified, query store data will be copied to the clone if the query store is enabled in the source database. This option is available starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1.

VERIFY_CLONEDB  
Verifies the consistency of the new database.  This option is required if the cloned database is intended for production use.  Enabling VERIFY_CLONEDB also disables statistics and query store collection, thus it is equivalent to running WITH VERIFY_CLONEDB, NO_STATISTICS, NO_QUERYSTORE.  This option is available starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP3, [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2, and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU8.

> [!NOTE]  
> The following command can be used to confirm that the cloned database is production-ready: <br/>`SELECT DATABASEPROPERTYEX('clone_database_name', 'IsVerifiedClone')`


SERVICEBROKER<br>
Specifies if service broker related system catalogs should be included in the clone.  The SERVICEBROKER option cannot be used in combination with VERIFY_CLONEDB.  This option is available starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP3, [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2, and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU8.

BACKUP_CLONEDB  
Creates and verifies a backup of the clone database.  If used in combination with VERIFY_CLONEDB, the clone database is verified before the backup is taken.  This option is available starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP3, [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2, and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU8.
  
## Remarks
The following validations are performed by DBCC CLONEDATABASE. The command fails if any of the validations fail.
- The source database must be a user database. Cloning of system databases (master, model, msdb, tempdb, distribution database etc.) isn't allowed.
- The source database must be online or readable.
- A database that uses the same name as the clone database must not already exist.
- The command isn't in a user transaction.

If all the validations succeed, the cloning of the source database is performed by the following operations:
- Creates a new destination database that uses the same file layout as the source but with default file sizes from the model database.
- Creates an internal snapshot of the source database.
- Copies the system metadata from the source to the destination database.
- Copies all schema for all objects from the source to the destination database.
- Copies statistics for all indexes from the source to the destination database.

> [!NOTE]  
> The new database generated from DBCC CLONEDATABASE is primarily intended for troubleshooting and diagnostic purposes.  In order for the cloned database to be supported for use as a production database, the VERIFY_CLONEDB option must be used.

All files in the target database will inherit the size and growth settings from the model database. The file names for the destination database will follow the source_file_name _underscore_random number convention. If the generated file name already exists in the destination folder, DBCC CLONEDATABASE will fail.

DBCC CLONEDATABASE doesn't support creation of a clone if there are any user objects (tables, indexes, schemas, roles, and so on) that were created in the model database. If user objects are present in the model database, the database clone fails with following error message:

```
Msg 2601, Level 14, State 1, Line 1
Cannot insert duplicate key row in object <system table> with unique index 'index name'. The duplicate key value is <key value>   
```

> [!IMPORTANT]
> If you have columnstore indexes, see [Considerations when you tune the queries with Columnstore indexes on clone databases](https://blogs.msdn.microsoft.com/sql_server_team/considerations-when-tuning-your-queries-with-columnstore-indexes-on-clone-databases/) to update columnstore index statistics before you run the **DBCC CLONEDATABASE** command.  Starting with SQL Server 2019, the manual steps outlined in the article above will no longer be required as the **DBCC CLONEDATABASE** command gathers this information automatically.

For information related to data security on cloned databases, see [Understanding data security in cloned databases](https://blogs.msdn.microsoft.com/sql_server_team/understanding-data-security-in-cloned-databases-created-using-dbcc-clonedatabase/).

## Internal Database Snapshot
DBCC CLONEDATABASE uses an internal database snapshot of the source database for the transactional consistency that is needed to perform the copy. Using this snapshot prevents blocking and concurrency problems when these commands are executed. If a snapshot can't be created, DBCC CLONEDATABASE will fail. 

Database level locks are held during following steps of the copy process:
- Validate the source database
- Get S lock for the source database
- Create snapshot of the source database
- Create a clone database (an empty database inherited from the model database)
- Get X lock for the clone database
- Copy the metadata to the clone database
- Release all DB locks

As soon as the command has finished running, the internal snapshot is dropped. TRUSTWORTHY and DB_CHAINING options are turned off on a cloned database. 

## Supported Objects
Only the following objects can be cloned in the destination database. Encrypted objects get cloned but aren't usable in the clone database. Any objects that are not listed in the following section aren't supported in the clone: 
- APPLICATION ROLE
- AVAILABILITY GROUP
- COLUMNSTORE INDEX
- CDB
- CDC
- CLR (starting in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 CU3, [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 and later versions)
- DATABASE PROPERTIES
- DEFAULT
- FILES AND FILEGROUPS
- Full text (starting in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 CU2)
- FUNCTION
- INDEX
- LOGIN
- PARTITION FUNCTION
- PARTITION SCHEME
- PROCEDURE   
> [!NOTE]   
> [!INCLUDE[tsql](../../includes/tsql-md.md)] procedures are supported in all releases starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2. CLR procedures are supported starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 CU3. Natively compiled procedures are supported starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1.  

- QUERY STORE (starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1)   
> [!NOTE]   
> Query store data is copied only if it is enabled on the source database. To copy the latest runtime statistics as part of the query store, execute sp_query_store_flush_db to flush the runtime statistics to the query store before executing DBCC CLONEDATABASE.  

- ROLE
- RULE
- SCHEMA
- SEQUENCE
- SPATIAL INDEX
- STATISTICS
- SYNONYM
- TABLE
- MEMORY OPTIMIZED TABLES (only in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 and later versions).
- FILESTREAM AND FILETABLE OBJECTS (Starting in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 CU3, [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 and later versions). 
- TRIGGER
- TYPE
- UPGRADED DB
- USER
- VIEW
- XML INDEX
- XML SCHEMA COLLECTION  

## Permissions  
Requires membership in the **sysadmin** fixed server role.

## Error log messages
The following messages are an example of the messages logged in the error log during the cloning process:

```
2018-03-26 15:33:56.05 spid53 Database cloning for 'sourcedb' has started with target as 'sourcedb_clone'.

2018-03-26 15:33:56.46 spid53 Starting up database 'sourcedb_clone'.

2018-03-26 15:33:57.80 spid53 Setting database option TRUSTWORTHY to OFF for database 'sourcedb_clone'.

2018-03-26 15:33:57.80 spid53 Setting database option DB_CHAINING to OFF for database 'sourcedb_clone'.

2018-03-26 15:33:57.88 spid53 Starting up database 'sourcedb_clone'.

2018-03-26 15:33:57.91 spid53 Database 'sourcedb_clone' is a cloned database. A cloned database should be used for diagnostic purposes only and is not supported for use in a production environment.

2018-03-26 15:33:57.92 spid53 Database cloning for 'sourcedb' has finished. Cloned database is 'sourcedb_clone'.
```

## Database Properties
`DATABASEPROPERTYEX('dbname', 'IsClone')` will return 1 if the database was generated by using DBCC CLONEDATABASE.

`DATABASEPROPERTYEX('dbname', 'IsVerifiedClone')` will return 1 if the database was successfully verified using WITH VERIFY_CLONEDB.

## Examples  
  
### A. Creating a clone of a database that includes schema, statistics and query store 
The following example creates a clone of the AdventureWorks database that includes schema, statistics and query store data ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 and later versions)

```sql  
DBCC CLONEDATABASE (AdventureWorks, AdventureWorks_Clone);    
GO 
```  
  
### B. Creating a schema-only clone of a database without statistics 
The following example creates a clone of the AdventureWorks database that does not include statistics ( [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 CU3 and later versions)

```sql  
DBCC CLONEDATABASE (AdventureWorks, AdventureWorks_Clone) WITH NO_STATISTICS;    
GO 
```  

### C. Creating a schema-only clone of a database without statistics and query store 
The following example creates a clone of the AdventureWorks database that does not include statistics and query store data ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 and later versions)

```sql  
DBCC CLONEDATABASE (AdventureWorks, AdventureWorks_Clone) WITH NO_STATISTICS, NO_QUERYSTORE;    
GO 
```  

### D. Creating a clone of a database that is verified for production use
The following example creates a schema-only clone of the AdventureWorks database without statistics and query store data that is verified for use as a production database ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and later versions).

```sql  
DBCC CLONEDATABASE (AdventureWorks, AdventureWorks_Clone) WITH VERIFY_CLONEDB;    
GO 
```  
  
### E. Creating a clone of a database that is verified for production use that includes a backup of the cloned database
The following example creates a schema-only clone of the AdventureWorks database without statistics and query store data that is verified for use as a production database.  A verified backup of the cloned database will also be created ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and later versions).

```sql  
DBCC CLONEDATABASE (AdventureWorks, AdventureWorks_Clone) WITH VERIFY_CLONEDB, BACKUP_CLONEDB;    
GO 
```

## See Also
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)    
[How to generate a script of the necessary database metadata to create a statistics-only database in SQL Server](https://support.microsoft.com/help/914288)   

