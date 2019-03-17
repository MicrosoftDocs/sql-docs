---
title: "What's New in Database Engine - SQL Server 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "what's new [SQL Server Database Engine]"
  - "Database Engine [SQL Server], what's new"
ms.assetid: 8f625d5a-763c-4440-97b8-4b823a6e2439
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# What's new in Database Engine - SQL Server 2016
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

This topic summarizes the enhancements introduced in the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] release of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)].  The new features and enhancements increase the power and productivity of architects, developers, and administrators who design, develop, and maintain data storage systems.

To review what is new in the other SQL Server components, see [What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md).

> [!NOTE]
>  [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] is a 64-bit application. 32-bit installation is discontinued, though some elements run as 32-bit components.

#### Try it out

- To download [!INCLUDE[ssSQL15](../includes/sssql15-md.md)], go to  **[Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016)**![download](../analysis-services/media/download.png "download").

- Have an Azure account?  Then go **[Here](https://azure.microsoft.com/services/virtual-machines/sql-server/)** to spin up a Virtual Machine with [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] already installed.

> [!NOTE]
> For the current release notes, see [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md).
  
## SQL Server 2016 Service Pack 1 (SP1)  
-  `CREATE OR ALTER <object>` syntax is now available for [procedures](../t-sql/statements/create-procedure-transact-sql.md), [views](../t-sql/statements/create-view-transact-sql.md), [functions](../t-sql/statements/create-function-transact-sql.md), and [triggers](../t-sql/statements/create-trigger-transact-sql.md).
- 	Support for a more generic query hinting model has been added: `OPTION (USE HINT('<hint1>', '<hint2>'))`. For more information, see [Query Hints (Transact-SQL)](../t-sql/queries/hints-transact-sql-query.md).  
- The [sys.dm_exec_valid_use_hints](../relational-databases/system-dynamic-management-views/sys-dm-exec-valid-use-hints-transact-sql.md) DMV is added to list hints.  
- The [sys.dm_exec_query_statistics_xml](../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md) DMV is added to return showplan XML transient statistics.  
- The [sys.dm_db_incremental_stats_properties](../relational-databases/system-dynamic-management-views/sys-dm-db-incremental-stats-properties-transact-sql.md) DMV is added to incremental statistics for the specified table.  
- The `instant_file_initialization_enabled` column is added to [sys.dm_server_services](../relational-databases/system-dynamic-management-views/sys-dm-server-services-transact-sql.md).  
- The of	`estimated_read_row_count` column is added to [sys.dm_exec_query_profiles](../relational-databases/system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md).  
-  The `sql_memory_model` and `sql_memory_model_desc` columns are added to [sys.dm_os_sys_info](../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) to provide information about the locking model for memory pages.
-  The editions that support a number of features has been expanded. This includes adding row-level security, Always Encrypted, dynamic data masking, database auditing, in-memory OLTP and several other features to all editions. For more information see [Editions and Supported Features for SQL Server 2016](../sql-server/editions-and-supported-features-for-sql-server-2016.md).   
-  [sp_refresh_parameter_encryption](../relational-databases/system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md) allows Always On encryption to update metadata, when objects encrypted using Always On are redefined.  


##  <a name="Feature"></a> SQL Server 2016 RTM
This section contains the following subsections:

-   [Columnstore Indexes](#columnstore)
-   [Database Scoped Configurations](#scopedconfiguration)
-   [In-Memory OLTP](#InMemory)
-   [Query Optimizer](#QueryOptimizer)
-   [Live Query Statistics](#LiveStats)
-   [Query Store](#QueryStore)
-   [Temporal Tables](#TT)
-   [Striped Backups to Microsoft Azure Blob Storage](#StripedBackupToAzure)
-   [File-Snapshot Backups to Microsoft Azure Blob Storage](#FileSnapshotBackup)
-   [Managed Backup](#ManagedBackup)
-   [TempDB Database](#multipleTempDB)
-   [Built-in JSON Support](#ForJson)
-   [PolyBase](#bkPolyBase)
-   [Stretch Database](#stretch)
-   [Support for UTF-8](#UTF8)
-   [New Default Database Size and Autogrow Values](#DefaultDB)
-   [Transact-SQL Enhancements](#TSQL)
-   [System View Enhancements](#SystemTable)
-   [Security Enhancements](#Security)
-   [High Availability Enhancements](#HighAvailability)
-   [Replication Enhancements](#Repl)
-   [Tools Enhancements](#Tools)

## Columnstore indexes

This release offers improvements for columnstore indexes including updateable nonclustered columnstore indexes, columnstore indexes on in-memory tables, and many more new features for operational analytics.

- A read-only nonclustered columnstore index is updateable after upgrade.  A rebuild of the index is not required to make it updateable.

- There are performance improvements for analytics queries on columnstore indexes,  especially for aggregates and string predicates.

- DMVs and XEvents have supportability improvements.

For more details, see these topics in the [Columnstore Indexes Guide](../relational-databases/indexes/columnstore-indexes-overview.md) section of Books Online:

- [Columnstore Indexes Versioned Feature Summary](~/relational-databases/indexes/columnstore-indexes-what-s-new.md) - includes what's new.

- [Columnstore Indexes Data Loading](../relational-databases/indexes/columnstore-indexes-data-loading-guidance.md)

- [Columnstore Indexes Query Performance](~/relational-databases/indexes/columnstore-indexes-query-performance.md)

- [Get started with Columnstore for real time operational analytics](../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)

- [Columnstore Indexes for Data Warehousing](~/relational-databases/indexes/columnstore-indexes-data-warehouse.md)

- [Columnstore Indexes Defragmentation](~/relational-databases/indexes/columnstore-indexes-defragmentation.md)


## Database scoped configurations


The new [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) statement gives you control of certain configurations for your particular database. The configuration settings affect application behavior.

The new statement is available in both [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] and [!INCLUDE[sqldbesa](../includes/sqldbesa-md.md)].



## In-Memory OLTP


### Storage format change

The storage format for memory-optimized tables is changed between SQL Server 2014 and 2016. For upgrade and attach/restore from SQL Server 2014, the new storage format is serialized and the database is restarted once during database recovery.

- [Upgrade to SQL Server 2016](../database-engine/install-windows/upgrade-sql-server.md)


### ALTER TABLE is log-optimized, and runs in parallel

Now when you execute an ALTER TABLE statement on a memory-optimized table, only the metadata changes are written to the log. This greatly reduces log IO. Also, most ALTER TABLE scenarios now run in parallel, which can greatly shorten the duration of the statement.

- For non-parallel exceptions, including LOBs, see [Altering Memory-Optimized Tables](../relational-databases/in-memory-oltp/altering-memory-optimized-tables.md).



### Statistics

[Statistics for memory-optimized](../relational-databases/in-memory-oltp/statistics-for-memory-optimized-tables.md) tables are now updated automatically. In addition, sampling is now a supported method to collect statistics, allowing you to avoid the more expensive fullscan method.


### Parallel and heap scan for memory-optimized tables

Memory-optimized tables, and indexes on memory-optimized tables, now support parallel scan. This improves the performance of analytical queries.

In addition, heap scan is supported, and can be performed in parallel. In the case of a memory-optimized table, a heap scan refers to scanning all the rows in a table using the in-memory heap data structure used for storing the rows. For a full table scan, heap scan is more efficient than using an index.

### Transact-SQL Improvements for memory-optimized tables

There are several Transact-SQL elements that were not supported for memory-optimized tables in SQL Server 2014, which are now supported in SQL Server 2016:


- UNIQUE constraints and indexes are supported.


- FOREIGN KEY references between memory-optimized tables are supported.
  - These foreign keys can reference only a primary key, and cannot reference a unique key.


- CHECK constraints are supported.

- A non-unique index can allow NULL values in its key.

- TRIGGERs are supported on memory-optimized tables.
  - Only AFTER triggers are supported. INSTEADOF triggers are not supported.
  - Any trigger on a memory-optimized table must use WITH NATIVE_COMPILATION.

- Full support for all SQL Server code pages and collations with indexes and other artifacts in memory-optimized tables and natively compiled T-SQL modules.


- Support for [Altering Memory-Optimized Tables](../relational-databases/in-memory-oltp/altering-memory-optimized-tables.md):
  - ADD and DROP indexes. Change bucket_count of hash indexes.
  - Make schema changes: add/drop/alter columns; add/drop constraint.

- A memory-optimized table can now have several columns whose combined lengths are longer than the length of the 8060 byte page. An example is a table that has three columns of type `nvarchar(4000)`. In such examples, some columns are now stored off-row. Your queries are blissfully unaware of whether a column is on-row or off-row.

- [LOB (large object) types](../relational-databases/in-memory-oltp/supported-data-types-for-in-memory-oltp.md) `varbinary(max)`, `nvarchar(max)`, and `varchar(max)` are now supported in memory-optimized tables.


For overall information, see:

- [Transact-SQL Constructs Not Supported by In-Memory OLTP](../relational-databases/in-memory-oltp/transact-sql-constructs-not-supported-by-in-memory-oltp.md)
- [Unsupported SQL Server Features for In-Memory OLTP](~/relational-databases/in-memory-oltp/unsupported-sql-server-features-for-in-memory-oltp.md)



### Performance and scaling improvements

- There is no longer any limitation on data size. See [Estimate Memory Requirements for Memory-Optimized Tables](~/relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md).

- There are now multiple concurrent threads responsible to [persist to disk the changes to memory-optimized tables](../relational-databases/in-memory-oltp/scalability.md).

- Parallel plan support for [Accessing Memory-Optimized Tables Using Interpreted Transact-SQL](../relational-databases/in-memory-oltp/accessing-memory-optimized-tables-using-interpreted-transact-sql.md).


### Enhancements in SQL Server Management Studio

- The [Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](../relational-databases/in-memory-oltp/determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md) no longer requires the configuration of data collectors or management data warehouse. The report can now run directly on a production database.

- [PowerShell Cmdlet for Migration Evaluation](../relational-databases/in-memory-oltp/powershell-cmdlet-for-migration-evaluation.md) for evaluating the migration fitness of multiple objects in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database.

- Generate migration checklists by right-clicking on a database, and selecting Tasks > Generate In-Memory OLTP migration checklists.

### Cross-feature support

- Support for using temporal system-versioning with In-Memory OLTP. For more information, see [System-Versioned Temporal Tables with Memory-Optimized Tables](../relational-databases/tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)

- Query store support for natively compiled code from In-Memory OLTP workloads. For more information, see [Using the Query Store with In-Memory OLTP](../relational-databases/performance/using-the-query-store-with-in-memory-oltp.md).

- [Row-Level Security in Memory-Optimized Tables](../relational-databases/in-memory-oltp/introduction-to-memory-optimized-tables.md#rls)

- [Using Multiple Active Result Sets &#40;MARS&#41;](../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md) connections  can now access memory-optimized tables and natively compiled stored procedures.

- [Transparent Data Encryption (TDE)](../relational-databases/security/encryption/transparent-data-encryption.md) support. If a database is configured for ENCRYPTION, files in the[The Memory Optimized Filegroup](../relational-databases/in-memory-oltp/the-memory-optimized-filegroup.md) are now also encrypted.

For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).


## Query Optimizer
### Compatibility Level Guarantees
When you upgrade your database to SQL Server 2016, there will be no plan changes seen if you remain at the older compatibility levels that you were using (for example, 120 or 110). New features and improvements related to query optimizer, will be available only under latest compatibility level. 
### Trace Flag 4199
In general, you do not need to use trace flag 4199 in SQL Server 2016 since most of the query optimizer behaviors controlled by this trace flag are enabled unconditionally under the latest compatibility level (130) in SQL Server 2016.
### New referential integrity operator
A table can reference a maximum of 253 other tables and columns as foreign keys (outgoing references). [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] increases the limit for the number of other table and columns that can reference columns in a single table (incoming references), from 253 to 10,000. For restrictions, see [Create Foreign Key Relationships](../relational-databases/tables/create-foreign-key-relationships.md). A new referential integrity operator is introduced (under compatibility level 130), which performs the referential integrity checks in place. This improves overall performance for UPDATE and DELETE operations, on tables that have a large number of incoming references, thereby making it feasible to have large number of incoming references. For more information, see [Query Optimizer Additions in SQL Server 2016](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/05/23/query-optimizer-additions-in-sql-server/)
### Parallel update of sampled statistics
Data sampling to build statistics is now done in parallel (under compatibility level 130), to improve the performance of statistics collection. For more information, see [Update Statistics](../t-sql/statements/update-statistics-transact-sql.md).
### Sublinear threshold for update of statistics
Automatic update of statistics is now more aggressive on large tables (under compatibility level 130). The threshold to trigger auto-update of statistics is 20%, starting SQL Server 2016, for larger tables, this threshold will start decreasing (still a percentage) as the number of rows increase in the table. You will no longer need to set trace flag 2371 to reduce the threshold. 
### Other enhancements
The Insert in an Insert-select statement is multi-threaded or can have a parallel plan (under compatibility level 130). To get a parallel plan, INSERT ... SELECT statement must use the TABLOCK hint. For more information, see [Parallel Insert Select](https://blogs.msdn.microsoft.com/sqlcat/2016/07/06/sqlsweet16-episode-3-parallel-insert-select/)

## Live Query statistics
 [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] provides the ability to view the live execution plan of an active query. This live query plan provides real-time insights into the query execution process as the controls flow from one query plan operator to another. For more information, see [Live Query Statistics](../relational-databases/performance/live-query-statistics.md).

## Query Store
Query store is a new feature that provides DBAs with insight on query plan choice and performance. It simplifies performance troubleshooting by enabling you to quickly find performance differences caused by changes in query plans. The feature automatically captures a history of queries, plans, and runtime statistics, and retains these for your review. It separates data by time windows, allowing you to see database usage patterns and understand when query plan changes happened on the server. The query store presents information by using a [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] dialog box, and lets you force the query to one of the selected query plans. For more information, see [Monitoring Performance By Using the Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).


## Temporal tables
[!INCLUDE[ssSQL15](../includes/sssql15-md.md)] now supports system-versioned temporal tables. A temporal table is a new type of table that provides correct information about stored facts at any point in time. Each temporal table consists of two tables actually, one for the current data and one for the historical data. The system ensures that when the data changes in the table with the current data the previous values are stored in the historical table. Querying constructs are provided to hide this complexity from users. For more information, see [Temporal Tables](../relational-databases/tables/temporal-tables.md).

## Backups

### Striped backups to Microsoft Azure Blob Storage
In [!INCLUDE[ssSQL15](../includes/sssql15-md.md)], SQL Server backup to URL using the Microsoft Azure Blob storage service now supports striped backups sets using block blobs to support a maximum backup size of 12.8 TB. For examples, see [Code Examples](../relational-databases/backup-restore/sql-server-backup-to-url.md#Examples).

### File-Snapshot backups to Microsoft Azure Blob Storage
 In [!INCLUDE[ssSQL15](../includes/sssql15-md.md)], SQL Server backup to URL now supports using Azure snapshots to backup databases in which all database files are stored using the Microsoft Azure Blob storage service. For more information, see [File-Snapshot Backups for Database Files in Azure](../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md).

### Managed backup
In [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] SQL Server Managed Backup to Microsoft Azure uses the new block blob storage for backup files. There are also several changes and enhancements to Managed Backup.

-   Support for both automated and custom scheduling of backups.

-   Support backups for system databases.

-   Support for databases that are using the Simple recovery model.

 For more information, see [SQL Server Managed Backup to Microsoft Azure](../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)

> [!NOTE]
>  For [!INCLUDE[ssSQL15](../includes/sssql15-md.md)], these new managed backup features do not yet have corresponding UI support in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].

## TempDB database
 There are several enhancements to TempDB:

-   Trace Flags 1117 and 1118 are not required for tempdb anymore. If there are multiple tempdb database files all files will grow at the same time depending on growth settings. In addition, all allocations in tempdb will use uniform extents.

-   By default, setup adds as many tempdb files as the CPU count or 8, whichever is lower.

-   During setup, you can configure the number of tempdb database files, initial size, autogrowth and directory placement using the new UI input control on the Database Engine Configuration - TempDB section of SQL Server Installation Wizard.

-   The default initial size is 8MB and the default autogrowth is 64MB.

-   You can specify multiple volumes for tempdb database files. If multiple directories are specified tempdb data files will be spread across the directories in a round-robin fashion.

## Built-in JSON support
SQL Server 2016 adds built-in support for importing and exporting JSON and working with JSON strings. This built-in support includes the following statements and functions.

-   Format query results as JSON, or export JSON, by adding the **FOR JSON** clause to a **SELECT** statement. Use the **FOR JSON** clause, for example, to delegate the formatting of JSON output from your client applications to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more info, see [Format Query Results as JSON with FOR JSON &#40;SQL Server&#41;](../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md).

-   Convert JSON data to rows and columns, or import JSON, by calling the **OPENJSON** rowset provider function. Use **OPENJSON** to import JSON data into SQL Server, or convert JSON data to rows and columns for an app or service that can't currently consume JSON directly. For more info, see [Convert JSON Data to Rows and Columns with OPENJSON &#40;SQL Server&#41;](../relational-databases/json/convert-json-data-to-rows-and-columns-with-openjson-sql-server.md).

-   The **ISJSON** function tests whether a string contains valid JSON. For more info, see [ISJSON &#40;Transact-SQL&#41;](../t-sql/functions/isjson-transact-sql.md)

-   The **JSON_VALUE** function extracts a scalar value from a JSON string.For more info, see [JSON_VALUE &#40;Transact-SQL&#41;](../t-sql/functions/json-value-transact-sql.md).

-   The **JSON_QUERY** function extracts an object or an array from a JSON string. For more info, see [JSON_QUERY &#40;Transact-SQL&#41;](../t-sql/functions/json-query-transact-sql.md).

-   The **JSON_MODIFY** function updates the value of a property in a JSON string and return the updated JSON string. For more info, see [JSON_MODIFY &#40;Transact-SQL&#41;](../t-sql/functions/json-modify-transact-sql.md).

## PolyBase
 PolyBase allows you to use T-SQL statements to access data stored in Hadoop or Azure Blob Storage and query it in an adhoc fashion. It also lets you query semi-structured data and join the results with relational data sets stored in SQL Server. PolyBase is optimized for data warehousing workloads and intended for analytical query scenarios.

 For more information, see [PolyBase Guide](../relational-databases/polybase/polybase-guide.md).

## Stretch database
 Stretch Database is a new feature in [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] that migrates your historical data transparently and securely to the Microsoft Azure cloud. You can access your SQL Server data seamlessly regardless of whether it's on-premises or stretched to the cloud. You set the policy that determines where data is stored, and SQL Server handles the data movement in the background. The entire table is always online and queryable. And, Stretch Database doesn't require any changes to existing queries or applications - the location of the data is completely transparent to the application. For more info, see [Stretch Database](../sql-server/stretch-database/stretch-database.md).
 
## Support for UTF-8
[bcp Utility](../tools/bcp-utility.md), [BULK INSERT](../t-sql/statements/bulk-insert-transact-sql.md), and [OPENROWSET](../t-sql/functions/openrowset-transact-sql.md) now support the UTF-8 code page. For more information, see those topics and [Create a Format File &#40;SQL Server&#41;](../relational-databases/import-export/create-a-format-file-sql-server.md).

## New default database Size and autogrow Values
New values for the model database and default values for new databases (which are based on model). The initial size of the data and log files is now 8 MB. The default auto-growth of data and log files is now 64MB.


## Transact-SQL enhancements
Numerous enhancements support the features described in the other sections of this topic. The following additional enhancements are available.
- The TRUNCATE TABLE statement now permits the truncation of specified partitions. For more information, see [TRUNCATE TABLE &#40;Transact-SQL&#41;](../t-sql/statements/truncate-table-transact-sql.md).
- [ALTER TABLE &#40;Transact-SQL&#41;](../t-sql/statements/alter-table-transact-sql.md) now allows many alter column actions to be performed while the table remains available.
- The full-text index DMV [sys.dm_fts_index_keywords_position_by_document &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-position-by-document-transact-sql.md) returns the location of keywords in documents. This DMV has also been added in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] SP2 and [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] SP1.
- A new query hint **NO_PERFORMANCE_SPOOL** can prevent a spool operator from being added to query plans. This can improve performance when many concurrent queries are running with spool operations. For more information, see [Query Hints &#40;Transact-SQL&#41;](../t-sql/queries/hints-transact-sql-query.md).
- The [FORMATMESSAGE &#40;Transact-SQL&#41;](../t-sql/functions/formatmessage-transact-sql.md) statement is enhances to accept a msg_string argument.
- The maximum index key size for NONCLUSTERED indexes has been increased to 1700 bytes.
- New DROP IF syntax is added for drop statements related to AGGREGATE, ASSEMBLY, COLUMN, CONSTRAINT, DATABASE, DEFAULT, FUNCTION, INDEX, PROCEDURE, ROLE, RULE, SCHEMA, SECURITY POLICY, SEQUENCE, SYNONYM, TABLE, TRIGGER, TYPE, USER, and VIEW. See individual syntax topics for syntax.
- A MAXDOP option has been added to [DBCC CHECKTABLE &#40;Transact-SQL&#41;](../t-sql/database-console-commands/dbcc-checktable-transact-sql.md), [DBCC CHECKDB &#40;Transact-SQL&#41;](../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md), and [DBCC CHECKFILEGROUP &#40;Transact-SQL&#41;](../t-sql/database-console-commands/dbcc-checkfilegroup-transact-sql.md) to specify the degree of parallelism.
- SESSION_CONTEXT can now be set. Includes the [SESSION_CONTEXT &#40;Transact-SQL&#41;](../t-sql/functions/session-context-transact-sql.md) function, [CURRENT_TRANSACTION_ID &#40;Transact-SQL&#41;](../t-sql/functions/current-transaction-id-transact-sql.md) function, and the [sp_set_session_context &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md) procedure.
- Advanced Analytics Extensions allow users to execute scripts written in a supported language such as R. [!INCLUDE[tsql](../includes/tsql-md.md)] supports R by introducing the [sp_execute_external_script &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) stored procedure, and the [external scripts enabled Server Configuration Option](../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md). For more information, see [SQL Server R Services](../advanced-analytics/r-services/sql-server-r-services.md).
- Also to support R, the ability to create an external resource pool. For more information, see [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../t-sql/statements/create-external-resource-pool-transact-sql.md).  New catalog views and DMVs ([sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md) and [sys.dm_resource_governor_external_resource_pool_affinity &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)). Additional arguments are available for [sp_execute_external_script &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) and [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../t-sql/statements/create-workload-group-transact-sql.md). Additional columns are added to some of the existing resource governor catalog views and DMVs.
- The [CREATE USER](../t-sql/statements/create-user-transact-sql.md) syntax is enhanced with the ALLOW_ENCRYPTED_VALUE_MODIFICATIONS option to support the Always Encrypted feature. For more information see [Migrate Sensitive Data Protected by Always Encrypted](../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md).
- The [COMPRESS &#40;Transact-SQL&#41;](../t-sql/functions/compress-transact-sql.md) and [DECOMPRESS &#40;Transact-SQL&#41;](../t-sql/functions/decompress-transact-sql.md) functions convert values into and out of the GZIP algorithm.
- The  [DATEDIFF_BIG &#40;Transact-SQL&#41;](../t-sql/functions/datediff-big-transact-sql.md) and [AT TIME ZONE &#40;Transact-SQL&#41;](../t-sql/queries/at-time-zone-transact-sql.md) functions and the [sys.time_zone_info &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/sys-time-zone-info-transact-sql.md) view are added to support date and time interactions.
- A credential can now be created at the database level (in addition to the server level credential that was previously available). For more information, see [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../t-sql/statements/create-database-scoped-credential-transact-sql.md).
- Eight new properties are added to [SERVERPROPERTY &#40;Transact-SQL&#41;](../t-sql/functions/serverproperty-transact-sql.md): InstanceDefaultDataPath, InstanceDefaultLogPath, ProductBuild, ProductBuildType, ProductMajorVersion, ProductMinorVersion, ProductUpdateLevel, and ProductUpdateReference.
- The input length limit of 8,000 bytes for the [HASHBYTES &#40;Transact-SQL&#41;](../t-sql/functions/hashbytes-transact-sql.md) function is removed.
- New string functions [STRING_SPLIT &#40;Transact-SQL&#41;](../t-sql/functions/string-split-transact-sql.md) and [STRING_ESCAPE &#40;Transact-SQL&#41;](../t-sql/functions/string-escape-transact-sql.md) are added.
- Autogrow options: Trace flag 1117 is replaced by the AUTOGROW_SINGLE_FILE and AUTOGROW_ALL_FILES option of ALTER DATABASE, and trace flag 1117 has no affect. For more information, see [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md) and the new is_autogrow_all_files column of [sys.filegroups &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md).
- Allocation of mixed extents: For user databases, default allocation for the first 8 pages of an object will change from using mixed page extents to using uniform extents. Trace flag 1118 is replaced with the SET MIXED_PAGE_ALLOCATION option of ALTER DATABASE, and trace flag 1118 has no affect. For more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-set-options.md), and the new `is_mixed_page_allocation_on` column of [sys.databases &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/sys-databases-transact-sql.md).

### Transact-SQL improvements for natively compiled modules

There are some Transact-SQL elements that were not supported for natively compiled modules in SQL Server 2014, which are now supported in SQL Server 2016:


- Query constructs:
  - UNION and UNION ALL
  - SELECT DISTINCT
  - OUTER JOIN
  - Subqueries in SELECT


- INSERT, UPDATE and DELETE statements can now include the [OUTPUT clause](../t-sql/queries/output-clause-transact-sql.md).

- LOBs can now be used in the following ways in a native proc:
  - Declaration of variables.
  - Input parameters received.
  - Parameters passed into string functions, such as into LTrim or Substring, in a native proc.


- Inline (meaning single statement) table-valued functions (TVFs) can now be natively compiled.

- Scalar user-defined functions (UDFs) can now be natively compiled.

- Increased support for a native proc to call:
  - Built-in [security functions](../t-sql/functions/security-functions-transact-sql.md).
  - Built-in [math functions](../t-sql/functions/mathematical-functions-transact-sql.md).
  - Built-in function `@@SPID`.


- EXECUTE AS CALLER is now support, which means the EXECUTE AS clause is no longer required when creating a natively compiled T-SQL module.


For overall information, see:

- [Supported Features for Natively Compiled T-SQL Modules](../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md)
- [Altering Natively Compiled T-SQL Modules](../relational-databases/in-memory-oltp/altering-natively-compiled-t-sql-modules.md)

## System View enhancements
- Two new views support row level security. For more information, see [sys.security_predicates &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/sys-security-predicates-transact-sql.md) and [sys.security_policies &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/sys-security-policies-transact-sql.md).
- Seven new views support the Query Store feature. For more information, see [Query Store Catalog Views &#40;Transact-SQL&#41;](../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md).
- 24 new columns are added to [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md) provide information about memory grants.
- Two new query hints (MIN_GRANT_PERCENT and MAX_GRANT_PERCENT) are added to specify memory grants. See [Query Hints &#40;Transact-SQL&#41;](../t-sql/queries/hints-transact-sql-query.md).
- [sys.dm_exec_session_wait_stats &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-exec-session-wait-stats-transact-sql.md) provides a per session report similar to the server wide [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md).
- [sys.dm_exec_function_stats &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-exec-function-stats-transact-sql.md) provides execution statistics regarding scalar valued functions.
- Beginning with [!INCLUDE[ssSQL15](../includes/sssql15-md.md)], entries in [sys.dm_db_index_usage_stats &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-db-index-usage-stats-transact-sql.md) are retained as they were prior to [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)].
- Information about statements submitted to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can be returned by the new dynamic management function [sys.dm_exec_input_buffer &#40;Transact-SQL&#41;](../relational-databases/system-dynamic-management-views/sys-dm-exec-input-buffer-transact-sql.md).
- Two new views support [SQL Server R Services](../advanced-analytics/r-services/sql-server-r-services.md): [sys.dm_external_script_requests](../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md) and [sys.dm_external_script_execution_stats](../relational-databases/system-dynamic-management-views/sys-dm-external-script-execution-stats.md). 


## Security enhancements

### Row-Level security
Row-level security introduces predicate based access control. It features a flexible, centralized, predicate-based evaluation that can take into consideration metadata (such as labels) or any other criteria the administrator determines as appropriate. The predicate is used as a criterion to determine whether or not the user has the appropriate access to the data based on user attributes. Label based access control can be implemented by using predicate based access control. For more information, see [Row-Level Security](../relational-databases/security/row-level-security.md).


### Always Encrypted
With Always Encrypted, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can perform operations on encrypted data, and best of all the encryption key resides with the application inside the customer's trusted environment and not on the server. Always Encrypted secures customer data so DBAs do not have access to plain text data. Encryption and decryption of data happens transparently at the driver level minimizing changes that have to be made to existing applications. For more information, see [Always Encrypted &#40;Database Engine&#41;](../relational-databases/security/encryption/always-encrypted-database-engine.md).


### Dynamic Data Masking
Dynamic data masking limits sensitive data exposure by masking it to non-privileged users. Dynamic data masking helps prevent unauthorized access to sensitive data by enabling customers to designate how much of the sensitive data to reveal with minimal impact on the application layer. It's a policy-based security feature that hides the sensitive data in the result set of a query over designated database fields, while the data in the database is not changed. For more information, see [Dynamic Data Masking](../relational-databases/security/dynamic-data-masking.md).


### New permissions
- The **ALTER ANY SECURITY POLICY** permission is available as part of the implementation of row level security.
- The **ALTER ANY MASK** and **UNMASK** permissions are available as part of the implementation of dynamic data masking.
- The **ALTER ANY COLUMN ENCRYPTION KEY**, **VIEW ANY COLUMN ENCRYPTION KEY**, **ALTER ANY COLUMN MASTER KEY DEFINITION**, and **VIEW ANY COLUMN MASTER KEY DEFINITION** permissions are available as part of the implementation of the Always Encrypted feature.
- The **ALTER ANY EXTERNAL DATA SOURCE** and **ALTER ANY EXTERNAL FILE FORMAT** permissions are visible in [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] but only apply to the [!INCLUDE[ssAPS](../includes/ssaps-md.md)] ([!INCLUDE[ssDW](../includes/ssdw-md.md)]).
- The **EXECUTE ANY EXTERNAL SCRIPT** permissions are available as part of the support for R scripts.
 - The **ALTER ANY DATABASE SCOPED CONFIGURATION** permissions is available to authorize the use of the [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) statement.

### Transparent Data Encryption
- Transparent Data Encryption has been enhanced with support for Intel AES-NI hardware acceleration of encryption. This will reduce the CPU overhead of turning on Transparent Data Encryption.

### AES encryption for endpoints
- The default encryption for endpoints is changed from RC4 to AES.

### New credential type
- A credential can now be created at the database level (in addition to the server level credential that was previously available). For more information, see [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../t-sql/statements/create-database-scoped-credential-transact-sql.md).


## High Availability enhancements
SQL Server 2016 Standard Edition now supports Always On Basic Availability Groups. Basic availability groups provide support for a primary and secondary replica. This capability replaces the obsolete Database Mirroring technology for high availability. For more information about the differences between basic and advanced availability groups, see [Basic Availability Groups &#40;Always On Availability Groups&#41;](../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md).

Load-balancing of read-intent connection requests is now supported across a set of read-only replicas. The previous behavior always directed connections to the first available read-only replica in the routing list. For more information, see [Configure load-balancing across read-only replicas](../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md#loadbalancing).

 The number of replicas that support automatic failover has been increased from two to three.

 Group Managed Service Accounts are now supported for Always On Failover Clusters. For more information, see [Group Managed Service Accounts](https://technet.microsoft.com/library/hh831782.aspx). For Windows Server 2012 R2, an update is required to avoid temporary downtime after a password change. To obtain the update, see [gMSA-based services can't log on after a password change in a Windows Server 2012 R2 domain](https://support.microsoft.com/kb/2998082/).

 [!INCLUDE[ssHADR](../includes/sshadr-md.md)] supports distributed transactions and the DTC on Windows Server 2016. For more information, see [Support for distributed transactions](../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md#dtcsupport).

 You can now configure [!INCLUDE[ssHADR](../includes/sshadr-md.md)] to failover when a database goes offline. This change requires the setting the **DB_FAILOVER** option to **ON** in the [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../t-sql/statements/create-availability-group-transact-sql.md) or [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../t-sql/statements/alter-availability-group-transact-sql.md) statements.

Always On now supports encrypted databases. The Availability Group wizards now prompt you for a password for any databases that contain a database master key when you create a new Availability Group or when you add databases or add replicas to an existing Availability Group.

Two availability groups in two separate Windows Server Failover Clusters (WSFC) can now be combined into a Distributed Availability Group. For more information, see [Distributed Availability Groups &#40;Always On Availability Groups&#41;](../database-engine/availability-groups/windows/distributed-availability-groups-always-on-availability-groups.md).

Direct seeding allows a secondary replica to be automatically seeded over the network (rather than manual seeding that requires a physical backup of the target database to be restored on the secondary). Direct seeding is specified by setting **SEEDING_MODE=AUTOMATIC** in the [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../t-sql/statements/create-availability-group-transact-sql.md) or [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../t-sql/statements/alter-availability-group-transact-sql.md) statements. You must also specify **GRANT CREATE ANY DATABASE** with [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../t-sql/statements/alter-availability-group-transact-sql.md) on each secondary replica that is used with direct seeding.

**Performance improvements** - The synchronization throughput of availability groups has been increased ~10x through parallel and faster compression of log blocks on the primary replica, an optimized synchronization protocol, and parallel decompression and redo of log records on the secondary replica. This increases the freshness of readable secondaries and reduces database recovery time in case of failover. Note that redo for memory-optimized tables is not yet parallel in SQL Server 2016.

## Replication enhancements
- Replication of memory-optimized tables are now supported. For more information, see [Replication to Memory-Optimized Table Subscribers](../relational-databases/replication/replication-to-memory-optimized-table-subscribers.md).
- Replication is now supported to [!INCLUDE[ssSDSFull](../includes/sssdsfull-md.md)]. For more information, see [Replication to SQL Database](../relational-databases/replication/replication-to-sql-database.md).

## Tools enhancements

### Management Studio
Download the latest [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)

- [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] supports the Active Directory Authentication Library (ADAL) which is under development for connecting to Microsoft Azure. This replaces the certificate-based authentication used in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)][!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].
- A new query result grid option supports keeping Carriage Return/Line Feed (newline characters) when copying or saving text from the results grid. Set this from the Tools/Options menu.
- SQL Server Management Tools is no longer installed from the main feature tree; for details see [Install SQL Server Management Tools with SSMS](https://msdn.microsoft.com/library/af68d59a-a04d-4f23-9967-ad4ee2e63381).

### Upgrade Advisor
SQL Server 2016 Upgrade Advisor Preview is a standalone tool that enables users of prior versions to run a set of upgrade rules against their SQL Server database to pinpoint breaking and behavior changes and deprecated features as well as providing help with the adoption of new features such as Stretch Database.

 You can download Upgrade Advisor Preview [here](https://www.microsoft.com/download/details.aspx?id=48119) or you can install it by using the Web Platform Installer.

## See Also
[What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)
 
[SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md) 
 
[Install SQL Server Management Tools with SSMS](https://msdn.microsoft.com/library/af68d59a-a04d-4f23-9967-ad4ee2e63381)
