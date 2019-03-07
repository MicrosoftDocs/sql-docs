---
title: "Cluster Network Configuration | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "cluster network selection, Setup"
  - "cluster network selection"
ms.assetid: 579482ef-a023-45b2-9176-b4a4188adf9d
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Cluster Network Configuration
  Use the **Cluster Network Selection** page to specify the network resources for your failover cluster instance.  
  
## Options  
 **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Network Name** - This is the name used to identify your failover cluster instance on the network.  
  
 **Network Settings** - Specify the IP type and IP address for your failover cluster instance.  
  
 During the Add Node and Remove Node operations, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] displays a read-only list of the existing IP addresses for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster.  
  
-   Integrated Install:  
  
    -   If you are adding a node that supports an identical set of network subnets supported by the existing nodes of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, no additional IP addresses can be added. The dependency setting remains unchanged.  
  
    -   If you are adding a node that supports a subset of the subnets supported by the existing nodes on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, no additional IP address resources can be added. The IP address resource dependency is set to OR to reflect that all the specified IP addresses are not valid on all the cluster nodes.  
  
    -   If you are adding a node that supports subnets in addition to the subnets already supported by the existing nodes on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, you have the option of adding new valid IP addresses. If new IP addresses are specified, the IP address resource dependency is set to OR to reflect that all the specified IP addresses are not valid on all the cluster nodes.  
  
    -   If you are adding a node that supports additional network subnets, but supports none of the subnets supported by the existing nodes on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, you are required to add additional IP addresses. The IP address resource dependency is set to OR to reflect that all the specified IP addresses are not valid on all the cluster nodes.  
  
-   Advanced Install: During the Complete step of the installation, specify the IP address for all the nodes and subnets for your failover cluster instance. You can specify multiple IP addresses for a multi-subnet failover cluster, but only one IP address per subnet is supported. Every prepared node should be an owner of at least one IP address. If you have multiple subnets in your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, you are prompted to set the IP address resource dependency to OR.Remove Node:  
  
    -   If the remaining IP addresses are supported on all the remaining nodes, you are prompted to set the IP address resource dependencyto AND.  
  
    -   If the remaining IP addresses are not supported on all the remaining nodes, the IP address resource dependency is left as OR.  
  
  
