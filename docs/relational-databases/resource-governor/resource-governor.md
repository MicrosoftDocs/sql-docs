---
title: Resource Governor
description: Learn about the SQL Server resource governor feature that limits the amount of CPU, physical I/O, and memory that query workloads can use.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman, randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Resource Governor, overview"
  - "Resource Governor"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Resource governor

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

You can use resource governor to manage [!INCLUDE [ssde-md](../../includes/ssde-md.md)] resource consumption and enforce policies for user workloads. Resource governor lets you reserve or limit the amount of CPU, memory, and physical I/O that user query workloads can use. You can also modify resource consumption behavior of each query, such as the degree of parallelism or the size of a memory grant.

Starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], you can:

- Use resource governor in the Enterprise, Enterprise Developer, Standard, and Standard Developer editions. In the previous versions, resource governor is only available in the Enterprise and Developer editions. For more information, see [What's new in SQL Server 2025](../../sql-server/what-s-new-in-sql-server-2025.md).
- Use resource governor to enforce limits on the total amount of `tempdb` space consumed by an application or user workload. For more information, see [Tempdb space resource governance](tempdb-space-resource-governance.md).

For configuration and monitoring examples and to learn resource governor best practices, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

> [!NOTE]  
> While [Azure SQL Database leverages resource governor](https://azure.microsoft.com/blog/resource-governance-in-azure-sql-database/) (among other techniques) to manage resources, user configuration of resource pools and workload groups in Azure SQL Database isn't supported.
>
> Azure Synapse Analytics has a different implementation of a similar resource governance behavior via the [Workload classification feature](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-workload-classification).

## Benefits of resource governor

Resource governor enables you to manage [!INCLUDE [ssde-md](../../includes/ssde-md.md)] workloads and resources by specifying reservations and limits on resource consumption by requests. In the resource governor context, a workload is a set of queries (requests) that can, and should be, treated as a single entity. For example, all queries executed by a certain application might be considered a workload. While this isn't a requirement, the more uniform the resource usage pattern of a workload is, the more benefit you're likely to derive from resource governor.

If multiple distinct workloads are present on the same server, resource governor enables you to allocate resources differently to different workloads, based on the limits that you specify.

Some of the usage scenarios supported by resource governor are:

- Provide multitenancy and resource isolation on single instances of SQL Server that serve multiple client workloads. That is, you can divide the available resources on a server among the workloads and minimize the problems that can occur when workloads compete for resources.
- Provide predictable performance and support SLAs for workloads in a multi-workload and multi-user environment.
- Isolate and limit runaway queries, or limit I/O resources for I/O intensive operations that can saturate the I/O subsystem and negatively impact other workloads.
- Add fine-grained resource tracking for resource usage chargebacks and provide predictable billing to the consumers of server resources.

## Interoperability and limitations

- Resource governor can be used with Always On [availability groups](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) and [failover cluster instances](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md). The following considerations apply:
  - When used in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], resource governor must be configured on each [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts an availability group. Resource governor configuration doesn't propagate from the primary availability group replica to secondary replicas. We recommend that you use the same resource governor configuration for all [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] instances hosting availability replicas. This ensures consistent behavior as availability group failovers occur.
  - When used in [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)], resource governor configuration propagates from the primary replica to all secondary replicas because the `master` database of the primary replica is replicated to all secondary replicas. This includes high availability and geo-replication secondaries. For more information, see [Resource governor](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#resource-governor).
  - If you use contained availability groups, see [Interactions with other features](../../database-engine/availability-groups/windows/contained-availability-groups-overview.md#resource-governor) for more information.
- Resource management is limited to the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. Resource governor can't be used for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)], and [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)].
- Resource governor doesn't provide workload monitoring or workload management across multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances.
- Very short queries, such as queries in some OLTP workloads, might not use CPU long enough to apply CPU bandwidth controls. This might skew CPU usage statistics and limit the effectiveness of CPU resource governance.
- The ability to govern physical I/O applies only to user operations and not system tasks. System tasks perform transaction log, checkpoint, and lazy writer I/O. Resource governor governs user physical reads I/O but not write I/O performed by system tasks.
- You can't modify resource governance controls for the `internal` resource pool and workload group.

## Resource concepts

The following three concepts are fundamental to understanding and using resource governor:

- **Resource pool**. A resource pool represents a container for the physical resources of the server, such as CPU, memory, and I/O. Two built-in resource pools, `internal` and `default`, are always present. Resource governor also supports user-defined resource pools. Depending on configuration, resources in a resource pool can be shared with other pools or reserved. For more information, see [Resource governor resource pool](resource-governor-resource-pool.md).
- **Workload group**. A workload group represents a container for sessions that are classified in the same way. A workload group allows for aggregate monitoring of session and request resource consumption, and defines request policies. Each workload group is in a resource pool. Two built-in workload groups, `internal` and `default`, always exist and are mapped to the `internal` and `default` resource pools respectively. Resource governor also supports user-defined workload groups. For more information, see [Resource governor workload group](resource-governor-workload-group.md).
- **Classification**. The classification process assigns incoming sessions to a workload group based on the attributes of the session such as login name or program name, using your custom classification logic. Once a session is classified into a workload group, all requests executing on that session are subject to the workload group policies. You define the classification logic by writing a scalar user-defined function, called a classifier function. For more information, see [Resource governor classifier function](resource-governor-classifier-function.md).

> [!NOTE]  
> Resource governor doesn't impose any controls on a [dedicated administrator connection (DAC)](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md). DAC queries always run in the `internal` workload group and resource pool.

The following illustration shows resource governor components and their relationship with each other within the [!INCLUDE [ssde-md](../../includes/ssde-md.md)]. From a processing perspective, the simplified flow is as follows:

- There's an incoming connection for a session (session 1 of `n`).
- The session is classified.
- Using the classification outcome, the session is assigned to a workload group, for example, `Group 4`.
- The workload group enforces its policies on all requests, and uses the resource pool it's associated with, for example, `Pool 2`.
- The resource pool provides and limits the resources required by the application, for example, `Application 3`.

:::image type="content" source="media/resource-governor/resource-governor-components.png" alt-text="Diagram showing resource governor components and the processing of incoming sessions.":::

## Resource governor tasks

| Task description | Article |
| --- | --- |
| View configuration examples | [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md) |
| Enable resource governor | [Enable resource governor](enable-resource-governor.md) |
| Disable resource governor | [Disable resource governor](disable-resource-governor.md) |
| Create, alter, and drop a resource pool | [Resource governor resource pool](resource-governor-resource-pool.md) |
| Create, alter, move, and drop a workload group | [Resource governor workload group](resource-governor-workload-group.md) |
| Create and test a classifier user-defined function | [Resource governor classifier function](resource-governor-classifier-function.md) |
| Configure resource governor using a template | [Configure resource governor using a template](configure-resource-governor-using-a-template.md) |
| View resource governor properties | [View and modify resource governor properties](view-resource-governor-properties.md) |
| Set a limit on `tempdb` space consumption | [Tempdb space resource governance](tempdb-space-resource-governance.md) |

## Related content

- [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md)
- [Enable resource governor](enable-resource-governor.md)
- [Disable resource governor](disable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
