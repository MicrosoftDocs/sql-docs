---
title: "Remote Partitions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "storage [Analysis Services], partitions"
  - "archiving remote partitions [Analysis Services]"
  - "partitions [Analysis Services], remote"
  - "restoring remote partitions [Analysis Services]"
  - "backing up remote partitions [Analysis Services]"
  - "partitions [Analysis Services], storage"
  - "storing data [Analysis Services], partitions"
  - "MasterDataSourceID property"
  - "remote partitions [Analysis Services]"
ms.assetid: 63f5d9f5-c6b6-4ceb-94fe-7b6c396d10bb
author: minewiskan
ms.author: owend
manager: craigg
---
# Remote Partitions
  The data of a remote partition is stored on a different instance of Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] than the instance that contains the definitions (metadata) of the partition and its parent cube. A remote partition is administered on the same instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] where the partition and its parent cube are defined.  
  
> [!NOTE]  
>  To store a remote partition, the computer must have an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] installed and be running the same service pack level as the instance where the partition was defined. Remote partitions on instances of an earlier version of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] are not supported.  
  
 When remote partitions are included in a measure group, the memory and CPU utilization of the cube is distributed across all the partitions in the measure group. For example, when a remote partition is processed, either alone or as part of parent cube processing, most of the memory and CPU utilization for that partition occurs on the remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
> [!NOTE]  
>  A cube that contains remote partitions can contain write-enabled dimensions; however, this may affect performance for the cube. For more information about write-enabled dimensions, see [Write-Enabled Dimensions](../multidimensional-models-olap-logical-dimension-objects/write-enabled-dimensions.md).  
  
## Storage Modes for Remote Partitions  
 Remote partitions may use any of the storage types used by local partitions: multidimensional OLAP (MOLAP), hybrid OLAP (HOLAP), or relational OLAP (ROLAP). Remote partitions may also use proactive caching. Depending on the storage mode of a remote partition, the following data is stored on the remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
|||  
|-|-|  
|Storage Type|Data|  
|MOLAP|The partition's aggregations and a copy of the partition's source data|  
|HOLAP|The partitions aggregations|  
|ROLAP|No partition data|  
  
 If a measure group contains multiple MOLAP or HOLAP partitions stored on multiple instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the cube distributes the data in the measure group data among those instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
## Merging Remote Partitions  
 Remote partitions can be merged only with other remote partitions that are stored on the same remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For more information about merging partitions, see [Merge Partitions in Analysis Services &#40;SSAS - Multidimensional&#41;](../multidimensional-models/merge-partitions-in-analysis-services-ssas-multidimensional.md).  
  
## Archiving and Restoring Remote Partitions  
 Data in remote partitions can be archived or restored when the database that stores the remote partition is archived or restored. If you restore a database without restoring a remote partition, you must process the remote partition before you can use the data in the partition. For more information about archiving and restoring databases, see [Backup and Restore of Analysis Services Databases](../multidimensional-models/backup-and-restore-of-analysis-services-databases.md).  
  
## See Also  
 [Create and Manage a Remote Partition &#40;Analysis Services&#41;](../multidimensional-models/create-and-manage-a-remote-partition-analysis-services.md)   
 [Processing Analysis Services Objects](../multidimensional-models/processing-analysis-services-objects.md)  
  
  
