---
title: "Custom Test Conditions  for SQL Server Unit Tests | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 32a15d61-e908-4ae1-a238-4fd0f988d8c8
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Custom Test Conditions  for SQL Server Unit Tests
You can add custom test conditions for SQL Server unit tests. However, you must first install the test condition before you can use it, whether you created the extension or you are installing one that someone else created.  
  
Before you install a test condition that you did not create, you should understand the following risks:  
  
-   The installation program for the test condition might be malicious and gain access to protected resources based on your installation permissions.  
  
-   The test condition itself might be malicious and gain control of protected resources if the user who uses the extension has sufficient permissions.  
  
To minimize risk, you should install a custom test condition only if it is from a known source. If you obtain a test condition from an untrusted source, you should inspect the source code for that test condition and its installation program (if it has one) before you install and it.  
  
To install a custom test condition, copy the signed assembly (.dll) to the %Program Files%\Microsoft Visual Studio <Version>\Common7\IDE\Extensions\Microsoft\SQLDB\TestConditions folder. If this folder does not exist, create it. You need administrative privileges on your machine to copy to this directory.  
  
> [!NOTE]  
> You need to install Visual Studio 2010 and Visual Studio 2012 versions of the test condition if,  
>   
> -   You install custom test conditions on a computer that may be used to build SQL Server unit tests.  
> -   Those unit tests are used in Visual Studio 2010 and Visual Studio 2012.  
  
For more information about custom test conditions for SQL Server unit tests, see:  
  
-   [How to: Create Test Conditions for the SQL Server Unit Test Designer](../ssdt/how-to-create-test-conditions-for-the-sql-server-unit-test-designer.md)  
  
-   [How to: Upgrade a Visual Studio 2010 Custom Test Condition from a Previous Release to SQL Server Data Tools](../ssdt/how-to-upgrade-visual-studio-2010-custom-test-condition-to-ssdt.md)  
  
-   [Walkthrough: Using a Custom Test Condition to Verify the Results of a Stored Procedure](../ssdt/walkthrough-use-custom-test-condition-to-verify-stored-procedure-results.md)  
  
## See Also  
[Verifying Database Code by Using SQL Server Unit Tests](../ssdt/verifying-database-code-by-using-sql-server-unit-tests.md)  
  
