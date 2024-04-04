---
title: "Use read-scale with availability groups"
description: "Learn details about how to achieve read-scale when using Always On availability groups, and about using distributed availability groups for geographic read-scale."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 10/17/2023
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
---
# Use read-scale with Always On availability groups

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

An availability group is a comprehensive solution that brings high-availability capabilities to SQL Server and offers integrated scaling solutions as well. In a typical database application, multiple clients run various types of workloads. Sometimes bottlenecks can develop due to resource constraints.

In the context of an availability group, read-scale is offloading read workloads to one or more secondary replicas. You can free up resources and achieve higher throughput for the OLTP workload. You also can deliver higher performance and scale on read-only workloads. Take advantage of the fastest replication technology for SQL Server and create a group of replicated databases to offload reporting and analytics workloads to read-only replicas.

With availability groups, one or more secondary replicas can be configured to support read-only access to secondary databases.

The client applications that run analytics or reporting workloads can directly connect to secondary databases. You also can set up a read-only routing list and connect to the primary database. It then forwards the connection request to each of the secondary replicas from the routing list in a round-robin fashion.

## Read-scale availability groups without cluster

In [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] and earlier versions, all availability groups required a cluster. The cluster provided business continuity for high availability and disaster recovery (HADR). In addition, secondary replicas were configured for read operations. If high availability wasn't the goal, considerable operational overhead was expended to configure and operate a cluster. [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)] introduces read-scale availability groups without a cluster.

> [!NOTE]  
> In availability groups that don't utilize Windows Server Failover Clustering (WSFC), such as read-scale availability groups, or [availability groups on Linux](../../../linux/sql-server-linux-availability-group-overview.md), columns in the [availability groups DMVs](../../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md) related to the cluster might display data about an internal default cluster. These columns are for internal use only and can be disregarded.

If your business requirement is to conserve resources for mission-critical workloads that run on the primary replica, you can use read-only routing or directly connect to readable secondary replicas. You don't need to depend on integration with any clustering technology. These new capabilities are available for [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)] running on both Windows and Linux platforms.

> [!IMPORTANT]  
> This isn't a high-availability setup. There's no infrastructure to monitor and coordinate failure detection and automatic failover. Without a cluster, SQL Server can't provide the low recovery time objective (RTO) that an automated high-availability solution provides. If you need high-availability capabilities, use a cluster manager (Windows Server Failover Cluster on Windows or Pacemaker on Linux).
>  
> The read-scale availability group can provide disaster recovery capability. When the read-only replicas are in synchronous-commit mode, they provide a recovery point objective (RPO) of zero. To fail over a read-scale availability group, see [Fail over the primary replica on a read-scale availability group](perform-a-planned-manual-failover-of-an-availability-group-sql-server.md#ReadScaleOutOnly).

## Use distributed availability groups for geographic read-scale

Geographically separated solutions can implement read-scale solutions with distributed availability groups. You can use them to offload read workloads from the primary replica to readable secondary replicas to sites that are closer to the source of the read workloads. Distributed availability groups reduce the utilization of resources on the primary replica. They also help with read throughput by reducing network latency and taking advantage of dedicated resources.

A single distributed availability group can have up to 17 readable secondary replicas. For increased scaling capacity, daisy-chain multiple availability groups to increase the number of readable replicas even further. You also can deploy two distributed availability groups from the same availability group for low latency reads in geographically dispersed environments.

## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Configure a SQL Server Availability Group for read-scale on Linux](../../../linux/sql-server-linux-availability-group-configure-rs.md)
- [Configure read-scale for an Always On availability group](configure-read-scale-availability-groups.md)
