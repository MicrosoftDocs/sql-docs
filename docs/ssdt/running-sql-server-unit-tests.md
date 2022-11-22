---
title: Running SQL Server Unit Tests
description: Become familiar with SQL Server unit tests. View resources on creating tests, creating custom test conditions, running tests, and interpreting results.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.unittesting.testconfig"
ms.assetid: febcc87f-eb18-4c12-ba30-82ef0d49aaa3
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# Running SQL Server Unit Tests

To improve and maintain the quality of your code, you can create and run SQL Server unit tests that verify the behavior of any database object and then check those tests in to version control. As you or any member of your team changes the database schema, you run both SQL Server unit tests and software unit tests to verify that the changes have not broken existing functionality. You can run individual tests, or you can run groups of tests, which are known as test lists. For more information, see [Using Test Lists (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms182461(v=vs.100)).  
  
## Ways to Run SQL Server Unit Tests  
You can run SQL Server unit tests in several ways that vary based on the software that you have installed, as the following shows:  
  
-   Run tests by using the Visual Studio 2010**Test View** window. For more information, see [How to: Run SQL Server Unit Tests](../ssdt/how-to-run-sql-server-unit-tests.md) and [How to: Run Automated Tests from Microsoft Visual Studio 2010](/previous-versions/visualstudio/visual-studio-2010/ms182470(v=vs.100)). For Visual Studio 2012, see [How to: Run Automated Tests from Microsoft Visual Studio 2012](/previous-versions/ms182470(v=vs.140)).  
  
-   Run tests by using the MSTest.exe command at a command prompt. For more information, see [How to: Run Automated Tests from the Command Line Using MSTest (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms182487(v=vs.100)) or [How to: Run Automated Tests from the Command Line Using MSTest (Visual Studio 2012)](/previous-versions/ms182487(v=vs.140)).  
  
-   Run tests from **Solution Explorer** by running a test project. For more information, see [How to: Run Automated Tests from Microsoft Visual Studio 2010](/previous-versions/visualstudio/visual-studio-2010/ms182470(v=vs.100)) or [How to: Run Automated Tests from Microsoft Visual Studio 2012](/previous-versions/ms182470(v=vs.140)).  
  
-   Re-run tests from the **Tests Results** window. For more information, see [How to: Rerun a Test (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms182472(v=vs.100)).  
  
-   Run individual tests or test lists (Visual Studio 2010) from the **Test List Editor** window. For more information, see [How to: Run Automated Tests from Microsoft Visual Studio 2010](/previous-versions/visualstudio/visual-studio-2010/ms182470(v=vs.100)) or [How to: Run Automated Tests from Microsoft Visual Studio 2012](/previous-versions/ms182470(v=vs.140)).  
  
-   Run tests as you build a project in Team Foundation Build. For more information, see [How to: Configure and Run Scheduled Tests After Building Your Application (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms182465(v=vs.100)) or [How to: Configure and Run Scheduled Tests After Building Your Application (Visual Studio 2012)](/previous-versions/visualstudio/visual-studio-2012/ms182465(v=vs.110)).  
  
You can run your SQL Server unit tests in a particular order by using an ordered test. For more information, see [How to: Create an Ordered Test (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms182631(v=vs.100)) or [How to: Create an Ordered Test (Visual Studio 2012)](/previous-versions/ms182631(v=vs.140)).  
  
## Interpreting Tests Results  
After you run your tests, the **Test Results** window shows which tests have passed or failed. For more information, see [Interpreting SQL Server Unit Test Results](../ssdt/interpreting-sql-server-unit-test-results.md). For more information about how to diagnose an unexpected failure, see [How to: Debug Database Objects](../ssdt/how-to-debug-database-objects.md).  
  
## Topics in this Section  
This section contains the following topics:  
  
-   [How to: Debug Database Objects](../ssdt/how-to-debug-database-objects.md)  
  
-   [How to: Run SQL Server Unit Tests from Team Foundation Build](../ssdt/how-to-run-sql-server-unit-tests-from-team-foundation-build.md)  
  
-   [How to: Run SQL Server Unit Tests](../ssdt/how-to-run-sql-server-unit-tests.md)  
  
-   [Interpreting SQL Server Unit Test Results](../ssdt/interpreting-sql-server-unit-test-results.md)  
  
## Related Scenarios  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
You can define unit tests to verify the behavior of your database objects and associate each test project with a different data generation plan, deployment configuration, and connection string.  
  
[Custom Test Conditions  for SQL Server Unit Tests](../ssdt/custom-test-conditions-for-sql-server-unit-tests.md)  
You can create a custom test condition to test for any condition that you cannot verify by using the default test conditions.  
  
## See Also  
[Verifying Database Code by Using SQL Server Unit Tests](../ssdt/verifying-database-code-by-using-sql-server-unit-tests.md)  
