---
title: Manage availability group failover - SQL Server on Linux
description: "This article describes types of failover: automatic, planned manual failover, and forced manual failover. Automatic and planned manual preserve all your data."
author: tejasaks
ms.author: tejasaks
ms.reviewer: vanto, randolphwest
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Always On availability group failover on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Within the context of an availability group (AG), the primary role and secondary role of availability replicas are typically interchangeable, in a process known as failover. Three forms of failover exist: automatic failover (without data loss), planned manual failover (without data loss), and forced manual failover (with possible data loss), typically called *forced failover*. Automatic and planned manual failovers preserve all your data. An AG fails over at the availability-replica level. That is, an AG fails over to one of its secondary replicas (the current failover target).

For background information about failover, see [Failover and Failover Modes (Always On Availability Groups)](../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).

## <a id="failover"></a> Manual failover

Use the cluster management tools to fail over an AG managed by an external cluster manager. For example, if a solution uses Pacemaker to manage a Linux cluster, use `pcs` to perform manual failovers on Red Hat Enterprise Linux (RHEL) or Ubuntu. On SUSE Linux Enterprise Server (SLES), use `crm`.

> [!IMPORTANT]  
> Under normal operations, don't fail over with Transact-SQL or SQL Server management tools like SSMS or PowerShell. When `CLUSTER_TYPE = EXTERNAL`, the only acceptable value for `FAILOVER_MODE` is `EXTERNAL`. With these settings, all manual or automatic failover actions are executed by the external cluster manager. For instructions to force failover with potential data loss, see [Force failover](#forceFailover).

### <a id="manualFailover"></a> Manual failover steps

To fail over, the secondary replica that will become the primary replica must be synchronous. If a secondary replica is asynchronous, [change the availability mode](../database-engine/availability-groups/windows/change-the-availability-mode-of-an-availability-replica-sql-server.md).

Manually fail over in two steps.

