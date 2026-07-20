---
title: Disable Resource Governor
description: Learn how to disable resource governor using either SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "Resource Governor, disabling"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Disable resource governor

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can disable resource governor using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

Disabling resource governor has the following immediate effects:

- The classifier function isn't executed when a new connection is opened.
- New connections are automatically classified into the `default` workload group.
- All existing workload group and resource pool settings are reset to their default values.
- No events are fired when limits are reached.
- Resource governor configuration changes can be made, but the changes don't take effect until resource governor is enabled.

<a id="LimitationsRestrictions"></a>

### Limitations

You can't use the `ALTER RESOURCE GOVERNOR` statement as part of a user transaction.

<a id="Permissions"></a>

### Permissions

Disabling resource governor requires the `CONTROL SERVER` permission.

<a id="RGOffObjEx"></a>

## Disable resource governor using Object Explorer in SQL Server Management Studio

To disable resource governor using Object Explorer in [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to **Resource Governor**.
1. From the **Resource Governor** context menu, select **Disable**.

<a id="RGOffProp"></a>

## Disable resource governor using resource governor properties

To disable resource governor using the resource governor properties page:

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to **Resource Governor**.
1. From the **Resource Governor** context menu, select **Properties**.
1. Clear the **Enable Resource Governor** check box and select **OK**.

<a id="RGOffTSQL"></a>

## Disable resource governor using Transact-SQL

To disable resource governor using [!INCLUDE[tsql](../../includes/tsql-md.md)], execute the `ALTER RESOURCE GOVERNOR DISABLE` statement.

### Example

 The following example disables resource governor.

```sql
ALTER RESOURCE GOVERNOR DISABLE;
```

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
- [ALTER RESOURCE GOVERNOR](../../t-sql/statements/alter-resource-governor-transact-sql.md)
