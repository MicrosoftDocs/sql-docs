---
title: "Upgrade a SQL Server Failover Cluster | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading failover clusters"
  - "clusters [SQL Server], upgrading"
  - "failover clustering [SQL Server], upgrading"
ms.assetid: daac41fe-7d0b-4f14-84c2-62952ad8cbfa
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Upgrade a SQL Server Failover Cluster
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports upgrade of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] from [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)], and [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] failover clusters separately on all failover cluster nodes.  
  
 Support details are as follows:  
  
-   Upgrade is supported both through the user interface and from the command prompt. For more information, see [Upgrade a SQL Server Failover Cluster Instance &#40;Setup&#41;](upgrade-a-sql-server-failover-cluster-instance-setup.md) and [Install SQL Server 2014 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
-   Upgrading from [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] - You can run upgrade from the command prompt on each failover cluster node, or by using the Setup UI to upgrade each cluster node. If Full-text search and Replication features do not exist on the instance being upgraded, they will be installed automatically with no option to omit them.  
  
-   Service pack installation - you must apply [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service packs and patches to [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] failover clusters separately on all nodes.  
  
-   The following scenarios are not supported:  
  
    -   You cannot migrate from a stand-alone instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to a failover cluster.  
  
    -   Add features to a failover cluster. For example, you cannot add the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] to an existing [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]-only failover cluster.  
  
    -   You cannot downgrade a failover cluster node to a stand-alone instance.  
  
-   For more information, see [ AlwaysOn Failover Cluster Instances (SQL Server)](always-on-failover-cluster-instances-sql-server.md).  
  
## Upgrading a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Multi-Subnet Failover Cluster  
 You cannot directly upgrade a non-multi-subnet [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover cluster. For more information, see [Upgrade a SQL Server Failover Cluster Instance &#40;Setup&#41;](upgrade-a-sql-server-failover-cluster-instance-setup.md).  
  
## See Also  
 [Supported Version and Edition Upgrades](../../../database-engine/install-windows/supported-version-and-edition-upgrades.md)   
 [Upgrade a SQL Server Failover Cluster Instance &#40;Setup&#41;](upgrade-a-sql-server-failover-cluster-instance-setup.md)   
 [Install SQL Server 2014 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)  
  
  
