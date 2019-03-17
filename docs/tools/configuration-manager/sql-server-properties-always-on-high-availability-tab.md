---
title: "SQL Server Properties (Always On High Availability Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: d8630923-a600-4f1c-aca1-027453a3ec82
author: "MikeRayMSFT"
ms.author: "mikeray"
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# SQL Server Properties (Always On High Availability Tab)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  Use the **Always On High Availability** tab of the **SQL Server Properties** dialog box in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to enable or disable the Always On Availability Groups feature in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Enabling Always On Availability Groups is a prerequisite for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use availability groups as a high availability and disaster recovery solution.  
  
##  <a name="Prerequisites"></a> Prerequisites  
 To be enabled for Always On Availability Groups, a server instance must meet the following prerequisites:  
  
-   The server instance must reside on a Windows Server Failover Clustering (WSFC) node.  
  
-   To be in the same availability group, instances must be in the same WSFC cluster. An availability group cannot span WSFC clusters.  
  
-   The server instance must be running an edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that supports [!INCLUDE[ssHADR](../../includes/sshadr-md.md)].  
  
-   Enable Always On Availability Groups for only one server instance at a time. After enabling Always On Availability Groups, wait until the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service has restarted before you enable the next server instance.  
  
> [!NOTE]  
>  For information about feature support and for information about additional prerequisites, restrictions, and recommendations for [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], see [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Books Online.  
  
## Dialog Options  
 **Windows failover cluster name**  
 Displays the name of the WSFC cluster in which the local computer is a node.  
  
 **Enable Always On Availability Groups**  
 Use this check box to enable or disable Always On Availability Groups on this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], as follows:  
  
-   If this check box is empty, Always On Availability Groups is currently disabled. To enable Always On Availability Groups, select this check box, click **OK**, and manually restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
-   If this check box is already selected, Always On Availability Groups is currently enabled. To disable Always On Availability Groups, uncheck the check box and click **OK**. This causes the server instance to restart.  
  
    > [!TIP]  
    >  After disabling Always On Availability Groups, you should remove any local availability replicas from the server instance. If you remove the last replica of a given availability group, you should also remove the group.  
  
## UIElement List  
  
> [!NOTE]  
>  For more information about follow up after you disable Always On Availability Groups and for information about how to create and configure availability groups, see [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Books Online.  
  
  
