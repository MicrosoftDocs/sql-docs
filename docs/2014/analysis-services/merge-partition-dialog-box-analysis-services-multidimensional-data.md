---
title: "Merge Partition Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.mergepartition.f1"
ms.assetid: 1c94e250-ee18-4f98-b112-985f6346102a
author: minewiskan
ms.author: owend
manager: craigg
---
# Merge Partition Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Merge Partition** dialog box in **SQL Server Management Studio** to merge partitions for a measure group in a cube. You can display the **Merge Partition** dialog box by right-clicking the Partitions folder or a partition in **Object Explorer** and selecting **Merge Partitions** from the context menu.  
  
## Options  
 **Server**  
 Select the name of the Analysis Services instance that contains the target partition.  
  
 **Name**  
 Select the name of an existing partition to use as the target partition.  
  
 **Folder**  
 Displays the name of the folder that contains the target partition, if the partition selected in Name does not use the default folder for the Analysis Services instance.  
  
 **Source Partitions**  
 Displays a grind containing the source partitions that can be merged into the target partition.  
  
> [!NOTE]  
>  Only partitions in the same measure group that share the same aggregation design can be merged.  
  
 The grid contains the following columns:  
  
|Column|Description|  
|------------|-----------------|  
|**Merge**|Select to merge the source partition into the target partition.|  
|**Partition Name**|Displays the name of the source partition.|  
|**Last Processed**|Displays the date and time at which the source partition was last processed.|  
  
## See Also  
 [Partitions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md)   
 [Merge Partitions in Analysis Services &#40;SSAS - Multidimensional&#41;](multidimensional-models/merge-partitions-in-analysis-services-ssas-multidimensional.md)  
  
  
