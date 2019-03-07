---
title: "Cluster Diagram Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.clustering.diagram.f1"
ms.assetid: 180e6f48-5c4d-4160-b84d-608b98f7b840
author: minewiskan
ms.author: owend
manager: craigg
---
# Cluster Diagram Tab (Mining Model Viewer)
  The **Cluster Diagram** tab provides a graphical view of all the clusters that the clustering model contains.  
  
 **For More Information:** [Microsoft Clustering Algorithm](data-mining/microsoft-clustering-algorithm.md), [Browse a Model Using the Microsoft Cluster Viewer](data-mining/browse-a-model-using-the-microsoft-cluster-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model, from those in the current mining structure. The mining model will open in its associated viewer.  
  
 **Viewer**  
 Choose a viewer to explore the selected mining model. You can use one of the custom clustering viewers, or use the [!INCLUDE[msCoName](../includes/msconame-md.md)] Mining Content Viewer. You can also use a plug-in viewer if available.  
  
 **Zoom In**  
 Zoom in to the diagram, to get a detailed view of the clusters.  
  
 **Zoom Out**  
 Zoom out from the diagram, to see more clusters.  
  
 **Copy Graph View**  
 Copy the visible section of the diagram to the clipboard.  
  
 **Copy Entire Graph**  
 Copy the complete diagram to the clipboard.  
  
 **Scale diagram to fit window**  
 Zoom out from the diagram until the whole diagram fits within the screen.  
  
 **Find Node**  
 Opens the **Find Node** dialog box. This feature is useful in large models, where it can be hard to find the attribute of interest. You can enter search criteria in the dialog box and the view of the clusters will be filtered to show only the cluster containing the search string.  
  
 **Improve Layout**  
 Reorder the clusters in the diagram to improve the layout.  
  
 **Density**  
 Use this option to change which attribute-value pairs are displayed in the cluster diagram. You use the **Shading Variable** option to select an attribute, and use **State** to choose a value. The shading in the graph indicates the density of that attribute-value pair within the cluster.  
  
 If **Population** is selected, the diagram shows the amount of support for each cluster, meaning the number of cases since no attribute is selected.  
  
 **Shading Variable**  
 Select an attribute to represent in the cluster diagram.  
  
 **State**  
 Select a single state of the **Shading Variable** to use in the cluster diagram.  
  
 **Links**  
 Adjust how many links are shown between clusters, by moving the slider up or down. Lowering the slider leaves only the strongest associations between clusters.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
