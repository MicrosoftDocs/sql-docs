---
title: Delete a Resource Pool
description: Learn how to delete a resource pool using either SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "Resource Governor, resource pool delete"
  - "resource pools [SQL Server], delete"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Delete a resource pool

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can delete a resource pool by using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

<a id="BeforeYouBegin"></a>

<a id="LimitationsRestrictions"></a>

## Limitations

- You can't delete the built-in `default` or `internal` resource pools.
- You can't delete a resource pool if it contains workload groups. For more information, see [Delete a workload group](../../relational-databases/resource-governor/delete-a-workload-group.md).

<a id="Permissions"></a>

### Permissions

Deleting a resource pool requires `CONTROL SERVER` permission.

<a id="DelRPSSMS"></a>

## Delete a resource pool using Object Explorer

To delete a resource pool using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to and including **Resource Governor**.
1. Open the context menu of the resource pool to be deleted and select **Delete**.
1. In the **Delete Object** window, the resource pool is listed in the **Object to be deleted** list. To delete the resource pool, select **OK**.

    > [!NOTE]
    >  If the resource pool that you are trying to delete contains a workload group, this action fails.

<a id="DelRPTSQL"></a>

## Delete a resource pool using Transact-SQL

To delete a resource pool using [!INCLUDE[tsql](../../includes/tsql-md.md)]:

1. Execute the [DROP RESOURCE POOL](../../t-sql/statements/drop-resource-pool-transact-sql.md) or [DROP EXTERNAL RESOURCE POOL](../../t-sql/statements/drop-external-resource-pool-transact-sql.md) statement specifying the name of the resource pool to delete.
1. Execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement for the changes to take effect.

### Example

The following example deletes a resource pool named `poolAdhoc` and makes the new configuration effective.

```sql
DROP RESOURCE POOL poolAdhoc;

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](../../relational-databases/resource-governor/resource-governor.md)
- [Resource governor resource pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
- [Create a resource pool](../../relational-databases/resource-governor/create-a-resource-pool.md)
- [Change resource pool settings](../../relational-databases/resource-governor/change-resource-pool-settings.md)
- [Resource governor workload group](../../relational-databases/resource-governor/resource-governor-workload-group.md)
- [Resource governor classifier function](../../relational-databases/resource-governor/resource-governor-classifier-function.md)
- [DROP WORKLOAD GROUP](../../t-sql/statements/drop-workload-group-transact-sql.md)
- [DROP RESOURCE POOL](../../t-sql/statements/drop-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR](../../t-sql/statements/alter-resource-governor-transact-sql.md)
- [DROP EXTERNAL RESOURCE POOL](../../t-sql/statements/drop-external-resource-pool-transact-sql.md)
- [CREATE EXTERNAL RESOURCE POOL](../../t-sql/statements/create-external-resource-pool-transact-sql.md)
- [ALTER EXTERNAL RESOURCE POOL](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
