---
title: "SQL Server Multi-Subnet Clustering (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "stretch cluster"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "failover clustering [SQL Server], AlwaysOn Availability Groups"
  - "multi-site failover cluster"
  - "failover clustering [SQL Server]"
ms.assetid: cd909612-99cc-4962-a8fb-e9a5b918e221
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# SQL Server Multi-Subnet Clustering (SQL Server)
  A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover cluster is a configuration where each failover cluster node is connected to a different subnet or different set of subnets. These subnets can be in the same location or in geographically dispersed sites. Clustering across geographically dispersed sites is sometimes referred to as stretch clusters. As there is no shared storage that all the nodes can access, data should be replicated between the data storage on the multiple subnets. With data replication, there is more than one copy of the data available. Therefore, a multi-subnet failover cluster provides a disaster recovery solution in addition to high availability.  
  
 
  
##  <a name="VisualElement"></a> SQL Server Multi-Subnet Failover Cluster (Two-Nodes, Two-Subnets)  
 The following illustration represents a two node, two subnet failover cluster instance (FCI) in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
 ![Multi-Subnet Architecture with MultiSubnetFailover](../../../database-engine/media/multi-subnet-architecture-withmultisubnetfailoverparam.gif "Multi-Subnet Architecture with MultiSubnetFailover")  
  

  
##  <a name="Configurations"></a> Multi-Subnet Failover Cluster Instance Configurations  
 The following are some examples of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCIs that use multiple subnets:  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI SQLCLUST1 includes Node1 and Node2. Node1 is connected to Subnet1. Node2 is connected to Subnet2. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup sees this configuration as a multi-subnet cluster and sets the IP address resource dependency to **OR**.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI SQLCLUST1 includes Node1, Node2, and Node3. Node1 and Node2 are connected to Subnet1. Node 3 is connected to Subnet2. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup sees this configuration as a multi-subnet cluster and sets the IP address resource dependency to **OR**. Because Node1 and Node2 are on the same subnet, this configuration provides additional local high availability.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI SQLCLUST1 includes Node1 and Node2. Node1 is on Subnet1. Node2 is on Subnet1 and Subnet2. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup sees this configuration as a multi-subnet cluster and sets the IP address resource dependency to **OR**.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI SQLCLUST1 includes Node1 and Node2. Node1 is connected to Subnet1 and Subnet2. Node2 is also connected to Subnet1 and Subnet2. The IP address resource dependency is set to **AND** by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup.  
  
    > [!NOTE]  
    >  This configuration is not considered as a multi-subnet failover cluster configuration because the clustered nodes are on the same set of subnets.  
  
##  <a name="ComponentsAndConcepts"></a> IP Address Resource Considerations  
 In a multi-subnet failover cluster configuration, the IP addresses are not owned by all the nodes in the failover cluster, and may not be all online during [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] startup. Beginning in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], you can set the IP address resource dependency to **OR**. This enables [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to be online when there is at least one valid IP address that it can bind to.  
  
> [!NOTE]  
>  In the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] versions earlier than [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], a stretch V-LAN technology was used in multi-site cluster configurations to expose a single IP address for failover across sites. With the new capability of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to cluster nodes across different subnets, you can now configure [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover clusters across multiple sites without implementing the stretch V-LAN technology.  
  
### IP Address Resource OR Dependency Considerations  
 You may want to consider the following failover behavior if you set the IP address resource dependency is set to **OR**:  
  
-   When there is a failure of one of the IP addresses on the node that currently owns the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cluster resource group, a failover is not triggered automatically until all the IP addresses valid on that node fail.  
  
-   When a failover occurs, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will come online if it can bind to at least one IP address that is valid on the current node. The IP addresses that did not bind to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] at startup will be listed in the error log.  
  
  
  
 When a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI is installed side-by-side with a standalone instance of the [!INCLUDE[ssDEnoversion](../../../includes/ssdenoversion-md.md)], take care to avoid TCP port number conflicts on the IP addresses. Conflicts usually occur when two instances of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] are both configured to use the default TCP port (1433). To avoid conflicts, configure one instance to use a non-default fixed port. Configuring a fixed port is usually easiest on the standalone instance. Configuring the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] to use different ports will prevent an unexpected IP Address/TCP port conflict that blocks an instance startup when a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI fails to the standby node.  
  
##  <a name="DNS"></a> Client Recovery Latency During Failover  
 A multi-subnet FCI by default enables the RegisterAllProvidersIP cluster resource for its network name. In a multi-subnet configuration, both the online and offline IP addresses of the network name will be registered at the DNS server. The client application then retrieves all registered IP addresses from the DNS server and attempts to connect to the addresses either in order or in parallel. This means that client recovery time in multi-subnet failovers no longer depend on DNS update latencies. By default, the client tries the IP addresses in order. When the client uses the new optional `MultiSubnetFailover=True` parameter in its connection string, it will instead try the IP addresses simultaneously and connects to the first server that responds. This can help minimize the client recovery latency when failovers occur. For more information, see [AlwaysOn Client Connectivity (SQL Server)](../../../database-engine/availability-groups/windows/always-on-client-connectivity-sql-server.md)  and [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).  
  
 With legacy client libraries or third party data providers, you cannot use the `MultiSubnetFailover` parameter in your connection string. To help ensure that your client application works optimally with multi-subnet FCI in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], try to adjust the connection timeout in the client connection string by 21 seconds for each additional IP address. This ensures that the client's reconnection attempt does not timeout before it is able to cycle through all IP addresses in your multi-subnet FCI.  
  
 The default client connection time-out period for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Studio and **sqlcmd** is 15 seconds.  
  
 
  
##  <a name="RelatedContent"></a> Related Content  
  
|Content Description|Topic|  
|-------------------------|-----------|  
|Installing a SQL Server Failover Cluster|[Create a New SQL Server Failover Cluster &#40;Setup&#41;](../install/create-a-new-sql-server-failover-cluster-setup.md)|  
|In-place upgrade of your existing SQL Server Failover Cluster|[Upgrade a SQL Server Failover Cluster Instance &#40;Setup&#41;](upgrade-a-sql-server-failover-cluster-instance-setup.md)|  
|Maintaining your existing SQL Server Failover Cluster|[Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md)|  
|Windows Failover Clustering|[Windows 2008 R2 Failover Multi-Site Clustering](https://www.microsoft.com/windowsserver2008/en/us/failover-clustering-multisite.aspx)|  
|Use the Failover Cluster Management snap-in to view WSFC events and logs|[View Events and Logs for a Failover Cluster](https://technet.microsoft.com/library/cc772342\(WS.10\).aspx)|  
|Use Windows PowerShell to create a log file for all nodes (or a specific a node) in a WSFC failover cluster|[Get-ClusterLog Failover Cluster Cmdlet](https://technet.microsoft.com/library/ee461045.aspx)|  
  
 
  
  
