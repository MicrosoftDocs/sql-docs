---
title: "Availability replica does not have a healthy role | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql12.swb.agdashboard.arp1rolehealthy.issues.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: ebb2c9f4-2097-4688-b4fb-8f0571047317
caps.latest.revision: 11
author: "rothja"
ms.author: "jroth"
manager: "jhubbard"
---
# Availability replica does not have a healthy role
    
## Introduction  
  
|||  
|-|-|  
|**Policy Name**|Availability Replica Role State|  
|**Issue**|Availability replica does not have a healthy role.|  
|**Category**|**Critical**|  
|**Facet**|Availability replica|  
  
## Description  
 This policy checks the state of the role of the availability replica. The policy is in an unhealthy state when the role of the availability replica is neither primary nor secondary. The policy is otherwise in a healthy state.  
  
> [!NOTE]  
>  For this release of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], information about possible causes and solutions is located at [Availability replica does not have a healthy role](http://go.microsoft.com/fwlink/p/?LinkId=220856) on the TechNet Wiki.  
  
## Possible Causes  
 The role of this availability replica is unhealthy. The replica does not have either the primary or secondary role.  
  
## Possible Solution: Information_still_to_come  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](../../2014/database-engine/overview-of-alwayson-availability-groups-sql-server.md)   
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](../../2014/database-engine/use-the-alwayson-dashboard-sql-server-management-studio.md)  
  
  