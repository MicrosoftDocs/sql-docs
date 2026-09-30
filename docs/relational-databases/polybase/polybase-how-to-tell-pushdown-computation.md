---
title: How to Tell If PolyBase External Pushdown Occurred
titleSuffix: SQL Server
description: If a PolyBase query is performing slowly, you should determine if pushdown of your PolyBase query is occurring. Queries benefit from PolyBase external pushdown.
author: markingmyname
ms.author: maghan
ms.reviewer: nathansc, hudequei, randolphwest
ms.date: 01/28/2026
ms.service: sql
ms.topic: how-to
---
# How to tell if external pushdown occurred

This article explains how to determine if a PolyBase query benefits from pushdown to the external data source. For more information on external pushdown, see [Pushdown computations in PolyBase](polybase-pushdown-computation.md).

## Is my query benefitting from external pushdown?

Pushdown computation improves the performance of queries on external data sources. Certain computation tasks are delegated to the external data source instead of being brought to the SQL Server instance. Especially in the cases of filtering and join pushdown, the workload on the SQL Server instance can be greatly reduced.

PolyBase pushdown computation can significantly improve the performance of the query. If a PolyBase query is performing slowly, check if pushdown of your PolyBase query is occurring.

You can observe pushdown in the execution plan in three different scenarios:

- Filter predicate pushdown
- Join pushdown
- Aggregation pushdown

Two new features of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] enable administrators to determine if a PolyBase query is being pushed down to the external data source:

