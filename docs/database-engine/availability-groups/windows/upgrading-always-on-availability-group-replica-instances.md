---
title: "Upgrading Always On Availability Group Replica Instances | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: f670af56-dbcc-4309-9119-f919dcad8a65
caps.latest.revision: 14
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Upgrading Always On Availability Group Replica Instances
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  When upgrading a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Always On Availability Group to a new [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] version, to a new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]service pack or cumulative update, or when installing to a new Windows service pack or cumulative update, you can reduce downtime for the primary replica to only a single manual failover by performing a rolling upgrade (or two manual failovers if failing back to the original primary). During the upgrade process, a secondary replica will not be available for failover or for read-only operations, and, after the upgrade, it may take some time for the secondary replica to catch up with the primary replica node depending upon the volume of activity on the primary replica node (so expect high network traffic).  
  
> [!NOTE]  
>  This topic limits the discussion to the upgrade of SQL Server itself. It does not cover upgrading the operating system containing the Windows Server Failover Clusting (WSFC) cluster. Upgrading the Windows operating system hosting the failover cluster is not supported for operating systems before Windows Server 2012 R2. To upgrade a cluster node running on Windows Server 2012 R2, see [Cluster Operating System Rolling Upgrade](https://technet.microsoft.com/library/dn850430.aspx)  
  
## Prerequisites  
 Before you begin, review the following important information:  
  
-   [Supported Version and Edition Upgrades](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md): Verify that you can upgrade to SQL Server 2016 from your version of the Windows operating system and version of SQL Server. For example, you cannot upgrade directly from a SQL Server 2005 instance to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
-   [Choose a Database Engine Upgrade Method](../../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md): Select the appropriate upgrade method and steps based on your review of supported version and edition upgrades and also based on other components installed in your environment to upgrade components in the correct order.  
  
-   [Plan and Test the Database Engine Upgrade Plan](../../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md): Review the release notes and known upgrade issues, the pre-upgrade checklist, and develop and test the upgrade plan.  
  
-   [Hardware and Software Requirements for Installing SQL Server 2016](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md):  Review the software requirements for installing [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. If additional software is required, install it on each node before you begin the upgrade process to minimize any downtime.  
  
## Rolling Upgrade Best Practices for Always On Availability Groups  
 The following best practices should be observed when performing server upgrades or updates in order to minimize downtime and data loss for your availability groups:  
  
-   Before starting the rolling upgrade,  
  
    -   Perform a practice manual failover on at least one of your synchronous-commit replica instances  
  
    -   Protect your data by performing a full database backup on every availability database  
  
    -   Run DBCC CHECKDB on every availability database  
  
-   Always upgrade the remote secondary replica instances first, then local secondary replica instances next, and the primary replica instance last.  
  
-   Backups cannot occur on a database that is in the process of being upgraded.  Prior to upgrading the secondary replicas, configure the automated backup preference to run backups only on the primary replica.  During a version upgrade, no replicas will be readable or available for backups. During a non-version upgrade, you can configure automated backups to run on secondary replicas prior to upgrading the primary replica.  
  
-   During a version upgrade, readable secondaries cannot be read after an upgrade of the readable secondary and before either the primary replica is failed over to an upgraded secondary or the primary replica is upgraded.  
  
-   To prevent the availability group from unintended failovers during the upgrade process, remove availability failover from all synchronous-commit replicas before you begin.  
  
-   Do not upgrade the primary replica instance before failing over the availability group to an upgraded instance with a secondary replica first. Otherwise, client applications may suffer extended downtime during the upgrade on the primary replica instance.  
  
-   Always fail over the availability group to a synchronous-commit secondary replica instance. If you fail over to an asynchronous-commit secondary replica instance, the databases will suffer data loss, and data movement is automatically suspended until you manually resume data movement.  
  
-   Do not upgrade the primary replica instance before upgrading or updating any other secondary replica instance. An upgraded primary replica can no longer ship logs to any secondary replica whose [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] instance that has not yet been upgraded to the same version. When data movement to a secondary replica is suspended, no automatic failover can occur for that replica, and your availability databases are vulnerable to data loss.  
  
-   Before failing over an availability group, verify that the synchronization state of the failover target is SYNCHRONIZED.  
  
## Rolling Upgrade Process  
 In practice, the exact process will depend on factors such as the deployment topology of your availability groups and the commit mode of each replica. But in the simplest scenario, a rolling upgrade is a multi-stage process that in its simplest form involves the following steps:  
  
 ![Availability Group Upgrade in HADR Scenario](../../../database-engine/availability-groups/windows/media/alwaysonupgrade-ag-hadr.gif "Availability Group Upgrade in HADR Scenario")  
  
1.  Remove automatic failover on all synchronous-commit replicas  
  
2.  Upgrade all remote secondary replica instances running asynchronous-commit secondary replicas  
  
3.  Upgrade the all local replica secondary instances that are not currently running the primary replica  
  
4.  Manually fail over the availability group to a local synchronous-commit secondary replica  
  
5.  Upgrade or update the local replica instance that formerly hosted the primary replica  
  
6.  Configure automatic failover partners as desired  
  
 If necessary, you can perform an extra manual failover to return the availability group to its original configuration.  
  
## Availability Group with One Remote Secondary Replica  
 If you have deployed an availability group only for disaster recovery, you may need to fail over the availability group to an asynchronous-commit secondary replica. Such configuration is illustrated by the following figure:  
  
 ![Availability Group Upgrade in DR Scenario](../../../database-engine/availability-groups/windows/media/agupgrade-ag-dr.gif "Availability Group Upgrade in DR Scenario")  
  
 In this situation, you must fail over the availability group to the asynchronous-commit secondary replica during the rolling upgrade. To prevent data loss, change the commit mode to synchronous commit and wait for the secondary replica to be synchronized before you fail over the availability group. Therefore, the rolling upgrade process may look as follows:  
  
1.  Upgrade the secondary replica instance on the remote site  
  
2.  Change the commit mode to synchronous commit  
  
3.  Wait until synchronization state is SYNCHRONIZED  
  
4.  Fail over the availability group to the secondary replica on the remote site  
  
5.  Upgrade or update the local (primary site) replica instance  
  
6.  Fail over the availability group back to the primary site  
  
7.  Change the commit mode to asynchronous commit  
  
 Since the synchronous-commit mode is not a recommended setting for data synchronization to a remote site, client applications may notice an immediate increase in database latency after the setting change. Moreover, performing a failover will cause all unacknowledged log messages to be discarded. The amount of discarded log messages can be quite large due to the high network latency between the two sites, causing clients to experience a high volume of transactional failure. You can minimize impact to client applications by doing the following:  
  
-   Carefully select a maintenance window during low client traffic  
  
-   While upgrading or updating [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] on the primary site, change the availability mode back to asynchronous commit, then revert to synchronous commit when you are ready to fail over to the primary site again  
  
## Availability Group with Failover Cluster Instance Nodes  
 If an availability group contains failover cluster instance (FCI) nodes, you should upgrade the inactive nodes before you upgrade the active nodes. The figure below illustrates a common availability group scenario with FCIs for local high availability and asynchronous commit between the FCIs for remote disaster recovery, and the upgrade sequence.  
  
 ![Availability Group Upgrade with FCIs](../../../database-engine/availability-groups/windows/media/agupgrade-ag-fci-dr.gif "Availability Group Upgrade with FCIs")  
  
1.  Upgrade or update REMOTE2  
  
2.  Fail over FCI2 to REMOTE2  
  
3.  Upgrade or update REMOTE1  
  
4.  Upgrade or update PRIMARY2  
  
5.  Fail over FCI1 to PRIMARY2  
  
6.  Upgrade or update PRIMARY1  
  
## Upgrade Update SQL Server Instances with Multiple Availability Groups  
 If you are running multiple availability groups with primary replicas on separate server nodes (an Active/Active configuration), the upgrade path involves more failover steps to preserve high availability in the process. Suppose you are running three availability groups on three server nodes as shown in the following table, and all secondary replicas are running in synchronous-commit mode.  
  
|Availability Group|Node1|Node2|Node3|  
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
  
 This upgrade sequence has an average downtime of less than two failovers per availability group. The resulting configuration is shown in the table below.  
  
|Availability Group|Node1|Node2|Node3|  
|------------------------|-----------|-----------|-----------|  
|AG1||Primary||  
|AG2|Primary|||  
|AG3|||Primary|  
  
 Based on your specific implementation, your upgrade path may vary, and the downtime that client applications experience may vary as well.  
  
> [!NOTE]  
>  In many cases, after the rolling upgrade is completed, you will failback to the original primary replica.  
  
## See Also  
 [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)   
 [Install SQL Server 2016 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)  
  
  
