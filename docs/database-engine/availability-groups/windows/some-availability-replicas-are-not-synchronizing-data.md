---
title: "Availability replicas not synchronizing data"
description: "Possible causes and resolutions for when one or more availability replicas in an Always On availability group are not synchronizing data with the primary replica."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.agdashboard.agp4synchronizing.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Some availability replicas are not synchronizing data
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Replicas Data Synchronization State
- **Issue**: Some availability replicas are not synchronizing data.
- **Category**: **Warning**
- **Facet**: Availability group  
  
## Description  
 This policy rolls up the data synchronization state of all availability replicas in the availability group and checks if the synchronization of any availability replica is not operational. The policy is in an unhealthy state if any of the data synchronization states of the availability replica is NOT SYNCRONIZING.  
  
 This policy is in a healthy state if none of the data synchronization states of the availability replica is NOT SYNCHRONIZING.  
 
## Possible Causes  
 In this availability group, at least one secondary replica has a NOT SYNCHRONIZING synchronization state and is not receiving data from the primary replica.  
  
## Possible Solution  
 Use the availability replica policy state to find the availability replica with a NOT SYNCHROINIZING state, and then resolve the issue at the availability replica.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
