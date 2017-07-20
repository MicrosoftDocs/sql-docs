---
# required metadata

title: Operate availability group SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 07/20/2017
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

# Operate HA availability group for SQL Server on Linux

## <a name="failover"></a>Fail over availability group

Use the cluster management tools to fail over an availability group managed by an external cluster manager. For example, if a solution uses Pacemaker to manage a Linux cluster, use `pcs` to perform manual failovers on RHEL or Ubuntu. On SLES use `crm`. 

> [!IMPORTANT]
> Under normal operations, do not fail over with Transact-SQL or SQL Server management tools like SSMS or PowerShell. When `CLUSTER_TYPE = EXTERNAL`, the only acceptable value for `FAILOVER_MODE` is `EXTERNAL`. With these settings, all manual or automatic failover actions are executed by the external cluster manager. 

### Manual failover examples

Manually fail over the availability group with the external cluster management tools. Under normal operations, do not initiate failover with Transact-SQL. If the external cluster management tools do not respond, you can force the availability group to fail over. For instructions to force the manual failover, see [Manual move when cluster tools are not responsive](#forceManual).

Complete the manual failover in two steps. 

1. Move the availability group resource from the cluster node that owns the resources to a new node.

   The cluster manager moves the availability group resource and adds a location constraint. This constraint configures the resource to run on the new node. You must remove this constraint in order to move either manually or automatically fail over in the future.

2. Remove the location constraint.

#### 1. Manually fail over

To manually fail over an availability group resource named *ag_cluster* to cluster node named *nodeName2*, run appropriate command for your distribution:

- **RHEL/Ubuntu example**

   ```bash
   sudo pcs resource move ag_cluster-master nodeName2 --master
   ```

- **SLES example**

   ```bash
   crm resource migrate ag_cluster nodeName2
   ```



>[!IMPORTANT]
>After you manually fail over a resource, you need to remove a location constraint that is automatically added during the move.

#### 2. Remove the location constraint

During a manual move, the `pcs` command `move` or `crm` command `migrate` adds a location constraint for the resource to be placed on the new target node. To see the new constraint, run the following command after manually moving the resource:

- **RHEL/Ubuntu example**

   ```bash
   sudo pcs constraint --full
   ```

- **SLES example**

   ```bash
   crm config show
   ```

You need to remove the location constraint so future moves - including automatic failover - succeed. 

To remove the constraint run the following command. 

- **RHEL/Ubuntu example**

   In this example `ag_cluster-master` is the name of the resource that was moved. 

   ```bash
   sudo pcs resource clear ag_cluster-master 
   ```

- **SLES example**

   In this example `ag_cluster` is the name of the resource that was moved. 

   ```bash
   crm resource clear ag_cluster
   ```

Alternatively, you can run the following command to remove the location constraint.  

- **RHEL/Ubuntu example**

   In the following command `cli-prefer-ag_cluster-master` is the ID of the constraint that needs to be removed. `sudo pcs constraint --full` returns this ID. 

   ```bash
   sudo pcs constraint remove cli-prefer-ag_cluster-master  
   ```
- **SLES example**

   In the following command `cli-prefer-ms-ag_cluster` is the ID of the constraint. `crm config show` returns this ID. 
   
   ```bash
   crm configure
   delete cli-prefer-ms-ag_cluster 
   commit
   ```

>[!NOTE]
>Automatic failover does not add a location constraint, so no cleanup is necessary. 

For more information:
- [Red Hat - Managing Cluster Resources](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/6/html/Configuring_the_Red_Hat_High_Availability_Add-On_with_Pacemaker/ch-manageresource-HAAR.html)
- [Pacemaker - Move Resources Manually](http://clusterlabs.org/doc/en-US/Pacemaker/1.1-pcs/html/Clusters_from_Scratch/_move_resources_manually.html)
 [SLES Administration Guide - Resources](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#sec.ha.troubleshooting.resource) 
 

### <a name="forceManual"></a> Manual move when cluster tools are not responsive 

In extreme cases, if a user cannot use the cluster management tools for interacting with the cluster (i.e. the cluster is unresponsive, cluster management tools have a faulty behavior), the user might have to fail over manually - bypassing the external cluster manager. This is not recommended for regular operations, and should be used within cases cluster is failing to execute the failover action using the cluster management tools.

If you cannot fail over the availability group with the cluster management tools, follow these steps to fail over from SQL Server tools:

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

## Database level monitoring and failover trigger

For `CLUSTER_TYPE=EXTERNAL`, the  failover trigger semantics are different compared to WSFC. When the availability group is on an instance of SQL Server in a WSFC, transitioning out of `ONLINE` state for the database causes the availability group health to report a fault. This will signal the cluster manager to trigger a failover action. On Linux, the SQL Server instance cannot communicate with the cluster. Monitoring for database health is done "outside-in". If user opted in for database level failover monitoring and failover (by setting the option `DB_FAILOVER=ON` when creating the availability group), the cluster will check if the database state is `ONLINE` every time when it runs a monitoring action. The cluster queries the state in `sys.databases`. For any state different than `ONLINE`, it will trigger a failover automatically (if automatic failover conditions are met). The actual time of the failover depends on the frequency of the monitoring action as well as the database state being updated in sys.databases.

## Upgrade availability group

Before you upgrade an availability group, review the best practices at [Upgrading availability group replica instances](../database-engine/availability-groups/windows/upgrading-always-on-availability-group-replica-instances.md).

The following sections explain how to perform a rolling upgrade with SQL Server instances on Linux with availability groups. 

>[!WARNING]
>On Linux, rolling upgrade to SQL Server 2017 RC1 does not work. Microsoft is planning to resolve this for a future release. 

### Upgrade steps on Linux

When availability group replicas are on instances of SQL Server in Linux, the cluster type of the availability group is either `EXTERNAL` or `NONE`. An availability group that is managed by a cluster manager besides Windows Server Failover Cluster (WSFC) is `EXTERNAL`. Pacemaker with Corosync is an example of an external cluster manager. An availability group with no cluster manager has cluster type `NONE` The upgrade steps outlined here are specific for availability groups of cluster type `EXTERNAL` or `NONE`.

1. Before you begin, backup each database.
2. Upgrade instances of SQL Server that host secondary replicas.

    a. Upgrade asynchronous secondary replicas first.

    b. Upgrade synchronous secondary replicas.

   >[!NOTE]
   >If an availability group only has asynchronous replicas - to avoid any data loss change one replica to synchronous and wait until it is synchronized. Then upgrade this replica.

   The following example upgrades `mssql-server` and `mssql-server-ha` packages.

   ```bash
   sudo yum update mssql-server
   sudo yum update mssql-server-ha
   ```

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

1. After failover, upgrade SQL Server on the old primary replica. 

   The following example upgrades `mssql-server` and `mssql-server-ha` packages.

   ```bash
   sudo yum update mssql-server
   sudo yum update mssql-server-ha
   ```

1. For an availability groups with an external cluster manager - where cluster type is EXTERNAL, cleanup the location constraint that was caused by the manual failover. 

   ```bash
   sudo pcs constraint remove cli-prefer-ag_cluster-master  
   ```

1. Resume data movement for the newly upgraded secondary replica - the former primary replica. This is required when a higher version instance of SQL Server is transferring log blocks to a lower version instance in an availability group. Run the following command on the new secondary replica (the previous primary replica).

   ```transact-sql
   ALTER DATABASE database_name SET HADR RESUME;
   ```

After upgrading all servers, you can failback - fail over back to the original primary - if necessary. 

## Drop an availability group

To delete an availability group, run [DROP AVAILABILITY GROUP](../t-sql/statements/drop-availability-group-transact-sql.md). If the cluster type is `EXTERNAL` or `NONE` run the command on every instance of SQL Server that hosts a replica. For example, to drop an availability group named `group_name` run the following command:

   ```transact-sql
   DROP AVAILABILITY GROUP group_name
   ```
 

## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)
