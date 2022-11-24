---
title: "WSFC quorum modes & voting configuration"
description: Learn about quorum modes and voting used by the Windows Server Failover Cluster with a SQL Server failover cluster instance or an Always On availability group.
ms.custom: "seo-lt-2019"
ms.date: "10/03/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: failover-cluster-instance
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], WSFC clusters"
  - "quorum [SQL Server], AlwaysOn and WSFC quorum"
  - "quorum [SQL Server], Always On and WSFC quorum"
  - "failover clustering [SQL Server], AlwaysOn Availability Groups"
  - "failover clustering [SQL Server], Always On Availability Groups"
ms.assetid: ca0d59ef-25f0-4047-9130-e2282d058283
author: MashaMSFT
ms.author: mathoma
---
# WSFC Quorum Modes and Voting Configuration (SQL Server)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Both [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)][!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] and Always On Failover Cluster Instances (FCI) take advantage of Windows Server Failover Clustering (WSFC) as a platform technology.  WSFC uses a quorum-based approach to monitoring overall cluster health and maximize node-level fault tolerance. A fundamental understanding of WSFC quorum modes and node voting configuration is very important to designing, operating, and troubleshooting your Always On high availability and disaster recovery solution.  
  
  
##  <a name="ClusterHealthDetectionbyQuorum"></a> Cluster Health Detection by Quorum  
 Each node in a WSFC cluster participates in periodic heartbeat communication to share the node's health status with the other nodes. Unresponsive nodes are considered to be in a failed state.  
  
 A *quorum* node set is a majority of the voting nodes and witnesses in the WSFC cluster. The overall health and status of a WSFC cluster is determined by a periodic *quorum vote*.  The presence of a quorum means that the cluster is healthy and able to provide node-level fault tolerance.  
  
 The absence of a quorum indicates that the cluster is not healthy.  Overall WSFC cluster health must be maintained in order to ensure that healthy secondary nodes are available for primary nodes to fail over to.  If the quorum vote fails, the WSFC cluster will be set offline as a precautionary measure.  This will also cause all [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instances registered with the cluster to be stopped.  
  
> [!IMPORTANT]  
>  If a WSFC cluster is set offline because of quorum failure, manual intervention is required to bring it back online.  
>   
>  For more information, see: [WSFC Disaster Recovery through Forced Quorum &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-disaster-recovery-through-forced-quorum-sql-server.md).  
  
##  <a name="QuorumModes"></a> Quorum Modes  
 A *quorum mode* is configured at the WSFC cluster level that dictates the methodology used for quorum voting.  The Failover Cluster Manager utility will recommend a quorum mode based on the number of nodes in the cluster.  
  
 The following quorum modes can be used to determine what constitutes a quorum of votes:  
  
-   **Node Majority.** More than one-half of the voting nodes in the cluster must vote affirmatively for the cluster to be healthy.  
  
-   **Node and File Share Majority.** Similar to Node Majority quorum mode, except that a remote file share is also configured as a voting witness, and connectivity from any node to that share is also counted as an affirmative vote.  More than one-half of the possible votes must be affirmative for the cluster to be healthy.  
  
     As a best practice, the witness file share should not reside on any node in the cluster, and it should be visible to all nodes in the cluster.  
  
-   **Node and Disk Majority.** Similar to Node Majority quorum mode, except that a shared disk cluster resource is also designated as a voting witness, and connectivity from any node to that shared disk is also counted as an affirmative vote.  More than one-half of the possible votes must be affirmative for the cluster to be healthy.  
  
-   **Disk Only.** A shared disk cluster resource is designated as a witness, and connectivity by any node to that shared disk is counted as an affirmative vote.  
  
> [!TIP]  
>  When using an asymmetric storage configuration for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], you should generally use the Node Majority quorum mode when you have an odd number of voting nodes, or the Node and File Share Majority quorum mode when you have an even number of voting nodes.  
  
##  <a name="VotingandNonVotingNodes"></a> Voting and Non-Voting Nodes  
 By default, each node in the WSFC cluster is included as a member of the cluster quorum; each node has a single vote in determining the overall cluster health, and each node will continuously attempt to establish a quorum.  The quorum discussion to this point has carefully qualified the set of WSFC cluster nodes that vote on cluster health as *voting nodes*.  
  
 No individual node in a WSFC cluster can definitively determine that the cluster as a whole is healthy or unhealthy.  At any given moment, from the perspective of each node, some of the other nodes may appear to be offline, or appear to be in the process of failover, or appear unresponsive due to a network communication failure.  A key function of the quorum vote is to determine whether the apparent state of each of node in the WSFC cluster is indeed that actual state of those nodes.  
  
 For all of the quorum models except 'Disk Only', the effectiveness of a quorum vote depends on reliable communications between all of the voting nodes in the cluster. Network communications between nodes on the same physical subnet should be considered reliable; the quorum vote should be trusted.  
  
 However, if a node on another subnet is seen as non-responsive in a quorum vote, but it is actually online and otherwise healthy, that is most likely due to a network communications failure between subnets.  Depending upon the cluster topology, quorum mode, and failover policy configuration, that network communications failure may effectively create more than one set (or subset) of voting nodes.  
  
 When more than one subset of voting nodes is able to establish a quorum on its own, that is known as a *split-brain scenario*.  In such a scenario, the nodes in the separate quorums may behave differently, and in conflict with one another.  
  
