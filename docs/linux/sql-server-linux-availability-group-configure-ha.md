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

# Configure Always On Availability Group for SQL Server on Linux

You can configure an Always On availability group for SQL Server on Linux. There are two architectures for availability groups. A *high availability* (HA) architecture uses a cluster manager to provide improved business continuity. This architecture can also include read-scale replicas. This document explains how to create the availability group HA architecture.

You can also create a *read-scale* availability group without a cluster manager. This architecture only provides read-only scalability. It does not provide HA. To create a read-scale availability group, see [Configure read-scale availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-rs.md).

[!INCLUDE [Create Prereq](../includes/ss-linux-cluster-availability-group-create-prereq.md)]

## Create the availability group

Create the availability group. In order to create the availability group for HA on Linux, set `CLUSTER_TYPE = EXTERNAL`. 

The `EXTERNAL` value for `CLUSTER_TYPE` option specifies that the an external cluster entity manages the availability group. Pacemaker is an example of an external cluster entity. When the availability group `CLUSTER_TYPE = EXTERNAL`, set each replica `FAILOVER_MODE = EXTERNAL`. After you create the availability group, configure the cluster resource for the availability group using the cluster management tools - for example with Pacemaker use `pcs`. See the Linux distribution specific cluster configuration section for an end-to-end example.

The following Transact-SQL script creates an availability group for HA named `ag1`. The script configures the availability group replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server. Update the following script for your environment. Replace the  `**<node1>**` and `**<node2>**` values with the names of the SQL Server instances that will host the replicas. Replace the `**<5022>**` with the port you set for the endpoint. Run the following Transact-SQL on the SQL Server instance that will host the primary replica to create the availability group.

```Transact-SQL
CREATE AVAILABILITY GROUP [ag1]
    WITH (DB_FAILOVER = ON, CLUSTER_TYPE = EXTERNAL)
    FOR REPLICA ON
        N'**<node1>**' WITH (
            ENDPOINT_URL = N'tcp://**<node1>:**<5022>**',
		    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = EXTERNAL,
		    SEEDING_MODE = AUTOMATIC
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

You can also configure an EXTERNAL availability group with SQL Server Management Studio or PowerShell. 

### Join secondary replicas to the availability group

The following Transact-SQL script joins a SQL Server instance to an availability group named `ag1`. Update the script for your environment. On each SQL Server instance that will host a secondary replica, run the following Transact-SQL to join the availability group.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = EXTERNAL);
		 
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```


[!INCLUDE [Create Post](../includes/ss-linux-cluster-availability-group-create-post.md)]

>[!IMPORTANT]
>After you create the availability group, you must configure integration with a cluster technology like WSFC (on Windows) and Pacemaker (Linux) for HA. In the case of a read-only scale-out architecture using availability groups, starting with SQL Server 2017, setting up a cluster is not required.

## Failover availability group

Use the cluster management tools to failover an availability group managed by an external cluster manager. For example, if a solution uses Pacemaker to manage a Linux cluster, use `pcs` to perform manual failovers. 

> [!IMPORTANT]
> Under normal conditions, do not fail over with Transact-SQL or SQL Server management tools like SSMS or PowerShell. When `CLUSTER_TYPE = EXTERNAL`, the only acceptable value for `FAILOVER_MODE` is `EXTERNAL`. With these settings, all manual or automatic failover actions are executed by the external cluster manager - for example Pacemaker. 

In extreme cases, you might have to failover with SQL Server tools to bypass the external cluster manager. For example, if the cluster is unresponsive, or the cluster management tools cannot interact with the cluster.  This is not recommended for regular operations, and should be used only when the cluster does not fail over with the cluster management tools. 

If you cannot failover the availability group with the cluster management tools, follow these steps to failover from SQL Server tools:

1. Verify that the availability group resource is not managed by the cluster any more. 

      - Attempt to set the resource to unmanaged mode. This signals the resource agent to stop resource monitoring and management. For example: 
      
      ```bash
      sudo pcs resource unmanage <**resourceName**>
      ```

      - If the attempt to set the resource mode to unmanaged mode fails, delete the resource. For example:

      ```bash
      sudo pcs resource delete <**resourceName**>
      ```

      >[!NOTE]
      >When you delete a resource it also deletes all of the associated constraints. 

1. Manually set the session context to `external_cluster`.

   ```Transact-SQL
   EXEC sp_set_session_context @key = N'external_cluster', @value = N'yes';
   ```

1. Fail over the availability group with Transact-SQL. In the example below replace `<**MyAg**>` with the name of your availability group. Connect to the instance of SQL Server that hosts the target secondary replica and run the following command:

   ```Transact-SQL
   ALTER AVAILABILITY GROUP <**MyAg**> FAILOVER;
   ```

1. Restart cluster resource monitoring and management. Run the following command:

   ```bash
   sudo pcs resource manage <**resourceName**>
   ```

## Pacemaker notification for availability group resource promotion

Before the CTP 1.4 release, the Pacemaker resource agent for availability groups could not know if a replica marked as `SYNCHRONOUS_COMMIT` was really up-to-date or not. It was possible that the replica had stopped synchronizing with the primary but was not aware. Thus the agent could promote an out-of-date replica to primary - which, if successful, would cause data loss. 

SQL Server vNext CTP 1.4 added `sequence_number` to `sys.availability_groups` to solve this issue. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local availability group replica is with respect to the rest of the replicas in the availability group. Performing failovers, adding or removing replicas, and other availability group operations update this number. The number is updated on the primary, then pushed to secondaries. Thus a secondary replica that is up-to-date will have the same sequence_number as the primary. 

