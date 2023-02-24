---
title: Interpreting SQL Server Unit Test Results
description: Find out how to use the Test Results window or .trx files to view SQL Server unit test results. See how to obtain detailed information on results.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: fde3c95b-2f68-483d-a197-0f7161b72fa3
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# Interpreting SQL Server Unit Test Results

When you run a SQL Server unit test, test results are automatically produced, saved to disk, and summarized in the **Test Results** window. As soon as you start a test run, the **Test Results** window appears and displays the progress of the test run. This display includes tests that are running and tests that have completed.  
  
To see detailed information about a test result, double-click it in the **Test Results** window to display the **Test Results Details** page. For more information about a test result, double-click it.  
  
For more information about how to change the display of the **Test Results** window, see [How to: Add or Remove Columns in Test Windows (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms182508(v=vs.100)) or [How to: Add or Remove Columns in Test Windows (Visual Studio 2012)](/previous-versions/visualstudio/visual-studio-2012/ms182508(v=vs.110)).  
  
## Storing Test Results  
Results of unit tests are automatically stored on your hard disk in files that have the extension .trx. A .trx file is an XML file that contains the details of a test run. You can load .trx files from previous test runs to review the results from those test runs or to re-run earlier tests. For more information, see [How to: Rerun a Test (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms182472(v=vs.100)).  
  
> [!NOTE]  
> You cannot run unit tests remotely.  
  
If your team is using a Visual Studio Team Foundation Server team project to help manage its work, you can also publish your test data to a SQL Server database known as an operational store.  
  
For more information about how to save test results, re-use them, and share them with your team, see [How to: Save and Open Test Results in Visual Studio 2010](/previous-versions/visualstudio/visual-studio-2010/ms404662(v=vs.100)) or [How to: Save and Open Test Results in Visual Studio 2012](/previous-versions/ms404662(v=vs.140)).  
  
## See Also  
[Running SQL Server Unit Tests](../ssdt/running-sql-server-unit-tests.md)  
