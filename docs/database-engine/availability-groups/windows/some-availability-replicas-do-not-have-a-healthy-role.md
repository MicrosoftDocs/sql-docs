---
title: "Some availability replicas do not have a healthy role"
description: Availability Replicas Role State checks if there are any availability replicas that are not in a healthy role.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: reference
f1_keywords:
  - "sql13.swb.agdashboard.agp6allroleshealthy.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Some availability replicas do not have a healthy role
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Replicas Role State
- **Issue**: Some availability replicas do not have a healthy role.
- **Category**: **Warning**
- **Facet**: Availability group  
  
## Description  
 This policy rolls up the connection state of all availability replicas and checks if there are any availability replicas that are not in a healthy role. The policy is in an unhealthy state when any availability replica is neither primary nor secondary. The policy is otherwise in a healthy state.  
  
## Possible Causes  
 In this availability group, at least one availability replica does not currently have the primary or secondary role.  
  
## Possible Solution  
 Use the availability replica policy state to find the availability replica whose role is not primary or secondary, and then resolve the issue at the availability replica.  
  
## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Use the Always On Availability Group dashboard (SQL Server Management Studio)](use-the-always-on-dashboard-sql-server-management-studio.md)
