---
title: "Scalar UDF Inlining in Microsoft SQL databases | Microsoft Docs"
description: "Scalar UDF Inlining feature to improve performance of queries that invoke scalar UDFs in SQL Server (2018 and later), and Azure SQL Database."
ms.custom: ""
ms.date: "05/21/2018"
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
monikerRange: "= azuresqldb-current || >= sql-server-2018 || = sqlallproducts-allversions"
---
# Scalar UDF Inlining
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

This article introduces Scalar UDF inlining, a feature under the intelligent query processing suite of features. This feature improves performance of queries that invoke scalar UDFs in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]) and [!INCLUDE[ssSDS](../../includes/sssds-md.md)].

## T-SQL Scalar User-Defined Functions
T-SQL Scalar User-Defined Functions are [User-Defined Functions] that are implemented in Transact-SQL and return a single data value.
T-SQL UDFs are an elegant way to achieve code reuse and modularity across SQL queries. Some computations (such as complex business rules) are easier to express in imperative UDF form. UDFs help in building up complex logic without requiring expertise in writing complex SQL queries. 

## Performance of Scalar UDFs
Scalar UDFs typically end up performing poorly due to the following reasons.

- Iterative invocation: UDFs are invoked in an iterative manner, once per qualifying tuple. This incurs additional costs of repeated context switching due to function invocation. Especially, UDFs that execute SQL queries in their body are severely affected. 
- Lack of costing: During optimization, only relational operators are costed, while scalar operators are not. Prior to the introduction of scalar UDFs, other scalar operators were generally cheap and did not require costing. A small CPU cost added for a scalar operation was enough. 
- Interpreted execution: UDFs are evaluated as a batch of statements, executed statement-by-statement. Note that each statement itself is compiled, and the compiled plan is cached. Although this caching strategy saves some time as it avoids recompilations, each statement executes in isolation. No cross-statement optimizations are carried out.
- Serial execution: SQL Server does not use intra-query parallelism in queries that invoke UDFs. There are several reasons for this, but it is beyond the scope of this article. 

## Automatic inlining of Scalar UDFs
The goal of the Scalar UDF inlining feature is to improve performance of queries that invoke scalar UDFs, where UDF execution is the main bottleneck.

With this new feature, scalar UDFs are transformed into scalar expressions or scalar subqueries which are substituted in the calling query in place of the UDF operator. These expressions and subqueries are then optimized. As a result, the query plan will no longer have a user-defined function operator, but its effects will be observed in the plan, like views or inline TVFs. 

### Example 1 - single statement scalar UDF

Consider the following query

```sql
SELECT L_SHIPDATE, O_SHIPPRIORITY, SUM (L_EXTENDEDPRICE *(1 - L_DISCOUNT)) 
FROM LINEITEM, ORDERS
WHERE O_ORDERKEY = L_ORDERKEY 
GROUP BY L_SHIPDATE, O_SHIPPRIORITY ORDER BY L_SHIPDATE
```

This query computes the sum of discounted prices for line items and presents the results grouped by the shipping date and shipping priority. The expression **L_EXTENDEDPRICE *(1 - L_DISCOUNT)** is the formula for the discounted price for a given line item. Such formulas can be extracted into functions for modularity and reuse.

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
FROM LINEITEM, ORDERS
WHERE O_ORDERKEY = L_ORDERKEY 
GROUP BY L_SHIPDATE, O_SHIPPRIORITY ORDER BY L_SHIPDATE
```

Due to the reasons outlined earlier, the query with the UDF performs poorly. Now, with scalar UDF inlining, the scalar expression in the body of the UDF is substituted directly in the query. The results of running this query  are shown in the below table:

| Query: | Query without UDF | Query with UDF (without inlining) | Query with scalar UDF inlining | 
| --- | --- | --- | --- |
| Execution time: | 1.6 seconds | 29 minutes 11 seconds | 1.6 seconds |

These numbers are based on a TPC-H 10GB CCI dataset, running on a machine with dual processor (12 core), 96GB RAM, backed by SSD. The numbers include compilation and execution time with a cold procedure cache and buffer pool. The default configuration was used, and no other indexes were created.

### Example 2 - multi-statement scalar UDF

Scalar UDFs that are implemented using multiple T-SQL statements such as variable assignments and conditional branching can also be inlined. Consider the following scalar UDF that, given a customer key, determines the service category for that customer. It arrives at the category by first computing the total price of all orders placed by the customer using a SQL query, and then uses an IF-ELSE logic to decide the category based on the total price. 

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

Now, consider a simple query that invokes this UDF.

```sql
SELECT C_NAME, dbo.customer_category(C_CUSTKEY) FROM CUSTOMER;
```

The execution plan for this query in SQL Server 2017 (compatibility level 140 and earlier) is as follows:

![Query Plan without inlining](./media/5_AQPStatsOneRow.png)

As the plan shows, SQL Server adopts a simple strategy here: for every tuple in the CUSTOMER table, invoke the UDF and output the results. This strategy is quite naïve and inefficient. With inlining, such UDFs are transformed into equivalent scalar subqueries which are substituted in the calling query in place of the UDF.

For the same query, the plan with the UDF inlined looks as below.

![Query Plan with inlining](./media/5_AQPStatsOneRow.png)

Here are some key observations from the above plan:
1. SQL Server has inferred the implicit join between CUSTOMER and ORDERS and made that explicit via a Join operator.
2. SQL Server has also inferred the implicit GROUP BY O_CUSTKEY on ORDERS and has used the IndexSpool + StreamAggregate to implement it.
3. SQL Server is using parallelism across all operators.

Depending upon the complexity of the logic in the UDF, the resulting query plan might also get bigger and more complex. As we can see, the operations inside the UDF are now no longer a black box, and hence the query optimizer is able to cost and optimize those operations. Also, since the UDF is no longer in the plan, iterative UDF invocation is replaced by a plan that avoids function call overhead.

The results of running this query are shown in the below table:

| Query: | Query without UDF inlining | Query with scalar UDF inlining | 
| --- | --- | --- | 
| Execution time: | TBD | TBD | 



## See Also
[Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md)     
[Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)    
[Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)    
[Joins](../../relational-databases/performance/joins.md)    
[Demonstrating Adaptive Query Processing](https://github.com/joesackmsft/Conferences/blob/master/Data_AMP_Detroit_2017/Demos/AQP_Demo_ReadMe.md)          

