---
title: SQL Server Unit Test Files
description: Learn about the files that make up a SQL Server unit test, such as the source code file, the resource file, the configuration file, and the setup file.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: concept-article
---

# SQL Server unit test files

Like the unit tests for managed code, SQL Server unit tests reside in test projects. You can see the items that compose a SQL Server unit test in the hierarchy of a test project in **Solution Explorer**.

A SQL Server unit test consists of multiple items that are contained in several files. The following table describes the files that interact to form a SQL Server unit test.

| File | Description |
| --- | --- |
| `.cs` or `.vb` | This source-code file contains a class that is decorated with the [TestClass] attribute. This class contains a single test method for each of the contained SQL Server unit tests. These methods are decorated with the [TestMethod] attribute.<br /><br />Each test method contains the appropriate code to exercise the Transact-SQL test script. This code is generated when the test methods are created, and you can modify it.<br /><br />**NOTE:** If you double-click this file in **Solution Explorer**, the test class opens in the SQL Server Unit Test Designer. To open the `.cs` or `.vb` file to see its source code, right-click the file in **Solution Explorer**, and then select **View Code**. |
| `.resx` | This resource file contains the Transact-SQL scripts for all tests in the associated `.cs` or `.vb` file. This group of scripts includes the pre-test script, the test script, and the post-test script. The resource file contains XML, which you can edit. The resource file is compiled into the test assembly.<br /><br />You should code your Transact-SQL scripts by using the **SQL Server Unit Test Designer**. For more information about the scripts that are used in SQL Server unit tests, see [Scripts in SQL Server Unit Tests](scripts-in-sql-server-unit-tests.md). |
| `app.config` | This file stores the database connection strings for the test project, in addition to other SQL Server unit test configuration settings such as command timeout. For more information, see [Scripts in SQL Server Unit Tests](scripts-in-sql-server-unit-tests.md). |
| `SQLDatabaseSetup.cs` or `SQLDatabaseSetup.vb` | This file contains a class that prepares the test environment for all SQL Server unit tests in the test project. Based on the configuration settings in the app.config file, it might deploy a SQL Server Database Project to the test database. |

## Related content

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [Verify database code by using SQL Server unit tests](verifying-database-code-by-using-sql-server-unit-tests.md)
- [Scripts in SQL Server unit tests](scripts-in-sql-server-unit-tests.md)
