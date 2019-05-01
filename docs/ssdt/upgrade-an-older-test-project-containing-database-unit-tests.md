---
title: "Upgrade an Older Test Project Containing Database Unit Tests | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 42782ff3-e8cf-4c9d-8dac-a95b236edfc4
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# Upgrade an Older Test Project Containing Database Unit Tests
You can upgrade an older test project, which was created in Visual Studio 2010 and that contains database unit tests, to use the new SQL Server Data Tools database unit testing runtime and tools. Once you have upgraded an older project you can add SQL Server unit tests to the project (see [Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md) for more information).  
  
> [!TIP]  
> If you are using Visual Studio 2010, after you add SQL Server unit tests to a test project, you should not add unit tests using the older database unit test template. If you do, you will need to convert the project again before the tests will execute correctly.  
  
If you have a test database project that was created in a release older than Visual Studio 2010, you can use the information in [How to: Upgrade Database Unit Tests from Previous Releases of Visual Studio](https://msdn.microsoft.com/library/dd193412(VS.100).aspx) to upgrade your database project to Visual Studio 2010, before upgrading the project to SQL Server Data Tools.  
  
### Initiating an Upgrade  
  
-   You can start a project upgrade from the context menu for a test project.  
  
    In some cases, SQL Server Data Tools will display a dialog box from which you can initiate a test project upgrade.  
  
-   Upgrading the project removes the assembly reference to the older database testing framework and adds a reference to the new framework and an adapter assembly. The app.config file is also updated.  
  
    > [!NOTE]  
    > If your test project has both a DatabaseSetup and a SQLDatabaseSetup code files, upgrading the project to SQL Server Data Tools will exclude the DatabaseSetup file from the build. You can remove the DatabaseSetup file if it is excluded from the build.  
  
-   After conversion, existing database unit tests created with the older template will use types in the adapter assembly to access the new framework. The use of an adapter assembly means that the upgrade procedure did not modify your test scripts and code. If you add a SQL Server unit test to the project, the new test will reference the new framework directly and not via an adapter. You can choose to manually update your existing code to use the new framework for consistency with new tests, but this is not required.  
  
## See Also  
[Verifying Database Code by Using SQL Server Unit Tests](../ssdt/verifying-database-code-by-using-sql-server-unit-tests.md)  
  
