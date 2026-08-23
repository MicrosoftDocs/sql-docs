---
title: Using Transact-SQL Assertions in SQL Server Unit Tests
description: Learn about Transact-SQL assertions. See when to use assertions in SQL Server unit tests and when to use test conditions, and view examples of assertion usage.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: concept-article
---

# Use Transact-SQL assertions in SQL Server unit tests

In a SQL Server unit test, a Transact-SQL test script runs and returns a result. Sometimes, the results are returned as a results set. You can validate results by using test conditions. For example, you can use a test condition to check how many rows were returned in a specific result set or to verify how long a particular test took to run. For more information about test conditions, see [Use test conditions in SQL Server unit tests](using-test-conditions-in-sql-server-unit-tests.md).

Instead of using test conditions, you can also use Transact-SQL assertions, which are `THROW` or `RAISERROR` statements in a Transact-SQL script. In certain circumstances, you might prefer to use a Transact-SQL assertion instead of a test condition.

## Use Transact-SQL assertions

You should consider the following points before you decide to validate data either by using Transact-SQL assertions or by using test conditions.

- **Performance**. It's faster to run a Transact-SQL assertion on the server than to first move data to a client computer and manipulate it locally.

- **Familiarity with language**. You might prefer a particular language based on your current expertise and therefore choose Transact-SQL assertions or C# or Visual Basic test conditions.

- **Complicated validation**. In some instances, you can build more complex tests in C# or Visual Basic and validate your tests on the client.

- **Simplicity**. It's often simpler to use a pre-defined test condition than to write the equivalent script in Transact-SQL.

- **Legacy validation libraries**. If you already have code that performs validation, you can use it in a SQL Server unit test instead of using test conditions.

## Mark unit test methods with the expected exception

To mark a SQL Server unit test method with expected exceptions, add the following attribute:

```vb
<ExpectedSqlException(MessageNumber=nnnnn, Severity=x, MatchFirstError=false, State=y)> _
```

```csharp
[ExpectedSqlException(MessageNumber=nnnnn, Severity=x, MatchFirstError=false, State=y)]
```

Where:

- *nnnnn* is the number of the expected message, for example 14025
- *x* is the severity of the expected exception
- *y* is the state of the expected exception

Any unspecified parameters are ignored. You pass these parameters to the `RAISERROR` statement in your database code. If you specify MatchFirstError = true, the attribute matches any of the SqlErrors in the exception. The default behavior (MatchFirstError = true) is to only match the first error that occurs.

For an example of how to use expected exceptions and a negative SQL Server unit test, see [Walkthrough: Create and run a SQL Server unit test](walkthrough-creating-and-running-a-sql-server-unit-test.md).

## The RAISERROR statement

> [!NOTE]  
> Use `THROW` instead of `RAISERROR`. `RAISERROR` is now deprecated.

You can directly use Transact-SQL assertions on the server by using the `RAISERROR` statement in your Transact-SQL script. Its syntax is:

`RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)`

where:

- `@ErrorMessage` is any user-defined error message. You can format this message string similar to the `printf_s` function.
- `@ErrorSeverity` is a user-defined severity level from 0 - 18.

> [!NOTE]  
> The values '0' and '10' for the severity level don't cause the SQL Server unit test to fail. You can use any other value in the range 0 - 18 to cause the test to fail.

`@ErrorState` is an arbitrary integer from 1 - 127. You can use this integer to differentiate between occurrences of a single error that is raised at different locations in the code.

For more information, see [RAISERROR](../t-sql/language-elements/raiserror-transact-sql.md). An example of using `RAISERROR` in a SQL Server unit test is provided in the article, [How to: Write a SQL Server Unit Test that Runs within the Scope of a Single Transaction](how-to-write-sql-server-unit-test-that-runs-in-single-transaction-scope.md).

## Related content

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [Use test conditions in SQL Server unit tests](using-test-conditions-in-sql-server-unit-tests.md)
- [Verify database code by using SQL Server unit tests](verifying-database-code-by-using-sql-server-unit-tests.md)
- [How to: Open a SQL Server unit test to edit](how-to-open-a-sql-server-unit-test-to-edit.md)
