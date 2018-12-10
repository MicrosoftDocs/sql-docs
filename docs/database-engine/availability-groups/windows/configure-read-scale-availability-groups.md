---
title: "Configure read-scale for an availability group"
description: "Configure your Always On availability group for read-scale workloads on Windows."
ms.custom: "seodec18"
author: MashaMSFT
ms.author: mathoma
manager: craigg
ms.reviewer: ""
ms.date: 05/24/2018
ms.topic: conceptual
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
---
# Configure read-scale for an Always On availability group

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

You can configure a SQL Server Always On availability group for read-scale workloads on Windows. There are two types of architecture for availability groups:
* An architecture for high availability that uses a cluster manager to provide improved business continuity and that can include readable-secondary replicas. To create this high-availability architecture, see [Create and configure availability groups on Windows](creation-and-configuration-of-availability-groups-sql-server.md). 
* An architecture that supports only read-scale workloads. 

This article explains how to create an availability group without a cluster manager for read-scale workloads. This architecture provides read-scale only. It doesn't provide high availability.

>[!NOTE]
>An availability group with `CLUSTER_TYPE = NONE` can include replicas that are hosted on a variety of operating system platforms. It cannot support high availability. For the Linux operating system, see [Configure a SQL Server availability group for read-scale on Linux](../../../linux/sql-server-linux-availability-group-configure-rs.md).

[!INCLUDE [Create prerequisites](../../../includes/ss-availability-group-rs-prereq.md)]

## Create an availability group

Create an availability group. Set `CLUSTER_TYPE = NONE`. In addition, set each replica with `FAILOVER_MODE = NONE`. Client applications that run analytics or reporting workloads can directly connect to the secondary databases. You can also create a read-only routing list. Connections to the primary replica forward read connection requests to each of the secondary replicas from the routing list in a round-robin fashion.

The following Transact-SQL script creates an availability group named `ag1`. The script configures the availability group replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server after it is added to the availability group. 

Update the following script for your environment. Replace the `<node1>` and `<node2>` values with the names of the SQL Server instances that host the replicas. Replace the `<5022>` value with the port that you set for the endpoint. Run the following Transact-SQL script on the primary SQL Server replica:

```sql
CREATE AVAILABILITY GROUP [ag1]
    WITH (CLUSTER_TYPE = NONE)
    FOR REPLICA ON
        N'<node1>' WITH (
            ENDPOINT_URL = N'tcp://<node1>:<5022>',
		    AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = MANUAL,
		    SEEDING_MODE = AUTOMATIC,
                    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    ),
        N'<node2>' WITH (
		    ENDPOINT_URL = N'tcp://<node2>:<5022>',
		    AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = MANUAL,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    );

ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

### Join secondary SQL Server instances to the availability group

The following Transact-SQL script joins a server to an availability group named `ag1`. Update the script for your environment. To join the availability group, run the following Transact-SQL script on each secondary SQL Server replica:

```sql
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = NONE);

ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

[!INCLUDE [Create post](../../../includes/ss-availability-group-rs-postactivity.md)]

This availability group isn't a high-availability configuration. If you need high availability, follow the instructions at [Configure an Always On availability group for SQL Server on Linux](../../../linux/sql-server-linux-availability-group-configure-ha.md) or [Creation and Configuration of availability groups on Windows](creation-and-configuration-of-availability-groups-sql-server.md).

## Connect to read-only secondary replicas

You can connect to read-only secondary replicas in either of two ways:
* Applications can connect directly to the SQL Server instance that hosts the secondary replica and query the databases. For more information, see [Readable secondary replicas](active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).
* Applications can also use read-only routing, which requires a listener. For more information, see [Read-only routing](listeners-client-connectivity-application-failover.md#ConnectToSecondary).

## Fail over the primary replica on a read-scale availability group

[!INCLUDE[Force failover](../../../includes/ss-force-failover-read-scale-out.md)]

## Next steps

* [Configure a distributed availability group](distributed-availability-groups-always-on-availability-groups.md)
* [Learn more about availability groups](overview-of-always-on-availability-groups-sql-server.md)
* [Perform a forced manual failover](perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)
