---
title: Create an Empty SQL Server Unit Test
description: Learn how to create SQL Server unit tests. See how to use the same TestInitialize and TestCleanup scripts that other tests use and how to use different scripts.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "sql.data.tools.unittesting.createtest"
---

# How to: Create an empty SQL Server unit test

Include unit tests in your database project to verify changes you make to database objects don't break existing functionality. The following procedures explain how to create SQL Server unit tests for any database object. SQL Server Data Tools includes some extra support for database functions, triggers, and stored procedures. For more information, see [How to: Create SQL Server Unit Tests for Functions, Triggers, and Stored Procedures](how-to-create-unit-tests-for-functions-triggers-stored-procedures.md).

When you create a SQL Server unit test using the first procedure, a test project is automatically created for you if no test project exists. If test projects already exist, you have the option of adding the new test to one of those projects or you can create a new test project. For more information about test projects, see [How to: Create a Test Project for SQL Server Database Unit Testing](how-to-create-a-test-project-for-sql-server-database-unit-testing.md).

You have two options for creating a SQL Server unit test:

- Create a new SQL Server unit test inside a new test class.

  All SQL Server unit tests within a given test class use the same TestInitialize and TestCleanup scripts. Create a new test class if you want your unit test to use different TestInitialize and TestCleanup scripts than other unit tests. For more information, see [Scripts in SQL Server Unit Tests](scripts-in-sql-server-unit-tests.md).

- Create a new SQL Server unit test inside an existing test class.

  Choose this option if your unit test uses the same TestInitialize and TestCleanup scripts as other unit tests within the class.

## Create a SQL Server unit test inside a new test class

1. On the **Test** menu, select **New Test**.

   The **Add New Test** dialog box appears.

1. Under **Templates**, select **SQL Server Unit Test**.

1. Under **Test Name**, enter a name for the test.

1. Under **Add to Test Project**, select an existing test project, into which to add this test. If no test project exists or if you want to create a new test project, select **Create a new \<language\> test project**.

1. Select **OK**.

   If your test project is new, the **New Test Project** dialog box appears. Name the project and select **OK**.

   If your test project is new or hasn't been configured, the **SQL Server Test Configuration \<ProjectName\>** dialog box appears. This dialog box allows you to configure the following information for your test project:

   - The database connection used to execute tests.

   - The database connection used to validate test results, deploy a database, and generate data.

   - The automatic deployment of the database project and any associated schema changes to a given project configuration before you run unit tests.

   For more information, see [How to: Configure SQL Server unit test execution](how-to-configure-sql-server-unit-test-execution.md).

1. Provide project configuration information and select **OK**.

   -or-

   Select **Cancel** to create the unit test without configuring the test project.

   Your blank test appears in the **SQL Server Unit TestDesigner**. Depending on the language you specified for creating the test project, a Visual Basic or C# source code file is added to the test project. This file contains the SQL Server unit test class that SQL Server Data Tools generates for the unit test you created. This test class can contain one or more unit tests that you can add through the SQL Server Unit Test Designer or through code as new test methods in the test class.

   You can also add more tests by:

   - Right-clicking on a test project in **Solution Explorer**, selecting **Add**, **New Test**, and then **SQL Server Unit Test**.
   - In SQL Server Object Explorer, select **Create Unit Tests**.

   When you select this file in **Solution Explorer**, it's displayed in the SQL Server Unit Test Designer, by default. To view the code or to customize it to add more functionality to your unit tests, select the file, right-click, and choose **View Code**.

### Create a SQL Server unit test inside an existing test class

1. Open an existing SQL Server unit test class in the **SQL Server Unit Test Designer**. You can access the **SQL Server Unit Test Designer** by double-clicking the unit test source code file in **Solution Explorer**.

1. Select the plus (**+**) sign in the navigation bar to display the **Specify a unit test name** dialog box.

1. Type a name and select **OK**.

   Your new SQL Server unit test is available in the dropdown list in the navigation bar. It's also added as a new test method in the test class. To view the test method in code, select the class file, right-click, and choose **View Code**. The name of the current test class file is displayed on the tab at the top of the **SQL Server Unit Test Designer**.

After you have configured the test project and created the unit test, the next steps are:

- Add a Transact-SQL test script.
- Define pre-test and post-test actions.
- Add test conditions or other assert statement to verify the script results.

> [!NOTE]  
> The Inconclusive test condition is the default condition added to every test. This test condition is included to indicate that test verification hasn't been implemented. Delete this test condition from your test after you have added other test conditions. For more information, see [How to: Add Test Conditions to Database Unit Tests](/previous-versions/visualstudio/visual-studio-2010/aa833242(v=vs.100)).

## Related content

- [How to: Run SQL Server unit tests](how-to-run-sql-server-unit-tests.md)
- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [Creating Unit Tests](/previous-versions/visualstudio/visual-studio-2008/ms182523(v=vs.90))
