---
title: "How to: Open a SQL Server Unit Test to Edit | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: c6af1b12-54cd-42f9-b2ef-7164f8078323
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Open a SQL Server Unit Test to Edit
After you create a SQL Server unit test, you use the **SQL Server Unit Test Designer** to add Transact\-SQL statements and test conditions. The tests created by using the designer generate Visual C# or Visual Basic code. This code is what executes when your test runs.  
  
If you are satisfied with your test, you can run it as it is. If you want to add more functionality to this unit test, you can edit its code. This code resides in a .cs or .vb file in your test project. For more information, see [SQL Server Unit Test Files](../ssdt/sql-server-unit-test-files.md). You can also customize your tests by creating new test conditions. For more information, see [How to: Create Test Conditions for the Database Unit Test Designer (Visual Studio 2010)](https://msdn.microsoft.com/library/aa833409(VS.100).aspx).  
  
> [!NOTE]  
> If you delete a test method by editing the .cs or .vb file, the test method still appears in the **SQL Server Unit Test Designer**. This situation occurs because the InitializeComponent method of the test class still contains member variables for that test. Although the test appears in the designer, you cannot run the test because its code is no longer present. To regenerate the test method for this test, edit the Transact\-SQL in the editor, and then either save the .cs or .vb test file or rebuild the test project.  
  
### To open the source code file of a SQL Server unit test from Solution Explorer  
  
-   In **Solution Explorer**, right-click the source-code file that contains the SQL Server unit test, and then click **View Code**.  
  
    The test method of the unit test appears in the main editing window of Visual Studio when the file opens.  
  
### To open the source code file of a SQL Server unit test from the Test View window (Visual Studio 2010)  
  
1.  Run a unit test. For more information, see the "Run SQL Server Unit Tests" section in [Walkthrough: Creating and Running a SQL Server Unit Test](../ssdt/walkthrough-creating-and-running-a-sql-server-unit-test.md).  
  
2.  In the Test View window, right-click the test, and then click **Open Test**.  
  
    The test method of the unit test appears in the main editing window of Visual Studio when the file opens.  
  
### To open the source code file of a SQL Server unit test from the Test View window (Visual Studio 2012)  
  
1.  Run a unit test. For more information, see the "Run SQL Server Unit Tests" section in [Walkthrough: Creating and Running a SQL Server Unit Test](../ssdt/walkthrough-creating-and-running-a-sql-server-unit-test.md).  
  
2.  In the Test Explorer window, click the name of the unit test source code file.  
  
    The test method of the unit test appears in the main editing window of Visual Studio when the file opens.  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
[Verifying Database Code by Using SQL Server Unit Tests](../ssdt/verifying-database-code-by-using-sql-server-unit-tests.md)  
  
