---
title: CHOOSE (Transact-SQL)
description: The CHOOSE logical function returns the item at the specified index from a list of values.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/30/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CHOOSE"
  - "CHOOSE_TSQL"
helpviewer_keywords:
  - "CHOOSE function"
dev_langs:
  - TSQL
---
# Logical functions - CHOOSE (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSE/DW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

The `CHOOSE` [!INCLUDE [tsql-md](../../includes/tsql-md.md)] function returns the item at the specified index from a list of values in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CHOOSE ( index , val_1 , val_2 [ , val_n ] )
```

## Arguments

#### *index*

An integer expression that represents a 1-based index into the list of the items following it.

If the provided index value has a numeric data type other than **int**, the value is implicitly converted to an integer. If the index value exceeds the bounds of the array of values, `CHOOSE` returns `NULL`.

#### val_1 ... val_n

List of comma-separated values of any data type.

## Return types

Returns the data type with the highest precedence from the set of types passed to the function. For more information, see [Data type precedence](../data-types/data-type-precedence-transact-sql.md).

## Remarks

`CHOOSE` acts like an index into an array, where the array is composed of the arguments that follow the index argument. The index argument determines which of the following values `CHOOSE` returns.

Explicitly including an extremely large number of values (thousands of values separated by commas) in a `CHOOSE` clause can consume resources and return the following error:

```output
Msg 8631, Level 17, State 1, Line 1
Internal error: Server stack limit has been reached. Please look for potentially deep nesting in your query, and try to simplify it.
```

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. CHOOSE example

The following example returns the third item from the list of values.

```sql
SELECT CHOOSE(3, 'Manager', 'Director', 'Developer', 'Tester') AS Result;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Result
-------------
Developer
```

### B. CHOOSE example based on column

The following example returns a basic character string based on the value in the `ProductCategoryID` column.

```sql
USE AdventureWorks2025;
GO

SELECT ProductCategoryID,
       CHOOSE(ProductCategoryID, 'A', 'B', 'C', 'D', 'E') AS Expression1
FROM Production.ProductCategory;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
ProductCategoryID Expression1
----------------- -----------
3                 C
1                 A
2                 B
4                 D
```

### C. CHOOSE in combination with MONTH

The following example returns the season when a product model was last modified. The `MONTH` function returns the month value from the `ModifiedDate` column.

```sql
USE AdventureWorksLT;
GO

SELECT Name,
       ModifiedDate,
       CHOOSE(MONTH(ModifiedDate), 'Winter', 'Winter', 'Spring', 'Spring', 'Spring', 'Summer', 'Summer', 'Summer', 'Autumn', 'Autumn', 'Autumn', 'Winter') AS Quarter_Modified
FROM SalesLT.ProductModel AS PM
WHERE Name LIKE '%Frame%'
ORDER BY ModifiedDate;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Name                        ModifiedDate            Quarter_Modified
--------------------------- ----------------------- ----------------
HL Road Frame               2002-05-02 00:00:00.000 Spring
HL Mountain Frame           2005-06-01 00:00:00.000 Summer
LL Road Frame               2005-06-01 00:00:00.000 Summer
ML Road Frame               2005-06-01 00:00:00.000 Summer
ML Road Frame-W             2006-06-01 00:00:00.000 Summer
ML Mountain Frame           2006-06-01 00:00:00.000 Summer
ML Mountain Frame-W         2006-06-01 00:00:00.000 Summer
LL Mountain Frame           2006-11-20 09:56:38.273 Autumn
HL Touring Frame            2009-05-16 16:34:28.980 Spring
LL Touring Frame            2009-05-16 16:34:28.980 Spring
```

## Related content

- [Logical Functions - IIF (Transact-SQL)](logical-functions-iif-transact-sql.md)
- [CASE (Transact-SQL)](../language-elements/case-transact-sql.md)
