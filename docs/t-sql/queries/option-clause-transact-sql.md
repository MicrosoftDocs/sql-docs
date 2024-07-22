---
title: "OPTION clause (Transact-SQL)"
description: The OPTION clause specifies that the indicated query hint should be used throughout the entire query.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, randolphwest
ms.date: 06/07/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - build-2024
f1_keywords:
  - "OPTION clause"
  - "OPTION_TSQL"
  - "OPTION"
  - "OPTION_clause_TSQL"
helpviewer_keywords:
  - "clauses [SQL Server], OPTION"
  - "OPTION clause"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# OPTION clause (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Specifies that the indicated query hint should be used throughout the entire query. Each query hint can be specified only one time, although multiple query hints are permitted. Only one `OPTION` clause can be specified with the statement.

This clause can be specified in the `SELECT`, `DELETE`, `UPDATE`, and `MERGE` statements.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE [ssnoversion-md.md](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]:

```syntaxsql
[ OPTION ( <query_hint> [ , ...n ] ) ]
```

Syntax for [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]:

```syntaxsql
OPTION ( <query_option> [ , ...n ] )

<query_option> ::=
    LABEL = label_name |
    <query_hint>

<query_hint> ::=
    HASH JOIN
    | LOOP JOIN
    | MERGE JOIN
    | FORCE ORDER
    | { FORCE | DISABLE } EXTERNALPUSHDOWN
    | FOR TIMESTAMP AS OF '<point_in_time>'
```

Syntax for [!INCLUDE [ssazuresynapse-md.md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [sspdw-md.md](../../includes/sspdw-md.md)] and [!INCLUDE [fabric-dw](../../includes/fabric-se.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]:

```syntaxsql
OPTION ( <query_option> [ , ...n ] )

<query_option> ::=
    LABEL = label_name |
    <query_hint>

<query_hint> ::=
    HASH JOIN
    | LOOP JOIN
    | MERGE JOIN
    | FORCE ORDER
    | { FORCE | DISABLE } EXTERNALPUSHDOWN
```

Syntax for [!INCLUDE [sssodfull-md.md](../../includes/sssodfull-md.md)]:

```syntaxsql
OPTION ( <query_option> [ , ...n ] )

<query_option> ::=
    LABEL = label_name
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *query_hint*

Keywords that indicate which optimizer hints are used to customize the way the Database Engine processes the statement. For more information, see [Query Hints](hints-transact-sql-query.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### <a id="a-using-an-option-clause-with-a-group-by-clause"></a> A. Use an OPTION clause with a GROUP BY clause

The following example shows how the `OPTION` clause is used with a `GROUP BY` clause.

```sql
USE AdventureWorks2022;
GO

SELECT ProductID,
    OrderQty,
    SUM(LineTotal) AS Total
FROM Sales.SalesOrderDetail
WHERE UnitPrice < $5.00
GROUP BY ProductID, OrderQty
ORDER BY ProductID, OrderQty
OPTION (HASH GROUP, FAST 10);
GO
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### B. SELECT statement with a label in the OPTION clause

The following example shows an [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] `SELECT` statement with a label in the `OPTION` clause.

```sql
SELECT * FROM FactResellerSales
OPTION (LABEL = 'q17');
```

### C. SELECT statement with a query hint in the OPTION clause

The following example shows a `SELECT` statement that uses a `HASH JOIN` query hint in the `OPTION` clause.

```sql
-- Uses AdventureWorks

SELECT COUNT(*) FROM dbo.DimCustomer a
INNER JOIN dbo.FactInternetSales b
    ON (a.CustomerKey = b.CustomerKey)
OPTION (HASH JOIN);
```

### D. SELECT statement with a label and multiple query hints in the OPTION clause

The following example is a [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] `SELECT` statement that contains a label and multiple query hints. When the query is run on the Compute nodes, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] applies a hash join or merge join, according to the strategy that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] decides is the most optimal.

```sql
SELECT COUNT(*) FROM dbo.DimCustomer a
INNER JOIN dbo.FactInternetSales b
    ON (a.CustomerKey = b.CustomerKey)
OPTION (Label = 'CustJoin', HASH JOIN, MERGE JOIN);
```

### <a id="e-using-a-query-hint-when-querying-a-view"></a> E. Use a query hint when querying a view

