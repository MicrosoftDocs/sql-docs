---
title: "Data synchronization state of some availability database is not healthy"
description: "Identify possible causes for why the data synchronization state of some database in an Always On availability group is not healthy."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: end-user-help
ms.custom: seodec18
f1_keywords:
  - "sql13.swb.agdashboard.drp3datasynchealthy.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Data synchronization state of some availability database is not healthy
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Replica Data Synchronization State
- **Issue**: Data synchronization state of some availability database is not healthy.
- **Category**: **Warning**
- **Facet**: Availability replica  
  
## Description  
 This policy checks the data synchronization state of the availability database (also known as a "database replica"). The policy is in an unhealthy state when the data synchronization state is NOT SYNCHRONIZING or the state is not SYNCHRONIZED for the synchronous-commit database replica.   
  
## Possible Causes  
 At least one availability database on the replica has an unhealthy data synchronization state. If this is an asynchronous-commit availability replica, all availability databases should be in the SYNCHRONIZING state. If this is a synchronous-commit availability replica, all availability databases should be in the SYNCHRONIZED state. This issue can be caused by the following:  
  
-   The availability replica might be disconnected.  
  
-   The data movement might be suspended.  
  
-   The database might not be accessible.  
  
-   There might be a temporary delay issue due to network latency or the load on the primary or secondary replica.  
  
## Possible Solution  
 Resolve any connection or data movement suspend issues. Check the events for this issue using SQL Server Management Studio, and find the database error. Follow the troubleshooting steps for the specific error.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
