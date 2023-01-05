---
title: "Availability database is suspended for an availability group"
description: "Identify possible causes for why a database in an Always On availability group might be suspended."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: end-user-help
ms.custom: seodec18
f1_keywords:
  - "sql13.swb.agdashboard.drp1notsuspended.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Availability database is suspended for an availability group
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Database Suspension State
- **Issue**: Availability database is suspended.
- **Category**: **Warning**
- **Facet**: Availability database  
  
## Description  
 This policy checks the state of data movement of the secondary database (also known as a "secondary database replica"). The policy is in an unhealthy state when the data movement is suspended. The policy is otherwise in a healthy state.  
  
## Possible Causes  
 Data synchronization on this availability database might have been suspended because of the following:  
  
-   Due to an error, the system might have suspended data synchronization.  
  
-   The database administrator might have suspended data synchronization for maintenance purposes.  
  
## Possible Solution  
 Resume data synchronization. If the issue persists, check the availability group in the Event log, and then diagnose why the system suspended data movement.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
