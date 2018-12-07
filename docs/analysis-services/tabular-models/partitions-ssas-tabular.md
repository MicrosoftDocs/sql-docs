---
title: "Partitions in Analysis Services tabular models | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Partitions
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Partitions divide a table into logical parts. Each partition can then be processed (Refreshed) independent of other partitions. Partitions created by using the Partitions dialog box in SSDT during model authoring apply to the model workspace database. When the model is deployed, the partitions defined for the model workspace database are duplicated in the deployed model database. You can further create and manage partitions for a deployed model database by using the Partitions dialog box in SSMS.  Information provided in this topic describes partitions created during model authoring by using the Partition Manager dialog box in SSDT. For information about creating and managing partitions for a deployed model, see [Create and Manage Tabular Model Partitions](../../analysis-services/tabular-models/create-and-manage-tabular-model-partitions-ssas-tabular.md).  
  
##  <a name="bkmk_benefits"></a> Benefits  
 Partitions, in tabular models, divide a table into logical partition objects. Each partition can then be processed independent of other partitions. For example, a table may include certain row sets that contain data that rarely changes, but other row sets have data that changes often. In these cases, there is no need to process all of the data when you really just want to process a portion of the data. Partitions enable you to divide portions of data you need to process frequently from the data that can be processed less frequently.  
  
 Effective model design utilizes partitions to eliminate unnecessary processing and subsequent processor load on Analysis Services servers, while at the same time, making certain that data is processed and refreshed often enough to reflect the most recent data from data sources. How you implement and utilize partitions during model authoring can be very different from how partitions are implemented and utilized for deployed models. Keep in-mind that during the model authoring phase, you may be working with only a subset of the data that will ultimately be in your deployed model.  
  
### Processing partitions  
 For deployed models, processing occurs by using SSMS, or by running a script which includes the process command and specifies processing options and settings. When authoring models by using SSDT, you can run process operations by using a Process command from the Model menu or toolbar. A Process operation can be specified for a partition, a table, or all.  
  
 When a process operation is run, a connection to the data source is made using the data connection. New data is imported into the model tables, relationships and hierarchies are rebuilt for each table, and calculations in calculated columns and measures are re-calculated.  
  
 By further dividing a table into logical partitions, you can selectively determine what, when, and how the data in each partition is processed. When you deploy a model, processing of partitions can be completed manually using the Partitions dialog box in SSMS, or by using a script that executes a process command.  
  
### Partitions in the model workspace database  
 You can create new partitions, edit, merge, or delete partitions using the Partition Manager in SSDT. Depending on the compatibility level of the model you are authoring, Partition Manager provides two modes for selecting tables, rows, and columns for a partition: For tabular 1400 models, partitions are defined by using an M query, or you can use Design mode to open Query Editor. For tabular 1100, 1103, 1200 models, use Table Preview mode and SQL query mode. 
  
### Partitions in a deployed model database  
 When you deploy a model, the partitions for the deployed model database will appear as database objects in SSMS. You can create, edit, merge, and delete partitions for a deployed model by using the Partitions dialog box in SSMS. Managing partitions for a deployed model in SSMS is outside the scope of this topic. To learn about managing partitions in SSMS, see [Create and Manage Tabular Model Partitions](../../analysis-services/tabular-models/create-and-manage-tabular-model-partitions-ssas-tabular.md).  
  
##  <a name="bkmk_related_tasks"></a> Related tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create and Manage Partitions in the Workspace Database](../../analysis-services/tabular-models/create-and-manage-partitions-in-the-workspace-database-ssas-tabular.md)|Describes how to create and manage partitions in the model workspace database by using Partition Manager in SSDT.|  
|[Process Partitions in the Workspace Databse](../../analysis-services/tabular-models/process-partitions-in-the-workspace-databse-ssas-tabular.md)|Describes how to process (refresh) partitions in the model workspace database.|  
  
## See also  
 [DirectQuery Mode](../../analysis-services/tabular-models/directquery-mode-ssas-tabular.md)   
 [Process Data](../../analysis-services/tabular-models/process-data-ssas-tabular.md)  
  
  
