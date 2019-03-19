---
title: "Options (SQL Server Object Explorer-Scripting Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.ObjectExplorerScripting"
  - "VS.ToolsOptionsPages.Sql_Server_Object_Explorer.ObjectExplorerScripting"
ms.assetid: 6105aec9-1b72-4cb2-bd24-fc35f6d95240
author: stevestein
ms.author: sstein
manager: craigg
---
# Options (SQL Server Object Explorer-Scripting Page)
  Use this page to set scripting options that apply to the following commands on object context menus in **Object Explorer**:  
  
-   **Edit** commands for user tables and views.  
  
-   **Script \<object> as** commands for user-created objects.  
  
-   **Modify** command for user-created objects.  
  
-   This page also sets the scripting option defaults for the **Generate SQL Server Script Wizard**.  
  
## Remarks  
 The **Edit** and **Modify** commands might produce results that are different from the **Script \<object> as** command for the same option setting. The **Edit** and **Modify** commands are designed to modify objects in the current database during a Query Editor session. The **Script \<object> as** command is designed to generate a script so that it can be used later to create objects.  
  
## Options  
 Specify scripting options by selecting from the available settings in the list to the right of each option.  
  
### General Scripting Options  
 **Delimit individual statements**  
 Separates individual [!INCLUDE[tsql](../../includes/tsql-md.md)] statements by using a batch separator. To change the default batch separator for **Query Editor**, select **Tools**/**Options**/**Query Execution**/**SQL Server**/**General**/**Batch separator**. Default is False. For more information, see [GO &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/sql-server-utilities-statements-go).  
  
 **Include descriptive headers**  
 Adds descriptive comments to the script by separating the script into sections for each object. Default is True. For more information, see [Comment &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/comment-transact-sql).  
  
 **Include vardecimal options**  
 Includes the vardecimal storage options. Default is False. For more information, see and [sp_db_vardecimal_storage_format &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-db-vardecimal-storage-format-transact-sql).  
  
 **Script change tracking**  
 Includes change tracking information in the script.  
  
 **Script for server version**  
 Creates a script that can be run on the selected version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Features that are new in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] cannot be scripted for earlier versions. Some scripts that are created for [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] cannot be executed on servers that are running on an earlier version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], or on a database that has an earlier [database compatibility level setting](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level).  
  
 **Script full-text catalogs**  
 Includes a script for full-text catalogs. Default is False. For more information, see [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-catalog-transact-sql).  
  
 **Script USE \<database>**  
 Adds the USE DATABASE statement to the script to create database objects in the context of the current **Object Explorer** database. When the script is expected for use in a different database, select False to omit. Default is True. For more information, see [USE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/use-transact-sql).  
  
### Object Scripting Options  
 **Generate script for dependent objects**  
 Generates a script for additional objects that are required when the script for the selected object is executed. Default is False.  
  
 **Include If NOT EXISTS clause**  
 Includes a statement to check that each object does not exist in the database before trying to create the object. Default is False. For more information, see [IF...ELSE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/if-else-transact-sql) and [EXISTS &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/exists-transact-sql).  
  
 **Schema qualify object names**  
 Qualifies object names with the object schema. Default is False. For more information, see [Create a Database Schema](../../relational-databases/security/authentication-access/create-a-database-schema.md).  
  
 **Script extended properties**  
 Includes extended properties in the script if the object has extended properties. Default is False. For more information, see [sp_addextendedproperty &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql).  
  
 **Script owner**  
 Includes the owner in the generated script. Default is False.  
  
 **Script permissions**  
 Includes permissions on database objects in the script. Default is True. For more information, see [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md).  
  
### Table/View Options  
 The following options apply only to scripts for tables or views.  
  
 **Convert user-defined data types to base types**  
 Converts user-defined data types to the base types from which they were created. Use True when the source database user-defined data types do not exist in the database where the script will be run. Use False to keep the user-defined data types. Default is False. For more information, see [CREATE TYPE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-type-transact-sql).  
  
 **Generate SET ANSI PADDING commands**  
 Adds the SET ANSI_PADDING statement before and after each CREATE TABLE statement. Default is True. For more information, see [SET ANSI_PADDING &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-ansi-padding-transact-sql).  
  
 **Include collation**  
 Includes collation in column definition. Default is True. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
 **Include IDENTITY property**  
 Includes definitions for IDENTITY seed and IDENTITY increment. Default is True. For more information, see [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql-identity-property).  
  
 **Schema qualify foreign key references**  
 Adds the schema name to table references for FOREIGN KEY constraints. Default is True.  
  
 **Script bound defaults and rules**  
 Includes the **sp_bindefault** and **sp_bindrule** binding stored procedure calls. Default is True. For more information, see [sp_bindefault &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-bindefault-transact-sql) and [sp_bindrule &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-bindrule-transact-sql).  
  
 **Script CHECK constraints**  
 Adds [CHECK constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md) to the script. Default is True.  
  
 **Script defaults**  
 Includes column default values in the script. Default is False. For more information, see [CREATE DEFAULT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-default-transact-sql).  
  
 **Script file groups**  
 Specifies the filegroup in the ON clause for table definitions. Default is False. For more information, see [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql).  
  
 **Script foreign keys**  
 Includes [FOREIGN KEY constraints](../../relational-databases/tables/primary-and-foreign-key-constraints.md) in the script. Default is False.  
  
 **Script full-text indexes**  
 Includes full-text indexes in the script. Default is False. For more information, see [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql).  
  
 **Script indexes**  
 Includes clustered, nonclustered, and XML indexes in the script. Default is True. For more information, see [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql).  
  
 **Script partition schemes**  
 Includes table partitioning schemes in the script. Default is False. For more information, see [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-partition-scheme-transact-sql).  
  
 **Script primary keys**  
 Includes [Primary and Foreign Key Constraints](../../relational-databases/tables/primary-and-foreign-key-constraints.md) in the script. Default is True.  
  
 **Script statistics**  
 Includes user-defined statistics in the script. Default is False. For more information, see [CREATE STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-statistics-transact-sql).  
  
 **Script triggers**  
 Include triggers in the script. Default is False. For more information, see [CREATE TRIGGER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-trigger-transact-sql).  
  
 **Script unique keys**  
 Includes [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md) in the script. Default is False.  
  
 **Script view columns**  
 Declares view columns in view headers. Default is False. For more information, see [CREATE VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-view-transact-sql).  
  
 **ScriptDriIncludeSystemNames**  
 Includes system generated constraint names to enforce declarative referential integrity. Default is False. For more information, see [REFERENTIAL_CONSTRAINTS &#40;Transact-SQL&#41;](/sql/relational-databases/system-information-schema-views/referential-constraints-transact-sql).  
  
## See Also  
 [Generate Scripts &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/generate-scripts-sql-server-management-studio.md)  
  
  
