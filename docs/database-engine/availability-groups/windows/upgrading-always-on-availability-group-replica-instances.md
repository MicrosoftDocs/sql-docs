---
title: "Upgrading Always On Availability Group replica instances | Microsoft Docs"
ms.custom: ""
ms.date: "01/10/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: f670af56-dbcc-4309-9119-f919dcad8a65
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Upgrading Always On Availability Group Replica Instances
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

When upgrading a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance that hosts an Always On Availability Group (AG) to a new [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] version, to a new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service pack or cumulative update, or when installing to a new Windows service pack or cumulative update, you can reduce downtime for the primary replica to only a single manual failover by performing a rolling upgrade (or two manual failovers if failing back to the original primary). During the upgrade process, a secondary replica will not be available for failover or for read-only operations, and after the upgrade, it may take some time for the secondary replica to catch up with the primary replica node depending upon the volume of activity on the primary replica node (so expect high network traffic). Also be aware that after the initial failover to a secondary replica running a newer version of SQL Server, the databases in that Availability Group will run through an upgrade process to bring them to the latest version. During this time, there will be no readable replicas for any of these databases. Downtime after the initial failover will depend on the number of databases in the Availability Group. If you plan on failing back to the original primary, this step will not be repeated when you fail back.
  
>[!NOTE]  
>This article limits the discussion to the upgrade of SQL Server itself. It does not cover upgrading the operating system containing the Windows Server Failover Cluster (WSFC). Upgrading the Windows operating system hosting the failover cluster is not supported for operating systems before Windows Server 2012 R2. To upgrade a cluster node running on Windows Server 2012 R2, see [Cluster Operating System Rolling Upgrade](https://docs.microsoft.com/windows-server/failover-clustering/cluster-operating-system-rolling-upgrade).  
  
## Prerequisites  
Before you begin, review the following important information:  
  
- [Supported version and edition upgrades](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md): Verify that you can upgrade to SQL Server 2016 from your version of the Windows operating system and version of SQL Server. For example, you cannot upgrade directly from a SQL Server 2005 instance to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
- [Choose a database engine upgrade method](../../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md): To upgrade in the correct order, select the appropriate upgrade method and steps based on your review of supported version and edition upgrades and also based on other components installed in your environment.  
  
- [Plan and test the database engine upgrade plan](../../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md): Review the release notes and known upgrade issues, the pre-upgrade checklist, and develop and test the upgrade plan.  
  
- [Hardware and software requirements for installing SQL Server](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md):  Review the software requirements for installing [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. If additional software is required, install it on each node before you begin the upgrade process to minimize any downtime.  

- [Check if change data capture  or replication is used for any AG databases](#special-steps-for-change-data-capture-or-replication): If any databases in the AG are enabled for change data capture (CDC), complete these [instructions](#special-steps-for-change-data-capture-or-replication).

>[!NOTE]  
>Mixing versions of SQL Server instances in the same AG is not supported outside of a rolling upgrade, which upgrades the replicas in place. A higher version of a SQL Server instance cannot be added as a new replica to an existing AG. For example, a SQL Server 2017 replica cannot be added to an existing SQL Server 2016 AG. To migrate to a new version of the SQL Server instance using AGs, the only supported method is a distributed AG, which is in SQL Server 2016 Enterprise Edition or later.

## Rolling Upgrade Basics for Always On AGs  
Observe the following guidelines when performing server upgrades or updates in order to minimize downtime and data loss for your AGs:  
  
- Before starting the rolling upgrade,  
  
    - Perform a practice manual failover on at least one of your synchronous-commit replica instances  
  
    - Protect your data by performing a full database backup on every availability database  
  
    - Run DBCC CHECKDB on every availability database  
  
-   Always upgrade the remote secondary replica instances first, then local secondary replica instances next, and the primary replica instance last.  
  
-   Backups cannot occur on a database that is in the process of being upgraded.  Prior to upgrading the secondary replicas, configure the automated backup preference to run backups only on the primary replica.  During a version upgrade, no replicas are readable or available for backups. During a non-version upgrade, you can configure automated backups to run on secondary replicas prior to upgrading the primary replica.  
  
-   During a version upgrade, readable secondaries cannot be read after an upgrade of the readable secondary and before either the primary replica is failed over to an upgraded secondary or the primary replica is upgraded.  
  
-   To prevent the AG from unintended failovers during the upgrade process, remove availability failover from all synchronous-commit replicas before you begin.  
  
-   Do not upgrade the primary replica instance before failing over the AG to an upgraded instance with a secondary replica first. Otherwise, client applications may suffer extended downtime during the upgrade on the primary replica instance.  
  
-   Always fail over the AG to a synchronous-commit secondary replica instance. If you fail over to an asynchronous-commit secondary replica instance, the databases are vulnerable to data loss, and data movement is automatically suspended until you manually resume data movement.  
  
-   Do not upgrade the primary replica instance before upgrading or updating any other secondary replica instance. An upgraded primary replica can no longer ship logs to any secondary replica whose [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] instance that has not yet been upgraded to the same version. When data movement to a secondary replica is suspended, no automatic failover can occur for that replica, and your availability databases are vulnerable to data loss.  
  
-   Before failing over an AG, verify that the synchronization state of the failover target is SYNCHRONIZED.  

  > [!WARNING]
  > Installing a new instance or new version of SQL Server to a server that has an older version of SQL Server installed  may inadvertently **cause an outage for any availability group that is hosted by the older version of SQL Server.** This is because during the installation of the instance or version of SQL Server, the SQL Server high availability module (RHS.EXE) gets upgraded. This results in a temporary interruption of your existing availability groups in the primary role on the server. Therefore, it is highly recommended that you do one of the following when installing a newer version of SQL Server to a system already hosting an older version of SQL Server with an availability group:
  > - Install the new version of SQL Server during a maintenance window. 
  > - Failover the availability group to the secondary replica so it is not primary during the installation of the new SQL Server instance. 
  
## Rolling Upgrade Process  
 In practice, the exact process depends on factors such as the deployment topology of your AGs and the commit mode of each replica. But in the simplest scenario, a rolling upgrade is a multi-stage process that in its simplest form involves the following steps:  
  
 ![AG Upgrade in HADR Scenario](../../../database-engine/availability-groups/windows/media/alwaysonupgrade-ag-hadr.gif "AG Upgrade in HADR Scenario")  
  
1.  Remove automatic failover on all synchronous-commit replicas  
  
2.  Upgrade all asynchronous-commit secondary replica instances. 
  
3.  Upgrade all remote synchronous-commit secondary replica instances. 

4.  Upgrade all local synchronous-commit secondary replica instances. 
  
4.  Manually fail over the AG to a (newly upgraded) local synchronous-commit secondary replica.  
  
5.  Upgrade or update the local replica instance that formerly hosted the primary replica.  
  
6.  Configure automatic failover partners as desired.
  
 If necessary, you can perform an extra manual failover to return the AG to its original configuration.  
 
   > [!NOTE]
   > - Upgrading a synchronous-commit replica and taking it offline will not delay transactions on the primary. Once the secondary replica is disconnected, transactions are committed on the primary without waiting for logs to harden on the secondary replica. 
   > - If `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is set to either `1` or `2`, the Primary replica may be unavailabile for read/writes when a corresponding number of sync secondary replicas are not available during the update process. 
  
## AG with One Remote Secondary Replica  
 If you have deployed an AG only for disaster recovery, you may need to fail over the AG to an asynchronous-commit secondary replica. Such configuration is illustrated by the following figure:  
  
 ![AG Upgrade in DR Scenario](../../../database-engine/availability-groups/windows/media/agupgrade-ag-dr.gif "AG Upgrade in DR Scenario")  
  
 In this situation, you must fail over the AG to the asynchronous-commit secondary replica during the rolling upgrade. To prevent data loss, change the commit mode to synchronous commit and wait for the secondary replica to be synchronized before you fail over the AG. Therefore, the rolling upgrade process may look as follows:  
  
1.  Upgrade the secondary replica instance on the remote site  
  
2.  Change the commit mode to synchronous commit  
  
3.  Wait until synchronization state is SYNCHRONIZED  
  
4.  Fail over the AG to the secondary replica on the remote site  
  
5.  Upgrade or update the local (primary site) replica instance  
  
6.  Fail over the AG back to the primary site  
  
7.  Change the commit mode to asynchronous commit  
  
 Since the synchronous-commit mode is not a recommended setting for data synchronization to a remote site, client applications may notice an immediate increase in database latency after the setting change. Moreover, performing a failover causes all unacknowledged log messages to be discarded. The number discarded log messages can be significant due to the high network latency between the two sites, causing clients to experience a high volume of transactional failure. You can minimize impact to client applications by doing the following actions:  
  
-   Carefully select a maintenance window during low client traffic  
  
-   While upgrading or updating [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] on the primary site, change the availability mode back to asynchronous commit, then revert to synchronous commit when you are ready to fail over to the primary site again  
  
## AG with Failover Cluster Instance Nodes  
 If an AG contains failover cluster instance (FCI) nodes, you should upgrade the inactive nodes before you upgrade the active nodes. The following figure illustrates a common AG scenario with FCIs for local high availability and asynchronous commit between the FCIs for remote disaster recovery, and the upgrade sequence.  
  
 ![AG Upgrade with FCIs](../../../database-engine/availability-groups/windows/media/agupgrade-ag-fci-dr.gif "AG Upgrade with FCIs")  
  
1.  Upgrade or update REMOTE2  
  
2.  Fail over FCI2 to REMOTE2  
  
3.  Upgrade or update REMOTE1  
  
4.  Upgrade or update PRIMARY2  
  
5.  Fail over FCI1 to PRIMARY2  
  
6.  Upgrade or update PRIMARY1  
  
## Upgrade or Update SQL Server Instances with Multiple AGs  
 If you are running multiple AGs with primary replicas on separate server nodes (an Active/Active configuration), the upgrade path involves more failover steps to preserve high availability in the process. Suppose you are running three AGs on three server nodes with all replicas in synchronous commit mode as shown in the following table:  
  
|AG|Node1|Node2|Node3|  
|------------------------|-----------|-----------|-----------|  
|AG1|Primary|||  
|AG2||Primary||  
|AG3|||Primary|  
  
 It may be appropriate in your situation to perform a load-balanced rolling upgrade in the following sequence:  
  
1.  Fail over AG2 to Node3 (to free up Node2)  
  
2.  Upgrade or update Node2  
  
3.  Fail over AG1 to Node2 (to free up Node1)  
  
4.  Upgrade or update Node1  
  
5.  Fail over both AG2 and AG3 to Node1 (to free up Node3)  
  
6.  Upgrade or update Node3  
  
7.  Fail over AG3 to Node3  
  
 This upgrade sequence has an average downtime of fewer than two failovers per AG. The resulting configuration is shown in the following table.  
  
|AG|Node1|Node2|Node3|  
|------------------------|-----------|-----------|-----------|  
|AG1||Primary||  
|AG2|Primary|||  
|AG3|||Primary|  
  
 Based on your specific implementation, your upgrade path may vary, and the downtime that client applications experience may vary as well.  
  
> [!NOTE]  
>  In many cases, after the rolling upgrade is completed, you will fail back to the original primary replica. 

## Rolling upgrade of a distributed Availability Group
To perform a rolling upgrade of a distributed availability group, first upgrade all of the secondary replicas. Next, failover the forwarder, and upgrade the last remaining instance of the second availability group. Once all other replicas have been upgraded, failover the global primary, and upgrade the last remaining instance of the first availability group. A detailed diagram with steps is provided below. 

 Based on your specific implementation, your upgrade path may vary, and the downtime that client applications experience may vary as well.  
  
> [!NOTE]  
>  In many cases, after the rolling upgrade is completed, you will fail back to the original primary replicas. 

### General steps to upgrade a distributed availability group
1. Backup all databases, including system databases, and those participating in the availability group. 
2. Upgrade and restart all secondary replicas of the second availability group (the downstream). 
3. Upgrade and restart all secondary replicas of the first availability group (the upstream). 
4. Fail over the forwarder primary to an upgraded secondary replica of the secondary availability group.
5. Wait for data synchronization. The databases should show as synchronized on all synchronous-commit replicas, and the global primary should be synchronized with the forwarder.  
6. Upgrade and restart the last remaining instance of the secondary availability group. 
7. Fail over the global primary to an upgraded secondary of the first availability group.  
8. Upgrade the last remaining instance of the primary availability group.
9. Restart the newly upgraded server. 
10. (optional) Fail both availability groups back to their original primary replicas.  

>[!IMPORTANT]
>- Verify synchronization between every step. Before proceeding to the next step, confirm that your synchronous-commit replicas are synchronized within the availability group, and that your global primary is synchronized with the forwarder in the distributed AG. 
>- **Recommendation**: Every time you verify synchronization, refresh both the database node and the distributed AG node in SQL Server Management Studio. After everything is synchronized, save a screenshot of the states of each replica. This will help you keep track of what step you're on, provide evidence that everything was working correctly before the next step, and assist you with troubleshooting if anything goes wrong. 


### Diagram example for a rolling upgrade of a distributed availability group

| Availability group | Primary replica | Secondary Replica|
| :------ | :----------------------------- |  :------ |
| AG1 | NODE1\SQLAG | NODE2\SQLAG|
| AG2 | NODE3\SQLAG | NODE4\SQLAG|
| Distributedag| AG1 (global) | AG2 (forwarder) |
| &nbsp; | &nbsp; | &nbsp; |

![Example diagram for distributed AG](media/upgrading-always-on-availability-group-replica-instances/rolling-upgrade-dag-diagram.png)


The steps to upgrade the instances in this diagram: 

1. Backup all databases, including system databases, and those participating in the availability group. 
2. Upgrade NODE4\SQLAG (secondary of AG2) and restart the server. 
3. Upgrade NODE2\SQLAG (secondary of AG1) and restart the server. 
4. Fail AG2 over from NODE3\SQLAG to NODE4\SQLAG. 
5. Upgrade NODE3\SQLAG and restart the server. 
6. Fail AG1 over from NODE1\SQLAG to NODE2\SQLAG. 
7. Upgrade NODE1\SQLAG and restart the server. 
8. (optional) Fail back to the original primary replicas.
    1. Fail AG2 over from NODE4\SQLAG back to NODE3\SQLAG.  
    2. Fail AG1 over from NODE2\SQLAG back to NODE1\SQLAG. 

If a third replica existed in each availability group, it would be upgraded before NODE3\SQLAG and NODE1\SQLAG. 

>[!IMPORTANT]
>- Verify synchronization between every step. Before proceeding to the next step, confirm that your synchronous-commit replicas are synchronized within the availability group, and that your global primary is synchronized with the forwarder in the distributed AG. 
>- Recommendation: Every time you verify synchronization, refresh both the database node and the Distributed AG node in SQL Server Management Studio. If After everything is synchronized, then take a screenshot and save it. This will help you keep track of what step you're on, provide evidence that everything was working correctly before the next step, and assist you with troubleshooting if anything goes wrong. 


## Special steps for change data capture or replication

Depending on the update being applied, additional steps may be required for AG replica databases that are enabled for change data capture or replication. Refer to the release notes for the update to determine if the following steps are required:

1. Upgrade each secondary replica.

1. After all secondary replicas have been upgraded, fail over the AG to an upgraded instance. 

1. Run the following Transact-SQL on the instance that hosts the primary replica:

   ```sql
   EXECUTE [master].[sys].[sp_vupgrade_replication];
   ```

   >[!NOTE]
   >This command may take several minutes to run. 

1. Upgrade the instance that was originally the primary replica.

For background information, see [CDC functionality may break after upgrading to the latest CU](https://blogs.msdn.microsoft.com/sql_server_team/cdc-functionality-may-break-after-upgrading-to-the-latest-cu-for-sql-server-2012-2014-and-2016/).

  
## See Also  
 [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)   

 [Install SQL Server 2016 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)  
  
  
