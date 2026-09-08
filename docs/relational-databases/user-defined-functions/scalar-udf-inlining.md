---
title: Scalar UDF Inlining
description: The scalar UDF inlining feature improves performance of queries that invoke scalar UDFs in SQL Server 2019 and later versions.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: karam, wiassaf, srdjanmatin
ms.date: 06/29/2026
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-ver15 || >=sql-server-linux-ver15 || =fabric || =fabric-sqldb"
---
# Scalar UDF inlining

[!INCLUDE [SQL Server 2019 SQL Database SQL Managed Instance FabricSE Fabric DW FabricSQLDB](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

This article introduces scalar UDF inlining, a feature under the [Intelligent query processing in SQL databases](../performance/intelligent-query-processing.md) suite of features. This feature improves the performance of queries that invoke scalar UDFs in [!INCLUDE [sssql19](../../includes/sssql19-md.md)] and later versions. 

## T-SQL scalar user-defined functions

User-defined functions (UDFs) that are implemented in [!INCLUDE [tsql](../../includes/tsql-md.md)] and return a single data value are referred to as T-SQL scalar user-defined functions. T-SQL UDFs are an elegant way to achieve code reuse and modularity across [!INCLUDE [tsql](../../includes/tsql-md.md)] queries. Some computations, such as complex business rules, are easier to express in imperative UDF form. UDFs help you build such logic without requiring expertise in writing SQL queries. For more information about UDFs, see [Create user-defined functions (Database Engine)](create-user-defined-functions-database-engine.md).

## Performance of scalar UDFs

Scalar UDFs typically perform poorly for the following reasons:

- **Iterative invocation.** The [SQL Database Engine](../../database-engine/sql-database-engine.md) invokes UDFs iteratively, once per qualifying tuple. This process adds extra cost because of repeated context switching due to function invocation. UDFs that execute [!INCLUDE [tsql](../../includes/tsql-md.md)] queries in their definition are severely affected.

- **Lack of costing.** During optimization, the database engine costs only relational operators, while it doesn't cost scalar operators. Before the introduction of scalar UDFs, other scalar operators were generally cheap and didn't require costing. A small CPU cost added for a scalar operation was enough. There are scenarios where the actual cost is significant, but the optimizer still underrepresents it.

- **Interpreted execution.** The database engine evaluates UDFs as a batch of statements and executes them statement by statement. Each statement is compiled, and the compiled plan is cached. Although this caching strategy saves some time by avoiding recompilations, each statement executes in isolation. The database engine carries out no cross-statement optimizations.

- **Serial execution.** [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't allow intra-query parallelism in queries that invoke UDFs.

## Automatic inlining of scalar UDFs

The goal of the scalar UDF inlining feature is to improve the performance of queries that invoke T-SQL scalar UDFs, where UDF execution is the main bottleneck.

By using the UDF inlining feature, the database engine automatically transforms scalar UDFs into scalar expressions or scalar subqueries. The database engine substitutes these expressions or subqueries in the calling query in place of the UDF operator. The query optimizer then optimizes these expressions and subqueries. As a result, the query plan no longer has a user-defined function operator, but you can observe its effects in the plan, like views or inline table-valued functions (TVFs).

### Automatic inlining of scalar UDFs in Microsoft Fabric Data Warehouse

In Microsoft Fabric Data Warehouse, scalar UDF inlining is available as a preview feature. 

In Fabric Data Warehouse scalar UDFs are automatically inlined at compile time when the function body and the calling query meet requirements for inlining. For more information, see [CREATE FUNCTION](../../t-sql/statements/create-function-sql-data-warehouse.md?view=fabric&preserve-view=true) and [Scalar UDF inlining](../../relational-databases/user-defined-functions/scalar-udf-inlining.md?view=fabric&preserve-view=true). For examples of code changes to make a non-inlinable UDF inlinable, see [How to create scalar user-defined functions in Fabric Data Warehouse (preview)](/fabric/data-warehouse/how-to-inline-udf).

## Examples

The examples in this section use the TPC-H benchmark database. For more information, see the [TPC-H Homepage](https://www.tpc.org/tpch/).

### A. Single statement scalar UDF

Consider the following query.

```sql
SELECT L_SHIPDATE,
       O_SHIPPRIORITY,
       SUM(L_EXTENDEDPRICE * (1 - L_DISCOUNT))
FROM LINEITEM
     INNER JOIN ORDERS
         ON O_ORDERKEY = L_ORDERKEY
GROUP BY L_SHIPDATE, O_SHIPPRIORITY
ORDER BY L_SHIPDATE;
```

This query computes the sum of discounted prices for line items and presents the results grouped by the shipping date and shipping priority. The expression `L_EXTENDEDPRICE *(1 - L_DISCOUNT)` is the formula for the discounted price for a given line item. Such formulas can be extracted into functions for the benefit of modularity and reuse.

```sql
CREATE FUNCTION dbo.discount_price
(
    @price DECIMAL (12, 2),
    @discount DECIMAL (12, 2)
)
RETURNS DECIMAL (12, 2)
AS
BEGIN
    RETURN @price * (1 - @discount);
END
```

Now the query can be modified to invoke this UDF.

```sql
SELECT L_SHIPDATE,
       O_SHIPPRIORITY,
       SUM(dbo.discount_price(L_EXTENDEDPRICE, L_DISCOUNT))
FROM LINEITEM
     INNER JOIN ORDERS
         ON O_ORDERKEY = L_ORDERKEY
GROUP BY L_SHIPDATE, O_SHIPPRIORITY
ORDER BY L_SHIPDATE;
```

The query with the UDF performs poorly, due to the reasons outlined previously. With scalar UDF inlining, the scalar expression in the body of the UDF is substituted directly in the query. The results of running this query are shown in the following table:

| Query | Query without UDF | Query with UDF (without inlining) | Query with scalar UDF inlining |
| --- | --- | --- | --- |
| `Execution time` | 1.6 seconds | 29 minutes 11 seconds | 1.6 seconds |

These numbers are based on a 10-GB CCI database (using the TPC-H schema), running on a machine with dual processor (12 core), 96-GB RAM, backed by SSD. The numbers include compilation and execution time with a cold procedure cache and buffer pool. The default configuration was used, and no other indexes were created.

### B. Multi-statement scalar UDF

You can also inline scalar UDFs with multiple T-SQL statements, such as variable assignments and conditional branching. Consider the following scalar UDF that, given a customer key, determines the service category for that customer. It arrives at the category by first computing the total price of all orders placed by the customer by using a SQL query. Then, it uses an `IF (...) ELSE` logic to decide the category based on the total price.

```sql
CREATE OR ALTER FUNCTION dbo.customer_category (@ckey INT)
RETURNS CHAR (10)
AS
BEGIN
    DECLARE @total_price AS DECIMAL (18, 2);
    DECLARE @category AS CHAR (10);
    SELECT @total_price = SUM(O_TOTALPRICE)
    FROM ORDERS
    WHERE O_CUSTKEY = @ckey;
    IF @total_price < 500000
        SET @category = 'REGULAR';
    ELSE
        IF @total_price < 1000000
            SET @category = 'GOLD';
        ELSE
            SET @category = 'PLATINUM';
    RETURN @category;
END
```

Now, consider a query that invokes this UDF.

```sql
SELECT C_NAME,
       dbo.customer_category(C_CUSTKEY)
FROM CUSTOMER;
```

The execution plan for this query in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] (compatibility level 140 and earlier) is as follows:

:::image type="content" source="media/scalar-udf-inlining/query-plan-without-udf-inlining.png" alt-text="Screenshot of Query Plan without inlining." lightbox="media/scalar-udf-inlining/query-plan-without-udf-inlining.png":::

As the plan shows, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] adopts the following basic strategy: for every tuple in the `CUSTOMER` table, invoke the UDF and output the results. This strategy is naive and inefficient. By using inlining, you can transform such UDFs into equivalent scalar subqueries, which the calling query substitutes in place of the UDF.

For the same query, the plan with the UDF inlined looks as follows.

:::image type="content" source="media/scalar-udf-inlining/query-plan-with-udf-inlining.png" alt-text="Screenshot of Query Plan with inlining." lightbox="media/scalar-udf-inlining/query-plan-with-udf-inlining.png":::

As mentioned earlier, the query plan no longer has a user-defined function operator, but you can now see its effects in the plan, like views or inline TVFs. Here are some key observations from the previous plan:

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] infers the implicit join between `CUSTOMER` and `ORDERS` and makes it explicit via a join operator.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] also infers the implicit `GROUP BY O_CUSTKEY on ORDERS` and uses the IndexSpool + StreamAggregate to implement it.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is now using parallelism across all operators.

