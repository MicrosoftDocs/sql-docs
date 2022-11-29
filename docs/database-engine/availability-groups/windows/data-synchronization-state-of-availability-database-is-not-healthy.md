---
title: "Data synchronization state of availability database is not healthy"
description: "Identify possible causes for why the data synchronization state of database in an Always On availability group is not healthy."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: end-user-help
ms.custom: seodec18
f1_keywords:
  - "sql13.swb.agdashboard.arp3datasynchealthy.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Data synchronization state of availability database is not healthy for an Always On availability group
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Database Data Synchronization State
- **Issue**: Data synchronization state of availability database is not healthy.
- **Category**: **Warning**
- **Facet**: Availability database  
  
## Description  
 This policy rolls up the data synchronization state of all availability databases (also known as "database replicas") in the availability replica. The policy is in an unhealthy sate when any database replica is not in the expected data synchronization state. The policy is otherwise in a healthy state.  
  
## Possible Causes  
 The data synchronization state of this availability database is unhealthy. On an asynchronous-commit availability replica, every availability database should be in the SYNCHRONIZING state. On a synchronous-commit replica, every availability database must be in the SYNCHRONIZED state.  
  
## Possible Solution  
 Use the database replica policy to find the database replica with an unhealthy data synchronization state, and then resolve the issue at the database replica.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](~/database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  


