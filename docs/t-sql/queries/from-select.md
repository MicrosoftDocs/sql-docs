---
title: FROM - SELECT (Transact-SQL)
description: Start a Fabric Transact-SQL query with FROM, place SELECT after the table sources, or omit SELECT to return all available columns.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop
ms.date: 08/05/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FROM_SELECT_TSQL"
  - "FROM_FIRST_TSQL"
  - "FROM_SELECT"
helpviewer_keywords:
  - "FROM clause, SELECT clause"
  - "FROM-first queries"
  - "SELECT clause after FROM"
  - "implicit SELECT"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---

# FROM - SELECT (Transact-SQL)

[!INCLUDE [fabric-se-dw](../../includes/applies-to-version/fabric-se-dw.md)]

In Fabric Transact-SQL, a query can start with the `FROM` clause and place the `SELECT` clause after the table sources. This FROM-first syntax establishes the available tables and columns before defining the projection.

The trailing `SELECT` clause is optional. If you omit `SELECT`, the query behaves as if you specified `SELECT *`.

The following statements are equivalent:

```sql
FROM dbo.Employee;
```

```sql
FROM dbo.Employee
SELECT *;
```

FROM-first syntax doesn't change query results or execution behavior. It provides another way to author a query while preserving the semantics of an equivalent SELECT-first statement.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

FROM-first syntax uses the table-source grammar supported by Fabric Data Warehouse. The only difference from SELECT-first syntax is that the optional `SELECT` clause follows the complete table-source expression.

```syntaxsql
FROM { <table_source> [ , ...n ] }
[ SELECT <select_list> ]
[ WHERE <search_condition> ]
[ GROUP BY <group_by_expression> [ , ...n ] ]
[ HAVING <search_condition> ]
[ WINDOW <window_definition> [ , ...n ] ]
[ QUALIFY <search_condition> ]
[ ORDER BY <order_by_expression> [ ASC | DESC ] [ , ...n ] ]
[ FOR <JSON> ]

<table_source> ::=
{
    [ database_name . [ schema_name ] . | schema_name . ]
        table_or_view_name [ [ AS ] table_alias ]
    | built_in_table_valued_function [ [ AS ] table_alias ]
        [ ( column_alias [ , ...n ] ) ]
    | user_defined_table_valued_function [ [ AS ] table_alias ]
        [ ( column_alias [ , ...n ] ) ]
    | derived_table [ [ AS ] table_alias ] [ ( column_alias [ , ...n ] ) ]
    | <joined_table>
    | <apply>
    | <pivoted_table>
    | <unpivoted_table>
}

```

The `SELECT` clause must appear immediately after all table sources, joins, and `APPLY` operators. If you omit the `SELECT` clause, `<select_list>` defaults to `*`.

## Arguments

#### \<table_source>

Specifies a table, view, built-in table-valued function, user-defined table-valued function, derived table, joined table, pivoted table, or unpivoted table, with or without an alias. Multiple table sources can be joined or separated by commas.

The order of table sources after the `FROM` keyword doesn't affect the result set. Duplicate names in the `FROM` clause produce an error. Duplicate unqualified column names can require table or alias qualification.

Queries that reference many table sources can require more compilation and optimization time. The practical number of table sources depends on available resources and query complexity.

#### *table_or_view_name*

The name of a table or view. Use a fully qualified name in the form *database*.*schema*.*object_name* when you reference an object in another database on the same SQL endpoint.

#### [AS] *table_alias*

An alias for a table source. Use an alias for convenience or to distinguish sources in joins, self-joins, and subqueries. After you define an alias, use the alias instead of the original table name to qualify columns. Derived tables, table-valued functions, `PIVOT`, and `UNPIVOT` can require an alias.

#### *built_in_table_valued_function*

Specifies a built-in function that returns a rowset. Supported Fabric table-valued functions include, but aren't limited to:

- [`OPENJSON`](/sql/t-sql/functions/openjson-transact-sql?view=fabric&preserve-view=true), which converts JSON text into rows and columns.
- [`OPENROWSET(BULK...)`](/sql/t-sql/functions/openrowset-bulk-transact-sql?view=fabric&preserve-view=true), which reads external files and returns their contents as rows.
- [`STRING_SPLIT`](/sql/t-sql/functions/string-split-transact-sql?view=fabric&preserve-view=true), which splits a delimited string into rows.
- [`GENERATE_SERIES`](/sql/t-sql/functions/generate-series-transact-sql?view=fabric&preserve-view=true), which generates a numeric series.
- `sys.fn_helpcollations`, which returns the collations supported by the SQL engine.

Built-in table-valued functions can define their own argument and output-column syntax. For example, `OPENJSON` can include a `WITH` clause that defines the returned columns.

#### *user_defined_table_valued_function*

Specifies a user-defined table-valued function. For syntax and behavior, see [CREATE FUNCTION (Microsoft Fabric, Azure Synapse Analytics)](../statements/create-function-sql-data-warehouse.md).

#### *derived_table*

