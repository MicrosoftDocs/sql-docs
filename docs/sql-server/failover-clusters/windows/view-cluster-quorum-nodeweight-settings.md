---
title: "View Cluster Quorum NodeWeight Settings | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], WSFC clusters"
  - "quorum [SQL Server], AlwaysOn and WSFC quorum"
ms.assetid: b845e73a-bb01-4de2-aac2-8ac12abebc95
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# View Cluster Quorum NodeWeight Settings
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to view NodeWeight settings for each member node in a Windows Server Failover Clustering (WSFC) cluster. NodeWeight settings are used during quorum voting to support disaster recovery and multi-subnet scenarios for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instances.  
  
-   **Before you start:**  [Prerequisites](#Prerequisites), [Security](#Security)  
  
-   **To view quorum NodeWeight settings using:** [Using Transact-SQL](#TsqlProcedure), [Using Powershell](#PowerShellProcedure), [Using Cluster.exe](#CommandPromptProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Start  
  
###  <a name="Prerequisites"></a> Prerequisites  
 This feature is supported only in [!INCLUDE[firstref_longhorn](../../../includes/firstref-longhorn-md.md)] or later versions.  
  
> [!IMPORTANT]  
>  In order to use NodeWeight settings, the following hotfix must be applied to all servers in the WSFC cluster:  
>   
>  [KB2494036](https://support.microsoft.com/kb/2494036): A hotfix is available to let you configure a cluster node that does not have quorum votes in [!INCLUDE[firstref_longhorn](../../../includes/firstref-longhorn-md.md)] and in [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)]  
  
> [!TIP]  
>  If this hotfix is not installed, the examples in this topic will return empty or NULL values for NodeWeight.  
  
###  <a name="Security"></a> Security  
 The user must be a domain account that is member of the local Administrators group on each node of the WSFC cluster.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
##### To view NodeWeight settings  
  
1.  Connect to any [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance in the cluster.  
  
2.  Query the [sys].[dm_hadr_cluster_members] view.  
  
### Example (Transact-SQL)  
 The following example queries a system view to return values for all of the nodes in that instance's cluster.  
  
```tsql  
SELECT  member_name, member_state_desc, number_of_quorum_votes  
 FROM   sys.dm_hadr_cluster_members;  
```  
  
##  <a name="PowerShellProcedure"></a> Using Powershell  
  
##### To view NodeWeight settings  
  
1.  Start an elevated Windows PowerShell via **Run as Administrator**.  
  
2.  Import the `FailoverClusters` module to enable cluster commandlets.  
  
3.  Use the `Get-ClusterNode` object to return a collection of cluster node objects.  
  
4.  Output the cluster node properties in a readable format.  
  
### Example (Powershell)  
 The following example output some of the node properties for the cluster called "Cluster001".  
  
```powershell  
Import-Module FailoverClusters  
  
$cluster = "Cluster001"  
$nodes = Get-ClusterNode -Cluster $cluster  
  
$nodes | Format-Table -property NodeName, State, NodeWeight  
```  
  
##  <a name="CommandPromptProcedure"></a> Using Cluster.exe  
  
> [!NOTE]  
>  The cluster.exe utility is deprecated in the [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)] release.  Please use PowerShell with Failover Clustering for future development.  The cluster.exe utility will be removed in the next release of Windows Server. For more information, see [Mapping Cluster.exe Commands to Windows PowerShell Cmdlets for Failover Clusters](https://technet.microsoft.com/library/ee619744\(WS.10\).aspx).  
  
##### To view NodeWeight settings  
  
1.  Start an elevated Command Prompt via **Run as Administrator**.  
  
2.  Use **cluster.exe** to return node status and NodeWeight values  
  
### Example (Cluster.exe)  
 The following example outputs some of the node properties for the cluster called "Cluster001".  
  
```ms-dos  
cluster.exe Cluster001 node /status /properties  
```  
  
## See Also  
 [WSFC Quorum Modes and Voting Configuration &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-quorum-modes-and-voting-configuration-sql-server.md)   
 [Configure Cluster Quorum NodeWeight Settings](../../../sql-server/failover-clusters/windows/configure-cluster-quorum-nodeweight-settings.md)   
 [sys.dm_hadr_cluster_members &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-members-transact-sql.md)   
 [Failover Cluster Cmdlets in Windows PowerShell Listed by Task Focus](https://technet.microsoft.com/library/ee619761\(WS.10\).aspx)  
  
  
