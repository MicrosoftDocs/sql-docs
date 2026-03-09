---
title: Configure SQL Server Availability Group with Custom High Availability Logic
description: How to configure a SQL Server Always On availability group using custom high availability and failover logic.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: atsingh, amitkh-msft
ms.date: 02/04/2026
ms.service: sql
ms.subservice: linux
ms.topic: design-pattern
ms.custom:
  - linux-related-content
---
# Configure SQL Server availability group with custom high availability logic on Linux

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This article explains how to configure a SQL Server Always On availability group (AG) on Linux using custom high availability and failover logic. This architecture uses `CLUSTER_TYPE = EXTERNAL` to allow manual or script-driven control over replica management. It's ideal for environments requiring tailored high availability and failover strategies. While data synchronization between replicas is handled by SQL Server, the high availability and failover mechanisms must be implemented externally.

## Types of availability group architectures

### High availability architecture

- Uses a *cluster manager* to provide automated failover and enhanced business continuity.

- Recommended for production environments requiring minimal downtime.

- To configure this setup, see [Configure SQL Server availability group for high availability on Linux](../../sql-server-linux-availability-group-configure-ha.md).

### Read-scale architecture

- Configured with `CLUSTER_TYPE = NONE`, this model **doesn't** use a cluster manager.

- Supports read-only workloads and can include replicas across different operating systems.

- Not suitable for high availability scenarios.

- For setup instructions, see [Configure a SQL Server availability group for read-scale on Linux](../../sql-server-linux-availability-group-configure-rs.md).

### Custom high availability and failover logic

- Offers flexibility to implement **custom failover logic** using external scripts.
- Configured with `CLUSTER_TYPE = EXTERNAL`, allowing manual or scripted management of availability replicas.
- Ideal for environments with specialized high availability requirements or nonstandard clustering solutions.

[!INCLUDE [Create prerequisites](../../includes/cluster-availability-group-create-prereq.md)]

## Create the availability group

Create the AG. Set `CLUSTER_TYPE = EXTERNAL`. In addition, set each replica with `FAILOVER_MODE = EXTERNAL`. Depending on the environment's requirements, set `AVAILABILITY_MODE` to either `SYNCHRONOUS_COMMIT` or `ASYNCHRONOUS_COMMIT.`

**Paxos protocol** plays a critical role in the internal communication and configuration consistency of Always On availability groups (AGs) in SQL Server, particularly in cluster-agnostic or external cluster configurations. Paxos maintains consistency of the AG configuration across replicas, prevent split-brain scenarios, and ensures only primary is responsible for configuration updates.

The following Transact-SQL script creates an AG named `ag1`. The script configures the AG replicas with `SEEDING_MODE = MANUAL`. This setting requires you to manually initialize secondary replicas with a copy of the database before adding them to the AG. Update the following script for your environment. Replace the `<node1>`, `<node2>`, and `<node3>` values with the names of the SQL Server instances that host the replicas. This AG also configures the configuration only replica `<node3>`. The configuration only replica maintains configuration information about the availability group in the `master` database but doesn't contain the user databases in the availability group. Replace the `<5022>` value with the port you set for the endpoint. Run the following Transact-SQL script on the primary SQL Server replica:

```sql
CREATE availability group [ag1]
    WITH (CLUSTER_TYPE = EXTERNAL)
    FOR REPLICA ON
        N'<node1>' WITH (
            ENDPOINT_URL = N'tcp://<node1>:<5022>',
            AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
            FAILOVER_MODE = EXTERNAL,
            SEEDING_MODE = MANUAL,
                    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
        ),
        N'<node2>' WITH (
            ENDPOINT_URL = N'tcp://<node2>:<5022>',
            AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
            FAILOVER_MODE = EXTERNAL,
            SEEDING_MODE = MANUAL,
            SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)

        N'<node3>' WITH (
            ENDPOINT_URL = N'tcp://<node3>:<5022>',
            AVAILABILITY_MODE = CONFIGURATION_ONLY
        );

ALTER availability group [ag1] GRANT CREATE ANY DATABASE;
```

### Join secondary SQL Server instances to the AG

The following Transact-SQL script joins a server to an AG named `ag1`. Update the script for your environment. On each secondary SQL Server replica, run the following Transact-SQL script to join the AG:

