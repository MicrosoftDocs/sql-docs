---
title: "Scalar UDF Inlining in Microsoft SQL databases | Microsoft Docs"
description: "Scalar UDF Inlining feature to improve performance of queries that invoke scalar UDFs in SQL Server (2018 and later), and Azure SQL Database."
ms.custom: ""
ms.date: 02/28/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
ms.tgt_pltfrm: ""
ms.topic: conceptual
helpviewer_keywords: 
ms.assetid: 
author: "s-r-k"
ms.author: "karam"
manager: craigg
monikerRange: "= azuresqldb-current || >= sql-server-ver15 || = sqlallproducts-allversions"
---
# Scalar UDF Inlining

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

This article introduces Scalar UDF inlining, a feature under the intelligent query processing suite of features. This feature improves the performance of queries that invoke scalar UDFs in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQLv15](../../includes/sssqlv15-md.md)]) and [!INCLUDE[ssSDS](../../includes/sssds-md.md)].

## T-SQL Scalar User-Defined Functions

User-Defined Functions that are implemented in Transact-SQL and return a single data value are referred to as T-SQL Scalar User-Defined Functions. T-SQL UDFs are an elegant way to achieve code reuse and modularity across SQL queries. Some computations (such as complex business rules) are easier to express in imperative UDF form. UDFs help in building up complex logic without requiring expertise in writing complex SQL queries.

## Performance of Scalar UDFs

Scalar UDFs typically end up performing poorly due to the following reasons.

- **Iterative invocation:** UDFs are invoked in an iterative manner, once per qualifying tuple. This incurs additional costs of repeated context switching due to function invocation. Especially, UDFs that execute SQL queries in their definition are severely affected.
- **Lack of costing:** During optimization, only relational operators are costed, while scalar operators are not. Prior to the introduction of scalar UDFs, other scalar operators were generally cheap and did not require costing. A small CPU cost added for a scalar operation was enough. There are scenarios where the actual cost is significant, and yet still remains underrepresented.
- **Interpreted execution:** UDFs are evaluated as a batch of statements, executed statement-by-statement. Each statement itself is compiled, and the compiled plan is cached. Although this caching strategy saves some time as it avoids recompilations, each statement executes in isolation. No cross-statement optimizations are carried out.
- **Serial execution:** SQL Server does not allow intra-query parallelism in queries that invoke UDFs. 

## Automatic Inlining of Scalar UDFs

The goal of the Scalar UDF inlining feature is to improve performance of queries that invoke T-SQL scalar UDFs, where UDF execution is the main bottleneck.

With this new feature, scalar UDFs are automatically transformed into scalar expressions or scalar subqueries that are substituted in the calling query in place of the UDF operator. These expressions and subqueries are then optimized. As a result, the query plan will no longer have a user-defined function operator, but its effects will be observed in the plan, like views or inline TVFs.

### Example 1 - Single statement scalar UDF

Consider the following query

```sql
SELECT L_SHIPDATE, O_SHIPPRIORITY, SUM (L_EXTENDEDPRICE *(1 - L_DISCOUNT)) 
FROM LINEITEM
INNER JOIN ORDERS
  ON O_ORDERKEY = L_ORDERKEY 
GROUP BY L_SHIPDATE, O_SHIPPRIORITY ORDER BY L_SHIPDATE;
```

This query computes the sum of discounted prices for line items and presents the results grouped by the shipping date and shipping priority. The expression `L_EXTENDEDPRICE *(1 - L_DISCOUNT)` is the formula for the discounted price for a given line item. Such formulas can be extracted into functions for the benefit of modularity and reuse.

```sql
CREATE FUNCTION dbo.discount_price(@price DECIMAL(12,2), @discount DECIMAL(12,2)) 
RETURNS DECIMAL (12,2) AS
BEGIN
  RETURN @price * (1 - @discount);
END

```

Now the query can be modified to invoke this UDF.

```sql
SELECT L_SHIPDATE, O_SHIPPRIORITY, SUM (dbo.discount_price(L_EXTENDEDPRICE, L_DISCOUNT)) 
FROM LINEITEM
INNER JOIN ORDERS
  ON O_ORDERKEY = L_ORDERKEY 
GROUP BY L_SHIPDATE, O_SHIPPRIORITY ORDER BY L_SHIPDATE
```

Due to the reasons outlined earlier, the query with the UDF performs poorly. Now, with scalar UDF inlining, the scalar expression in the body of the UDF is substituted directly in the query. The results of running this query  are shown in the below table:

| Query: | Query without UDF | Query with UDF (without inlining) | Query with scalar UDF inlining | 
| --- | --- | --- | --- |
| Execution time: | 1.6 seconds | 29 minutes 11 seconds | 1.6 seconds |

These numbers are based on a 10-GB CCI database (using the TPC-H schema), running on a machine with dual processor (12 core), 96-GB RAM, backed by SSD. The numbers include compilation and execution time with a cold procedure cache and buffer pool. The default configuration was used, and no other indexes were created.

