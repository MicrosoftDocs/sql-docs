---
title: "Use read-scale with availability groups"
description: "A description of how to achieve read-scale when using Always On availability groups. "
ms.custom: "seodec18"
ms.date: "10/24/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid:
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Use read-scale with Always On availability groups
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

An availability group is a comprehensive solution that brings high-availability capabilities to SQL Server and offers integrated scaling solutions as well. In a typical database application, multiple clients run various types of workloads. Sometimes bottlenecks can develop due to resource constraints. You can free up resources and achieve higher throughput for the OLTP workload. You also can deliver higher performance and scale on read-only workloads. Take advantage of the fastest replication technology for SQL Server and create a group of replicated databases to offload reporting and analytics workloads to read-only replicas.

With availability groups, one or more secondary replicas can be configured to support read-only access to secondary databases.

The client applications that run analytics or reporting workloads can directly connect to secondary databases. You also can set up a read-only routing list and connect to the primary database. It then forwards the connection request to each of the secondary replicas from the routing list in a round-robin fashion.

## Read-scale availability groups without cluster

In [!INCLUDE[sssql15-md](../../../includes/sssql15-md.md)] and earlier, all availability groups required a cluster. The cluster provided business continuity for high availability and disaster recovery (HADR). In addition, secondary replicas were configured for read operations. If high availability wasn't the goal, considerable operational overhead was expended to configure and operate a cluster. SQL Server 2017 introduces read-scale availability groups without a cluster. 

If your business requirement is to conserve resources for mission-critical workloads that run on the primary replica, you can use read-only routing or directly connect to readable secondary replicas. You don't need to depend on integration with any clustering technology. These new capabilities are available for SQL Server 2017 running on both Windows and Linux platforms.

>[!IMPORTANT]
>This is not a high-availability setup. There is no infrastructure to monitor and coordinate failure detection and automatic failover. Without a cluster, SQL Server can't provide the low recovery time objective (RTO) that an automated high-availability solution provides. If you need high-availability capabilities, use a cluster manager (Windows Server failover clustering on Windows or Pacemaker on Linux).
>
>The read-scale availability group can provide disaster recovery capability. When the read-only replicas are in synchronous-commit mode, they provide a recovery point objective (RPO) of zero. To fail over a read-scale availability group, see [Fail over the primary replica on a read-scale availability group](perform-a-planned-manual-failover-of-an-availability-group-sql-server.md#ReadScaleOutOnly).

## Use distributed availability groups for geographic read-scale

Geographically separated solutions can implement read-scale solutions with distributed availability groups. You can use them to offload read workloads from the primary replica to readable secondary replicas to sites that are closer to the source of the read workloads. Distributed availability groups reduce the utilization of resources on the primary replica. They also help with read throughput by reducing network latency and taking advantage of dedicated resources.

A single distributed availability group can have up to 17 readable secondary replicas. For increased scaling capacity, daisy-chain multiple availability groups to increase the number of readable replicas even further. You also can deploy two distributed availability groups from the same availability group for low latency reads in geographically dispersed environments.




## Next steps

[Configure a read-scale availability group on Linux](../../../linux/sql-server-linux-availability-group-configure-rs.md)

[Configure a read-scale availability group on Windows](../../../database-engine/availability-groups/windows/configure-read-scale-availability-groups.md)

## See also

 [Overview of AlwaysOn availability groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
