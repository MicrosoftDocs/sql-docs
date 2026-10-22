---
title: Configure SQL Server Unit Test Execution
description: Learn how to configure SQL Server unit tests. See how to specify connection strings and how to deploy a database schema.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
ms.custom:
  - sfi-ropc-nochange
---

# How to: Configure SQL Server unit test execution

By configuring your test project, you can specify several settings that control aspects of how your SQL Server unit tests are run. These configuration settings are stored in your test project's app.config file. If you edit this file directly, the new values appear in the test configuration dialog box.

Your solution can contain multiple test projects. Each test project contains one app.config file (that is, one set of configuration settings). As a result, your solution can contain different sets of unit tests (one set for each test project) that are configured to run differently.

These settings control how your test connects to the database that you test, how you deploy a schema from a database project to that database:

- **Database Connections**. You use this setting to specify the connection strings that are used to connect to the database that you're testing. For more information, see [Specify Connection Strings](#SpecifyConnectionStrings)

- **Schema deployment**. A database project is an offline representation of your database. The database project represents the structure of your database objects but contains no data. After you make schema changes in a database project, you can test them in an actual database. In the schema deployment step, database objects that you want to test are copied from your database project into the database on which you run tests. For more information about schema deployment, see [Deploy a Database Schema](#DeployingDBSchema).

  > [!NOTE]  
  > Tests don't run in the solution folder but in a separate folder on the local hard disk. Although you can configure aspects of test deployment, you typically don't need to configure them for unit tests. For more information about test deployment, see [Running Tests](/previous-versions/visualstudio/visual-studio-2010/dd286680(v=vs.100)).

<a id="SpecifyConnectionStrings"></a>

## Specify connection strings

### Specify database connection strings

1. Right-click on the unit test project in **Solution Explorer** and select **SQL Server Test Configuration**.

   The **SQL Server Test Configuration -'\<projectname\>'** dialog box appears.

1. Under **Database Connections**, you can do the following steps:

   - Select the database connection against which you want to execute unit tests.

   - Select the **Use a secondary data connection to validate unit tests** check box, and select a database connection in the list if you want test execution to be validated against a different database connection.

   - Select **New Connection** to add a connection to either list. You can also select **Edit Connection** to modify settings on an existing connection.

   This step creates the `ExecutionContext` connection string, which is used to execute the test script in your unit test. If you also specify a secondary connection, the `PrivilegedContext` connection string is also created. This connection is used to test interactions with the database outside the test script in your unit test. For more information, see [Overview of connection strings and permissions](overview-of-connection-strings-and-permissions.md).

1. Select **OK** to close the **SQL Server Test Configuration -'\<projectname\>'** dialog box.

1. Rebuild the test project to apply the configuration changes.

<a id="DeployingDBSchema"></a>

## Deploy a database schema

### Deploy to a database the schema of a database project

1. In **Solution Explorer**, right-click your database project, and then select **Build**.

   When you build your database project, you generate a Transact-SQL script. This script, when it runs against a database, recreates the structure of your database project in that database.

1. Select the test project that you want to configure.

1. Right-click on the unit test project in **Solution Explorer** and select **SQL Server Test Configuration**.

   The **SQL Server Test Configuration -'\<projectname\>'** dialog box appears.

1. Under **Deployment**, you can do the following steps:

   - Select the **Automatically deploy database projects before running tests** check box to make sure that any schema changes that you have made to your database project are committed before tests are run.

   - Under **Database Project**, select the database project that you want to deploy, or select the ellipsis to browse for another project. Database project files have the extension `.dbproj`.

   - Under **Deployment Configuration**, select the project configuration against which you want to deploy. Your choices are **Debug**, **Default**, or **Release**. However, if you create a configuration for unit testing, that configuration also appears as an option.

1. Select **OK** to close the **SQL Server Test Configuration -'\<projectname\>'** dialog box.

   At the start of the test run, the Transact-SQL script that was generated in step 1 is run. This action deploys the schema to the target database.

1. Rebuild the unit test project to apply the configuration changes.

## Related content

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [Verify database code by using SQL Server unit tests](verifying-database-code-by-using-sql-server-unit-tests.md)
