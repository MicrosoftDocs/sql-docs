---
title: "Upgrade a SQL Server Failover Cluster Instance | Microsoft Docs"
ms.custom: ""
ms.date: "10/01/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading failover clusters"
  - "clusters [SQL Server], upgrading"
  - "failover clustering [SQL Server], upgrading"
ms.assetid: daac41fe-7d0b-4f14-84c2-62952ad8cbfa
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Upgrade a SQL Server Failover Cluster Instance
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports upgrading a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster to a new version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], to a new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service pack or cumulative update, or when installing to a new Windows service pack or cumulative update  separately on all failover cluster nodes, with downtime limited to a single manual failover (or two manual failovers if failing back to the original primary).  
  
 Upgrading the Windows operating system of a failover cluster is not supported for operating systems before [!INCLUDE[winblue-server-2-md](../../../includes/winblue-server-2-md.md)]. To upgrade a cluster node running on [!INCLUDE[winblue-server-2-md](../../../includes/winblue-server-2-md.md)] or above, see [Perform a rolling upgrade or update](#perform-a-rolling-upgrade-or-update).  
  
 Support details are as follows:  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] upgrade is supported both through the user interface and from the command prompt. You can run upgrade from the command prompt on each failover cluster node, or by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup UI  to upgrade each cluster node.  For more information, see [Upgrade a SQL Server Failover Cluster Instance &#40;Setup&#41;](../../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance-setup.md) and [Install SQL Server from the Command Prompt](../../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md).  
  
-   The following scenarios are not supported as part of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] upgrade:  
  
    -   You cannot upgrade from a stand-alone instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to a failover cluster.  
  
    -   You cannot add features to a failover cluster. For example, you cannot add the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] to an existing [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]-only failover cluster.  
  
    -   You cannot downgrade a failover cluster node to a stand-alone instance.  
  
    -   Changing the edition of the failover cluster is limited to certain scenarios. For more information, see [Supported Version and Edition Upgrades](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md).  
  
-   During the failover cluster upgrade, downtime is limited to failover time and the time that is required for upgrade scripts to run. If you follow the failover cluster rolling upgrade process below and meet all prerequisites on all nodes before you begin the upgrade process, your downtime is minimal. Upgrading [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] when memory-optimized tables are in use will take some extra time. For more information, see [Plan and Test the Database Engine Upgrade Plan](../../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md).  
  
## Prerequisites  
 Before you begin, review the following important information:  
  
-   [Supported Version and Edition Upgrades](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md): Verify that you can upgrade to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] from your version of the Windows operating system and version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For example, you cannot upgrade directly from a SQL Server 2005 failover clustering instance to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] or upgrade a failover cluster running on [!INCLUDE[winxpsvr-md](../../../includes/winxpsvr-md.md)].  
  
-   [Choose a Database Engine Upgrade Method](../../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md): Select the appropriate upgrade method and steps based on your review of supported version and edition upgrades and also based on other components installed in your environment to upgrade components in the correct order.  
  
-   [Plan and Test the Database Engine Upgrade Plan](../../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md): Review the release notes and known upgrade issues, the pre-upgrade checklist, and develop and test the upgrade plan.  
  
-   [Hardware and Software Requirements for Installing SQL Server](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md):  Review the software requirements for installing [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. If additional software is required, install it on each node before you begin the upgrade process to minimize any downtime.  
  
## Perform a rolling upgrade or update  
 To upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup to upgrade each failover cluster node, one at a time, starting with the passive nodes. As you upgrade each node, it is left out of the possible owners of the failover cluster. If there is an unexpected failover, the upgraded nodes do not participate in the failover until cluster resource group ownership is moved to an upgraded node by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup.  
  
 By default, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup automatically determines when to fail over to an upgraded node. This depends on the total number of nodes in the failover cluster instance and the number of nodes that have already been upgraded. When half of the nodes or more have already been upgraded, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup causes a failover to an upgraded node when you perform upgrade on the next node. Upon failover to an upgraded node, the cluster group is moved to an upgraded node. All the upgraded nodes are put in the possible owners list and all the nodes that are not yet upgraded are removed from the possible owners list. As you upgrade each remaining node, it is added to the possible owners of the failover cluster.  
  
 This process results in downtime limited to one failover time and database upgrade script execution time during the whole failover cluster upgrade.  
  
 To control the failover behavior of cluster nodes during the upgrade process, run the upgrade operation at the command prompt and use the /FAILOVERCLUSTERROLLOWNERSHIP parameter. For more information, see [Install SQL Server from the Command Prompt](../../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md).  
  
## Next steps  
 [Upgrade SQL Server Using the Installation Wizard &#40;Setup&#41;](../../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)   
 [Install SQL Server from the Command Prompt](../../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)   
 [Upgrade a SQL Server Failover Cluster Instance &#40;Setup&#41;](../../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance-setup.md)  
  
  
