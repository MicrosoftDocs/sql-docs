---
title: Required Permissions for SQL Server Data Tools
description: Learn about the permissions that you need for various tasks in SQL Server Data Tools, such as creating a database, running unit tests, and comparing schemas.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: concept-article
---

# Required permissions for SQL Server Data Tools

Before you can perform an action on a database in Visual Studio, you must sign in with an account that has certain permissions on that database. The specific permissions that you need vary based on what action you want to perform. The following sections describe each action that you might want to perform and the permissions that you need to perform it.

<a id="DatabaseCreationAndDeploymentPermissions"></a>

## Permissions to create or deploy a database

You must have the following permissions to create or deploy a database.

| Action | Required permissions |
| --- | --- |
| Import database objects and settings | You must be able to connect to the source database.<br /><br />If the source database is based on SQL Server 2005, you must also own or have the `VIEW DEFINITION` permission on each object.<br /><br />If the source database is based on SQL Server 2008 or later, you must also own or have the `VIEW DEFINITION` permission on each object. Your login must have the `VIEW SERVER STATE` permission (for database encryption keys). |
| Import server objects and settings | You must be able to connect to the primary database on the specified server.<br /><br />If the server is running SQL Server 2005, you must have the `VIEW ANY DEFINITION` permission on the server.<br /><br />If the source database is based on SQL Server 2008 or later, you must have the `VIEW ANY DEFINITION` permission on the server. Your login must have the `VIEW SERVER STATE` permission (for database encryption keys). |
| Create or update a database project | You don't need any database permissions to create or modify a database project. |
| Deploy a new database or deploy with the **Always Re-create Database** option set | You must either have the `CREATE DATABASE` permission or be a member of the **dbcreator** role on the target server.<br /><br />When you create a database, Visual Studio connects to the `model` database and copies its contents. The initial login (for example, *yourLogin*) that you use to connect to the target database must have **db_creator** and `CONNECT SQL` permissions. This login must have a user mapping on the `model` database. If you have **sysadmin** permissions, you can create the mapping by issuing the following Transact-SQL statement:<br /><br />`USE [model] CREATE USER yourUser FROM LOGIN yourLogin.`<br /><br />The user (in this example, *yourUser*) must have `CONNECT` and `VIEW DEFINITION` permissions on the `model` database. If you have **sysadmin** permissions, you can grant these permissions by issuing the following Transact-SQL statement:<br /><br />`USE [model] GRANT CONNECT to yourUser GRANT VIEW DEFINITION TO yourUser`.<br /><br />If you deploy a database that contains unnamed constraints and the **CheckNewConstraints** option is enabled, you must have **db_owner** or **sysadmin** permissions or deployment fails. (**CheckNewConstraints** is enabled by default.) This requirement is true only for unnamed constraints. |
| Deploy updates to an existing database | You must be a valid database user. You must also be a member of the **db_ddladmin** role, own the schema, or own the objects that you want to create or modify on the target database. You need additional permissions to work with more advanced concepts such as logins or linked servers in your pre-deployment or post-deployment scripts.<br /><br />If you deploy to the primary database, you must also have the `VIEW ANY DEFINITION` permission on the server to which you deploy. |
| Use an assembly with the `EXTERNAL_ACCESS` option in a database project | You must set the `TRUSTWORTHY` property for your database project. You must have the `EXTERNAL ACCESS ASSEMBLY` permission for your SQL Server login. |
| Deploy assemblies to a new or existing database | You must be a member of the **sysadmin** role on the target deployment server. |

<a id="DatabaseRefactoringPermissions"></a>

## Permissions to refactor a database

Database refactoring occurs only within the database project. You must have permissions to use the database project. You don't need permissions on a target database until you deploy your changes to it.

<a id="DatabaseUnitTestingPermissions"></a>

## Permissions to perform unit tests on a SQL Server database

You must have the following permissions to perform unit tests on a database.

