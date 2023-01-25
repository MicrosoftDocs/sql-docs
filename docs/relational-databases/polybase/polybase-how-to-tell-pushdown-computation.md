---
title: How to tell if PolyBase external pushdown occurred 
titlesuffix: SQL Server
description: If a PolyBase query is performing slowly, you should determine if pushdown of your PolyBase query is occurring. Queries benefit from PolyBase external pushdown.
ms.date: 05/20/2021
author: WilliamDassafMSFT
ms.author: wiassaf
ms.topic: conceptual
ms.service: sql
ms.subservice: big-data-cluster
---
# How to tell if external pushdown occurred

This article will detail how to determine if a PolyBase query is benefitting from pushdown to the external data source. For more information on external pushdown, see [pushdown computations in PolyBase](./polybase-pushdown-computation.md). 

## Is my query benefitting from external pushdown?

Pushdown computation improves the performance of queries on external data sources. Certain computation tasks are delegated to the external data source instead of being brought to the SQL Server. Especially in the cases of filtering and join pushdown, the workload on the SQL Server instance can be greatly reduced. 

PolyBase pushdown computation can significantly improve performance of the query. If a PolyBase query is performing slowly, you should determine if pushdown of your PolyBase query is occurring.

There are three different scenarios where pushdown can be observed in the execution plan:

- Filter predicate pushdown
- Join pushdown
- Aggregation pushdown

