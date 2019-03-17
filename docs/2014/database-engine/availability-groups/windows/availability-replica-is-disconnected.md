---
title: "Availability replica is disconnected | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.agdashboard.arp2connected.issues.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: 1a2162d3-54fb-4356-b349-effbdc15a5a4
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Availability replica is disconnected
    
## Introduction  
  
|||  
|-|-|  
|**Policy Name**|Availability Replica Connection State|  
|**Issue**|Availability replica is disconnected.|  
|**Category**|**Critical**|  
|**Facet**|Availability replica|  
  
## Description  
 This policy checks the connection state between availability replicas. The policy is in an unhealthy state when the connection state of the availability replica is DISCONNECTED. The policy is otherwise in a healthy state.  
  
> [!NOTE]  
>  For this release of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], information about possible causes and solutions is located at [Availability replica is disconnected](https://go.microsoft.com/fwlink/p/?LinkId=220857) on the TechNet Wiki.  
  
## Possible Causes  
 The secondary replica is not connected to the primary replica. The connected state is DISCONNECTED. This issue can be caused by the following:  
  
-   The connection port might be in conflict with another application.  
  
-   The encryption type or algorithm is mismatched.  
  
-   The connection endpoint has been deleted or has not been started.  
  
-   The transport is disconnected.  
  
## Possible Solutions  
 Following are possible solutions for this issue:  
  
-   Check the database mirroring endpoint configuration for the instances of the primary and secondary replica and update the mismatched configuration.  
  
-   Check if the port is conflicting, and if so, change the port number.  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
