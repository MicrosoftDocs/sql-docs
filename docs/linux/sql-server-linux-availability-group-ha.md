---
title: "Availability group deployment patterns - SQL Server on Linux"
description: Learn supported deployment configurations for SQL Server Always on availability groups on Linux servers.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# High availability and data protection for availability group configurations

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article presents supported deployment configurations for SQL Server Always On availability groups on Linux servers. An availability group supports high availability and data protection. Automatic failure detection, automatic failover, and transparent reconnection after failover provide high availability. Synchronized replicas provide data protection.

On a Windows Server Failover Cluster (WSFC), a common configuration for high availability uses two synchronous replicas and a third server or file share to provide quorum. The file-share witness validates the availability group configuration - status of synchronization, and the role of the replica, for example. This configuration ensures that the secondary replica chosen as the failover target has the latest data and availability group configuration changes.

The WSFC synchronizes configuration metadata for failover arbitration between the availability group replicas and the file-share witness. When an availability group isn't on a WSFC, the SQL Server instances store configuration metadata in the `master` database.

For example, an availability group on a Linux cluster has `CLUSTER_TYPE = EXTERNAL`. There's no WSFC to arbitrate failover. In this case, the configuration metadata is managed and maintained by the SQL Server instances. Because there's no witness server in this cluster, a third SQL Server instance is required to store configuration state metadata. All three SQL Server instances together provide distributed metadata storage for the cluster.

The cluster manager can query the instances of SQL Server in the availability group, and orchestrate failover to maintain high availability. In a Linux cluster, Pacemaker is the cluster manager.

