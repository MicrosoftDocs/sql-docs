---
title: Create a Workload Group
description: Learn how to create a workload group using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "Resource Governor, workload group create"
  - "workload groups [SQL Server], create"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Create a workload group

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can create a workload group by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

<a id="Permissions"></a>

## Permissions

Creating a workload group requires the `CONTROL SERVER` permission.

<a id="CreRPProp"></a>

## Create a workload group using SQL Server Management Studio

To create a workload group using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In Object Explorer, expand the **Management** node down to and including the resource pool that contains the workload group to be modified.
1. Use the **Workload Groups** context menu, and select **New Workload Group**.
1. In the **Resource pools** grid, ensure the resource pool where you want to add the workload group is selected.
1. The **Workload groups for resource pool** grid has a new row with a blank name and default values in the other columns.
1. Select the **Name** cell and enter a name for the workload group.
1. Select any other cells in the row you want to change from their default settings, and enter new values.
1. To save the changes, select **OK**.

<a id="CreRPTSQL"></a>

## Create a workload group using Transact-SQL

To create a workload group by using [!INCLUDE[tsql](../../includes/tsql-md.md)]:

1. Execute the [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md) statement specifying the values to be set.
1. Execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement for the changes to take effect.

### Example

The following example creates a workload group named `groupAdhoc` in the resource pool named `poolAdhoc`, and configures the maximum memory grant per request.

```sql
CREATE WORKLOAD GROUP groupAdhoc WITH (REQUEST_MAX_MEMORY_GRANT_PERCENT = 30)
USING poolAdhoc;

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

Resource pools can govern a variety of system resources. For more information, see [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md).

For more samples and a complete walkthrough, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Create a resource pool](create-a-resource-pool.md)
- [Change workload group settings](change-workload-group-settings.md)
- [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/create-workload-group-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
- [CREATE EXTERNAL RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-external-resource-pool-transact-sql.md)
