---
title: "BEGIN...END (Transact-SQL)"
description: BEGIN...END allows the execution of a group of Transact-SQL statements in a control of flow.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/21/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "BEGIN"
  - "BEGIN_TSQL"
helpviewer_keywords:
  - "enclosing statements [SQL Server]"
  - "BEGIN statement"
  - "control-of-flow language [SQL Server], BEGIN...END statement"
  - "BEGIN...END keyword"
  - "grouping statements, BEGIN...END statement"
  - "executing Transact-SQL statements together [SQL Server]"
  - "statements [SQL Server], grouping"
dev_langs:
  - "TSQL"
ai-usage: ai-assisted
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# BEGIN...END (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Encloses a sequence of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements into a logical block of code. This use of `BEGIN` is unrelated to the `BEGIN TRANSACTION` and `BEGIN ATOMIC` statements.

You can use `BEGIN...END` blocks with a preceding flow-control statement such as `IF`, `ELSE`, and `WHILE`. However, you can also use these blocks without any preceding flow-control statement to group sequences of statements in an organized way. However, each new `BEGIN...END` block doesn't create a new lexical scope.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
BEGIN [ ; ]
    { sql_statement | statement_block }
END [ ; ]
```

## Arguments

#### { *sql_statement* | *statement_block* }

Any valid [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or statement grouping as defined by using a statement block.

## Remarks

A `BEGIN...END` block must contain at least one statement. If you try to use an empty `BEGIN...END` block, you get a syntax error, even if you use a semicolon after each keyword. You can avoid empty `BEGIN...END` blocks by using a `GOTO` label as a placeholder statement. See [Example C: Use a GOTO label for dynamically generated BEGIN...END blocks](#c-use-a-goto-label-for-dynamically-generated-beginend-blocks).

`BEGIN...END` blocks can be nested.

`BEGIN...END` blocks don't define any lexical scope. If you declare a variable within a block, it's visible throughout the parent batch, not just within the block containing the `DECLARE` statement.

You can't use `BEGIN...END` blocks across multiple batches. For example, you can't use the `GO` batch separator inside a `BEGIN...END` block.

Using a `BEGIN...END` block to group statements doesn't mean all statements in the group run atomically. When a batch runs outside a transaction and an error is raised or an exception is thrown by the second statement of a multistatement `BEGIN...END` block, the first statement isn't rolled back.

Semicolons after the `BEGIN` and `END` keywords are [optional but recommended](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md), except in the following cases:

- You need a semicolon before the `WITH` keyword that starts a [common table expression](../queries/recursive-common-table-expression-transact-sql.md) (CTE).

- You need a semicolon with a `THROW` statement within a block.

- Use a semicolon after `BEGIN` to prevent confusion with the `BEGIN TRANSACTION` or `BEGIN ATOMIC` statements.

- Using a semicolon after `END` ensures that any subsequent statement, particularly a `WITH` keyword or `THROW` statement, doesn't need a preceding semicolon.

Although all [!INCLUDE [tsql](../../includes/tsql-md.md)] statements are valid within a `BEGIN...END` block, you shouldn't group certain [!INCLUDE [tsql](../../includes/tsql-md.md)] statements together within the same batch or statement block. Make sure statements don't conflict with existing Transact-SQL batch requirements.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Define a sequence of logically related statements in order

In the following example, `BEGIN` and `END` define sequences of logically related [!INCLUDE [tsql](../../includes/tsql-md.md)] statements to execute in order. The example also shows nested blocks.

```sql
USE AdventureWorks2025;
GO

DECLARE @personId AS INT = (
    SELECT p.BusinessEntityID
    FROM Person.Person AS p
    WHERE p.rowguid = { GUID '92C4279F-1207-48A3-8448-4636514EB7E2' }
);

IF (@personId IS NULL)
    THROW 50001, 'Person not found.', 1;

/* Concatenate the person's name fields: */;
BEGIN
    DECLARE @title AS NVARCHAR (8),
            @first AS NVARCHAR (50),
            @middle AS NVARCHAR (50),
            @last AS NVARCHAR (50),
            @suffix AS NVARCHAR (10);

    SELECT @title = NULLIF (p.Title, N''),
           @first = p.FirstName,
           @middle = NULLIF (p.MiddleName, N''),
           @last = p.LastName,
           @suffix = NULLIF (p.Suffix, N'')
    FROM Person.Person AS p
    WHERE p.BusinessEntityID = @personId;

    DECLARE @nameConcat AS NVARCHAR (255) = CONCAT_WS(N' ', @title, @first, @middle, @last, @suffix);

    /* This is a nested BEGIN...END block: */;
    BEGIN
        DECLARE @emails AS NVARCHAR (MAX) = (
            SELECT STRING_AGG(e.EmailAddress, /*separator:*/N'; ')
            FROM Person.EmailAddress AS e
            WHERE e.BusinessEntityID = @personId
        );

        SET @nameConcat = CONCAT(@nameConcat, N' (', @emails, N')');
    END
END

/* BEGIN...END blocks do not define a lexical scope, so
   even though @nameAndEmails is declared above, it is
   still in-scope after the END keyword. */
SELECT @nameConcat AS NameAndEmails;
```

### B. Use BEGIN...END in a transaction

In the following example, `BEGIN` and `END` define a series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that execute together. If the `BEGIN...END` block isn't included, both `ROLLBACK TRANSACTION` statements execute, and both `PRINT` messages are returned.

```sql
USE AdventureWorks2025;
GO

BEGIN TRANSACTION;

IF @@TRANCOUNT = 0
    BEGIN
        SELECT FirstName,
               MiddleName
        FROM Person.Person
        WHERE LastName = 'Adams';

        ROLLBACK TRANSACTION;

        PRINT N'Rolling back the transaction two times causes an error.';
    END

ROLLBACK TRANSACTION;

PRINT N'Rolled back the transaction.';
```

### C. Use a GOTO label for dynamically generated BEGIN...END blocks

If you generate dynamic Transact-SQL with a `BEGIN...END` block and you want your program to always render the `BEGIN...END` keywords, you can use a `GOTO` label as a placeholder statement to avoid having an empty `BEGIN...END` block.

```sql
BEGIN
    unusedLabel:
END
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### C. Define a series of statements that run together

In the following example, `BEGIN` and `END` define a series of [!INCLUDE [DWsql](../../includes/dwsql-md.md)] statements that run together.

> [!CAUTION]  
> If you remove the `BEGIN` and `END` keywords, the following example runs in an infinite loop. The `WHILE` statement loops only the `SELECT` query, and never reaches the `SET @Iteration += 1` statement.

```sql
-- Uses AdventureWorksDW;
DECLARE @Iteration AS INT = 0;

WHILE @Iteration < 10
    BEGIN
        SELECT FirstName,
               MiddleName
        FROM dbo.DimCustomer
        WHERE LastName = 'Adams';
        SET @Iteration + = 1;
    END
```

## Related content

- [ALTER TRIGGER (Transact-SQL)](../statements/alter-trigger-transact-sql.md)
- [Control-of-Flow](control-of-flow.md)
- [CREATE TRIGGER (Transact-SQL)](../statements/create-trigger-transact-sql.md)
- [END (BEGIN...END) (Transact-SQL)](end-begin-end-transact-sql.md)