Depending on the complexity of the logic in the UDF, the resulting query plan might also get bigger and more complex. As you can see, the operations inside the UDF are now no longer opaque, so the query optimizer can cost and optimize those operations. Also, since the UDF is no longer in the plan, iterative UDF invocation is replaced by a plan that completely avoids function call overhead.

<a id="requirements"></a>

## Inlineable scalar UDF requirements

A scalar T-SQL UDF can be inlined if the function definition uses allowed constructs, and the function is used in a context that enables inlining:

All of the following conditions of the *UDF definition* must be true:

- The UDF is written using the following constructs:
  - `DECLARE`, `SET`: Variable declaration and assignments.
  - `SELECT`: SQL query with single/multiple variable assignments <sup>1</sup>.
  - `IF`/`ELSE`: Branching with arbitrary levels of nesting.
  - `RETURN`: Single or multiple return statements. Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU5, the UDF can only contain a single RETURN statement to be considered for inlining <sup>6</sup>.
  - `UDF`: Nested/recursive function calls <sup>2</sup>.
  - Others: Relational operations such as `EXISTS`, `IS NULL`.
- The UDF doesn't invoke any intrinsic function that is either time-dependent (such as `GETDATE()`) or has side effects <sup>3</sup> (such as `NEWSEQUENTIALID()`).
- The UDF uses the `EXECUTE AS CALLER` clause (default behavior if the `EXECUTE AS` clause isn't specified).
- The UDF doesn't reference table variables or table-valued parameters.
- The UDF isn't natively compiled (interop is supported).
- The UDF doesn't reference user-defined types.
- There are no signatures added to the UDF <sup>9</sup>.
- The UDF isn't a partition function.
- The UDF doesn't contain references to Common Table Expressions (CTEs).
- The UDF doesn't contain references to intrinsic functions that might alter the results when inlined (such as `@@ROWCOUNT`) <sup>4</sup>.
- The UDF doesn't contain aggregate functions being passed as parameters to a scalar UDF <sup>4</sup>.
- The UDF doesn't reference built-in views (such as `OBJECT_ID`) <sup>4</sup>.
- The UDF doesn't reference XML methods <sup>5</sup>.
- The UDF doesn't contain a SELECT with `ORDER BY` without a `TOP 1` clause <sup>5</sup>.
- The UDF doesn't contain a SELECT query that performs an assignment with the `ORDER BY` clause (such as `SELECT @x = @x + 1 FROM table1 ORDER BY col1`) <sup>5</sup>.
- The UDF doesn't contain multiple RETURN statements <sup>6</sup>.
- The UDF doesn't reference the `STRING_AGG` function <sup>6</sup>.
- The UDF doesn't reference remote tables <sup>7</sup>.
- The UDF doesn't reference encrypted columns <sup>8</sup>.
- The UDF doesn't contain references to `WITH XMLNAMESPACES` <sup>8</sup>.
- If the UDF definition runs into thousands of lines of code, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might choose not to inline it.

<sup>1</sup> `SELECT` with variable accumulation/aggregation isn't supported for inlining (such as `SELECT @val += col1 FROM table1`).

<sup>2</sup> Recursive UDFs are inlined to a certain depth only.

<sup>3</sup> Intrinsic functions whose results depend upon the current system time are time-dependent. An intrinsic function that might update some internal global state is an example of a function with side effects. Such functions return different results each time they're called, based on the internal state.

<sup>4</sup> Restriction added in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 2

<sup>5</sup> Restriction added in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 4

<sup>6</sup> Restriction added in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 5

<sup>7</sup> Restriction added in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 6

<sup>8</sup> Restriction added in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 11

<sup>9</sup> Because signatures could be added and dropped after a UDF is created, the decision whether to inline is done when the query referencing a scalar UDF is compiled. For example, system functions are typically signed with a certificate. You can use [sys.crypt_properties](../system-catalog-views/sys-crypt-properties-transact-sql.md) to find which objects are signed.

All of the following requirement of the *execution context* must be true:

- The UDF isn't used in `ORDER BY` clause.
- The query invoking a scalar UDF doesn't reference a scalar UDF call in its `GROUP BY` clause.
- The query invoking a scalar UDF in its select list with `DISTINCT` clause doesn't have an `ORDER BY` clause.
- The UDF isn't called from a RETURN statement <sup>1</sup>.
- The query invoking the UDF doesn't have common table expressions (CTEs) <sup>3</sup>.
- The UDF-calling query doesn't use `GROUPING SETS`, `CUBE`, or `ROLLUP` <sup>2</sup>.
- The UDF-calling query doesn't contain a variable that you use as a UDF parameter for assignment (for example, `SELECT @y = 2`, `@x = UDF(@y)`) <sup>2</sup>.
- You don't use the UDF in a computed column or a check constraint definition.

<sup>1</sup> Restriction added in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 5

<sup>2</sup> Restriction added in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 6

<sup>3</sup> Restriction added in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 11

For information about the latest T-SQL scalar UDF inlining fixes and changes to inlining eligibility scenarios, see the Knowledge Base article: [FIX: scalar UDF inlining issues in SQL Server 2019](https://support.microsoft.com/help/4538581).

### Check whether a UDF can be inlined

For every T-SQL scalar UDF, the [sys.sql_modules](../system-catalog-views/sys-sql-modules-transact-sql.md) catalog view includes a property called `is_inlineable`, which indicates whether a UDF is inlineable.

The `is_inlineable` property is derived from the constructs found inside the UDF definition. It doesn't check whether the UDF is in fact inlineable at compile time. For more information, see the [conditions for inlining](#requirements).

A value of `1` indicates that the UDF is inlineable, and `0` indicates otherwise. This property has a value of `1` for all inline TVFs as well. For all other modules, the value is `0`.

If a scalar UDF is inlineable, it doesn't imply that it's always inlined. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] decides (on a per-query, per-UDF basis) whether to inline a UDF. Refer to the lists of requirements earlier in this article.

  ```sql
  SELECT b.name,
         b.type_desc,
         a.is_inlineable
  FROM sys.sql_modules AS a
       INNER JOIN sys.objects AS b
           ON a.object_id = b.object_id
  WHERE b.type IN ('IF', 'TF', 'FN');
  ```

### Check whether inlining has happened

If all the preconditions are satisfied and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] decides to perform inlining, it transforms the UDF into a relational expression. From the query plan, you can figure out whether inlining occurred:

