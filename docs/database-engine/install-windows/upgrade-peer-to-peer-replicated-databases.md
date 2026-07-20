---
title: Upgrade or Patch Peer-to-Peer Replicated Databases
description: SQL Server supports upgrading Peer-to-Peer replicated databases from previous versions of SQL Server without stopping activity on other peers.
author: asavioliMSFT
ms.author: akbarf
ms.reviewer: jopilov, mathoma
ms.date: 12/12/2025
ms.service: sql
ms.subservice: install
ms.topic: upgrade-and-migration-article
helpviewer_keywords:
  - "peer-to-peer replication database upgrades [SQL Server replication]"
  - "replication [SQL Server], upgrading upgrade-peer-to-peer-replication.md"
  - "transactional replication, upgrading databases"
  - "p2p replication [SQL Server], upgrading databases"
  - "upgrading replicated databases"
monikerRange: ">=sql-server-2017"
---

# Upgrade or patch peer-to-peer replicated databases

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article provides guidance on how to upgrade or patch SQL Server instances that participate in [peer-to-peer (P2P) replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md), both outside of an Always On availability group and for databases that are within an Always On availability group.

If your replication topology includes other types of replication, such as snapshot, merge or transactional replication, see [Upgrade or patch replicated databases](upgrade-replicated-databases.md) for more information.

## Upgrade P2P databases outside of an availability group

The steps in this section provide guidance on how to upgrade or patch SQL Server instances that are participating in peer-to-peer (P2P) replication outside of an Always On availability group.

The following table describes the roles and names of the servers that participate in the replication topology used in the example:

| Name | Role |
| --- | --- |
| **Peer1** | The SQL Server instance that hosts the replication databases for the first peer in the peer-to-peer topology. |
| **Peer2** | The SQL Server instance that hosts the replication databases for the second peer in the peer-to-peer topology. |
| **Dist1** | The remote distributor for Peer1. |
| **Dist2** | The remote distributor for Peer2. |

When patching or upgrading peer-to-peer replicated databases outside of an availability group, follow these steps:
1. Stop incoming traffic to **Peer2** by stopping all applications and distribution agents from any other peers that replicate to this instance. For example, stop the distribution agent on **Peer1**.
1. Confirm there are no pending replicated transactions coming to **Peer2** by checking the log reader agents and distribution agents associated with this replication topology.
1. If the distributor is remote, first upgrade the remote distributor **Dist2**. If the distributor is the local **Peer2** instance, skip to the next step.
1. Upgrade the instance **Peer2**.
1. Start the distribution agent from **Peer1** (or any remaining peers replicating to this peer).
1. Stop incoming traffic to **Peer1** by stopping all applications and distribution agents from any other peers that replicate to this instance. For example, stop the distribution agent on **Peer2**.
1. Confirm there are no pending replicated transactions coming to **Peer1** by checking the log reader agents and distribution agents associated with this replication topology.
1. If the distributor is remote, first upgrade the remote distributor **Dist1**. If the distributor is the local **Peer1** instance, skip to the next step.
1. Upgrade the instance **Peer1**.
1. Start the distribution agent from **Peer2** (or any remaining peers replicating to this peer).
1. If there are any other peers in the topology, repeat these same steps for each peer.

## Upgrade P2P databases in an availability group

The steps in this section provide guidance on how to upgrade or patch SQL Server instances that are participating in peer-to-peer (P2P) replication within an Always On availability group.

The following table describes the roles and names of the servers that participate in the replication topology used in the example:

| Name | Role |
| --- | --- |
| **Peer1N1** | The SQL Server instance that hosts the primary replica, and is **Peer1** in the peer-to-peer topology. |
| **Peer1N2** | The SQL Server instance that hosts the secondary replica associated with **Peer1** in the peer-to-peer topology. |
| **Dist1** | The remote distributor for **Peer1**. |
| **Peer2N3** | The SQL Server instance that hosts the primary replica, and is **Peer2** in the peer-to-peer topology. |
| **Peer2N4** | The SQL Server instance that hosts the secondary replica associated with **Peer2** in the peer-to-peer topology. |
| **Dist2** | The remote distributor for **Peer2**. |

> [!NOTE]  
> Using a local distributor for a P2P database that is part of an availability group isn't a recommended configuration, as it's a single point of failure.

When patching or upgrading peer-to-peer replicated databases within an availability group, follow these steps:
1. Stop incoming traffic to **Peer2** by stopping all applications and distribution agents from any other peers that replicate to this instance. For example, stop the distribution agent on **Peer1**.
1. Confirm there are no pending replicated transactions coming to **Peer2** by checking the log reader agents and distribution agents associated with this replication topology.
1. Upgrade the remote distributor for **Peer2**, **Dist2**, by following the sequence in [Upgrade availability group replicas](../availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md). If your distributor is local, skip to the next step.
1. Upgrade the secondary replica **Peer2N4**.
1. Perform a failover of the availability group from the current primary replica **Peer2N3** to the upgraded secondary **Peer2N4**.
1. Upgrade the former primary replica **Peer2N3**.
1. Perform a failover of the availability group from the current primary **Peer2N4** to the previous primary replica **Peer2N3**.
1. Start the distribution agent from **Peer1** (or any remaining peers replicating to this peer).
1. Stop incoming traffic to **Peer1** by stopping all applications and distribution agents from any other peers that replicate to this instance. For example, stop the distribution agent on **Peer2**.
1. Confirm there are no pending replicated transactions coming to **Peer1** by checking the log reader agents and distribution agents associated with this replication topology.
1. Upgrade the remote distributor for **Peer1**, **Dist1**, by following the sequence in [Upgrade availability group replicas](../availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md). If your distributor is local, skip to the next step.
1. Upgrade the secondary replica **Peer1N2**.
1. Perform a failover of the availability group from the current primary replica **Peer1N1** to the upgraded secondary **Peer1N2**.
1. Upgrade the former primary replica **Peer1N1**.
1. Perform a failover of the availability group from the current primary **Peer1N2** to the previous primary replica **Peer1N1**.
1. Start the distribution agent from **Peer2** (or any remaining peers replicating to this peer).
1. If there are any other peers in the topology, repeat these same steps for each peer.

## Related content

- [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md)
- [Upgrade or patch replicated databases](upgrade-replicated-databases.md)
- [Replication Administration FAQ](../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.yml)
- [Replication backward compatibility](../../relational-databases/replication/replication-backward-compatibility.md)
- [Supported version and edition upgrades (SQL Server 2022)](supported-version-and-edition-upgrades-2022.md)
- [Upgrade SQL Server](upgrade-sql-server.md)
- [Upgrade availability group replicas](../availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md)