> [!NOTE]  
>  The split-brain scenario is only possible when a system administrator manually performs a forced quorum operation, or in very rare circumstances, a forced failover; explicitly subdividing the quorum node set.  
  
 In order to simplify your quorum configuration and increase up-time, you may want to adjust each node's *NodeWeight* setting so that the node's vote is not counted towards the quorum.  
  
> [!IMPORTANT]  
>  In order to use NodeWeight settings, the following hotfix must be applied to all servers in the WSFC cluster:  
>   
>  [KB2494036](https://support.microsoft.com/kb/2494036): A hotfix is available to let you configure a cluster node that does not have quorum votes in [!INCLUDE[winserver2008](../../../includes/winserver2008-md.md)] and in [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)]  
  
##  <a name="RecommendedAdjustmentstoQuorumVoting"></a> Recommended Adjustments to Quorum Voting  
 When enabling or disabling a given WSFC node's vote, follow these guidelines:  
  
-   **No vote by default.** Assume that each node should not vote without explicit justification.  
  
-   **Include all primary replicas.** Each WSFC node that hosts an availability group primary replica or is the preferred owner of an FCI should have a vote.  
  
-   **Include possible automatic failover owners.** Each node that could host a primary replica, as the result of an automatic availability group failover or FCI failover, should have a vote. If there is only one availability group in the WSFC cluster and availability replicas are hosted only by standalone instances, this rule includes only the secondary replica that is the automatic failover target.  
  
-   **Exclude secondary site nodes.** In general, do not give votes to WSFC nodes that reside at a secondary disaster recovery site.  You do not want nodes in the secondary site to contribute to a decision to take the cluster offline when there is nothing wrong with the primary site.  
  
-   **Odd number of votes.** If necessary, add a witness file share, a witness node, or a witness disk to the cluster and adjust the quorum mode to prevent possible ties in the quorum vote.  
  
-   **Re-assess vote assignments post-failover.** You do not want to fail over into a cluster configuration that does not support a healthy quorum.  
  
> [!IMPORTANT]
>  When validating WSFC quorum vote configuration, the Always On Availability Group Wizard shows a warning if any of the following conditions are true:  
> 
>  -   The cluster node that hosts the primary replica does not have a vote  
> -   A secondary replica is configured for automatic failover and its cluster node does not have a vote.  
> -   [KB2494036](https://support.microsoft.com/kb/2494036) is not installed on all cluster nodes that host availability replicas. This patch is required to add or remove votes for cluster nodes in multi-site deployments. However, in single-site deployments, it is usually not required and you may safely ignore the warning.  
> 
> [!TIP]
>  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] exposes several system dynamic management views (DMVs) that can help you manage settings related WSFC cluster configuration and node quorum voting.  
> 
>  For more information, see:  [sys.dm_hadr_cluster](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-transact-sql.md), [sys.dm_hadr_cluster_members](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-members-transact-sql.md), [sys.dm_os_cluster_nodes](../../../relational-databases/system-dynamic-management-views/sys-dm-os-cluster-nodes-transact-sql.md), [sys.dm_hadr_cluster_networks](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-networks-transact-sql.md)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [View Cluster Quorum NodeWeight Settings](../../../sql-server/failover-clusters/windows/view-cluster-quorum-nodeweight-settings.md)  
  
-   [Configure Cluster Quorum NodeWeight Settings](../../../sql-server/failover-clusters/windows/configure-cluster-quorum-nodeweight-settings.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))  
  
-   [Quorum vote configuration check in Always On Availability Group Wizards](/archive/blogs/sqlalwayson/quorum-vote-configuration-check-in-alwayson-availability-group-wizards-andy-jing)  
  
-   [Windows Server Technologies:  Failover Clusters](https://technet.microsoft.com/library/cc732488\(v=WS.10\).aspx)  
  
-   [Failover Cluster Step-by-Step Guide: Configuring the Quorum in a Failover Cluster](https://technet.microsoft.com/library/cc770620\(WS.10\).aspx)  
  
## See Also  
 [WSFC Disaster Recovery through Forced Quorum &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-disaster-recovery-through-forced-quorum-sql-server.md)   
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)  
  
