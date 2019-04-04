---
title: "SQL Server Failover Cluster Installation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: c0e75a7c-85c5-423c-a218-77247bf071aa
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# SQL Server Failover Cluster Installation
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  To install a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, you must create and configure a failover cluster instance by running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup.  
  
## Installing a Failover Cluster  
 To install a failover cluster, you must use a domain account with local administrator rights, permission to log on as a service, and to act as part of the operating system on all nodes in the failover cluster. To install a failover cluster by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program, follow these steps:  
  
1.  To install, configure, and maintain a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup.  
  
    -   Identify the information you need to create your failover cluster instance (for example, cluster disk resource, IP addresses, and network name) and the nodes available for failover. For more information:  
  
        -   [Before Installing Failover Clustering](../../../sql-server/failover-clusters/install/before-installing-failover-clustering.md)  
  
        -   [Security Considerations for a SQL Server Installation](../../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
    -   The configuration steps must take place before you run the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program; use the Windows Cluster Administrator to carry them out. You must have one WSFC group for each failover cluster instance you want to configure.  
  
    -   You must ensure that your system meets minimum requirements. For more information on specific requirements for a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, see [Before Installing Failover Clustering](../../../sql-server/failover-clusters/install/before-installing-failover-clustering.md).  
  
2.  Add or remove nodes from a failover cluster configuration without affecting the other cluster nodes. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).  
  
    -   All nodes in a failover cluster must be of the same platform, either 32-bit or 64-bit, and must run the same operating system edition and version. Also, 64-bit [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] editions must be installed on 64-bit hardware running the 64-bit versions of Windows operating systems. There is no WOW64 support for failover clustering in this release.  
  
3.  Specify multiple IP addresses for each failover cluster instance. You can specify mutiple IP addresses for each subnet. If the mutiple IP addresses are on the same subnet, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup sets the dependency to AND. If you are clustering nodes across multiple subnets, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup sets the dependency to OR.  

4.  SQL Server failover cluster instance (FCI) requires the cluster nodes to be domain joined. The following configurations are **not supported**:
    - SQL FCI on workgroup clusters. 
    - SQL FCI on Multi-Domain cluster.   
    - SQL FCI on Domain + Workgroup Clusters. 

## [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Installation options  
  
##### Option 1: Integrated installation with Add Node  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] integrated failover cluster installation consists of two steps:  
  
1.  Create and configure a single-node [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance. At the completion of a successful configuration of the node, you have a fully functional failover cluster instance. At this time it does not have high-availability because there is only one node in the failover cluster.  
  
2.  On each node to be added to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, run Setup with Add Node functionality to add that node.  
  
##### Option 2: Advanced/Enterprise installation  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Advanced/Enterprise failover cluster installation consists of two steps:  
  
1.  On each node that will be part of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, run Setup with Prepare Failover Cluster functionality. This step prepares the nodes ready to be clustered, but there is no operational [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance at the end of this step.  
  
2.  After the nodes are prepared for clustering, run Setup on the node that owns the shared disk with the Complete Failover Cluster functionality. This step configures and completes the failover cluster instance. At the end of this step, you will have an operational [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance.  
  
    > [!NOTE]  
    >  Either installation option allows for multi-node [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster installation. Add Node can be used to add additional nodes for either option after a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster has been created.  
  
    > [!IMPORTANT]  
    >  The operating system drive letter for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] install locations must match on all the nodes added to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster.  
  
#### IP Address Configuration During Setup  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup lets you set or change the IP resource dependency settings during the following actions:  
  
-   Integrated Install - [Create a New SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)  
  
-   CompleteFailoverCluster (Advanced Install) - [Create a New SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)  
  
-   Add Node - [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md)  
  
-   Remove Node - [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md)  
  
 **Note** IPV6 IP addresses are supported.  If you configure both IPV4 and IPV6 there are treated like different subnets, and IPV6 is expected to come online first.  
  
##### [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Multi-Subnet Failover Cluster  
 You can set OR dependencies when the nodes on the cluster are on different subnets. However, each node in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover cluster must be a possible owner of at least one of IP address specified.  
  
## See Also  
 [Before Installing Failover Clustering](../../../sql-server/failover-clusters/install/before-installing-failover-clustering.md)   
 [Create a New SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)   
 [Install SQL Server 2016 from the Command Prompt](../../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)   
 [Upgrade a SQL Server Failover Cluster Instance](../../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md)  
  
  
