---
title: "Scalar UDF Inlining in Microsoft SQL Server | Microsoft Docs"
description: "Scalar UDF Inlining feature to improve performance of queries that invoke scalar UDFs in SQL Server (starting with SQL Server 2019)."
ms.custom: ""
ms.date: 06/23/2020
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
monikerRange: "= azuresqldb-current || >= sql-server-ver15 || = sqlallproducts-allversions"
---
# Scalar UDF Inlining

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article introduces Scalar UDF Inlining, a feature under the [Intelligent Query Processing](../../relational-databases/performance/intelligent-query-processing.md) suite of features. This feature improves the performance of queries that invoke scalar UDFs in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQLv15](../../includes/sssqlv15-md.md)]).

## T-SQL scalar User-Defined Functions
User-Defined Functions (UDFs) that are implemented in [!INCLUDE[tsql](../../includes/tsql-md.md)] and return a single data value are referred to as T-SQL Scalar User-Defined Functions. T-SQL UDFs are an elegant way to achieve code reuse and modularity across [!INCLUDE[tsql](../../includes/tsql-md.md)] queries. Some computations (such as complex business rules) are easier to express in imperative UDF form. UDFs help in building up complex logic without requiring expertise in writing complex SQL queries. For more information about UDFs, see [Create User-defined Functions (Database Engine)](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md).

## Performance of scalar UDFs
Scalar UDFs typically end up performing poorly due to the following reasons:

- **Iterative invocation:** UDFs are invoked in an iterative manner, once per qualifying tuple. This incurs additional costs of repeated context switching due to function invocation. Especially, UDFs that execute [!INCLUDE[tsql](../../includes/tsql-md.md)] queries in their definition are severely affected.

- **Lack of costing:** During optimization, only relational operators are costed, while scalar operators are not. Prior to the introduction of scalar UDFs, other scalar operators were generally cheap and did not require costing. A small CPU cost added for a scalar operation was enough. There are scenarios where the actual cost is significant, and yet still remains underrepresented.

- **Interpreted execution:** UDFs are evaluated as a batch of statements, executed statement-by-statement. Each statement itself is compiled, and the compiled plan is cached. Although this caching strategy saves some time as it avoids recompilations, each statement executes in isolation. No cross-statement optimizations are carried out.

- **Serial execution:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not allow intra-query parallelism in queries that invoke UDFs. 

## Automatic inlining of scalar UDFs
The goal of the scalar UDF inlining feature is to improve performance of queries that invoke T-SQL scalar UDFs, where UDF execution is the main bottleneck.

With this new feature, scalar UDFs are automatically transformed into scalar expressions or scalar subqueries that are substituted in the calling query in place of the UDF operator. These expressions and subqueries are then optimized. As a result, the query plan will no longer have a user-defined function operator, but its effects will be observed in the plan, like views or inline TVFs.

### Example 1 - Single statement scalar UDF
Consider the following query.

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
Scalar UDFs that are implemented using multiple T-SQL statements such as variable assignments and conditional branching can also be inlined. Consider the following scalar UDF that, given a customer key, determines the service category for that customer. It arrives at the category by first computing the total price of all orders placed by the customer using a SQL query. Then, it uses an `IF (...) ELSE` logic to decide the category based on the total price.

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

The execution plan for this query in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] (compatibility level 140 and earlier) is as follows:

![Query Plan without inlining](./media/query-plan-without-udf-inlining.png)

As the plan shows, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] adopts a simple strategy here: for every tuple in the `CUSTOMER` table, invoke the UDF and output the results. This strategy is naive and inefficient. With inlining, such UDFs are transformed into equivalent scalar subqueries, which are substituted in the calling query in place of the UDF.

For the same query, the plan with the UDF inlined looks as below.

![Query Plan with inlining](./media/query-plan-with-udf-inlining.png)

As mentioned earlier, the query plan no longer has a user-defined function operator, but its effects are now observable in the plan, like views or inline TVFs. Here are some key observations from the above plan:

-  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has inferred the implicit join between `CUSTOMER` and `ORDERS` and made that explicit via a join operator.
-  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has also inferred the implicit `GROUP BY O_CUSTKEY on ORDERS` and has used the IndexSpool + StreamAggregate to implement it.
-  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is now using parallelism across all operators.

Depending upon the complexity of the logic in the UDF, the resulting query plan might also get bigger and more complex. As we can see, the operations inside the UDF are now no longer a black box, and hence the query optimizer is able to cost and optimize those operations. Also, since the UDF is no longer in the plan, iterative UDF invocation is replaced by a plan that completely avoids function call overhead.

## Inlineable scalar UDFs requirements
<a name="requirements"></a> A scalar T-SQL UDF can be inlined if all of the following conditions are true:

