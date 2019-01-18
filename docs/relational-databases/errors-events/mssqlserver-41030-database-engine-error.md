---
title: "MSSQLSERVER_41030 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "41030 (Database Engine error)"
ms.assetid: c85341ae-0fbf-42ae-9275-4cfe678238f0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_41030
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41030|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|OPEN_CLUSTER_SUB_KEY|  
|Message Text|Failed to open the Windows Server Failover Clustering registry subkey '%.*ls' (Error code %d).  The parent key is the cluster root key.  The WSFC service may not be running or may not be accessible in its current state, or the specified arguments are invalid. If the corresponding availability group has been dropped, this error is expected. For information about this error code, see "System Error Codes" in the Windows Development documentation.|  
  
## Explanation  
If a WSFC cluster is destroyed, it removes all registry keys related to [!INCLUDE[ssHADR](../../includes/sshadr-md.md)].  
  
## User Action  
After re-creating a WSFC cluster, disable and then re-enable AlwaysOn on every cluster node on which an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is enabled for AlwaysOn. You can use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for this task.  
  
## See Also  
[Enable and Disable AlwaysOn Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)  
[Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](~/sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)  
  
