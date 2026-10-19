---
title: Run SQL Server Unit Tests
description: Find out how to run SQL Server unit tests. See steps for running tests from various windows and tools in different versions of Visual Studio.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Run SQL Server unit tests

You can run a SQL Server unit test in any one of several ways, such as using various windows and the Command Prompt window.

> [!NOTE]  
> You can't run unit tests remotely.

The ways that are available to you depend on the software that you installed, as described in [Run SQL Server unit tests](running-sql-server-unit-tests.md).

## Run SQL Server unit tests using Test View (Visual Studio 2010)

1. On the **Test** menu, point to **Windows**, and then select **Test View**.

   The **Test View** window opens.

1. In the **Test View** window, select the test or tests that you want to run. By using the <kbd>Ctrl</kbd> key or the <kbd>Shift</kbd> key, you can specify discontinuous or continuous blocks of tests.

1. Do either of the following steps:

   - Right-click the surface of the **Test View** window, and then select **Run Selection**.
   - On the **Test View** toolbar, select **Run Selection**.

## Run SQL Server unit tests using Test Explorer (Visual Studio 2012)

1. On the **Test** menu, point to **Windows**, and then select **Test Explorer**.

   The **Test Explorer** window opens.

1. In the **Test Explorer**, select the test or tests that you want to run. By using the CTRL key or the SHIFT key, you can specify discontinuous or continuous blocks of tests.

1. Right-click one of the highlighted tests and select **Run Selected Tests**.

## Run SQL Server unit tests from the SQL Server Unit Test Designer (Visual Studio 2010)

On the **Test Tools** toolbar, you find buttons to start a project with or without the debugger.

This step runs all tests in the current test run. As soon as you start a test run, the **Test Results** window appears and displays the progress of the test run. This display includes tests that are running and tests that have completed. For more information, see [Interpret SQL Server unit test results](interpreting-sql-server-unit-test-results.md).

## Related content

- [Run SQL Server unit tests](running-sql-server-unit-tests.md)
- [How to: Run Automated Tests from Microsoft Visual Studio 2010](/previous-versions/visualstudio/visual-studio-2010/ms182470(v=vs.100))
- [Running Automated Tests from the Command Line (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms182486(v=vs.100))
- [Testing the Application (Visual Studio 2012)](/azure/devops/test/overview)
