---
title: "Variables (Transact-SQL)"
description: A Transact-SQL local variable is an object that can hold a single data value of a specific type.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/08/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# Variables (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

A Transact-SQL local variable is an object that can hold a single data value of a specific type. Variables in batches and scripts are typically used:

- As a counter either to count the number of times a loop is performed, or to control how many times the loop is performed.
- To hold a data value to be tested by a control-of-flow statement.
- To save a data value to be returned by a stored procedure return code or function return value.

## Remarks

The names of some Transact-SQL system functions begin with two *at* signs (`@@`). Although in earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the `@@` functions are referred to as global variables, `@@` functions aren't variables, and they don't have the same behaviors as variables. The `@@` functions are system functions, and their syntax usage follows the rules for functions.

You can't use variables in a view.

Changes to variables aren't affected by the rollback of a transaction.

## Declare a Transact-SQL variable

The `DECLARE` statement initializes a Transact-SQL variable by:

- Assigning a name. The name must have a single `@` as the first character.

- Assigning a system-supplied or user-defined data type and a length. For numeric variables, a precision and scale are also assigned. For variables of type XML, an optional schema collection might be assigned.

- Setting the value to `NULL`.

For example, the following `DECLARE` statement creates a local variable named `@mycounter` with an **int** data type. By default, the value for this variable is `NULL`.

```sql
DECLARE @MyCounter INT;
```

To declare more than one local variable, use a comma after the first local variable defined, and then specify the next local variable name and data type.

For example, the following `DECLARE` statement creates three local variables named `@LastName`, `@FirstName` and `@StateProvince`, and initializes each to `NULL`:

```sql
DECLARE @LastName NVARCHAR(30), @FirstName NVARCHAR(20), @StateProvince NCHAR(2);
```

In another example, the following `DECLARE` statement creates a Boolean variable called `@IsActive`, which is declared as **bit** with a value of `0` (`false`):

```sql
DECLARE @IsActive BIT = 0;
```

## Variable scope

The scope of a variable is the range of Transact-SQL statements that can reference the variable. The scope of a variable lasts from the point it's declared until the end of the batch or stored procedure in which it's declared. For example, the following script generates a syntax error because the variable is declared in one batch (separated by the `GO` keyword) and referenced in another:

```sql
USE AdventureWorks2022;
GO

DECLARE @MyVariable INT;

SET @MyVariable = 1;
GO

SELECT BusinessEntityID,
    NationalIDNumber,
    JobTitle
FROM HumanResources.Employee
WHERE BusinessEntityID = @MyVariable;
```

Variables have local scope and are only visible within the batch or procedure where they're defined. In the following example, the nested scope created for execution of `sp_executesql` doesn't have access to the variable declared in the higher scope and returns and error.

```sql
DECLARE @MyVariable INT;
SET @MyVariable = 1;
EXECUTE sp_executesql N'SELECT @MyVariable'; -- this produces an error
```

## Set a value in a Transact-SQL variable

When a variable is first declared, its value is set to `NULL`. To assign a value to a variable, use the `SET` statement. This is the preferred method of assigning a value to a variable. A variable can also have a value assigned by being referenced in the select list of a `SELECT` statement.

To assign a variable a value by using the SET statement, include the variable name and the value to assign to the variable. This is the preferred method of assigning a value to a variable. The following batch, for example, declares two variables, assigns values to them, and then uses them in the `WHERE` clause of a `SELECT` statement:

```sql
USE AdventureWorks2022;
GO

-- Declare two variables.
DECLARE @FirstNameVariable NVARCHAR(50),
    @PostalCodeVariable NVARCHAR(15);

-- Set their values.
SET @FirstNameVariable = N'Amy';
SET @PostalCodeVariable = N'BA5 3HX';

-- Use them in the WHERE clause of a SELECT statement.
SELECT LastName,
    FirstName,
    JobTitle,
    City,
    StateProvinceName,
    CountryRegionName
FROM HumanResources.vEmployee
WHERE FirstName = @FirstNameVariable
    OR PostalCode = @PostalCodeVariable;
GO
```

A variable can also have a value assigned by being referenced in a select list. If a variable is referenced in a select list, it should be assigned a scalar value or the `SELECT` statement should only return one row. For example:

```sql
USE AdventureWorks2022;
GO
DECLARE @EmpIDVariable INT;

SELECT @EmpIDVariable = MAX(EmployeeID)
FROM HumanResources.Employee;
GO
```

> [!WARNING]  
> If there are multiple assignment clauses in a single `SELECT` statement, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] doesn't guarantee the order of evaluation of the expressions. Effects are only visible if there are references among the assignments.

If a `SELECT` statement returns more than one row and the variable references a nonscalar expression, the variable is set to the value returned for the expression in the last row of the result set. For example, in the following batch `@EmpIDVariable` is set to the `BusinessEntityID` value of the last row returned, which is `1`:

```sql
USE AdventureWorks2022;
GO
DECLARE @EmpIDVariable INT;

SELECT @EmpIDVariable = BusinessEntityID
FROM HumanResources.Employee
ORDER BY BusinessEntityID DESC;

SELECT @EmpIDVariable;
GO
```

## Examples

The following script creates a small test table and populates it with 26 rows. The script uses a variable to do three things:

- Control how many rows are inserted by controlling how many times the loop is executed.
- Supply the value inserted into the integer column.
- Function as part of the expression that generates letters to be inserted into the character column.

```sql
-- Create the table.
CREATE TABLE TestTable (cola INT, colb CHAR(3));
GO

SET NOCOUNT ON;
GO

-- Declare the variable to be used.
DECLARE @MyCounter INT;

-- Initialize the variable.
SET @MyCounter = 0;

-- Test the variable to see if the loop is finished.
WHILE (@MyCounter < 26)
BEGIN;
    -- Insert a row into the table.
    INSERT INTO TestTable
    VALUES
        -- Use the variable to provide the integer value
        -- for cola. Also use it to generate a unique letter
        -- for each row. Use the ASCII function to get the
        -- integer value of 'a'. Add @MyCounter. Use CHAR to
        -- convert the sum back to the character @MyCounter
        -- characters after 'a'.
        (
        @MyCounter,
        CHAR((@MyCounter + ASCII('a')))
        );

    -- Increment the variable to count this iteration
    -- of the loop.
    SET @MyCounter = @MyCounter + 1;
END;
GO

SET NOCOUNT OFF;
GO

-- View the data.
SELECT cola, colb FROM TestTable;
GO

DROP TABLE TestTable;
GO
```

## Related content

- [DECLARE @local_variable (Transact-SQL)](declare-local-variable-transact-sql.md)
- [SET @local_variable (Transact-SQL)](set-local-variable-transact-sql.md)
- [SELECT @local_variable (Transact-SQL)](select-local-variable-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Compound Operators (Transact-SQL)](compound-operators-transact-sql.md)
