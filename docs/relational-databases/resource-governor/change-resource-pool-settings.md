---
title: Change Resource Pool Settings
description: Learn how to change resource pool settings using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "Resource Governor, resource pool alter"
  - "resource pools [SQL Server], alter"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Change resource pool settings

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can change resource pool settings by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

<a id="BeforeYouBegin"></a>
<a id="LimitationsRestrictions"></a>

## Limitations

- The maximum CPU percentage must be equal to or higher than the minimum CPU percentage. The maximum memory percentage must be equal to or higher than the minimum memory percentage.
- The sums of the minimum CPU percentages and minimum memory percentages for all resource pools must not exceed 100.

<a id="Permissions"></a>

## Permissions

Changing resource pool settings requires the `CONTROL SERVER` permission.

<a id="ChgRPProp"></a>

## Change resource pool settings using SQL Server Management Studio

To change resource pool settings using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to and including **Resource Pools**.
1. Use the context menu for the resource pool to be modified, and select **Properties**.
1. In the **Resource Governor Properties** page, select the row for the resource pool in the **Resource pools** grid.
1. Select the cells in the row to be changed, and enter new values.
1. To save the changes, select **OK**

<a id="ChgRPTSQL"></a>

## Change resource pool settings using Transact-SQL

To change resource pool settings using [!INCLUDE[tsql](../../includes/tsql-md.md)]:

1. Execute the [ALTER RESOURCE POOL](../../t-sql/statements/alter-resource-pool-transact-sql.md) or [ALTER EXTERNAL RESOURCE POOL](../../t-sql/statements/alter-external-resource-pool-transact-sql.md) statement specifying the values to be changed.
1. Execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement for the changes to take effect.

### Example

The following example changes the max CPU percentage setting for the resource pool named `poolAdhoc` and makes the new configuration effective.

```sql
ALTER RESOURCE POOL poolAdhoc WITH (MAX_CPU_PERCENT = 25);

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Create a resource pool](create-a-resource-pool.md)
- [Delete a resource pool](delete-a-resource-pool.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
- [ALTER RESOURCE POOL (Transact-SQL)](../../t-sql/statements/alter-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
- [CREATE EXTERNAL RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-external-resource-pool-transact-sql.md)
- [ALTER EXTERNAL RESOURCE POOL (Transact-SQL)](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