- The UDF is written using the following constructs:
    - `DECLARE`, `SET`: Variable declaration and assignments.
    - `SELECT`: SQL query with single/multiple variable assignments <sup>1</sup>.
    - `IF`/`ELSE`: Branching with arbitrary levels of nesting.
    - `RETURN`: Single or multiple return statements.
    - `UDF`: Nested/recursive function calls <sup>2</sup>.
    - Others: Relational operations such as `EXISTS`, `ISNULL`.
- The UDF does not invoke any intrinsic function that is either time-dependent (such as `GETDATE()`) or has side effects <sup>3</sup> (such as `NEWSEQUENTIALID()`).
- The UDF uses the `EXECUTE AS CALLER` clause (default behavior if the `EXECUTE AS` clause is not specified).
- The UDF does not reference table variables or table-valued parameters.
- The query invoking a scalar UDF does not reference a scalar UDF call in its `GROUP BY` clause.
- The query invoking a scalar UDF in its select list with `DISTINCT` clause does not have `ORDER BY` clause.
- The UDF is not used in `ORDER BY` clause.
- The UDF is not natively compiled (interop is supported).
- The UDF is not used in a computed column or a check constraint definition.
- The UDF does not reference user-defined types.
- There are no signatures added to the UDF.
- The UDF is not a partition function.
- The UDF does not contain references to Common Table Expressions (CTEs).
- The UDF does not contain references to intrinsic functions that may alter the results when inlined (such as `@@ROWCOUNT`) <sup>4</sup>.
- The UDF does not contain aggregate functions being passed as parameters to a scalar UDF <sup>4</sup>.
- The UDF does not reference built-in views (such as `OBJECT_ID`) <sup>4</sup>.
- The UDF does not reference XML methods <sup>5</sup>.
- The UDF does not contain a SELECT with `ORDER BY` without a `TOP 1` clause <sup>5</sup>.
- The UDF does not contain a SELECT query that performs an assignment in conjunction with the `ORDER BY` clause (such as `SELECT @x = @x + 1 FROM table1 ORDER BY col1`) <sup>5</sup>.
- The UDF does not contain multiple RETURN statements <sup>6</sup>.
- The UDF is not called from a RETURN statement <sup>6</sup>.
- The UDF does not reference the `STRING_AGG` function <sup>6</sup>. 

<sup>1</sup> `SELECT` with variable accumulation/aggregation is not supported for inlining (such as `SELECT @val += col1 FROM table1`).

<sup>2</sup> Recursive UDFs will be inlined to a certain depth only.

<sup>3</sup> Intrinsic functions whose results depend upon the current system time are time-dependent. An intrinsic function that may update some internal global state is an example of a function with side effects. Such functions return different results each time they are called, based on the internal state.

<sup>4</sup> Restriction added in [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] CU2

<sup>5</sup> Restriction added in [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] CU4

<sup>6</sup> Restriction added in [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] CU5

