---
title: "Data synchronization state of some availability database is not healthy | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.agdashboard.drp3datasynchealthy.issues.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: 89f95d15-33c6-4768-bccd-9dbf8c4f49a9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Data synchronization state of some availability database is not healthy
    
## Introduction  
  
|||  
|-|-|  
|**Policy Name**|Availability Replica Data Synchronization State|  
|**Issue**|Data synchronization state of some availability database is not healthy.|  
|**Category**|**Warning**|  
|**Facet**|Availability replica|  
  
## Description  
 This policy checks the data synchronization state of the availability database (also known as a "database replica"). The policy is in an unhealthy state when the data synchronization state is NOT SYNCHRONIZING or the state is not SYNCHRONIZED for the synchronous-commit database replica.  
  
> [!NOTE]  
>  For this release of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], information about possible causes and solutions is located at [Data synchronization state of availability database is not healthy](https://go.microsoft.com/fwlink/p/?LinkId=220863) on the TechNet Wiki.  
  
## Possible Causes  
 At least one availability database on the replica has an unhealthy data synchronization state. If this is an asynchronous-commit availability replica, all availability databases should be in the SYNCHRONIZING state. If this is a synchronous-commit availability replica, all availability databases should be in the SYNCHRONIZED state. This issue can be caused by the following:  
  
-   The availability replica might be disconnected.  
  
-   The data movement might be suspended.  
  
-   The database might not be accessible.  
  
-   There might be a temporary delay issue due to network latency or the load on the primary or secondary replica.  
  
## Possible Solution  
 Resolve any connection or data movement suspend issues. Check the events for this issue using SQL Server Management Studio, and find the database error. Follow the troubleshooting steps for the specific error.  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
