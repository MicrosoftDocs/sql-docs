---
title: "Drill Through to Case Data from a Mining Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "drillthrough [Analysis Services]"
ms.assetid: b4d3f350-e543-4ea9-b3a2-b4f7c0a9ae27
author: minewiskan
ms.author: owend
manager: craigg
---
# Drill Through to Case Data from a Mining Model
  If a mining model has been configured to let you drill through to model cases, when you browse the model, you can retrieve detailed information about the cases that were used to create the model. Moreover, if the underlying mining structure has been configured to allow drillthrough to structure cases, and you have the appropriate permissions, you can return information from the mining structure. This can include columns that were not included in the mining model.  
  
 If the mining structure does not allow you to drill through to the underlying data, but the mining model does, you can view information from the model cases, but not from the mining structure.  
  
> [!NOTE]  
>  You can add the ability to drillthrough on an existing mining model by setting the property `AllowDrillthrough` to `True`. However, after you enable drillthrough, the model must be reprocessed before you can see the case data. For more information, see [Enable Drillthrough for a Mining Model](enable-drillthrough-for-a-mining-model.md).  
  
 Depending on the type of viewer you are using, you can select the node for drillthrough in the following ways:  
  
|Viewer name|Pane or tab name|Select node|  
|-----------------|----------------------|-----------------|  
|**Microsoft Tree Viewer**|**Decision Tree** tab|Click a tree node.<br /><br /> **Note** Avoid using drillthrough on the `All` node, because it may take a very long time to return results.|  
|**Microsoft Cluster Viewer**|**Cluster Diagram**|Click a cluster node.|  
|**Microsoft Cluster Viewer**|**Cluster Profiles**|Click anywhere in cluster column.|  
|**Microsoft Association Viewer**|**Rules** tab|Click a row that contains a set of rules.|  
|**Microsoft Association Viewer**|**Itemsets** tab|Click a row that contains an itemset.|  
|**Microsoft Sequence Clustering Viewer**|**Rules** tab|Click a row that contains a set of rules.|  
|**Microsoft Sequence Clustering Viewer**|**Itemsets** tab|Click a row that contains an itemset.|  
  
> [!NOTE]  
>  Some models cannot use drillthrough. The ability to use drillthrough depends on the algorithm that was used to create the model. For a list of the mining model types that support drillthrough, see [Drillthrough Queries &#40;Data Mining&#41;](drillthrough-queries-data-mining.md).  
  
### To view drillthrough data from a mining model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the mining structure that contains the mining model you want.  
  
2.  In Data Mining Designer, click the **Mining Model Viewer** tab.  
  
3.  Select the model from the **Mining Model** drop-down list.  
  
4.  Select a viewer from the **Viewer** drop-down list, and right-click the specific node.  
  
5.  Select **Drill Through**, and then select either **Models Columns Only**, or **Model and Structure Columns** to open the **Drill Through** window.  
  
6.  To copy the data to the Clipboard, right-click any row in the table, and select **Copy All**.  
  
## See Also  
 [Drillthrough Queries &#40;Data Mining&#41;](drillthrough-queries-data-mining.md)  
  
  