- The plan XML doesn't have a `<UserDefinedFunction>` XML node for a UDF that is inlined successfully.
- Certain Extended Events are emitted.

## Enable scalar UDF inlining

You can make workloads automatically eligible for scalar UDF inlining by enabling compatibility level 150 for the database. You can set this using [!INCLUDE [tsql](../../includes/tsql-md.md)]. For example:

```sql
ALTER DATABASE [WideWorldImportersDW]
    SET COMPATIBILITY_LEVEL = 150;
```

Apart from this step, there are no other changes required to be made to UDFs or queries to take advantage of this feature.

## Disable scalar UDF inlining without changing the compatibility level

You can disable scalar UDF inlining at the database, statement, or UDF scope while still maintaining database compatibility level 150 and higher. To disable scalar UDF inlining at the database scope, run the following statement within the context of the applicable database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET TSQL_SCALAR_UDF_INLINING = OFF;
```

To re-enable scalar UDF inlining for the database, run the following statement within the context of the applicable database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET TSQL_SCALAR_UDF_INLINING = ON;
```

When you set this option to `ON`, it appears as enabled in [sys.database_scoped_configurations](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md).

You can also disable scalar UDF inlining for a specific query by designating `DISABLE_TSQL_SCALAR_UDF_INLINING` as a `USE HINT` query hint.