When Pacemaker decides to promote a replica to primary, it first sends a notification to all replicas to extract the sequence number and store it (we call this the pre-promote notification). Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers from all replicas and rejects the promote operation otherwise. In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss. 

Note that this is only guaranteed to work as long as at least one replica available for promotion has the same sequence number as the previous primary. To ensure this, the default behavior is for the Pacemaker resource agent to automatically set `REQUIRED_COPIES_TO_COMMIT` such that at least one synchronous commit secondary replica is up to date and available to be the target of an automatic failover. With each monitoring action, the value of `REQUIRED_COPIES_TO_COMMIT` is computed (and updated if necessary)  as ('number of synchronous commit replicas' / 2). Then, at failover time, the resource agent will require (`total number of replicas` - `required_copies_to_commit` replicas) to respond to the pre-promote notification to be able to promote one of them to primary. The replica with the highest `sequence_number` will be promoted to primary. 

For example, let's consider the case of an availability group with three synchronous replicas - one primary replica and two synchronous commit secondary replicas.

- `REQUIRED_COPIES_TO_COMMIT`  is 3 / 2 = 1

- The required number of replicas to respond to pre-promote action is 3 - 1 = 2. So 2 replicas have to be up for the failover to be triggered. This means that if one of the secondary replicas is unresponsive and only one of the secondaries responds to the pre-promote action, the resource agent cannot guarantee that the secondary that responded has the highest sequence_number, and a failover is not triggered.

A user can choose to override the default behavior, and configure the availability group resource to not set `REQUIRED_COPIES_TO_COMMIT` automatically as above.

>[!IMPORTANT]
>When `REQUIRED_COPIES_TO_COMMIT` is 0 there is risk of data loss. In the case of an outage of the primary, the resource agent will not automatically trigger a failover. The user has to decide if they want to wait for primary to recover or manually fail over.

To set `REQUIRED_COPIES_TO_COMMIT` to 0, run:

```bash
sudo pcs resource update <**ag1**> required_copies_to_commit=0
```

To revert to default computed value, run:

```bash
sudo pcs resource update <**ag1**> required_copies_to_commit=
```

Because the resource agent requires Pacemaker to send notifications to all replicas, the availability resource needs to be configured with `notify=true`. The following commmand sets `notify=true`.

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=<**ag1**> --master meta notify=true
```

## Manage availability group with two synchronous replicas

The above default behavior applies to the case of 2 synchronous replicas (primary + secondary) as well. Pacemaker will default `REQUIRED_COPIES_TO_COMMIT` = 1 to ensure the secondary replica is always up to date for maximum data protection.  

>[!WARNING]
>This comes with higher risk of unavailability of the primary replica due to planned or unplanned outages on the secondary. The user can choose to change the default behavior of the resource agent and override the `REQUIRED_COPIES_TO_COMMIT` to 0:

```bash
sudo pcs resource update <**ag1**> required_copies_to_commit=0
```

Once overridden, the resource agent will use the new setting for `REQUIRED_COPIES_TO_COMMIT` and stop computing it. This means that users have to manually update it accordingly (for example, if they increase the number of replicas).

The table below describes the outcome of an outage for primary or secondary replicas in different availability group resource configurations:

| Availability group with 2 sync replicas |  |Availability Group with 3 sync replicas | | |
|---|---|---|---|---|
| |`REQUIRED_COPIES_TO_COMMIT=0`|`REQUIRED_COPIES_TO_COMMIT=1`* |`REQUIRED_COPIES_TO_COMMIT=0` |`REQUIRED_COPIES_TO_COMMIT=1`* |`REQUIRED_COPIES_TO_COMMIT=2` 
|**Primary outage** |User has to issue a manual FAILOVER (might have data loss) -> New primary is R/W |Cluster will automatically issue FAILOVER (no data loss) -> New primary is RO until former primary recovers and joins availability group as secondary | User has to issue a manual FAILOVER (might have data loss) -> New primary is R/W | Cluster will automatically issue FAILOVER (no data loss) -> New primary is R/W |Cluster will automatically issue FAILOVER (no data loss) -> New primary is R/O until former primary recovers and joins availability group as secondary
|**One secondary replica outage** |Primary is R/W, running exposed to data loss |Primary is RO until secondary recovers |Primary is R/W | Primary is R/W Primary is RO

* SQL Server resource agent for Pacemaker default behavior


## Notes

### Database level monitoring and failover trigger

For `CLUSTER_TYPE=EXTERNAL`, the  failover trigger semantics are different compared to WSFC. When the availability group is on an instance of SQL Server in a WSFC, transitioning out of `ONLINE` state for the database causes the availability group health to report a fault. This signals the cluster manager to trigger a failover. In Linux, the SQL Server instance cannot communicate with the cluster. Monitoring for database health is done "outside-in". If you opted in for database level failover monitoring and failover (by setting the DDL option `DB_FAILOVER=ON`), the cluster will check if the database state is `ONLINE` every time when it runs a monitoring action. The cluster queries the state in `sys.databases`. For any state different than `ONLINE`, it triggers a failover automatically (if automatic failover conditions are met). The actual time of the failover depends on the frequency of the monitoring action as well as the database state being updated in sys.databases.

### The availability group is not a clustered resource at this point 

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