[!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 1 enables high availability for an availability group with `CLUSTER_TYPE = EXTERNAL` for two synchronous replicas plus a configuration only replica. The configuration only replica can be hosted on any edition of [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 1 or later versions (including SQL Server Express edition). The configuration only replica maintains configuration information about the availability group in the `master` database but doesn't contain the user databases in the availability group.

## How the configuration affects default resource settings

[!INCLUDE [sssql17-md](../includes/sssql17-md.md)] introduces the `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` cluster resource setting. This setting guarantees the specified number of secondary replicas write the transaction data to log before the primary replica commits each transaction. When you use an external cluster manager, this setting affects both high availability and data protection. The default value for the setting depends on the architecture at the time the cluster resource is created. When you install the SQL Server resource agent - `mssql-server-ha` - and create a cluster resource for the availability group, the cluster manager detects the availability group configuration and sets `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` accordingly.

If supported by the configuration, the resource agent parameter `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is set to the value that provides high availability and data protection. For more information, see [Understand SQL Server resource agent for pacemaker](#pacemakerNotify).

The following sections explain the default behavior for the cluster resource.

Choose an availability group design to meet specific business requirements for high availability, data protection, and read-scale.

The following configurations describe the availability group design patterns and the capabilities of each pattern. These design patterns apply to availability groups with `CLUSTER_TYPE = EXTERNAL` for high availability solutions.

- **Three synchronous replicas**
- **Two synchronous replicas**
- **Two synchronous replicas and a configuration only replica**

## <a id="threeSynch"></a> Three synchronous replicas

This configuration consists of three synchronous replicas. By default, it provides high availability and data protection. It can also provide read-scale.

:::image type="content" source="media/sql-server-linux-availability-group-ha/3-three-replica.png" alt-text="Diagram showing three synchronous replicas.":::

An availability group with three synchronous replicas can provide read-scale, high availability, and data protection. The following table describes availability behavior.

| Availability behavior | read-scale | High availability &<br />data protection | Data protection |
| :--- | --- | --- | --- |
| `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=` | 0 | 1 <sup>1</sup> | 2 |
| Primary outage | Automatic failover. Might have data loss. New primary is R/W. | Automatic failover. New primary is R/W. | Automatic failover. New primary isn't available for user update transactions until former primary recovers and joins availability group as secondary. |
| One secondary replica outage | Primary is R/W. | Primary is R/W. | Primary isn't available for user update transactions until failed secondary recovers and joins availability group. |

<sup>1</sup> Default

## <a id="twoSynch"></a> Two synchronous replicas

This configuration enables data protection. Like the other availability group configurations, it can enable read-scale. The two synchronous replicas configuration doesn't provide automatic high availability. A two replica configuration is only applicable to [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] RTM and is no longer supported with higher (CU1 and beyond) versions of [!INCLUDE [sssql17-md](../includes/sssql17-md.md)].

:::image type="content" source="media/sql-server-linux-availability-group-ha/1-read-scale-out.png" alt-text="Diagram showing two synchronous replicas.":::

An availability group with two synchronous replicas provides read-scale and data protection. The following table describes availability behavior.

| Availability behavior | read-scale | Data protection |
| :--- | --- | --- |
| `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=` | 0 <sup>1</sup> | 1 |
| Primary outage | Automatic failover. Might have data loss. New primary is R/W. | Automatic failover. New primary isn't available for user update transactions until former primary recovers and joins availability group as secondary. |
| One secondary replica outage | Primary is R/W, running exposed to data loss. | Primary isn't available for user update transactions until secondary recovers. |

<sup>1</sup> Default

## <a id="configOnly"></a> Two synchronous replicas and a configuration only replica

An availability group with two (or more) synchronous replicas and a configuration only replica provides data protection and might also provide high availability. The following diagram represents this architecture:

:::image type="content" source="media/sql-server-linux-availability-group-ha/2-configuration-only.png" alt-text="Diagram showing a configuration-only availability group.":::

1. Synchronous replication of user data to the secondary replica. It also includes availability group configuration metadata.
1. Synchronous replication of availability group configuration metadata. It doesn't include user data.

In the availability group diagram, a primary replica pushes configuration data to both the secondary replica and the configuration only replica. The secondary replica also receives user data. The configuration only replica doesn't receive user data. The secondary replica is in synchronous availability mode. The configuration only replica doesn't contain the databases in the availability group - only metadata about the availability group. Configuration data on the configuration only replica is committed synchronously.

> [!NOTE]  
> An availability group with configuration only replica is new for [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 1. All instances of SQL Server in the availability group must be [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 1 or later versions.

The default value for `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is 0. The following table describes availability behavior.

| Availability behavior | High availability &<br />data protection | Data protection |
| :--- | --- | --- |
| `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=` | 0 <sup>1</sup> | 1 |
| Primary outage | Automatic failover. New primary is R/W. Might have data loss. | Automatic failover. New primary isn't available for user update transactions. |
| Secondary replica outage | Primary is R/W, running exposed to data loss (if primary fails and can't be recovered). No automatic failover if primary fails as well. | Primary isn't available for user update transactions. No replica to fail over to if primary fails as well. |
| Configuration only replica outage | Primary is R/W. No automatic failover if primary fails as well. | Primary is R/W. No automatic failover if primary fails as well. |
| Synchronous secondary + configuration only replica outage | Primary isn't available for user update transactions. No automatic failover. | Primary isn't available for user update transactions. No replica to fail over to if primary fails as well. |

<sup>1</sup> Default

> [!NOTE]  
> The instance of SQL Server that hosts the configuration only replica can also host other databases. It can also participate as a configuration only database for more than one availability group.

## Requirements

- All replicas in an availability group with a configuration only replica must be [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 1 or later versions.
- Any edition of SQL Server can host a configuration only replica, including SQL Server Express.
- The availability group needs at least one secondary replica - in addition to the primary replica.
- Configuration only replicas don't count toward the maximum number of replicas per instance of SQL Server. SQL Server standard edition allows up to three replicas, SQL Server Enterprise Edition allows up to 9.

## Considerations

- No more than one configuration only replica per availability group.
- A configuration only replica can't be a primary replica.
- You can't modify the availability mode of a configuration only replica. To change from a configuration only replica to a synchronous or asynchronous secondary replica, remove the configuration only replica, and add a secondary replica with the required availability mode.
- A configuration only replica is synchronous with the availability group metadata. There's no user data.
- An availability group with one primary replica and one configuration only replica, but no secondary replica isn't valid.
- You can't create an availability group on an instance of SQL Server Express edition.

[!INCLUDE [cluster-pacemaker-concepts](includes/cluster-pacemaker-concepts.md)]

## Related content

- [Availability groups for SQL Server on Linux](sql-server-linux-availability-group-overview.md)
