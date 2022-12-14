---
title: "Upgrade SQL Server instances: Windows Server 2012 & older clusters"
description: Describes how to upgrade your SQL Server failover cluster instances running on Windows Server 2008, Windows Server 2008 R2, and Windows Server 2012.
ms.date: "06/15/2020"
ms.custom:
  - seo-lt-2019
  - intro-migration
ms.service: sql
ms.subservice: failover-cluster-instance
ms.topic: how-to
helpviewer_keywords:
  - "upgrading failover clusters"
  - "clusters [SQL Server], upgrading"
  - "failover clustering [SQL Server], upgrading"
author: MashaMSFT
ms.author: mathoma
---

# Upgrade SQL Server instances running on Windows Server 2008/2008 R2/2012 clusters

[!INCLUDE[winserver2008-md](../../../includes/winserver2008-md.md)], [!INCLUDE[winserver2008r2-md](../../../includes/winserver2008r2-md.md)], and [!INCLUDE[winserver2012-md](../../../includes/winserver2012-md.md)] prevent Windows Server failover clusters from performing in-place operating system upgrades, capping the allowed version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] for a cluster. Once the cluster is upgraded to at least [!INCLUDE[winserver2012r2-md](../../../includes/winserver2012r2-md.md)], the cluster can remain up-to-date.

## Prerequisites

-   Prior to performing any of the migration strategies, a parallel Windows Server failover cluster with Windows Server 2016/2012 R2 must be prepared. All nodes comprising [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instances (FCIs) must be joined to the Windows cluster with the parallel FCIs installed. Any standalone machines **must not** be joined to the Windows Server failover cluster prior to migration. User databases should be synchronized on the new environment prior to migration.
-   All destination instances must be running the same version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] as their parallel instance in the original environment, with the same instance names and IDs, and installed with the same features. Installation paths and directory structure should be identical on the destination machines. This does not include FCI virtual network names, which must be different prior to migration. Any features enabled by the original instance (Always On, FILESTREAM, etc.) should be enabled on the destination instance.

-   The parallel cluster should have no [!INCLUDE[sshadr-md](../../../includes/sshadr-md.md)] installed prior to migration.

-   Downtime when migrating a cluster that uses strictly Availability Groups (with or without SQL FCIs) can be greatly limited by using Distributed Availability Groups, but this does require that all instances run [!INCLUDE[sssql15-md](../../../includes/sssql16-md.md)] RTM (or above) versions.

-   All migration strategies require [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] sysadmin role. All Windows users used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] services (i.e. Windows account running replication agents) must have the OS level permissions on each machine in the new environment.