The following example creates a view named CustomerView and then uses a `HASH JOIN` query hint in a query that references a view and a table.

```sql
CREATE VIEW CustomerView
AS
SELECT CustomerKey,
    FirstName,
    LastName
FROM ssawPDW..DimCustomer;
GO

SELECT COUNT(*)
FROM dbo.CustomerView a
INNER JOIN dbo.FactInternetSales b
    ON (a.CustomerKey = b.CustomerKey)
OPTION (HASH JOIN);
GO

DROP VIEW CustomerView;
GO
```

### F. Query with a subselect and a query hint

The following example shows a query that contains both a subselect and a query hint. The query hint is applied globally. Query hints can't be appended to the subselect statement.

```sql
CREATE VIEW CustomerView
AS
SELECT CustomerKey,
    FirstName,
    LastName
FROM ssawPDW..DimCustomer;
GO

SELECT *
FROM (
    SELECT COUNT(*) AS a
    FROM dbo.CustomerView a
    INNER JOIN dbo.FactInternetSales b
        ON (a.CustomerKey = b.CustomerKey)
) AS t
OPTION (HASH JOIN);
```

### G. Force the join order to match the order in the query

The following example uses the `FORCE ORDER` hint to force the query plan to use the join order specified by the query. This hint improves performance on some queries, but not all queries.

This query obtains partition numbers, boundary values, boundary value types, and rows per boundary for the partitions in the `ProspectiveBuyer` table of the `ssawPDW` database.

```sql
SELECT sp.partition_number,
    prv.value AS boundary_value,
    lower(sty.name) AS boundary_value_type,
    sp.rows
FROM sys.tables st
INNER JOIN sys.indexes si
    ON st.object_id = si.object_id AND si.index_id < 2
INNER JOIN sys.partitions sp
    ON sp.object_id = st.object_id AND sp.index_id = si.index_id
INNER JOIN sys.partition_schemes ps
    ON ps.data_space_id = si.data_space_id
INNER JOIN sys.partition_range_values prv
    ON prv.function_id = ps.function_id
INNER JOIN sys.partition_parameters pp
    ON pp.function_id = ps.function_id
INNER JOIN sys.types sty
    ON sty.user_type_id = pp.user_type_id AND prv.boundary_id = sp.partition_number
WHERE st.object_id = (
    SELECT object_id
    FROM sys.objects
    WHERE name = 'FactResellerSales'
)
ORDER BY sp.partition_number
OPTION (FORCE ORDER);
```

### <a id="h-using-externalpushdown"></a> H. Use EXTERNALPUSHDOWN

The following example forces the pushdown of the `WHERE` clause to the MapReduce job on the external Hadoop table.

```sql
SELECT ID FROM External_Table_AS A
WHERE ID < 1000000
OPTION (FORCE EXTERNALPUSHDOWN);
```

The following example prevents the pushdown of the `WHERE` clause to the MapReduce job on the external Hadoop table. All rows are returned to PDW where the `WHERE` clause is applied.

```sql
SELECT ID FROM External_Table_AS A
WHERE ID < 10
OPTION (DISABLE EXTERNALPUSHDOWN);
```

### I. Query data as of a point in time

**Applies to**: [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)]

For more information, see [FOR TIMESTAMP query hint](hints-transact-sql-query.md#for-timestamp).

Use the `TIMESTAMP` syntax in the `OPTION` clause to query data as it existed in the past, in Synapse Data Warehouse in Microsoft Fabric. The following sample query returns data as it appeared on March 13, 2024 at 7:39:35.28 PM UTC. The time zone is always in UTC.

```sql
SELECT OrderDateKey,
    SUM(SalesAmount) AS TotalSales
FROM FactInternetSales
GROUP BY OrderDateKey
ORDER BY OrderDateKey
OPTION (FOR TIMESTAMP AS OF '2024-03-13T19:39:35.28');--March 13, 2024 at 7:39:35.28 PM UTC
```

## Related content

- [Query hints (Transact-SQL)](hints-transact-sql-query.md)
- [SELECT (Transact-SQL)](select-transact-sql.md)
- [UPDATE (Transact-SQL)](update-transact-sql.md)
- [MERGE (Transact-SQL)](../statements/merge-transact-sql.md)
- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
