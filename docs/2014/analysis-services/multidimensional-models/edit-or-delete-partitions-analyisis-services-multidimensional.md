---
title: "Edit or Delete Partitions (Analyisis Services - Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying partitions"
  - "partitions [Analysis Services], modifying"
ms.assetid: fb7a64ca-d021-4926-b92d-83476fbc40a3
author: minewiskan
ms.author: owend
manager: craigg
---
# Edit or Delete Partitions (Analyisis Services - Multidimensional)
  Cube partitions are modified using the **Partitions** tab in Cube Designer in [!INCLUDE[ssBIDevStudio](../../../includes/ssbidevstudio-md.md)]. The **Partitions** tab lists the partitions for all of the measure groups in a cube. It also lists the writeback partitions that have writeback enabled.  
  
 To edit the partitions for any measure group, expand the measure group on the **Partitions** tab. Partitions for a measure group are listed by ordinal number in a table format with the columns listed in the following table.  
  
 Settings for a linked measure group must be edited in the source cube.  
  
 Deleting partitions occurs automatically when you merge a source partition into a destination partition. The partition specified as the Source is deleted after the merge is completed. You can also delete partitions manually in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or in the Partitions tab in [!INCLUDE[ssBIDevStudio](../../../includes/ssbidevstudio-md.md)]. Right-click and choose **Delete**. Remember that deleting a partition also deletes data and aggregations. As a precaution, make sure you have a recent back up of the database in case you need to reverse this step later.  
  
> [!NOTE]  
>  Alternatively, you can use XMLA scripts that automate tasks for building, merging, and deleting partitions. XMLA script can be created and executed in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], or in custom SSIS packages that runs as a scheduled task. For more information, see [Automate Analysis Services Administrative Tasks with SSIS](../instances/automate-analysis-services-administrative-tasks-with-ssis.md).  
  
## Partition source  
 Specifies the source table or named query for the partition. To change the source table, click the cell and then click the browse (**...**) button.  
  
 ![Source column in Partition pane](../media/ssas-partitionsource.png "Source column in Partition pane")  
  
 If the partition is based on a query, click the browse (**...**) button to edit the query. This edits the **Source** property for the partition. For more information, see [Change a partition source to use a different fact table](change-a-partition-source-to-use-a-different-fact-table.md).  
  
 You can specify a table in the data source view that has the same structure as the original source table (in the external data source from which the data is retrieved). The source can be in any of the data sources or data source views of the cube database.  
  
## Storage settings  
 In Cube Designer, on the Partitions tab, you can click **Storage Settings** to pick one of the standard settings for MOLAP, ROLAP, or HOLAP storage, or to configure custom settings for the storage mode and proactive caching. The default is MOLAP because it delivers the fastest query performance. For more information about each setting, see [Set Partition Storage &#40;Analysis Services - Multidimensional&#41;](set-partition-storage-analysis-services-multidimensional.md).  
  
 Storage can be configured separately for each partition of each measure group in a cube. You can also configure the default storage settings for a cube or measure group. Storage is configured on the **Partitions** tab in the Cube Wizard.  
  
## See Also  
 [Create and Manage a Local Partition &#40;Analysis Services&#41;](create-and-manage-a-local-partition-analysis-services.md)   
 [Designing Aggregations &#40;Analysis Services - Multidimensional&#41;](designing-aggregations-analysis-services-multidimensional.md)   
 [Merge Partitions in Analysis Services &#40;SSAS - Multidimensional&#41;](merge-partitions-in-analysis-services-ssas-multidimensional.md)  
  
  