A `USE HINT` query hint takes precedence over the database scoped configuration or compatibility level setting.

For example:

```sql
SELECT L_SHIPDATE,
       O_SHIPPRIORITY,
       SUM(dbo.discount_price(L_EXTENDEDPRICE, L_DISCOUNT))
FROM LINEITEM
     INNER JOIN ORDERS
         ON O_ORDERKEY = L_ORDERKEY
GROUP BY L_SHIPDATE, O_SHIPPRIORITY
ORDER BY L_SHIPDATE
OPTION (USE HINT('DISABLE_TSQL_SCALAR_UDF_INLINING'));
```

You can also disable scalar UDF inlining for a specific UDF by using the INLINE clause in the `CREATE FUNCTION` or `ALTER FUNCTION` statement.
For example:

```sql
CREATE OR ALTER FUNCTION dbo.discount_price
(
    @price DECIMAL (12, 2),
    @discount DECIMAL (12, 2)
)
RETURNS DECIMAL (12, 2)
WITH INLINE = OFF
AS
BEGIN
    RETURN @price * (1 - @discount);
END
```

After you run the previous statement, this UDF is never inlined into any query that invokes it. To re-enable inlining for this UDF, run the following statement:

```sql
CREATE OR ALTER FUNCTION dbo.discount_price
(
    @price DECIMAL (12, 2),
    @discount DECIMAL (12, 2)
)
RETURNS DECIMAL (12, 2)
WITH INLINE = ON
AS
BEGIN
    RETURN @price * (1 - @discount);
END
```

