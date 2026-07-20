---
title: Enable Resource Governor
description: Learn how to enable resource governor using either SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 04/09/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "Resource Governor, enabling"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Enable resource governor

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Resource governor is turned off by default. You can enable resource governor using either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

For configuration and monitoring examples and to learn resource governor best practices, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

Enabling resource governor has the following results:

- The classifier function is executed for new connections so that workloads can be assigned to workload groups.
- The resource limits that are specified in resource governor configuration are honored and enforced.

<a id="LimitationsRestrictions"></a>

### Limitations

You can't use the `ALTER RESOURCE GOVERNOR` statement as part of a user transaction.

<a id="Permissions"></a>

### Permissions

Enabling resource governor requires the `CONTROL SERVER` permission.

<a id="RGOnObjEx"></a>

## Enable resource governor using Object Explorer in SQL Server Management Studio

To enable resource governor using Object Explorer in [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to **Resource Governor**.
1. From the **Resource Governor** context menu, select **Enable**.

<a id="RGOnProp"></a>

## Enable resource governor using resource governor properties

To enable resource governor by using the resource governor properties page:

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to **Resource Governor**.
1. From the **Resource Governor** context menu, select **Properties**.
1. Select the **Enable Resource Governor** check box and select **OK**.

<a id="RGOnTSQL"></a>

## Enable resource governor using Transact-SQL

To enable resource governor using [!INCLUDE[tsql](../../includes/tsql-md.md)], execute the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement.

### Example

The following example enables resource governor and makes all prior resource governor configuration changes effective.

```sql
ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
- [ALTER RESOURCE GOVERNOR](../../t-sql/statements/alter-resource-governor-transact-sql.md)