-   Any network file shares and network mapped drives used in the original [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cluster environment must still exist and remain accessible by the target cluster with the same permissions as the original instances.

-   Any TCP/IP ports listened to by the original [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance must be unused and allow inbound traffic on the target machine.
-   All SQL-related services should be installed and run by the same Windows user.

-   The destination instances must be installed with the same locale as the original instances.

## Migration scenarios

The proper migration strategy depends on certain parameters of the original [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cluster topology, namely the usage of [!INCLUDE[sshadr-md](../../../includes/sshadr-md.md)] and SQL failover cluster instances. The chosen strategy also depends on the requirements of the destination environment. If the new environment requires that each machine or SQL FCI maintains the original virtual network name (VNN), or if the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] topology depends on the new instances inheriting all the system objects of the original instances, we must choose a strategy that migrates these.

|                                   | Requires all server objects and VNNS | Requires all server objects and VNNS | Does not require server objects/VNNS\* | Does not require server objects/VNNS\* |
|-----------------------------------|--------------------------------------|--------------------------------------------------------------------|------------|------------|
| **_Availability Groups? (Y/N)_**                  | **_Y_**                              | **_N_**                                                            | **_Y_**    | **_N_**    |
| **Cluster uses SQL FCI only**         | [Scenario 3](#scenario-3-windows-cluster-has-both-sql-fcis-and-sql-server-availability-groups)                           | [Scenario 2](#scenario-2-windows-clusters-with-sql-server-failover-cluster-instances-fcis)                                                        | [Scenario 1](#scenario-1-windows-cluster-with-sql-server-availability-groups-and-no-failover-cluster-instances-fcis) | [Scenario 2](#scenario-2-windows-clusters-with-sql-server-failover-cluster-instances-fcis) |
| **Cluster uses standalone instances** | [Scenario 5](#scenario-5-windows-cluster-with-standalone-sql-server-instances-and-availability-groups)                           | [Scenario 4](#scenario-4-windows-cluster-with-standalone-sql-server-instances-and-no-availability-groups)                                                         | [Scenario 1](#scenario-1-windows-cluster-with-sql-server-availability-groups-and-no-failover-cluster-instances-fcis) | [Scenario 4](#scenario-4-windows-cluster-with-standalone-sql-server-instances-and-no-availability-groups) |

\* Excluding Availability Group listener names

## Scenario 1: Windows Cluster with SQL Server Availability Groups and no Failover Cluster Instances (FCIs)
If you have a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup that uses Availability Groups (AGs) and no failover cluster instances, you can migrate to a new cluster by creating a parallel [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] deployment on a different Windows Cluster with Windows Server 2016/2012 R2. After this, you can create a distributed AG where the target cluster is the secondary to the current production cluster. This does require that the user upgrade to [!INCLUDE[sssql15-md](../../../includes/sssql16-md.md)] or above.

###  To perform the upgrade

1.  If needed, upgrade all instances to [!INCLUDE[sssql15-md](../../../includes/sssql16-md.md)] or above. Parallel instances should be running the same version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

2.  Create an Availability Group for the target cluster. If the primary node of the target cluster is not an FCI, create a listener.

3.  Form a distributed availability group where the target cluster is the secondary Availability Group.

    >[!NOTE]
    >The LISTENER\_URL parameter for the create distributed AG T-SQL behaves differently for AGs with SQL FCI as the primary instance. If this is the case for either the primary or the secondary AG, use the VNN of the primary SQL FCI as the listener URL in place of the listener's network name, along with the database mirroring endpoint port.

4.  Join the secondary Availability Group to the distributed AG.

5.  Join the secondary databases on the secondary AG.

    >[!NOTE]
    >This will be done automatically if the target Availability Group uses automatic seeding; this is only possible if the data and log paths are the same across all replicas.

6.  Cut off all traffic to the primary AG and allow the secondary to sync.

7.  Change the commit policy on both Availability Groups to SYNCHRONOUS_COMMIT and failover to the target cluster once status is SYNCHRONIZED.

8.  Delete the distributed AG.

9.  Delete or rename the listener on the original AG.

10. Rename or create the new AG's listener with the name of the original AG's listener name.

    >[!NOTE]
    >While the DNS record for the original AG listener exists, attempts to create a listener using this name will fail.

11. Resume traffic towards the listener.

## Scenario 2: Windows Clusters with SQL Server Failover Cluster Instances (FCIs)

If you have a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] environment with only SQL FCI instances, you can migrate to a new cluster by creating a parallel [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] environment on a different Windows Cluster with Windows Server 2016/2012 R2. You will migrate to the target cluster by "stealing" the VNNs of the old SQL FCIs and acquiring them on the new clusters. This will create additional downtime dependent on DNS propagation times.

###  To perform the upgrade

1.  Take a full backup and stop traffic towards the original [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cluster.

2.  Take a tail log backup of user databases and restore with recovery on the new environment.

3.  On the target cluster in Failover Cluster Manager, bring down each SQL FCI clustered role.

4.  Still in Failover Cluster Manager on the target cluster, bring back up clustered disks used by each SQL FCI.

5.  On the original cluster in Failover Cluster Manager, bring down each SQL FCI clustered role.

6.  Still in Failover Cluster Manager on the original cluster, bring back up the clustered disks used by each SQL FCI.

7.  Copy the system databases from the original machines to its parallel target machine.

8.  In the original environment in Failover Cluster Manager, change the name of the 'Server Name' resource of each [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI role.

9.  Now bring just the renamed Server Name resource back online for each of the SQL FCI roles.

10. Now on the target cluster in Failover Cluster Manager, rename each of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI role's "Server Name" resource to the name previously held by the original cluster.

    >[!NOTE]
    >Errors arising from the name already being held by another machine will stop once the DNS records for the name are deleted.

11. Once all the FCIs have been renamed, restart each of the machines in the new cluster.

12. As the machines come back online following the restart, start each of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI roles in Failover Cluster Manager.

## Scenario 3: Windows Cluster has both SQL FCIs and SQL Server Availability Groups

If you have a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup that uses no standalone [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instances, only SQL FCIs, which are contained in at least one Availability Group, you can migrate this to a new cluster using methods similar to the "no Availability Group, no standalone instance" scenario. Prior to copying system tables to the target FCI shared disks, you must drop all Availability Groups in the original environment. After all databases have been migrated to the target machines, you will recreate the Availability Groups with the same schema and listener names. By doing this, the Windows Server failover cluster resources will be correctly formed and managed on the target cluster. **Always On must be enabled in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager on each machine in the target environment prior to migration.**

### To perform the upgrade

1.  Stop traffic towards [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

2.  Take a tail log backup of user databases and restore with recovery on the new environment's intended primary, and with NORECOVERY on all intended secondaries.

3.  On the target cluster in Failover Cluster Manager, bring down each SQL FCI clustered role.

4.  Still in Failover Cluster Manager on the target cluster, bring back up the clustered disks used by each SQL FCI.

5.  On the original cluster, delete the Availability Group.

6.  On the original cluster in Failover Cluster Manager, bring down each SQL FCI clustered role.

7.  Still in Failover Cluster Manager on the original cluster, bring back up the clustered disks used by each SQL FCI.

8.  Copy the system databases from the original machines to its parallel target machine.

9.  In the original environment in Failover Cluster Manager, change the name of the 'Server Name' resource of each [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI role.

10. Now bring just the renamed Server Name resource back online for each of the SQL FCI roles.

11. Now on the target cluster in Failover Cluster Manager, rename each of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI role's "Server Name" resource to the name previously held by the original cluster.

12. Once all the FCIs have been renamed, restart each of the machines in the new cluster.

13. As the machines come back online following the restart, start each of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI roles in Failover Cluster Manager.

14. Once all instances are online, form the Availability Group on the replica where the databases were restored with recovery.

15. Join all secondary replicas to the AG, join all secondary databases to the AG.

16. Create a listener in the new AG with the listener name of the original Availability Group's listener.

## Scenario 4: Windows Cluster with Standalone SQL Server Instances and no Availability Groups

Migrating a cluster with standalone instances is similar in process to migrating a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cluster with only FCIs, but rather than changing the VNN of the FCI's network name cluster resource, you change the machine name of the original standalone machine, and "steal" the old machine's name on the target machine. This does introduce additional downtime relative to the no standalone scenarios, as you cannot join the target standalone machine to the WSFC until you have acquired the old machine's network name.

###  To perform the upgrade

1.  Stop traffic towards [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

2.  Take a tail log backup of user databases and restore with recovery on the new environment on each machine.

3.  On the target cluster in Failover Cluster Manager, bring down each SQL FCI clustered role.

4.  Still on the target cluster in Failover Cluster Manager, bring back up clustered disks used by each SQL FCI.

5.  On the original cluster in Failover Cluster Manager, bring down each SQL FCI clustered role, and stop the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] standalone instances in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.

6.  For each standalone machine in the original cluster, rename each machine to a new unique machine name. Restart each of these machines as directed.

7.  On the original cluster in Failover Cluster Manager, bring back up the clustered disks used by each SQL FCI.

8.  Copy the system databases to the target machines.

9.  In the original environment in Failover Cluster Manager, change the 'Server Name' resource of each [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI role to a new, unique name.

10. Now bring just the renamed Server Name resource back online for each of the SQL FCI roles.

11. Now on the parallel standalone instances, rename the machines to the original standalone machine names. (Drop old server name, add new server name with local param.) Restart the machines as instructed.

12. Following the restart, join each of the standalone machines to the target Windows Server Failover Cluster.

13. Now on the target cluster in Failover Cluster Manager, rename each of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI role's "Server Name" resource to the name previously held by the original cluster.

14. Once all the FCIs have been renamed, restart each of the machines in the new cluster.

15. As the machines come back online following the restart, start each of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI roles in Failover Cluster Manager.

## Scenario 5: Windows Cluster with Standalone SQL Server Instances and Availability Groups

Migrating a cluster that uses Availability Groups with standalone replicas is similar in process to migrating a cluster with FCIs using Availability Groups. You still must delete the original Availability Groups and reconstruct them on the target cluster; however, additional downtime is introduced because of the additional costs in migrating standalone instances. **Always On must be enabled on each FCI in the target environment prior to migration.**

###  To perform the upgrade

1.  Stop traffic towards [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

2.  Take a tail log backup of user databases and restore with recovery on the new environment on the intended primary, and with NORECOVERY on each intended secondary.

3.  Delete the Availability Group on the original cluster.

4.  On the target cluster in Failover Cluster Manager, bring down each SQL FCI clustered role.

5.  Still on the target cluster in Failover Cluster Manager, bring back up clustered disks used by each SQL FCI.

6.  On the original cluster in Failover Cluster Manager, bring down each SQL FCI clustered role, and stop the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] standalone instances in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.

7.  For each standalone machine in the original cluster, rename each machine to a new unique machine name. Restart each of these machines as directed.

8.  On the original cluster in Failover Cluster Manager, bring back up the clustered disks used by each SQL FCI.

9.  Copy the system databases to target machines.

10. In the original environment in Failover Cluster Manager, change the 'Server Name' resource of each [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI role to a new, unique name.

11. Now bring just the renamed Server Name resource back online for each of the SQL FCI roles.

12. Now on the parallel standalone instances, rename the machines to the original standalone machine names. (Drop and add server name in SQL.) Restart the machines as instructed.

13. Following the restart, join each of the standalone machines to the target Windows Server failover cluster.

14. Now on the target cluster in Failover Cluster Manager, rename each of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI role's "Server Name" resource to the name previously held by the original cluster.

15. Once all the FCIs have been renamed, restart each of the machines in the new cluster.

16. As the machines come back online following the restart, start each of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI roles in Failover Cluster Manager.

17. Once all instances are online, recreate the Availability Group on the intended primary.

18. Join each Secondary replica and secondary database.

19. Recreate the Availability Group listener with the same name as the original.

## Specific concerns for individual features

### [!INCLUDE[sshadr-md](../../../includes/sshadr-md.md)]

-   **Database mirroring endpoint**

    From a SQL perspective, the database mirroring endpoint will migrate to the new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance along with the system tables. Prior to migration, ensure that the appropriate rules are applied in firewalls and that no other process is listening on the same port.

-   **Availability Groups**

    Availability Groups and their listeners cannot migrate between instances. The Windows Server failover cluster resources created by the Availability Group cannot be easily recreated on the target environment. Rather than attempting to migrate Availability Groups, we look to drop and recreate the Availability Groups on the target cluster.

-   **Availability Group listeners**

    Like Availability Groups themselves, we drop and recreate listeners as opposed to migrating them directly.

### Replication

-   **Remote distributors, publishers, subscribers**

    The relationship between a distributor and publisher relies only on the VNN of the machines hosting the two, which will properly resolve to the new machine. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent jobs will also properly migrate with the system tables, so the various replication agents will be able to continue execution as usual. We do require prior to migration that any Windows accounts running the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent itself or any [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent job have the same permissions in the target environment. Communication with both the publisher and the subscribers will execute as usual.

-   **Snapshot folder**

    Prior to migration we require that any network shares used by any [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features be accessible by machines in the target environment with the same permissions as the original environment. You will have to ensure this is true prior to migration.

### Service broker

-   **Service broker endpoint**

    From a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] perspective, there are no concerns with the endpoint. Prior to migration, you will have to ensure that no process is already listening on the same port and that no firewall rule is blocking that port, or that there is a firewall rule specifically allowing the port.

-   **Certificates**

    Certificates should also be backed up and restored to the target machines in the event that the certificate needs to be restored to a new machine.

-   **Routes**

    Routes depend on the virtual network name of the target, which for both machine names and SQL FCI network names will properly resolve to the correct machines in the new environment. Any other VNN referenced must also be redirected to the new machine.

-   **Remote service bindings**

    Remote service bindings will function as intended after migration, as any user using the remote service binding will properly migrate.

### [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent

-   **Jobs**

    Jobs will be properly migrated along with the system databases. Any user running either a SQL Agent job or the SQL Agent itself will have the same permissions on the target machine as specified in the prerequisites.

-   **Alerts and operators**

    Alerts and operators will be properly migrated with the system databases.

### FILESTREAM

-   **Windows file-sharing ports**

    Windows file-sharing ports 139 and 445 both must have rules allowing inbound traffic to use FILESTREAM

-   **Windows share**

    The Windows share path depends upon the SQL FCI name resource, as it is accessed by `\\ServerName\ShareName`. Migrating FILESTREAM requires that all nodes in a target FCI enable FILESTREAM, and if a Windows share is used, are configured to use the same name for the Windows share as the original machine. Once the Target FCI obtains the correct server name, it will host the Windows share using the desired path.

-   **FILESTREAM data**

    FILESTREAM data is included in the backup.

### Integration Services

-   **SSIS projects**

    SSIS projects migrate along with the SSIS database. Once the SSIS database is moved, packages are immediately executable prior to moving any system tables.

-   **File-based data sources**

    Flat files, Excel files, XML sources, and others must be accessible at the same location specified by the SSIS package.

## Next steps
- [Complete the Database Engine Upgrade](../../../database-engine/install-windows/complete-the-database-engine-upgrade.md)
- [Change the Database Compatibility Mode and Use the Query Store](../../../database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store.md)
- [Take Advantage of New SQL Server 2016 Features](../../what-s-new-in-sql-server-2017.md)
- [Upgrade a SQL Server Failover Cluster Instance](upgrade-a-sql-server-failover-cluster-instance.md)
- [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)
- [Add Features to an Instance of SQL Server 2016 (Setup)](../../../database-engine/install-windows/add-features-to-an-instance-of-sql-server-setup.md)
