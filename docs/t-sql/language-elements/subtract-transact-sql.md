---
title: "- (Subtraction) (Transact-SQL)"
description: Subtract two numbers using this built-in arithmetic subtraction operator.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/15/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "subtract"
  - "-"
  - "-_TSQL"
helpviewer_keywords:
  - "- (subtract)"
  - "subtract operator (-)"
  - "minus operator (-)"
  - "subtracting numbers"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# - (Subtraction) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Subtracts two numbers (an arithmetic subtraction operator). Can also subtract a number, in days, from a date.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression - expression
```

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md) of any one of the data types of the numeric data type category, except the **bit** data type. Can't be used with **date**, **time**, **datetime2**, or **datetimeoffset** data types.

## Return types

Returns the data type of the argument with the higher precedence. For more information, see [Data type precedence](../data-types/data-type-precedence-transact-sql.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use subtraction in a SELECT statement

The following example calculates the difference in tax rate between the state or province with the highest tax rate, and the state or province with the lowest tax rate.

**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssSDS](../../includes/sssds-md.md)].

```sql
SELECT MAX(TaxRate) - MIN(TaxRate) AS 'Tax Rate Difference'
FROM Sales.SalesTaxRate
WHERE StateProvinceID IS NOT NULL;
GO
```

You can change the order of execution by using parentheses. Calculations inside parentheses are evaluated first. If parentheses are nested, the most deeply nested calculation has precedence.

### B. Use date subtraction

The following example subtracts several days from a **datetime** date.

Applies to: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssSDS](../../includes/sssds-md.md)].

```sql
DECLARE @altstartdate DATETIME;
SET @altstartdate = CONVERT(DATETIME, 'January 10, 1900 3:00 AM', 101);
SELECT @altstartdate - 1.5 AS 'Subtract Date';
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
Subtract Date
-----------------------
1900-01-08 15:00:00.000
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### C. Use subtraction in a SELECT statement

The following example calculates the difference in a base rate between the employee with the highest base rate and the employee with the lowest tax rate, from the `dimEmployee` table.

```sql
SELECT MAX(BaseRate) - MIN(BaseRate) AS BaseRateDifference
FROM DimEmployee;
```

## Related content

- [-= (Subtraction assignment) (Transact-SQL)](subtract-equals-transact-sql.md)
- [Compound operators (Transact-SQL)](compound-operators-transact-sql.md)
- [Arithmetic operators (Transact-SQL)](arithmetic-operators-transact-sql.md)
- [Unary operators - Negative (Transact-SQL)](unary-operators-negative.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [What are the SQL database functions?](../functions/functions.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
