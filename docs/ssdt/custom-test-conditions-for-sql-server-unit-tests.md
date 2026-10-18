---
title: Custom Test Conditions for SQL Server Unit Tests
description: Find out how to install custom test conditions for SQL Server unit tests. View the risks involved with installing test conditions that you didn't create.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# Custom test conditions for SQL Server unit tests

You can add custom test conditions for SQL Server unit tests. However, you must first install the test condition before you can use it, whether you created the extension or you're installing one that someone else created.

## Limitations

Before you install a test condition that you didn't create, you should understand the following risks:

- The installation program for the test condition might be malicious and gain access to protected resources based on your installation permissions.

- The test condition itself might be malicious and gain control of protected resources if the user who uses the extension has sufficient permissions.

## Remarks

To minimize risk, you should install a custom test condition only if it's from a known source. If you obtain a test condition from an untrusted source, you should inspect the source code for that test condition and its installation program (if it has one) before you install and it.

To install a custom test condition, copy the signed assembly (`.dll`) to the `%ProgramFiles%\Microsoft Visual Studio <Version>\Common7\IDE\Extensions\Microsoft\SQLDB\TestConditions` folder. If this folder doesn't exist, create it. You need administrative privileges on your machine to copy to this directory.

You need to install Visual Studio 2010 and Visual Studio 2012 versions of the test condition if:

- You install custom test conditions on a computer that might be used to build SQL Server unit tests.
- Those unit tests are used in Visual Studio 2010 and Visual Studio 2012.

For more information about custom test conditions for SQL Server unit tests, see:

- [How to: Create Test Conditions for the SQL Server Unit Test Designer](how-to-create-test-conditions-for-the-sql-server-unit-test-designer.md)
- [How to: Upgrade a Visual Studio 2010 Custom Test Condition from a Previous Release to SQL Server Data Tools](how-to-upgrade-visual-studio-2010-custom-test-condition-to-ssdt.md)
- [Walkthrough: Using a Custom Test Condition to Verify the Results of a Stored Procedure](walkthrough-use-custom-test-condition-to-verify-stored-procedure-results.md)

## Related content

- [Verify database code by using SQL Server unit tests](verifying-database-code-by-using-sql-server-unit-tests.md)
