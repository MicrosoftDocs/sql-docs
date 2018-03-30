---
title: "Options (SQL Server Object Explorer-Scripting Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-cross-instance"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "VS.ToolsOptionsPages.ObjectExplorerScripting"
  - "VS.ToolsOptionsPages.Sql_Server_Object_Explorer.ObjectExplorerScripting"
ms.assetid: 6105aec9-1b72-4cb2-bd24-fc35f6d95240
caps.latest.revision: 12
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
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
 Separates individual [!INCLUDE[tsql](../../includes/tsql-md.md)] statements by using a batch separator. To change the default batch separator for **Query Editor**, select **Tools**/**Options**/**Query Execution**/**SQL Server**/**General**/**Batch separator**. Default is False. For more information, see [GO &#40;Transact-SQL&#41;](../Topic/GO%20\(Transact-SQL\).md).  
  
 **Include descriptive headers**  
 Adds descriptive comments to the script by separating the script into sections for each object. Default is True. For more information, see [Comment &#40;Transact-SQL&#41;](../Topic/Comment%20\(Transact-SQL\).md).  
  
 **Include vardecimal options**  
 Includes the vardecimal storage options. Default is False. For more information, see and [sp_db_vardecimal_storage_format &#40;Transact-SQL&#41;](../Topic/sp_db_vardecimal_storage_format%20\(Transact-SQL\).md).  
  
 **Script change tracking**  
 Includes change tracking information in the script.  
  
 **Script for server version**  
 Creates a script that can be run on the selected version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Features that are new in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] cannot be scripted for earlier versions. Some scripts that are created for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] cannot be executed on servers that are running on an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or on a database that has an earlier [database compatibility level setting](../Topic/ALTER%20DATABASE%20Compatibility%20Level%20\(Transact-SQL\).md).  
  
 **Script full-text catalogs**  
 Includes a script for full-text catalogs. Default is False. For more information, see [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](../Topic/CREATE%20FULLTEXT%20CATALOG%20\(Transact-SQL\).md).  
  
 **Script USE \<database>**  
 Adds the USE DATABASE statement to the script to create database objects in the context of the current **Object Explorer** database. When the script is expected for use in a different database, select False to omit. Default is True. For more information, see [USE &#40;Transact-SQL&#41;](../Topic/USE%20\(Transact-SQL\).md).  
  
### Object Scripting Options  
 **Generate script for dependent objects**  
 Generates a script for additional objects that are required when the script for the selected object is executed. Default is False.  
  
 **Include If NOT EXISTS clause**  
 Includes a statement to check that each object does not exist in the database before trying to create the object. Default is False. For more information, see [IF...ELSE &#40;Transact-SQL&#41;](../Topic/IF...ELSE%20\(Transact-SQL\).md) and [EXISTS &#40;Transact-SQL&#41;](../Topic/EXISTS%20\(Transact-SQL\).md).  
  
 **Schema qualify object names**  
 Qualifies object names with the object schema. Default is False. For more information, see [Create a Database Schema](../../2014/database-engine/create-a-database-schema.md).  
  
 **Script extended properties**  
 Includes extended properties in the script if the object has extended properties. Default is False. For more information, see [sp_addextendedproperty &#40;Transact-SQL&#41;](../Topic/sp_addextendedproperty%20\(Transact-SQL\).md).  
  
 **Script owner**  
 Includes the owner in the generated script. Default is False.  
  
 **Script permissions**  
 Includes permissions on database objects in the script. Default is True. For more information, see [Permissions &#40;Database Engine&#41;](../../2014/database-engine/permissions-database-engine.md).  
  
### Table/View Options  
 The following options apply only to scripts for tables or views.  
  
 **Convert user-defined data types to base types**  
 Converts user-defined data types to the base types from which they were created. Use True when the source database user-defined data types do not exist in the database where the script will be run. Use False to keep the user-defined data types. Default is False. For more information, see [CREATE TYPE &#40;Transact-SQL&#41;](../Topic/CREATE%20TYPE%20\(Transact-SQL\).md).  
  
 **Generate SET ANSI PADDING commands**  
 Adds the SET ANSI_PADDING statement before and after each CREATE TABLE statement. Default is True. For more information, see [SET ANSI_PADDING &#40;Transact-SQL&#41;](../Topic/SET%20ANSI_PADDING%20\(Transact-SQL\).md).  
  
 **Include collation**  
 Includes collation in column definition. Default is True. For more information, see [Collation and Unicode Support](../../2014/database-engine/collation-and-unicode-support.md).  
  
 **Include IDENTITY property**  
 Includes definitions for IDENTITY seed and IDENTITY increment. Default is True. For more information, see [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../Topic/IDENTITY%20\(Property\)%20\(Transact-SQL\).md).  
  
 **Schema qualify foreign key references**  
 Adds the schema name to table references for FOREIGN KEY constraints. Default is True.  
  
 **Script bound defaults and rules**  
 Includes the **sp_bindefault** and **sp_bindrule** binding stored procedure calls. Default is True. For more information, see [sp_bindefault &#40;Transact-SQL&#41;](../Topic/sp_bindefault%20\(Transact-SQL\).md) and [sp_bindrule &#40;Transact-SQL&#41;](../Topic/sp_bindrule%20\(Transact-SQL\).md).  
  
 **Script CHECK constraints**  
 Adds [CHECK constraints](../../2014/database-engine/unique-constraints-and-check-constraints.md) to the script. Default is True.  
  
 **Script defaults**  
 Includes column default values in the script. Default is False. For more information, see [CREATE DEFAULT &#40;Transact-SQL&#41;](../Topic/CREATE%20DEFAULT%20\(Transact-SQL\).md).  
  
 **Script file groups**  
 Specifies the filegroup in the ON clause for table definitions. Default is False. For more information, see [CREATE TABLE &#40;Transact-SQL&#41;](../Topic/CREATE%20TABLE%20\(Transact-SQL\).md).  
  
 **Script foreign keys**  
 Includes [FOREIGN KEY constraints](../../2014/database-engine/primary-and-foreign-key-constraints.md) in the script. Default is False.  
  
 **Script full-text indexes**  
 Includes full-text indexes in the script. Default is False. For more information, see [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../Topic/CREATE%20FULLTEXT%20INDEX%20\(Transact-SQL\).md).  
  
 **Script indexes**  
 Includes clustered, nonclustered, and XML indexes in the script. Default is True. For more information, see [CREATE INDEX &#40;Transact-SQL&#41;](../Topic/CREATE%20INDEX%20\(Transact-SQL\).md).  
  
 **Script partition schemes**  
 Includes table partitioning schemes in the script. Default is False. For more information, see [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../Topic/CREATE%20PARTITION%20SCHEME%20\(Transact-SQL\).md).  
  
 **Script primary keys**  
 Includes [Primary and Foreign Key Constraints](../../2014/database-engine/primary-and-foreign-key-constraints.md) in the script. Default is True.  
  
 **Script statistics**  
 Includes user-defined statistics in the script. Default is False. For more information, see [CREATE STATISTICS &#40;Transact-SQL&#41;](../Topic/CREATE%20STATISTICS%20\(Transact-SQL\).md).  
  
 **Script triggers**  
 Include triggers in the script. Default is False. For more information, see [CREATE TRIGGER &#40;Transact-SQL&#41;](../Topic/CREATE%20TRIGGER%20\(Transact-SQL\).md).  
  
 **Script unique keys**  
 Includes [Unique Constraints and Check Constraints](../../2014/database-engine/unique-constraints-and-check-constraints.md) in the script. Default is False.  
  
 **Script view columns**  
 Declares view columns in view headers. Default is False. For more information, see [CREATE VIEW &#40;Transact-SQL&#41;](../Topic/CREATE%20VIEW%20\(Transact-SQL\).md).  
  
 **ScriptDriIncludeSystemNames**  
 Includes system generated constraint names to enforce declarative referential integrity. Default is False. For more information, see [REFERENTIAL_CONSTRAINTS &#40;Transact-SQL&#41;](../Topic/REFERENTIAL_CONSTRAINTS%20\(Transact-SQL\).md).  
  
## See Also  
 [Generate Scripts &#40;SQL Server Management Studio&#41;](../../2014/database-engine/generate-scripts-sql-server-management-studio.md)  
  
  