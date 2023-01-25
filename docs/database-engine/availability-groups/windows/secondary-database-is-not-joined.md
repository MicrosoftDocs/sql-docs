---
title: "Secondary database is not joined"
description: Availability Database Join State checks the join state of the secondary database as part of policy based management for Always On availability groups.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.agdashboard.drp2joined.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Secondary database is not joined
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Database Join State
- **Issue**: Secondary database is not joined.
- **Category**: **Warning**
- **Facet**: Availability database  
  
## Description  
 This policy checks the join state of the secondary database (also known as a "secondary database replica"). The policy is in an unhealthy state when the dataset replica is not joined. The policy is otherwise in a healthy state.  

## Possible Causes  
 This secondary database is not joined to the availability group. The configuration of this secondary database is incomplete.  
  
## Possible Solution  
 Use Transact-SQL, PowerShell, or SQL Server Management Studio to join the secondary replica to the availability group. For more information about joining secondary replicas to availability groups, see [Joining a Secondary Replica to an Availability Group (SQL Server)](https://msdn.microsoft.com/library/ff878473\(SQL.110\).aspx).  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
