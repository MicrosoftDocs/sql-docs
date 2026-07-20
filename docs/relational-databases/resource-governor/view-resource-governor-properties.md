---
title: View and Modify Resource Governor Properties
description: Learn how to create and configure resource governor entities by using the resource governor properties page in SQL Server Management Studio.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
f1_keywords:
  - "sql13.swb.rg.properties.f1"
helpviewer_keywords:
  - "Resource Governor, properties"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# View and modify resource governor properties

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can create and configure resource governor resource pools and workload groups by using the resource governor properties page in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

## Reconfigure resource governor

When you select **OK** after adding, deleting, or moving a workload group or resource pool, the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement is executed.

If the create or reconfigure operation for the resource pool or workload group fails, a summary error message appears below the title of the property page. To see a detailed error message, select the down arrow on the error message.

You can determine whether there is a pending configuration change by checking the value of the `is_configuration_pending` column in the [sys.dm_resource_governor_configuration](../system-dynamic-management-views/sys-dm-resource-governor-configuration-transact-sql.md) dynamic management view.

<a id="Permissions"></a>

## Permissions

Viewing resource governor properties requires the `VIEW SERVER STATE` permission, or the `VIEW SERVER PERFORMANCE STATE` permission in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later. The resource governor configuration tasks require the `CONTROL SERVER` permission.

<a id="ViewRGProp"></a>

## Resource governor properties page

To view resource governor properties by using the resource governor properties page in [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms):

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open Object Explorer and expand the **Management** node down to **Resource Governor**.
1. Use the **Resource Governor** context menu and select **Properties**.
1. For descriptions of resource pool and workload group properties, see [resource governor properties](#RGProp).
1. To save any changes, select **OK**.

<a id="RGProp"></a>

## Resource governor properties

| Property | Description |
|:--|:--|
| Classifier function name | Specify the classifier function by selecting from the list. To create a classifier function and for more information, see [Resource governor classifier function](resource-governor-classifier-function.md). |
| Enable resource governor | Enable or disable resource governor by selecting or clearing the check box. |

### Resource pool properties

Create or modify resource pool and external resource pool configuration by using the grids. Each grid displays the current configuration of the existing resource pools, including the built-in `internal` and `default` pools. Select a pool to work with by selecting a row. When the **Enable Resource Governor** check box is selected, you can create a new resource pool by selecting the row marked with an asterisk (`*`).

| Property | Description |
|:--|:--|
| **Name** | Specify the name of the resource pool. |
| **Minimum CPU %** | Specify the guaranteed average CPU bandwidth for requests in the resource pool when there is CPU contention. Range is 0 to 100. The default setting is 0. |
| **Maximum CPU %** | Specify the maximum average CPU bandwidth that requests in this resource pool receive when there is CPU contention. Range is 0 to 100. The default setting is 100. |
| **Minimum Memory %** | Specify the minimum amount of memory reserved for requests in this resource pool that can not be shared with other resource pools. Range is 0 to 100. The default setting is 0. |
| **Maximum Memory %** | Specify the total server memory that requests in this resource pool can use. Range is 0 to 100. The default setting is 100. |

For more information, see [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md) and [CREATE EXTERNAL RESOURCE POOL](../../t-sql/statements/create-external-resource-pool-transact-sql.md).

### Workload group properties

Create or modify the workload group configuration by using the grid. The grid displays the current configuration of the existing workload groups, including the built-in `internal` and `default` groups. Select a group to work with by selecting a row. When the **Enable Resource Governor** check box is selected, and a resource pool other than `internal` is selected, you can create a new workload group in that resource pool by selecting the row marked with an asterisk (`*`).

| Property | Description |
|:--|:--|
| **Name** | Specify the name of the workload group. |
| **Importance** | Specify the relative importance for requests in the workload group. Available settings are `Low`, `Medium`, and `High`. |
| **Maximum Requests** | Specify the maximum number of concurrent requests that are allowed to execute in the workload group. Must be 0 (not limited by resource governor) or a positive integer. |
| **CPU Time (sec)** | Specify the maximum amount of CPU time that a request can use. Must be 0 (not limited by resource governor) or a positive integer. |
| **Memory Grant %** | Specify the maximum amount of query grant memory that a single request can take from the pool. Range is 0 to 100. |
| **Grant Time-out (sec)** | Specify the maximum time that a query can wait for a memory grant to become available before the query fails. Must be 0 (not limited by resource governor) or a positive integer. |
| **Degree of Parallelism** | Specify the maximum degree of parallelism (DOP) for requests using intra-query parallelism. Range is 0 (not limited by resource governor) to 64. |

For more information, see [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md).

## View resource governor properties using Transact-SQL

1. To view the persisted resource governor configuration, use [resource governor catalog views](../system-catalog-views/resource-governor-catalog-views-transact-sql.md). If the persisted resource governor configuration is modified, it doesn't become effective until the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement is executed.
1. To view the currently effective runtime resource governor configuration and statistics, use [resource governor dynamic management views](../system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md).

## Related tasks

In addition to viewing and modifying resource governor properties, you can use the **Resource Governor Properties** page to perform several configuration tasks. For more information, see:

- [Enable resource governor](enable-resource-governor.md)
- [Disable resource governor](disable-resource-governor.md)
- [Create a resource pool](create-a-resource-pool.md)
- [Create a workload group](create-a-workload-group.md)
- [Change resource pool settings](change-resource-pool-settings.md)
- [Change workload group settings](change-workload-group-settings.md)
- [Move a workload group](move-a-workload-group.md)

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
