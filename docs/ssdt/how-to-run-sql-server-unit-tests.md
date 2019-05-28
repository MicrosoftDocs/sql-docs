---
title: "How to: Run SQL Server Unit Tests | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 34fe2d1e-d47b-4808-af56-8cc0fdae6518
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Run SQL Server Unit Tests
You can run a SQL Server unit test in any one of several ways, such as using various windows and the Command Prompt window.  
  
> [!NOTE]  
> You cannot run unit tests remotely.  
  
The ways that are available to you depend on the software that you installed, as described in [Running SQL Server Unit Tests](../ssdt/running-sql-server-unit-tests.md).  
  
### To run SQL Server unit tests using Test View (Visual Studio 2010)  
  
1.  On the **Test** menu, point to **Windows**, and then click **Test View**.  
  
    The **Test View** window opens.  
  
2.  In the **Test View** window, click the test or tests that you want to run. By using the CTRL key or the SHIFT key, you can specify discontinuous or continuous blocks of tests.  
  
3.  Do either of the following:  
  
    -   Right-click the surface of the **Test View** window, and then click **Run Selection**.  
  
    -   On the **Test View** toolbar, click **Run Selection**.  
  
### To run SQL Server unit tests using Test Explorer (Visual Studio 2012)  
  
1.  On the **Test** menu, point to **Windows**, and then click **Test Explorer**.  
  
    The **Test Explorer** window opens.  
  
2.  In the **Test Explorer**, click the test or tests that you want to run. By using the CTRL key or the SHIFT key, you can specify discontinuous or continuous blocks of tests.  
  
3.  Right-click one of the highlighted tests and click **Run Selected Tests**.  
  
### To run SQL Server unit tests from the SQL Server Unit Test Designer (Visual Studio 2010)  
  
-   On the **Test Tools** toolbar, you will find buttons to start a project with or without the debugger.  
  
This step runs all tests in the current test run. As soon as you start a test run, the **Test Results** window appears and displays the progress of the test run. This display includes tests that are running and tests that have completed. For more information, see [Interpreting SQL Server Unit Test Results](../ssdt/interpreting-sql-server-unit-test-results.md).  
  
## See Also  
[Running SQL Server Unit Tests](../ssdt/running-sql-server-unit-tests.md)  
[How to: Run Automated Tests from Microsoft Visual Studio 2010](https://msdn.microsoft.com/library/ms182470(VS.100).aspx)  
[Running Automated Tests from the Command Line (Visual Studio 2010)](https://msdn.microsoft.com/library/ms182486(VS.100).aspx)  
[Testing the Application (Visual Studio 2012)](https://msdn.microsoft.com/library/ms182409.aspx)  
  
