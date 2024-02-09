---
title: "DBCC CLONEDATABASE (Transact-SQL)"
description: DBCC CLONEDATABASE generates a schema-only clone of a database by using DBCC CLONEDATABASE in order to investigate performance issues related to the query optimizer.
author: bluefooted
ms.author: pamela
ms.reviewer: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "CLONEDATABASE"
  - "CLONE DATABASE"
  - "DBCC_CLONEDATABASE_TSQL"
  - "DBCC CLONEDATABASE"
  - "DBCC CLONE DATABASE"
  - "CLONEDATABASE_TSQL"
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
  - "DBCC CLONEDATABASE statement"
dev_langs:
  - "TSQL"
---
# DBCC CLONEDATABASE (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Generates a schema-only, read-only copy of a database by using `DBCC CLONEDATABASE` in order to investigate performance issues related to the query optimizer.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC CLONEDATABASE
(
    source_database_name
    ,  target_database_name
)
    [ WITH { [ NO_STATISTICS ] [ , NO_QUERYSTORE ] [ , VERIFY_CLONEDB | SERVICEBROKER ] [ , BACKUP_CLONEDB ] } ]
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *source_database_name*

The name of the database to be copied.

#### *target_database_name*

The name of the database the source database will be copied to. This database will be created by `DBCC CLONEDATABASE` and shouldn't already exist.

#### NO_STATISTICS

