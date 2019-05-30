---
title: "Availability group is offline | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.agdashboard.agp2online.issues.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: 093c5208-bf7a-49f4-a546-72b48197cadf
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Availability group is offline
    
## Introduction  
  
|||  
|-|-|  
|**Policy Name**|Availability Group Online State|  
|**Issue**|Availability group is offline.|  
|**Category**|**Critical**|  
|**Facet**|Availability group|  
  
## Description  
 This policy checks the online or offline state of the availability group. The policy is in an unhealthy state and an alert is raised when the cluster resource of the availability group is offline or the availability group does not have a primary replica.  
  
 The policy state is healthy when the cluster resource of the availability group is online and the availability group has a primary replica.  
  
> [!NOTE]  
>  For this release of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], information about possible causes and solutions is located at [Availability group is offline](https://go.microsoft.com/fwlink/p/?LinkId=220850) on the TechNet Wiki.  
  
## Possible Causes  
 This issue can be caused by a failure in the server instance that hosts the primary replica or by the Windows Server Failover Cluster (WSFC) availability group resource going offline. Following are possible causes for the availability group to be offline:  
  
-   The availability group is not configured with automatic failover mode. The primary replica becomes unavailable and the role of all replicas in the availability group become RESOLVING.  
  
    -   The primary replica instance service is down or unresponsive.  
  
    -   The availability group has a connectivity issue with the cluster.  
  
-   The availability group is configured with automatic failover mode and does not complete successfully.  
  
    -   During the automatic failover, the primary readiness check on the target replica fails, and there is no replica available to become the new primary.  
  
-   The availability group resource in the cluster becomes offline.  
  
    -   Any dependent cluster resource encounters a critical issue and becomes offline. The availability group resource is also offline until the dependent resource becomes online.  
  
    -   A critical issue in the cluster turns off the availability group resource.  
  
-   There is an automatic, manual, or forced failover in progress for the availability group.  
  
## Possible Solutions  
 Following are possible solutions for this issue:  
  
-   If the SQL Server instance of the primary replica is down, restart the server and then verify that the availability group recovers to a healthy state.  
  
-   If the automatic failover appears to have failed, verify that the databases on the replica are synchronized with the previously known primary replica, and then failover to the primary replica. If the databases are not synchronized, select a replica with a minimum loss of data, and then recover to failover mode.  
  
-   If the resource in the cluster is offline while the instances of SQL Server appear to be healthy, use Failover Cluster Manager to check the cluster health or other cluster issues on the server. You can also use the Failover Cluster Manager to attempt to turn the availability group resource online.  
  
-   If there is a failover in progress, wait for the failover to complete.  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
