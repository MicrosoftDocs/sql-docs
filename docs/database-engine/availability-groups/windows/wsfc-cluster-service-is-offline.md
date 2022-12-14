---
title: "WSFC cluster service is offline"
description: WSFC Cluster State checks the state of the Windows Server Failover Cluster. The policy is unhealthy when the cluster is offline or in the forced quorum state.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.agdashboard.agp1WSFCquorum.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
monikerRange: ">=sql-server-2016"
---
# WSFC cluster service is offline

[!INCLUDE[sql windows only](../../../includes/applies-to-version/sql-windows-only.md)]
    
## Introduction  
  
- **Policy Name**: WSFC Cluster State
- **Issue**: WSFC cluster service is offline.
- **Category**: **Critical**
- **Facet**: Instance of SQL Server  
  
## Description  
 This policy checks the state of the Windows Server Failover Cluster (WSFC). The policy is in an unhealthy state and an alert is raised when the WSFC cluster is offline or in the forced quorum state. All availability groups hosted within this cluster are offline or a disaster recovery action is required.  
  
 The policy state is healthy when the cluster state is in the normal quorum.

## Possible Causes  
 This issue can be caused by a cluster service issue or by the loss of the quorum in the cluster.  
  
## Possible Solution  
 Use the Cluster Administrator tool to perform the forced quorum or disaster recovery workflow. If you cannot resolve the issue by performing the forced quorum or disaster recovery, contact your cluster administrator to help resolve this issue. For more information, see [Force a WSFC Cluster to Start Without a Quorum](../../../sql-server/failover-clusters/windows/force-a-wsfc-cluster-to-start-without-a-quorum.md) in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
