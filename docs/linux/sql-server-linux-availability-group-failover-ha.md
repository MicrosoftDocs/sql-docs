---
# required metadata

title: Fail over HA availability group SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 04/12/2017
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

# Fail over HA availability group for SQL Server on Linux

Use the cluster management tools to failover an availability group managed by an external cluster manager. For example, if a solution uses Pacemaker to manage a Linux cluster, use `pcs` to perform manual failovers. 

> [!IMPORTANT]
> Under normal operations, do not fail over with Transact-SQL or SQL Server management tools like SSMS or PowerShell. When `CLUSTER_TYPE = EXTERNAL`, the only acceptable value for `FAILOVER_MODE` is `EXTERNAL`. With these settings, all manual or automatic failover actions are executed by the external cluster manager - for example Pacemaker for automatic failover or using the external cluster management tools like `pcs` on RHEL/Ubuntu or `crm` on SLES. 

In extreme cases, if a user cannot use the cluster management tools for interacting with the cluster (i.e. the cluster is unresponsive, cluster management tools have a faulty behaviour), the user might have to perform a failover bypassing the external cluster manager. This is not recommended for regular operations, and should be used within cases cluster is failing to execute the failover action using the cluster management tools.

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

1. Manually set the session context variable `external_cluster`.

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
   sudo pcs resource cleanup <**resourceName**>
   ```

## Pacemaker notification for availability group resource promotion

Before the CTP 1.4 release, the Pacemaker resource agent for availability groups could not know if a replica marked as `SYNCHRONOUS_COMMIT` was really up-to-date or not. It was possible that the replica had stopped synchronizing with the primary but was not aware. Thus the agent could promote an out-of-date replica to primary - which, if successful, would cause data loss. 

SQL Server vNext CTP 1.4 added `sequence_number` to `sys.availability_groups` to solve this issue. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local availability group replica is with respect to the rest of the replicas in the availability group. Performing failovers, adding or removing replicas, and other availability group operations update this number. The number is updated on the primary, then pushed to secondary replicas. Thus a secondary replica that is up-to-date will have the same sequence_number as the primary. 

When Pacemaker decides to promote a replica to primary, it first sends a notification to all replicas to extract the sequence number and store it (we call this the pre-promote notification). Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers from all replicas and rejects the promote operation otherwise. In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss. 

Note that this is only guaranteed to work as long as at least one replica available for promotion has the same sequence number as the previous primary. To ensure this, the default behavior is for the Pacemaker resource agent to automatically set `REQUIRED_COPIES_TO_COMMIT` such that at least one synchronous commit secondary replica is up to date and available to be the target of an automatic failover. With each monitoring action, the value of `REQUIRED_COPIES_TO_COMMIT` is computed (and updated if necessary)  as ('number of synchronous commit replicas' / 2). Then, at failover time, the resource agent will require (`total number of replicas` - `required_copies_to_commit` replicas) to respond to the pre-promote notification to be able to promote one of them to primary. The replica with the highest `sequence_number` will be promoted to primary. 

For example, let's consider the case of an availability group with three synchronous replicas - one primary replica and two synchronous commit secondary replicas.

- `REQUIRED_COPIES_TO_COMMIT`  is 3 / 2 = 1

- The required number of replicas to respond to pre-promote action is 3 - 1 = 2. So 2 replicas have to be up for the failover to be triggered. This means that, in the case of primary outage, if one of the secondary replicas is unresponsive and only one of the secondaries responds to the pre-promote action, the resource agent cannot guarantee that the secondary that responded has the highest sequence_number, and a failover is not triggered.

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

>[!NOTE]
>Updating resource properties causes all replicas to stop and restart. This means primary will temporarily be demoted to secondary, then promoted again which will casue temporary write unavailability. The new value for REQUIRED_COPIES_TO_COMMIT will only be set once replicas are restarted, so it won't be instantaneous with running the pcs command.

## Manage availability group with two synchronous replicas

The above default behavior applies to the case of 2 synchronous replicas (primary + secondary) as well. Pacemaker will default `REQUIRED_COPIES_TO_COMMIT` = 1 to ensure the secondary replica is always up to date for maximum data protection.  

>[!WARNING]
>This comes with higher risk of unavailability of the primary replica due to planned or unplanned outages on the secondary. The user can choose to change the default behavior of the resource agent and override the `REQUIRED_COPIES_TO_COMMIT` to 0:

```bash
sudo pcs resource update <**ag1**> required_copies_to_commit=0
```

Once overridden, the resource agent will use the new setting for `REQUIRED_COPIES_TO_COMMIT` and stop computing it. This means that users have to manually update it accordingly (for example, if they increase the number of replicas).

The table below describes the outcome of an outage for primary or secondary replicas in different availability group resource configurations:

| | Availability group with 2 sync replicas |  |Availability Group with 3 sync replicas | | |
|---|---|---|---|---|---|
| |`REQUIRED_COPIES_TO_COMMIT=0`|`REQUIRED_COPIES_TO_COMMIT=1`* |`REQUIRED_COPIES_TO_COMMIT=0` |`REQUIRED_COPIES_TO_COMMIT=1`* |`REQUIRED_COPIES_TO_COMMIT=2` 
|**Primary outage** |User has to issue a manual FAILOVER (might have data loss) -> New primary is R/W |Cluster will automatically issue FAILOVER (no data loss) -> New primary is RO until former primary recovers and joins availability group as secondary | User has to issue a manual FAILOVER (might have data loss) -> New primary is R/W | Cluster will automatically issue FAILOVER (no data loss) -> New primary is R/W |Cluster will automatically issue FAILOVER (no data loss) -> New primary is R/O until former primary recovers and joins availability group as secondary
|**One secondary replica outage** |Primary is R/W, running exposed to data loss |Primary is RO until secondary recovers |Primary is R/W | Primary is R/W Primary is RO

* SQL Server resource agent for Pacemaker default behavior

## Notes

### Database level monitoring and failover trigger

For `CLUSTER_TYPE=EXTERNAL`, the  failover trigger semantics are different compared to WSFC. When the availability group is on an instance of SQL Server in a WSFC, transitioning out of `ONLINE` state for the database causes the availability group health to report a fault. This will signal the cluster manager to trigger a failover action. On Linux, the SQL Server instance cannot communicate with the cluster. Monitoring for database health is done "outside-in". If user opted in for database level failover monitoring and failover (by setting the option `DB_FAILOVER=ON` when creating the availability group), the cluster will check if the database state is `ONLINE` every time when it runs a monitoring action. The cluster queries the state in `sys.databases`. For any state different than `ONLINE`, it will trigger a failover automatically (if automatic failover conditions are met). The actual time of the failover depends on the frequency of the monitoring action as well as the database state being updated in sys.databases.

## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)
