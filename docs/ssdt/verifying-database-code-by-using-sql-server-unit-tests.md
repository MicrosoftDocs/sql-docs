---
title: "Verifying Database Code by Using SQL Server Unit Tests | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 003713e2-de6b-4277-a0a8-7d1f2f4ffb39
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# Verifying Database Code by Using SQL Server Unit Tests
You can use SQL Server unit tests to establish a baseline state for your database and then to verify any subsequent changes that you make to database objects.  
  
To establish a baseline state for a database, you create a test project and write sets of Transact\-SQL that operate on your database objects. By using these tests, you can verify in an isolated development environment whether those objects function as expected. SQL Server unit testing works well in combination with offline database development using SQL Server database projects (see [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) for more information). Once you have a baseline set of SQL Server unit tests, you can use these tests to verify that the database is working correctly before checking in changes to version control.  
  
You can create tests that verify changes to any database object. In addition, you can automatically generate stubs of Transact\-SQL code that test database functions, triggers, and stored procedures.  
  
> [!NOTE]  
> You can create and run SQL Server unit tests without having a database project open. However, if you want to auto-generate test scripts to test specific database objects from your project, you must open the database project that contains the objects that you want to test.  
  
As you or your team members change the database schema, you can use these tests to verify whether the changes have broken existing functionality. You create SQL Server unit tests to complement the software unit tests that your software developers create. You must complete both sets of tests to verify the overall behavior of your application.  
  
Your unit tests can verify that the procedures succeed when they are expected to succeed and that procedures fail when they are expected to fail. Testing that appropriate failures occur is referred to as negative testing.  
  
## Visual Studio Editions' Support for SQL Server Unit Tests  
The SQL Server unit tests feature, which was added in the December 2012 update of SQL Server Data Tools, allows you to create, modify, and run SQL Server unit tests in Visual Studio 2010 Professional and Visual Studio 2012 Professional and higher editions.  
  
To ensure that you install the most recent SQL Server Data Tools update, access the [Check for Updates Dialog Box](../ssdt/check-for-updates-dialog-box.md).  
  
The Visual Studio 2010 and Visual Studio 2012 integrated SQL Server Data Tools shell does not support SQL Server unit tests.  
  
## Common Tasks  
In the following table, you can find descriptions of common tasks that support this scenario and links to more information about how you can successfully complete those tasks.  
  
|Common Tasks|Supporting Content|  
|----------------|----------------------|  
|**Get hands-on practice:** You can follow an introductory walkthrough to become familiar with how to create and run a simple SQL Server unit test. This walkthrough includes an example of a negative SQL Server unit test.|[Walkthrough: Creating and Running a SQL Server Unit Test](../ssdt/walkthrough-creating-and-running-a-sql-server-unit-test.md)|  
|**Define SQL Server unit tests:** You must create SQL Server unit tests in their own project. You configure the settings for that project and define one or more test conditions for each test.|[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)<br /><br />[Using Test Conditions in SQL Server Unit Tests](../ssdt/using-test-conditions-in-sql-server-unit-tests.md)|  
|**Run SQL Server unit tests:** After you define one or more unit tests, you run them, debug any problems, and examine your test results.|[Running SQL Server Unit Tests](../ssdt/running-sql-server-unit-tests.md)|  
|**Manage groups of tests (Visual Studio 2010):** You might organize tests into groups if they should usually be run at the same time. Test lists are still supported but for new groups of tests, you should instead consider test categories. For example, you might create a test category for the tests for your triggers or for all objects in a particular *schema*.|[Defining Test Categories to Group Your Tests](https://msdn.microsoft.com/library/dd286595(VS.100).aspx)<br /><br />[Defining Test Lists to Group Your Tests](https://msdn.microsoft.com/library/dd286584(VS.100).aspx)|  
|**Check your test projects and tests in to version control:** After you run your tests and verify whether they work correctly, you should check your test project and all associated files in to version control so that all members of your team can run your tests. By checking your test project into version control alongside your SQL Server database project, you can easily restore compatible versions of both the database and your database tests.|[Add Files to Version Control](https://msdn.microsoft.com/library/ms181374(VS.100).aspx)<br /><br />[Using the Check In and Pending Changes Windows](https://msdn.microsoft.com/library/ms245462(VS.100).aspx)|  
|**Define custom test conditions:** You can create custom test conditions if you must test for behavior that the default set of test conditions does not cover. You must distribute these conditions to all members of your team who want to run the tests that use the new conditions.|[Scenario: Define Custom Test Conditions for SQL Server Unit Tests](https://msdn.microsoft.com/library/dd193282(VS.100).aspx)|  
|**Update existing unit tests:** If you have database unit tests that were created in a previous version of Visual Studio, you must upgrade them before they will build and run successfully with this release.<br /><br />**NOTE:** If you open a solution that contains both a database project and a database unit test project from a previous version of Visual Studio you will be prompted to upgrade the database project. You will not be prompted to upgrade database unit test projects, which must be upgraded manually.|[Upgrade an Older Test Project Containing Database Unit Tests](../ssdt/upgrade-an-older-test-project-containing-database-unit-tests.md)|  
|**Extensibility:** You can extend SQL Server Data Tools by creating feature extensions.|[Custom Test Conditions  for SQL Server Unit Tests](../ssdt/custom-test-conditions-for-sql-server-unit-tests.md)|  
|**Troubleshoot problems:** You can learn more about how to troubleshoot common problems with SQL Server unit testing.|[Troubleshooting SQL Server Database Unit Testing Issues](../ssdt/troubleshooting-sql-server-database-unit-testing-issues.md)|  
  
## Related Scenarios  
[Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md)  
Database unit testing is particularly effective when used in conjunction with offline project development using SQL Server database projects.  
  
## See Also  
[SQL Server Data Tools](../ssdt/sql-server-data-tools.md)  
  
