---
title: "ALTER DATABASE Compatibility Level (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/08/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "COMPATIBILITY_LEVEL_TSQL"
  - "COMPATIBILITY_LEVEL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "80 compatibility level"
  - "ALTER DATABASE statement, compatibility levels"
  - "90 compatibility level"
  - "compatibility levels [SQL Server]"
  - "100 compatibility level"
  - "db compatibility level"
  - "db compat level"
ms.assetid: ca5fd220-d5ea-4182-8950-55d4101a86f6
author: CarlRabeler
ms.author: carlrab
manager: craigg'
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER DATABASE (Transact-SQL) Compatibility Level
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Sets certain database behaviors to be compatible with the specified version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For other ALTER DATABASE options, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).
  
## Syntax  
  
```  
ALTER DATABASE database_name   
SET COMPATIBILITY_LEVEL = { 150 | 140 | 130 | 120 | 110 | 100 | 90 }  
```  
  
## Arguments  
 *database_name*  
 Is the name of the database to be modified.  
  
 COMPATIBILITY_LEVEL { 150 | 140 | 130 | 120 | 110 | 100 | 90 | 80 }  
 Is the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with which the database is to be made compatible. The following compatibility level values can be configured (not all versions supports all of the above listed compatibility level):  
  
|Product|Database Engine Version|Compatibility Level Designation|Supported Compatibility Level Values|  
|-------------|-----------------------------|-------------------------------------|------------------------------------------|  
|[!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]|15|150|150, 140, 130, 120, 110, 100| 
|[!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]|14|140|140, 130, 120, 110, 100|
|[!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] single database/elastic pool|12|130|150, 140, 130, 120, 110, 100|  
|[!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] managed instance|12|130|150, 140, 130, 120, 110, 100|  
|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|13|130|130, 120, 110, 100|  
|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|12|120|120, 110, 100|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|11|110|110, 100, 90|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|10.5|100|100, 90, 80|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|10|100|100, 90, 80|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|9|90|90, 80|  
|SQL Server 2000|8|80|80|  
  
> [!NOTE]  
> As of **January 2018**, in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the default compatibility level is 140 for newly created databases. We do not update database compatibility level for existing databases. This is up to customers to do at their own discretion. With that said, we highly recommend customers plan on moving to the latest compatibility level in order to leverage the latest improvements.
> 
> If you want to leverage database compatibility level 140 for your database overall, but you have reason to prefer the **cardinality estimation** model of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], mapping to database compatibility level 110, see [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md), and in particular its keyword `LEGACY_CARDINALITY_ESTIMATION = ON`.
>  
> For details about how to assess the performance differences of your most important queries, between two compatibility levels on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], see [Improved Query Performance with Compatibility Level 130 in Azure SQL Database](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/05/06/improved-query-performance-with-compatibility-level-130-in-azure-sql-database/). Note that this article refers to compatibility level 130 and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but the same methodology applies for moves to 140 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] .

Execute the following query to determine the version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that you are connected to.  
  
```sql  
SELECT SERVERPROPERTY('ProductVersion');  
```  
  
> [!NOTE]  
> Not all features that vary by compatibility level are supported on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  

 To determine the current compatibility level, query the **compatibility_level** column of [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).  
  
```sql  
SELECT name, compatibility_level FROM sys.databases;  
```  
  
## Remarks  
For all installations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default compatibility level is set to the version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Databases are set to this level unless the **model** database has a lower compatibility level. When a database is upgraded from any earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the database retains its existing compatibility level, if it is at least minimum allowed for that instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Upgrading a database with a compatibility level lower than the allowed level, automatically sets the database to the lowest compatibility level allowed. This applies to both system and user databases.   

The below behaviors are expected for [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] when a database is attached or restored, and after an in-place upgrade: 
- If the compatibility level of a user database was 100 or higher before the upgrade, it remains the same after upgrade.    
- If the compatibility level of a user database was 90 before upgrade, in the upgraded database, the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].    
- The compatibility levels of the tempdb, model, msdb and Resource databases are set to the current compatibility level after upgrade.  
- The master system database retains the compatibility level it had before upgrade.

Use `ALTER DATABASE` to change the compatibility level of the database. The new compatibility level setting for a database takes effect when a `USE <database>` command is issued, or a new login is processed with that database as the default database context.     
To view the current compatibility level of a database, query the **compatibility_level** column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  

> [!NOTE]  
> A [distribution database](../../relational-databases/replication/distribution-database.md) that was created in an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is upgraded to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] RTM or Service Pack 1 has a compatibility level of 90, which is not supported for other databases. This does not have an impact on the functionality of replication. Upgrading to later service packs and versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will result in the compatibility level of the distribution database to be increased to match that of the **master** database.

