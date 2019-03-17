---
title: "Some availability replicas are disconnected | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.agdashboard.agp7allconnected.issues.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: aea808be-6f0f-40c2-9aa2-a2a435ec6443
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Some availability replicas are disconnected
    
## Introduction  
  
|||  
|-|-|  
|**Policy Name**|Availability Replicas Connection State|  
|**Issue**|Some availability replicas are disconnected.|  
|**Category**|**Warning**|  
|**Facet**|Availability group|  
  
## Description  
 This policy rolls up the connection state of all availability replicas and checks for any availability replicas that are DISCONENCTED. The policy is in an unhealthy state when any availability replica is DISCONNECTED. The policy is otherwise in a healthy state.  
  
> [!NOTE]  
>  For this release of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], information about possible causes and solutions is located at [Some availability replicas are disconnected](https://go.microsoft.com/fwlink/p/?LinkId=220855) on the TechNet Wiki.  
  
## Possible Causes  
 In this availability group, at least one secondary replica is not connected to the primary replica. The connected state is DISCONNECTED.  
  
## Possible Solution  
 Use the availability replica policy state to find the availability replica that is DISCONNECTED, and then resolve the issue at the availability replica.  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
