---
title: Prepared statement parameter performance
description: Learn how parameter types, scale, and precision affect prepared statement performance in the JDBC Driver for SQL Server and how to optimize parameter usage to minimize server repreparation.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 12/18/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: best-practice
ai-usage: ai-assisted
---
# Prepared statement parameter performance for the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article explains how prepared statement parameters affect server-side performance in the Microsoft JDBC Driver for SQL Server and provides guidance on optimizing parameter usage.

## Understanding prepared statement parameters

Prepared statements offer significant performance benefits by allowing SQL Server to parse, compile, and optimize a query once, then reuse the execution plan multiple times. However, the way you specify parameters can significantly affect this performance benefit.

When you create a prepared statement, SQL Server generates an execution plan based on the parameter metadata, including:

- Data type
- Precision (for numeric types)
- Scale (for decimal types)
- Maximum length (for string and binary types)

This metadata is crucial because SQL Server uses it to optimize the query execution plan. Changes to any of these parameter characteristics can force SQL Server to discard the existing plan and create a new one, which results in a performance penalty.

## How parameter changes affect performance

### Parameter type changes

When a prepared statement's parameter type changes between executions, SQL Server must reprepare the statement. This repreparation includes:

1. Parsing the SQL statement again.
1. Compiling a new execution plan.
1. Caching the new plan (if caching is enabled).

Consider the following example:

```java
String sql = "SELECT * FROM Employees WHERE EmployeeID = ?";
PreparedStatement pstmt = connection.prepareStatement(sql);

// First execution with Integer
pstmt.setInt(1, 100);
pstmt.executeQuery();

// Second execution with String - causes re-preparation
pstmt.setString(1, "100");
pstmt.executeQuery();
```

In this scenario, switching from `setInt` to `setString` changes the parameter type from `int` to `varchar`, which forces SQL Server to reprepare the statement.

### Precision and scale changes

For numeric types like `decimal` and `numeric`, changes in precision or scale also trigger repreparation:

```java
String sql = "UPDATE Products SET Price = ? WHERE ProductID = ?";
PreparedStatement pstmt = connection.prepareStatement(sql);

// First execution with specific precision
BigDecimal price1 = new BigDecimal("19.99"); // precision 4, scale 2
pstmt.setBigDecimal(1, price1);
pstmt.setInt(2, 1);
pstmt.executeUpdate();

// Second execution with different precision - causes re-preparation
BigDecimal price2 = new BigDecimal("1999.9999"); // precision 8, scale 4
pstmt.setBigDecimal(1, price2);
pstmt.setInt(2, 2);
pstmt.executeUpdate();
```

SQL Server creates different execution plans for different precision and scale combinations because precision and scale affect how the database engine processes the query.

## Best practices for parameter usage

To maximize prepared statement performance, follow these best practices:

### Specify parameter types explicitly

When possible, use the explicit setter methods that match your database column types:

```java
// Good: Explicit type matching
pstmt.setInt(1, employeeId);
pstmt.setString(2, name);
pstmt.setBigDecimal(3, salary);

// Avoid: Using setObject() without explicit types
pstmt.setObject(1, employeeId); // Type inference might vary
```

### Use consistent parameter metadata

Maintain consistent precision and scale for numeric parameters:

```java
// Good: Consistent precision and scale
BigDecimal price1 = new BigDecimal("19.99").setScale(2);
BigDecimal price2 = new BigDecimal("29.99").setScale(2);

// Avoid: Varying precision and scale
BigDecimal price3 = new BigDecimal("19.9");    // scale 1
BigDecimal price4 = new BigDecimal("29.999");  // scale 3
```

### Understand data rounding with numeric types

Using incorrect precision and scale for numeric parameters can result in unintended data rounding. The precision and scale must be appropriate for both the parameter value and where it's used in the SQL statement.

