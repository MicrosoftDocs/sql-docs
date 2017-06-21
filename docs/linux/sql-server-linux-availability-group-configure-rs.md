---
# required metadata

title: Configure read-scale availability group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 05/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---

# Configure read-scale availability group for SQL Server on Linux

You can configure a read-scale availability group for SQL Server on Linux. There are two architectures for availability groups. A *high availability* (HA) architecture uses a cluster manager to provide improved business continuity. This architecture can also include read-scale replicas. To create the HA architecture, see [Configure Always On availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md).

This document explains how to create a *read-scale* availability group without a cluster manager. This architecture only provides read-only scalability. It does not provide HA.

[!INCLUDE [Create Prereq](../includes/ss-linux-cluster-availability-group-create-prereq.md)]

## Create the availability group

Create the availability group. In order to create the availability group for read-scale on Linux, set `CLUSTER_TYPE = NONE`. In addition, set each replica with `FAILOVER_MODE = NONE`. In this configuration the availability group does not provide HA, but it does provide read-scale. The client applications running analytics or reporting workloads can directly connect to the secondary databases. Or the customer can setup a read only routing list and connect to the primary that will forward the connection request to each of the secondary replicas from the routing list in a round robin fashion.

The following Transact-SQL script creates an availability group name `ag1`. The script configures the availability group replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server after it is added to the availability group. Update the following script for your environment. Replace the  `**<node1>**` and `**<node2>**` values with the names of the SQL Server instances that will host the replicas. Replace the `**<5022>**` with the port you set for the endpoint. Run the following Transact-SQL on the primary SQL Server replica to create the availability group.

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
   > [!NOTE] 
   > Running the CREATE AVAILABILITY GROUP command will complete with a warning: "Attempt to access non-existent or uninitialized availability group with ID . This is usually an internal condition, such as the availability group is being dropped or the local WSFC node has lost quorum. In such cases, and no user action is required.". This is a known issue and product team is working on a fix. Meanwhile, users should assume command completed successfully.

### Join secondary SQL Servers to the availability group

The following Transact-SQL script joins a server to an availability group named `ag1`. Update the script for your environment. On each secondary SQL Server replica, run the following Transact-SQL to join the availability group.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = NONE);
		 
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

[!INCLUDE [Create Post](../includes/ss-linux-cluster-availability-group-create-post.md)]

This is not an HA configuration, if you need HA, follow the instructions at [Configure Always On Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md). Specifically, create the availability group with `CLUSTER_TYPE=WSFC` (in Windows) or `CLUSTER_TYPE=EXTERNAL` (in Linux) and integrate with a cluster manager - either WSFC on Windows or Pacemaker on Linux.

## Connect to read only secondary replicas

There are two ways to connect to the read only secondary replicas. Applications can connect directly to the SQL Server instance that hosts the secondary replica and query the databases, or they can use read-only routing. Read only routing requires a listener.

[Readable secondary replicas](../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)

[Read only routing](../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md#ConnectToSecondary)

## Failover primary replica on read-scale availability group

Each availability group has only one primary replica. The primary replica allows reads and writes. To change which replica is the primary, you can failover. In an availability group for HA, the cluster manager automates in the failover process. In a read-scale availability group, the failover process is manual. There are two ways to failover the primary replica in a read scale availability group.

- Forced manual failover with data loss

- Manual failover without data loss

### Forced failover with data loss

Use this method when the primary replica is not available and can not be recovered. You can find more information about forced failover with data loss at [Perform a Forced Manual Failover](../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md).

To issue a forced failover with data loss, connect to the SQL instance hosting the target secondary replica and run:
```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] FORCE_FAILOVER_ALLOW_DATA_LOSS;
```

### Manual failover without data loss

Use this method when the primary replica is available, but you need to temporarly or permanently change the configuration and change the SQL instance that hosts the primary replica. One such case is during SQL Server upgrades. Before issuing manual failover, the user has to ensure that the target secondary replica is up to date, and there is no potential of data loss. The steps below describe how to achive this safely, without incuring data loss.

1. Make the target secondary replica synchronous commit.

   ```Transact-SQL
   ALTER AVAILABILITY GROUP [ag1] MODIFY REPLICA ON N'**<node2>*' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
   ```
1. Update `REQUIRED_COPIES_TO_COMMIT` to 1.

   This ensures no active transactions are committed to the primary replica without committing first to at least one synchronous secondary. The availabilty group is ready to failover when they synchronization_state_desc is SYNCHRONIZED and the sequence_number is the same for both primary and target secondary replica. Run this query to check:

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

1. Demote the primary replica to secondary replica. After the primary replica is demoted, connections to the primary replica will not be able to write to the databases. Run this command on the SQL instance hosting the primary replica to to update the role to SECONDARY:

   ```Transact-SQL
   ALTER AVAILABILITY GROUP [ag1] SET (ROLE = SECONDARY); 
   ```

1. Promote the target secondary replica to primary. 

   ```Transact-SQL
   ALTER AVAILABILITY GROUP distributedag FORCE_FAILOVER_ALLOW_DATA_LOSS; 
   ```  

   > [!NOTE] 
   > To delete an availability group use [DROP AVAILABILITY GROUP](https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-availability-group-transact-sql). For an availability group created with CLUSTER_TYPE NONE or EXTERNAL, the command has to be executed on all replicas part of the avilability group.

## Next steps

[Configure distributed availability group](..\database-engine\availability-groups\windows\distributed-availability-groups-always-on-availability-groups.md)

[Learn more about availability groups](..\database-engine\availability-groups\windows\overview-of-always-on-availability-groups-sql-server.md)