### Example 2 - Multi-statement scalar UDF

Scalar UDFs that are implemented using multiple T-SQL statements such as variable assignments and conditional branching can also be inlined. Consider the following scalar UDF that, given a customer key, determines the service category for that customer. It arrives at the category by first computing the total price of all orders placed by the customer using a SQL query. Then, it uses an `IF-ELSE` logic to decide the category based on the total price.

```sql
CREATE OR ALTER FUNCTION dbo.customer_category(@ckey INT) 
RETURNS CHAR(10) AS
BEGIN
  DECLARE @total_price DECIMAL(18,2);
  DECLARE @category CHAR(10);

  SELECT @total_price = SUM(O_TOTALPRICE) FROM ORDERS WHERE O_CUSTKEY = @ckey;

  IF @total_price < 500000
    SET @category = 'REGULAR';
  ELSE IF @total_price < 1000000
    SET @category = 'GOLD';
  ELSE 
    SET @category = 'PLATINUM';

  RETURN @category;
END

```

Now, consider a query that invokes this UDF.

```sql
SELECT C_NAME, dbo.customer_category(C_CUSTKEY) FROM CUSTOMER;
```

The execution plan for this query in SQL Server 2017 (compatibility level 140 and earlier) is as follows:

![Query Plan without inlining](./media/query-plan-without-udf-inlining.png)

As the plan shows, SQL Server adopts a simple strategy here: for every tuple in the `CUSTOMER` table, invoke the UDF and output the results. This strategy is naïve and inefficient. With inlining, such UDFs are transformed into equivalent scalar subqueries, which are substituted in the calling query in place of the UDF.

For the same query, the plan with the UDF inlined looks as below.

![Query Plan with inlining](./media/query-plan-with-udf-inlining.png)

As mentioned earlier, the query plan no longer has a user-defined function operator, but its effects are now observable in the plan, like views or inline TVFs. Here are some key observations from the above plan:

1. SQL Server has inferred the implicit join between `CUSTOMER` and `ORDERS` and made that explicit via a join operator.
2. SQL Server has also inferred the implicit `GROUP BY O_CUSTKEY on ORDERS` and has used the IndexSpool + StreamAggregate to implement it.
3. SQL Server is now using parallelism across all operators.

Depending upon the complexity of the logic in the UDF, the resulting query plan might also get bigger and more complex. As we can see, the operations inside the UDF are now no longer a black box, and hence the query optimizer is able to cost and optimize those operations. Also, since the UDF is no longer in the plan, iterative UDF invocation is replaced by a plan that completely avoids function call overhead.

## Inlineable Scalar UDFs requirements

A scalar T-SQL UDF can be inline if all of the following conditions are true:

- The UDF is written using the following constructs:
    - `DECLARE`, `SET`: Variable declaration and assignments.
    - `SELECT`: SQL query with single/multiple variable assignments<sup>1</sup>.
    - `IF`/`ELSE`: Branching with arbitrary levels of nesting.
    - `RETURN`: Single or multiple return statements.
    - `UDF`: Nested/recursive function calls<sup>2</sup>.
    - Others: Relational operations such as `EXISTS`, `ISNULL`.
- The UDF does not invoke any intrinsic function that is either time-dependent (such as `GETDATE()`) or has side effects<sup>3</sup>  (such as `NEWSEQUENTIALID()`).
- The UDF uses the `EXECUTE AS CALLER` clause (the default behavior if the `EXECUTE AS` clause is not specified).
- The UDF does not reference table variables or table-valued parameters.
- The query invoking a scalar UDF does not reference a scalar UDF call in its `GROUP BY` clause.
- The query invoking a scalar UDF in its select list with `DISTINCT` clause does not reference a scalar UDF call in its `ORDER BY` clause.
- The UDF is not natively compiled (interop is supported).
- The UDF is not used in a computed column or a check constraint definition.
- The UDF does not reference user-defined types.
- There are no signatures added to the UDF.
- The UDF is not a partition function.

<sup>1</sup> `SELECT` with variable accumulation/aggregation (for example, `SELECT @val += col1 FROM table1`) is not supported for inlining.

<sup>2</sup> Recursive UDFs will be inlined to a certain depth only.

<sup>3</sup> Intrinsic functions whose results depend upon the current system time are time-dependent. An intrinsic function that may update some internal global state is an example of a function with side effects. Such functions return different results each time they are called, based on the internal state.

### Checking whether or not a UDF can be inlined

For every T-SQL scalar UDF, the [sys.sql_modules](../system-catalog-views/sys-sql-modules-transact-sql.md) catalog view includes a property called `is_inlineable`, which indicates whether a UDF is inlineable or not. A value of 1 indicates that it is inlineable, and 0 indicates otherwise. This property will have a value of 1 for all inline TVFs as well. For all other modules, the value will be 0.

