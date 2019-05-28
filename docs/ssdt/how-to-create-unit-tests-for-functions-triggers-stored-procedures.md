---
title: "How to: Create SQL Server Unit Tests for Functions, Triggers, and Stored Procedures | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: bda57c10-a1ab-4a1a-8a71-42085a3cb793
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Create SQL Server Unit Tests for Functions, Triggers, and Stored Procedures
You can write unit tests that evaluate changes to any database object. However SQL Server Data Tools includes additional support for creating tests for database functions, triggers, and stored procedures from a database project node in SQL Server Object Explorer. Transact\-SQL code stubs can be automatically generated for you to customize.  
  
### To create a SQL Server unit test from a function, trigger, or stored procedure  
  
1.  See [Walkthrough: Creating and Running a SQL Server Unit Test](../ssdt/walkthrough-creating-and-running-a-sql-server-unit-test.md) for an example of adding a unit test for a stored procedure, in the "To create a SQL Server unit test for the stored procedures" section.  
  
    The Inconclusive test condition is the default condition that is added to every test. This test condition is included to indicate that test verification has not been implemented. Delete this test condition from your test after you add other test conditions. For more information, see [How to: Add Test Conditions to SQL Server Unit Tests](../ssdt/how-to-add-test-conditions-to-sql-server-unit-tests.md).  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
[How to: Create an Empty SQL Server Unit Test](../ssdt/how-to-create-an-empty-sql-server-unit-test.md)  
  
