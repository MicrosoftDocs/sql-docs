---
title: "Availability group is not ready for automatic failover"
description: "Learn how to identify possible reasons why an Always On availability group is not ready for failover."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: reference
f1_keywords:
  - "sql13.swb.agdashboard.agp3autofailover.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---
# Always On availability group is not ready for automatic failover
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Group Automatic Failover Readiness
- **Issue**: Availability group is not ready for automatic failover.
- **Category**: **Critical**
- **Facet**: Availability group  
  
## Description  
 This policy checks to verify that the availability group has at least one secondary replica that is failover ready. The policy is in an unhealthy state and an alert is raised when the failover mode of the primary replica is automatic, however none of the secondary replicas in the availability group are failover ready.  
  
 The policy is in a healthy state when at least one secondary replica is automatic failover ready.
  
## Possible Causes  
 The availability group is not ready for [automatic failover](failover-and-failover-modes-always-on-availability-groups.md#AutomaticFailover). The primary replica is configured for automatic failover; however, the secondary replica is not ready for automatic failover. The secondary replica that is configured for automatic failover might be unavailable or its [data synchronization state is currently not SYNCHRONIZED](data-synchronization-state-of-some-availability-database-is-not-healthy.md).  
  
## Possible Solutions  
 Following are possible solutions for this issue:  
  
-   Verify that at least one secondary replica is configured as [automatic failover](failover-and-failover-modes-always-on-availability-groups.md#EnableAutoFo). If there is not a secondary replica configured as automatic failover, update the configuration of a secondary replica to be the automatic failover target with synchronous commit.  
  
-   Use the policy to verify that the data is in a synchronization state and the automatic failover target is SYNCHRONIZED, and then resolve the issue at the availability replica.  
  
## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Use the Always On Availability Group dashboard (SQL Server Management Studio)](use-the-always-on-dashboard-sql-server-management-studio.md)
