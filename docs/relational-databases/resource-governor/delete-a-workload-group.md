---
title: Delete a Workload Group
description: Learn how to delete a workload group using either SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "workload groups [SQL Server], delete"
  - "Resource Governor, workload group delete"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Delete a workload group

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can delete a workload group or resource pool using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

<a id="LimitationsRestrictions"></a>

### Limitations

You can't delete a workload group if it contains active sessions.

If a workload group contains active sessions, deleting the workload group fails when the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement is executed to apply the change. To avoid this problem, you can take one of the following actions:

- Wait until all sessions in the affected group disconnect, and then execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.
- Explicitly stop sessions in the affected group by using the [KILL](../../t-sql/language-elements/kill-transact-sql.md) command, and then execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement. If you decide that you don't want to explicitly stop sessions, re-create the group by using the original name and settings.
- Restart the server. When the server restarts, the deleted group is deleted permanently.

<a id="Permissions"></a>

### Permissions

Deleting a workload group requires the `CONTROL SERVER` permission.

<a id="DelWGObjEx"></a>

## Delete a workload group using Object Explorer in SQL Server Management Studio

To delete a workload group using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to and including **Resource Pools**.
1. Expand **Resource Pools** down to and including the **Workload Groups** node in the resource pool that contains the workload group to be deleted.
1. Open the context menu for the workload group and select **Delete**.
1. In the **Delete Object** window, the workload group is listed in the **Object to be deleted** list. To delete the workload group, select **OK**.

<a id="DelWGRGProp"></a>

## Delete a workload group using resource governor properties

To delete a workload group by using the resource governor properties page:

1. In Object Explorer, expand the **Management** node down to and including **Resource Pools**.
1. Open the context menu for the resource pool that contains the workload group to be deleted and select **Properties**. This opens the **Resource Governor Properties** page.
1. In the **Workload groups for resource pool** window, select the row for the workload group to be deleted. Open the context menu and select **Delete**.
1. To delete the workload group, select **OK**.

<a id="DelWGTSQL"></a>

## Delete a workload group using Transact-SQL

To delete a workload group using [!INCLUDE[tsql](../../includes/tsql-md.md)]:

1. Execute the [DROP WORKLOAD GROUP](../../t-sql/statements/drop-workload-group-transact-sql.md) statement specifying the name of the workload group to delete.
1. Before you execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement, verify that there are no active requests in the workload group being deleted. If there are active requests, `ALTER RESOURCE GOVERNOR` fails. For more information and for solutions, see [Limitations and restrictions](#LimitationsRestrictions).
1. Execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.

### Example

The following example deletes a workload group named `groupAdhoc` and makes the new configuration effective.

```sql
DROP WORKLOAD GROUP groupAdhoc;

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](resource-governor.md)
- [Create a resource pool](create-a-resource-pool.md)
- [Create a workload group](create-a-workload-group.md)
- [Delete a resource pool](delete-a-resource-pool.md)
- [DROP WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/drop-workload-group-transact-sql.md)
- [DROP RESOURCE POOL (Transact-SQL)](../../t-sql/statements/drop-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
