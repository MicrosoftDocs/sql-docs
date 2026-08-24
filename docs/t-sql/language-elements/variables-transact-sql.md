---
title: "Variables (Transact-SQL)"
description: A Transact-SQL local variable is an object that can hold a single data value of a specific type.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/28/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Variables (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

A Transact-SQL local variable is an object that can hold a single data value of a specific type. You typically use variables in batches and scripts for the following purposes:

- Use a variable as a counter to count the number of times a loop is performed, or to control how many times the loop is performed.
- Hold a data value to test by a control-of-flow statement.
- Save a data value to return by a stored procedure return code or function return value.

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## Remarks

The names of some Transact-SQL system functions begin with two *at* signs (`@@`). Although earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] refer to the `@@` functions as global variables, `@@` functions aren't variables, and they don't have the same behaviors as variables. The `@@` functions are system functions, and their syntax usage follows the rules for functions.

You can't use variables in a view.

Changes to variables aren't affected by the rollback of a transaction.

## Declare a Transact-SQL variable

Use the `DECLARE` statement to initialize a Transact-SQL variable by:

- Assigning a name. The name must start with a single `@` character.

- Assigning a system-supplied or user-defined data type and a length. For numeric variables, you can assign a precision and optional scale. For variables of type XML, you can optionally assign a schema collection.

- Setting the value to `NULL`.

For example, the following `DECLARE` statement creates a local variable named `@mycounter` with an **int** data type. By default, the value for this variable is `NULL`.

```sql
DECLARE @MyCounter AS INT;
```

To declare more than one local variable, use a comma after the first local variable definition, and then specify the next local variable name and data type.

For example, the following `DECLARE` statement creates three local variables named `@LastName`, `@FirstName`, and `@StateProvince`, and initializes each to `NULL`:

```sql
DECLARE @LastName AS NVARCHAR (30),
        @FirstName AS NVARCHAR (20),
        @StateProvince AS NCHAR (2);
```

In another example, the following `DECLARE` statement creates a Boolean variable called `@IsActive`, which is declared as **bit** with a value of `0` (`false`):

```sql
DECLARE @IsActive AS BIT = 0;
```

## Variable scope

The scope of a variable is the range of Transact-SQL statements that can reference the variable. The scope of a variable lasts from the point it's declared until the end of the batch or stored procedure in which it's declared. For example, the following script generates a syntax error because the variable is declared in one batch (separated by the `GO` keyword) and referenced in another:

```sql
USE AdventureWorks2025;
GO

DECLARE @MyVariable AS INT;
SET @MyVariable = 1;
GO

SELECT BusinessEntityID,
       NationalIDNumber,
       JobTitle
FROM HumanResources.Employee
WHERE BusinessEntityID = @MyVariable;
```

Variables have local scope and are only visible within the batch or procedure where you define them. In the following example, the nested scope created for execution of `sp_executesql` doesn't have access to the variable declared in the higher scope and returns an error.

```sql
DECLARE @MyVariable AS INT;
SET @MyVariable = 1;

EXECUTE sp_executesql N'SELECT @MyVariable';
```

This query produces the following error:

```output
Msg 137, Level 15, State 2, Line 1
Must declare the scalar variable "@MyVariable".
```

## Set a value in a Transact-SQL variable

When you first declare a variable, its value is `NULL`. To assign a value to a variable, use the `SET` statement. This method is the preferred way to assign a value to a variable. You can also assign a value to a variable by referencing it in the select list of a `SELECT` statement.

To assign a variable a value by using the `SET` statement, include the variable name and the value to assign to the variable. This method is the preferred way to assign a value to a variable. The following batch, for example, declares two variables, assigns values to them, and then uses them in the `WHERE` clause of a `SELECT` statement:

```sql
USE AdventureWorks2025;
GO

-- Declare two variables.
DECLARE @FirstNameVariable AS NVARCHAR (50),
        @PostalCodeVariable AS NVARCHAR (15);

-- Set their values.
SET @FirstNameVariable = N'Amy';
SET @PostalCodeVariable = N'BA5 3HX';

-- Use them in the WHERE clause of a SELECT statement.
SELECT LastName,
       FirstName,
       JobTitle,
       City,
       PostalCode,
       StateProvinceName,
       CountryRegionName
FROM HumanResources.vEmployee
WHERE FirstName = @FirstNameVariable
      OR PostalCode = @PostalCodeVariable;
```

You can also assign a value to a variable by referencing it in a select list. If you reference a variable in a select list, assign it a scalar value or ensure the `SELECT` statement returns only one row. For example:

```sql
USE AdventureWorks2025;
GO

DECLARE @EmpIDVariable AS INT;

SELECT @EmpIDVariable = MAX(BusinessEntityID)
FROM HumanResources.Employee;
```

> [!WARNING]  
> If there are multiple assignment clauses in a single `SELECT` statement, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] doesn't guarantee the order of evaluation of the expressions. Effects are only visible if there are references among the assignments.

If a `SELECT` statement returns more than one row and the variable references a nonscalar expression, the variable is set to the value returned for the expression in the last row of the result set. For example, in the following batch `@EmpIDVariable` is set to the `BusinessEntityID` value of the last row returned, which is `1`:

```sql
USE AdventureWorks2025;
GO

DECLARE @EmpIDVariable AS INT;

SELECT @EmpIDVariable = BusinessEntityID
FROM HumanResources.Employee
ORDER BY BusinessEntityID DESC;

SELECT @EmpIDVariable;
```

## Examples

The following script creates a small test table and populates it with 26 rows. The script uses a variable to do three things:

- Control how many rows are inserted by controlling how many times the loop is executed.
- Supply the value inserted into the integer column.
- Function as part of the expression that generates letters to insert into the character column.

```sql
-- Create the table.
CREATE TABLE TestTable
(
    cola INT,
    colb CHAR (3)
);

SET NOCOUNT ON;

-- Declare the variable to be used.
DECLARE @MyCounter AS INT;

-- Initialize the variable.
SET @MyCounter = 0;

-- Test the variable to see if the loop is finished.
WHILE (@MyCounter < 26)
    -- Insert a row into the table.
    BEGIN
        INSERT INTO TestTable
        -- Use the variable to provide the integer value
        -- for cola. Also use it to generate a unique letter
        -- for each row. Use the ASCII function to get the
        -- integer value of 'a'. Add @MyCounter. Use CHAR to
        -- convert the sum back to the character @MyCounter
        -- characters after 'a'.
        VALUES (
            @MyCounter,
            CHAR((@MyCounter + ASCII('a')))
        );
        -- Increment the variable to count this iteration
        -- of the loop.
        SET @MyCounter = @MyCounter + 1;
    END
SET NOCOUNT OFF;

-- View the data.
SELECT cola,
       colb
FROM TestTable;
DROP TABLE TestTable;
```

## Related content

- [DECLARE @local_variable (Transact-SQL)](declare-local-variable-transact-sql.md)
- [SET @local_variable (Transact-SQL)](set-local-variable-transact-sql.md)
- [SELECT @local_variable (Transact-SQL)](select-local-variable-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Compound operators (Transact-SQL)](compound-operators-transact-sql.md)
