---
title: "Combine a failover cluster instance with availability groups"
description: "Enhance your high availability and disaster recoverability by combining the features of a SQL Server failover cluster instance and an Always On availability group."
ms.custom: "seodec18"
ms.date: "07/02/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "clustering [SQL Server]"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "Failover Cluster Instances [SQL Server], see failover clustering [SQL Server]"
  - "quorum [SQL Server]"
  - "failover clustering [SQL Server], AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], Failover Cluster Instances"
ms.assetid: 613bfbf1-9958-477b-a6be-c6d4f18785c3
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---
# Failover Clustering and Always On Availability Groups (SQL Server)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

   [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], the high availability and disaster recovery solution introduced in [!INCLUDE[sssql11](../../../includes/sssql11-md.md)], requires Windows Server Failover Clustering (WSFC). Also, though [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] is not dependent upon [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Clustering, you can use a failover clustering instance (FCI) to host an availability replica for an availability group. It is important to know the role of each clustering technology, and to know what considerations are necessary as you design your [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] environment.  
  
> [!NOTE]  
>  For information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] concepts, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
  
##  <a name="WSFC"></a> Windows Server Failover Clustering and Availability Groups  
 Deploying [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] requires a Windows Server Failover Cluster (WSFC). To be enabled for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] must reside on a WSFC node, and the WSFC and node must be online. Furthermore, each availability replica of a given availability group must reside on a different node of the same WSFC. The only exception is that while being migrated to another WSFC, an availability group can temporarily straddle two clusters.  
  
 [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] relies on the Windows Server Failover Cluste(WSFC) to monitor and manage the current roles of the availability replicas that belong to a given availability group and to determine how a failover event affects the availability replicas. A WSFC resource group is created for every availability group that you create. The WSFC monitors this resource group to evaluate the health of the primary replica.  
  
 The quorum for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] is based on all nodes in the WSFC regardless of whether a given cluster node hosts any availability replicas. In contrast to database mirroring, there is no witness role in [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)].  
  
 The overall health of a WSFC is determined by the votes of quorum of nodes in the cluster. If the WSFC goes offline because of an unplanned disaster, or due to a persistent hardware or communications failure, manual administrative intervention is required. A Windows Server or WSFC administrator will need to force a quorum and then bring the surviving cluster nodes back online in a non-fault-tolerant configuration.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] registry keys are subkeys of the WSFC. If you delete and re-create a WSFC, you must disable and re-enable the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature on each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosted an availability replica on the original WSFC.  
  
 For information about running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on WSFC nodes and about WSFC quorum, see [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md).  
  
