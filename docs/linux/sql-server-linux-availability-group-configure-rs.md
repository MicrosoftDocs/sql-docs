---
# required metadata

title: Configure read-scale availability group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 03/17/2017
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

Create the availability group. In order to create the availability group for read-scale on Linux, set `CLUSTER_TYPE = NONE`. Use this setting when there is no cluster manager. In addition, set each replica with `FAILOVER_MODE = NONE`. In this configuration the availability group does not provide HA, but it does provide read-scale. 

The following Transact-SQL script creates an availability group name `ag1`. The script configures the availability group replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server after it is added to the availability group. Update the following script for your environment. Replace the  `**<node1>**` and `**<node2>**` values with the names of the SQL Server instances that will host the replicas. Replace the `**<5022>**` with the port you set for the endpoint. Run the following Transact-SQL on the primary SQL Server replica to create the availability group.

```Transact-SQL
CREATE AVAILABILITY GROUP [ag1]
    WITH (DB_FAILOVER = ON, CLUSTER_TYPE = NONE)
    FOR REPLICA ON
        N'**<node1>**' WITH (
            ENDPOINT_URL = N'tcp://**<node1>:**<5022>**',
		    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = MANUAL,
		    SEEDING_MODE = AUTOMATIC
		    ),
        N'**<node2>**' WITH ( 
		    ENDPOINT_URL = N'tcp://**<node2>**:**<5022>**', 
		    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = MANUAL,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    );
		
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

>[!NOTE]
>`CLUSTER_TYPE` is a new option for `CREATE AVAILABILITY GROUP`. An availability group requires`CLUSTER_TYPE = NONE` when it is on a SQL Server instance that is not a member a cluster.

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

- Manual failover with data loss

- Manual failover without data loss

### Manual failover with data loss

Use this method when the primary replica is not available.

### Manual failover without data loss

Use this method when the primary replica is available. Choose one secondary replica to failover to. This is the target secondary replica.

1. Make the target secondary replica synchronous commit.

1. Update the required copies to commit to 1.

   This ensures that any active connections to the primary replica commit transactions on the target secondary replica.

1. Demote the primary replica to secondary replica.

   After the primary replica is demoted, connections to the primary replica will not be able to write to the databases.

1. Promote the target replica to primary. 

## Next steps

[Configure distributed availability group](..\database-engine\availability-groups\windows\distributed-availability-groups-always-on-availability-groups.md)

[Learn more about availability groups](..\database-engine\availability-groups\windows\overview-of-always-on-availability-groups-sql-server.md)

