---
title: "Drill Through Dialog Box (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.drillthrough.f1"
ms.assetid: 42b78399-143d-4f44-90e0-b545ffb79e10
author: minewiskan
ms.author: owend
manager: craigg
---
# Drill Through Dialog Box (Mining Model Viewer)
  When you view a mining model by using the **Mining Model Viewer** tab of Data Mining Designer, you can drill through into details about the case data, provided the model has drillthrough enabled. Moreover, if the underlying mining structure has drillthrough enabled, you can also see columns in the mining structure, even if those columns were not included in the mining model. In the column list, the structure columns are prefixed by the "Structure." label.  
  
> [!NOTE]  
>  You cannot enable drillthrough on an existing mining structure. Instead, you must re-create the mining structure and enable drillthrough during the creation process.  
  
 For information about how to access case data from each of the mining model viewers that support drillthrough, **see** [Drill Through to Case Data from a Mining Model](data-mining/drill-through-to-case-data-from-a-mining-model.md).  
  
## Options  
 **Cases Classified To**  
 Displays the definition of the rule, itemset, and cluster that are contained in the selected node.  
  
 **Column list**  
 Displays the columns in the model, followed by the structure columns.  
  
 **Note** Structure columns are displayed only if drillthrough is enabled on the mining structure, and if you selected the option, **Model and Structure Columns**. Moreover, you must have drillthrough permissions on both the mining model and the mining structure to view the columns.  
  
 Structure columns that are not included in the model appear as **Structure.\<column name>**.  
  
> [!NOTE]  
>  You can right-click anywhere in the column grid and select **Copy All** to copy the drillthrough data, in tab-delimited format, to the Clipboard. The copied data includes only the case data, not the node definition.  
  
 **Play**  
 Click the green arrow button to refresh the data.  
  
## See Also  
 [Drillthrough Queries &#40;Data Mining&#41;](data-mining/drillthrough-queries-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Mining Model Viewer Tasks and How-tos](data-mining/mining-model-viewer-tasks-and-how-tos.md)  
  
  
