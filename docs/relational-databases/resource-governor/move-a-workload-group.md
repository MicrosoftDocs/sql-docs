---
title: Move a Workload Group
description: Learn how to move a resource governor workload group to a different resource pool by using either SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
f1_keywords:
  - "sql13.swb.rg.properties_moveworkloadgroup.f1"
helpviewer_keywords:
  - "workload groups [SQL Server], move"
  - "Resource Governor, workload group move"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Move a workload group

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can move a resource governor workload group to a different resource pool by using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

You can't move a workload group if there's a pending resource governor configuration operation.

<a id="LimitationsRestrictions"></a>

### Limitations

- You can't move a workload group if there's a pending resource governor configuration operation. You can determine whether there's a configuration pending by querying the [sys.dm_resource_governor_configuration](../system-dynamic-management-views/sys-dm-resource-governor-configuration-transact-sql.md) dynamic management view to get the current value of the `is_configuration_pending` column.
- If a workload group contains active sessions, moving it to a different resource pool fails when the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement is executed to apply the change. To avoid this problem, you can take one of the following actions:
  - Wait until all sessions in the affected group disconnect, and then execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.
  - Explicitly stop sessions in the affected group by using the [KILL](../../t-sql/language-elements/kill-transact-sql.md) T-SQL command, and then execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement. If you decide that you don't want to explicitly stop sessions, move the group to the original resource pool.
  - Restart the server. When the server restarts, a moved group uses the new resource pool assignment.

<a id="Permissions"></a>

### Permissions

Moving a workload group requires the `CONTROL SERVER` permission.

<a id="MoveWGSSMS"></a>

## Move a workload group using SQL Server Management Studio

To move a workload group by using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In Object Explorer, expand the **Management** node down to **Resource Governor**.
1. Open the **Resource Governor** context menu and select **Properties**. This opens the **Resource Governor Properties** page.
1. In the **Resource Pools** grid, select the resource pool containing the workload group to be moved. The **Workload Groups** grid now lists the workload groups in that resource pool.
1. In the **Workload Groups** grid, open the context menu for the workload group to be moved, and select **Move to**. This opens a **Move Workload Group** window.
1. Available resource pools are displayed in the window. Select the resource pool that you want to move the workload group to, and select **OK**.
1. Select **OK** to execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.
1. If the create or reconfigure operation fails for the resource pool or workload group, a summary error message appears below the title of the property page. To see a detailed error message, select the down arrow on the error message.

<a id="MoveWGTSQL"></a>

## Move a workload group using Transact-SQL

To move a workload group by using [!INCLUDE[tsql](../../includes/tsql-md.md)]:

1. Execute the [ALTER WORKLOAD GROUP](../../t-sql/statements/alter-workload-group-transact-sql.md) statement specifying the name of the workload group to be moved and the resource pool to which it should be moved.
1. Execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.

### Example

The following example moves a workload group named `groupAdhoc` to the `default` resource pool.

```sql
ALTER WORKLOAD GROUP groupAdhoc USING [default];

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Create a resource pool](create-a-resource-pool.md)
- [Create a workload group](create-a-workload-group.md)
- [ALTER WORKLOAD GROUP](../../t-sql/statements/alter-workload-group-transact-sql.md)
- [ALTER RESOURCE GOVERNOR](../../t-sql/statements/alter-resource-governor-transact-sql.md)