| Action | Required permissions |
| --- | --- |
| Execute a test action | You must use the execution context database connection. For more information, see [Overview of connection strings and permissions](overview-of-connection-strings-and-permissions.md). |
| Execute a pre-test or post-test action | You must use the privileged context database connection. This database connection has more permissions than the execution context connection does. |
| Run `TestInitialize` and `TestCleanup` scripts | You must use the privileged context database connection. |
| Deploy database changes before you run tests | You must use the privileged context database connection. For more information, see [How to: Configure SQL Server unit test execution](how-to-configure-sql-server-unit-test-execution.md). |
| Generate data before you run tests | You must use the privileged context database connection. For more information, see [How to: Configure SQL Server unit test execution](how-to-configure-sql-server-unit-test-execution.md). |

<a id="DataGenerationPermissions"></a>

## Permissions to generate data

To generate test data by using Data Generator, you must have `INSERT` and `SELECT` permissions on the objects in the target database. If you delete data before you generate data, you must also have `DELETE` permissions on the objects in the target database. To reset the `IDENTITY` column on a table, you must own the table, or you must be a member of the **db_owner** or **db_ddladmin** role.

<a id="SchemaAndDataComparePermissions"></a>

## Permissions to compare schemas and data

You must have the following permissions to compare schemas or data.

| Action | Required permissions |
| --- | --- |
| Compare the schemas of two databases | You must have permissions to import objects and settings from the databases, as described in [Permissions to create or deploy a database](#DatabaseCreationAndDeploymentPermissions). |
| Compare the schemas of a database and a database project | You must have permissions to import objects and settings from the database, as described in [Permissions to create or deploy a database](#DatabaseCreationAndDeploymentPermissions). You must also have the database project open in Visual Studio. |
| Write updates to a target database | You must have permissions to deploy updates to the target database, as described in [Permissions to Create or Deploy a Database](#DatabaseCreationAndDeploymentPermissions). |
| Compare the data of two databases | In addition to the permissions that you need to compare the schemas of two databases, you need the `SELECT` permission on all the tables that you want to compare. You also need the `VIEW DATABASE STATE` permission. |

<a id="Transact-SQLEditorPermissions"></a>

## Permissions to run the Transact-SQL editor

Your execution context to the target database determines what you can do within the Transact-SQL editor.

<a id="SQLCLRPermissions"></a>

## Permissions for SQL Server CLR projects

The following table lists the permissions that you must have to deploy or debug Common Language Runtime (CLR) projects.

| Action | Required permissions |
| --- | --- |
| Deploy (initially or incrementally) a safe permission set assembly | The **db_DDLAdmin** permission grants `CREATE` and `ALTER` permissions for the assemblies and objects types that you deploy.<br /><br />The database-level `VIEW DEFINITION` permission is required for deployment.<br /><br />The database-level `CONNECT` permission grants the ability to connect to the database. |
| Deploy a permission set assembly for external access | The **db_DDLAdmin** permission grants `CREATE` and `ALTER` permissions for the assemblies and object types that you deploy.<br /><br />The database-level `VIEW DEFINITION` permission is required for deployment.<br /><br />The database-level `CONNECT` permission grants the ability to connect to the database.<br /><br />You must also have the `TRUSTWORTHY` database option set to `ON`.<br /><br />The login that you use to deploy must have the **External Access Assembly** server permission. |
| Deploy an unsafe permission set assembly | The **db_DDLAdmin** permission grants `CREATE` and `ALTER` permissions for the assemblies and object types that you deploy.<br /><br />The database-level `VIEW DEFINITION` permission is required for deployment.<br /><br />The database-level `CONNECT` permission grants the ability to connect to the database.<br /><br />You must also have the `TRUSTWORTHY` database option set to `ON`.<br /><br />The login that you use to deploy must have the **Unsafe Assembly** server permission. |
| Remotely debug a SQL CLR assembly | You must have the **sysadmin** fixed role permission. |

> [!IMPORTANT]  
> In all cases, the assembly owner must be the user that you're using to deploy the assembly, or the owner must be a role in which that user is a member. This requirement also applies to any assemblies referenced by the assembly that you deploy.

## Related content

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [SQL Server Data Tools](sql-server-data-tools.md)