**Applies to:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 2 CU 3, [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 1, and later versions.

Specifies if table/index statistics need to be excluded from the clone. If this option isn't specified, table/index statistics are automatically included.

#### NO_QUERYSTORE

**Applies to:** [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 1 and later versions.

Specifies if Query Store data needs to be excluded from the clone. If this option isn't specified, Query Store data will be copied to the clone if the Query Store is enabled in the source database.

#### VERIFY_CLONEDB

**Applies to:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 3, [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 2, [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU 8, and later versions.

Verifies the consistency of the new database. Enabling `VERIFY_CLONEDB` also disables statistics and Query Store collection, thus it is equivalent to running `WITH VERIFY_CLONEDB, NO_STATISTICS, NO_QUERYSTORE`.

The following command can be used to determine if the cloned database has been verified:

```sql
SELECT DATABASEPROPERTYEX('clone_database_name', 'IsVerifiedClone');
```

#### SERVICEBROKER

**Applies to:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 3, [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 2, [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU 8, and later versions.

Specifies if service broker related system catalogs should be included in the clone. The `SERVICEBROKER` option can't be used in combination with `VERIFY_CLONEDB`.

#### BACKUP_CLONEDB

**Applies to:** [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 3, [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 2, [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU 8, and later versions.

Creates and verifies a backup of the clone database. If used in combination with `VERIFY_CLONEDB`, the clone database is verified before the backup is taken.

## Remarks

A clone of a database generated with `DBCC CLONEDATABASE` is only intended for troubleshooting and diagnostic purposes. The clone is a read-only, schema-only copy of the original database and has limitations on which objects are copied over. See the [Supported objects](#supported-objects) section for more details. Any other use of a clone database isn't supported. 

The following validations are performed by `DBCC CLONEDATABASE`. The command fails if any of the validations fail.

- The source database must be a user database. Cloning of system databases (`master`, `model`, `msdb`, `tempdb`, `distribution` database, and so on) isn't allowed.
- The source database must be online or readable.
- A database that uses the same name as the clone database must not already exist.
- The command isn't in a user transaction.

If all the validations succeed, the cloning of the source database is performed by the following operations:

- Creates a new destination database that uses the same file layout as the source but with default file sizes from the `model` database.
- Creates an internal snapshot of the source database.
- Copies the system metadata from the source to the destination database.
- Copies all schema for all objects from the source to the destination database.
- Copies statistics for all indexes from the source to the destination database.


All files in the target database will inherit the size and growth settings from the `model` database. The file names for the destination database will follow the `<source_file_name_underscore_random number>` convention. If the generated file name already exists in the destination folder, `DBCC CLONEDATABASE` will fail.

`DBCC CLONEDATABASE` doesn't support creation of a clone if there are any user objects (tables, indexes, schemas, roles, and so on) that were created in the `model` database. If user objects are present in the `model` database, the database clone fails with following error message:

```output
Msg 2601, Level 14, State 1, Line 1
Cannot insert duplicate key row in object <system table> with unique index 'index name'. The duplicate key value is <key value>
```

> [!IMPORTANT]  
> If you have columnstore indexes, see [Considerations when you tune the queries with Columnstore indexes on clone databases](https://techcommunity.microsoft.com/t5/SQL-Server/Considerations-when-tuning-your-queries-with-columnstore-indexes/ba-p/385294) to update columnstore index statistics before you run the `DBCC CLONEDATABASE` command. Starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], the manual steps outlined in the article above will no longer be required as the `DBCC CLONEDATABASE` command gathers this information automatically.

## <a id="ctp23"></a> Stats blob for columnstore indexes

Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], `DBCC CLONEDATABASE` automatically captures the stats blobs for columnstore indexes, so no manual steps are required. `DBCC CLONEDATABASE` creates a schema-only copy of a database that includes all the elements necessary to troubleshoot query performance issues without copying the data. In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the command didn't copy the statistics necessary to accurately troubleshoot columnstore index queries and manual steps were required to capture this information.

For information related to data security on cloned databases, see [Understanding data security in cloned databases](https://techcommunity.microsoft.com/t5/SQL-Server/Understanding-data-security-in-cloned-databases-created-using/ba-p/385287).

## Internal database snapshot

`DBCC CLONEDATABASE` uses an internal database snapshot of the source database for the transactional consistency that is needed to perform the copy. Using this snapshot prevents blocking and concurrency problems when these commands are executed. If a snapshot can't be created, `DBCC CLONEDATABASE` will fail.

Database level locks are held during following steps of the copy process:

- Validate the source database
- Get shared (S) lock for the source database
- Create snapshot of the source database
- Create a clone database (an empty database inherited from the `model` database)
- Get exclusive (X) lock for the clone database
- Copy the metadata to the clone database
- Release all database locks

As soon as the command has finished running, the internal snapshot is dropped. `TRUSTWORTHY` and `DB_CHAINING` options are turned off on a cloned database.

## Supported objects

Only the following objects can be cloned in the destination database. Encrypted objects get cloned but aren't usable in the clone database. Any objects that aren't listed in the following section aren't supported in the clone:

- APPLICATION ROLE
- AVAILABILITY GROUP
- COLUMNSTORE INDEX
- CDB
- CDC
- Change Tracking <sup>6, 7, 8</sup>
- CLR <sup>1, 2</sup>
- DATABASE PROPERTIES
- DEFAULT
- FILES AND FILEGROUPS
- Full text <sup>3</sup>
- FUNCTION
- INDEX
- LOGIN
- PARTITION FUNCTION
- PARTITION SCHEME
- PROCEDURE <sup>4</sup>
- QUERY STORE <sup>2, 5</sup>
- ROLE
- RULE
- SCHEMA
- SEQUENCE
- SPATIAL INDEX
- STATISTICS
- SYNONYM
- TABLE <sup>9</sup>
- MEMORY OPTIMIZED TABLES <sup>2</sup>
- FILESTREAM AND FILETABLE OBJECTS <sup>1, 2</sup>
- TRIGGER
- TYPE
- UPGRADED DB
- USER
- VIEW
- XML INDEX
- XML SCHEMA COLLECTION

<sup>1</sup> Starting in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 2 CU 3.

<sup>2</sup> Starting in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 1.

<sup>3</sup> Starting in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 1 CU 2.

<sup>4</sup> [!INCLUDE[tsql](../../includes/tsql-md.md)] procedures are supported in all releases starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 2. CLR procedures are supported starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 2 CU 3. Natively compiled procedures are supported starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 1.

<sup>5</sup> Query Store data is copied only if it is enabled on the source database. To copy the latest runtime statistics as part of the Query Store, execute `sp_query_store_flush_db` to flush the runtime statistics to the Query Store before executing `DBCC CLONEDATABASE`.

<sup>6</sup> Starting in [!INCLUDE[ssSQL16-md](../../includes/sssql16-md.md)] Service Pack 2 CU 10.

<sup>7</sup> Starting in [!INCLUDE[ssSQL17-md](../../includes/sssql17-md.md)] Service Pack 2 CU 17.

<sup>8</sup> Starting in [!INCLUDE[ssSQL19-md](../../includes/sssql19-md.md)] CU 1 and later versions.

<sup>9</sup> Most system tables flagged as `is_ms_shipped` aren't cloned.

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Error log messages

The following messages are an example of the messages logged in the error log during the cloning process:

```output
2018-03-26 15:33:56.05 spid53 Database cloning for 'sourcedb' has started with target as 'sourcedb_clone'.

2018-03-26 15:33:56.46 spid53 Starting up database 'sourcedb_clone'.

2018-03-26 15:33:57.80 spid53 Setting database option TRUSTWORTHY to OFF for database 'sourcedb_clone'.

2018-03-26 15:33:57.80 spid53 Setting database option DB_CHAINING to OFF for database 'sourcedb_clone'.

2018-03-26 15:33:57.88 spid53 Starting up database 'sourcedb_clone'.

2018-03-26 15:33:57.91 spid53 Database 'sourcedb_clone' is a cloned database. A cloned database should be used for diagnostic purposes only and is not supported for use in a production environment.

2018-03-26 15:33:57.92 spid53 Database cloning for 'sourcedb' has finished. Cloned database is 'sourcedb_clone'.
```

## About service packs for SQL Server

Service packs are cumulative. Each new service pack contains all the fixes that are in previous service packs, together with any new fixes. Our recommendation is to apply the latest service pack and the latest cumulative update for that service pack. You don't have to install a previous service pack before you install the latest service pack. See **Table 1** in [Latest updates and version history for SQL Server](/troubleshoot/sql/releases/download-and-install-latest-updates) for finding more information about the latest service pack and latest cumulative update.

> [!NOTE]
> The newly generated database generated from DBCC CLONEDATABASE isn't supported to be used as a production database and is primarily intended for troubleshooting and diagnostic purposes. We recommend detaching the cloned database after the database is created.

## Database properties

`DATABASEPROPERTYEX('dbname', 'IsClone')` will return 1 if the database was generated by using `DBCC CLONEDATABASE`.

`DATABASEPROPERTYEX('dbname', 'IsVerifiedClone')` will return 1 if the database was successfully verified using WITH `VERIFY_CLONEDB`.

## Examples

### A. Create a clone of a database that includes schema, statistics and Query Store

The following example creates a clone of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database that includes schema, statistics and Query Store data ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 1 and later versions):

```sql
DBCC CLONEDATABASE (AdventureWorks2022, AdventureWorks_Clone);
GO
```

### B. Create a schema-only clone of a database without statistics

The following example creates a clone of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database that doesn't include statistics ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Service Pack 2 CU 3 and later versions):

```sql
DBCC CLONEDATABASE (AdventureWorks2022, AdventureWorks_Clone) WITH NO_STATISTICS;
GO
```

### C. Create a schema-only clone of a database without statistics and Query Store

The following example creates a clone of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database that doesn't include statistics and Query Store data ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 1 and later versions):

```sql
DBCC CLONEDATABASE (AdventureWorks2022, AdventureWorks_Clone) WITH NO_STATISTICS, NO_QUERYSTORE;
GO
```

### D. Create a clone of a database that is verified

The following example creates a schema-only clone of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database without statistics and Query Store data that is verified ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 2 and later versions):

```sql
DBCC CLONEDATABASE (AdventureWorks2022, AdventureWorks_Clone) WITH VERIFY_CLONEDB;
GO
```

### E. Create a clone of a database that is verified for use that includes a backup of the cloned database

The following example creates a schema-only clone of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database without statistics and Query Store data that is verified for use. A verified backup of the cloned database will also be created ([!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] Service Pack 2 and later versions).

```sql
DBCC CLONEDATABASE (AdventureWorks2022, AdventureWorks_Clone) WITH VERIFY_CLONEDB, BACKUP_CLONEDB;
GO
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [How to generate a script of the necessary database metadata to create a statistics-only database in SQL Server](https://support.microsoft.com/help/914288)
