---
title: "Remove Failover Cluster Instance"
description: Use this procedure to uninstall an Always On failover cluster instance. This article includes important considerations before you proceed.
ms.custom: "seo-lt-2019"
ms.date: "12/13/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: failover-cluster-instance
ms.topic: how-to
helpviewer_keywords: 
  - "clusters [SQL Server], removing failover cluster instance"
  - "failover clustering [SQL Server], removing failover cluster instance"
  - "uninstalling failover cluster instances"
  - "removing failover cluster instances"
ms.assetid: bf63353b-69cf-4c5c-98ea-7b151e36537f
author: MashaMSFT
ms.author: mathoma
---

# Remove a failover cluster instance (Setup)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Use this procedure to uninstall an Always On [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance.  
  
> [!IMPORTANT]  
>  To update or remove a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, you must be a local administrator with permission to login as a service on all nodes of the Windows Server failover cluster.  
  
 **Before you begin**  
  
 Consider the following important points before you uninstall a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance:  
  
-   If [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is uninstalled by accident, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resources will fail to start. To reinstall [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, run the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program to install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] prerequisites.  
  
-   If you uninstall a failover cluster that has more than one SQL IP cluster resource, you must remove the additional SQL IP resources using Failover Cluster Manager or PowerShell.  
  
 For information about command prompt syntax, see [Install SQL Server 2016 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
### To uninstall a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance
  
1.  To uninstall a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, use the Remove Node functionality provided by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup to remove each node individually. For more information, see [Add or Remove Nodes in an Always On Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).  
  
## See Also  
 [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)  
  
