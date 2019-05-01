---
title: "Options (SQL Server Object Explorer - Scripting Page) | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.ObjectExplorerScripting"
  - "VS.ToolsOptionsPages.Sql_Server_Object_Explorer.ObjectExplorerScripting"
ms.assetid: 6105aec9-1b72-4cb2-bd24-fc35f6d95240
author: "markingmyname"
ms.author: "maghan"
manager: craigg
---
# Options (SQL Server Object Explorer - Scripting Page)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Use this page to set scripting options that apply to the following commands on object context menus in **Object Explorer**:  
  
-   **Edit** commands for user tables and views.  
  
-   **Script <object> as** commands for user-created objects.  
  
-   **Modify** command for user-created objects.  
  
-   This page also sets the scripting option defaults for the **Generate SQL Server Script Wizard**.  
  
## Remarks  
The **Edit** and **Modify** commands might produce results that are different from the **Script <object> as** command for the same option setting. The **Edit** and **Modify** commands are designed to modify objects in the current database during a Query Editor session. The **Script <object> as** command is designed to generate a script so that it can be used later to create objects.  
  
## Options  
Specify scripting options by selecting from the available settings in the list to the right of each option.  
  
### General scripting options  
**Delimit individual statements**  
Separates individual [!INCLUDE[tsql](../../includes/tsql-md.md)] statements by using a batch separator. To change the default batch separator for **Query Editor**, select **Tools**/**Options**/**Query Execution**/**SQL Server**/**General**/**Batch separator**. Default is False. For more information, see [GO (Transact-SQL)](https://msdn.microsoft.com/b2ca6791-3a07-4209-ba8e-2248a92dd738).  
  
**Include descriptive headers**  
Adds descriptive comments to the script by separating the script into sections for each object. Default is True. For more information, see [/*...*/ (Comment) (Transact-SQL)](https://msdn.microsoft.com/4d9ab1b2-4bbb-4c16-beb1-cafc1af7417c).  
  
**Include enabling vardecimal compression**  
Includes the vardecimal storage options. Default is False. For more information, see [sp_db_vardecimal_storage_format (Transact-SQL)](https://msdn.microsoft.com/9920b2f7-b802-4003-913c-978c17ae4542).  
  
**Script change tracking**  
Includes change tracking information in the script.  
  
**Script full-text catalogs**  
Includes a script for full-text catalogs. Default is False. For more information, see [CREATE FULLTEXT CATALOG (Transact-SQL)](https://msdn.microsoft.com/d7a8bd93-e2d7-4a40-82ef-39069e65523b).  
  
**Script USE <database>**  
Adds the USE DATABASE statement to the script to create database objects in the context of the current **Object Explorer** database. When the script is expected for use in a different database, select False to omit. Default is True. For more information, see [USE (Transact-SQL)](https://msdn.microsoft.com/c05acac8-c063-4770-8e36-d7f71d500b10).  
  
### Object scripting options  

**Check for object existence**
Check that an object with the given name exists before dropping or altering or that an object with the given name does not exist before creating. For more information, see [IF...ELSE (Transact-SQL)](https://msdn.microsoft.com/676c881f-dee1-417a-bc51-55da62398e81) and [EXISTS (Transact-SQL)](https://msdn.microsoft.com/b6510a65-ac38-4296-a3d5-640db0c27631).

**Generate script for dependent objects**  
Generates a script for additional objects that are required when the script for the selected object is executed. Default is False.  
  
**Schema qualify object names**  
Qualifies object names with the object schema. Default is False. For more information, see [Create a Database Schema](../../relational-databases/security/authentication-access/create-a-database-schema.md).  

**Script data compression options**
Includes data compression options in the script. Default is False.

**Script extended properties**  
Includes extended properties in the script if the object has extended properties. Default is False. For more information, see [sp_addextendedproperty (Transact-SQL)](https://msdn.microsoft.com/565483ea-875b-4133-b327-d0006d2d7b4c).  
  
**Script owner**  
Includes the owner in the generated script. Default is False.  
  
**Script permissions**  
Includes permissions on database objects in the script. Default is True. For more information, see [Permissions](../../relational-databases/security/permissions-database-engine.md).  
  
### Table/View options  
The following options apply only to scripts for tables or views.  
  
**Convert user-defined data types to base types**  
Converts user-defined data types to the base types from which they were created. Use True when the source database user-defined data types do not exist in the database where the script will be run. Use False to keep the user-defined data types. Default is False. For more information, see [CREATE TYPE (Transact-SQL)](https://msdn.microsoft.com/2202236b-e09f-40a1-bbc7-b8cff7488905).  
  
**Generate SET ANSI PADDING commands**  
Adds the SET ANSI_PADDING statement before and after each CREATE TABLE statement. Default is True. For more information, see [SET ANSI_PADDING (Transact-SQL)](https://msdn.microsoft.com/92bd29a3-9beb-410e-b7e0-7bc1dc1ae6d0).  
  
**Include collation**  
Includes collation in column definition. Default is True. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
**Include IDENTITY property**  
Includes definitions for IDENTITY seed and IDENTITY increment. Default is True. For more information, see [IDENTITY (Property) (Transact-SQL)](https://msdn.microsoft.com/8429134f-c821-4033-a07c-f782a48d501c).  
  
**Schema qualify foreign key references**  
Adds the schema name to table references for FOREIGN KEY constraints. Default is True.  
  
**Script bound defaults and rules**  
Includes the **sp_bindefault** and **sp_bindrule** binding stored procedure calls. Default is True. For more information, see [sp_bindefault (Transact-SQL)](https://msdn.microsoft.com/3da70c10-68d0-4c16-94a5-9e84c4a520f6) and [sp_bindrule (Transact-SQL)](https://msdn.microsoft.com/2606073e-c52f-498d-a923-5026b9d97e67).  
  
**Script CHECK constraints**  
Adds [CHECK constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md) to the script. Default is True.  
  
**Script defaults**  
Includes column default values in the script. Default is False. For more information, see [CREATE DEFAULT (Transact-SQL)](https://msdn.microsoft.com/08475db4-7d90-486a-814c-01a99d783d41).  
  
**Script file groups**  
Specifies the filegroup in the ON clause for table definitions. Default is False. For more information, see [CREATE TABLE (Transact-SQL)](https://msdn.microsoft.com/1e068443-b9ea-486a-804f-ce7b6e048e8b).  
  
**Script foreign keys**  
Includes [FOREIGN KEY constraints](../../relational-databases/tables/primary-and-foreign-key-constraints.md) in the script. Default is False.  
  
**Script full-text indexes**  
Includes full-text indexes in the script. Default is False. For more information, see [CREATE FULLTEXT INDEX (Transact-SQL)](https://msdn.microsoft.com/8b80390f-5f8b-4e66-9bcc-cabd653c19fd).  
  
**Script indexes**  
Includes clustered, nonclustered, and XML indexes in the script. Default is True. For more information, see [CREATE INDEX (Transact-SQL)](https://msdn.microsoft.com/d2297805-412b-47b5-aeeb-53388349a5b9).  
  
**Script partition schemes**  
Includes table partitioning schemes in the script. Default is False. For more information, see [CREATE PARTITION SCHEME (Transact-SQL)](https://msdn.microsoft.com/5b21c53a-b4f4-4988-89a2-801f512126e4).  
  
**Script primary keys**  
Includes [Primary and Foreign Key Constraints](../../relational-databases/tables/primary-and-foreign-key-constraints.md) in the script. Default is True.  
  
**Script statistics**  
Includes user-defined statistics in the script. Default is False. For more information, see [CREATE STATISTICS (Transact-SQL)](https://msdn.microsoft.com/b23e2f6b-076c-4e6d-9281-764bdb616ad2).  
  
**Script triggers**  
Include triggers in the script. Default is False. For more information, see [CREATE TRIGGER (Transact-SQL)](https://msdn.microsoft.com/edeced03-decd-44c3-8c74-2c02f801d3e7).  
  
**Script unique keys**  
Includes [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md) in the script. Default is False.  
  
**Script view columns**  
Declares view columns in view headers. Default is False. For more information, see [CREATE VIEW (Transact-SQL)](https://msdn.microsoft.com/aecc2f73-2ab5-4db9-b1e6-2f9e3c601fb9).  
  
**Include dri system names**  
Includes system generated constraint names to enforce declarative referential integrity. Default is False. For more information, see [REFERENTIAL_CONSTRAINTS (Transact-SQL)](https://msdn.microsoft.com/5d358f18-0a85-4b55-af4b-98d5f4cd1020).  
  
### Version options

**Match script settings to source**
If enabled the target version, engine edition and engine type of the scripts generated will be set to the values of the server the object being scripted. This will disable (and ignore) the other version options. 

**Script for database engine edition**
Scripts generated will be targeted for the specified [Engine Edition](https://msdn.microsoft.com/library/microsoft.sqlserver.management.smo.edition.aspx).

**Script for database engine type**
Scripts generated will be targeted for the specified [Database Engine Type](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.databaseenginetype.aspx).

**Script for server version**  
Scripts generated will be targeted for the specified version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Features that are new in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] cannot be scripted for earlier versions. Some scripts that are created for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] cannot be executed on servers that are running on an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or on a database that has an earlier [database compatibility level setting](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  

## See also  
[Generate Scripts (SQL Server Management Studio)](https://msdn.microsoft.com/9711c617-3c68-4e5a-aea3-befc64d51524)  
  
