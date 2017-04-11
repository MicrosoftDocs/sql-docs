---
# required metadata

title: Configure availability group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 03/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 150b0765-2c54-4bc4-b55a-7e57a5501a0f 

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

# Configure Always On Availability Group for SQL Server on Linux

You can configure an Always On availability group for SQL Server on Linux. There are two architectures for availability groups. A *high availability* (HA) architecture uses a cluster manager to provide improved business continuity. This architecture can also include read-scale replicas. This document explains how to create the availability group HA architecture.

You can also create a *read-scale* availability group without a cluster manager. This architecture only provides read-only scalability. It does not provide HA. To create a read-scale availability group, see [Configure read-scale availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-rs.md).

[!INCLUDE [Create Prereq](../includes/ss-linux-cluster-availability-group-create-prereq.md)]

## Create the availability group

Create the availability group. In order to create the avalability group for HA on Linux, set `CLUSTER_TYPE = EXTERNAL`. This setting allows an external (non-Windows) cluster manager to manage SQL Server. When `CLUSTER_TYPE = EXTERNAL` the only valid setting for `FAILOVER_MODE` is `EXTERNAL`.

The following Transact-SQL script creates an availability group name `ag1`. The script configures the availability group replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server after it is added to the availability group. Update the following script for your environment. Replace the  `**<node1>**` and `**<node2>**` values with the names of the SQL Server instances that will host the replicas. Replace the `**<5022>**` with the port you set for the endpoint. Run the following Transact-SQL on the primary SQL Server replica to create the availability group.

```Transact-SQL
CREATE AVAILABILITY GROUP [ag1]
    WITH (DB_FAILOVER = ON, CLUSTER_TYPE = EXTERNAL)
    FOR REPLICA ON
        N'**<node1>**' WITH (
            ENDPOINT_URL = N'tcp://**<node1>:**<5022>**',
		    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = EXTERNAL,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    ),
        N'**<node2>**' WITH ( 
		    ENDPOINT_URL = N'tcp://**<node2>**:**<5022>**', 
		    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = EXTERNAL,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    );
		
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

>[!NOTE]
>`CLUSTER_TYPE` is a new option for `CREATE AVAILABILITY GROUP`. An availability group requires`CLUSTER_TYPE = EXTERNAL` when it is on a SQL Server instance that is not a member of a cluster that is not a Windows server failover cluster.

### Database level monitoring and failover trigger

For `CLUSTER_TYPE=EXTERNAL`, the  failover trigger semantics are different compared to WSFC. When the availability group is on an instance of SQL Server in a WSFC, transitioning out of `ONLINE` state for the database causes the availability group health to report a fault. This signals the cluster manager to trigger a failover. In Linux, the SQL Server instance cannot communicate with the cluster. Monitoring for database health is done "outside-in". If you opted in for database level failover monitoring and failover (by setting the DDL option `DB_FAILOVER=ON`), the cluster will check if the database state is `ONLINE` every time when it runs a monitoring action. The cluster queries the state in `sys.databases`. For any state different than `ONLINE`, it triggers a failover automatically (if automatic failover conditions are met). The actual time of the failover depends on the frequency of the monitoring action as well as the database state being updated in sys.databases.


[!INCLUDE [Create Post](../includes/ss-linux-cluster-availability-group-create-post.md)]


## Notes

**The availability group is not a clustered resource at this point.** 

   If you followed the steps in this document, you have an availability group that is not yet clustered. The next step is to add the cluster. While this is a valid configuration in read-scale/load balancing scenarios, it is not valid for HADR. To achieve HADR, you need to add the availability group as a cluster resource. See [Next steps](#next-steps) for instructions. 
   
   While the availability group is not in a cluster, note the following behaviors:

   - If the primary replica goes down and comes back up - for example if the SQL Server instance or node restarts the availability group will go in `RESOLVING` state. A database restart does not trigger the availability group to go into a `RESOLVING` state. Only an instance restart triggers availability group state evaluation. Because there is no cluster controller to manage the availability group elect one of replicas as primary, you need to manually fail over the availability group. To do this, run `ALTER AVAILABILITY GROUP FAILOVER` on the replica that you choose as primary. You can run `ALTER AVAILABILITY GROUP FAILOVER` on any the former primary replica or any secondary replica. Note the following behavior:
      - If you run this on the former primary replica then previous configuration returns.
      - If you run this on a secondary replica then the rest of replicas - including the former PRIMARY - will automatically join the availability group.
      
   - During failover a cluster manager ensures that the demotion action is completed before the promotion action starts so that there will not be two primary replicas in the configuration. On Windows, WSFC is the cluster manager. On a Linux cluster, Pacemaker can be the cluster manager. Without a cluster manager, you have to manually demote the primary replica and promote the secondary replicas in sequential order. If you initiate a failover without a cluster manager, there is no guarantee the demotion is successful - if the primary replica is unresponsive, for example - but promotion can succeed. To avoid having two primary replicas if the demotion fails, you can complete the manual fail over in two steps. First demote the primary replica, and then promote the secondary replica. 
      1. Demote the current primary. On the primary SQL Server replica, run the following query:
         ```Transact-SQL
         ALTER AVAILABILITY GROUP [ag1] SET (ROLE = SECONDARY);
         ```
      1. Promote the current secondary to new primary. On the node that you want to promote run the following query:
         ```Transact-SQL
         ALTER AVAILABILITY GROUP [ag1] FAILOVER;
         ```

>[!IMPORTANT]
>After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs`. In SLES use `crm`. 

>[!IMPORTANT]
>If the availability group is a cluster resource, there is a known issue in current release where forced failover with data loss to an asynchronous replica does not work. This will be fixed in the upcoming release. Manual or automatic failover to a synchronous replica will succeed. 


## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)
