---
title: Change Workload Group Settings
description: Learn how to change workload group settings of the default and user-defined workload groups using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "workload groups [SQL Server], alter"
  - "Resource Governor, workload group alter"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Change workload group settings

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can change workload group settings by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

<a id="Permissions"></a>

## Permissions

Changing workload group settings requires the `CONTROL SERVER` permission.

<a id="ChgWGProp"></a>

## Change workload group settings using SQL Server Management Studio

To change workload group settings using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In Object Explorer, expand the **Management** node down to and including the **Workload Groups** folder that contains the workload group to be modified.
1. Use the context menu for the workload group to be modified, and select **Properties**.
1. In the **Resource Governor Properties** page, select the row for the workload group in the **Workload groups for resource pool** grid.
1. Select the cells in the row to be changed, and enter new values.
1. To save the changes, select **OK**.

<a id="ChgWGTSQL"></a>

## Change workload group settings using Transact-SQL

To change workload group settings using [!INCLUDE[tsql](../../includes/tsql-md.md)]:

1. Execute the [ALTER WORKLOAD GROUP](../../t-sql/statements/alter-workload-group-transact-sql.md) statement specifying the values to be changed.
1. Execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement for the changes to take effect.

### Example

The following example changes the max memory grant percent setting for the workload group named `groupAdhoc` and makes the new configuration effective.

```sql
ALTER WORKLOAD GROUP groupAdhoc WITH (REQUEST_MAX_MEMORY_GRANT_PERCENT = 30);

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](resource-governor.md)
- [Create a workload group](create-a-workload-group.md)
- [Create a resource pool](create-a-resource-pool.md)
- [Change resource pool settings](change-resource-pool-settings.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/alter-workload-group-transact-sql.md)
- [ALTER RESOURCE POOL (Transact-SQL)](../../t-sql/statements/alter-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
