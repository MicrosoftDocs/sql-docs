---
title: "QUALIFY Clause (Transact-SQL)"
description: The QUALIFY clause filters rows based on predicates that can reference window (analytic) functions, without requiring a subquery.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, jovanpop
ms.date: 09/21/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
f1_keywords:
  - "QUALIFY_TSQL"
  - "QUALIFY"
helpviewer_keywords:
  - "QUALIFY clause [Transact-SQL]"
  - "filtering with window functions [SQL Server]"
  - "analytic functions [SQL Server], QUALIFY"
  - "row-number per group [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---
# SELECT - QUALIFY clause (Transact-SQL)

[!INCLUDE [fabric-se-dw](../../includes/applies-to-version/fabric-se-dw.md)]

The `QUALIFY` clause filters rows returned by a query based on a search condition that can reference window (analytic) functions. The `QUALIFY` clause allows filtering on analytic results such as row numbers, ranks, running totals, or moving averages without requiring a subquery or CTE. `QUALIFY` doesn't replace `WHERE` or `HAVING`. Instead, it adds another filtering stage that becomes available *after window functions are computed*.

## Syntax

```syntaxsql
QUALIFY <filter_condition>
```

Placement:

```syntaxsql
SELECT select_list
FROM   table_source
[ WHERE <search_condition> ]
[ GROUP BY group_by_specification ]
[ HAVING <search_condition> ]
[ QUALIFY <filter_condition> ]
[ ORDER BY <order_by_expression> [ , ...n ] ]
[ FOR JSON <options> ]
```

## Arguments

### *filter_condition*

A predicate that determines which rows are returned. It can reference:

- Columns or variables in scope.
- Aliases of window functions defined in the `SELECT` list.
- Window functions written directly inside `QUALIFY`.

## Remarks 

Use `QUALIFY` to:

- Filter on the result of a window (analytic) function such as `ROW_NUMBER`, `RANK`, `DENSE_RANK`, `NTILE`, or windowed aggregates.
- Express common analytic patterns like "top‑N per group" without using subqueries.

## Evaluation order

`QUALIFY` is evaluated **after** `WHERE`, `GROUP BY`, and `HAVING`, and **after** window functions in the `SELECT` list are computed,
but **before** `ORDER BY`, `DISTINCT`, and `FOR JSON`.

`QUALIFY` applies after window computation but doesn't affect window values.

The evaluation order of clauses in the query is: `FROM` → `WHERE` → `GROUP BY` → `HAVING` → **compute window functions** → `QUALIFY` → `ORDER BY`.

Filtering roles of `WHERE`, `HAVING`, and `QUALIFY` are:

- `WHERE` – Filters individual rows **before grouping and before window functions**.  
- `HAVING` – Filters **grouped results after GROUP BY aggregates** are computed.  
- `QUALIFY` – Filters **rows after window functions** are evaluated, enabling predicates on analytic function results.

## Interoperability

The following table shows where the `QUALIFY` clause is supported:

| Context                                   | Supported? | Notes |
|-------------------------------------------|------------|-------|
| `SELECT`                                  | Yes        | Primary use case; `QUALIFY` filters after window functions. |
| Subqueries                                | Yes        | Each subquery may include its own `QUALIFY` clause. |
| Common Table Expressions (CTEs)           | Yes        | `QUALIFY` can appear inside the CTE's `SELECT`. |
| Views (`CREATE VIEW`)                     | Yes        | View definition may contain `QUALIFY` in the `SELECT` statement. |
| `INSERT` … `SELECT`                       | Yes        | `QUALIFY` allowed inside the `SELECT` portion. |
| `CREATE TABLE AS SELECT` (CTAS)           | Yes        | `QUALIFY` supported inside the `SELECT` query spec. |
| `UNION` / `INTERSECT` / `EXCEPT`          | Yes        | Each branch of the set operation may include `QUALIFY`. |
| `MERGE` … `USING` (source query)          | Yes        | `QUALIFY` allowed inside the `USING` source `SELECT`. |
| Inline table‑valued functions (TVF)       | Yes        | TVF's `SELECT` query may contain `QUALIFY`. |
| `UPDATE` … `SET`                          | No         | `UPDATE` doesn't accept `QUALIFY`. |
| `DELETE`                                  | No         | `DELETE` doesn't accept `QUALIFY`. |

## Best practices

Follow these best practices when using the `QUALIFY` clause:

- Use aliases in the `SELECT` list for clarity. Avoid duplicating the same function definition in `SELECT` and `QUALIFY`.
- Prefer `QUALIFY` to complex nested subqueries when filtering on window function results.

## Errors

