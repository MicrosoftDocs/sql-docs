---
title: "Decision Tree Tab (Mining Model Viewer) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.decisiontree.f1"
ms.assetid: dc88606f-ba7c-4f8d-af65-bfa17ec16e2b
author: minewiskan
ms.author: owend
manager: craigg
---
# Decision Tree Tab (Mining Model Viewer)
  The **DecisionTree** pane displays a visual representation of the decision rules that are created in a decision tree model. Decision rules describe path paths towards a certain outcome.  
  
 **For More Information:** [Microsoft Decision Trees Algorithm](data-mining/microsoft-decision-trees-algorithm.md), [Browse a Model Using the Microsoft Tree Viewer](data-mining/browse-a-model-using-the-microsoft-tree-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model from those in the current mining structure. The mining model will open in its associated viewer.  
  
 **Viewer**  
 Choose a viewer to use to explore the selected mining model. You can use the custom viewer, or the [!INCLUDE[msCoName](../includes/msconame-md.md)] Mining Content Viewer. You can also use plug-in viewers if available.  
  
 **Zoom In**  
 Zoom in to get a more detailed view of the nodes and branches in the decision tree. In a complex model, decision trees can have many levels of branching.  
  
 **Zoom Out**  
 Zoom out to get an overall view of the tree structure.  
  
 **Copy Graph View**  
 Copy the visible section of the diagram to the clipboard.  
  
 **Copy Entire Graph**  
 Copy the complete diagram to the clipboard.  
  
 **Scale diagram to fit window**  
 Zoom out from the diagram until the whole tree structure fits within the screen.  
  
 **Histograms**  
 Select the number of states that can be displayed in the histogram for each node. If the number of states in the model is fewer than the value you select, no additional bars are displayed.  
  
 **Tree**  
 Choose a tree to display in the viewer. If you create a model that has multiple predictable attributes, the algorithm creates a separate tree for each predictable attribute.  
  
 **Background**  
 Choose a value of the predictable attribute to use in representing the background color of each node. For example, in the sample AdventureWorks models, if you set **Background** to 1 ([Bike Buyer] = Yes), the nodes are shaded darker if they have a greater proportion of bike buyers. This option provides an additional visual cue about the meaning of the branches and nodes in the tree.  
  
 **Default Expansion**  
 Choose a value from the list to set the default for the number of levels displayed in the tree graph.  
  
 **Show Level**  
 Move the slider bar right or left to adjust the number of levels that are displayed in the tree graph. To see all the nodes in the model, slide the bar all the way to the right.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)  
  
  
