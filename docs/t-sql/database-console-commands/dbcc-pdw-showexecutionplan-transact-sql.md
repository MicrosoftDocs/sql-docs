---
title: DBCC PDW_SHOWEXECUTIONPLAN (Transact-SQL)
description: DBCC PDW_SHOWEXECUTIONPLAN displays the execution plan for a query running on a specific Azure Synapse Analytics compute node or control node.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: data-warehouse
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---

# DBCC PDW_SHOWEXECUTIONPLAN (Transact-SQL)

[!INCLUDE[asa-md](../../includes/applies-to-version/asa.md)]

Displays the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] execution plan for a query running on a specific [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] Compute node or Control node. Use this to troubleshoot query performance problems while queries are running on the Compute nodes and Control node.

Once query performance problems are understood for SMP [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] queries running on the Compute nodes, there are several ways to improve performance. Possible ways to improve query performance on the Compute nodes include creating multi-column statistics, creating nonclustered indexes, or using query hints.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for Azure Synapse Analytics:

```syntaxsql
DBCC PDW_SHOWEXECUTIONPLAN ( distribution_id , spid )
[;]
```

> [!NOTE]  
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### *distribution_id*

 Identifier for the distribution that is running the query plan. This is an integer and can't be `NULL`. Value must be between 1 and 60. Used when targeting [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

#### *spid*

 Identifier for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] session that is running the query plan. This is an integer and can't be `NULL`.

## Permissions

 Requires CONTROL permission on [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

Requires **VIEW SERVER STATE** permission on the Appliance.

## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]

### A. DBCC PDW_SHOWEXECUTIONPLAN basic syntax

The following sample query will return the `sql_spid` for each actively running distribution.

```sql
SELECT [sql_spid]
    , [pdw_node_id]
    , [request_id]
    , [dms_step_index]
    , [type]
    , [start_time]
    , [end_time]
    , [status]
    , [distribution_id]
FROM sys.dm_pdw_dms_workers
WHERE [status] <> 'StepComplete'
    AND [status] <> 'StepError'
ORDER BY request_id
    , [dms_step_index];
```

If you are curious as to what `distribution_id` 1 was running in session 375, you would run the following command:

```sql
DBCC PDW_SHOWEXECUTIONPLAN (1, 375);
```

## Related content

- [DBCC PDW_SHOWPARTITIONSTATS (Transact-SQL)](dbcc-pdw-showpartitionstats-transact-sql.md)
- [DBCC PDW_SHOWSPACEUSED (Transact-SQL)](dbcc-pdw-showspaceused-transact-sql.md)
- [Table size queries](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-overview#table-size-queries)
