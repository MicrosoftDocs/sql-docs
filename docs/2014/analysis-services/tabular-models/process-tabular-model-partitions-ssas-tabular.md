---
title: "Process Tabular Model Partitions (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 6c498d2b-22d6-4661-bc21-2ee708336c8b
author: minewiskan
ms.author: owend
manager: craigg
---
# Process Tabular Model Partitions (SSAS Tabular)
  Partitions divide a table into logical parts. Each partition can then be processed (Refreshed) independent of other partitions. The tasks in this topic describe how to process partitions in a model database by using the **Process Partitions** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
###  <a name="bkmk_create_new"></a> To process a partition  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click on the table that has the partitions you want to process, and then click **Partitions**.  
  
2.  In the **Partitions** dialog box, in **Partitions**, click on the Process button.  
  
3.  In the **Process Partition(s)** dialog box, in the **Mode** listbox, select one of the following process modes:  
  
    |Mode|Description|  
    |----------|-----------------|  
    |**Process Default**|Detects the process state of a partition object, and performs processing necessary to deliver unprocessed or partially processed partition objects to a fully processed state. Data for empty tables and partitions is loaded; hierarchies, calculated columns, and relationships are built or rebuilt.|  
    |**Process Full**|Processes a partition object and all the objects that it contains. When Process Full is run for an object that has already been processed, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] drops all data in the object, and then processes the object. This kind of processing is required when a structural change has been made to an object.|  
    |**Process Data**|Load data into a partition or a table without rebuilding hierarchies or relationships or recalculating calculated columns and measures.|  
    |**Process Clear**|Removes all data from a partition.|  
    |**Process Add**|Incrementally update partition with new data.|  
  
4.  In the **Process** checkbox column, select the partitions you want to process with the selected mode, and then click **Ok**.  
  
## See Also  
 [Tabular Model Partitions &#40;SSAS Tabular&#41;](partitions-ssas-tabular.md)   
 [Create and Manage Tabular Model Partitions &#40;SSAS Tabular&#41;](create-and-manage-tabular-model-partitions-ssas-tabular.md)  
  
  