```java
// Example: Column defined as DECIMAL(10, 2)
// Good: Matching precision and scale
BigDecimal amount = new BigDecimal("12345.67").setScale(2, RoundingMode.HALF_UP);
pstmt.setBigDecimal(1, amount);

// Problem: Scale too high causes rounding
BigDecimal amount2 = new BigDecimal("12345.678"); // scale 3
pstmt.setBigDecimal(1, amount2); // Rounds to 12345.68

// Problem: Precision too high
BigDecimal amount3 = new BigDecimal("123456789.12"); // Exceeds precision
pstmt.setBigDecimal(1, amount3); // Might cause truncation or error
```

While you need appropriate precision and scale for your data, avoid changing these values for every execution of a prepared statement. Each change in precision or scale causes the statement to be reprepared on the server, negating the performance benefits of prepared statements.

```java
// Good: Consistent precision and scale across executions
PreparedStatement pstmt = conn.prepareStatement(
    "INSERT INTO Orders (OrderID, Amount) VALUES (?, ?)");

for (Order order : orders) {
    pstmt.setInt(1, order.getId());
    // Always use scale 2 for currency
    BigDecimal amount = order.getAmount().setScale(2, RoundingMode.HALF_UP);
    pstmt.setBigDecimal(2, amount);
    pstmt.executeUpdate();
}

// Avoid: Changing scale for each execution
for (Order order : orders) {
    pstmt.setInt(1, order.getId());
    // Different scale each time - causes re-preparation
    pstmt.setBigDecimal(2, order.getAmount()); // Variable scale
    pstmt.executeUpdate();
}
```

To balance correctness and performance:

1. Determine the appropriate precision and scale for your business requirements.
1. Normalize all parameter values to use consistent precision and scale.
1. Use explicit rounding modes to control how values are adjusted.
1. Validate that your normalized values match the target column definitions.

> [!NOTE]
> You can use the `calcBigDecimalPrecision` connection option to automatically optimize parameter precisions. When enabled, the driver calculates the minimum precision needed for each BigDecimal value, which helps avoid unnecessary rounding. However, this approach might incur more statement prepares as the data changes because different precision values cause repreparation. Manually defining the optimum precision and scale in your application code is the best option when possible, as it provides both data accuracy and consistent statement reuse.

### Avoid mixing parameter setting methods

Don't switch between different setter methods for the same parameter position across executions:

```java
// Avoid: Mixing setter methods
pstmt.setInt(1, 100);
pstmt.executeQuery();

pstmt.setString(1, "100"); // Different method - causes re-preparation
pstmt.executeQuery();
```

### Use setNull() with explicit types

When you set null values, specify the SQL type to maintain consistency:

```java
// Good: Explicit type for null
pstmt.setNull(1, java.sql.Types.INTEGER);

// Avoid: Generic null without type
pstmt.setObject(1, null); // Type might be inferred differently
```

## Monitoring parameter-related performance

### Detecting repreparation issues

To identify whether parameter changes are causing performance issues:

1. Use SQL Server Profiler or Extended Events to monitor `SP:CacheMiss` and `SP:Recompile` events.
2. Review the `sys.dm_exec_cached_plans` DMV to check plan reuse.
3. Analyze query performance metrics to identify statements with frequent repreparations.

Example query to check plan reuse:

```sql
SELECT 
    text,
    usecounts,
    size_in_bytes,
    cacheobjtype,
    objtype
FROM sys.dm_exec_cached_plans AS cp
CROSS APPLY sys.dm_exec_sql_text(cp.plan_handle) AS st
WHERE text LIKE '%YourQueryText%'
ORDER BY usecounts DESC;
```

### Performance counters

Monitor these SQL Server performance counters:

- **SQL Statistics: SQL Re-Compilations/sec** - Shows how often statements are recompiled.
- **SQL Statistics: SQL Compilations/sec** - Shows how often new plans are created.
- **Plan Cache: Cache Hit Ratio** - Indicates how effectively plans are being reused.