1. First, [manually fail over by moving AG resource](#manualMove) from the cluster node that owns the resources to a new node.

   The cluster fails the AG resource over and adds a location constraint. This constraint configures the resource to run on the new node. Remove this constraint in order to successfully fail over in the future.

1. Second, [remove the location constraint](#removeLocConstraint).

#### <a id="manualMove"></a> Step 1. Manually fail over by moving availability group resource

To manually fail over an AG resource named `ag_cluster` to cluster node named *nodeName2*, run the appropriate command for your distribution:

- **RHEL/Ubuntu example**

   ```bash
   sudo pcs resource move ag_cluster-master nodeName2 --master --lifetime=30S
   ```

- **SLES example**

   ```bash
   crm resource migrate ag_cluster nodeName2 --lifetime=30S
   ```

When you use the `--lifetime` option, the location constraint created to move the resource is temporary in nature and is valid for 30 seconds in previous example.

The temporary constraint isn't cleared automatically and might show up in the constraint list, but as an expired constraint. Expired constraints don't affect the failover behavior of pacemaker cluster. If you don't use the `--lifetime` option when moving the resource, you should remove a location constraint that is automatically added, which is noted in the following section.

#### <a id="removeLocConstraint"></a> Step 2. Remove the location constraint

During a manual failover, the `pcs` command `move` or `crm` command `migrate` adds a location constraint for the resource to be placed on the new target node. To see the new constraint, run the following command after manually moving the resource:

- **RHEL/Ubuntu example**

   ```bash
   sudo pcs constraint list --full
   ```

- **SLES example**

  ```bash
  crm config show
  ```

  This is an example of the constraint, which gets created because of a manual failover.

  `Enabled on: Node1 (score:INFINITY) (role: Master) (id:cli-prefer-ag_cluster-master)`

  > [!NOTE]  
  > The AG resource name in pacemaker clusters on Red Hat Enterprise Linux 8.x and Ubuntu 18.04 might resemble *ag_cluster-clone* as the nomenclature regarding resources has been evolving to use *promotable clone*.

- **RHEL/Ubuntu example**

  In the following command, `cli-prefer-ag_cluster-master` is the ID of the constraint that needs to be removed. `sudo pcs constraint list --full` returns this ID.

  ```bash
  sudo pcs resource clear ag_cluster-master
  ```

  Or

  ```bash
  sudo pcs constraint remove cli-prefer-ag_cluster-master
  ```

  Alternatively, you can perform both move and clearing of auto generated constraints in a single line as follows. The following example uses the *clone* terminology as per Red Hat Enterprise Linux 8.x.

  ```bash
  sudo pcs resource move ag_cluster-clone --master nodeName2 && sleep 30 && sudo pcs resource clear ag_cluster-clone
  ```

- **SLES example**

  In the following command, `cli-prefer-ms-ag_cluster` is the ID of the constraint. `crm config show` returns this ID.

  ```bash
  crm configure
  delete cli-prefer-ms-ag_cluster
  commit
  ```

> [!NOTE]  
> Automatic failover doesn't add a location constraint, so no cleanup is necessary.

For more information:

- [Red Hat - Managing cluster resources](https://docs.redhat.com/documentation/red_hat_enterprise_linux/9/html/configuring_and_managing_high_availability_clusters/assembly_managing-cluster-resources-configuring-and-managing-high-availability-clusters#proc_display-configured-resources-managing-cluster-resources)
- [Pacemaker - Move Resources manually](https://clusterlabs.org/pacemaker/doc/deprecated/en-US/Pacemaker/1.1/html/Clusters_from_Scratch/_move_resources_manually.html)
- [SLES Administration Guide - Managing cluster resources](https://documentation.suse.com/sle-ha/15-SP4/html/SLE-HA-all/cha-ha-manage-resources.html)

## <a id="forceFailover"></a> Force failover

A forced failover is intended strictly for disaster recovery. In this case, you can't fail over with cluster management tools because the primary datacenter is down. If you force failover to an unsynchronized secondary replica, some data loss is possible. Only force failover if you must restore service to the AG immediately and are willing to risk losing data.

If you can't use the cluster management tools for interacting with the cluster (for example, if the cluster is unresponsive due to a disaster event in the primary data center), you might have to force failover to bypass the external cluster manager. This procedure isn't recommended for regular operations because it risks data loss. Use it when the cluster management tools fail to execute the failover action. Functionally, this procedure is similar to [performing a forced manual failover](../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md) on an AG in Windows.

This process for forcing failover is specific to SQL Server on Linux.

1. Verify that the cluster no longer manages the AG resource.

   - Set the resource to unmanaged mode on the target cluster node. This command signals the resource agent to stop resource monitoring and management. For example:

     ```bash
     sudo pcs resource unmanage <resourceName>
     ```

   - If the attempt to set the resource mode to unmanaged mode fails, delete the resource. For example:

     ```bash
     sudo pcs resource delete <resourceName>
     ```

     > [!NOTE]  
     > When you delete a resource, it also deletes all of the associated constraints.

1. On the instance of SQL Server that hosts the secondary replica, set the session context variable `external_cluster`.

   ```sql
   EXEC sp_set_session_context @key = N'external_cluster', @value = N'yes';
   ```

1. Fail over the AG with Transact-SQL. In the following example, replace `<MyAg>` with the name of your AG. Connect to the instance of SQL Server that hosts the target secondary replica and run the following command:

   ```sql
   ALTER AVAILABILITY GROUP <MyAg> FORCE_FAILOVER_ALLOW_DATA_LOSS;
   ```

1. After a forced failover, bring the AG to a healthy state before either restarting the cluster resource monitoring and management or recreating the AG resource. Review the [Essential Tasks After a Forced Failover](../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md#FollowUp).

1. Either restart cluster resource monitoring and management:

   To restart the cluster resource monitoring and management, run the following command:

   ```bash
   sudo pcs resource manage <resourceName>
   sudo pcs resource cleanup <resourceName>
   ```

   If you deleted the cluster resource, recreate it. To recreate the cluster resource, follow the instructions at [Create availability group resource](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=rhel#create-availability-group-resource).

> [!IMPORTANT]  
> Don't use the preceding steps for disaster recovery drills because they risk data loss. Instead change the asynchronous replica to synchronous, and the instructions for [normal manual failover](#manualFailover).

## Database level monitoring and failover trigger

For `CLUSTER_TYPE=EXTERNAL`, the failover trigger semantics are different compared to WSFC. When the AG is on an instance of SQL Server in a WSFC, transitioning out of `ONLINE` state for the database causes the AG health to report a fault. In response, the cluster manager triggers a failover action. On Linux, the SQL Server instance can't communicate with the cluster. Monitoring for database health is done *outside-in*. If user opted in for database level failover monitoring and failover (by setting the option `DB_FAILOVER=ON` when creating the AG), the cluster checks if the database state is `ONLINE` every time it runs a monitoring action. The cluster queries the state in `sys.databases`. For any state different to `ONLINE`, it triggers a failover automatically (if automatic failover conditions are met). The actual time of the failover depends on the frequency of the monitoring action, and the database state being updated in `sys.databases.`

Automatic failover requires at least one synchronous replica.

## Related content

- [Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=rhel)
- [Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=sles)
- [Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=ubuntu)

[!INCLUDE [contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
