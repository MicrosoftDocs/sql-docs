---
title: Open a SQL Server Unit Test to Edit
description: "Learn how to edit a SQL Server unit test so that you can add functionality or customize conditions. See different ways of opening the test's source code file."
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Open a SQL Server unit test to edit

After you create a SQL Server unit test, you use the **SQL Server Unit Test Designer** to add Transact-SQL statements and test conditions. The tests created by using the designer generate C# or Visual Basic code. This code is what executes when your test runs.

If you're satisfied with your test, you can run it as it's. If you want to add more functionality to this unit test, you can edit its code. This code resides in a `.cs` or `.vb` file in your test project. For more information, see [SQL Server Unit Test Files](sql-server-unit-test-files.md). You can also customize your tests by creating new test conditions. For more information, see [How to: Create Test Conditions for the Database Unit Test Designer (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/aa833409(v=vs.100)).

> [!NOTE]  
> If you delete a test method by editing the `.cs` or `.vb` file, the test method still appears in the **SQL Server Unit Test Designer**. This situation occurs because the InitializeComponent method of the test class still contains member variables for that test. Although the test appears in the designer, you can't run the test because its code is no longer present. To regenerate the test method for this test, edit the Transact-SQL in the editor, and then either save the `.cs` or `.vb` test file or rebuild the test project.

## Open the source code file of a SQL Server unit test from Solution Explorer

In **Solution Explorer**, right-click the source-code file that contains the SQL Server unit test, and then select **View Code**.

The test method of the unit test appears in the main editing window of Visual Studio when the file opens.

## Open the source code file of a SQL Server unit test from the Test View window (Visual Studio 2010)

1. Run a unit test. For more information, see the "Run SQL Server Unit Tests" section in [Walkthrough: Create and run a SQL Server unit test](walkthrough-creating-and-running-a-sql-server-unit-test.md).

1. In the Test View window, right-click the test, and then select **Open Test**.

   The test method of the unit test appears in the main editing window of Visual Studio when the file opens.

## Open the source code file of a SQL Server unit test from the Test View window (Visual Studio 2012)

1. Run a unit test. For more information, see the "Run SQL Server Unit Tests" section in [Walkthrough: Create and run a SQL Server unit test](walkthrough-creating-and-running-a-sql-server-unit-test.md).

1. In the Test Explorer window, select the name of the unit test source code file.

   The test method of the unit test appears in the main editing window of Visual Studio when the file opens.

## Related content

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [Verify database code by using SQL Server unit tests](verifying-database-code-by-using-sql-server-unit-tests.md)