> [!NOTE]
> There are limitations on what can be pushed down to external data sources with [PolyBase pushdown computations](./polybase-pushdown-computation.md):
> * Some T-SQL functions can prevent pushdown, for more information, see [PolyBase features and limitations](polybase-pushdown-computation.md#syntax-that-prevents-pushdown). 
> * For a list of T-SQL functions that can otherwise be pushed down, see [Pushdown computations in PolyBase](./polybase-pushdown-computation.md#pushdown-for-basic-expressions-and-operators).

Two new features of [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] have been introduced to allow administrators to determine if a PolyBase query is being pushed down to the external data source:

- View the [Estimated Execution Plan with trace flag 6408](#tf6408)
- View the `read_command` in the [sys.dm_exec_external_work](#dmv) dynamic management view

This article will provide details on how to use each of these two use cases, for each of three pushdown scenarios.

## <a id="tf6408"> </a> Use TF6408

By default, the estimated execution plan will not expose the remote query plan, and you will only see the remote query operator object. For example, an estimated execution plan from SQL Server Management Studio (SSMS):

![View estimated execution plan from SSMS](./media/polybase-how-to-tell-pushdown-computation/1-exec-plan-without-t6408-ssms.png)

Or, in Azure Data Studio:

![View estimated execution plan from Azure Data Studio](./media/polybase-how-to-tell-pushdown-computation/2-exec-plan-without-t6408-azure-data-studio.png)

Starting in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], you can enable a new trace flag 6408 globally using [DBCC TRACEON](../../t-sql/database-console-commands/dbcc-traceon-transact-sql.md). For example:

```tsql
DBCC TRACEON (6408, -1);  
```

This trace flag only works with estimated execution plans and has no effect on actual execution plans. This trace flag exposes information about the Remote Query operator that will show what is happening during the Remote Query phase.

[Execution plans](../performance/execution-plans.md) are read from right-to-left, as indicated by the direction of the arrows. If an operator is to the right of another operator, it is said to be "before" it. If an operator is to the left of another operator, it is said to be "after" it.

* In SSMS, highlight the query and select Display Estimated Execution Plan from the toolbar or use Ctrl+L. 
* In Azure Data Studio, highlight the query and click on "Explain". Then consider the following scenarios below to determine whether pushdown occurred.

Each of the below examples will include the output from SSMS and Azure Data Studio.

### Pushdown of filter predicate (view with execution plan)

Consider the following query, which uses a filter predicate in the WHERE clause:

```tsql
SELECT *
FROM [Person].[BusinessEntity] AS be
WHERE be.BusinessEntityID = 17907;
```

If pushdown of the filter predicate is occurring, the filter operator will be before the external operator. This indicates the filtering occurred before getting selected back from the external data source, indicating the filter predicate was pushed down.

#### With pushdown of filter predicate (view with execution plan)

With the Trace Flag 6408 enabled, you now see additional information in the estimated execution plan output. The output varies between SSMS and Azure Data Studio.

In SSMS, the remote query plan is displayed in the estimated execution plan as Query 2 (**sp_execute_memo_node_1**) and corresponds to the Remote Query operator in Query 1. For example:

![View execution plan with filter predicate pushdown from SSMS](./media/polybase-how-to-tell-pushdown-computation/3-exec-plan-with-t6408-filter-pushdown-ssms.png)

In Azure Data Studio, the remote query execution is instead represented as a single query plan. For example:

![View execution plan with filter predicate pushdown from Azure Data Studio](./media/polybase-how-to-tell-pushdown-computation/4-exec-plan-with-t6408-filter-pushdown-azure-data-studio.png)

#### Without pushdown of filter predicate (view with execution plan)

If pushdown of the filter predicate is not occurring the filter will be after the external operator.

The estimated execution plan from SSMS:

![View execution plan without filter predicate pushdown from SSMS](./media/polybase-how-to-tell-pushdown-computation/5-exec-plan-with-t6408-no-filter-pushdown-ssms.png)

The estimated execution plan from Azure Data Studio:

![View execution plan without filter predicate pushdown from Azure Data Studio](./media/polybase-how-to-tell-pushdown-computation/6-exec-plan-with-t6408-no-filter-pushdown-azure-data-studio.png)

### Pushdown of JOIN

Consider the following query that utilizes the JOIN operator:

```tsql
SELECT be.BusinessEntityID, bea.AddressID
FROM [Person].[BusinessEntity] AS be
INNER JOIN [Person].[BusinessEntityAddress] AS bea
ON be.BusinessEntityID = bea.BusinessEntityID;
```

If the JOIN is pushed down to the external data source, the Join operator will be before the external operator.

#### With pushdown of join (view with execution plan)

The estimated execution plan from SSMS:

![View execution plan with join pushdown from SSMS](./media/polybase-how-to-tell-pushdown-computation/7-exec-plan-with-t6408-join-pushdown-ssms.png)

The estimated execution plan from Azure Data Studio:

![View execution plan with join pushdown from Azure Data Studio](./media/polybase-how-to-tell-pushdown-computation/8-exec-plan-with-t6408-join-pushdown-azure-data-studio.png)

#### Without pushdown of join (view with execution plan)

If the JOIN is not pushed down to the external data source, the Join operator will be after the external operator. In SSMS, the external operator is in the query plan for **sp_execute_memo_node**, which is in the Remote Query operator in Query 1. In Azure Data Studio, the Join operator is after the external operator(s).

The estimated execution plan from SSMS:

![View execution plan without join pushdown from SSMS](./media/polybase-how-to-tell-pushdown-computation/9-exec-plan-with-t6408-no-join-pushdown-ssms.png)

The estimated execution plan from Azure Data Studio:

![View execution plan without join pushdown from Azure Data Studio](./media/polybase-how-to-tell-pushdown-computation/10-exec-plan-with-t6408-no-join-pushdown-azure-data-studio.png)

### Pushdown of aggregation (view with execution plan)

Consider the following query, which uses an aggregate function:

```tsql
SELECT SUM([Quantity]) as Quant
FROM [AdventureWorks2017].[Production].[ProductInventory];
```

#### With pushdown of aggregation (view with execution plan)

If pushdown of the aggregation is occurring, the aggregation operator will be before the external operator. This indicates the aggregation occurred before getting selected back from the external data source, indicating the aggregation was pushed down.

The estimated execution plan from SSMS:

![View execution plan with aggregate pushdown from SSMS](./media/polybase-how-to-tell-pushdown-computation/11-exec-plan-with-t6408-aggregate-pushdown-ssms.png)

The estimated execution plan from Azure Data Studio:

![View execution plan with aggregate pushdown from Azure Data Studio](./media/polybase-how-to-tell-pushdown-computation/12-exec-plan-with-t6408-aggregate-pushdown-azure-data-studio.png)

#### Without pushdown of aggregation (view with execution plan)

If pushdown of the aggregation is not occurring the aggregation operator will be after the external operator.

The estimated execution plan from SSMS:

![View execution plan without aggregate pushdown from SSMS](./media/polybase-how-to-tell-pushdown-computation/13-exec-plan-with-t6408-no-aggregate-pushdown-ssms.png)

The estimated execution plan from Azure Data Studio:

![View execution plan without aggregate pushdown from Azure Data Studio](./media/polybase-how-to-tell-pushdown-computation/14-exec-plan-with-t6408-no-aggregate-pushdown-azure-data-studio.png)

## <a id="dmv"> </a>Use DMV

Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], the `read_command` column of [sys.dm_exec_external_work](../system-dynamic-management-views/sys-dm-exec-external-work-transact-sql.md) DMV will show the query that is sent to the external data source. This will allow you determine if pushdown is occurring, but does not expose the execution plan. Viewing the remote query does not require TF6408.

> [!NOTE]
> For Hadoop and Azure storage, the `read_command` always returns `NULL`.

You can execute the following query and use the `start_time`/`end_time` and `read_command` to identify the query being investigated:

```tsql
SELECT execution_id, start_time, end_time, read_command
FROM sys.dm_exec_external_work
ORDER BY execution_id desc;
```

> [!NOTE]
> One limitation of the [sys.dm_exec_external_work](../system-dynamic-management-views/sys-dm-exec-external-work-transact-sql.md) method is that the `read_command` field in the DMV is limited to 4000 characters. If the query is sufficiently long, the `read_command` may be truncated before you'd see the WHERE/JOIN/aggregation function in the `read_command`.

### Pushdown of filter predicate (view with DMV)

Consider the query used in the previous filter predicate example:

```tsql
SELECT *
FROM [Person].[BusinessEntity] be
WHERE be.BusinessEntityID = 17907;
```

#### With pushdown of filter (view with DMV)

If pushdown of the filter predicate is occurring, you will be able to tell by checking the `read_command` in the DMV. You will see something like this sample:

```tsql
SELECT [T1_1].[BusinessEntityID] AS [BusinessEntityID], [T1_1].[rowguid] AS [rowguid], 
  [T1_1].[ModifiedDate] AS [ModifiedDate] FROM 
  (SELECT [T2_1].[BusinessEntityID] AS [BusinessEntityID], [T2_1].[rowguid] AS [rowguid], 
    [T2_1].[ModifiedDate] AS [ModifiedDate] 
FROM [AdventureWorks2017].[Person].[BusinessEntity] AS T2_1 
WHERE ([T2_1].[BusinessEntityID] = CAST ((17907) AS INT))) AS T1_1;
```

The WHERE clause is in the command sent to the external data source, which means the filter predicate is being evaluated at the external data source. Filtering on the dataset occurred at the external data source, and only the filtered dataset was retrieved by PolyBase.

#### Without pushdown of filter (view with DMV)

If pushdown is not occurring, you'll see something like:

```tsql
SELECT "BusinessEntityID","rowguid","ModifiedDate" FROM "AdventureWorks2017"."Person"."BusinessEntity"
```

There is no WHERE clause in the command sent to the external data source, so the filter predicate is not pushed down. Filtering on the entire dataset occurred on the SQL Server side, after the dataset was retrieved by PolyBase.

### Pushdown of JOIN (view with DMV)

Consider the query used in the previous JOIN example:

```tsql
SELECT be.BusinessEntityID, bea.AddressID
FROM [Person].[BusinessEntity] be
INNER JOIN [Person].[BusinessEntityAddress] bea ON be.BusinessEntityID = bea.BusinessEntityID;
```

#### With pushdown of join (view with DMV)

If the JOIN is pushed down to the external data source, you will see something like:

```tsql
SELECT [T1_1].[BusinessEntityID] AS [BusinessEntityID], [T1_1].[AddressID] AS [AddressID] 
FROM (SELECT [T2_2].[BusinessEntityID] AS [BusinessEntityID], [T2_1].[AddressID] AS [AddressID] 
FROM [AdventureWorks2017].[Person].[BusinessEntityAddress] AS T2_1 
INNER JOIN  [AdventureWorks2017].[Person].[BusinessEntity] AS T2_2  
ON ([T2_1].[BusinessEntityID] = [T2_2].[BusinessEntityID])) AS T1_1;
```

The JOIN clause is in the command sent to the external data source, so the JOIN is pushed down. The join on the dataset occurred at the external data source, and only the dataset that matches the join condition was retrieved by PolyBase.

#### Without pushdown of join (view with DMV)

If the pushdown of the join is not occurring, you'll see there are two different queries executed against the external data source:

```tsql
SELECT [T1_1].[BusinessEntityID] AS [BusinessEntityID], [T1_1].[AddressID] AS [AddressID] 
FROM [AdventureWorks2017].[Person].[BusinessEntityAddress] AS T1_1;

SELECT [T1_1].[BusinessEntityID] AS [BusinessEntityID] FROM [AdventureWorks2017].[Person].[BusinessEntity] AS T1_1;
```

The joining the two datasets occurred on the SQL Server side, after both datasets were retrieved by PolyBase.

### Pushdown of aggregation (view with DMV)

Consider the following query, which uses an aggregate function:

```tsql
SELECT SUM([Quantity]) as Quant
FROM [AdventureWorks2017].[Production].[ProductInventory];
```

#### With Pushdown of aggregation (view with DMV)

If pushdown of the aggregation is occurring, you'll see the aggregation function in the `read_command`. For example:

```tsql
SELECT [T1_1].[col] AS [col] FROM (SELECT SUM([T2_1].[Quantity]) AS [col] 
FROM [AdventureWorks2017].[Production].[ProductInventory] AS T2_1) AS T1_1
```

The aggregation function is in the command sent to the external data source, so the aggregation is pushed down. The aggregation occurred at the external data source, and only the dataset that has been aggregated was retrieved by PolyBase.

#### Without pushdown of aggregation (view with DMV)

If the pushdown of the aggregation isn't occurring, you won't see the aggregation function in the `read_command`. For example:

```tsql
SELECT "Quantity" FROM "AdventureWorks2017"."Production"."ProductInventory"
```

The aggregation was performed in SQL Server, after the un-aggregated dataset was retrieved by PolyBase.

## Next steps

- [PolyBase troubleshooting](polybase-troubleshooting.md)
- [PolyBase errors and possible solutions](polybase-errors-and-possible-solutions.md)   
