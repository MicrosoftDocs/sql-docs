---
title: "BEGIN...END (Transact-SQL)"
description: BEGIN...END allows the execution of a group of Transact-SQL statements in a control of flow.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/18/2024
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
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# BEGIN...END (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Encloses a sequence of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements into a logical block of code. Note this use of "`BEGIN`" is unrelated to the `BEGIN TRANSACTION` and `BEGIN ATOMIC` statements.

`BEGIN...END` blocks are often used with a preceding flow-control statement such as `IF`, `ELSE` and `WHILE`, but these blocks can also be used without any preceding flow-control to aesthetically group sequences of statements in a way similar to an anonymous scope `{ ... }` in C-style languages except that `BEGIN...END` blocks do not create a new lexical scope.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md#:~:text=semicolon)

## Syntax

```syntaxsql
BEGIN[;]
    { sql_statement | statement_block }
END[;]
```

* The use of semicolons after the `BEGIN` and `END` keywords is optional [but recommended]((../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)), excepting for some cases where they are required, such as when a CTE (`WITH`) or `THROW` statement is used within a block.
  * Using a semicolon after `BEGIN` can help avoid potential confusion with the `BEGIN TRANSACTION` or `BEGIN ATOMIC` statements.
    Using a semicolon after `END` ensures that any subsequent statement, in particular the `WITH` keyword or `THROW` statement, will not need any preceding semicolon.
* `BEGIN...END` must contain at least one statement: attempting to use an empty `BEGIN...END` block will result in a syntax error, even when each keyword is used with a semicolon terminator.

## Arguments

#### { *sql_statement* | *statement_block* }

Any valid [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or statement grouping as defined by using a statement block.

## Remarks

* `BEGIN...END` blocks can be nested.
* `BEGIN...END` blocks cannot span multiple batches, i.e. the `GO` batch separator cannot be used inside a `BEGIN...END` block.
* `BEGIN...END` blocks do not define any lexical scope: a variable declared within a block will be visible throughout the parent batch and not just within the block containing the `DECLARE` statement.
* Using a `BEGIN...END` block to group statements does not imply all that all statements in the group will be executed atomically: When a batch runs outside of a transaction and an error is raised or an exception is thrown by the 2nd statement of a multi-statement `BEGIN...END` block then the 1st statement will not be rolled-back.
* To avoid having a (syntactically invalid) empty `BEGIN...END` block, you may use a `GOTO` label as a "no-op" placeholder statement.

Although all [!INCLUDE [tsql](../../includes/tsql-md.md)] statements are valid within a `BEGIN...END` block, certain [!INCLUDE [tsql](../../includes/tsql-md.md)] statements shouldn't be grouped together within the same batch, or statement block<!-- TODO: Is there an authoritative list of statements that should not be used? -->.

## Examples

In the following example, `BEGIN` and `END` define sequences of logically related [!INCLUDE [tsql](../../includes/tsql-md.md)] statements to be executed in-order; nested blocks are also demonstrated.

```sql
USE AdventureWorks2022;

GO

DECLARE @personId int = ( SELECT p.BusinessEntityID FROM Person.Person AS p WHERE p.rowguid = {guid'92C4279F-1207-48A3-8448-4636514EB7E2'} );
IF( @personId IS NULL ) THROW 50001, 'Person not found.', 1;

/* Concatenate the person's name fields: */
BEGIN;
  DECLARE @title nvarchar(8), @first nvarchar(50), @middle nvarchar(50), @last nvarchar(50), @suffix nvarchar(10);

  SELECT
    @title  = NULLIF( p.Title, N'' ),
    @first  =         p.FirstName,
    @middle = NULLIF( p.MiddleName, N'' ),
    @last   =         p.LastName,
    @suffix = NULLIF( p.Suffix, N'' )
  FROM
    Person.Person AS p
  WHERE
    p.BusinessEntityID = @personId;

  DECLARE @nameConcat nvarchar(255) = CONCAT_WS( /*separator: */ N' ', @title, @first, @middle, @last, @suffix );

  /* This is a nested BEGIN...END block: */
  BEGIN;
    DECLARE @emails nvarchar(max) = ( SELECT STRING_AGG( e.EmailAddress, /*separator:*/ N'; ' ) FROM Person.EmailAddress AS e WHERE e.BusinessEntityID = @personId );
	SET @nameConcat = CONCAT( @nameConcat, N' (', @emails, N')' );
  END;
  
END;

/* BEGIN...END blocks do not define a lexical scope, so even though @nameAndEmails is declared above, it is still in-scope after the END keyword. */
SELECT @nameConcat AS NameAndEmails;
```

## Empty blocks:

If you are generating Dynamic SQL with a `BEGIN...END` block such that it's simpler for your program to always render the `BEGIN...END` keywords then you may use a `GOTO` label as a "NOOP" or placeholder statement:

```sql
BEGIN;
unusedNoopLabel:
END;
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

In the following example, `BEGIN` and `END` define a series of [!INCLUDE [DWsql](../../includes/dwsql-md.md)] statements that run together. If the `BEGIN` and `END` keywords are commented-out then the following example will run forever in an infinite loop because only the `SELECT` query will be looped by the `WHILE` statement while the `SET @Iteration += 1` statement will never be reached. 

```sql
USE AdventureWorksDW;

DECLARE @Iteration INT = 0;

WHILE @Iteration < 10
BEGIN;
    SELECT FirstName, MiddleName
    FROM dbo.DimCustomer
    WHERE LastName = 'Adams';

    SET @Iteration += 1;
END;
```

## Related content

- [ALTER TRIGGER (Transact-SQL)](../statements/alter-trigger-transact-sql.md)
- [Control-of-Flow Language (Transact-SQL)](control-of-flow.md)
- [CREATE TRIGGER (Transact-SQL)](../statements/create-trigger-transact-sql.md)
- [END (BEGIN...END) (Transact-SQL)](end-begin-end-transact-sql.md)
