---
title: Configure read scale-out availability group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 06/14/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 
---
# Configure read scale-out availability group for SQL Server on Linux

[!INCLUDE[tsql-appliesto-sslinx-only_md](../../docs/includes/tsql-appliesto-sslinx-only_md.md)]

You can configure a read scale-out availability group for SQL Server on Linux. There are two architectures for availability groups. A *high availability* architecture uses a cluster manager to provide improved business continuity. This architecture can also include read scale-out replicas. To create the high availability architecture, see [Configure Always On availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md).

This document explains how to create a *read scale-out* availability group without a cluster manager. This architecture only provides read scale-out only. It does not provide high availability.

[!INCLUDE [Create prerequisites](../includes/ss-linux-cluster-availability-group-create-prereq.md)]

## Create the availability group

Create the availability group. Set `CLUSTER_TYPE = NONE`. In addition, set each replica with `FAILOVER_MODE = NONE`. Client applications running analytics or reporting workloads can directly connect to the secondary databases. You can also create a read-only routing list. Connections to the primary replica forward read connection requests to each of the secondary replicas from the routing list in a round robin fashion.

The following Transact-SQL script creates an availability group name `ag1`. The script configures the availability group replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server after it is added to the availability group. Update the following script for your environment. Replace the  `**<node1>**` and `**<node2>**` values with the names of the SQL Server instances that host the replicas. Replace the `**<5022>**` with the port you set for the endpoint. Run the following Transact-SQL on the primary SQL Server replica:

```Transact-SQL
CREATE AVAILABILITY GROUP [ag1]
    WITH (CLUSTER_TYPE = NONE)
    FOR REPLICA ON
        N'**<node1>**' WITH (
            ENDPOINT_URL = N'tcp://**<node1>:**<5022>**',
		    AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = MANUAL,
		    SEEDING_MODE = AUTOMATIC,
                    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    ),
        N'**<node2>**' WITH ( 
		    ENDPOINT_URL = N'tcp://**<node2>**:**<5022>**', 
		    AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = MANUAL,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    );
		
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

### Join secondary SQL Servers to the availability group

The following Transact-SQL script joins a server to an availability group named `ag1`. Update the script for your environment. On each secondary SQL Server replica, run the following Transact-SQL to join the availability group.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = NONE);
		 
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

[!INCLUDE [Create Post](../includes/ss-linux-cluster-availability-group-create-post.md)]

This is not an high availability configuration, if you need high availability, follow the instructions at [Configure Always On Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md). Specifically, create the availability group with `CLUSTER_TYPE=WSFC` (in Windows) or `CLUSTER_TYPE=EXTERNAL` (in Linux) and integrate with a cluster manager - either WSFC on Windows or Pacemaker on Linux.

## Connect to read-only secondary replicas

There are two ways to connect to the read-only secondary replicas. Applications can connect directly to the SQL Server instance that hosts the secondary replica and query the databases, or they can use read-only routing. read-only routing requires a listener.

[Readable secondary replicas](../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)

[read-only routing](../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md#ConnectToSecondary)

## Fail over primary replica on read scale-out availability group

Each availability group has only one primary replica. The primary replica allows reads and writes. To change which replica is the primary, you can fail over. In an availability group for high availability, the cluster manager automates in the failover process. In a read scale-out availability group, the failover process is manual. There are two ways to fail over the primary replica in a read scale availability group.

- Forced manual fail over with data loss

- Manual fail over without data loss

### Forced fail over with data loss

Use this method when the primary replica is not available and can not be recovered. You can find more information about forced failover with data loss at [Perform a Forced Manual Failover](../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md).

To force fail over with data loss, connect to the SQL instance hosting the target secondary replica and run:
```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] FORCE_FAILOVER_ALLOW_DATA_LOSS;
```

### Manual fail over without data loss

Use this method when the primary replica is available, but you need to temporarily or permanently change the configuration and change the SQL Server instance that hosts the primary replica. Before issuing manual failing over, ensure that the target secondary replica is up to date, so that there is no potential data loss. 

The following steps describe how to manually fail over without data loss:

1. Make the target secondary replica synchronous commit.

   ```Transact-SQL
   ALTER AVAILABILITY GROUP [ag1] MODIFY REPLICA ON N'**<node2>*' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
   ```
1. Update `required_synchronized_secondaries_to_commit`to 1.

   This setting ensures that every active transaction is committed to the primary replica and at least one synchronous secondary. The availability group is ready to fail over when the synchronization_state_desc is SYNCHRONIZED and the sequence_number is the same for both primary and target secondary replica. Run this query to check:

   ```Transact-SQL
   SELECT ag.name, 
      drs.database_id, 
      drs.group_id, 
      drs.replica_id, 
      drs.synchronization_state_desc, 
      ag.sequence_number
   FROM sys.dm_hadr_database_replica_states drs, sys.availability_groups ag
   WHERE drs.group_id = ag.group_id; 
   ```

1. Demote the primary replica to secondary replica. After the primary replica is demoted, it is read-only. Run this command on the SQL instance hosting the primary replica to update the role to SECONDARY:

   ```Transact-SQL
   ALTER AVAILABILITY GROUP [ag1] SET (ROLE = SECONDARY); 
   ```

1. Promote the target secondary replica to primary. 

   ```Transact-SQL
   ALTER AVAILABILITY GROUP distributedag FORCE_FAILOVER_ALLOW_DATA_LOSS; 
   ```  

   > [!NOTE] 
   > To delete an availability group use [DROP AVAILABILITY GROUP](https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-availability-group-transact-sql). For an availability group created with CLUSTER_TYPE NONE or EXTERNAL, the command has to be executed on all replicas part of the availability group.

## Next steps

[Configure distributed availability group](..\database-engine\availability-groups\windows\distributed-availability-groups-always-on-availability-groups.md)

[Learn more about availability groups](..\database-engine\availability-groups\windows\overview-of-always-on-availability-groups-sql-server.md)

