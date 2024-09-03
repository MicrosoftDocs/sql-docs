---
title: "BEGIN...END (Transact-SQL)"
description: BEGIN...END allows the execution of a group of Transact-SQL statements in a control of flow.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/18/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# BEGIN...END (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Encloses a series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements so that a group of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements can be executed in a logical block of code. `BEGIN` and `END` are control-of-flow language keywords.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
BEGIN
    { sql_statement | statement_block }
END
```

## Arguments

#### { *sql_statement* | *statement_block* }

Any valid [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or statement grouping as defined by using a statement block.

## Remarks

`BEGIN...END` blocks can be nested.

Although all [!INCLUDE [tsql](../../includes/tsql-md.md)] statements are valid within a `BEGIN...END` block, certain [!INCLUDE [tsql](../../includes/tsql-md.md)] statements shouldn't be grouped together within the same batch, or statement block.

## Examples

In the following example, `BEGIN` and `END` define a series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that execute together. If the `BEGIN...END` block isn't included, both `ROLLBACK TRANSACTION` statements would execute, and both `PRINT` messages would be returned.

```sql
USE AdventureWorks2022;
GO

BEGIN TRANSACTION
GO

IF @@TRANCOUNT = 0
BEGIN
    SELECT FirstName, MiddleName
    FROM Person.Person
    WHERE LastName = 'Adams';

    ROLLBACK TRANSACTION;

    PRINT N'Rolling back the transaction two times would cause an error.';
END;

ROLLBACK TRANSACTION;

PRINT N'Rolled back the transaction.';
GO
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

In the following example, `BEGIN` and `END` define a series of [!INCLUDE [DWsql](../../includes/dwsql-md.md)] statements that run together. If the `BEGIN...END` block isn't included, the following example runs in a continuous loop.

```sql
-- Uses AdventureWorksDW

DECLARE @Iteration INT = 0;

WHILE @Iteration < 10
BEGIN
    SELECT FirstName,
        MiddleName
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