The `INLINE` clause isn't mandatory. If you don't specify the `INLINE` clause, it's automatically set to `ON` or `OFF` based on whether the UDF can be inlined. If you specify `INLINE = ON` but the UDF is found ineligible for inlining, an error is thrown.

## Remarks

As described in this article, scalar UDF inlining transforms a query with scalar UDFs into a query with an equivalent scalar subquery. Because of this transformation, you might notice some differences in behavior in the following scenarios:

- Inlining results in a different query hash for the same query text.

- Certain warnings in statements inside the UDF (such as divide by zero) that might be previously hidden, can show up due to inlining.

- Query level join hints might not be valid anymore, as inlining can introduce new joins. You must use local join hints instead.

- You can't index views that reference inline scalar UDFs. If you need to create an index on such views, disable inlining for the referenced UDFs.

- There might be some differences in the behavior of [Dynamic data masking](../security/dynamic-data-masking.md) with UDF inlining.

  In certain situations (depending upon the logic in the UDF), inlining might be more conservative with respect to masking output columns. In scenarios where the columns referenced in a UDF aren't output columns, they aren't masked.

- If a UDF references built-in functions such as `SCOPE_IDENTITY()`, `@@ROWCOUNT`, or `@@ERROR`, the value returned by the built-in function changes with inlining. This change in behavior is because inlining changes the scope of statements inside the UDF. Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU2, inlining is blocked if the UDF references certain intrinsic functions (for example `@@ROWCOUNT`).

- If you assign a variable with the result of an inlined UDF and also use it as `index_column_name` in `FORCESEEK` [Query hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md), it results in error 8622. This error indicates that the query processor couldn't produce a query plan because of the hints defined in the query.

## Related content

- [Create user-defined functions (Database Engine)](create-user-defined-functions-database-engine.md)
- [Performance Center for SQL Server Database Engine and Azure SQL Database](../performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md)
- [Query processing architecture guide](../query-processing-architecture-guide.md)
- [Logical and physical showplan operator reference](../showplan-logical-and-physical-operators-reference.md)
- [Joins (SQL Server)](../performance/joins.md)
- [Demonstrating Intelligent Query Processing](https://aka.ms/IQPDemos)
- [FIX: scalar UDF inlining issues in SQL Server 2019](https://support.microsoft.com/help/4538581)
