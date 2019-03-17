---
title: "Database Properties (Options Page) | Microsoft Docs"
ms.custom: ""
ms.date: "08/28/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.databaseproperties.options.f1"
ms.assetid: a3447987-5507-4630-ac35-58821b72354d
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Database Properties (Options Page)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Use this page to view or modify options for the selected database. For more information about the options available on this page, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md) and [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).  
  
## Page Header  
 **Collation**  
 Specify the collation of the database by selecting from the list. For more information, see [Set or Change the Database Collation](../../relational-databases/collations/set-or-change-the-database-collation.md).  
  
 **Recovery model**  
 Specify one of the following models for recovering the database: **Full**, **Bulk-Logged**, or **Simple**. For more information about recovery models, see [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md).  
  
 **Compatibility level**  
 Specify the latest version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that the database supports. For possible values, see [ALTER DATABASE (Transact-SQL) Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md). When a SQL Server database is upgraded, the compatibility level for that database is retained if possible, or changed to the minimum level supported for the new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 
  
 **Containment type**  
 Specify none or partial to designate if this is a contained database. For more information about contained databases, see [Contained Databases](../../relational-databases/databases/contained-databases.md). The server property **Enable Contained Databases** must be set to **TRUE** before a database can be configured as contained.  
  
> [!IMPORTANT]  
>  Enabling partially contained databases delegates control over access to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the owners of the database. For more information, see [Security Best Practices with Contained Databases](../../relational-databases/databases/security-best-practices-with-contained-databases.md).  
  
## Automatic  
 **Auto Close**  
 Specify whether the database shuts down cleanly and frees resources after the last user exits. Possible values are **True** and **False**. When **True**, the database is shut down cleanly and its resources are freed after the last user logs off.  

 **Auto Create Incremental Statistics**  
 Specify whether to use the incremental option when per partition statistics are created. For information about incremental statistics, see [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md).  
  
 **Auto Create Statistics**  
 Specify whether the database automatically creates missing optimization statistics. Possible values are **True** and **False**. When **True**, any missing statistics needed by a query for optimization are automatically built during optimization. For more information, see [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md).  
  
 **Auto Shrink**  
 Specify whether the database files are available for periodic shrinking. Possible values are **True** and **False**. For more information, see [Shrink a Database](../../relational-databases/databases/shrink-a-database.md).  
  
 **Auto Update Statistics**  
 Specify whether the database automatically updates out-of-date optimization statistics. Possible values are **True** and **False**. When **True**, any out-of-date statistics needed by a query for optimization are automatically built during optimization. For more information, see [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md).  
  
 **Auto Update Statistics Asynchronously**  
 When **True**, queries that initiate an automatic update of out-of-date statistics do not wait for the statistics to be updated before compiling. Subsequent queries use the updated statistics when they are available.  
  
 When **False**, queries that initiate an automatic update of out-of-date statistics, wait until the updated statistics can be used in the query optimization plan.  
  
 Setting this option to **True** has no effect unless **Auto Update Statistics** is also set to **True**.  

## Azure
When connected to Azure SQL Database, this section has settings to control the Service Level Objective (SLO). The default SLO for a new database is Standard S2.

  **Current Service Level Objective**
  The specific SLO to use. Valid values are constrained by the selected edition. If your desired SLO value is not in the list, you can type the value.

  **Edition**
  The Azure SQL Database edition to use, such as Basic or Premium. If the edition value you need is not in the list, you can type the value, which must match the value used in Azure REST APIs.
  
  **Max Size**
  The maximum size of the database. If the desired size value is not in the list, you can type the value. Leave blank for the default size of the given edition and SLO.
  
