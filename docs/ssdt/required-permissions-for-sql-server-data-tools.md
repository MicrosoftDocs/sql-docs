---
title: "Required Permissions for SQL Server Data Tools | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: b27038c4-94ab-449c-90b7-29d87ce37a8b
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Required Permissions for SQL Server Data Tools
Before you can perform an action on a database in Visual Studio, you must log on with an account that has certain permissions on that database. The specific permissions that you need vary based on what action you want to perform. The following sections describe each action that you might want to perform and the specific permission that you need to perform it.  
  
-   [Permissions to Create or Deploy a Database](#DatabaseCreationAndDeploymentPermissions)  
  
-   [Permissions to Refactor a Database](#DatabaseRefactoringPermissions)  
  
-   [Permissions to Perform Unit Tests on a SQL Server Database](#DatabaseUnitTestingPermissions)  
  
-   [Permissions to Generate Data](#DataGenerationPermissions)  
  
-   [Permissions to Compare Schemas and Data](#SchemaAndDataComparePermissions)  
  
-   [Permissions to Run the Transact-SQL Editor](#Transact-SQLEditorPermissions)  
  
-   [Permissions for SQL Server Common Language Run-time (SQL CLR) Projects](#SQLCLRPermissions)  
  
## <a name="DatabaseCreationAndDeploymentPermissions"></a>Permissions to Create or Deploy a Database  
You must have the following permissions to create or deploy a database.  
  
|||  
|-|-|  
|Actions|Required Permissions|  
|Import database objects and settings|You must be able to connect to the source database.<br /><br />If the source database is based on SQL Server 2005, you must also own or have the **VIEW DEFINITION** permission on each object.<br /><br />If the source database is based on SQL Server 2008 or later, you must also own or have the **VIEW DEFINITION** permission on each object. Your login must have the **VIEW SERVER STATE** permission (for database encryption keys).|  
|Import server objects and settings|You must be able to connect to the master database on the specified server.<br /><br />If the server is running SQL Server 2005, you must have the **VIEW ANY DEFINITION** permission on the server.<br /><br />If the source database is based on SQL Server 2008 or later, you must have the **VIEW ANY DEFINITION** permission on the server. Your login must have the **VIEW SERVER STATE** permission (for database encryption keys).|  
|Create or update a database project|You do not require any database permissions to create or modify a database project.|  
|Deploy new database or deploy with **Always Re-create Database** option set|You must either have the **CREATE DATABASE** permission or be a member of the **dbcreator** role on the target server.<br /><br />When you create a database, Visual Studio connects to the model database and copies its contents. The initial login (for example, *yourLogin*) that you use to connect to the target database must have **db_creator** and **CONNECT SQL** permissions. This login must have a user mapping on the model database. If you have **sysadmin** permissions, you can create the mapping by issuing the following Transact\-SQL statements:<br /><br />`USE [model] CREATE USER yourUser FROM LOGIN yourLogin`<br /><br />The user (in this example, yourUser) must have **CONNECT** and **VIEW DEFINITION** permissions on the model database. If you have **sysadmin** permissions, you can grant these permissions by issuing the following Transact\-SQL statements:<br /><br />`USE [model] GRANT CONNECT to yourUser GRANT VIEW DEFINITION TO yourUser`<br /><br />If you deploy a database that contains unnamed constraints and the **CheckNewContraints** option is enabled (it is enabled by default), you must have **db_owner** or **sysadmin** permissions or deployment will fail. This is only true for unnamed constraints. For more information about the **CheckNewConstraints** option, see [Database Project Settings](../ssdt/database-project-settings.md).|  
|Deploy updates to an existing database|You must be a valid database user. You must also be a member of the **db_ddladmin** role, own the schema, or own the objects that you want to create or modify on the target database. You need additional permissions to work with more advanced concepts such as logins or linked servers in your pre-deployment or post-deployment scripts.<br /><br />**NOTE:** If you deploy to the master database, you must also have the **VIEW ANY DEFINITION** permission on the server to which you deploy.|  
|Use an assembly with the EXTERNAL_ACCESS option in a database project|You must set the TRUSTWORTHY property for your database project. You must have the EXTERNAL ACCESS ASSEMBLY permission for your SQL Server login.|  
|Deploy assemblies to a new or existing database|You must be a member of the sysadmin role on the target deployment server.|  
  
For more information, see SQL Server Books Online.  
  
## <a name="DatabaseRefactoringPermissions"></a>Permissions to Refactor a Database  
*Database refactoring* occurs only within the database project. You must have permissions to use the database project. You do not need permissions on a target database until you deploy your changes to it.  
  
## <a name="DatabaseUnitTestingPermissions"></a>Permissions to Perform Unit Testing on a SQL Server Database  
You must have the following permissions to perform unit tests on a database.  
  
|||  
|-|-|  
|Actions|Required Permissions|  
|Execute a test action|You must use the execution context database connection. For more information, see [Overview of Connection Strings and Permissions](../ssdt/overview-of-connection-strings-and-permissions.md).|  
|Execute a pre-test or post-test action|You must use the privileged context database connection. This database connection has more permissions than the execution context connection does.|  
|Run TestInitialize and TestCleanup scripts|You must use the privileged context database connection.|  
|Deploy database changes before you run tests|You must use the privileged context database connection. For more information, see [How to: Configure SQL Server Unit Test Execution](../ssdt/how-to-configure-sql-server-unit-test-execution.md).|  
|Generate data before you run tests|You must use the privileged context database connection. For more information, see [How to: Configure SQL Server Unit Test Execution](../ssdt/how-to-configure-sql-server-unit-test-execution.md).|  
  
## <a name="DataGenerationPermissions"></a>Permissions to Generate Data  
You must have the **INSERT** and **SELECT** permissions on the objects in the target database to generate test data by using Data Generator. If you purge data before you generate data, you must also have **DELETE** permissions on the objects in the target database. To reset the **IDENTITY** column on a table, you must own the table, or you must be a member of the db_owner or db_ddladmin role.  
  
## <a name="SchemaAndDataComparePermissions"></a>Permissions to Compare Schemas and Data  
You must have the following permissions to compare schemas or data.  
  
|||  
|-|-|  
|Actions|Required Permissions|  
|Compare the schemas of two databases|You must have the permissions to import objects and settings from the databases as described in [Permissions to Create or Deploy a Database](#DatabaseCreationAndDeploymentPermissions).|  
|Compare the schemas of a database and a database project|You must have the permissions to import objects and settings from the database as described in [Permissions to Create or Deploy a Database](#DatabaseCreationAndDeploymentPermissions). You must also have the database project open in Visual Studio.|  
|Write updates to a target database|You must have the permissions to deploy updates to the target database as described in [Permissions to Create or Deploy a Database](#DatabaseCreationAndDeploymentPermissions).|  
|Compare the data of two databases|In addition to the permissions that you need to compare the schemas of two databases, you also need the **SELECT** permission on all the tables that you want to compare and **VIEW DATABASE STATE** permission.|  
  
For more information, see SQL Server Books Online.  
  
## <a name="Transact-SQLEditorPermissions"></a>Permissions to Run the Transact\-SQL Editor  
What you can do within the Transact\-SQL editor is determined by your execution context to the target database.  
  
## <a name="SQLCLRPermissions"></a>Permissions for SQL Server Common Language Run-time Projects  
The following table lists the permissions that you must have to deploy or debug CLR projects:  
  
|Actions|Required Permissions|  
|-----------|------------------------|  
|Deploy (initial or incremental) of a safe permission set assembly|db_DDLAdmin - this permission grants CREATE and ALTER permissions for the assemblies and objects types that you deploy<br /><br />database-level VIEW DEFINITION - required in order to deploy<br /><br />database-level CONNECT - grants the ability to connect to the database|  
|Deploy an external_access permission set assembly|db_DDLAdmin - this permission grants CREATE and ALTER permissions for the assemblies and objects types that you deploy<br /><br />database-level VIEW DEFINITION - required in order to deploy<br /><br />database-level CONNECT - grants the ability to connect to the database<br /><br />In addition, you must also have:<br /><br />TRUSTWORTHY database option set to ON<br /><br />The login that you use to deploy must have the External Access Assembly server permission.|  
|Deploy an unsafe permission set assembly|db_DDLAdmin - this permission grants CREATE and ALTER permissions for the assemblies and objects types that you deploy<br /><br />database-level VIEW DEFINITION - required in order to deploy<br /><br />database-level CONNECT - grants the ability to connect to the database<br /><br />In addition, you must also have:<br /><br />TRUSTWORTHY database option set to ON<br /><br />The login that you use to deploy must have the Unsafe Assembly server permission.|  
|Remote debug a SQL CLR assembly|You must have the sysadmin fixed role permission.|  
  
> [!IMPORTANT]  
> In all cases, the assembly owner must be the user that you are using to deploy the assembly or the owner must be a role in which that user is a member. This requirement also applies to any assemblies referenced by the assembly that you deploy.  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
[SQL Server Data Tools](../ssdt/sql-server-data-tools.md)  
  
