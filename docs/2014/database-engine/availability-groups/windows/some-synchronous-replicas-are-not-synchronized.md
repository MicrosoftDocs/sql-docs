---
title: "Some synchronous replicas are not synchronized | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.agdashboard.agp5synchronized.issues.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: e58ed56e-4c30-42e6-a9fc-a8c401620e02
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Some synchronous replicas are not synchronized
    
## Introduction  
  
|||  
|-|-|  
|**Policy Name**|Synchronous Replicas Data Synchronization State|  
|**Issue**|Some synchronous replicas are not synchronized.|  
|**Category**|**Warning**|  
|**Facet**|Availability group|  
  
## Description  
 This policy rolls up the data synchronization state of all availability replicas and checks for any availability replicas that are not in the expected synchronization state. The policy is in an unhealthy state when any asynchronous replica is not in a SYNCHRONIZING state and any synchronous replica is not in a SYNCHRONIZED state. The policy state is otherwise healthy.  
  
> [!NOTE]  
>  For this release of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], information about possible causes and solutions is located at [Some synchronous replicas are not synchronized](https://go.microsoft.com/fwlink/p/?LinkId=220853) on the TechNet Wiki.  
  
## Possible Causes  
 In this availability group, at least one synchronous replica is not currently synchronized. The replica synchronization state could be either SYNCHONIZING or NOT SYNCHRONIZING.  
  
## Possible Solution  
 Use the availability replica policy state to find the availability replica with the incorrect synchronization state, and then resolve the issue at the availability replica.  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
