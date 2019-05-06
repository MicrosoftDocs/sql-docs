---
title: "How to: Create a Test Project for SQL Server Database Unit Testing | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 4b3e7ba8-b565-4689-af1a-34cc255b7c60
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Create a Test Project for SQL Server Database Unit Testing
Before you can start to write unit tests that evaluate database objects, you must first create a test project. This project contains SQL Server unit tests, but it could contain other types of tests.  
  
You can place all of your SQL Server unit tests for a given database project within a single test project. However, you might want to create additional test projects based on your answers to the following questions:  
  
|||  
|-|-|  
|**Question**|**Decision**|  
|Do different SQL Server unit tests need to access different database connections for test execution or test validation?|If yes, you need more than one test project. You cannot specify more than one database connection for test execution. However, you can specify a different database connection for test validation.|  
|Do you want to deploy different database projects for different unit tests?|If yes, you need more than one test project. A test project can only deploy a single database project.|  
  
For more information about each of these questions, see [How to: Configure SQL Server Unit Test Execution](../ssdt/how-to-configure-sql-server-unit-test-execution.md). As an alternative to creating multiple test projects, you can also provide your own [DatabaseTestService](https://msdn.microsoft.com/library/microsoft.data.schema.unittesting.databasetestservice.aspx) Microsoft.Data.Schema.UnitTesting.DatabaseTestService implementation.  
  
You have three options for adding a test project to a solution that contains a database project:  
  
-   Add a test project to the solution. The test project contains a standard unit test, which you can delete. This project does not contain a SQL Server unit test class, which you must add.  
  
-   Add a new SQL Server unit test from the **Test** menu. When you add the unit test, SQL Server Data Tools also creates a test project if you request it. This project contains a SQL Server unit test class. SQL Server unit test classes contain one or more unit tests.  
  
-   Create a unit test from a stored procedure, function, or trigger from an open project in SQL Server Object Explorer. When you create the unit test, SQL Server Data Tools also creates a test project, if you request it. This project contains a SQL Server unit test class. SQL Server test classes contain one or more unit tests.  
  
Each approach is outlined in the following procedures.  
  
### To add a test project to an existing solution  
  
1.  On the **File** menu, point to **New**, and click **Project**.  
  
    The **New Project** dialog box appears.  
  
2.  Under **Installed Templates**, expand the **SQL Server** node, and then select **SQL Server Database Project**.  
  
3.  In **Name**, type a project name.  
  
### To create a test project with a SQL Server unit test class  
  
-   Follow the procedure that is outlined in [How to: Create an Empty SQL Server Unit Test](../ssdt/how-to-create-an-empty-sql-server-unit-test.md) or [How to: Create SQL Server Unit Tests for Functions, Triggers, and Stored Procedures](../ssdt/how-to-create-unit-tests-for-functions-triggers-stored-procedures.md).  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
  
