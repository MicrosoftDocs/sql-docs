---
title: "sp_create_plan_guide_from_handle (Transact-SQL)"
description: sp_create_plan_guide_from_handle creates one or more plan guides from a query plan in the plan cache.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_create_plan_guide_from_handle_TSQL"
  - "sp_create_plan_guide_from_handle"
helpviewer_keywords:
  - "sp_create_plan_guide_from_handle"
dev_langs:
  - "TSQL"
---
# sp_create_plan_guide_from_handle (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates one or more plan guides from a query plan in the plan cache. You can use this stored procedure to ensure the query optimizer always uses a specific query plan for a specified query. For more information about plan guides, see [Plan Guides](../performance/plan-guides.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_create_plan_guide_from_handle
    [ @name = ] N'name'
    , [ @plan_handle = ] plan_handle
    [ , [ @statement_start_offset = ] statement_start_offset ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the plan guide. *@name* is **sysname**, with no default. Plan guide names are scoped to the current database. *@name* must comply with the rules for [identifiers](../databases/database-identifiers.md) and can't start with the number sign (`#`). The maximum length of *@name* is 124 characters.

#### [ @plan_handle = ] *plan_handle*

Identifies a batch in the plan cache. *@plan_handle* is **varbinary(64)**, with no default. *@plan_handle* can be obtained from the [sys.dm_exec_query_stats](../system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md) dynamic management view.

#### [ @statement_start_offset = ] *statement_start_offset*

Identifies the starting position of the statement within the batch of the specified *@plan_handle*. *@statement_start_offset* is **int**, with a default of `NULL`.

The statement offset corresponds to the `statement_start_offset` column in the [sys.dm_exec_query_stats](../system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md) dynamic management view.

When `NULL` is specified or a statement offset isn't specified, a plan guide is created for each statement in the batch using the query plan for the specified plan handle. The resulting plan guides are equivalent to plan guides that use the `USE PLAN` query hint to force the use of a specific plan.

## Remarks

A plan guide can't be created for all statement types. If a plan guide can't be created for a statement in the batch, the stored procedure ignores the statement and continues to the next statement in the batch. If a statement occurs multiple times in the same batch, the plan for the last occurrence is enabled and previous plans for the statement are disabled. If no statements in the batch can be used in a plan guide, error 10532 is raised and the statement fails. We recommend that you always obtain the plan handle from the `sys.dm_exec_query_stats` dynamic management view to help avoid the possibility of this error.

> [!IMPORTANT]  
> `sp_create_plan_guide_from_handle` creates plan guides based on plans as they appear in the plan cache. This means that the batch text, [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, and XML Showplan are taken character-by-character (including any literal values passed to the query) from the plan cache into the resulting plan guide. These text strings can contain sensitive information that is then stored in the metadata of the database. Users with appropriate permissions can view this information by using the `sys.plan_guides` catalog view and the **Plan Guide Properties** dialog box in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. To ensure that sensitive information isn't disclosed through a plan guide, we recommend reviewing the plan guides created from the plan cache.

## Create plan guides for multiple statements within a query plan

Like `sp_create_plan_guide`, `sp_create_plan_guide_from_handle` removes the query plan for the targeted batch or module from the plan cache. This is done to ensure that all users begin using the new plan guide. When creating a plan guide for multiple statements within a single query plan, you can postpone the removal of the plan from the cache by creating all the plan guides in an explicit transaction. This method allows the plan to remain in the cache until the transaction is complete and a plan guide for each specified statement is created. See Example B.

## Permissions

Requires `VIEW SERVER STATE` permission. In addition, individual permissions are required for each plan guide that is created by using `sp_create_plan_guide_from_handle`. Creating a plan guide of type `OBJECT` requires `ALTER` permission on the referenced object. Creating a plan guide of type `SQL` or `TEMPLATE` requires `ALTER` permission on the current database. To determine the plan guide type that will be created, run the following query:

```sql
SELECT cp.plan_handle,
    sql_handle,
    st.text,
    objtype
FROM sys.dm_exec_cached_plans AS cp
INNER JOIN sys.dm_exec_query_stats AS qs
    ON cp.plan_handle = qs.plan_handle
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS st;
```

In the row that contains the statement for which you create the plan guide, examine the `objtype` column in the result set. A value of `Proc` indicates the plan guide is of type OBJECT. Other values such as `Ad hoc` or `Prepared` indicate the plan guide is of type SQL.

## Examples

### A. Create a plan guide from a query plan in the plan cache

The following example creates a plan guide for a single `SELECT` statement by specifying a query plan from the plan cache. The example begins by executing a simple `SELECT` statement for which the plan guide will be created. The plan for this query is examined by using the `sys.dm_exec_sql_text` and `sys.dm_exec_text_query_plan` dynamic management views. The plan guide is then created for the query by specifying the query plan in the plan cache that is associated with the query. The final statement in the example verifies that the plan guide exists.

```sql
USE AdventureWorks2022;
GO

SELECT WorkOrderID,
    p.Name,
    OrderQty,
    DueDate
FROM Production.WorkOrder AS w
INNER JOIN Production.Product AS p
    ON w.ProductID = p.ProductID
WHERE p.ProductSubcategoryID > 4
ORDER BY p.Name, DueDate;
GO

-- Inspect the query plan by using dynamic management views.
SELECT *
FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(sql_handle)
CROSS APPLY sys.dm_exec_text_query_plan(
    qs.plan_handle, qs.statement_start_offset,
    qs.statement_end_offset
) AS qp
WHERE TEXT LIKE N'SELECT WorkOrderID, p.Name, OrderQty, DueDate%';
GO

-- Create a plan guide for the query by specifying the query plan in the plan cache.
DECLARE @plan_handle VARBINARY(64);
DECLARE @offset INT;

SELECT @plan_handle = plan_handle,
    @offset = qs.statement_start_offset
FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS st
CROSS APPLY sys.dm_exec_text_query_plan(
    qs.plan_handle, qs.statement_start_offset,
    qs.statement_end_offset
) AS qp
WHERE text LIKE N'SELECT WorkOrderID, p.Name, OrderQty, DueDate%';

EXECUTE sp_create_plan_guide_from_handle @name = N'Guide1',
    @plan_handle = @plan_handle,
    @statement_start_offset = @offset;
GO

-- Verify that the plan guide is created.
SELECT *
FROM sys.plan_guides
WHERE scope_batch LIKE N'SELECT WorkOrderID, p.Name, OrderQty, DueDate%';
GO
```

### B. Create multiple plan guides for a multistatement batch

The following example creates a plan guide for two statements within a multistatement batch. The plan guides are created within an explicit transaction so that the query plan for the batch isn't removed from the plan cache after the first plan guide is created. The example begins by executing a multistatement batch. The plan for the batch is examined by using dynamic management views. A row for each statement in the batch is returned. A plan guide is then created for the first and third statements in the batch by specifying the `@statement_start_offset` parameter. The final statement in the example verifies that the plan guides exist.

:::code language="sql" source="codesnippet/tsql/sp-create-plan-guide-fro_1.sql":::

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sys.dm_exec_query_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)
- [Plan Guides](../performance/plan-guides.md)
- [sp_create_plan_guide (Transact-SQL)](sp-create-plan-guide-transact-sql.md)
- [sys.dm_exec_sql_text (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)
- [sys.dm_exec_text_query_plan (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-text-query-plan-transact-sql.md)
- [sp_control_plan_guide (Transact-SQL)](sp-control-plan-guide-transact-sql.md)