```sql
ALTER availability group [ag1] JOIN WITH (CLUSTER_TYPE = EXTERNAL);

ALTER availability group [ag1] GRANT CREATE ANY DATABASE;
```

[!INCLUDE [Create post](../../includes/cluster-availability-group-create-post.md)]

## Fail over the primary replica in a custom high availability and failover logic

Each availability group has only one primary replica. The primary replica allows reads and writes. To change which replica is primary, you can fail over. In a typical availability group, the cluster manager automates the failover process. In an availability group with cluster type `EXTERNAL`, with **custom failover logic** using external scripts or third-party tools the failover process can be manual or automated by implementing the custom health check and failover logic.

Manual fail over the primary replica can be done in two ways:

- Manual failover without data loss

- Forced manual failover with data loss

### Manual failover without data loss

Use this method when the primary replica is available, but you need to temporarily or permanently change which instance hosts the primary replica. To avoid potential data loss, before you issue the manual failover, ensure that the target secondary replica is up to date.

To manually fail over without data loss:

1. Make the current primary and target secondary replica `SYNCHRONOUS_COMMIT`.

   ```sql
   ALTER AVAILABILITY GROUP [ag1] MODIFY REPLICA ON N'<node2>' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
   ```

1. To identify that active transactions are committed to the primary replica and at least one synchronous secondary replica, run the following query:

   ```sql
   SELECT AG.NAME,
          DRS.DATABASE_ID,
          DRS.GROUP_ID,
          DRS.REPLICA_ID,
          DRS.SYNCHRONIZATION_STATE_DESC,
          AG.SEQUENCE_NUMBER
   FROM SYS.DM_HADR_DATABASE_REPLICA_STATES AS DRS, SYS.AVAILABILITY_GROUPS AS AG
   WHERE DRS.GROUP_ID = AG.GROUP_ID;
   ```

   The secondary replica is synchronized when `synchronization_state_desc` is `SYNCHRONIZED`.

1. Set the primary replica and the secondary replicas not participating in the failover offline to prepare for the role change:

   ```sql
   ALTER AVAILABILITY GROUP [ag1] OFFLINE;
   ```

1. Promote the target secondary replica to primary

   ```sql
   ALTER AVAILABILITY GROUP ag1 FORCE_FAILOVER_ALLOW_DATA_LOSS;
   ```

1. Update the role of the old primary and other secondaries to `SECONDARY`, then run the following command on the SQL Server instance that hosts the old primary replica:

   ```sql
   ALTER availability group [ag1] SET (ROLE = SECONDARY);
   ```

1. Resume data movement, run the following command for every database in the availability group on the SQL Server instance that hosts the primary replica:

   ```sql
   ALTER DATABASE [db1]
       SET HADR RESUME;
   ```

### Forced manual failover with data loss

If the primary replica isn't available and can't immediately be recovered, then you need to force a failover to the secondary replica with data loss. However, if the original primary replica recovers after failover, it will assume the primary role. To avoid having each replica be in a different state, remove the original primary from the availability group after a forced failover with data loss. Once the original primary comes back online, remove the availability group from it entirely.

To force a manual failover with data loss from primary replica N1 to secondary replica N2, follow these steps:

1. On the secondary replica (N2), initiate the forced failover:

   ```sql
   ALTER AVAILABILITY GROUP [ag1] FORCE_FAILOVER_ALLOW_DATA_LOSS;
   ```

1. On the new primary replica (N2), remove the original primary (N1):

   ```sql
   ALTER AVAILABILITY GROUP [ag1] REMOVE REPLICA ON N'N1';
   ```

1. Validate that all application traffic is pointed to the listener and/or the new primary replica.

   ```sql
   ALTER AVAILABILITY GROUP [ag1] OFFLINE;
   ```

1. If there's data, or unsynchronized changes, preserve this data via backups or other data replicating options that suit your business needs.

1. Next, remove the availability group from the original primary (N1):

   ```sql
   DROP AVAILABILITY GROUP [ag1];
   ```

1. Drop the availability group database on original primary replica (N1)

   ```sql
   USE [master];
   GO

   DROP DATABASE [db1];
   GO
   ```

1. (Optional) If desired, you can now add N1 back as a new secondary replica to the availability group AG1.

## Related content

- [Distributed availability groups](../../../database-engine/availability-groups/windows/distributed-availability-groups.md)
- [What is an Always On availability group?](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Perform a forced manual failover of an Always On availability group (SQL Server)](../../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)
