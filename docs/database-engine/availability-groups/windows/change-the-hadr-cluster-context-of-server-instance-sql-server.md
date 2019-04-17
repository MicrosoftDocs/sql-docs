---
title: "Change which cluster manages the metadata for replicas in an availability group"
description: "When doing a cross-cluster migration, change which cluster manages the metadata for availability replicas within an Always On availability group by changing the HADR cluster context for an instance of SQL Server."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], WSFC clusters"
  - "Availability replicas [SQL Server], change WSFC cluster context"
ms.assetid: ecd99f91-b9a2-4737-994e-507065a12f80
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---
# Change which cluster manages the metadata for replicas in an Always On availability group

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to switch the HADR cluster context of an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../../includes/tsql-md.md)] in [!INCLUDE[ssSQL11SP1](../../../includes/sssql11sp1-md.md)] and later versions. The *HADR cluster context* determines which Windows Server Failover Clustering (WSFC) cluster manages the metadata for availability replicas hosted by the server instance.  
  
 Switch the HADR cluster context only during a cross-cluster migration of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] to an instance of [!INCLUDE[ssSQL11SP1](../../../includes/sssql11sp1-md.md)] on a new WSFC cluster. Cross-cluster migration of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] supports OS upgrade to [!INCLUDE[win8](../../../includes/win8-md.md)] or [!INCLUDE[win8srv](../../../includes/win8srv-md.md)] with minimal downtime of availability groups. For more information, see [Cross-Cluster Migration of Always On Availability Groups for OS Upgrade](https://msdn.microsoft.com/library/jj873730.aspx).  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To switch the cluster context of an availability replica, using:**  [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After Switching the Cluster Context of an Availability Replica](#FollowUp)  
  
-   [Related Tasks](#RelatedTasks)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
> [!CAUTION]  
>  Switch the HADR cluster context only during cross-cluster migration of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] deployments.  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   You can switch the HADR cluster context only from the local WSFC cluster to a remote cluster and then back from the remote cluster to the local cluster. You cannot switch the HADR cluster context from one remote cluster to another remote cluster.  
  
-   The HADR cluster context can be switched to a remote cluster only when the instance of SQL Server is not hosting any availability replicas.  
  
-   A remote HADR cluster context can be switched back to the local cluster at any time. However, the context cannot be switched again as long as the server instance is hosting any availability replicas.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   The server instance on which you change the HADR cluster context must be running [!INCLUDE[ssSQL11SP1](../../../includes/sssql11sp1-md.md)] or above (Enterprise edition or above).  
  
-   The server instance must be enabled for Always On. For more information, see [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).  
  
-   To be eligible to be switched from the local cluster context to a remote cluster cluster, a server instance cannot be hosting any availability replicas. The [sys.availability_replicas](../../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md) catalog view should not return any rows.  
  
     If any availability replicas exist on the server instance, before you can change the HADR cluster context, you must do one of the following:  
  
    |Replica Role|Action|Link|  
    |------------------|------------|----------|  
    |Primary|Take the availability group offline.|[Take an Availability Group Offline &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/take-an-availability-group-offline-sql-server.md)|  
    |Secondary|Remove the replica from its availability group|[Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-replica-from-an-availability-group-sql-server.md)|  
  
-   Before you can switch from a remote cluster to the local cluster, all synchronous-commit replicas must be SYNCHRONIZED.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   We recommend that you specify the full domain name. This is because to find the target IP address of a short name, ALTER SERVER CONFIGURATION uses DNS resolution. Under some situations, depending on the DNS searching order, using a short name could cause confusion. For example, consider the following command, which is executed on a node in the `abc` domain, (`node1.abc.com`). The intended destination cluster is the `CLUS01` cluster in the `xyz` domain (`clus01.xyz.com`). However, the local  domain hosts also hosts a cluster named `CLUS01` (`clus01.abc.com`).  
  
     If the short name of the target cluster, `CLUS01`, were specified, DNS name resolution could return the IP address of the wrong cluster, `clus01.abc.com`. To avoid such confusion, specify the full name of the target cluster, as in the following example:  
  
    ```  
    ALTER SERVER CONFIGURATION SET HADR CLUSTER CONTEXT = 'clus01.xyz.com'  
    ```  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
-   **SQL Server login**  
  
     Requires CONTROL SERVER permission.  
  
-   **SQL Server service account**  
  
     The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account of the server instance must have:  
  
    -   Permission to open the destination WSFC cluster.  
  
    -   Remote WSFC read-write access.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To change the WSFC cluster context of an availability replica**  
  
1.  Connect to the server instance that hosts either the primary replica or a secondary replica of the availability group.  
  
2.  Use the SET HADR CLUSTER CONTEXT clause of the [ALTER SERVER CONFIGURATION](../../../t-sql/statements/alter-server-configuration-transact-sql.md) statement, as follows:  
  
     ALTER SERVER CONFIGURATION SET HADR CLUSTER CONTEXT **=** { **'**_windows\_cluster_**'** | LOCAL }  
  
     where,  
  
     *windows_cluster*  
     The cluster object name (CON) of a WSFC cluster. You can specify either the short name or the full domain name. We recommend that you specify the full domain name. For more information, see [Recommendations](#Recommendations), earlier in this topic.  
  
     LOCAL  
     The local WSFC cluster.  
  
### Examples  
 The following example changes the HADR cluster context to a different cluster. To identify the destination WSFC cluster, `clus01`, the example specifies the full cluster object name, `clus01.xyz.com`.  
  
```  
ALTER SERVER CONFIGURATION SET HADR CLUSTER CONTEXT = 'clus01.xyz.com';  
```  
  
 The following example changes the HADR cluster context to the local WSFC cluster.  
  
```  
ALTER SERVER CONFIGURATION SET HADR CLUSTER CONTEXT = LOCAL;  
```  
  
##  <a name="FollowUp"></a> Follow Up: After Switching the Cluster Context of an Availability Replica  
 The new HADR cluster context takes effect immediately, without restarting the server instance. The HADR cluster context setting is a persistent instance-level setting that remains unchanged if the server instance restarts.  
  
 Confirm the new HADR cluster context by querying the [sys.dm_hadr_cluster](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-transact-sql.md) dynamic management view, as follows:  
  
```  
SELECT cluster_name FROM sys.dm_hadr_cluster  
```  
  
 This query should return the name of the cluster to which you set the HADR cluster context.  
  
 When the HADR cluster context is switched to a new cluster:  
  
-   The metadata is cleaned up for any availability replicas that are currently hosted by the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   All the databases that previously belonged to an availability replica are now in the RESTORING state.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Remove an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-an-availability-group-listener-sql-server.md)  
  
-   [Take an Availability Group Offline &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/take-an-availability-group-offline-sql-server.md)  
  
-   [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-replica-from-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [SQL Server 2012 Technical Articles](https://msdn.microsoft.com/library/bb418445\(SQL.10\).aspx)  
  
-   [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)  
  
## See Also  
 [Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)   
 [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-server-configuration-transact-sql.md)  
  
  
