---
title: "Force a WSFC Cluster to Start Without a Quorum | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], WSFC clusters"
  - "quorum [SQL Server], AlwaysOn and WSFC quorum"
ms.assetid: 4a121375-7424-4444-b876-baefa8fe9015
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Force a WSFC Cluster to Start Without a Quorum
  This topic describes how to force a Windows Server Failover Clustering (WSFC) cluster node to start without a quorum.  This may be required in disaster recovery and multi-subnet scenarios to recover data and fully re-establish high-availability for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instances.  
  
-   **Before you start:**  [Recommendations](#Recommendations), [Security](#Security)  
  
-   **To force a cluster to start without a quorum using:**  [Using Failover Cluster Manager](#FailoverClusterManagerProcedure), [Using Powershell](#PowerShellProcedure), [Using Net.exe](#CommandPromptProcedure)  
  
-   **Follow up:**  [Follow Up: After Forcing Cluster to Start without a Quorum](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Start  
  
###  <a name="Recommendations"></a> Recommendations  
 Except where explicitly directed, the procedures in this topic should work if you execute them from any node in the WSFC cluster.  However, you may obtain better results, and avoid networking issues, by executing these steps from the node that you intend to force to start without a quorum.  
  
###  <a name="Security"></a> Security  
 The user must be a domain account that is member of the local Administrators group on each node of the WSFC cluster.  
  
##  <a name="FailoverClusterManagerProcedure"></a> Using Failover Cluster Manager  
  
##### To force a cluster to start without a quorum  
  
1.  Open a Failover Cluster Manager and connect to the desired cluster node to force online.  
  
2.  In the **Actions** pane, click **Force Cluster Start**, and then click **Yes - Force my cluster to start**.  
  
3.  In the left pane, in the **Failover Cluster Manager** tree, click the cluster name.  
  
4.  In the summary pane, confirm that the current **Quorum Configuration** value is:  **Warning: Cluster is running in ForceQuorum state**.  
  
##  <a name="PowerShellProcedure"></a> Using Powershell  
  
##### To force a cluster to start without a quorum  
  
1.  Start an elevated Windows PowerShell via **Run as Administrator**.  
  
2.  Import the `FailoverClusters` module to enable cluster commandlets.  
  
3.  Use `Stop-ClusterNode` to make sure that the cluster service is stopped.  
  
4.  Use `Start-ClusterNode` with `-FixQuorum` to force the cluster service to start.  
  
5.  Use `Get-ClusterNode` with `-Propery NodeWieght = 1` to set the value the guarantees that the node is a voting member of the quorum.  
  
6.  Output the cluster node properties in a readable format.  
  
### Example (Powershell)  
 The following example forces the AlwaysOnSrv02 node cluster service to start without a quorum, sets the `NodeWeight = 1`, and then enumerates cluster node status from the newly forced node.  
  
```powershell  
Import-Module FailoverClusters  
  
$node = "AlwaysOnSrv02"  
Stop-ClusterNode -Name $node  
Start-ClusterNode -Name $node -FixQuorum  
  
(Get-ClusterNode $node).NodeWeight = 1  
  
$nodes = Get-ClusterNode -Cluster $node  
$nodes | Format-Table -property NodeName, State, NodeWeight  
  
```  
  
##  <a name="CommandPromptProcedure"></a> Using Net.exe  
  
##### To force a cluster to start without a quorum  
  
1.  Use Remote Desktop to connect to the desired cluster node to force online.  
  
2.  Start an elevated Command Prompt via **Run as Administrator**.  
  
3.  Use **net.exe** to make sure that the local cluster service is stopped.  
  
4.  Use **net.exe** with `/forcequorum` to force the local cluster service to start.  
  
### Example (Net.exe)  
 The following example forces a node cluster service to start without a quorum, sets the `NodeWeight = 1`, and then enumerates cluster node status from the newly forced node.  
  
```ms-dos  
net.exe stop clussvc  
net.exe start clussvc /forcequorum  
```  
  
##  <a name="FollowUp"></a> Follow Up: After Forcing Cluster to Start without a Quorum  
  
-   You must re-evaluate and reconfigure NodeWeight values to correctly construct a new quorum before bringing other nodes back online. Otherwise, the cluster may go back offline again.  
  
     For more information, see [WSFC Quorum Modes and Voting Configuration &#40;SQL Server&#41;](wsfc-quorum-modes-and-voting-configuration-sql-server.md).  
  
-   The procedures in this topic are only one step in bringing the WSFC cluster back online if an un-planned quorum failure were to occur.  You may also want to take additional steps to prevent other WSFC cluster nodes from interfering with the new quorum configuration.  
  
-   Other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features such as [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], database mirroring, and log shipping may also require subsequent actions to recover data and to fully re-establish high-availability.  
  
     **For more information:**  
  
     [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)  
  
     [Force Service in a Database Mirroring Session &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/force-service-in-a-database-mirroring-session-transact-sql.md)  
  
     [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [View Events and Logs for a Failover Cluster](https://technet.microsoft.com/en-us/library/cc772342\(WS.10\).aspx)  
  
-   [Get-ClusterLog Failover Cluster Cmdlet](https://technet.microsoft.com/library/ee461045.aspx)  
  
## See Also  
 [WSFC Disaster Recovery through Forced Quorum &#40;SQL Server&#41;](wsfc-disaster-recovery-through-forced-quorum-sql-server.md)   
 [Configure Cluster Quorum NodeWeight Settings](configure-cluster-quorum-nodeweight-settings.md)   
 [Failover Cluster Cmdlets in Windows PowerShell Listed by Task Focus](https://technet.microsoft.com/library/ee619761\(WS.10\).aspx)  
  
  