> [!NOTE]
> For information on the latest T-SQL Scalar UDF Inlining fixes and changes to inlining eligibility scenarios, see the Knowledge Base article: [FIX: Scalar UDF Inlining issues in SQL Server 2019](https://support.microsoft.com/help/4538581).

### Checking whether or not a UDF can be inlined
For every T-SQL scalar UDF, the [sys.sql_modules](../system-catalog-views/sys-sql-modules-transact-sql.md) catalog view includes a property called `is_inlineable`, which indicates whether a UDF is inlineable or not. 

> [!NOTE]
> The `is_inlineable` property is derived from the constructs found inside the UDF definition. It does not check whether the UDF is in fact inlineable at compile time. For more information, see the [conditions for inlining](#requirements).

A value of 1 indicates that it is inlineable, and 0 indicates otherwise. This property will have a value of 1 for all inline TVFs as well. For all other modules, the value will be 0.

If a scalar UDF is inlineable, it does not imply that it will always be inlined. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will decide (on a per-query, per-UDF basis) whether to inline a UDF or not. A few examples of when a UDF may not be inlined include:

-  If the UDF definition runs into thousands of lines of code, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might choose not to inline it. 
-  A UDF invocation in a `GROUP BY` clause will not be inlined. This decision is made when the query referencing a scalar UDF is compiled.
-  If the UDF is signed with a certificate. Because signatures could be added and dropped after a UDF has been created, the decision of whether to inline or not is done when the query referencing a scalar UDF is compiled. For example, system functions are typically signed with a certificate. You can use [sys.crypt_properties](../../relational-databases/system-catalog-views/sys-crypt-properties-transact-sql.md) to find which objects are signed. 

   ```sql
   SELECT * 
   FROM sys.crypt_properties AS cp
   INNER JOIN sys.objects AS o ON cp.major_id = o.object_id;
   ```

### Checking whether inlining has happened or not
If all the preconditions are satisfied and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] decides to perform inlining, it transforms the UDF into a relational expression. From the query plan, it is easy to figure out whether inlining has happened or not:

- The plan xml will not have a `<UserDefinedFunction>` xml node for a UDF that has been inlined successfully. 
- Certain XEvents are emitted.

## Enabling Scalar UDF Inlining
You can make workloads automatically eligible for Scalar UDF Inlining by enabling compatibility level 150 for the database. You can set this using [!INCLUDE[tsql](../../includes/tsql-md.md)]. For example:  

```sql
ALTER DATABASE [WideWorldImportersDW] SET COMPATIBILITY_LEVEL = 150;
```

Apart from this, there are no other changes required to be made to UDFs or queries to take advantage of this feature.

## Disabling Scalar UDF Inlining without changing the compatibility level
Scalar UDF inlining can be disabled at the database, statement, or UDF scope while still maintaining database compatibility level 150 and higher. To disable scalar UDF inlining at the database scope, execute the following statement within the context of the applicable database: 

```sql
ALTER DATABASE SCOPED CONFIGURATION SET TSQL_SCALAR_UDF_INLINING = OFF;
```

To re-enable Scalar UDF Inlining for the database, execute the following statement within the context of the applicable database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET TSQL_SCALAR_UDF_INLINING = ON;
```

When ON, this setting will appear as enabled in [`sys.database_scoped_configurations`](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md). 
You can also disable Scalar UDF Inlining for a specific query by designating `DISABLE_TSQL_SCALAR_UDF_INLINING` as a `USE HINT` query hint. For example:

```sql
SELECT L_SHIPDATE, O_SHIPPRIORITY, SUM (dbo.discount_price(L_EXTENDEDPRICE, L_DISCOUNT)) 
FROM LINEITEM
INNER JOIN ORDERS
  ON O_ORDERKEY = L_ORDERKEY 
GROUP BY L_SHIPDATE, O_SHIPPRIORITY ORDER BY L_SHIPDATE
OPTION (USE HINT('DISABLE_TSQL_SCALAR_UDF_INLINING'));
```

> [!TIP]
> A `USE HINT` query hint takes precedence over the database scoped configuration or compatibility level setting.

Scalar UDF Inlining can also be disabled for a specific UDF using the INLINE clause in the `CREATE FUNCTION` or `ALTER FUNCTION` statement.
For example:

```sql
CREATE OR ALTER FUNCTION dbo.discount_price(@price DECIMAL(12,2), @discount DECIMAL(12,2))
RETURNS DECIMAL (12,2)
WITH INLINE = OFF
AS
BEGIN
    RETURN @price * (1 - @discount);
END;
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

> [!NOTE]
> The `INLINE` clause is not mandatory. If `INLINE` clause is not specified, it is automatically set to `ON`/`OFF` based on whether the UDF can be inlined. If `INLINE = ON` is specified but the UDF is found ineligible for inlining, an error will be thrown.

## Important Notes
As described in this article, scalar UDF inlining transforms a query with scalar UDFs into a query with an equivalent scalar subquery. Due to this transformation, users may notice some differences in behavior in the following scenarios:

1. Inlining will result in a different query hash for the same query text.
1. Certain warnings in statements inside the UDF (such as divide by zero etc.) which might have been hidden earlier, might show up due to inlining.
1. Query level join hints might not be valid anymore, as inlining may introduce new joins. Local join hints will have to be used instead.
1. Views that reference inline scalar UDFs cannot be indexed. If you need to create an index on such views, disable inlining for the referenced UDFs.
1. There might be some differences in the behavior of [Dynamic Data masking](../security/dynamic-data-masking.md) with UDF inlining. 
In certain situations (depending upon the logic in the UDF), inlining might be more conservative w.r.t masking output columns. In scenarios where the columns referenced in a UDF are not output columns, they will not be masked. 
1. If a UDF references built-in functions such as `SCOPE_IDENTITY()`, `@@ROWCOUNT`, or `@@ERROR`, the value returned by the built-in function will change with inlining. This change in behavior is because inlining changes the scope of statements inside the UDF. Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] CU2, inlining is blocked if the UDF references certain intrinsic functions (for example `@@ROWCOUNT`).

## See Also
[Create User-defined Functions (Database Engine)](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md)   
[Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md)     
[Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)     
[Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)     
[Joins](../../relational-databases/performance/joins.md)     
[Demonstrating Intelligent Query Processing](https://aka.ms/IQPDemos)     
[FIX: Scalar UDF Inlining issues in SQL Server 2019](https://support.microsoft.com/help/4538581)     

