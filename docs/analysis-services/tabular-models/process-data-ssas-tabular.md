---
title: "Process Data (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d88f2dc9-2933-4be5-9bf3-48ffbc2d0a1a
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Process Data (SSAS Tabular)
  When you import data into a tabular model, in Cached mode, you are capturing a snapshot of that data at the time of import. In some cases, that data may never change and it does not need to be updated in the model. However, it is more likely that the data you are importing from changes regularly, and in order for your model to reflect the most recent data from the data sources, it is necessary to process (refresh) the data and re-calculate calculated data. To update the data in your model, you perform a process action on all tables, on an individual table, by a partition, or by a data source connection.  
  
 When authoring your model project, process actions must be initiated manually in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. After a model has been deployed, process operations can be performed by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or scheduled by using a script. Tasks described here all pertain to process actions that you can do during model authoring in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For more information about process actions for a deployed model, see [Process Database, Table, or Partition &#40;Analysis Services&#41;](../../analysis-services/tabular-models/process-database-table-or-partition-analysis-services.md).  
  
## Related Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Manually Process Data &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/manually-process-data-ssas-tabular.md)|Describes how to manually process model workspace data.|  
|[Troubleshoot Process Data &#40;SSAS Tabular&#41;](../../analysis-services/troubleshoot-process-data-ssas-tabular.md)|Describes how to troubleshoot process operations.|  
  
  