For more details about the counters and how to interpret them, see [SQL Server, Plan Cache object](../../relational-databases/performance-monitor/sql-server-plan-cache-object.md).

## Advanced considerations

### Parameterized queries and plan cache pollution

Plan cache pollution happens when varying decimal or numeric precision causes SQL Server to create multiple execution plans for the same query. This problem wastes memory and reduces plan reuse efficiency:

```java
// Avoid: Varying precision pollutes the plan cache
String sql = "UPDATE Products SET Price = ? WHERE ProductID = ?";
PreparedStatement pstmt = connection.prepareStatement(sql);

for (int i = 0; i < 1000; i++) {
    // Each different precision/scale creates a separate cached plan
    BigDecimal price = new BigDecimal("19." + i); // Varying scale
    pstmt.setBigDecimal(1, price);
    pstmt.setInt(2, i);
    pstmt.executeUpdate();
}
pstmt.close();
```

To avoid plan cache pollution, keep consistent precision and scale for numeric parameters:

```java
// Good: Consistent precision and scale enables plan reuse
String sql = "UPDATE Products SET Price = ? WHERE ProductID = ?";
PreparedStatement pstmt = connection.prepareStatement(sql);

for (int i = 0; i < 1000; i++) {
    // Same precision and scale - reuses the same cached plan
    // Note: Round or increase to a consistent scale that aligns with your application data needs.
    BigDecimal price = new BigDecimal("19." + i).setScale(2, RoundingMode.HALF_UP);
    pstmt.setBigDecimal(1, price);
    pstmt.setInt(2, i);
    pstmt.executeUpdate();
}
pstmt.close();
```

String length and integer value variations don't cause plan cache pollution—only precision and scale changes for numeric types create this issue.

### Connection string properties

The JDBC driver provides connection properties that affect prepared statement behavior and performance:

- **enablePrepareOnFirstPreparedStatementCall** - (Default: `false`) Controls whether the driver calls `sp_prepexec` on the first or second execution. Preparing on the first execution slightly improves performance if an application consistently executes the same prepared statement multiple times. Preparing on the second execution improves the performance of applications that mostly execute prepared statements once. This strategy removes the need for a separate call to unprepare if the prepared statement is only executed once.
- **prepareMethod** - (Default: `prepexec`) Specifies the behavior to use for preparation (`prepare` or `prepexec`). Setting `prepareMethod` to `prepare` results in a separate, initial trip to the database to prepare the statement without any initial values for the database to consider in the execution plan. Set to `prepexec` to use `sp_prepexec` as the prepare method. This method combines the prepare action with the first execute, reducing network round trips. It also provides the database with initial parameter values that the database can consider in the execution plan. Depending on how your indexes are optimized, one setting can perform better than the other.
- **serverPreparedStatementDiscardThreshold** - (Default: `10`) Controls batching of `sp_unprepare` operations. This option can improve performance by batching together `sp_unprepare` calls. A higher value leaves prepared statements lingering on the server longer.

For more information, see [Setting the connection properties](setting-the-connection-properties.md).

## Summary

To optimize prepared statement performance for parameters:

1. Use explicit setter methods that match your database column types.
1. Keep parameter metadata (type, precision, scale, length) consistent across executions.
1. Don't switch between different setter methods for the same parameter.
1. Specify SQL types explicitly when you use `setObject` or `setNull`.
1. Reuse prepared statements instead of creating new ones.
1. Monitor plan cache statistics to identify repreparation problems.
1. Consider connection properties that affect prepared statement performance.

By following these practices, you minimize server-side repreparation and get the most performance benefits from prepared statements.

## Related content

- [Prepared statement metadata caching for the JDBC driver](prepared-statement-metadata-caching-for-the-jdbc-driver.md)
- [Improving performance and reliability (JDBC)](improving-performance-and-reliability-with-the-jdbc-driver.md)
- [Set the connection properties](setting-the-connection-properties.md)
