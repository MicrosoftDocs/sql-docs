---
title: "Databases | Microsoft Docs"
description: "Learn about database schemas, tables, filegroups, logins, and roles. See how you can use the SQL Server Management Studio tool to work with databases."
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "data warehouse [SQL Server]"
  - "OLTP databases [SQL Server]"
  - "databases [SQL Server], about databases"
ms.assetid: 316eea58-81b8-4bf3-a1fc-801946740e94
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Databases
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  A database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is made up of a collection of tables that stores a specific set of structured data. A table contains a collection of rows, also referred to as records or tuples, and columns, also referred to as attributes. Each column in the table is designed to store a certain type of information, for example, dates, names, dollar amounts, and numbers.  
  
## Basic Information about Databases  
 A computer can have one or more than one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed. Each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can contain one or many databases.  Within a database, there are one or many object ownership groups called schemas. Within each schema there are database objects such as tables, views, and stored procedures. Some objects such as certificates and asymmetric keys are contained within the database, but are not contained within a schema. For more information about creating tables, see [Tables](../../relational-databases/tables/tables.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases are stored in the file system in files. Files can be grouped into filegroups. For more information about files and filegroups, see [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md).  
  
 When people gain access to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] they are identified as a login. When people gain access to a database they are identified as a database user. A database user can be based on a login. If contained databases are enabled, a database user can be created that is not based on a login. For more information about users, see [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md).  
  
 A user that has access to a database can be given permission to access the objects in the database. Though permissions can be granted to individual users, we recommend creating database roles, adding the database users to the roles, and then grant access permission to the roles. Granting permissions to roles instead of users makes it easier to keep permissions consistent and understandable as the number of users grow and continually change. For more information about roles permissions, see [CREATE ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-role-transact-sql.md) and [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md).  
  
## Working with Databases  
 Most people who work with databases use the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] tool. The [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] tool has a graphical user interface for creating databases and the objects in the databases. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] also has a query editor for interacting with databases by writing [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] can be installed from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation disk, or downloaded from MSDN. For more information about [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] tool, see [SQL Server Management Studio (SSMS)](../../ssms/sql-server-management-studio-ssms.md).
  
## In This Section  

:::row:::
    :::column:::
        [System Databases](../../relational-databases/databases/system-databases.md)  
        [Contained Databases](../../relational-databases/databases/contained-databases.md)  
        [SQL Server Data Files in Microsoft Azure](../../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md)  
        [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)  
        [Database States](../../relational-databases/databases/database-states.md)  
        [File States](../../relational-databases/databases/file-states.md)  
        [Estimate the Size of a Database](../../relational-databases/databases/estimate-the-size-of-a-database.md)  
        [Copy Databases to Other Servers](../../relational-databases/databases/copy-databases-to-other-servers.md)  
        [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)  
        [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md)  
        [Change the Configuration Settings for a Database](../../relational-databases/databases/change-the-configuration-settings-for-a-database.md)  
        [Create a Database](../../relational-databases/databases/create-a-database.md)  
        [Delete a Database](../../relational-databases/databases/delete-a-database.md)  
    :::column-end:::
    :::column:::
        [Delete Data or Log Files from a Database](../../relational-databases/databases/delete-data-or-log-files-from-a-database.md)  
        [Display Data and Log Space Information for a Database](../../relational-databases/databases/display-data-and-log-space-information-for-a-database.md)  
        [Increase the Size of a Database](../../relational-databases/databases/increase-the-size-of-a-database.md)  
        [Rename a Database](../../relational-databases/databases/rename-a-database.md)  
        [Set a Database to Single-user Mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md)  
        [Shrink a Database](../../relational-databases/databases/shrink-a-database.md)  
        [Shrink a File](../../relational-databases/databases/shrink-a-file.md)  
        [View or Change the Properties of a Database](../../relational-databases/databases/view-or-change-the-properties-of-a-database.md)  
        [View a List of Databases on an Instance of SQL Server](../../relational-databases/databases/view-a-list-of-databases-on-an-instance-of-sql-server.md)  
        [View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md)  
        [Use the Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md)  
        [Create a User-Defined Data Type Alias](../../relational-databases/databases/create-a-user-defined-data-type-alias.md)  
        [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md)  
    :::column-end:::
:::row-end:::

## Related Content  
 [Indexes](../../relational-databases/indexes/indexes.md)  
  
 [Views](../../relational-databases/views/views.md)  
  
 [Stored Procedures &#40;Database Engine&#41;](../../relational-databases/stored-procedures/stored-procedures-database-engine.md)  