| Scenario                                                                  | Error message                                                                                                 |
|-----------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------|
| `QUALIFY` clause is present but the predicate contains **no window functions**.               | QUALIFY clause requires at least one window function in its predicate or in referenced aliases.                           |
| A **window function** appears in the `WHERE` clause.                                        | Window functions aren't allowed in WHERE. Move the predicate to QUALIFY or rewrite using a subquery/CTE.                 |
| `QUALIFY` references an **alias** that exists, but it **is not a window function**.           | QUALIFY clause requires at least one window function in its predicate or in referenced aliases                            |
| `QUALIFY` references an **undefined alias or column**.                                        | Invalid column name '{column name}'.|
| `SELECT` clause returns multiple columns with the same alias where one of them is referenced in `QUALIFY`  | Ambiguous column name '{column name}'.                                                         |
| `QUALIFY` uses an alias that **collides** with a base column name, creating ambiguity.        | Ambiguous reference to '{name}'. Rename the alias or qualify the column reference explicitly.                             |
| `QUALIFY` predicate contains a **group aggregate** (for example, `SUM()`) without `OVER()`.          | Aggregate `SUM` must be used with an `OVER` clause in `QUALIFY`. Use `HAVING` to filter groups, or add an `OVER()` window in `QUALIFY`.   |
| `QUALIFY` references a **non-scalar** expression (for example, unsupported subquery returning multiple rows). | Subquery used in `QUALIFY` must return a single scalar value.                                                               |

## Examples

### A. Basic example

For example, in the following query, use `QUALIFY` to return the top‑priced product per color:

```sql
SELECT *
FROM (VALUES ('Red',    'Road Bike',     1200.00 ),
             ('Red',    'City Bike',     800.00  ),
             ('Blue',   'Mountain Bike', 1500.00 ),
             ('Blue',   'Road Bike',     1100.00 )) AS
      Source ( Color,    Product,        Price   )
QUALIFY
        ROW_NUMBER() OVER (PARTITION BY Color ORDER BY Price DESC) = 1;
```

The `ROW_NUMBER()` function is evaluated for every row in the source. It creates partitions by grouping together all rows that share the same `Color` value. Within each partition, `ROW_NUMBER()` assigns a sequential number to each row based on the `Price` value that is specified in the `ORDER BY` clause.

Finally, the `QUALIFY` clause filters the result set and returns only the row with  `ROW_NUMBER()` = 1, which corresponds to the highest‑priced row within each partition.

### B. Top‑1 per category

Returns the single highest‑priced product in each `ProductSubcategory` by assigning a row number per category and keeping only the first row (`rn = 1`).

```sql
SELECT ProductSubcategoryID,
       ProductID,
       [Name],
       ListPrice
FROM Production.Product
WHERE ProductSubcategoryID IS NOT NULL
QUALIFY
       1 = ROW_NUMBER() OVER (PARTITION BY ProductSubcategoryID ORDER BY ListPrice DESC ) ;
```

Without the `QUALIFY` clause, you would need to rewrite the query using an additional CTE or subquery. In a query without `QUALIFY`, the window function is computed in the inner query, projected as an output column, and then filtered in the outer query's `WHERE` clause. An equivalent T‑SQL query without `QUALIFY` might look like:

```sql
WITH ranked AS (
  SELECT ProductSubcategoryID,
         ProductID,
         [Name],
         ListPrice,
         ROW_NUMBER() OVER (
           PARTITION BY ProductSubcategoryID
           ORDER BY ListPrice DESC
         ) AS rn
  FROM Production.Product
)
SELECT ProductSubcategoryID, ProductID, [Name], ListPrice
FROM ranked
WHERE rn = 1;
```

### C. Top‑3 per category

Returns the top three highest‑priced products within each `ProductSubcategory` by assigning a row number per category and filtering to the first three rows.

```sql
SELECT ProductSubcategoryID,
       ProductID,
       [Name],
       ListPrice,
       ROW_NUMBER() OVER (
         PARTITION BY ProductSubcategoryID
         ORDER BY ListPrice DESC, ProductID
       ) AS rn
FROM Production.Product
QUALIFY rn <= 3;
```

### D. Keep rows within rank threshold

Returns the products that rank in the top two highest `ListPrice` values within each `ProductSubcategory`.

```sql
SELECT ProductSubcategoryID,
       ProductID,
       [Name],
       ListPrice,
       RANK() OVER (
         PARTITION BY ProductSubcategoryID
         ORDER BY ListPrice DESC
       ) AS rnk
FROM Production.Product
QUALIFY rnk <= 2;
```

### E. Filtering on a windowed average

Returns only the orders where `TotalDue` is at least 1.5× higher than that customer's average order amount.

```sql
SELECT h.CustomerID,
       h.SalesOrderID,
       h.OrderDate,
       h.TotalDue,
       AVG(h.TotalDue) OVER (PARTITION BY h.CustomerID) AS avg_customer_total
FROM Sales.SalesOrderHeader AS h
QUALIFY h.TotalDue >= 1.5 * avg_customer_total;
```

## Related content

- [SELECT (Transact-SQL)](select-transact-sql.md)