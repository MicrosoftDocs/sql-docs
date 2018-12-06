---
title: "Take an Availability Group Offline (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], take offline"
ms.assetid: 50f5aad8-0dff-45ef-8350-f9596d3db898
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Take an Availability Group Offline (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to take an Always On availability group from the ONLINE state to the OFFLINE state by using [!INCLUDE[tsql](../../../includes/tsql-md.md)] in [!INCLUDE[ssSQL11SP1](../../../includes/sssql11sp1-md.md)] and later versions. There is no data loss for synchronous-commit databases because if any synchronous-commit replica is not synchronized, the OFFLINE operation raises an error and leaves the availability group ONLINE. Keeping the availability group online protects unsynchronized synchronous-commit databases from possible data loss. After an availability group goes offline, its databases become unavailable to clients and you cannot bring the availability group back online. Therefore, take an availability group offline only to migrate the availability group resources from one WSFC cluster to another.  
  
 During a cross-cluster migration of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], if any applications connect directly to the primary replica of an availability group, the availability group must be taken offline. Cross-cluster migration of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] supports OS upgrade with minimal downtime of availability groups. The typical scenario is to use cross-cluster migration of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] for OS upgrade to [!INCLUDE[win8](../../../includes/win8-md.md)] or [!INCLUDE[win8srv](../../../includes/win8srv-md.md)]. For more information, see [Cross-Cluster Migration of Always On Availability Groups for OS Upgrade](https://msdn.microsoft.com/library/jj873730.aspx).  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To take an availability group offline, using:**  [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After the Availability Group Goes Offline](#FollowUp)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
> [!CAUTION]  
>  Use the OFFLINE option only for a cross-cluster migration of availability group resources.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   The server instance on which you enter the OFFLINE command must be running [!INCLUDE[ssSQL11SP1](../../../includes/sssql11sp1-md.md)] or above (Enterprise edition or above).  
  
-   The availability group must currently be online.  
  
###  <a name="Recommendations"></a> Recommendations  
 Before you take the availability group offline, delete the availability group listener or listeners. For more information, see [Remove an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-an-availability-group-listener-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To take an availability group offline**  
  
1.  Connect to a server instance that hosts an availability replica for the availability group. This replica can be the primary replica or a secondary replica.  
  
2.  Use the [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) statement, as follows:  
  
     ALTER AVAILABILITY GROUP *group_name* OFFLINE  
  
     where *group_name* is the name of the availability group.  
  
### Example  
 The following example takes the `AccountsAG` availability group offline.  
  
```  
ALTER AVAILABILITY GROUP AccountsAG OFFLINE;  
```  
  
##  <a name="FollowUp"></a> Follow Up: After the Availability Group Goes Offline  
  
-   **Logging of OFFLINE operation:**  The identity of the WSFC node where the OFFLINE operation was initiated is stored in both the WSFC cluster log and the SQL ERRORLOG.  
  
-   **If you did not delete the availability group listener before taking the group offline:**  If you are migrating the availability group to another WSFC cluster, delete the VNN and VIP of the listener. You can delete them by using either the Failover Cluster Management console, the [Remove-ClusterResource](https://technet.microsoft.com/library/ee461015\(WS.10\).aspx) PowerShell cmdlet, or [cluster.exe](https://technet.microsoft.com/library/ee461015\(WS.10\).aspx). Note that cluster.exe is deprecated on Windows 8.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Remove an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-an-availability-group-listener-sql-server.md)  
  
-   [Change the HADR Cluster Context of Server Instance &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-hadr-cluster-context-of-server-instance-sql-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [SQL Server 2012 Technical Articles](https://msdn.microsoft.com/library/bb418445\(SQL.10\).aspx)  
  
-   [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)  
  
## See Also  
 [Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  
  
