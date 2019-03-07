---
title: "Processing and Storage Locations (Partition Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.partitionwizard.specifyprocessingandstorage.f1"
ms.assetid: dda2dc57-923d-4db9-93a7-38e95770f3df
author: minewiskan
ms.author: owend
manager: craigg
---
# Processing and Storage Locations (Partition Wizard)
  Use the **Processing and Storage Locations** page to specify the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance of the cube that owns the partition, as well as the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance that stores the data for the partition. You can define a partition as a remote partition by specifying either a remote [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance or a storage location other than the default storage location. For more information about remote partitions, see [Remote Partitions](multidimensional-models-olap-logical-cube-objects/partitions-remote-partitions.md).  
  
## Processing location Options  
 **Current server instance**  
 Makes the current [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance responsible for processing the partition.  
  
 **Remote Analysis Services data source**  
 Makes a remote [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance responsible for processing this partition.  
  
 From the dropdown list, select the data source representing the remote [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance that will be responsible for processing the partition.  
  
> [!NOTE]  
>  An error occurs if you select a data source in which the `Initial Catalog` connection string property is not set to a valid [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, or if the database specified in the `Initial Catalog` connection string property does not support remote partitions (in other words, the `MasterDatasourceID` property of the specified database is not set to a valid value).  
  
 **New**  
 Creates a new data source representing the remote [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance responsible for processing the partition.  
  
## Storage location Options  
 **Default server location**  
 Makes the data folder of the current [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance the storage location of the aggregation and indexing data for the partition.  
  
 **Specified folder**  
 Specifies the storage location of the aggregation and indexing data for the partition.  
  
 **...**  
 Displays the **Browse for Remote Folder** dialog box in which you can select a folder for **Specified folder**.  
  
## See Also  
 [Partition Wizard F1 Help &#40;Analysis Services - Multidimensional Data&#41;](partition-wizard-f1-help-analysis-services-multidimensional-data.md)   
 [Partitions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md)   
 [Browse for Remote Folder Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](browse-for-remote-folder-dialog-box-analysis-services-multidimensional-data.md)  
  
  
