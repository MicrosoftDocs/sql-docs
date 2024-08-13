---
title: ISNULL (Transact-SQL)
description: ISNULL replaces NULL with the specified replacement value.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ISNULL"
  - "ISNULL_TSQL"
  - "IFNULL_TSQL"
helpviewer_keywords:
  - "replacing null values"
  - "null values [SQL Server], ISNULL"
  - "null values [SQL Server], replacement values"
  - "ISNULL function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# ISNULL (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Replaces `NULL` with the specified replacement value.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ISNULL ( check_expression , replacement_value )
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *check_expression*

The [expression](../language-elements/expressions-transact-sql.md) to be checked for `NULL`. *check_expression* can be of any type.

#### *replacement_value*

The expression to be returned if *check_expression* is `NULL`. *replacement_value* must be of a type that is implicitly convertible to the type of *check_expression*.

## Return types

Returns the same type as *check_expression*. If a literal `NULL` is provided as *check_expression*, `ISNULL` returns the data type of the *replacement_value*. If a literal `NULL` is provided as *check_expression* and no *replacement_value* is provided, `ISNULL` returns an **int**.

## Remarks

The value of *check_expression* is returned if it's not `NULL`. Otherwise, *replacement_value* is returned after it's implicitly converted to the type of *check_expression*, if the types are different. *replacement_value* can be truncated if *replacement_value* is longer than *check_expression*.

> [!NOTE]  
> Use [COALESCE](../language-elements/coalesce-transact-sql.md) to return the first non-null value.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use ISNULL with AVG

The following example finds the average of the weight of all products. It substitutes the value `50` for all `NULL` entries in the `Weight` column of the `Product` table.

```sql
USE AdventureWorks2022;
GO
SELECT AVG(ISNULL(Weight, 50))
FROM Production.Product;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
59.79
```

### B. Use ISNULL

The following example selects the description, discount percentage, minimum quantity, and maximum quantity for all special offers in [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)]. If the maximum quantity for a particular special offer is `NULL`, the `MaxQty` shown in the result set is `0.00`.

```sql
USE AdventureWorks2022;
GO
SELECT Description, DiscountPct, MinQty, ISNULL(MaxQty, 0.00) AS 'Max Quantity'
FROM Sales.SpecialOffer;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

| Description | DiscountPct | MinQty | Max Quantity |
| --- | --- | --- | --- |
| `No Discount` | 0.00 | 0 | 0 |
| `Volume Discount 11 to 14` | 0.02 | 11 | 14 |
| `Volume Discount 15 to 24` | 0.05 | 15 | 24 |
| `Volume Discount 25 to 40` | 0.10 | 25 | 40 |
| `Volume Discount 41 to 60` | 0.15 | 41 | 60 |
| `Volume Discount over 60` | 0.20 | 61 | 0 |
| `Mountain-100 Clearance Sale` | 0.35 | 0 | 0 |
| `Sport Helmet Discount-2002` | 0.10 | 0 | 0 |
| `Road-650 Overstock` | 0.30 | 0 | 0 |
| `Mountain Tire Sale` | 0.50 | 0 | 0 |
| `Sport Helmet Discount-2003` | 0.15 | 0 | 0 |
| `LL Road Frame Sale` | 0.35 | 0 | 0 |
| `Touring-3000 Promotion` | 0.15 | 0 | 0 |
| `Touring-1000 Promotion` | 0.20 | 0 | 0 |
| `Half-Price Pedal Sale` | 0.50 | 0 | 0 |
| `Mountain-500 Silver Clearance Sale` | 0.40 | 0 | 0 |

The following example uses `ISNULL` to replace a `NULL` value for `Color`, with the string `None`.

```sql
USE AdventureWorks2022;
GO
SELECT ProductID,
    Name,
    ProductNumber,
    ISNULL(Color, 'None') AS Color
FROM Production.Product;
```

Here's a partial result set.

| ProductID | Name | ProductNumber | Color |
| --- | --- | --- | --- |
| 1 | `Adjustable Race` | AR-5381 | None |
| 2 | `Bearing Ball` | BA-8327 | None |
| 3 | `BB Ball Bearing` | BE-2349 | None |
| 4 | `Headset Ball Bearings` | BE-2908 | None |
| 316 | `Blade` | BL-2036 | None |
| 317 | `LL Crankarm` | CA-5965 | Black |
| 318 | `ML Crankarm` | CA-6738 | Black |
| 319 | `HL Crankarm` | CA-7457 | Black |

### C. Test for `NULL` in a WHERE clause

Don't use `ISNULL` to find `NULL` values. Use `IS NULL` instead. The following example finds all products that have `NULL` in the weight column. Note the space between `IS` and `NULL`.

```sql
USE AdventureWorks2022;
GO
SELECT Name, Weight
FROM Production.Product
WHERE Weight IS NULL;
GO
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### D. Use ISNULL with AVG

The following example finds the average of the weight of all products in a sample table. It substitutes the value `50` for all `NULL` entries in the `Weight` column of the `Product` table.

```sql
-- Uses AdventureWorksDW

SELECT AVG(ISNULL(Weight, 50))
FROM dbo.DimProduct;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
52.88
```

### E. Use ISNULL

The following example uses `ISNULL` to test for `NULL` values in the column `MinPaymentAmount` and display the value `0.00` for those rows.

```sql
-- Uses AdventureWorks

SELECT ResellerName,
       ISNULL(MinPaymentAmount,0) AS MinimumPayment
FROM dbo.DimReseller
ORDER BY ResellerName;
```

Here's a partial result set.

| ResellerName | MinimumPayment |
| --- | --- |
| A Bicycle Association | 0.0000 |
| A Bike Store | 0.0000 |
| A Cycle Shop | 0.0000 |
| A Great Bicycle Company | 0.0000 |
| A Typical Bike Shop | 200.0000 |
| Acceptable Sales & Service | 0.0000 |

### F. Use IS NULL to test for NULL in a WHERE clause

The following example finds all products that have `NULL` in the `Weight` column. Note the space between `IS` and `NULL`.

```sql
-- Uses AdventureWorksDW

SELECT EnglishProductName, Weight
FROM dbo.DimProduct
WHERE Weight IS NULL;
```

## Related content

- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [IS NULL (Transact-SQL)](../queries/is-null-transact-sql.md)
- [System Functions by category for Transact-SQL](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
- [WHERE (Transact-SQL)](../queries/where-transact-sql.md)
- [COALESCE (Transact-SQL)](../language-elements/coalesce-transact-sql.md)