##  <a name="SQLServerFC"></a> [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instances (FCIs) and Availability Groups  
 You can set up a second layer of failover at the server-instance level by implementing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] an FCI together with the WSFC. An availability replica can be hosted by either a standalone instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or an FCI instance. Only one FCI partner can host a replica for a given availability group. When an availability replica is running on an FCI, the possible owners list for the availability group will contain only the active FCI node.  
  
 [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] does not depend on any form of shared storage. However, if you use a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance (FCI) to host one or more availability replicas, each of those FCIs will require shared storage as per standard SQL Server failover cluster instance installation.  
  
 For more information about additional prerequisites, see the "Prerequisites and Restrictions for Using a SQL Server Failover Cluster Instance (FCI) to Host an Availability Replica" section of [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
### Comparison of Failover Cluster Instances and Availability Groups  
 Regardless of the number of nodes in the FCI, an entire FCI hosts a single replica within an availability group. The following table describes the distinctions in concepts between nodes in an FCI and replicas within an availability group.  
  
||Nodes within an FCI|Replicas within an availability group|  
|-|-------------------------|-------------------------------------------|  
|**Uses WSFC**|Yes|Yes|  
|**Protection level**|Instance|Database|  
|**Storage type**|Shared|Non-shared<br /><br /> While the replicas in an availability group do not share storage, a replica that is hosted by an FCI uses a shared storage solution as required by that FCI. The storage solution is shared only by nodes within the FCI and not between replicas of the availability group.|  
|**Storage solutions**|Direct attached, SAN, mount points, SMB|Depends on node type|  
|**Readable secondaries**|No*|Yes|  
|**Applicable failover policy settings**|WSFC quorum<br /><br /> FCI-specific<br /><br /> Availability group settings**|WSFC quorum<br /><br /> Availability group settings|  
|**Failed-over resources**|Server, instance, and database|Database only|  
  
 *Whereas synchronous secondary replicas in an availability group are always running on their respective [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instances, secondary nodes in an FCI actually have not started their respective [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instances and are therefore not readable. In an FCI, a secondary node starts its [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance only when the resource group ownership is transferred to it during an FCI failover. However, on the active FCI node, when an FCI-hosted database belongs to an availability group, if the local availability replica is running as a readable secondary replica, the database is readable.  
  
 **Failover policy settings for the availability group apply to all replicas, whether it is hosted in a standalone instance or an FCI instance.  
  
> [!NOTE]  
>  For more information about **Number of nodes** within FCIs and **Always On Availability Groups** for different editions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2012](https://go.microsoft.com/fwlink/?linkid=232473) (https://go.microsoft.com/fwlink/?linkid=232473).  
  
### Considerations for hosting an Availability Replica on an FCI  
  
> [!IMPORTANT]  
>  If you plan to host an availability replica on a SQL Server Failover Cluster Instance (FCI), ensure that the Windows Server 2008 host nodes meet the Always On prerequisites and restrictions for Failover Cluster Instances (FCIs). For more information, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instances (FCIs) do not support automatic failover by availability groups, so any availability replica that is hosted by an FCI can only be configured for manual failover.  
  
 You might need to configure a WSFC to include shared disks that are not available on all nodes. For example, consider a WSFC across two data centers with three nodes. Two of the nodes host a SQL Server failover cluster instance (FCI) in the primary data center and have access to the same shared disks. The third node hosts a stand-alone instance of SQL Server in a different data center and does not have access to the shared disks from the primary data center. This WSFC configuration supports the deployment of an availability group if the FCI hosts the primary replica and the stand-alone instance hosts the secondary replica.  
  
 When choosing an FCI to host an availability replica for a given availability group, ensure that an FCI failover could not potentially cause a single WSFC node to attempt to host two availability replicas for the same availability group.  
  
 The following example scenario illustrates how this configuration could lead to problems:  
  
 Marcel configures two a WSFC with two nodes, `NODE01` and `NODE02`. He installs a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance, `fciInstance1`, on both `NODE01` and `NODE02` where `NODE01` is the current owner for `fciInstance1`.  
 On `NODE02`, Marcel installs another instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], `Instance3`, which is a stand-alone instance.  
 On `NODE01`, Marcel enables fciInstance1 for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. On `NODE02`, he enables `Instance3` for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. Then he sets up an availability group for which `fciInstance1` hosts the primary replica, and `Instance3` hosts the secondary replica.  
 At some point `fciInstance1` becomes unavailable on `NODE01`, and the WSFC causes a failover of `fciInstance1` to `NODE02`. After the failover, `fciInstance1` is a [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]-enabled instance running under the primary role on `NODE02`. However, `Instance3` now resides on the same WSFC node as `fciInstance1`. This violates the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] constraint.  
 To correct the problem that this scenario presents, the stand-alone instance, `Instance3`, must reside on another node in the same WSFC as `NODE01` and `NODE02`.  
  
 For more information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCIs, see [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md).  
  
##  <a name="FCMrestrictions"></a> Restrictions on Using The WSFC Failover Cluster Manager with Availability Groups  
 Do not use the Failover Cluster Manager to manipulate availability groups, for example:  
  
-   Do not add or remove resources in the clustered service (resource group) for the availability group.  
  
-   Do not change any availability group properties, such as the possible owners and preferred owners. These properties are set automatically by the availability group.  
  
-   **Do not use the Failover Cluster Manager to move availability groups to different nodes or to fail over availability groups.** The Failover Cluster Manager is not aware of the synchronization status of the availability replicas, and doing so can lead to extended downtime. You must use [!INCLUDE[tsql](../../../includes/tsql-md.md)] or [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  

  >[!WARNING]
  > Using the Failover Cluster Manager to move a *failover cluster instance* hosting an availability group to a node that is *already* hosting a replica of the same availability group may result in the loss of the availability group replica, preventing it from being brought online on the target node. A single node of a failover cluster cannot host more than one replica for the same availability group. For more information on  how this occurs, and how to recover, see the blog [Replica unexpectedly dropped in availability group](https://blogs.msdn.microsoft.com/alwaysonpro/2014/02/03/issue-replica-unexpectedly-dropped-in-availability-group/). 
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [Configure Windows Failover Clustering for SQL Server (Availability Group or FCI) with Limited Security](https://blogs.msdn.microsoft.com/sqlalwayson/2012/06/05/configure-windows-failover-clustering-for-sql-server-availability-group-or-fci-with-limited-security/)  
  
     [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Whitepapers:**  
  
     [Always On Architecture Guide: Building a High Availability and Disaster Recovery Solution by Using Failover Cluster Instances and Availability Groups](https://msdn.microsoft.com/library/jj215886.aspx)  
  
     [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](https://sqlcat.com/)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md)  
  
  
