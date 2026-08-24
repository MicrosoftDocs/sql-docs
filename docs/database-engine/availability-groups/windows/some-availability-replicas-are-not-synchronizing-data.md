---
title: "Availability replicas not synchronizing data"
description: "Possible causes and resolutions for when one or more availability replicas in an Always On availability group are not synchronizing data with the primary replica."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: reference
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
 This policy rolls up the data synchronization state of all availability replicas in the availability group and checks if the synchronization of any availability replica is not operational. The policy is in an unhealthy state if any of the data synchronization states of the availability replica is NOT SYNCHRONIZING.  
  
 This policy is in a healthy state if none of the data synchronization states of the availability replica is NOT SYNCHRONIZING.  
 
## Possible Causes  
 In this availability group, at least one secondary replica has a NOT SYNCHRONIZING synchronization state and is not receiving data from the primary replica.  
  
## Possible Solution  
 Use the availability replica policy state to find the availability replica with a NOT SYNCHRONIZING state, and then resolve the issue at the availability replica.  
  
## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Use the Always On Availability Group dashboard (SQL Server Management Studio)](use-the-always-on-dashboard-sql-server-management-studio.md)
