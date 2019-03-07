---
title: "Dependency Network Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.dependencynetwork.f1"
ms.assetid: e58ce1b7-20d6-42cb-ade5-916da8471e09
author: minewiskan
ms.author: owend
manager: craigg
---
# Dependency Network Tab (Mining Model Viewer)
  The **Dependency Net** tab provides a graphical view of all attributes that the mining model contains, and shows how the attributes are related.  
  
 The **Dependency Net**  tab is used for several types of mining models, including Naïve Bayes models, decision tree models, and association models. For more information about how to interpret the contents of the **Dependency Net**  tab in the context of these models, see the following links:  
  
 [Browse a Model Using the Microsoft Tree Viewer](data-mining/browse-a-model-using-the-microsoft-tree-viewer.md)  
  
 [Browse a Model Using the Microsoft Naive Bayes Viewer](data-mining/browse-a-model-using-the-microsoft-naive-bayes-viewer.md)  
  
 [Browse a Model Using the Microsoft Association Rules Viewer](data-mining/browse-a-model-using-the-microsoft-association-rules-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model to view, from those in the current mining structure. The mining model will open in a custom viewer. The type of custom viewer that is used for each model is determined by the algorithm that you used to create the model.  
  
 **Viewer**  
 Choose a viewer to use to explore the selected mining model. For each model, you can use either the custom viewer is provided for each mining model, or the [!INCLUDE[msCoName](../includes/msconame-md.md)] Mining Content Viewer. You can also use plug-in viewers if available. The Microsoft Content Tree Viewer can be used with all models, and represents the model content in an HTML table.  
  
 **Zoom In**  
 Zoom in to the diagram, so that you can see the attributes in more detail.  
  
 **Zoom Out**  
 Zoom out from the diagram, so that you can see more attributes and the links between them.  
  
 **Copy Graph View**  
 Copy the visible section of the diagram to the clipboard.  
  
 **Copy Entire Graph**  
 Copy the complete diagram to the clipboard.  
  
 **Links**  
 Adjust how many links (edges) and nodes the viewer shows by adjusting the slider to the right of the attributes. Dragging the slider bar down increases the threshold value, so that only the strongest links are shown. For each model type, a slightly different value is used to represent the links in the graph:  
  
-   In a **decision tree** model, the edges represent the predictive strength of the connection, determined partly by the split score.  
  
-   In a **Naïve Bayes** model, the links between two nodes can go either way: that is, node A can predict the node B, and vice versa. The score associated with the edge represents the predictive strength of the connection.  
  
-   In an **association model**, the edges between nodes represent the importance score of the rule that connects two itemsets.  
  
 A general rule for all model types is that the stronger the link, the stronger the predictive relationship between the two attributes.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
