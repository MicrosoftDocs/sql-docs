---
title: "Set Up FILESTREAM on a Failover Cluster | Microsoft Docs"
ms.custom: ""
ms.date: "08/26/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], setting up on a failover cluster"
ms.assetid: 6721f780-20b7-4109-8ddb-ac327310699e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Set Up FILESTREAM on a Failover Cluster
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to enable FILESTREAM on a failover cluster. Before you try this procedure, you should understand [failover clustering](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md) and have FILESTREAM enabled. For information about how to enable FILESTREAM, see [Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md).  
  
### To set up FILESTREAM on a failover cluster  
  
1.  Set up the primary node for the failover cluster.  
  
     After the setup finishes, enable FILESTREAM on the primary node by using **SQL Server Configuration Manager**. This enables the settings that require Windows Admin privileges. If remote access is required, select **Allow remote clients to have streaming access to FILESTREAM data**. This will create a file-share cluster resource.  
  
2.  Set up a passive node.  
  
     After the setup finishes, enable FILESTREAM on the passive node by using **SQL Server Configuration Manager**. The name that you specify for **Windows Share Name** must be the same across all nodes in the cluster.  
  
3.  To add more passive nodes, repeat step 2.  
  
4.  After all the nodes are added, complete the process by executing the sp_configure stored procedure on each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
5.  To add and enable additional nodes to the cluster at any time, you can repeat steps 2, 3, and 4.  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [Create a New SQL Server Failover Cluster &#40;Setup&#41;](../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)   
 [Remove a SQL Server Failover Cluster Instance &#40;Setup&#41;](../../sql-server/failover-clusters/install/remove-a-sql-server-failover-cluster-instance-setup.md)   
 [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md)  
  
  