## Containment  
 In a contained database, some settings usually configured at the server level can be configured at the database level.  
  
 **Default Fulltext Language LCID**  
 Specifies a default language for full-text indexed columns. Linguistic analysis of full-text indexed data is dependent on the language of the data. The default value of this option is the language of the server. For the language that corresponds to the displayed setting, see [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md).  
  
 **Default Language**  
 The default language for all new contained database users, unless otherwise specified.  
  
 **Nested Triggers Enabled**  
 Allows triggers to fire other triggers. Triggers can be nested to a maximum of 32 levels. For more information, see the "Nested Triggers" section in [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md).  
  
 **Transform Noise Words**  
 Suppress an error message if noise words, that is stopwords, cause a Boolean operation on a full-text query to return zero rows. For more information, see [transform noise words Server Configuration Option](../../database-engine/configure-windows/transform-noise-words-server-configuration-option.md).  
  
 **Two Digit Year Cutoff**  
 Indicates the highest year number that can be entered as a two-digit year. The year listed and the previous 99 years can be entered as a two-digit year. All other years must be entered as a four-digit year.  
  
 For example, the default setting of 2049 indicates that a date entered as '3/14/49' will be interpreted as March 14, 2049, and a date entered as '3/14/50' will be interpreted as March 14, 1950. For more information, see [Configure the two digit year cutoff Server Configuration Option](../../database-engine/configure-windows/configure-the-two-digit-year-cutoff-server-configuration-option.md).  
  
