---
title: "IF...ELSE (Transact-SQL)"
description: Transact-SQL language reference for IF-ELSE statements to provide control flow.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/17/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "IF_TSQL"
  - "IF"
helpviewer_keywords:
  - "IF...ELSE keyword"
  - "ELSE (IF...ELSE) keyword"
  - "ELSE keyword"
  - "IF keyword"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# IF...ELSE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Imposes conditions on the execution of a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. The [!INCLUDE [tsql](../../includes/tsql-md.md)] statement that follows an `IF` keyword and its condition is executed if the condition is satisfied: the Boolean expression returns `TRUE`. The optional `ELSE` keyword introduces another [!INCLUDE [tsql](../../includes/tsql-md.md)] statement that is executed when the `IF` condition isn't satisfied: the Boolean expression returns `FALSE`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
IF boolean_expression
    { sql_statement | statement_block }
[ ELSE
    { sql_statement | statement_block } ]
```

## Arguments

#### *boolean_expression*

An expression that returns `TRUE` or `FALSE`. If the Boolean expression contains a `SELECT` statement, the `SELECT` statement must be enclosed in parentheses.

#### { *sql_statement* | *statement_block* }

Any [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or statement grouping as defined by using a statement block. Unless a statement block is used, the `IF` or `ELSE` condition can affect the performance of only one [!INCLUDE [tsql](../../includes/tsql-md.md)] statement.

To define a statement block, use the control-of-flow keywords `BEGIN` and `END`.

## Remarks

An `IF...ELSE` construct can be used in batches, in stored procedures, and in ad hoc queries. When this construct is used in a stored procedure, it's usually to test for the existence of some parameter.

`IF` tests can be nested after another `IF` or following an `ELSE`. The limit to the number of nested levels depends on available memory.

## Examples

```sql
IF DATENAME(weekday, GETDATE()) IN (N'Saturday', N'Sunday')
    SELECT 'Weekend';
ELSE
    SELECT 'Weekday';
```

For more examples, see [ELSE (IF...ELSE)](else-if-else-transact-sql.md).

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following example uses `IF...ELSE` to determine which of two responses to show the user, based on the weight of an item in the `DimProduct` table.

```sql
-- Uses AdventureWorksDW

DECLARE @maxWeight FLOAT, @productKey INT;

SET @maxWeight = 100.00;
SET @productKey = 424;

IF @maxWeight <= (
        SELECT Weight
        FROM DimProduct
        WHERE ProductKey = @productKey
    )
    SELECT @productKey AS ProductKey,
        EnglishDescription,
        Weight,
        'This product is too heavy to ship and is only available for pickup.' AS ShippingStatus
    FROM DimProduct
    WHERE ProductKey = @productKey;
ELSE
    SELECT @productKey AS ProductKey,
        EnglishDescription,
        Weight,
        'This product is available for shipping or pickup.' AS ShippingStatus
    FROM DimProduct
    WHERE ProductKey = @productKey;
```

## Related content

- [BEGIN...END (Transact-SQL)](begin-end-transact-sql.md)
- [END (BEGIN...END) (Transact-SQL)](end-begin-end-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [WHILE (Transact-SQL)](while-transact-sql.md)
- [CASE (Transact-SQL)](case-transact-sql.md)
- [Control-of-Flow Language (Transact-SQL)](control-of-flow.md)
- [ELSE (IF...ELSE) (Transact-SQL)](else-if-else-transact-sql.md)
