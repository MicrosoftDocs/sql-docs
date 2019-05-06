---
title: "Creating and Defining SQL Server Unit Tests | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.unittesting.DatabaseMethodNameDialog"
  - "sql.data.tools.unittesting.designer"
ms.assetid: 3c082177-a2b1-4fde-8833-b49b2a351815
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# Creating and Defining SQL Server Unit Tests
You can run SQL Server unit tests to verify whether changes to one or more database objects in a schema have broken existing functionality in a database application. These tests complement the unit tests that your software developers create. You must run both kinds of tests to verify the behavior of your application.  
  
You can verify the behavior of any object in your schema by adding a SQL Server unit test and adding a Transact\-SQL script to test that object. As an alternative, you can automatically generate a stub of a Transact\-SQL script if you want to verify the behavior of a particular function, trigger, or stored procedure. After you generate the stub, you must customize it to obtain meaningful results.  
  
> [!NOTE]  
> You can create an empty test, add code to it, and run it without having a SQL Server database project open. However, you cannot automatically generate a Transact\-SQL stub that tests a function, trigger, or stored procedure without opening the project that contains the object that you want to test.  
  
## Common Tasks  
In the following table, you can find descriptions of common tasks that support this scenario and links to more information about how you can successfully complete those tasks.  
  
|Common Tasks|Supporting Content|  
|----------------|----------------------|  
|**Get hands-on practice**: You can follow an introductory walkthrough to become familiar with how to create and run a simple SQL Server unit test.|-   [Walkthrough: Creating and Running a SQL Server Unit Test](../ssdt/walkthrough-creating-and-running-a-sql-server-unit-test.md)|  
|**Learn more about SQL Server unit tests**: You can learn more about the files and scripts that compose a SQL Server unit test. You can also learn about how to use test conditions and Transact\-SQL assertions in your unit tests.|-   [Scripts in SQL Server Unit Tests](../ssdt/scripts-in-sql-server-unit-tests.md)<br />-   [SQL Server Unit Test Files](../ssdt/sql-server-unit-test-files.md)<br />-   [Using Test Conditions in SQL Server Unit Tests](../ssdt/using-test-conditions-in-sql-server-unit-tests.md)<br />-   [Using Transact-SQL Assertions in SQL Server Unit Tests](../ssdt/using-transact-sql-assertions-in-sql-server-unit-tests.md)|  
|**Create one or more test projects**: You must create SQL Server unit tests in a test project. If you create a SQL Server unit test using SQL Server Object Explorer before you create a test project, a test project is created for you. You can create more than one test project if, for example, you want to use different data generation plans or different deployment configurations in different sets of tests. When you create the test project, you can configure test settings (such as the connection string), deployment settings, and a data generation plan to use for that project.|-   [How to: Create a Test Project for SQL Server Database Unit Testing](../ssdt/how-to-create-a-test-project-for-sql-server-database-unit-testing.md)<br />-|  
|**Configure how the unit test is run**: You can specify the connection string to the database against which you run the tests, the data generation plan, and deployment settings. You configure these settings when first adding a SQL Server unit test to the project, but you can also modify them later.|-   [How to: Configure SQL Server Unit Test Execution](../ssdt/how-to-configure-sql-server-unit-test-execution.md)<br />-   [Overview of Connection Strings and Permissions](../ssdt/overview-of-connection-strings-and-permissions.md)|  
|**Create a SQL Server unit test**: You can automatically create Transact\-SQL code stubs for SQL Server unit tests that verify the behavior of a function, a trigger, or a stored procedure. You can also create an empty SQL Server unit test and then add Transact\-SQL code to test other types of database objects.|-   [How to: Create SQL Server Unit Tests for Functions, Triggers, and Stored Procedures](../ssdt/how-to-create-unit-tests-for-functions-triggers-stored-procedures.md)<br />-   [How to: Create an Empty SQL Server Unit Test](../ssdt/how-to-create-an-empty-sql-server-unit-test.md)|  
|**Write code for a SQL Server unit test**: After you create a unit test, you modify or write Transact\-SQL code to test a database object. For each test, you define one or more test conditions that determine whether the test will pass or fail. For more complex tests, you can modify the Visual Basic or Visual C\# code in the database project. For example, you can write a unit test that runs in the scope of a single transaction.|-   [How to: Open a SQL Server Unit Test to Edit](../ssdt/how-to-open-a-sql-server-unit-test-to-edit.md)<br />-   [How to: Add Test Conditions to SQL Server Unit Tests](../ssdt/how-to-add-test-conditions-to-sql-server-unit-tests.md)<br />-   [How to: Write a SQL Server Unit Test that Runs within the Scope of a Single Transaction](../ssdt/how-to-write-sql-server-unit-test-that-runs-in-single-transaction-scope.md)<br />-   [Keyboard Shortcuts for SQL Server Unit Test Designer](../ssdt/keyboard-shortcuts-for-sql-server-unit-test-designer.md)|  
|**Troubleshoot problems**: You can learn more about how to troubleshoot common problems with SQL Server.|-   [Troubleshooting SQL Server Database Unit Testing Issues](../ssdt/troubleshooting-sql-server-database-unit-testing-issues.md)|  
  
## Related Scenarios  
[Running SQL Server Unit Tests](../ssdt/running-sql-server-unit-tests.md)  
After you create your SQL Server unit tests, you can run them from the Test View window, the SQL Server Unit Test Designer, or by using Team Foundation Build.  
  
[Scenario: Define Custom Test Conditions for Database Unit Tests](https://msdn.microsoft.com/library/dd193282(VS.100).aspx)  
You can create a custom test condition to test a behavior that the default test conditions cannot verify.  
  
## See Also  
[Verifying Database Code by Using SQL Server Unit Tests](../ssdt/verifying-database-code-by-using-sql-server-unit-tests.md)  
  