## Cursor  
 **Close Cursor on Commit Enabled**  
 Specify whether cursors close after the transaction opening the cursor has committed. Possible values are **True** and **False**. When **True**, any cursors that are open when a transaction is committed or rolled back are closed. When **False**, such cursors remain open when a transaction is committed. When **False**, rolling back a transaction closes any cursors except those defined as INSENSITIVE or STATIC. For more information, see [SET CURSOR_CLOSE_ON_COMMIT &#40;Transact-SQL&#41;](../../t-sql/statements/set-cursor-close-on-commit-transact-sql.md).  
  
 **Default Cursor**  
 Specify default cursor behavior. When **True**, cursor declarations default to LOCAL. When **False**, [!INCLUDE[tsql](../../includes/tsql-md.md)] cursors default to GLOBAL.  
  
## Database Scoped Configurations  
 In SQL Server 2016 and in Azure SQL Database, there are a number of configuration properties that can be scoped to the database level. For more information for all of these settings, see [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).  
  
 **Legacy Cardinality Estimation**  
 Specify the query optimizer cardinality estimation model for the primary independent of the compatibility level of the database. This is equivalent to [Trace Flag 9481](https://support.microsoft.com/kb/2801413).  
  
 **Legacy Cardinality Estimation for Secondary**  
 Specify the query optimizer cardinality estimation model for secondaries, if any, independent of the compatibility level of the database. This is equivalent to [Trace Flag 9481](https://support.microsoft.com/kb/2801413).  
  
 **Max DOP**  
 Specify the default [MAXDOP](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) setting for the primary that should be used for statements.  
  
 **Max DOP for Secondary**  
 Specify the default [MAXDOP](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) setting for secondaries, if any, that should be used for statements.  
  
 **Parameter Sniffing**  
 Enables or disables parameter sniffing on the primary. This is equivalent to [Trace Flag 4136](https://support.microsoft.com/kb/980653).  
  
 **Parameter Sniffing for Secondary**  
 Enables or disables parameter sniffing on secondaries, if any. This is equivalent to [Trace Flag 4136](https://support.microsoft.com/kb/980653).  
  
 **Query Optimizer Fixes**  
 Enables or disables query optimization hotfixes on the primary regardless of the compatibility level of the database. This is equivalent to [Trace Flag 4199](https://support.microsoft.com/kb/974006).  
  
 **Query Optimizer Fixes for Secondary**  
 Enables or disables query optimization hotfixes on secondaries, if any, regardless of the compatibility level of the database. This is equivalent to [Trace Flag 4199](https://support.microsoft.com/kb/974006).  
  
## FILESTREAM  
 **FILESTREAM Directory Name**  
 Specify the directory name for the FILESTREAM data associated with the selected database.  
  
 **FILESTREAM Non-transacted Access**  
 Specify one of the following options for non-transactional access through the file system to FILESTREAM data stored in FileTables: **OFF**, **READ_ONLY**, or **FULL**. If FILESTREAM is not enabled on the server, this value is set to OFF and is disabled. For more information, see [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md).  
  
## Miscellaneous  
**Allow Snapshot Isolation**  
Enables this feature.  

 **ANSI NULL Default**  
 Allow null values for all user-defined data types or columns that are not explicitly defined as **NOT NULL** during a **CREATE TABLE** or **ALTER TABLE** statement (the default state). For more information, see [SET ANSI_NULL_DFLT_ON &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-null-dflt-on-transact-sql.md) and [SET ANSI_NULL_DFLT_OFF &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-null-dflt-off-transact-sql.md).  
  
 **ANSI NULLS Enabled**  
 Specify the behavior of the Equals (`=`) and Not Equal To (`<>`) comparison operators when used with null values. Possible values are **True** (on) and **False** (off). When **True**, all comparisons to a null value evaluate to UNKNOWN. When **False**, comparisons of non-UNICODE values to a null value evaluate to **True** if both values are NULL. For more information, see [SET ANSI_NULLS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-nulls-transact-sql.md).  
  
 **ANSI Padding Enabled**  
 Specify whether ANSI padding is on or off. Permissible values are **True** (on) and **False** (off). For more information, see [SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md).  
  
 **ANSI Warnings Enabled**  
 Specify ISO standard behavior for several error conditions. When **True**, a warning message is generated if null values appear in aggregate functions (such as SUM, AVG, MAX, MIN, STDEV, STDEVP, VAR, VARP, or COUNT). When **False**, no warning is issued. For more information, see [SET ANSI_WARNINGS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-warnings-transact-sql.md).  
  
 **Arithmetic Abort Enabled**  
 Specify whether the database option for arithmetic abort is enabled or not. Possible values are **True** and **False**. When **True**, an overflow or divide-by-zero error causes the query or batch to terminate. If the error occurs in a transaction, the transaction is rolled back. When **False**, a warning message is displayed, but the query, batch, or transaction continues as if no error occurred. For more information, see [SET ARITHABORT &#40;Transact-SQL&#41;](../../t-sql/statements/set-arithabort-transact-sql.md).  
  
 **Concatenate Null Yields Null**  
 Specify the behavior when null values are concatenated. When the property value is **True**, **string** + NULL returns NULL. When **False**, the result is **string**. For more information, see [SET CONCAT_NULL_YIELDS_NULL &#40;Transact-SQL&#41;](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).  
  
 **Cross-database Ownership Chaining Enabled**  
 This read-only value indicates if cross-database ownership chaining has been enabled. When **True**, the database can be the source or target of a cross-database ownership chain. Use the ALTER DATABASE statement to set this property.  
  
 **Date Correlation Optimization Enabled**  
 When **True**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maintains correlation statistics between any two tables in the database that are linked by a FOREIGN KEY constraint and have **datetime** columns.  
  
 When **False**, correlation statistics are not maintained.  
 
 **Delayed Durability**  
 Enables this feature.  
 
 **Is Read Committed Snapshot On**  
 Enables this feature.  
 
 **Numeric Round-Abort**  
 Specify how the database handles rounding errors. Possible values are **True** and **False**. When **True**, an error is generated when loss of precision occurs in an expression. When **False**, losses of precision do not generate error messages, and the result is rounded to the precision of the column or variable storing the result. For more information, see [SET NUMERIC_ROUNDABORT &#40;Transact-SQL&#41;](../../t-sql/statements/set-numeric-roundabort-transact-sql.md).  
  
 **Parameterization**  
 When **SIMPLE**, queries are parameterized based on the default behavior of the database. When **FORCED**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] parameterizes all queries in the database.  
  
 **Quoted Identifiers Enabled**  
 Specify whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] keywords can be used as identifiers (an object or variable name) if enclosed in quotation marks. Possible values are **True** and **False**. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
 **Recursive Triggers Enabled**  
 Specify whether triggers can be fired by other triggers. Possible values are **True** and **False**. When set to **True**, this enables recursive firing of triggers. When set to **False**, only direct recursion is prevented. To disable indirect recursion, set the nested triggers server option to 0 using sp_configure. For more information, see [Create Nested Triggers](../../relational-databases/triggers/create-nested-triggers.md).  
  
 **Trustworthy**  
 When displaying **True**, this read-only option indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows access to resources outside the database under an impersonation context established within the database. Impersonation contexts can be established within the database using the EXECUTE AS user statement or the EXECUTE AS clause on database modules.  
  
 To have access, the owner of the database also needs to have the AUTHENTICATE SERVER permission at the server level.  
  
 This property also allows the creation and execution of unsafe and external access assemblies within the database. In addition to setting this property to **True**, the owner of the database must have the EXTERNAL ACCESS ASSEMBLY or UNSAFE ASSEMBLY permission at the server level.  
  
 By default, all user databases and all system databases (with the exception of **MSDB**) have this property set to **False**. The value cannot be changed for the **model** and **tempdb** databases.  
  
 TRUSTWORTHY is set to **False** whenever a database is attached to the server.  
  
 The recommended approach for accessing resources outside the database under an impersonation context is to use certificates and signatures as apposed to the **Trustworthy** option.  
  
 To set this property, use the ALTER DATABASE statement.  
  
 **VarDecimal Storage Format Enabled**  
 This option is read-only starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]. When **True**, this database is enabled for the vardecimal storage format. Vardecimal storage format cannot be disabled while any tables in the database are using it. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, all databases are enabled for the vardecimal storage format. This option uses [sp_db_vardecimal_storage_format](../../relational-databases/system-stored-procedures/sp-db-vardecimal-storage-format-transact-sql.md).  
  
## Recovery  
 **Page Verify**  
 Specify the option used to discover and report incomplete I/O transactions caused by disk I/O errors. Possible values are **None**, **TornPageDetection**, and **Checksum**. For more information, see [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md).  
  
 **Target Recovery Time (Seconds)**  
 Specifies the maximum bound on the time, expressed in seconds, to recover the specified database in the event of a crash. For more information, see [Database Checkpoints &#40;SQL Server&#41;](../../relational-databases/logs/database-checkpoints-sql-server.md).  

## Service Broker  
**Broker Enabled**  
Enables or disables Service Broker.  

**Honor Broker Priority**  
Read-only Service Broker property.  

**Serice Broker Identifier**  
Read-only identifier.  

## State  
 **Database Read Only**  
 Specify whether the database is read-only. Possible values are **True** and **False**. When **True**, users can only read data in the database. Users cannot modify the data or database objects; however, the database itself can be deleted using the `DROP DATABASE` statement. The database cannot be in use when a new value for the **Database Read Only** option is specified. The master database is the exception, and only the system administrator can use master while the option is being set.  
  
 **Database State**  
 View the current state of the database. It is not editable. For more information about **Database State**, see [Database States](../../relational-databases/databases/database-states.md).  

 **Encryption Enabled**  
 When **True**, this database is enabled for database encryption. A Database Encryption Key is required for encryption. For more information, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).  
 
 **Restrict Access**  
 Specify which users may access the database. Possible values are:  
  
-   **Multiple**  
  
     The normal state for a production database, allows multiple users to access the database at once.  
  
-   **Single**  
  
     Used for maintenance actions, only one user is allowed to access the database at once.  
  
-   **Restricted**  
  
     Only members of the db_owner, dbcreator, or sysadmin roles can use the database.  
  


## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)  
  
