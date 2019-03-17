---
title: "Sequence Clustering Cluster Characteristics Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.sequenceclustering.characteristics.f1"
ms.assetid: 3a9e8a0c-7d03-47cc-8625-e68d73a8c947
author: minewiskan
ms.author: owend
manager: craigg
---
# Sequence Clustering Cluster Characteristics Tab (Mining Model Viewer)
  The **Cluster Characteristics** tab in the **Microsoft Sequence Clustering Viewer** provides a detailed list of the characteristics that define a sequence cluster. Those characteristics can include simple attribute-value pairs as well as transitions between states.  
  
 Use this view of a sequence clustering model to drill down into the cluster contents, and see the sequences that are representative of a cluster.  
  
 **For More Information:** [Microsoft Sequence Clustering Algorithm](data-mining/microsoft-sequence-clustering-algorithm.md), [Browse a Model Using the Microsoft Sequence Cluster Viewer](data-mining/browse-a-model-using-the-microsoft-sequence-cluster-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model to view that is contained in the current mining structure. The mining model will open in its associated viewer.  
  
 **Viewer**  
 Choose a viewer to use in exploring the selected mining model. You can use the custom viewer, or the **Microsoft Generic Content Tree Viewer**. You can also use plug-in viewers if available.  
  
 **Cluster**  
 Choose the cluster you want to view.  
  
 **Characteristics for \<Cluster>**  
 This table provides a list of the sequences that were assigned to the current cluster, ordered by probability. Remember that a sequence is basically one attribute-value pair, followed by one or more additional attribute-value pairs. The combination of sequences and their probabilities are the defining characteristics of each cluster.  
  
 For example, in a sequence clustering model based on market basket analysis, one cluster might have as its top characteristic a customer choosing the sale item and then ending the transaction without purchasing anything more. In a sequence clustering model that seeks to analyze server failures, the primary characteristics of a cluster might be a series of high frequency error events.  
  
|Value|Description|  
|-----------|-----------------|  
|**Variable**|This column indicates whether the characteristic is a value, or a transition.<br /><br /> If the characteristic is a value, the **Variable** column contains the attribute name.<br /><br /> If the characteristic represents a state transition, the **Variable** column contains the text "Transitions".|  
|**Values**|The value of this column depends on whether the characteristic is a simple attribute-value pair, or a state transition representing a common sequence of items or events.<br /><br /> If the characteristic is a value, the **Value** column contains the state.<br /><br /> If the characteristic represents a state transition, the **Value** column contains the description of the state transition.|  
|**Probability**|This column displays a bar that indicates the relative probability that this characteristic (either a simple attribute-value pair, or some combination of states) are members of the current cluster.<br /><br /> You can hover the mouse over the par to display the frequency value of the characteristic.|  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
