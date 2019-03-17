---
title: "Data synchronization state of availability database is not healthy | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.agdashboard.arp3datasynchealthy.issues.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: 4fd003e7-808e-4b0e-b28a-47d9f2616f06
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Data synchronization state of availability database is not healthy
    
## Introduction  
  
|||  
|-|-|  
|**Policy Name**|Availability Database Data Synchronization State|  
|**Issue**|Data synchronization state of availability database is not healthy.|  
|**Category**|**Warning**|  
|**Facet**|Availability database|  
  
## Description  
 This policy rolls up the data synchronization state of all availability databases (also known as "database replicas") in the availability replica. The policy is in an unhealthy sate when any database replica is not in the expected data synchronization state. The policy is otherwise in a healthy state.  
  
> [!NOTE]  
>  For this release of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], information about possible causes and solutions is located at [Data synchronization state of some availability database is not healthy](https://go.microsoft.com/fwlink/p/?LinkId=220858) on the TechNet Wiki.  
  
## Possible Causes  
 The data synchronization state of this availability database is unhealthy. On an asynchronous-commit availability replica, every availability database should be in the SYNCHRONIZING state. On a synchronous-commit replica, every availability database must be in the SYNCHRONIZED state.  
  
## Possible Solution  
 Use the database replica policy to find the database replica with an unhealthy data synchronization state, and then resolve the issue at the database replica.  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
