---
title: "How to: Add Test Conditions to SQL Server Unit Tests | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 85ba2e56-a0b2-489c-aea2-fb135cce0cfc
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Add Test Conditions to SQL Server Unit Tests
You can add test conditions to a SQL Server unit test by using the **SQL Server Unit Test Designer**. When you save the test class, the test conditions are automatically saved in your test project as Visual C\# or Visual Basic code in the source-code file containing the test class. After you save a test condition, you can edit it either in the **SQL Server Unit Test Designer** or in its source-code file.  
  
### To add test conditions to a SQL Server unit test  
  
1.  Open a SQL Server unit test in the **SQL Server Unit Test Designer**.  
  
    The name of the test you opened is displayed in the navigation bar at the top of the SQL Server Unit Test Designer. By using the navigation bar, you can select the different test methods that are in your test class.  
  
2.  In the navigation bar, click the test method to which you want to add test conditions, or click **Common Scripts**.  
  
    > [!NOTE]  
    > Common scripts do not belong to a particular unit test. Rather, they precede or follow unit tests in the test class. For more information, see [Scripts in SQL Server Unit Tests](../ssdt/scripts-in-sql-server-unit-tests.md).  
  
3.  In the navigation bar, click the Transact\-SQL script to which you want to add test conditions. You can add test conditions to the pre-test, test, or post-test script.  
  
    The Transact\-SQL script for that test appears in the Transact\-SQL editor and its test conditions appear in the **Test Conditions** pane.  
  
4.  In the **Test Conditions** selection list, click a test condition and then click **Add Test Condition** (+).  
  
    The test condition is added to the unit test method.  
  
    > [!NOTE]  
    > You can reorder test conditions within a test method by clicking a test condition and then clicking the up and down arrows in the **Test Conditions** pane.  
  
5.  Select the test condition you just added and view the **Properties** window.  
  
    Configure the test condition in the Properties window. For example, you can change the **Execution Time** property of an Execution Time test condition. If you set this property, you cause your test to fail if the Transact\-SQL script does not execute within the time that you specified.  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
[How to: Create an Empty SQL Server Unit Test](../ssdt/how-to-create-an-empty-sql-server-unit-test.md)  
[How to: Create SQL Server Unit Tests for Functions, Triggers, and Stored Procedures](../ssdt/how-to-create-unit-tests-for-functions-triggers-stored-procedures.md)  
[Using Test Conditions in SQL Server Unit Tests](../ssdt/using-test-conditions-in-sql-server-unit-tests.md)  
[Scripts in SQL Server Unit Tests](../ssdt/scripts-in-sql-server-unit-tests.md)  
[Interpreting SQL Server Unit Test Results](../ssdt/interpreting-sql-server-unit-test-results.md)  
[How to: Run SQL Server Unit Tests](../ssdt/how-to-run-sql-server-unit-tests.md)  
  
