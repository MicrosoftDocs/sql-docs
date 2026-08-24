---
title: Add Test Conditions to SQL Server Unit Tests
description: Find out how to add test conditions to a SQL Server unit. See how to use the SQL Server Unit Test Designer for this task.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Add test conditions to SQL Server unit tests

You can add test conditions to a SQL Server unit test by using the **SQL Server Unit Test Designer**. When you save the test class, the test conditions are automatically saved in your test project as C# or Visual Basic code in the source-code file containing the test class. After you save a test condition, you can edit it either in the **SQL Server Unit Test Designer** or in its source-code file.

## Add test conditions to a SQL Server unit test

1. Open a SQL Server unit test in the **SQL Server Unit Test Designer**.

   The name of the test you opened is displayed in the navigation bar at the top of the SQL Server Unit Test Designer. By using the navigation bar, you can select the different test methods that are in your test class.

1. In the navigation bar, select the test method to which you want to add test conditions, or select **Common Scripts**.

   Common scripts don't belong to a particular unit test. Rather, they precede or follow unit tests in the test class. For more information, see [Scripts in SQL Server Unit Tests](scripts-in-sql-server-unit-tests.md).

1. In the navigation bar, select the Transact-SQL script to which you want to add test conditions. You can add test conditions to the pre-test, test, or post-test script.

   The Transact-SQL script for that test appears in the Transact-SQL editor and its test conditions appear in the **Test Conditions** pane.

1. In the **Test Conditions** selection list, select a test condition and then select **Add Test Condition** (+).

   The test condition is added to the unit test method.

   You can reorder test conditions within a test method by selecting a test condition and then selecting the up and down arrows in the **Test Conditions** pane.

1. Select the test condition you just added and view the **Properties** window.

   Configure the test condition in the Properties window. For example, you can change the **Execution Time** property of an Execution Time test condition. If you set this property, you cause your test to fail if the Transact-SQL script doesn't execute within the time that you specified.

## Related content

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [How to: Create an empty SQL Server unit test](how-to-create-an-empty-sql-server-unit-test.md)
- [How to: Create SQL Server unit tests for functions, triggers, and stored procedures](how-to-create-unit-tests-for-functions-triggers-stored-procedures.md)
- [Use test conditions in SQL Server unit tests](using-test-conditions-in-sql-server-unit-tests.md)
- [Scripts in SQL Server unit tests](scripts-in-sql-server-unit-tests.md)
- [Interpret SQL Server unit test results](interpreting-sql-server-unit-test-results.md)
- [How to: Run SQL Server unit tests](how-to-run-sql-server-unit-tests.md)