A subquery that returns rows and acts as input to the outer query. A derived table requires a table alias. A table value constructor can also define a derived table.

#### *column_alias*

An optional alias that replaces a column name in the result of a derived table or table-valued function. If you specify a column alias list, provide an alias for every output column.

#### \<joined_table>

Specifies a table source produced by joining two or more table sources. For complete join syntax and behavior, see [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](from-transact-sql.md) and [Joins](../../relational-databases/performance/joins.md).

For supported optimizer and data-movement options, see [Join hints (Transact-SQL)](hints-transact-sql-join.md).

#### \<apply>

Specifies a `CROSS APPLY` or `OUTER APPLY` table source. For its complete syntax and behavior, see [Use APPLY](from-transact-sql.md#use-apply).

#### \<pivoted_table>

Specifies a table source transformed by the `PIVOT` operator. For its complete syntax and behavior, see [Using PIVOT and UNPIVOT](from-using-pivot-and-unpivot.md).

#### \<unpivoted_table>

Specifies a table source transformed by the `UNPIVOT` operator. For its complete syntax and behavior, see [Using PIVOT and UNPIVOT](from-using-pivot-and-unpivot.md).

#### SELECT \<select_list>

Specifies the columns and expressions returned by the query. For complete projection syntax, see [SELECT clause (Transact-SQL)](select-clause-transact-sql.md).

The `SELECT` clause is optional. If you omit it, the query returns all available columns, equivalent to `SELECT *`.

#### FOR \<JSON>

Specifies JSON formatting for the query result. For its complete syntax and behavior, see [FOR clause (Transact-SQL)](select-for-clause-transact-sql.md).

## Remarks

- SELECT-first and FROM-first syntax are both supported, but you can't combine them in the same query block.
- In FROM-first syntax, place `SELECT` immediately after the complete table-source expression, including any joins, `APPLY`, `PIVOT`, or `UNPIVOT` operators.
- A query block can contain only one `FROM` clause and one optional trailing `SELECT` clause.
- Omitting `SELECT` doesn't bypass existing restrictions on `SELECT *`, including restrictions in schema-bound objects.
- FROM-first syntax can be used in top-level queries, common table expressions (CTEs), subqueries, views, inline table-valued functions, and supported `INSERT` query forms.
- FROM-first syntax doesn't introduce a new `SELECT INTO` form.
- FROM-first and equivalent SELECT-first queries return the same result and have the same runtime behavior.

## Examples

### A. Specify SELECT after FROM

The following example declares the table source before the projected columns:

```sql
FROM dbo.Employee AS e
SELECT e.empId, e.name, e.dept;
```

This query is equivalent to:

```sql
SELECT e.empId, e.name, e.dept
FROM dbo.Employee AS e;
```

### B. Omit SELECT

The following query omits the trailing `SELECT` clause:

```sql
FROM dbo.Employee;
```

The query is equivalent to:

```sql
SELECT *
FROM dbo.Employee;
```

### C. Filter and group a FROM-first query

The following example declares the source, specifies the projection, and then filters and groups the rows:

```sql
FROM dbo.Employee AS e
SELECT e.dept, COUNT(*) AS EmployeeCount
WHERE e.managerId IS NOT NULL
GROUP BY e.dept;
```

### D. Use joins before SELECT

The trailing `SELECT` clause appears after all table sources and join conditions:

```sql
FROM Sales.SalesOrderHeader AS soh
INNER JOIN Sales.SalesOrderDetail AS sod
    ON soh.SalesOrderID = sod.SalesOrderID
SELECT
    soh.SalesOrderID,
    soh.OrderDate,
    sod.ProductID,
    sod.LineTotal;
```

### E. Use FROM-first syntax in a CTE

The following example defines and consumes a CTE with FROM-first query blocks:

```sql
WITH managers AS (
    FROM dbo.Employee
    SELECT empId, name, dept
    WHERE managerId IS NOT NULL
)
FROM managers
SELECT empId, name, dept;
```

### F. Use FROM-first syntax in an EXISTS subquery

The following example uses FROM-first syntax in both the outer query and the correlated subquery:

```sql
FROM dbo.Employee AS e
SELECT e.empId, e.name, e.dept
WHERE EXISTS (
    FROM dbo.Employee AS e2
    WHERE e2.dept = e.dept
      AND e2.empId <> e.empId
);
```

The omitted `SELECT` in the `EXISTS` subquery defaults to `SELECT *`. The `EXISTS` predicate tests only whether the subquery returns a row.

### G. Use FROM-first syntax with INSERT

The following example inserts rows returned by a FROM-first query:

```sql
INSERT INTO dbo.EmployeeArchive
FROM dbo.Employee
SELECT empId, name, dept
WHERE dept = 'Accounting';
```

## Related content

- [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](from-transact-sql.md)
- [SELECT clause (Transact-SQL)](select-clause-transact-sql.md)
- [SELECT (Transact-SQL)](select-transact-sql.md)
- [Joins (SQL Server)](../../relational-databases/performance/joins.md)
