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
		    FAILOVER_MODE = NONE,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    ),
        N'**<node2>**' WITH ( 
		    ENDPOINT_URL = N'tcp://**<node2>**:**<5022>**', 
		    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = NONE,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    );
		
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

>[!NOTE]
>`CLUSTER_TYPE` is a new option for `CREATE AVAILABILITY GROUP`. An availability group requires`CLUSTER_TYPE = NONE` when it is on a SQL Server instance that is not a member a cluster.

[!INCLUDE [Create Post](../includes/ss-linux-cluster-availability-group-create-post.md)]

## Next steps


