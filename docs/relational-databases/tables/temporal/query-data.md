---
title: Query Data in a System-Versioned Temporal Table
description: Use FOR SYSTEM_TIME clause to query data in temporal tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/18/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Query data in a system-versioned temporal table

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

To get the latest (current) state of data in a temporal table, query it the same way as a non-temporal table. If the `PERIOD` columns aren't hidden, their values appear in a `SELECT *` query. If you specify `PERIOD` columns as `HIDDEN`, their values don't appear in a `SELECT *` query. When the `PERIOD` columns are hidden, reference them specifically in the `SELECT` clause to return their values.

To perform time-based analysis, use the `FOR SYSTEM_TIME` clause with four temporal-specific subclauses to query data across the current and history tables. For more information on these clauses, see [Temporal tables](overview.md) and [FROM clause plus JOIN, APPLY, PIVOT](../../../t-sql/queries/from-transact-sql.md)

- `AS OF <date_time>`
- `FROM <start_date_time> TO <end_date_time>`
- `BETWEEN <start_date_time> AND <end_date_time>`
- `CONTAINED IN (<start_date_time>, <end_date_time>)`
- `ALL`

You can specify `FOR SYSTEM_TIME` independently for each table in a query. Use it inside common table expressions, table-valued functions, and stored procedures. When you use a table alias with a temporal table, include the `FOR SYSTEM_TIME` clause between the temporal table name and the alias (see [Query for a specific time using the `AS OF` subclause](#query-for-a-specific-time-using-the-as-of-subclause) second example).

## Query for a specific time using the `AS OF` subclause

Use the `AS OF` subclause to reconstruct the state of data as it was at any specific time in the past. You can reconstruct the data with the precision of the **datetime2** type that you specified in `PERIOD` column definitions.

Use the `AS OF` subclause with constant literals or variables to dynamically specify the time condition. The values you provide are interpreted as UTC time.

This first example returns the state of the `dbo.Department` table `AS OF` a specific date in the past.

```sql
-- State of entire table AS OF specific date in the past
SELECT [DeptID],
       [DeptName],
       [ValidFrom],
       [ValidTo]
FROM [dbo].[Department] FOR SYSTEM_TIME
    AS OF '2021-09-01 T10:00:00.7230011';
```

This second example compares the values between two points in time for a subset of rows.

```sql
DECLARE @ADayAgo AS DATETIME2;

SET @ADayAgo = DATEADD(DAY, -1, SYSUTCDATETIME());

-- Comparison between two points in time for subset of rows
SELECT D_1_Ago.DeptID,
       d.DeptID,
       D_1_Ago.DeptName,
       d.DeptName,
       D_1_Ago.ValidFrom,
       d.ValidFrom,
       D_1_Ago.ValidTo,
       d.ValidTo
FROM dbo.Department FOR SYSTEM_TIME
    AS OF @ADayAgo AS D_1_Ago
    INNER JOIN Department AS d
         ON D_1_Ago.DeptID = d.DeptID
        AND D_1_Ago.DeptID BETWEEN 1 AND 5;
```

### Use views with `AS OF` subclause in temporal queries

Views are useful when you need complex point-in-time analysis. A common example is generating a business report today with the values for the previous month.

Usually, customers have a normalized database model, which involves many tables with foreign key relationships. Finding out how data from that normalized model looked at a point in the past can be challenging, because all tables change independently on their own cadence.

In this case, the best option is to create a view and apply the `AS OF` subclause to the entire view. This approach decouples modeling of the data access layer from point-in-time analysis, because SQL Server applies the `AS OF` clause transparently to all temporal tables that participate in the view definition. Furthermore, you can combine temporal with non-temporal tables in the same view and `AS OF` is applied only to temporal ones. If the view doesn't reference at least one temporal table, applying temporal querying clauses to it fails with an error.

The following sample code creates a view that joins three temporal tables: `Department`, `CompanyLocation`, and `LocationDepartments`:

```sql
CREATE VIEW [dbo].[vw_GetOrgChart]
AS
SELECT [CompanyLocation].LocID,
       [CompanyLocation].LocName,
       [CompanyLocation].City,
       [Department].DeptID,
       [Department].DeptName
FROM [dbo].[CompanyLocation]
    LEFT OUTER JOIN [dbo].[LocationDepartments]
        ON [CompanyLocation].LocID = LocationDepartments.LocID
    LEFT OUTER JOIN [dbo].[Department]
        ON LocationDepartments.DeptID = [Department].DeptID;
GO
```

You can query the view using the `AS OF` subclause and a **datetime2** literal:

```sql
/* Query view AS OF */
SELECT *
FROM [vw_GetOrgChart] FOR SYSTEM_TIME
    AS OF '2021-09-01 T10:00:00.7230011';
```

## Query for changes to specific rows over time

The temporal subclauses `FROM ... TO`, `BETWEEN ... AND`, and `CONTAINED IN` are useful when you need to get all historical changes for a specific row in the current table (also known as a data audit).

The first two subclauses return row versions that overlap with a specified period (that is, those that started before the given period and ended after it), while `CONTAINED IN` returns only those that existed within the specified period boundaries.

If you search for non-current row versions only, query the history table directly for best query performance. Use `ALL` to query current and historical data without any restrictions.

```sql
/* Query using BETWEEN...AND sub-clause*/
SELECT [DeptID],
       [DeptName],
       [ValidFrom],
       [ValidTo],
       IIF (YEAR(ValidTo) = 9999, 1, 0) AS IsActual
FROM [dbo].[Department] FOR SYSTEM_TIME
    BETWEEN '2021-01-01' AND '2021-12-31'
WHERE DeptId = 1
ORDER BY ValidFrom DESC;

/* Query using CONTAINED IN sub-clause */
SELECT [DeptID],
       [DeptName],
       [ValidFrom],
       [ValidTo]
FROM [dbo].[Department] FOR SYSTEM_TIME
    CONTAINED IN ('2021-04-01', '2021-09-25')
WHERE DeptId = 1
ORDER BY ValidFrom DESC;

/* Query using ALL sub-clause */
SELECT [DeptID],
       [DeptName],
       [ValidFrom],
       [ValidTo],
       IIF (YEAR(ValidTo) = 9999, 1, 0) AS IsActual
FROM [dbo].[Department] FOR SYSTEM_TIME ALL
ORDER BY [DeptID], [ValidFrom] DESC;
```

## Analyze trends across a time window

To summarize how data looks across a period, combine `FOR SYSTEM_TIME BETWEEN ... AND` with `GROUP BY` and aggregate functions. Use this technique for descriptive statistics and trend analysis. It rolls up every row version that was active during the window instead of reconstructing a single point in time.

The following example computes descriptive statistics for employee salaries across a time window, grouped by department. It uses the `Employee` system-versioned table defined in [Temporal tables](overview.md) and [Temporal table usage scenarios](usage-scenarios.md). The `AnnualSalary` column is a numeric measure well suited to the `AVG`, `MIN`, `MAX`, and `STDEV` functions.

The `FOR SYSTEM_TIME BETWEEN ... AND` clause returns every row version that overlaps the period. As a result, the aggregates include every salary that was in effect during the window. When an employee's salary changed, the query returns multiple versions for that employee, and each version contributes to the aggregates.

```sql
DECLARE @periodStart AS DATETIME2 = '2021-01-01';
DECLARE @periodEnd AS DATETIME2 = '2021-12-31';

SELECT [Department],
       AVG([AnnualSalary]) AS AvgSalary,
       MIN([AnnualSalary]) AS MinSalary,
       MAX([AnnualSalary]) AS MaxSalary,
       STDEV([AnnualSalary]) AS SalaryStdDev,
       COUNT(*) AS RowVersions
FROM [dbo].[Employee] FOR SYSTEM_TIME
    BETWEEN @periodStart AND @periodEnd
GROUP BY [Department]
ORDER BY AvgSalary DESC;
```

## Related content

- [Temporal tables](overview.md)
- [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../../../t-sql/queries/from-transact-sql.md)
- [Create a system-versioned temporal table](create.md)
- [Modify data in a system-versioned temporal table](modify-data.md)
- [Change the schema of a system-versioned temporal table](change-schema.md)
- [Stop system-versioning on a system-versioned temporal table](stop-system-versioning.md)
