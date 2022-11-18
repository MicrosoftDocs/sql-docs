---
title: "Pushdown computations in PolyBase"
titlesuffix: SQL Server
description: Enable pushdown computation to improve performance of queries on your Hadoop cluster. You can select a subset of rows/columns in an external table for pushdown.
ms.date: 10/07/2022
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
monikerRange: ">= sql-server-2016||>=sql-server-linux-ver15"
---

# Pushdown computations in PolyBase

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Pushdown computation improves the performance of queries on external data sources. Beginning in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], pushdown computations were available for Hadoop external data sources. [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] introduced pushdown computations for other types of external data sources.  

> [!NOTE]
> To determine whether or not PolyBase pushdown computation is benefiting your query, see [How to tell if external pushdown occurred](polybase-how-to-tell-pushdown-computation.md).

## Enable pushdown computation

The following articles include information about configuring pushdown computation for specific types of external data sources:

- [Enable pushdown computation in Hadoop](polybase-configure-hadoop.md#pushdown)
- [Configure PolyBase to access external data in Oracle](polybase-configure-oracle.md)
- [Configure PolyBase to access external data in Teradata](polybase-configure-teradata.md)
- [Configure PolyBase to access external data in MongoDB](polybase-configure-mongodb.md)
- [Configure PolyBase to access external data with ODBC generic types](polybase-configure-odbc-generic.md)
- [Configure PolyBase to access external data in SQL Server](polybase-configure-sql-server.md)

This table summarizes pushdown computation support on different external data sources:

| Data Source      | Joins  | Projections | Aggregations | Filters   | Statistics |
|------------------|--------|-------------|--------------|-----------|------------|
| **Generic ODBC** | Yes    | Yes         | Yes          | Yes       | Yes        |  
| **Oracle**       | Yes    | Yes         | Yes          | Yes       | Yes        |
| **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**   | Yes    | Yes         | Yes          | Yes       | Yes        |
| **Teradata**     | Yes    | Yes         | Yes          | Yes       | Yes        |  
| **MongoDB\***  | **No** | Yes         | Yes\*\*\*          | Yes\*\*\*       | Yes        |
| **Hadoop**     | **No** | Yes         | Some\*\*      | Some\*\*  | Yes        |  
| **Azure Blob Storage** | No | No | No | No | Yes |

\* Azure Cosmos DB pushdown support is enabled via the Azure Cosmos DB API for MongoDB. 

\*\* See [Pushdown computation and Hadoop providers](#pushdown-computation-and-hadoop-providers).

\*\*\* Pushdown support for aggregations and filters for the MongoDB ODBC connector for SQL Server 2019 was introduced with SQL Server 2019 CU18.

> [!NOTE]
> Pushdown computation can be blocked by some T-SQL syntax. For more information, review [Syntax that prevents pushdown](polybase-pushdown-computation.md#syntax-that-prevents-pushdown).

### Pushdown computation and Hadoop providers

PolyBase currently supports two Hadoop providers: Hortonworks Data Platform (HDP) and Cloudera Distributed Hadoop (CDH). There are no differences between the two features in terms of pushdown computation.

To use the computation pushdown functionality with Hadoop, the target Hadoop cluster must have the core components of HDFS, YARN and MapReduce, with the job history server enabled. PolyBase submits the pushdown query via MapReduce and pulls status from the job history server. Without either component, the query fails.

Some aggregation must occur after the data reaches [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. But a portion of the aggregation occurs in Hadoop. This method is common in computing aggregations in massively parallel processing systems.  

Hadoop providers support the following aggregations and filters.

| **Aggregations**                  | **Filters (binary comparison)** | 
|-----------------------------------|---------------------------------| 
| Count_Big                         | NotEqual                        | 
| Sum                               | LessThan                        | 
| Avg                               | LessOrEqual                     | 
| Max                               | GreaterOrEqual                  | 
| Min                               | GreaterThan                     | 
| Approx_Count_Distinct             | Is                              | 
|                                   | IsNot                           | 

## Key beneficial scenarios of pushdown computation

With PolyBase pushdown computation, you can delegate computation tasks to external data sources. This reduces the workload on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and can significantly improve performance. 

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can push joins, projections, aggregations, and filters to external data sources to take advantage of remote compute and restrict the data sent over the network. 

### Pushdown of joins

In many cases, PolyBase can facilitate pushdown of the join operator, which will greatly improve performance.  

If the join can be done at the external data source, this reduces the amount of data movement and improves the query's performance. Without join pushdown, the data from the tables to be joined must be brought locally into tempdb, then joined.

### Select a subset of rows

Use predicate pushdown to improve performance for a query that selects a subset of rows from an external table.

In this example, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] initiates a map-reduce job to retrieve the rows that match the predicate `customer.account_balance < 200000` on Hadoop. Because the query can complete successfully without scanning all of the rows in the table, only the rows that meet the predicate criteria are copied to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This saves significant time and requires less temporary storage space when the number of customer balances < 200000 is small in comparison with the number of customers with account balances >= 200000.

```sql
SELECT * FROM customer WHERE customer.account_balance < 200000;
SELECT * FROM SensorData WHERE Speed > 65;  
```

### Select a subset of columns

Use predicate pushdown to improve performance for a query that selects a subset of columns from an external table.

In this query, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] initiates a map-reduce job to pre-process the Hadoop delimited-text file so that only the data for the two columns, customer.name and customer.zip_code, will be copied to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
SELECT customer.name, customer.zip_code
FROM customer
WHERE customer.account_balance < 200000;
```

### Pushdown for basic expressions and operators

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows the following basic expressions and operators for predicate pushdown.

- Binary comparison operators (`<`, `>`, `=`, `!=`, `<>`, `>=`, `<=`) for numeric, date, and time values.
- Arithmetic operators (`+`, `-`, `*`, `/`, `%`).
- Logical operators (`AND`, `OR`).
- Unary operators (`NOT`, `IS NULL`, `IS NOT NULL`).

The operators `BETWEEN`, `NOT`, `IN`, and `LIKE` might be pushed down. The actual behavior depends on how the query optimizer rewrites the operator expressions as a series of statements that use basic relational operators.

The query in this example has multiple predicates that can be pushed down to Hadoop. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can push map-reduce jobs to Hadoop to perform the predicate `customer.account_balance <= 200000`. The expression `BETWEEN 92656 AND 92677` is also composed of binary and logical operations that can be pushed to Hadoop. The logical **AND** in `customer.account_balance AND customer.zipcode` is a final expression.

Given this combination of predicates, the map-reduce jobs can perform all of the WHERE clause. Only the data that meets the `SELECT` criteria is copied back to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
SELECT * FROM customer 
WHERE customer.account_balance <= 200000 
AND customer.zipcode BETWEEN 92656 AND 92677;
```

### Supported functions for pushdown

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows the following functions for predicate pushdown.

String functions
- `CONCAT`
- `DATALENGTH`
- `LEN`
- `LIKE`
- `LOWER`
- `LTRIM`
- `RTRIM`
- `SUBSTRING`
- `UPPER`

Mathematical functions
- `ABS`
- `ACOS`
- `ASIN`
- `ATAN`
- `CEILING`
- `COS`
- `EXP`
- `FLOOR`
- `POWER`
- `SIGN`
- `SIN`
- `SQRT`
- `TAN`

General functions
- `COALESCE`
- `NULLIF`

Date & time functions
- `DATEADD`
- `DATEDIFF`
- `DATEPART`

## Syntax that prevents pushdown

The following T-SQL functions or syntax will prevent pushdown computation:

- `AT TIME ZONE`
- `CONCAT_WS`
- `TRANSLATE`
- `RAND`
- `CHECKSUM`
- `BINARY_CHECKSUM`
- `ISJSON`
- `JSON_VALUE`
- `JSON_QUERY`
- `JSON_MODIFY`
- `NEWID`
- `STRING_ESCAPE`
- `COMPRESS`
- `DECOMPRESS`
- `GREATEST`
- `LEAST`
- `PARSE`

Pushdown support for the `FORMAT` and `TRIM` syntax was introduced in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] CU10.

### Filter clause with variable
If you are specifying a variable in a filter clause, by default this will prevent pushdown of the filter clause. For example, if you run the following query, the filter clause will not be pushed down:

```sql
DECLARE @BusinessEntityID INT

SELECT * FROM [Person].[BusinessEntity]  
WHERE BusinessEntityID = @BusinessEntityID;
```

To achieve pushdown of the variable, you need to enable query optimizer hotfixes functionality. This can be done in any of the following ways:
- Instance Level: Enable trace flag 4199 as a startup parameter for the instance
- Database Level: In the context of the database that has the PolyBase external objects, execute ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = ON
- Query level: 
        Use query hint OPTION (QUERYTRACEON 4199) or OPTION (USE HINT ('ENABLE_QUERY_OPTIMIZER_HOTFIXES'))

This limitation applies to execution of [sp_executesql](../system-stored-procedures/sp-executesql-transact-sql.md). 

Note: The ability to pushdown the variable was first introduced in SQL Server 2019 CU5. 

## Examples

### Force pushdown

```sql
SELECT * FROM [dbo].[SensorData]
WHERE Speed > 65
OPTION (FORCE EXTERNALPUSHDOWN);
```

### Disable pushdown

```sql
SELECT * FROM [dbo].[SensorData]
WHERE Speed > 65
OPTION (DISABLE EXTERNALPUSHDOWN);
```

## Next steps

- For more information about PolyBase, see [Introducing data virtualization with PolyBase](polybase-guide.md)

## See also

- [How to tell if external pushdown occurred](polybase-how-to-tell-pushdown-computation.md)  
