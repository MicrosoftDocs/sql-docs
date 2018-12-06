---
title: "Integration Services (SSIS) in a Cluster | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 0216266d-d866-4ea2-bbeb-955965f4d7c2
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Integration Services (SSIS) in a Cluster
  Clustering [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is not recommended because the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is not a clustered or cluster-aware service, and does not support failover from one cluster node to another. Therefore, in a clustered environment, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] should be installed and started as a stand-alone service on each node in the cluster.  
  
 Although the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is not a clustered service, you can manually configure the service to operate as a cluster resource after you install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] separately on each node of the cluster.  
  
 However, if high availability is your goal in establishing a clustered hardware environment, you can achieve this goal without configuring the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service as a cluster resource.  To manage your packages on any node in the cluster from any other node in the cluster, modify the configuration file for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on each node in the cluster. You modify each of these configuration files to point to all available instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on which packages are stored. This solution provides the high availability that most customers need, without the potential problems encountered when the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is configured as a cluster resource. For more information about how to change the configuration file, see [Configuring the Integration Services Service &#40;SSIS Service&#41;](integration-services-service-ssis-service.md)  
  
 Understanding the role of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is critical to making an informed decision about how to configure the service in a clustered environment. For more information, see [Integration Services Service &#40;SSIS Service&#41;](integration-services-service-ssis-service.md).  
  
## Understanding the Disadvantages of Configuring Integration Services as a Cluster Resource  
 Some of the potential disadvantages of configuring the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service as a cluster resource include the following:  
  
-   When a failover occurs, running packages do not restart. You can recover from package failures by restarting packages from checkpoints. You can restart from checkpoints without configuring the service as a cluster resource. For more information, see [Restart Packages by Using Checkpoints](../packages/restart-packages-by-using-checkpoints.md).  
  
-   When you configure the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service in a different resource group from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you cannot use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] from client computers to manage packages that are stored in the msdb database. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service cannot delegate credentials in this double-hop scenario.  
  
-   When you have multiple [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource groups that include the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service in a cluster, a failover could lead to unexpected results. Consider the following scenario. Group1, which includes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service and the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service, is running on Node A. Group2, which also includes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service and the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service, is running on Node B. Group 2 fails over to Node A. The attempt to start another instance of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on Node A fails because the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is a single-instance service. Whether the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service that is trying to fail over to Node A also fails depends on the configuration of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service in Group 2. If the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service was configured to affect the other services in the resource group, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service that is failing over will fail because the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service failed. If the service was configured not to affect the other services in the resource group, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service will be able to fail over to Node A.Unless the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service in Group 2 was configured not to affect the other services in the resource group, the failure of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service that is failing over could cause the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service that is failing over to fail also.  
  
## Related Tasks  
 For step-by-step instructions on configuring the Integration Services service in a cluster, see [Configure the Integration Services Service as a Cluster Resource](../configure-the-integration-services-service-as-a-cluster-resource.md).  
  
  
