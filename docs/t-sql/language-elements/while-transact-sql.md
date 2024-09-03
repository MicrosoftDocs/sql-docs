---
title: "WHILE (Transact-SQL)"
description: WHILE sets a condition for the repeated execution of an SQL statement or statement block.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/10/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "WHILE_TSQL"
  - "WHILE"
  - "CONTINUE_TSQL"
  - "CONTINUE"
helpviewer_keywords:
  - "statements [SQL Server], repeated executions"
  - "statement blocks [SQL Server]"
  - "repeated statement executions"
  - "nested WHILE loops"
  - "WHILE keyword"
  - "restarting WHILE loop"
  - "CONTINUE keyword"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# WHILE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Sets a condition for the repeated execution of an SQL statement or statement block. The statements are executed repeatedly as long as the specified condition is true. The execution of statements in the `WHILE` loop can be controlled from inside the loop with the `BREAK` and `CONTINUE` keywords.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and Microsoft Fabric.

```syntaxsql
WHILE boolean_expression
    { sql_statement | statement_block | BREAK | CONTINUE }
```

Syntax for Azure Synapse Analytics and Analytics Platform System (PDW).

```syntaxsql
WHILE boolean_expression
    { sql_statement | statement_block | BREAK }
```

## Arguments

#### *boolean_expression*

An [expression](expressions-transact-sql.md) that returns `TRUE` or `FALSE`. If the Boolean expression contains a `SELECT` statement, the `SELECT` statement must be enclosed in parentheses.

#### { *sql_statement* | *statement_block* }

Any [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or statement grouping as defined with a statement block. To define a statement block, use the control-of-flow keywords `BEGIN` and `END`.

#### BREAK

Causes an exit from the innermost `WHILE` loop. Any statements that appear after the `END` keyword, marking the end of the loop, are executed.

#### CONTINUE

Restarts a `WHILE` loop. Any statements after the `CONTINUE` keyword are ignored. `CONTINUE` is frequently, but not always, opened by an `IF` test. For more information, see [Control-of-Flow](control-of-flow.md).

## Remarks

If two or more `WHILE` loops are nested, the inner `BREAK` exits to the next outermost loop. All the statements after the end of the inner loop run first, and then the next outermost loop restarts.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use BREAK and CONTINUE with nested IF...ELSE and WHILE

In the following example, if the average list price of a product is less than $300, the `WHILE` loop doubles the prices and then selects the maximum price. If the maximum price is less than or equal to $500, the `WHILE` loop restarts and doubles the prices again. This loop continues doubling the prices until the maximum price is greater than $500, and then exits the `WHILE` loop and prints a message.

```sql
USE AdventureWorks2022;
GO

WHILE (
        SELECT AVG(ListPrice)
        FROM Production.Product
        ) < $300
BEGIN
    UPDATE Production.Product
    SET ListPrice = ListPrice * 2

    SELECT MAX(ListPrice)
    FROM Production.Product

    IF (
            SELECT MAX(ListPrice)
            FROM Production.Product
            ) > $500
        BREAK
    ELSE
        CONTINUE
END

PRINT 'Too much for the market to bear';
```

### B. Use WHILE in a cursor

The following example uses `@@FETCH_STATUS` to control cursor activities in a `WHILE` loop.

```sql
DECLARE @EmployeeID AS NVARCHAR(256)
DECLARE @Title AS NVARCHAR(50)

DECLARE Employee_Cursor CURSOR
FOR
SELECT LoginID,
    JobTitle
FROM AdventureWorks2022.HumanResources.Employee
WHERE JobTitle = 'Marketing Specialist';

OPEN Employee_Cursor;

FETCH NEXT
FROM Employee_Cursor
INTO @EmployeeID,
    @Title;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT '   ' + @EmployeeID + '      ' + @Title

    FETCH NEXT
    FROM Employee_Cursor
    INTO @EmployeeID,
        @Title;
END;

CLOSE Employee_Cursor;

DEALLOCATE Employee_Cursor;
GO
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### C: WHILE loop

In the following example, if the average list price of a product is less than $300, the `WHILE` loop doubles the prices and then selects the maximum price. If the maximum price is less than or equal to $500, the `WHILE` loop restarts and doubles the prices again. This loop continues doubling the prices until the maximum price is greater than $500, and then exits the `WHILE` loop.

```sql
WHILE (
        SELECT AVG(ListPrice)
        FROM dbo.DimProduct
        ) < $300
BEGIN
    UPDATE dbo.DimProduct
    SET ListPrice = ListPrice * 2;

    SELECT MAX(ListPrice)
    FROM dbo.DimProduct

    IF (
            SELECT MAX(ListPrice)
            FROM dbo.DimProduct
            ) > $500
        BREAK;
END
```

## Related content

- [ALTER TRIGGER (Transact-SQL)](../statements/alter-trigger-transact-sql.md)
- [Control-of-Flow](control-of-flow.md)
- [CREATE TRIGGER (Transact-SQL)](../statements/create-trigger-transact-sql.md)
- [Cursors (Transact-SQL)](cursors-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
