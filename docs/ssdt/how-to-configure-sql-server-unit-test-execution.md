---
title: Configure SQL Server Unit Test Execution
description: Learn how to configure SQL Server unit tests. See how to specify connection strings and how to deploy a database schema.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: e0179429-13ce-4d23-ae27-e6419de0a575
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: Configure SQL Server Unit Test Execution

By configuring your test project, you can specify several settings that control aspects of how your SQL Server unit tests are run. These configuration settings are stored in your test project's app.config file. If you edit this file directly, the new values appear in the test configuration dialog box.  
  
Your solution can contain multiple test projects. Each test project contains one app.config file (that is, one set of configuration settings). As a result, your solution can contain different sets of unit tests (one set for each test project) that are configured to run differently.  
  
These settings control how your test connects to the database that you will test, how you deploy a schema from a database project to that database:  
  
-   **Database Connections**. You use this setting to specify the connection strings that are used to connect to the database that you are testing. For more information, see [Specify Connection Strings](#SpecifyConnectionStrings)  
  
-   **Schema deployment**. A database project is an offline representation of your database. The database project represents the structure of your database objects but contains no data. After you make schema changes in a database project, you can test them in an actual database. In the schema deployment step, database objects that you want to test are copied from your database project into the database on which you run tests. For more information about schema deployment, see [Deploy a Database Schema](#DeployingDBSchema).  
  
    > [!NOTE]  
    > Tests do not run in the solution folder but in a separate folder on the local hard disk. Although you can configure aspects of test deployment, you typically do not need to configure them for unit tests. For more information about test deployment, see [Running Tests](/previous-versions/visualstudio/visual-studio-2010/dd286680(v=vs.100)).  
  
## <a name="SpecifyConnectionStrings"></a>Specify Connection Strings  
  
#### To specify database connection strings  
  
1.  Right click on the unit test project in **Solution Explorer** and click **SQL Server Test Configuration**.  
  
    The **SQL Server Test Configuration -'\<projectname\>'** dialog box appears.  
  
2.  Under **Database Connections**, you can do the following:  
  
    -   Click the database connection against which you want to execute unit tests.  
  
    -   Select the **Use a secondary data connection to validate unit tests** check box, and click a database connection in the list if you want test execution to be validated against a different database connection.  
  
    -   Click **New Connection** to add a connection to either list. You can also click **Edit Connection** to modify settings on an existing connection.  
  
    This step creates the `ExecutionContext` connection string, which is used to execute the test script in your unit test. If you also specify a secondary connection, the `PrivilegedContext` connection string is also created. This connection is used to test interactions with the database outside the test script in your unit test. For more information, see [Overview of Connection Strings and Permissions](../ssdt/overview-of-connection-strings-and-permissions.md).  
  
3.  Click **OK** to close the **SQL Server Test Configuration -'\<projectname\>'** dialog box.  
  
4.  Rebuild the test project to apply the configuration changes.  
  
## <a name="DeployingDBSchema"></a>Deploy a Database Schema  
  
#### To deploy to a database the schema of a database project  
  
1.  In **Solution Explorer**, right-click your database project, and then click **Build**.  
  
    When you build your database project, you generate a Transact\-SQL script. This script, when it is run against a database, re-creates the structure of your database project in that database.  
  
2.  Select the test project that you want to configure.  
  
3.  Right click on the unit test project in **Solution Explorer** and click **SQL Server Test Configuration**.  
  
    The **SQL Server Test Configuration -'\<projectname\>'** dialog box appears.  
  
4.  Under **Deployment**, you can do the following:  
  
    -   Select the **Automatically deploy database projects before running tests** check box to make sure that any schema changes that you have made to your database project are committed before tests are run.  
  
    -   Under **Database Project**, click the database project that you want to deploy, or click the ellipsis to browse for another project. Database project files have the extension .dbproj.  
  
    -   Under **Deployment Configuration**, click the project configuration against which you want to deploy. Your choices are **Debug**, **Default**, or **Release**. However, if you create a configuration for unit testing, that configuration also appears as an option.  
  
5.  Click **OK** to close the **SQL Server Test Configuration -'\<projectname\>'** dialog box.  
  
    At the start of the test run, the Transact\-SQL script that was generated in step 1 is run. This action deploys the schema to the target database.  
  
6.  Rebuild the unit test project to apply the configuration changes.  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
[Verifying Database Code by Using SQL Server Unit Tests](../ssdt/verifying-database-code-by-using-sql-server-unit-tests.md)  
