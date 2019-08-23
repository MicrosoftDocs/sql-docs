---
title: "Remove a SQL Server Failover Cluster Instance (Setup) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "clusters [SQL Server], removing failover clustered instance"
  - "failover clustering [SQL Server], removing failover clustered instance"
  - "uninstalling failover clustered instances"
  - "removing failover clustered instances"
ms.assetid: bf63353b-69cf-4c5c-98ea-7b151e36537f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Remove a SQL Server Failover Cluster Instance (Setup)
  Use this procedure to uninstall a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover clustered instance.  
  
> [!IMPORTANT]  
>  To update or remove a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, you must be a local administrator with permission to login as a service on all nodes of the failover cluster.  
  
 **Before you begin**  
  
 Consider the following important points before you uninstall a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster:  
  
-   If [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is uninstalled by accident, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resources will fail to start. To reinstall [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, run the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program to install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] prerequisites.  
  
-   If you uninstall a failover cluster that has more than one SQL IP cluster resource, you must remove the additional SQL IP resources using cluster administrator.  
  
 For information about command prompt syntax, see [Install SQL Server 2014 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
### To uninstall a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster  
  
1.  To uninstall a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, use the Remove Node functionality provided by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup to remove each node individually. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).  
  
## See Also  
 [View and Read SQL Server Setup Log Files](../../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)  
  
  