>[!NOTE]
>If a scalar UDF is inlineable, it does not imply that it will always be inlined. SQL Server will decide (on a per-query, per-UDF basis) whether to inline a UDF or not. For instance, if the UDF definition runs into thousands of lines of code, SQL Server might choose not to inline it. Another example is a UDF in a `GROUP BY` clause - which will not be inlined. This decision is made when the query referencing a scalar UDF is compiled.

### Checking whether inlining has happened or not

If all the preconditions are satisfied and SQL Server decides to perform inlining, it transforms the UDF into a relational expression. From the query plan, it is easy to figure out whether inlining has happened or not:

- The plan xml will not have a `<UserDefinedFunction>` xml node for a UDF that has been inlined successfully. 
- Certain XEvents are emitted.

## Enabling scalar UDF inlining

You can make workloads automatically eligible for scalar UDF inlining by enabling compatibility level 150 for the database.  You can set this using Transact-SQL. For example:  

```sql
ALTER DATABASE [WideWorldImportersDW] SET COMPATIBILITY_LEVEL = 150;
```

Apart from this, there are no other changes required to be made to UDFs or queries to take advantage of this feature.

## Disabling Scalar UDF inlining without changing the compatibility level

Scalar UDF inlining can be disabled at the database, statement, or UDF scope while still maintaining database compatibility level 150 and higher. To disable scalar UDF inlining at the database scope, execute the following statement within the context of the applicable database: 

```sql
ALTER DATABASE SCOPED CONFIGURATION SET TSQL_SCALAR_UDF_INLINING = OFF;
```

To re-enable scalar UDF inlining for the database, execute the following statement within the context of the applicable database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET TSQL_SCALAR_UDF_INLINING = ON;
```

When ON, this setting will appear as enabled in [`sys.database_scoped_configurations`](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md). 
You can also disable scalar UDF inlining for a specific query by designating `DISABLE_TSQL_SCALAR_UDF_INLINING` as a `USE HINT` query hint. For example:

```sql
SELECT L_SHIPDATE, O_SHIPPRIORITY, SUM (dbo.discount_price(L_EXTENDEDPRICE, L_DISCOUNT)) 
FROM LINEITEM
INNER JOIN ORDERS
  ON O_ORDERKEY = L_ORDERKEY 
GROUP BY L_SHIPDATE, O_SHIPPRIORITY ORDER BY L_SHIPDATE
OPTION (USE HINT('DISABLE_TSQL_SCALAR_UDF_INLINING'));
```

A `USE HINT` query hint takes precedence over the database scoped configuration or compatibility level setting.

Scalar UDF inlining can also be disabled for a specific UDF using the INLINE clause in the `CREATE FUNCTION` or `ALTER FUNCTION` statement.
For example:

```sql
CREATE OR ALTER FUNCTION dbo.discount_price(@price DECIMAL(12,2), @discount DECIMAL(12,2))
RETURNS DECIMAL (12,2)
WITH INLINE = OFF
AS
BEGIN
    RETURN @price * (1 - @discount);
END
```

Once the above statement is executed, this UDF will never be inlined into any query that invokes it. To re-enable inlining for this UDF, execute the following statement:

```sql
CREATE OR ALTER FUNCTION dbo.discount_price(@price DECIMAL(12,2), @discount DECIMAL(12,2))
RETURNS DECIMAL (12,2)
WITH INLINE = ON
AS
BEGIN
    RETURN @price * (1 - @discount);
END
```

>[!NOTE]
>The `INLINE` clause is not mandatory. If `INLINE` clause is not specified, it is automatically set to `ON`/`OFF` based on whether the UDF can be inlined. If `INLINE=ON` is specified but the UDF is found ineligible for inlining, an error will be thrown.

## Important Notes

As described in this article, scalar UDF inlining transforms a query with scalar UDFs into a query with an equivalent scalar subquery. Due to this transformation, users may notice some differences in behavior in the following scenarios:

1. Inlining will result in a different query hash for the same query text.
1. Certain warnings in statements inside the UDF (such as divide by zero etc.) which might have been hidden earlier, might show up due to inlining.
1. Query level join hints might not be valid anymore, as inlining may introduce new joins. Local join hints will have to be used instead.
1. Views that reference inline scalar UDFs cannot be indexed. If you need to create an index on such views, disable inlining for the referenced UDFs.
1. There might be some differences in the behavior of [Dynamic Data masking](../security/dynamic-data-masking.md) with UDF inlining. 
In certain situations (depending upon the logic in the UDF), inlining might be more conservative w.r.t masking output columns. In scenarios where the columns referenced in a UDF are not output columns, they will not be masked. 
1. If a UDF references built-in functions such as `SCOPE_IDENTITY()`, the value returned by the built-in function will change with inlining. This change in behavior is because inlining changes the scope of statements inside the UDF.

## See Also

[Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md)

[Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)

[Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)

[Joins](../../relational-databases/performance/joins.md)

[Demonstrating Adaptive Query Processing](https://github.com/joesackmsft/Conferences/blob/master/Data_AMP_Detroit_2017/Demos/AQP_Demo_ReadMe.md)
