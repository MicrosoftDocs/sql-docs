---
title: Using Transact-SQL Assertions in SQL Server Unit Tests
description: Learn about Transact-SQL assertions. See when to use assertions in SQL Server unit tests and when to use test conditions, and view examples of assertion usage.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: 55d8be9c-9282-47d3-be7f-e2c26f00c95e
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# Using Transact-SQL Assertions in SQL Server Unit Tests

In a SQL Server unit test, a Transact\-SQL test script runs and returns a result. Sometimes, the results are returned as a results set. You can validate results by using test conditions. For example, you can use a test condition to check how many rows were returned in a specific result set or to verify how long a particular test took to run. For more information about test conditions, see [Using Test Conditions in SQL Server Unit Tests](../ssdt/using-test-conditions-in-sql-server-unit-tests.md).  
  
Instead of using test conditions, you can also use Transact\-SQL assertions, which are THROW or RAISERROR statements in a Transact\-SQL script. In certain circumstances, you might prefer to use a Transact\-SQL assertion instead of a test condition.  
  
## Using Transact-SQL Assertions  
You should consider the following points before you decide to validate data either by using Transact\-SQL assertions or by using test conditions.  
  
-   **Performance**. It is faster to run a Transact\-SQL assertion on the server than to first move data to a client computer and manipulate it locally.  
  
-   **Familiarity with language**. You might prefer a particular language based on your current expertise and therefore choose Transact\-SQL assertions or Visual C\# or Visual Basic test conditions.  
  
-   **Complicated validation**. In some instances, you can build more complex tests in Visual C\# or Visual Basic and validate your tests on the client.  
  
-   **Simplicity**. It is often simpler to use a pre-defined test condition than to write the equivalent script in Transact\-SQL.  
  
-   **Legacy validation libraries**. If you already have code that performs validation, you can use it in a SQL Server unit test instead of using test conditions.  
  
## Mark Unit Test Methods with the Expected Exception  
To mark a SQL Server unit test method with expected exceptions, add the following attribute:  
  
```vb  
<ExpectedSqlException(MessageNumber=nnnnn, Severity=x, MatchFirstError=false, State=y)> _  
```  
  
```csharp  
[ExpectedSqlException(MessageNumber=nnnnn, Severity=x, MatchFirstError=false, State=y)]  
```  
  
Where:  
  
-   *nnnnn* is the number of the expected message, for example 14025  
  
-   *x* is the severity of the expected exception  
  
-   *y* is the state of the expected exception  
  
Any unspecified parameters are ignored. You pass these parameters to the RAISERROR statement in your database code. If you specify MatchFirstError = true, the attribute will match any of the SqlErrors in the exception. The default behavior (MatchFirstError = true) is to only match the first error that occurs.  
  
For an example of how to use expected exceptions and a negative SQL Server unit test, see [Walkthrough: Creating and Running a SQL Server Unit Test](../ssdt/walkthrough-creating-and-running-a-sql-server-unit-test.md).  
  
## The RAISERROR Statement  
  
> [!NOTE]  
> Use THROW instead of RAISERROR. RAISERROR is now deprecated.  
  
You can directly use Transact\-SQL assertions on the server by using the RAISERROR statement in your Transact\-SQL script. Its syntax is:  
  
**RAISERROR (\@ErrorMessage, \@ErrorSeverity, \@ErrorState)**  
  
where:  
  
@ErrorMessage is any user-defined error message. You can format this message string similar to the printf_s function.  
  
@ErrorSeverity is a user-defined severity level from 0 - 18.  
  
> [!NOTE]  
> The values '0' and '10' for the severity level do not cause the SQL Server unit test to fail. You can use any other value in the range 0 - 18 to cause the test to fail.  
  
@ErrorState is an arbitrary integer from 1 - 127. You can use this integer to differentiate between occurrences of a single error that is raised at different locations in the code.  
  
For more information, see [RAISERROR (Transact-SQL)](../t-sql/language-elements/raiserror-transact-sql.md). An example of using RAISERROR in a SQL Server unit test is provided in the topic, [How to: Write a SQL Server Unit Test that Runs within the Scope of a Single Transaction](../ssdt/how-to-write-sql-server-unit-test-that-runs-in-single-transaction-scope.md).  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
[Using Test Conditions in SQL Server Unit Tests](../ssdt/using-test-conditions-in-sql-server-unit-tests.md)  
[Verifying Database Code by Using SQL Server Unit Tests](../ssdt/verifying-database-code-by-using-sql-server-unit-tests.md)  
[How to: Open a SQL Server Unit Test to Edit](../ssdt/how-to-open-a-sql-server-unit-test-to-edit.md)  
