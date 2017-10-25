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

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

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

[!INCLUDE[Force Failover](../includes/ss-force-failover-read-scale-out.md)]

## Next steps

[Configure distributed availability group](..\database-engine\availability-groups\windows\distributed-availability-groups-always-on-availability-groups.md)

[Learn more about availability groups](..\database-engine\availability-groups\windows\overview-of-always-on-availability-groups-sql-server.md)

