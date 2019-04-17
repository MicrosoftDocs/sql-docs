---
title: "Availability replica does not have a healthy role for an availability group"
description: "Identify possible causes for why a replica does not have a healthy role within an Always On availability group."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.agdashboard.arp1rolehealthy.issues.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], policies"
ms.assetid: ebb2c9f4-2097-4688-b4fb-8f0571047317
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Availability replica does not have a healthy role for an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
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
>  For this release of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], information about possible causes and solutions is located at [Availability replica does not have a healthy role](https://go.microsoft.com/fwlink/p/?LinkId=220856) on the TechNet Wiki.  
  
## Possible Causes  
 The role of this availability replica is unhealthy. The replica does not have either the primary or secondary role.  
  
## Possible Solution: Information_still_to_come  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
