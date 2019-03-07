---
title: "Partitions (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 708b9bdf-8c0b-4476-809a-8f616be23a58
author: minewiskan
ms.author: owend
manager: craigg
---
# Partitions (SSAS Tabular)
  Partitions divide a table into logical parts. Each partition can then be processed (Refreshed) independent of other partitions. Partitions created by using the Partitions dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] during model authoring apply to the model workspace database. When the model is deployed, the partitions defined for the model workspace database are duplicated in the deployed model database. You can further create and manage partitions for a deployed model database by using the Partitions dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  Information provided in this topic describes partitions created during model authoring by using the Partition Manager dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For information about creating and managing partitions for a deployed model, see [Create and Manage Tabular Model Partitions &#40;SSAS Tabular&#41;](create-and-manage-tabular-model-partitions-ssas-tabular.md).  
  
 Sections in this topic:  
  
-   [Benefits](#bkmk_benefits)  
  
-   [Related Tasks](#bkmk_related_tasks)  
  
##  <a name="bkmk_benefits"></a> Benefits  
 Partitions, in tabular models, divide a table into logical partition objects. Each partition can then be processed independent of other partitions. For example, a table may include certain row sets that contain data that rarely changes, but other row sets have data that changes often. In these cases, there is no need to process all of the data when you really just want to process a portion of the data. Partitions enable you to divide portions of data you need to process frequently from the data that can be processed less frequently.  
  
 Effective model design utilizes partitions to eliminate unnecessary processing and subsequent processor load on Analysis Services servers, while at the same time, making certain that data is processed and refreshed often enough to reflect the most recent data from data sources. How you implement and utilize partitions during model authoring can be very different from how partitions are implemented and utilized for deployed models. Keep in-mind that during the model authoring phase, you may be working with only a subset of the data that will ultimately be in your deployed model.  
  
### Processing Partitions  
 For deployed models, processing occurs by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or by running a script which includes the process command and specifies processing options and settings. When authoring models by using [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], you can run process operations by using a Process command from the Model menu or toolbar. A Process operation can be specified for a partition, a table, or all.  
  
 When a process operation is run, a connection to the data source is made using the data connection. New data is imported into the model tables, relationships and hierarchies are rebuilt for each table, and calculations in calculated columns and measures are re-calculated.  
  
 By further dividing a table into logical partitions, you can selectively determine what, when, and how the data in each partition is processed. When you deploy a model, processing of partitions can be completed manually using the Partitions dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or by using a script that executes a process command.  
  
### Partitions in the Model Workspace Database  
 You can create new partitions, edit, merge, or delete partitions using the Partition Manager in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. Partition Manager provides two modes for selecting tables, rows, and columns for a partition: Table Preview mode and SQL query mode. All partitions are defined by using a SQL query; however, by using the Table Preview mode, you can preview and select the data you want to include in the partition. The SQL query is automatically created and validated for you. Because the Table Preview mode is the same table preview that is in the Edit Table Properties dialog box and in the Table Preview page of the Table Import Wizard, the maximum number of rows in preview is 50.  
  
### Partitions in a deployed model database  
 When you deploy a model, the partitions for the deployed model database will appear as database objects in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You can create, edit, merge, and delete partitions for a deployed model by using the Partitions dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Managing partitions for a deployed model in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is outside the scope of this topic. To learn about managing partitions in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], see [Create and Manage Tabular Model Partitions &#40;SSAS Tabular&#41;](create-and-manage-tabular-model-partitions-ssas-tabular.md).  
  
##  <a name="bkmk_related_tasks"></a> Related Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create and Manage Partitions in the Workspace Database &#40;SSAS Tabular&#41;](workspace-database-ssas-tabular.md)|Describes how to create and manage partitions in the model workspace database by using Partition Manager in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].|  
|[Process Partitions in the Workspace Database &#40;SSAS Tabular&#41;](process-partitions-in-the-workspace-database-ssas-tabular.md)|Describes how to process (refresh) partitions in the model workspace database.|  
  
## See Also  
 [DirectQuery Mode &#40;SSAS Tabular&#41;](directquery-mode-ssas-tabular.md)   
 [Process Data &#40;SSAS Tabular&#41;](../process-data-ssas-tabular.md)  
  
  
