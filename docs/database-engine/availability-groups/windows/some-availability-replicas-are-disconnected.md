---
title: "Some availability replicas are disconnected"
description: Possible causes and solutions for when your availability group replica is disconnected for an Always On SQL Server availability group.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.agdashboard.agp7allconnected.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Some availability replicas are disconnected
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Replicas Connection State
- **Issue**: Some availability replicas are disconnected.
- **Category**: **Warning**
- **Facet**: Availability group  
  
## Description  
 This policy rolls up the connection state of all availability replicas and checks for any availability replicas that are DISCONENCTED. The policy is in an unhealthy state when any availability replica is DISCONNECTED. The policy is otherwise in a healthy state.  
 
## Possible Causes  
 In this availability group, at least one secondary replica is not connected to the primary replica. The connected state is DISCONNECTED.  
  
## Possible Solution  
 Use the availability replica policy state to find the availability replica that is DISCONNECTED, and then resolve the issue at the availability replica.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
