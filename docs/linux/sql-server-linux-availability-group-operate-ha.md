---
title: Operate availability group SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 03/01/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 
---
# Operate Always On Availability Groups on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

## Upgrade availability group

Before you upgrade an availability group, review the patterns and practices at [Upgrading availability group replica instances](../database-engine/availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md).

The following sections explain how to perform a rolling upgrade with SQL Server instances on Linux with availability groups. 

### Upgrade steps on Linux

When availability group replicas are on instances of SQL Server in Linux, the cluster type of the availability group is either `EXTERNAL` or `NONE`. An availability group that is managed by a cluster manager besides Windows Server Failover Cluster (WSFC) is `EXTERNAL`. Pacemaker with Corosync is an example of an external cluster manager. An availability group with no cluster manager has cluster type `NONE` The upgrade steps outlined here are specific for availability groups of cluster type `EXTERNAL` or `NONE`.

The order in which you upgrade instances depends on if their role is secondary and whether or not they host synchronous or asynchronous replicas. Upgrade instances of SQL Server that host asynchronous secondary replicas first. Then upgrade instances that host synchronous secondary replicas. 

   >[!NOTE]
   >If an availability group only has asynchronous replicas, to avoid any data loss change one replica to synchronous and wait until it is synchronized. Then upgrade this replica.
   
Before you begin, back up each database.

1. Stop the resource on the node hosting the secondary replica targeted for upgrade.
   
   Before running the upgrade command, stop the resource so the cluster will not monitor it and fail it unnecessarily. The following example adds a location constraint on the node that will result on the resource to be stopped. Update `ag_cluster-master` with the resource name and `nodeName1` with the node hosting the replica targeted for upgrade.

   ```bash
   pcs constraint location ag_cluster-master avoids nodeName1
   ```

1. Upgrade SQL Server on the secondary replica.

   The following example upgrades `mssql-server` and `mssql-server-ha` packages.

   ```bash
   sudo yum update mssql-server
   sudo yum update mssql-server-ha
   ```
1. Remove the location constraint.

   Before running the upgrade command, stop the resource so the cluster will not monitor it and fail it unnecessarily. The following example adds a location constraint on the node that will result on the resource to be stopped. Update `ag_cluster-master` with the resource name and `nodeName1` with the node hosting the replica targeted for upgrade.

   ```bash
   pcs constraint remove location-ag_cluster-master-rhel1--INFINITY
   ```
   As a best practice, ensure the resource is started (using `pcs status` command) and the secondary replica is connected and synchronized state after upgrade.

1. After all secondary replicas are upgraded, manually fail over to one of the synchronous secondary replicas.

   For availability groups with `EXTERNAL` cluster type, use the cluster management tools to fail over; availability groups with `NONE` cluster type should use Transact-SQL to fail over. 
   The following example fails over an availability group with the cluster management tools. Replace `<targetReplicaName>` with the name of the synchronous secondary replica that will become primary:

   ```bash
   sudo pcs resource move ag_cluster-master <targetReplicaName> --master  
   ``` 
   
   >[!IMPORTANT]
   >The following steps only apply to availability groups that do not have a cluster manager.

   If the availability group cluster type is `NONE`, manually fail over. Complete the following steps in order:

      a. The following command sets the primary replica to secondary. Replace `AG1` with the name of your availability group. Run the Transact-SQL command on the instance of SQL Server that hosts the primary replica.

      ```transact-sql
      ALTER AVAILABILITY GROUP [ag1] SET (ROLE = SECONDARY);
      ```

      b. The following command sets a synchronous secondary replica to primary. Run the following Transact-SQL command on the target instance of SQL Server - the instance that hosts the synchronous secondary replica.

      ```transact-sql
      ALTER AVAILABILITY GROUP [ag1] FAILOVER;
      ```

1. After failover, upgrade SQL Server on the old primary replica by repeating the preceding procedure.

   The following example upgrades `mssql-server` and `mssql-server-ha` packages.

   ```bash
   # add constraint for the resource to stop on the upgraded node
   # replace 'nodename2' with the name of the cluster node targeted for upgrade
   pcs constraint location ag_cluster-master avoids nodeName2
   sudo yum update mssql-server
   sudo yum update mssql-server-ha
   ```
   
   ```bash
   # upgrade mssql-server and mssql-server-ha packages
   sudo yum update mssql-server
   sudo yum update mssql-server-ha
   ```

   ```bash
   # remove the constraint; make sure the resource is started and replica is connected and synchronized
   pcs constraint remove location-ag_cluster-master-rhel1--INFINITY
   ```

1. For an availability groups with an external cluster manager - where cluster type is EXTERNAL, clean up the location constraint that was caused by the manual failover. 

   ```bash
   sudo pcs constraint remove cli-prefer-ag_cluster-master  
   ```

1. Resume data movement for the newly upgraded secondary replica - the former primary replica. This step is required when a higher version instance of SQL Server is transferring log blocks to a lower version instance in an availability group. Run the following command on the new secondary replica (the previous primary replica).

   ```transact-sql
   ALTER DATABASE database_name SET HADR RESUME;
   ```

After upgrading all servers, you can fail back. Fail over back to the original primary - if necessary. 

## Drop an availability group

To delete an availability group, run [DROP AVAILABILITY GROUP](../t-sql/statements/drop-availability-group-transact-sql.md). If the cluster type is `EXTERNAL` or `NONE` run the command on every instance of SQL Server that hosts a replica. For example, to drop an availability group named `group_name` run the following command:

   ```transact-sql
   DROP AVAILABILITY GROUP group_name
   ```
 

## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)
