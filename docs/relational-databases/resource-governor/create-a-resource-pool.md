---
title: Create a Resource Pool
description: Learn how to create a resource pool using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "resource pools [SQL Server], create"
  - "Resource Governor, resource pool create"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Create a resource pool

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can create a resource pool by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. To understand the concepts related to resource pools, see [Resource governor resource pool](resource-governor-resource-pool.md).

<a id="BeforeYouBegin"></a>

<a id="LimitationsRestrictions"></a>

## Limitations

- The maximum CPU percentage must be equal to or higher than the minimum CPU percentage. The maximum memory percentage must be equal to or higher than the minimum memory percentage.
- The sums of the minimum CPU percentages and minimum memory percentages for all resource pools must not exceed 100.

<a id="Permissions"></a>

### Permissions

Creating a resource pool requires `CONTROL SERVER` permission.

<a id="CreRPProp"></a>

## Create a resource pool using SQL Server Management Studio

To create a resource pool using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to and including **Resource Governor**.
1. Open the **Resource Governor** context menu and select **Properties**.
1. In the **Resource pools** grid, select the empty row. This row labeled with an asterisk (`*`).
1. Select the empty cell in the **Name** column. Enter the resource pool name.
1. Select any other cells in the row you want to change, and enter new values.
1. To save the changes, select **OK**.

<a id="CreRPTSQL"></a>

## Create a resource pool using Transact-SQL

To create a resource pool using [!INCLUDE[tsql](../../includes/tsql-md.md)]:

1. Execute the [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md) or [CREATE EXTERNAL RESOURCE POOL](../../t-sql/statements/create-external-resource-pool-transact-sql.md) statement specifying the property values to be set.
1. Execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.

### Example

The following example creates a resource pool named `poolAdhoc` and makes the new configuration effective.

```sql
CREATE RESOURCE POOL poolAdhoc WITH (MAX_CPU_PERCENT = 20);

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

Resource pools can govern a variety of system resources. For more information, see [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md).

For more samples and a complete walkthrough, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Change resource pool settings](change-resource-pool-settings.md)
- [Delete a resource pool](delete-a-resource-pool.md)
- [Configure resource governor using a template](configure-resource-governor-using-a-template.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
- [CREATE RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
- [CREATE EXTERNAL RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-external-resource-pool-transact-sql.md)
- [ALTER EXTERNAL RESOURCE POOL (Transact-SQL)](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