- View the [Estimated Execution Plan with trace flag 6408](#tf6408)
- View the `read_command` in the [sys.dm_exec_external_work](#dmv) dynamic management view

This article provides details on how to use each of these two use cases, for each of three pushdown scenarios.

## Limitations

The following limitations affect what you can push down to external data sources with [pushdown computations in PolyBase](polybase-pushdown-computation.md):

- Some T-SQL functions can prevent pushdown. For more information, see [PolyBase features and limitations](polybase-pushdown-computation.md#syntax-that-prevents-pushdown).

- For a list of T-SQL functions that can otherwise be pushed down, see [Pushdown computations in PolyBase](polybase-pushdown-computation.md#pushdown-for-basic-expressions-and-operators).

<a id="tf6408"></a>

## Use trace flag 6408

By default, the estimated execution plan doesn't expose the remote query plan. You only see the remote query operator object. For example, an estimated execution plan from SQL Server Management Studio (SSMS):

:::image type="content" source="media/polybase-how-to-tell-pushdown-computation/1-exec-plan-without-t6408-ssms.png" alt-text="Screenshot of an estimated execution plan in SSMS.":::

Starting in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], you can enable a new trace flag 6408 globally using [DBCC TRACEON](../../t-sql/database-console-commands/dbcc-traceon-transact-sql.md). For example:

```sql
DBCC TRACEON (6408, -1);
```

This trace flag works only with estimated execution plans and has no effect on actual execution plans. This trace flag exposes information about the Remote Query operator that shows what happens during the Remote Query phase.

[Execution plan overview](../performance/execution-plans.md) are read from right to left, as indicated by the direction of the arrows. If an operator is to the right of another operator, it's *before* it. If an operator is to the left of another operator, it's *after* it.

- In SSMS, highlight the query and select **Display Estimated Execution Plan** from the toolbar or use **Ctrl**+**L**.

Each of the following examples includes the output from SSMS.

### Pushdown of filter predicate (view with execution plan)

Consider the following query, which uses a filter predicate in the WHERE clause:

```sql
SELECT *
FROM [Person].[BusinessEntity] AS be
WHERE be.BusinessEntityID = 17907;
```

If the filter predicate is pushed down, the filter operator appears before the external operator in the execution plan. When the filter operator is before the external operator, the filtering happens before the query engine retrieves data from the external data source, which means the filter predicate is pushed down.

#### With pushdown of filter predicate (view with execution plan)

When you enable trace flag 6408, you see extra information in the estimated execution plan output.

In SSMS, the remote query plan appears as Query 2 (`sp_execute_memo_node_1`) in the estimated execution plan. It corresponds to the Remote Query operator in Query 1. For example:

:::image type="content" source="media/polybase-how-to-tell-pushdown-computation/3-exec-plan-with-t6408-filter-pushdown-ssms.png" alt-text="Screenshot of an execution plan with filter predicate pushdown from SSMS.":::

#### Without pushdown of filter predicate (view with execution plan)

If the filter predicate isn't pushed down, the filter operator appears after the external operator.

The estimated execution plan from SSMS:

:::image type="content" source="media/polybase-how-to-tell-pushdown-computation/5-exec-plan-with-t6408-no-filter-pushdown-ssms.png" alt-text="Screenshot of an execution plan without filter predicate pushdown from SSMS.":::

### Pushdown of JOIN

Consider the following query that uses the `JOIN` operator for two external tables on the same external data source:

```sql
SELECT be.BusinessEntityID,
       bea.AddressID
FROM [Person].[BusinessEntity] AS be
     INNER JOIN [Person].[BusinessEntityAddress] AS bea
         ON be.BusinessEntityID = bea.BusinessEntityID;
```

If the query engine pushes the `JOIN` operation to the external data source, the Join operator appears before the external operator. In this example, both `[BusinessEntity]` and `[BusinessEntityAddress]` are external tables.

#### With pushdown of join (view with execution plan)

The estimated execution plan from SSMS:

:::image type="content" source="media/polybase-how-to-tell-pushdown-computation/7-exec-plan-with-t6408-join-pushdown-ssms.png" alt-text="Screenshot of an execution plan with join pushdown from SSMS." lightbox="media/polybase-how-to-tell-pushdown-computation/7-exec-plan-with-t6408-join-pushdown-ssms.png":::

#### Without pushdown of join (view with execution plan)

If the query engine doesn't push the `JOIN` operation to the external data source, the Join operator appears after the external operator. In SSMS, the query plan for `sp_execute_memo_node` includes the external operator. This operator is part of the Remote Query operator in Query 1.

The estimated execution plan from SSMS:

:::image type="content" source="media/polybase-how-to-tell-pushdown-computation/9-exec-plan-with-t6408-no-join-pushdown-ssms.png" alt-text="Screenshot of an execution plan without join pushdown from SSMS.":::

### Pushdown of aggregation (view with execution plan)

Consider the following query, which uses an aggregate function:

```sql
SELECT SUM([Quantity]) AS Quant
FROM [AdventureWorks2022].[Production].[ProductInventory];
```

#### With pushdown of aggregation (view with execution plan)

If the aggregation is pushed down, the aggregation operator appears before the external operator. When the aggregation operator is before the external operator, the query performs the aggregation before selecting data from the external data source, which means the aggregation is pushed down.

The estimated execution plan from SSMS:

:::image type="content" source="media/polybase-how-to-tell-pushdown-computation/11-exec-plan-with-t6408-aggregate-pushdown-ssms.png" alt-text="Screenshot of an execution plan with aggregate pushdown from SSMS." lightbox="media/polybase-how-to-tell-pushdown-computation/11-exec-plan-with-t6408-aggregate-pushdown-ssms.png":::

#### Without pushdown of aggregation (view with execution plan)

If the aggregation isn't pushed down, the aggregation operator is after the external operator.

The estimated execution plan from SSMS:

:::image type="content" source="media/polybase-how-to-tell-pushdown-computation/13-exec-plan-with-t6408-no-aggregate-pushdown-ssms.png" alt-text="Screenshot of an execution plan without aggregate pushdown from SSMS." lightbox="media/polybase-how-to-tell-pushdown-computation/13-exec-plan-with-t6408-no-aggregate-pushdown-ssms.png":::

<a id="dmv"></a>

## Use DMV

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, the `read_command` column of [sys.dm_exec_external_work](../system-dynamic-management-objects/sys-dm-exec-external-work-transact-sql.md) DMV shows the query that you send to the external data source. You can determine if pushdown is happening, but it doesn't expose the execution plan. You don't need trace flag 6408 to view the remote query.

> [!NOTE]  
> For Hadoop and Azure storage, the `read_command` always returns `NULL`.

Run the following query and use the `start_time`/`end_time` and `read_command` values to identify the query you're investigating:

```sql
SELECT execution_id,
       start_time,
       end_time,
       read_command
FROM sys.dm_exec_external_work
ORDER BY execution_id DESC;
```

> [!NOTE]  
> One limitation of the [sys.dm_exec_external_work](../system-dynamic-management-objects/sys-dm-exec-external-work-transact-sql.md) method is that the `read_command` field in the DMV is limited to 4,000 characters. If the query is long enough, the `read_command` might be truncated before you see the `WHERE`, `JOIN`, or aggregation function in the `read_command`.

### Pushdown of filter predicate (view with DMV)

Consider the query used in the previous filter predicate example:

```sql
SELECT *
FROM [Person].[BusinessEntity] AS be
WHERE be.BusinessEntityID = 17907;
```

#### With pushdown of filter (view with DMV)

You can check the `read_command` in the DMV to see if pushdown of the filter predicate is happening. You see a similar example to the following query:

```sql
SELECT [T1_1].[BusinessEntityID] AS [BusinessEntityID],
       [T1_1].[rowguid] AS [rowguid],
       [T1_1].[ModifiedDate] AS [ModifiedDate]
FROM (SELECT [T2_1].[BusinessEntityID] AS [BusinessEntityID],
             [T2_1].[rowguid] AS [rowguid],
             [T2_1].[ModifiedDate] AS [ModifiedDate]
      FROM [AdventureWorks2022].[Person].[BusinessEntity] AS T2_1
      WHERE ([T2_1].[BusinessEntityID] = CAST ((17907) AS INT))) AS T1_1;
```

The command sent to the external data source includes the `WHERE` clause, which means the filter predicate is evaluated at the external data source. Filtering on the dataset happens at the external data source, and PolyBase retrieves only the filtered dataset.

#### Without pushdown of filter (view with DMV)

If pushdown isn't happening, you see something like:

```sql
SELECT "BusinessEntityID",
       "rowguid",
       "ModifiedDate"
FROM "AdventureWorks2022"."Person"."BusinessEntity";
```

The command sent to the external data source doesn't include a `WHERE` clause, so the filter predicate isn't pushed down. Filtering on the entire dataset happens on the SQL Server side, after PolyBase retrieves the dataset.

### Pushdown of JOIN (view with DMV)

Consider the query used in the previous `JOIN` example:

```sql
SELECT be.BusinessEntityID,
       bea.AddressID
FROM [Person].[BusinessEntity] AS be
     INNER JOIN [Person].[BusinessEntityAddress] AS bea
         ON be.BusinessEntityID = bea.BusinessEntityID;
```

#### With pushdown of join (view with DMV)

If you push the `JOIN` down to the external data source, you see something like:

```sql
SELECT [T1_1].[BusinessEntityID] AS [BusinessEntityID],
       [T1_1].[AddressID] AS [AddressID]
FROM (SELECT [T2_2].[BusinessEntityID] AS [BusinessEntityID],
             [T2_1].[AddressID] AS [AddressID]
      FROM [AdventureWorks2022].[Person].[BusinessEntityAddress] AS T2_1
           INNER JOIN [AdventureWorks2022].[Person].[BusinessEntity] AS T2_2
               ON ([T2_1].[BusinessEntityID] = [T2_2].[BusinessEntityID])) AS T1_1;
```

The command you send to the external data source includes the `JOIN` clause, so the `JOIN` is pushed down. The external data source handles the join on the dataset, and PolyBase retrieves only the dataset that matches the join condition.

#### Without pushdown of join (view with DMV)

If the pushdown of the join isn't occurring, you see two different queries executed against the external data source:

```sql
SELECT [T1_1].[BusinessEntityID] AS [BusinessEntityID],
       [T1_1].[AddressID] AS [AddressID]
FROM [AdventureWorks2022].[Person].[BusinessEntityAddress] AS T1_1;
SELECT [T1_1].[BusinessEntityID] AS [BusinessEntityID]
FROM [AdventureWorks2022].[Person].[BusinessEntity] AS T1_1;
```

The SQL Server side handles joining the two datasets after PolyBase retrieves both datasets.

### Pushdown of aggregation (view with DMV)

Consider the following query, which uses an aggregate function:

```sql
SELECT SUM([Quantity]) AS Quant
FROM [AdventureWorks2022].[Production].[ProductInventory];
```

#### With pushdown of aggregation (view with DMV)

If pushdown of the aggregation is happening, you see the aggregation function in the `read_command`. For example:

```sql
SELECT [T1_1].[col] AS [col]
FROM (SELECT SUM([T2_1].[Quantity]) AS [col]
      FROM [AdventureWorks2022].[Production].[ProductInventory] AS T2_1) AS T1_1;
```

The aggregation function is in the command sent to the external data source, so the aggregation is pushed down. The aggregation happens at the external data source, and PolyBase retrieves only the aggregated dataset.

#### Without pushdown of aggregation (view with DMV)

If pushdown of the aggregation isn't happening, you don't see the aggregation function in the `read_command`. For example:

```sql
SELECT "Quantity"
FROM "AdventureWorks2022"."Production"."ProductInventory";
```

PolyBase retrieves the unaggregated dataset, and SQL Server performs the aggregation.

## Related content

- [Monitor and troubleshoot PolyBase](polybase-troubleshooting.md)
- [PolyBase errors and possible solutions](polybase-errors-and-possible-solutions.md)
