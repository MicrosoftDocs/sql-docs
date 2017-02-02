---
title: "Options (SQL Server Object Explorer - Scripting Page) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "VS.ToolsOptionsPages.ObjectExplorerScripting"
  - "VS.ToolsOptionsPages.Sql_Server_Object_Explorer.ObjectExplorerScripting"
ms.assetid: 6105aec9-1b72-4cb2-bd24-fc35f6d95240
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Options (SQL Server Object Explorer - Scripting Page)
Use this page to set scripting options that apply to the following commands on object context menus in **Object Explorer**:  
  
-   **Edit** commands for user tables and views.  
  
-   **Script <object> as** commands for user-created objects.  
  
-   **Modify** command for user-created objects.  
  
-   This page also sets the scripting option defaults for the **Generate SQL Server Script Wizard**.  
  
## Remarks  
The **Edit** and **Modify** commands might produce results that are different from the **Script <object> as** command for the same option setting. The **Edit** and **Modify** commands are designed to modify objects in the current database during a Query Editor session. The **Script <object> as** command is designed to generate a script so that it can be used later to create objects.  
  
## Options  
Specify scripting options by selecting from the available settings in the list to the right of each option.  
  
### General Scripting Options  
**Delimit individual statements**  
Separates individual [!INCLUDE[tsql](../../includes/tsql_md.md)] statements by using a batch separator. To change the default batch separator for **Query Editor**, select **Tools**/**Options**/**Query Execution**/**SQL Server**/**General**/**Batch separator**. Default is False. For more information, see [GO (Transact-SQL)](http://msdn.microsoft.com/en-us/b2ca6791-3a07-4209-ba8e-2248a92dd738).  
  
**Include descriptive headers**  
Adds descriptive comments to the script by separating the script into sections for each object. Default is True. For more information, see [/*...*/ (Comment) (Transact-SQL)](http://msdn.microsoft.com/en-us/4d9ab1b2-4bbb-4c16-beb1-cafc1af7417c).  
  
**Include vardecimal options**  
Includes the vardecimal storage options. Default is False. For more information, see and [sp_db_vardecimal_storage_format (Transact-SQL)](http://msdn.microsoft.com/en-us/9920b2f7-b802-4003-913c-978c17ae4542).  
  
**Script change tracking**  
Includes change tracking information in the script.  
  
**Script for server version**  
Creates a script that can be run on the selected version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)]. Features that are new in [!INCLUDE[ssCurrent](../../includes/sscurrent_md.md)] cannot be scripted for earlier versions. Some scripts that are created for [!INCLUDE[ssCurrent](../../includes/sscurrent_md.md)] cannot be executed on servers that are running on an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)], or on a database that has an earlier [database compatibility level setting](http://msdn.microsoft.com/en-us/ca5fd220-d5ea-4182-8950-55d4101a86f6).  
  
**Script full-text catalogs**  
Includes a script for full-text catalogs. Default is False. For more information, see [CREATE FULLTEXT CATALOG (Transact-SQL)](http://msdn.microsoft.com/en-us/d7a8bd93-e2d7-4a40-82ef-39069e65523b).  
  
**Script USE <database>**  
Adds the USE DATABASE statement to the script to create database objects in the context of the current **Object Explorer** database. When the script is expected for use in a different database, select False to omit. Default is True. For more information, see [USE (Transact-SQL)](http://msdn.microsoft.com/en-us/c05acac8-c063-4770-8e36-d7f71d500b10).  
  
### Object Scripting Options  
**Generate script for dependent objects**  
Generates a script for additional objects that are required when the script for the selected object is executed. Default is False.  
  
**Include If NOT EXISTS clause**  
Includes a statement to check that each object does not exist in the database before trying to create the object. Default is False. For more information, see [IF...ELSE (Transact-SQL)](http://msdn.microsoft.com/en-us/676c881f-dee1-417a-bc51-55da62398e81) and [EXISTS (Transact-SQL)](http://msdn.microsoft.com/en-us/b6510a65-ac38-4296-a3d5-640db0c27631).  
  
**Schema qualify object names**  
Qualifies object names with the object schema. Default is False. For more information, see [Create a Database Schema](http://msdn.microsoft.com/en-us/ed2a5522-f4d2-4111-95a4-d3e1e5081739).  
  
**Script extended properties**  
Includes extended properties in the script if the object has extended properties. Default is False. For more information, see [sp_addextendedproperty (Transact-SQL)](http://msdn.microsoft.com/en-us/565483ea-875b-4133-b327-d0006d2d7b4c).  
  
**Script owner**  
Includes the owner in the generated script. Default is False.  
  
**Script permissions**  
Includes permissions on database objects in the script. Default is True. For more information, see [Permissions](http://msdn.microsoft.com/en-us/f28e3dea-24e6-4a81-877b-02ec4c7e36b9).  
  
### Table/View Options  
The following options apply only to scripts for tables or views.  
  
**Convert user-defined data types to base types**  
Converts user-defined data types to the base types from which they were created. Use True when the source database user-defined data types do not exist in the database where the script will be run. Use False to keep the user-defined data types. Default is False. For more information, see [CREATE TYPE (Transact-SQL)](http://msdn.microsoft.com/en-us/2202236b-e09f-40a1-bbc7-b8cff7488905).  
  
**Generate SET ANSI PADDING commands**  
Adds the SET ANSI_PADDING statement before and after each CREATE TABLE statement. Default is True. For more information, see [SET ANSI_PADDING (Transact-SQL)](http://msdn.microsoft.com/en-us/92bd29a3-9beb-410e-b7e0-7bc1dc1ae6d0).  
  
**Include collation**  
Includes collation in column definition. Default is True. For more information, see [Collation and Unicode Support](http://msdn.microsoft.com/en-us/92d34f48-fa2b-47c5-89d3-a4c39b0f39eb).  
  
**Include IDENTITY property**  
Includes definitions for IDENTITY seed and IDENTITY increment. Default is True. For more information, see [IDENTITY (Property) (Transact-SQL)](http://msdn.microsoft.com/en-us/8429134f-c821-4033-a07c-f782a48d501c).  
  
**Schema qualify foreign key references**  
Adds the schema name to table references for FOREIGN KEY constraints. Default is True.  
  
**Script bound defaults and rules**  
Includes the **sp_bindefault** and **sp_bindrule** binding stored procedure calls. Default is True. For more information, see [sp_bindefault (Transact-SQL)](http://msdn.microsoft.com/en-us/3da70c10-68d0-4c16-94a5-9e84c4a520f6) and [sp_bindrule (Transact-SQL)](http://msdn.microsoft.com/en-us/2606073e-c52f-498d-a923-5026b9d97e67).  
  
**Script CHECK constraints**  
Adds [CHECK constraints](http://msdn.microsoft.com/en-us/637098af-2567-48f8-90f4-b41df059833e) to the script. Default is True.  
  
**Script defaults**  
Includes column default values in the script. Default is False. For more information, see [CREATE DEFAULT (Transact-SQL)](http://msdn.microsoft.com/en-us/08475db4-7d90-486a-814c-01a99d783d41).  
  
**Script file groups**  
Specifies the filegroup in the ON clause for table definitions. Default is False. For more information, see [CREATE TABLE (Transact-SQL)](http://msdn.microsoft.com/en-us/1e068443-b9ea-486a-804f-ce7b6e048e8b).  
  
**Script foreign keys**  
Includes [FOREIGN KEY constraints](http://msdn.microsoft.com/en-us/31fbcc9f-2dc5-4bf9-aa50-ed70ec7b5bcd) in the script. Default is False.  
  
**Script full-text indexes**  
Includes full-text indexes in the script. Default is False. For more information, see [CREATE FULLTEXT INDEX (Transact-SQL)](http://msdn.microsoft.com/en-us/8b80390f-5f8b-4e66-9bcc-cabd653c19fd).  
  
**Script indexes**  
Includes clustered, nonclustered, and XML indexes in the script. Default is True. For more information, see [CREATE INDEX (Transact-SQL)](http://msdn.microsoft.com/en-us/d2297805-412b-47b5-aeeb-53388349a5b9).  
  
**Script partition schemes**  
Includes table partitioning schemes in the script. Default is False. For more information, see [CREATE PARTITION SCHEME (Transact-SQL)](http://msdn.microsoft.com/en-us/5b21c53a-b4f4-4988-89a2-801f512126e4).  
  
**Script primary keys**  
Includes [Primary and Foreign Key Constraints](http://msdn.microsoft.com/en-us/31fbcc9f-2dc5-4bf9-aa50-ed70ec7b5bcd) in the script. Default is True.  
  
**Script statistics**  
Includes user-defined statistics in the script. Default is False. For more information, see [CREATE STATISTICS (Transact-SQL)](http://msdn.microsoft.com/en-us/b23e2f6b-076c-4e6d-9281-764bdb616ad2).  
  
**Script triggers**  
Include triggers in the script. Default is False. For more information, see [CREATE TRIGGER (Transact-SQL)](http://msdn.microsoft.com/en-us/edeced03-decd-44c3-8c74-2c02f801d3e7).  
  
**Script unique keys**  
Includes [Unique Constraints and Check Constraints](http://msdn.microsoft.com/en-us/637098af-2567-48f8-90f4-b41df059833e) in the script. Default is False.  
  
**Script view columns**  
Declares view columns in view headers. Default is False. For more information, see [CREATE VIEW (Transact-SQL)](http://msdn.microsoft.com/en-us/aecc2f73-2ab5-4db9-b1e6-2f9e3c601fb9).  
  
**ScriptDriIncludeSystemNames**  
Includes system generated constraint names to enforce declarative referential integrity. Default is False. For more information, see [REFERENTIAL_CONSTRAINTS (Transact-SQL)](http://msdn.microsoft.com/en-us/5d358f18-0a85-4b55-af4b-98d5f4cd1020).  
  
## See Also  
[Generate Scripts (SQL Server Management Studio)](http://msdn.microsoft.com/en-us/9711c617-3c68-4e5a-aea3-befc64d51524)  
  
