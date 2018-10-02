---
title: "Attribute Characteristics Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.naivebayse.characteristics.f1"
ms.assetid: f0c3350d-84c0-4ab8-9fb8-1527c2647299
author: minewiskan
ms.author: owend
manager: craigg
---
# Attribute Characteristics Tab (Mining Model Viewer)
  Use the **Attribute Characteristics** pane to explore the relationships between outcomes and input attributes in a Naïve Bayes model. You can choose the value of the target attribute, and then see a list of the input attributes that have the strongest effect on the outcomes.  
  
 **For More Information:** [Microsoft Naive Bayes Algorithm](data-mining/microsoft-naive-bayes-algorithm.md), [Browse a Model Using the Microsoft Naive Bayes Viewer](data-mining/browse-a-model-using-the-microsoft-naive-bayes-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model to view, from the models in the current mining structure. The mining model will automatically open in a custom viewer that is best for the particular type of model you chose.  
  
 **Viewer**  
 Choose a viewer to use to explore the selected mining model. For each model, you have the choice of a custom viewer, or the [!INCLUDE[msCoName](../includes/msconame-md.md)] Mining Content Viewer. Plug-in viewers also appear in this list if available.  
  
 **Attribute**  
 Choose the predictable attribute that you want to analyze.  
  
 **Value**  
 Choose a state for the predictable attribute that you set in **Attribute**. Because Naïve Bayes models do not support continuous variables, all target attributes have discrete or discretized outcomes. The Missing attribute is always automatically added to the list.  
  
 **Characteristics for \<predictable state>**  
 The graph contains the following columns, which describe how states of the input attributes are related to the selected predictable attribute state.  
  
|Value|Description|  
|-----------|-----------------|  
|**Variable**|Lists the input attributes in the mining model.|  
|**Values**|Lists each state of the input attribute in **Variable**.|  
|**Probability**|The bar represents the probability that the attribute and value in that row are associated with the selected state of the predictable attribute. Hover the mouse over the bar to view the probability as a percentage.|  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