## Compatibility Levels and SQL Server Upgrades  
Database compatibility level is a valuable tool to assist in database modernization, by allowing the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to be upgraded, while keeping connecting applications functional status by maintaining the same pre-upgrade database compatibility level. 
As long as the application does not need to leverage enhancements that are only available in a higher database compatibility level, it is a valid approach to upgrade the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and maintain the previous database compatibility level. For more information on using compatibility level for backward compatibility, see the [Using Compatibility Level for Backward Compatibility](#using-compatibility-level-for-backward-compatibility) later in this article.    

For new development work, or when an existing application requires use of new features, as well as performance improvements done in the query optimizer space, plan to upgrade the database compatibility level to the latest available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and certify your application to work with that compatibility level. For more details on upgrading the database compatibility level, see the [Best Practices for upgrading Database Compatibility Level](#best-practices-for-upgrading-database-compatibility-level) later in the article.     

> [!TIP] 
> If an application was tested and certified on a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version, then it was implicitly tested and certified on that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version native database compatibility level.
> 
> So, database compatibility level provides an easy certification path for an existing application, when using the database compatibility level corresponding to the tested [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version.
>
> For more information about differences between compatibility levels, see the appropriate sections later in this article. 

To upgrade the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to the latest version, while maintaining the database compatibility level that existed before the upgrade and its supportability status, it is recommended to perform static functional surface area validation of the application code in the database, by using the [Microsoft Data Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595) tool (DMA). The absence of errors in the DMA tool output, about missing or incompatible functionality, protects application from any functional regressions on the new target version. For more information on the DMA tool, see [here](https://blogs.msdn.microsoft.com/datamigration/dma).

> [!NOTE] 
> DMA supports database compatibility level 100 and above. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] as source version is excluded. 

> [!IMPORTANT] 
> Microsoft recommends that some minimal testing is done to validate the success of an upgrade, while maintaining the previous database compatibility level. You should determine what minimal testing means for your own application and scenario. 

> [!NOTE] 
> Microsoft provides query plan shape protection when:
> - The new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version (target) runs on hardware that is comparable to the hardware where the previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version (source) was running. 
> - The same [supported database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#remarks) is used both at the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and source [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 
> 
> Any query plan shape regression (as compared to the source [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]) that occurs in the above conditions will be addressed. Please contact Microsoft Customer Support if this is the case.

## Using Compatibility Level for Backward Compatibility  
The *database compatibility level* setting affects behaviors only for the specified database, not for the entire server. Database compatibility level provides only partial backward compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].   

> [!TIP]
> Because *database compatibility level* is a database-level setting, an application running on a newer [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] while using an older database compatibility level, can still leverage server-level enhancements without any requirement for application changes.
>
> These include rich monitoring and troubleshooting improvements, with new [System Dynamic Management Views](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md) and [Extended Events](../../relational-databases/extended-events/extended-events.md). And also improved scalability, for example with [Automatic Soft-NUMA
](../../database-engine/configure-windows/soft-numa-sql-server.md#automatic-soft-numa).

Starting with compatibility mode 130, any new query plan affecting features have been intentionally added only to the new compatibility level. This has been done in order to minimize the risk during upgrades that arise from performance degradation due to query plan changes.   
From an application perspective, the goal should still be to upgrade to the latest compatibility level at some point in time, in order to inherit some of the new features, as well as performance improvements done in the query optimizer space, but to do so in a controlled way. Use the lower compatibility level as a safer migration aid to work around version differences, in the behaviors that are controlled by the relevant compatibility level setting. 
For more details, including the recommended workflow for upgrading database compatibility level, see the [Best Practices for upgrading Database Compatibility Level](#best-practices-for-upgrading-database-compatibility-level) later in the article.  
  
> [!IMPORTANT]
> Discontinued functionality introduced in a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version is not protected by compatibility level. This refers to functionality that was removed from the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].
> 
> For example, the `FASTFIRSTROW` hint was discontinued in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and replaced with the `OPTION (FAST n )` hint. Setting the database compatibility level to 110 will not restore the discontinued hint.
> For more information on discontinued functionality, see [Discontinued Database Engine Functionality in SQL Server 2016](../../database-engine/discontinued-database-engine-functionality-in-sql-server-2016.md), [Discontinued Database Engine Functionality in SQL Server 2014](../../database-engine/discontinued-database-engine-functionality-in-sql-server-2016.md)), [Discontinued Database Engine Functionality in SQL Server 2012](../../database-engine/discontinued-database-engine-functionality-in-sql-server-2016.md)), and [Discontinued Database Engine Functionality in SQL Server 2008](../../database-engine/discontinued-database-engine-functionality-in-sql-server-2016.md)).

> [!IMPORTANT]
> Breaking changes introduced in a given [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version **may** not be protected by compatibility level. This refers to behavior changes between versions of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. [!INCLUDE[tsql](../../includes/tsql-md.md)] behavior is usually protected by compatibility level. However, changed or removed system objects are **not** protected by compatibility level.
>
> An example of a breaking change **protected** by compatibility level is an implicit conversion from datetime to datetime2 data types. Under database compatibility level 130, these show improved accuracy by accounting for the fractional milliseconds, resulting in different converted values. To restore previous conversion behavior, set the database compatibility level to 120 or lower.
>
> Examples of breaking changes **not protected** by compatibility level are:
> -  Changed column names in system objects. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the column *single_pages_kb* in sys.dm_os_sys_info was renamed to *pages_kb*. Regardless of the compatibility level, the query `SELECT single_pages_kb FROM sys.dm_os_sys_info` will produce error 207 (Invalid column name).
> -  Removed system objects. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the `sp_dboption` was removed. Regardless of the compatibility level, the statement `EXEC sp_dboption 'AdventureWorks2016CTP3', 'autoshrink', 'FALSE';` will produce error 2812 (Could not find stored procedure 'sp_dboption').
>
> For more information on breaking changes, see [Breaking Changes to Database Engine Features in SQL Server 2017](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2017.md), [Breaking Changes to Database Engine Features in SQL Server 2016](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md), [Breaking Changes to Database Engine Features in SQL Server 2014](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md)), [Breaking Changes to Database Engine Features in SQL Server 2012](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md)), and [Breaking Changes to Database Engine Features in SQL Server 2008](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md)).
  
