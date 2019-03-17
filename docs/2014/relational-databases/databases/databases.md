---
title: "Databases | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "data warehouse [SQL Server]"
  - "OLTP databases [SQL Server]"
  - "databases [SQL Server], about databases"
ms.assetid: 316eea58-81b8-4bf3-a1fc-801946740e94
author: stevestein
ms.author: sstein
manager: craigg
---
# Databases
  A database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is made up of a collection of tables that stores a specific set of structured data. A table contains a collection of rows, also referred to as records or tuples, and columns, also referred to as attributes. Each column in the table is designed to store a certain type of information, for example, dates, names, dollar amounts, and numbers.  
  
## Basic Information about Databases  
 A computer can have one or more than one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed. Each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can contain one or many databases.  Within a database, there are one or many object ownership groups called schemas. Within each schema there are database objects such as tables, views, and stored procedures. Some objects such as certificates and asymmetric keys are contained within the database, but are not contained within a schema. For more information about creating tables, see [Tables](../tables/tables.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases are stored in the file system in files. Files can be grouped into filegroups. For more information about files and filegroups, see [Database Files and Filegroups](database-files-and-filegroups.md).  
  
 When people gain access to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] they are identified as a login. When people gain access to a database they are identified as a database user. A database user can be based on a login. If contained databases are enabled, a database user can be created that is not based on a login. For more information about users, see [CREATE USER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-user-transact-sql).  
  
 A user that has access to a database can be given permission to access the objects in the database. Though permissions can be granted to individual users, we recommend creating database roles, adding the database users to the roles, and then grant access permission to the roles. Granting permissions to roles instead of users makes it easier to keep permissions consistent and understandable as the number of users grow and continually change. For more information about roles permissions, see [CREATE ROLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-role-transact-sql) and [Principals &#40;Database Engine&#41;](../security/authentication-access/principals-database-engine.md).  
  
## Working with Databases  
 Most people who work with databases use the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] tool. The [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] tool has a graphical user interface for creating databases and the objects in the databases. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] also has a query editor for interacting with databases by writing [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] can be installed from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation disk, or downloaded from MSDN.  
  
## In This Section  
  
|||  
|-|-|  
|[System Databases](system-databases.md)|[Delete Data or Log Files from a Database](delete-data-or-log-files-from-a-database.md)|  
|[Contained Databases](contained-databases.md)|[Display Data and Log Space Information for a Database](display-data-and-log-space-information-for-a-database.md)|  
|[SQL Server Data Files in Windows Azure](sql-server-data-files-in-microsoft-azure.md)|[Increase the Size of a Database](increase-the-size-of-a-database.md)|  
|[Database Files and Filegroups](database-files-and-filegroups.md)|[Rename a Database](rename-a-database.md)|  
|[Database States](database-states.md)|[Set a Database to Single-user Mode](set-a-database-to-single-user-mode.md)|  
|[File States](file-states.md)|[Shrink a Database](shrink-a-database.md)|  
|[Estimate the Size of a Database](estimate-the-size-of-a-database.md)|[Shrink a File](shrink-a-file.md)|  
|[Copy Databases to Other Servers](copy-databases-to-other-servers.md)|[View or Change the Properties of a Database](view-or-change-the-properties-of-a-database.md)|  
|[Database Detach and Attach &#40;SQL Server&#41;](database-detach-and-attach-sql-server.md)|[View a List of Databases on an Instance of SQL Server](view-a-list-of-databases-on-an-instance-of-sql-server.md)|  
|[Add Data or Log Files to a Database](add-data-or-log-files-to-a-database.md)|[View or Change the Compatibility Level of a Database](view-or-change-the-compatibility-level-of-a-database.md)|  
|[Change the Configuration Settings for a Database](change-the-configuration-settings-for-a-database.md)|[Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md)|  
|[Create a Database](create-a-database.md)|[Create a User-Defined Data Type Alias](create-a-user-defined-data-type-alias.md)|  
|[Delete a Database](delete-a-database.md)|[Database Snapshots &#40;SQL Server&#41;](database-snapshots-sql-server.md)|  
  
## Related Content  
 [Indexes](../indexes/indexes.md)  
  
 [Views](../views/views.md)  
  
 [Stored Procedures &#40;Database Engine&#41;](../stored-procedures/stored-procedures-database-engine.md)  
  
  
