---
title: "Some synchronous replicas are not synchronized"
description: Describes some possible causes and solutions for when a synchronous replica is not synchronized for an Always On availability group.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.agdashboard.agp5synchronized.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Some synchronous replicas are not synchronized
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Synchronous Replicas Data Synchronization State
- **Issue**: Some synchronous replicas are not synchronized.
- **Category**: **Warning**
- **Facet**: Availability group  
  
## Description  
 This policy rolls up the data synchronization state of all availability replicas and checks for any availability replicas that are not in the expected synchronization state. The policy is in an unhealthy state when any asynchronous replica is not in a SYNCHRONIZING state and any synchronous replica is not in a SYNCHRONIZED state. The policy state is otherwise healthy.  

## Possible Causes  
 In this availability group, at least one synchronous replica is not currently synchronized. The replica synchronization state could be either SYNCHRONIZING or NOT SYNCHRONIZING.  
  
## Possible Solution  
 Use the availability replica policy state to find the availability replica with the incorrect synchronization state, and then resolve the issue at the availability replica.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