## Best Practices for upgrading Database Compatibility Level 
For the recommended workflow for upgrading the compatibility level, see [Change the Database Compatibility Mode and Use the Query Store](../../database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store.md).  

## Compatibility Levels and Stored Procedures  
 When a stored procedure executes, it uses the current compatibility level of the database in which it is defined. When the compatibility setting of a database is changed, all of its stored procedures are automatically recompiled accordingly.  

## Differences Between Compatibility Level 140 and Level 150  
This section describes new behaviors introduced with compatibility level 150.

Database compatibility level 150 is currently in Public Preview for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]. This database compatibility level will be associated with the next generation of query processing improvements beyond what was introduced in database compatibility level 140.     

For more information on query processing features enabled in database compatibility level 150, refer to [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-ver15.md) and [Intelligent query processing in SQL databases](https://docs.microsoft.com/sql/relational-databases/performance/intelligent-query-processing?view=sql-server-2017).

## Differences Between Compatibility Level 130 and Level 140  
This section describes new behaviors introduced with compatibility level 140.

|Compatibility-level setting of 130 or lower|Compatibility-level setting of 140|  
|--------------------------------------------------|-----------------------------------------|  
|Cardinality estimates for statements referencing multi-statement table valued functions use a fixed row guess.|Cardinality estimates for eligible statements referencing multi-statement table valued functions will use the actual cardinality of the function output.  This is enabled via **interleaved execution** for multi-statement table valued functions.|
|Batch-mode queries that request insufficient memory grant sizes that result in spills to disk may continue to have issues on consecutive executions.|Batch-mode queries that request insufficient memory grant sizes that result in spills to disk may have improved performance on consecutive executions. This is enabled via **batch mode memory grant feedback** which will update the memory grant size of a cached plan if spills have occurred for batch mode operators. |
|Batch-mode queries that request an excessive memory grant size that results in concurrency issues may continue to have issues on consecutive executions.|Batch-mode queries that request an excessive memory grant size that results in concurrency issues may have improved concurrency on consecutive executions. This is enabled via **batch mode memory grant feedback** which will update the memory grant size of a cached plan if an excessive amount was originally requested.|
|Batch-mode queries that contain join operators are eligible for three physical join algorithms, including nested loop, hash join and merge join. If cardinality estimates are incorrect for join inputs, an inappropriate join algorithm may be selected. If this occurs, performance will suffer and the inappropriate join algorithm will remain in-use until the cached plan is recompiled.|There is an additional join operator called **adaptive join**. If cardinality estimates are incorrect for the outer build join input, an inappropriate join algorithm may be selected. If this occurs and the statement is eligible for an adaptive join, a nested loop will be used for smaller join inputs and a hash join will be used for larger join inputs dynamically without requiring recompilation. |
|Trivial plans referencing Columnstore indexes are not eligible for batch mode execution. |A trivial plan referencing Columnstore indexes will be discarded in favor of a plan that is eligible for batch mode execution.|
|The `sp_execute_external_script` UDX operator can only run in row mode.|The `sp_execute_external_script` UDX operator is eligible for batch mode execution.|  
|Multi-statement table-valued functions (TVF's) do not have interleaved execution |Interleaved execution for multi-statement TVFs to improve plan quality.|

Fixes that were under trace flag 4199 in earlier versions of SQL Server prior to SQL Server 2017 are now enabled by default. With compatibility mode 140. Trace flag 4199 will still be applicable for new query optimizer fixes that are released after SQL Server 2017. For information about Trace Flag 4199, see [Trace Flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#4199).  
  
## Differences Between Compatibility Level 120 and Level 130  
This section describes new behaviors introduced with compatibility level 130.   

|Compatibility-level setting of 120 or lower|Compatibility-level setting of 130|  
|--------------------------------------------------|-----------------------------------------|  
|The INSERT in an INSERT-SELECT statement is single-threaded.|The INSERT in an INSERT-SELECT statement is multi-threaded or can have a parallel plan.|  
|Queries on a memory-optimized table execute single-threaded.|Queries on a memory-optimized table can now have parallel plans.|  
|Introduced the SQL 2014 Cardinality estimator **CardinalityEstimationModelVersion="120"**|Further cardinality estimation ( CE) Improvements with the Cardinality Estimation Model 130 which is visible from a Query plan. **CardinalityEstimationModelVersion="130"**| 
|Batch mode versus Row Mode changes with Columnstore indexes:<br /><ul><li>Sorts on a table with Columnstore index are in Row mode <li>Windowing function aggregates operate in row mode such as `LAG` or `LEAD` <li>Queries on Columnstore tables with Multiple distinct clauses operated in Row mode <li>Queries running under MAXDOP 1 or with a serial plan executed in Row mode</li></ul>| Batch mode versus Row Mode changes with Columnstore indexes:<br /><ul><li>Sorts on a table with a Columnstore index are now in batch mode <li>Windowing aggregates now operate in batch mode such as `LAG` or `LEAD` <li>Queries on Columnstore tables with Multiple distinct clauses operate in Batch mode <li>Queries running under MAXDOP 1 or with a serial plan execute in Batch Mode</li></ul>|  
|Statistics can be automatically updated. | The logic which automatically updates statistics is more aggressive on large tables.  In practice, this should reduce cases where customers have seen performance issues on queries where newly inserted rows are queried frequently but where the statistics had not been updated to include those values. |  
|Trace 2371 is OFF by default in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. | [Trace 2371](https://blogs.msdn.microsoft.com/psssql/2016/10/04/default-auto-statistics-update-threshold-change-for-sql-server-2016/) is ON by default in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Trace flag 2371 tells the auto statistics updater to sample a smaller yet wiser subset of rows, in a table that has a great many rows. <br/> <br/> One improvement is to include in the sample more rows that were inserted recently. <br/> <br/> Another improvement is to let queries run while the update statistics process is running, rather than blocking the query. |  
|For level 120, statistics are sampled by a *single*-threaded process.|For level 130, statistics are sampled by a *multi*-threaded process. |  
|253 incoming foreign keys is the limit.| A given table can be referenced by up to 10,000 incoming foreign keys or similar references. For restrictions, see [Create Foreign Key Relationships](../../relational-databases/tables/create-foreign-key-relationships.md). |  
|The deprecated MD2, MD4, MD5, SHA, and SHA1 hash algorithms are permitted.|Only SHA2_256 and SHA2_512 hash algorithms are permitted.|
||[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] includes improvements in some data types conversions and some (mostly uncommon) operations. For details see [SQL Server 2016 improvements in handling some data types and uncommon operations](https://support.microsoft.com/help/4010261/sql-server-2016-improvements-in-handling-some-data-types-and-uncommon).|
|The STRING_SPLIT function is not available.|The STRING_SPLIT function is available under compatibility level 130 or above. If your database compatibility level is lower than 130, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not be able to find and execute STRING_SPLIT function.|
  
Fixes that were under trace flag 4199 in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] are now enabled by default. With compatibility mode 130. Trace flag 4199 will still be applicable for new query optimizer fixes that are released after [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. To use the older query optimizer in [!INCLUDE[ssSDS](../../includes/sssds-md.md)] you must select compatibility level 110. For information about Trace Flag 4199, see [Trace Flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#4199).  
  
## Differences Between Lower Compatibility Levels and Level 120  
 This section describes new behaviors introduced with compatibility level 120.  
  
|Compatibility-level setting of 110 or lower|Compatibility-level setting of 120|  
|--------------------------------------------------|-----------------------------------------|  
|The older query optimizer is used.|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] includes substantial improvements to the component that creates and optimizes query plans. This new query optimizer feature is dependent upon use of the database compatibility level 120. New database applications should be developed using database compatibility level 120 to take advantage of these improvements. Applications that are migrated from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be carefully tested to confirm that good performance is maintained or improved. If performance degrades, you can set the database compatibility level to 110 or earlier to use the older query optimizer methodology.<br /><br /> Database compatibility level 120 uses a new cardinality estimator that is tuned for modern data warehousing and OLTP workloads. Before setting database compatibility level to 110 because of performance issues, see the recommendations in the Query Plans section of the [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] [What's New in Database Engine](../../database-engine/configure-windows/what-s-new-in-sql-server-2016-database-engine.md) topic.|  
|In compatibility levels lower than 120, the language setting is ignored when converting a **date** value to a string value. Note that this behavior is specific only to the **date** type. See example B in the Examples section below.|The language setting is not ignored when converting a **date** value to a string value.|  
|Recursive references on the right-hand side of an `EXCEPT` clause create an infinite loop. Example C in the Examples section below demonstrates this behavior.|Recursive references in an `EXCEPT` clause generates an error in compliance with the ANSI SQL standard.|  
|Recursive common table expression (CTE) allows duplicate column names.|Recursive CTE do not allow duplicate column names.|  
|Disabled triggers are enabled if the triggers are altered.|Altering a trigger does not change the state (enabled or disabled) of the trigger.|  
|The OUTPUT INTO table clause ignores the `IDENTITY_INSERT SETTING = OFF` and allows explicit values to be inserted.|You cannot insert explicit values for an identity column in a table when `IDENTITY_INSERT` is set to OFF.|  
|When the database containment is set to partial, validating the `$action` field in the `OUTPUT` clause of a `MERGE` statement can return a collation error.|The collation of the values returned by the `$action` clause of a `MERGE` statement is the database collation instead of the server collation and a collation conflict error is not returned.|  
|A `SELECT INTO` statement always creates a single-threaded insert operation.|A `SELECT INTO` statement can create a parallel insert operation. When inserting a large numbers of rows, the parallel operation can improve performance.|  
  
## Differences Between Lower Compatibility Levels and Levels 110 and 120  
 This section describes new behaviors introduced with compatibility level 110.   This section also applies to level 120.  
  
|Compatibility-level setting of 100 or lower|Compatibility-level setting of at least 110|  
|--------------------------------------------------|--------------------------------------------------|  
|Common language runtime (CLR) database objects are executed with version 4 of the CLR. However, some behavior changes introduced in version 4 of the CLR are avoided. For more information, see [What's New in CLR Integration](../../relational-databases/clr-integration/clr-integration-what-s-new.md).|CLR database objects are executed with version 4 of the CLR.|  
|The XQuery functions **string-length** and **substring** count each surrogate as two characters.|The XQuery functions **string-length** and **substring** count each surrogate as one character.|  
|`PIVOT` is allowed in a recursive common table expression (CTE) query. However, the query returns incorrect results when there are multiple rows per grouping.|`PIVOT` is not allowed in a recursive common table expression (CTE) query. An error is returned.|  
|The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.|New material cannot be encrypted using RC4 or RC4_128. Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.|  
|The default style for `CAST` and `CONVERT` operations on **time** and **datetime2** data types is 121 except when either type is used in a computed column expression. For computed columns, the default style is 0. This behavior impacts computed columns when they are created, used in queries involving auto-parameterization, or used in constraint definitions.<br /><br /> Example D in the Examples section below shows the difference between styles 0 and 121. It does not demonstrate the behavior described above. For more information about date and time styles, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).|Under compatibility level 110, the default style for `CAST` and `CONVERT` operations on **time** and **datetime2** data types is always 121. If your query relies on the old behavior, use a compatibility level less than 110, or explicitly specify the 0 style in the affected query.<br /><br /> Upgrading the database to compatibility level 110 will not change user data that has been stored to disk. You must manually correct this data as appropriate. For example, if you used `SELECT INTO` to create a table from a source that contained a computed column expression described above, the data (using style 0) would be stored rather than the computed column definition itself. You would need to manually update this data to match style 121.|  
|Any columns in remote tables of type **smalldatetime** that are referenced in a partitioned view are mapped as **datetime**. Corresponding columns in local tables (in the same ordinal position in the select list) must be of type **datetime**.|Any columns in remote tables of type **smalldatetime** that are referenced in a partitioned view are mapped as **smalldatetime**. Corresponding columns in local tables (in the same ordinal position in the select list) must be of type **smalldatetime**.<br /><br /> After upgrading to 110, the distributed partitioned view will fail because of the data type mismatch. You can resolve this by changing the data type on the remote table to **datetime** or setting the compatibility level of the local database to 100 or lower.|  
|`SOUNDEX` function implements the following rules:<br /><br /> 1) Upper-case H or upper-case W are ignored when separating two consonants that have the same number in the `SOUNDEX` code.<br /><br /> 2) If the first 2 characters of *character_expression* have the same number in the `SOUNDEX` code, both characters are included. Else, if a set of side-by-side consonants have same number in the `SOUNDEX` code, all of them are excluded except the first.|`SOUNDEX` function implements the following rules:<br /><br /> 1) If upper-case H or upper-case W separate two consonants that have the same number in the `SOUNDEX` code, the consonant to the right is ignored<br /><br /> 2) If a set of side-by-side consonants have same number in the `SOUNDEX` code, all of them are excluded except the first.<br /><br /> <br /><br /> The additional rules may cause the values computed by the `SOUNDEX` function to be different than the values computed under earlier compatibility levels. After upgrading to compatibility level 110, you may need to rebuild the indexes, heaps, or CHECK constraints that use the `SOUNDEX` function. For more information, see [SOUNDEX &#40;Transact-SQL&#41;](../../t-sql/functions/soundex-transact-sql.md).|  
  
## Differences Between Compatibility Level 90 and Level 100  
 This section describes new behaviors introduced with compatibility level 100.  
  
|Compatibility-level setting of 90|Compatibility-level setting of 100|Possibility of impact|  
|----------------------------------------|-----------------------------------------|---------------------------|  
|The QUOTED_IDENTIFER setting is always set to ON for multistatement table-valued functions when they are created regardless of the session level setting.|The QUOTED IDENTIFIER session setting is honored when multistatement table-valued functions are created.|Medium|  
|When you create or alter a partition function, **datetime** and **smalldatetime** literals in the function are evaluated assuming US_English as the language setting.|The current language setting is used to evaluate **datetime** and **smalldatetime** literals in the partition function.|Medium|  
|The `FOR BROWSE` clause is allowed (and ignored) in `INSERT` and `SELECT INTO` statements.|The `FOR BROWSE` clause is not allowed in `INSERT` and `SELECT INTO` statements.|Medium|  
|Full-text predicates are allowed in the `OUTPUT` clause.|Full-text predicates are not allowed in the `OUTPUT` clause.|Low|  
|`CREATE FULLTEXT STOPLIST`, `ALTER FULLTEXT STOPLIST`, and `DROP FULLTEXT STOPLIST` are not supported. The system stoplist is automatically associated with new full-text indexes.|`CREATE FULLTEXT STOPLIST`, `ALTER FULLTEXT STOPLIST`, and `DROP FULLTEXT STOPLIST` are supported.|Low|  
|`MERGE` is not enforced as a reserved keyword.|MERGE is a fully reserved keyword. The `MERGE` statement is supported under both 100 and 90 compatibility levels.|Low|  
|Using the \<dml_table_source> argument of the INSERT statement raises a syntax error.|You can capture the results of an OUTPUT clause in a nested INSERT, UPDATE, DELETE, or MERGE statement, and insert those results into a target table or view. This is done using the \<dml_table_source> argument of the INSERT statement.|Low|
|Unless `NOINDEX` is specified, `DBCC CHECKDB` or `DBCC CHECKTABLE` performs both physical and logical consistency checks on a single table or indexed view and on all its nonclustered and XML indexes. Spatial indexes are not supported.|Unless `NOINDEX` is specified, `DBCC CHECKDB` or `DBCC CHECKTABLE` performs both physical and logical consistency checks on a single table and on all its nonclustered indexes. However, on XML indexes, spatial indexes, and indexed views, only physical consistency checks are performed by default.<br /><br /> If `WITH EXTENDED_LOGICAL_CHECKS` is specified, logical checks are performed on indexed views, XML indexes, and spatial indexes, where present. By default, physical consistency checks are performed before the logical consistency checks. If `NOINDEX` is also specified, only the logical checks are performed.|Low|  
|When an OUTPUT clause is used with a data manipulation language (DML) statement and a run-time error occurs during statement execution, the entire transaction is terminated and rolled back.|When an `OUTPUT` clause is used with a data manipulation language (DML) statement and a run-time error occurs during statement execution, the behavior depends on the `SET XACT_ABORT` setting. If `SET XACT_ABORT` is OFF, a statement abort error generated by the DML statement using the `OUTPUT` clause will terminate the statement, but the execution of the batch continues and the transaction is not rolled back. If `SET XACT_ABORT` is ON, all run-time errors generated by the DML statement using the OUTPUT clause will terminate the batch, and the transaction is rolled back.|Low|  
|CUBE and ROLLUP are not enforced as reserved keywords.|`CUBE` and `ROLLUP` are reserved keywords within the GROUP BY clause.|Low|  
|Strict validation is applied to elements of the XML **anyType** type.|Lax validation is applied to elements of the **anyType** type. For more information, see [Wildcard Components and Content Validation](../../relational-databases/xml/wildcard-components-and-content-validation.md).|Low|  
|The special attributes **xsi:nil** and **xsi:type** cannot be queried or modified by data manipulation language statements.<br /><br /> This means that `/e/@xsi:nil` fails while `/e/@*` ignores the **xsi:nil** and **xsi:type** attributes. However, `/e` returns the **xsi:nil** and **xsi:type** attributes for consistency with `SELECT xmlCol`, even if `xsi:nil = "false"`.|The special attributes **xsi:nil** and **xsi:type** are stored as regular attributes and can be queried and modified.<br /><br /> For example, executing the query `SELECT x.query('a/b/@*')` returns all attributes including **xsi:nil** and **xsi:type**. To exclude these types in the query, replace `@*` with `@*[namespace-uri(.) != "`*insert xsi namespace uri*`"` and not `(local-name(.) = "type"` or `local-name(.) ="nil".`|Low|  
|A user-defined function that converts an XML constant string value to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] datetime type is marked as deterministic.|A user-defined function that converts an XML constant string value to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] datetime type is marked as non-deterministic.|Low|  
|The XML union and list types are not fully supported.|The union and list types are fully supported including the following functionality:<br /><br /> Union of list<br /><br /> Union of union<br /><br /> List of atomic types<br /><br /> List of union|Low|  
|The SET options required for an xQuery method are not validated when the method is contained in a view or inline table-valued function.|The SET options required for an xQuery method are validated when the method is contained in a view or inline table-valued function. An error is raised if the SET options of the method are set incorrectly.|Low|  
|XML attribute values that contain end-of-line characters (carriage return and line feed) are not normalized according to the XML standard. That is, both characters are returned instead of a single line-feed character.|XML attribute values that contain end-of-line characters (carriage return and line feed) are normalized according to the XML standard. That is, all line breaks in external parsed entities (including the document entity) are normalized on input by translating both the two-character sequence #xD #xA and any #xD that is not followed by #xA to a single #xA character.<br /><br /> Applications that use attributes to transport string values that contain end-of-line characters will not receive these characters back as they are submitted. To avoid the normalization process, use the XML numeric character entities to encode all end-of-line characters.|Low|  
|The column properties `ROWGUIDCOL` and `IDENTITY` can be incorrectly named as a constraint. For example the statement `CREATE TABLE T (C1 int CONSTRAINT MyConstraint IDENTITY)` executes, but the constraint name is not preserved and is not accessible to the user.|The column properties `ROWGUIDCOL` and `IDENTITY` cannot be named as a constraint. Error 156 is returned.|Low|  
|Updating columns by using a two-way assignment such as `UPDATE T1 SET @v = column_name = <expression>` can produce unexpected results because the live value of the variable can be used in other clauses such as the `WHERE` and `ON` clause during statement execution instead of the statement starting value. This can cause the meanings of the predicates to change unpredictably on a per-row basis.<br /><br /> This behavior is applicable only when the compatibility level is set to 90.|Updating columns by using a two-way assignment produces expected results because only the statement starting value of the column is accessed during statement execution.|Low|  
|See example E in the Examples section below.|See example F in the Examples section below.|Low|  
|The ODBC function {fn CONVERT()} uses the default date format of the language. For some languages, the default format is YDM, which can result in conversion errors when CONVERT() is combined with other functions, such as `{fn CURDATE()}`, that expect a YMD format.|The ODBC function `{fn CONVERT()}` uses style 121 (a language-independent YMD format) when converting to the ODBC data types SQL_TIMESTAMP, SQL_DATE, SQL_TIME, SQLDATE, SQL_TYPE_TIME, and SQL_TYPE_TIMESTAMP.|Low|  
|Datetime intrinsics such as DATEPART do not require string input values to be valid datetime literals. For example, `SELECT DATEPART (year, '2007/05-30')` compiles successfully.|Datetime intrinsics such as `DATEPART` require string input values to be valid datetime literals. Error 241 is returned when an invalid datetime literal is used.|Low|  
  
## Reserved Keywords  
 The compatibility setting also determines the keywords that are reserved by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The following table shows the reserved keywords that are introduced by each of the compatibility levels.  
  
|Compatibility-level setting|Reserved keywords|  
|----------------------------------|-----------------------|  
|130|To be determined.|  
|120|None.|  
|110|WITHIN GROUP, TRY_CONVERT, SEMANTICKEYPHRASETABLE, SEMANTICSIMILARITYDETAILSTABLE, SEMANTICSIMILARITYTABLE|  
|100|CUBE, MERGE, ROLLUP|  
|90|EXTERNAL, PIVOT, UNPIVOT, REVERT, TABLESAMPLE|  
  
 At a given compatibility level, the reserved keywords include all of the keywords introduced at or below that level. Thus, for instance, for applications at level 110, all of the keywords listed in the preceding table are reserved. At the lower compatibility levels, level-100 keywords remain valid object names, but the level-110 language features corresponding to those keywords are unavailable.  
  
 Once introduced, a keyword remains reserved. For example, the reserved keyword PIVOT, which was introduced in compatibility level 90, is also reserved in levels 100, 110, and 120.  
  
 If an application uses an identifier that is reserved as a keyword for its compatibility level, the application will fail. To work around this, enclose the identifier between either brackets (**[]**) or quotation marks (**""**); for example, to upgrade an application that uses the identifier **EXTERNAL** to compatibility level 90, you could change the identifier to either **[EXTERNAL]** or **"EXTERNAL"**.  
  
 For more information, see [Reserved Keywords &#40;Transact-SQL&#41;](../../t-sql/language-elements/reserved-keywords-transact-sql.md).  
  
## Permissions  
 Requires ALTER permission on the database.  
  
## Examples  
  
### A. Changing the compatibility level  
 The following example changes the compatibility level of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `110,`[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
```sql  
ALTER DATABASE AdventureWorks2012  
SET COMPATIBILITY_LEVEL = 110;  
GO  
```  
  
 The following example returns the compatibility level of the current database.  
  
```sql  
SELECT name, compatibility_level   
FROM sys.databases   
WHERE name = db_name();  
```  
  
### B. Ignoring  the SET LANGUAGE statement except under compatibility level 120  
 The following query ignores the SET LANGUAGE statement except under compatibility level 120.  
  
```sql  
SET DATEFORMAT dmy;   
DECLARE @t2 date = '12/5/2011' ;  
SET LANGUAGE dutch;   
SELECT CONVERT(varchar(11), @t2, 106);   
  
-- Results when the compatibility level is less than 120.   
12 May 2011   
  
-- Results when the compatibility level is set to 120).  
12 mei 2011  
```  
  
### C.  
 For compatibility-level setting of 110 or lower, recursive references on the right-hand side of an EXCEPT clause create an infinite loop.  
  
```sql  
WITH   
cte AS (SELECT * FROM (VALUES (1),(2),(3)) v (a)),  
r   
AS (SELECT a FROM Table1  
UNION ALL  
(SELECT a FROM Table1 EXCEPT SELECT a FROM r) )   
SELECT a   
FROM r;  
  
```  
  
### D.  
 This example shows the difference between styles 0 and 121. For more information about date and time styles, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
```sql  
CREATE TABLE t1 (c1 time(7), c2 datetime2);   
  
INSERT t1 (c1,c2) VALUES (GETDATE(), GETDATE());  
  
SELECT CONVERT(nvarchar(16),c1,0) AS TimeStyle0  
       ,CONVERT(nvarchar(16),c1,121)AS TimeStyle121  
       ,CONVERT(nvarchar(32),c2,0) AS Datetime2Style0  
       ,CONVERT(nvarchar(32),c2,121)AS Datetime2Style121  
FROM t1;  
  
-- Returns values such as the following.  
TimeStyle0       TimeStyle121       
Datetime2Style0      Datetime2Style121  
---------------- ----------------   
-------------------- --------------------------  
3:15PM           15:15:35.8100000   
Jun  7 2011  3:15PM  2011-06-07 15:15:35.8130000  
```  
  
### E.  
 Variable assignment is allowed in a statement containing a top-level UNION operator, but returns unexpected results. For example, in the following statements, local variable `@v` is assigned the value of the column `BusinessEntityID` from the union of two tables. By definition, when the SELECT statement returns more than one value, the variable is assigned the last value that is returned. In this case, the variable is correctly assigned the last value, however, the result set of the SELECT UNION statement is also returned.  
  
```sql  
ALTER DATABASE AdventureWorks2012  
SET compatibility_level = 110;  
GO  
USE AdventureWorks2012;  
GO  
DECLARE @v int;  
SELECT @v = BusinessEntityID FROM HumanResources.Employee  
UNION ALL  
SELECT @v = BusinessEntityID FROM HumanResources.EmployeeAddress;  
SELECT @v;  
```  
  
### F.  
 Variable assignment is not allowed in a statement containing a top-level UNION operator. Error 10734 is returned. To resolve the error, rewrite the query as shown in the following example.  
  
```sql  
DECLARE @v int;  
SELECT @v = BusinessEntityID FROM   
    (SELECT BusinessEntityID FROM HumanResources.Employee  
     UNION ALL  
     SELECT BusinessEntityID FROM HumanResources.EmployeeAddress) AS Test;  
SELECT @v;  
```  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [Reserved Keywords &#40;Transact-SQL&#41;](../../t-sql/language-elements/reserved-keywords-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md?&tabs=sqlserver)   
 [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)  
 [View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md) 
  
