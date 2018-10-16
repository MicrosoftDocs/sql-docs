---
title: "Sequence Clustering Cluster Discrimination Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.sequenceclustering.discrimination.f1"
ms.assetid: 7dd16479-2633-4f4b-83bf-cf55972a2241
author: minewiskan
ms.author: owend
manager: craigg
---
# Sequence Clustering Cluster Discrimination Tab (Mining Model Viewer)
  The  **Cluster Discrimination** tab in the **Microsoft Sequence Clustering Viewer** compares selected clusters from a sequence clustering model.  
  
 Use this view of a sequence clustering model to compare two clusters and see which states and transitions are different.  
  
 **For More Information:** [Microsoft Sequence Clustering Algorithm](data-mining/microsoft-sequence-clustering-algorithm.md), [Browse a Model Using the Microsoft Sequence Cluster Viewer](data-mining/browse-a-model-using-the-microsoft-sequence-cluster-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model to view that is contained in the current mining structure. The mining model will open in its associated viewer.  
  
 **Viewer**  
 Choose a viewer to use in exploring the selected mining model. You can use the custom viewer, or the **Microsoft Generic Content Tree Viewer**. You can also use plug-in viewers if available.  
  
 **Cluster 1**  
 Select a cluster from those in the model.  
  
 **Cluster 2**  
 Select a second cluster in the mining model, to compare to **Cluster 1**.  
  
 If you do not select another cluster, by default the selected cluster is compared to its complement, meaning all cases in the model that are not in Cluster 1.  
  
 **Discrimination scores for \<cluster 1> and \<cluster 2>**  
 This chart provides the detailed comparison of the clusters that you selected. In general, a clustering model rarely assigns states or values exclusively to a single cluster. Therefore, the viewer indicates only that a particular attribute or state *favors* a particular cluster.  
  
 Overall, a certain cluster might contain more of one state: for example, a common state might be the purchase of a Water Bottle and Water Bottle Cage in sequence. However, the sequence might be present in other clusters that have more important defining characteristics. For example, another cluster might be characterized most strongly by very short transaction times, and an analysis would reveal that [Water Bottle and Waterthe items are positioned to might usually be grouped in this cluster, but not always.  
  
|Value|Description|  
|-----------|-----------------|  
|**Variables**|An attribute in the mining model.|  
|**Values**|A state of the attribute that is listed in **Variables**.|  
|**Favors \<cluster 1>**|Contains a shaded bar that indicates how strongly the attribute and the state that are listed in **Variables** and **Value** favor the cluster that is selected in **Cluster 1**.|  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
